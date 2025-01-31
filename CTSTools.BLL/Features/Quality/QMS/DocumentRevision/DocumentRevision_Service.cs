using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Quality.QMS.Document;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status.Status_Enum;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevision_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDocumentRevision_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        // Step 1. Assign Revision
        var _documentRevisionDTO = new DocumentRevisionDTO { DocumentID = DocumentRevisionDTO.DocumentID };
        var _documentRevisionList = GetDocumentRevisionList_Global(_documentRevisionDTO);
        var _lastDocumentRevisionDTO = _documentRevisionList.LastOrDefault();
        DocumentRevisionDTO.Revision = (_lastDocumentRevisionDTO == null) ? "A" : GenerateNewRevisionSequence(_lastDocumentRevisionDTO.Revision);
        DocumentRevisionDTO.StatusID = (int)QMS_Document.Released;
        //Step 2. Validate fields
        _validationResultDTO = DocumentRevision_Validator.CreateDocumentRevision_Validation(DocumentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Change statuses if needed
        if (_documentRevisionList.Count() == 0)
            return _validationResultDTO;

        var _revisionToBeObsoleteList = new List<DocumentRevisionDTO>();
        // Add new status and validate fields
        foreach (var _revisionToBeObsoleteDTO in _documentRevisionList)
        {
            _revisionToBeObsoleteDTO.StatusID = (int)QMS_Document.Obsolete;
            _revisionToBeObsoleteDTO.LastUpdateByID = DocumentRevisionDTO.AddedByID;
            _revisionToBeObsoleteDTO.LastUpdate = DateTime.Now;
            _revisionToBeObsoleteList.Add(_revisionToBeObsoleteDTO);
        }
        // Update multiple records
        _validationResultDTO = DocumentRevision_Repository.UpdateMultipleDocumentRevision(_revisionToBeObsoleteList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step 4. Create new document revision
        DocumentRevisionDTO.AddedDate = DateTime.Now;
        _validationResultDTO = DocumentRevision_Repository.CreateDocumentRevision(DocumentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 5. Update Document status
        var _documentDTO = Document_Repository.GetDocumentByID((int)DocumentRevisionDTO.DocumentID);
        _documentDTO.StatusID = DocumentRevisionDTO.StatusID;
        _documentDTO.LastRevision = DocumentRevisionDTO.Revision;
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.AddedByID;
        _documentDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Document_Service.UpdateDocument_Global(_documentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 6. Upload a file
        DocumentRevisionDTO.FileDTO.URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{_documentDTO.TypeName}\\{_documentDTO.Number}\\{DocumentRevisionDTO.Revision}\\";
        _validationResultDTO = File_Service.CreateFile(DocumentRevisionDTO.FileDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDocumentRevision_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        // Step 1. Validate Fields
        var _ValidationResultDTO = DocumentRevision_Validator.UpdateDocumentRevision_Validation(DocumentRevisionDTO);
        if (!_ValidationResultDTO.Result)
            return _ValidationResultDTO;

        //Step 2. Update Document Revision
        DocumentRevisionDTO.LastUpdate = DateTime.Now;
        _ValidationResultDTO = DocumentRevision_Repository.UpdateDocumentRevision(DocumentRevisionDTO);
        if (!_ValidationResultDTO.Result)
            return _ValidationResultDTO;

        //Step 3. Update Document  status
        var _documentDTO = Document_Repository.GetDocumentByID((int)DocumentRevisionDTO.DocumentID);
        _documentDTO.StatusID = DocumentRevisionDTO.StatusID;
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.LastUpdateByID;
        _documentDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Document_Service.UpdateDocument_Global(_documentDTO);
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDocumentRevision_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _ValidationResultDTO = DocumentRevision_Validator.DeleteDocumentRevision_Validation(DocumentRevisionDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DocumentRevision_Repository.DeleteDocumentRevision(DocumentRevisionDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DocumentRevisionDTO> GetDocumentRevisionList_Global(DocumentRevisionDTO DocumentRevisionDTO, PagedResultDTO<DocumentRevisionDTO> PagedResultDTO = null)
    {
        var _documentRevisionList = new List<DocumentRevisionDTO>();
        try
        {
            _documentRevisionList = DocumentRevision_Repository.GetDocumentRevisionList(DocumentRevisionDTO, PagedResultDTO);
            if (_documentRevisionList.Count() == 0 || !DocumentRevisionDTO.GetDocumentDTO && !DocumentRevisionDTO.GetStatusDTO && DocumentRevisionDTO.GetFileDTO)
                return _documentRevisionList;

            DocumentRevisionDTO = GetDocumentRevisionRelatedData(DocumentRevisionDTO, _documentRevisionList);
            _documentRevisionList = DocumentRevisionMap.DictionariesToList(DocumentRevisionDTO, _documentRevisionList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentRevisionList;
    }
    public static DocumentRevisionDTO GetDocumentRevisionByID_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        try
        {
            DocumentRevisionDTO = DocumentRevision_Repository.GetDocumentByID((int)DocumentRevisionDTO.ID);
            // if DocumentRevision is empty, return list
            if (DocumentRevisionDTO == null || !DocumentRevisionDTO.GetDocumentDTO && !DocumentRevisionDTO.GetStatusDTO && !DocumentRevisionDTO.GetFileDTO)
                return DocumentRevisionDTO;
            DocumentRevisionDTO = GetDocumentRevisionRelatedData(DocumentRevisionDTO);
            DocumentRevisionDTO = DocumentRevisionMap.DictionaryToDTO(DocumentRevisionDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return DocumentRevisionDTO;
    }
    internal static DocumentRevisionDTO GetDocumentRevisionRelatedData(DocumentRevisionDTO DocumentRevisionDTO, List<DocumentRevisionDTO> DocumentRevisionList = null)
    {
        try
        {
            if (DocumentRevisionDTO.GetDocumentDTO)
            {
                DocumentRevisionDTO.DocumentDTO.DocumentIDArray = DocumentRevisionList == null ?
                                                                  [DocumentRevisionDTO.DocumentID] :
                                                                  DocumentRevisionList.GroupBy(g => g.DocumentID)
                                                                                      .Select(s => s.Key)
                                                                                      .ToArray();

                DocumentRevisionDTO.DocumentDict = Document_Service.GetDocumentList_Global(DocumentRevisionDTO.DocumentDTO)
                                                                   .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentRevisionDTO.GetStatusDTO)
            {
                DocumentRevisionDTO.StatusDTO.StatusIDArray = DocumentRevisionList == null ?
                                                              [DocumentRevisionDTO.StatusID] :
                                                              DocumentRevisionList.GroupBy(g => g.StatusID)
                                                                                  .Select(s => s.Key)
                                                                                  .ToArray();

                DocumentRevisionDTO.StatusDict = Status_Service.GetStatusList_Global(DocumentRevisionDTO.StatusDTO)
                                                               .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return DocumentRevisionDTO;

    }
    public static int GetDocumentRevisionTotalCount(PagedResultDTO<DocumentRevisionDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DocumentRevision_Repository.GetDocumentRevisionCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static string GenerateNewRevisionSequence(string Sequence)
    {
        //Convert the revision to a list.
        var _sequenceList = Sequence.ToList();
        //Get the last letter of the revision
        char _letter = _sequenceList[_sequenceList.Count() - 1];
        //If the letter is 'Z', add a new letter and replace the 'Z' to 'A'
        if (_letter == 'Z')
        {
            _sequenceList.Add('A');
            Sequence = string.Join("", _sequenceList);
            Sequence = Sequence.Replace('Z', 'A');
        }
        //If the letter is different from 'Z', we increment the char element to get the next value
        else
        {
            _letter++;
            _sequenceList[_sequenceList.Count() - 1] = _letter;
            Sequence = string.Join("", _sequenceList);
        }
        //return the new sequence
        return Sequence;
    }
    public static ValidationResultDTO UpdateDocumentFile(DocumentRevisionDTO DocumentRevisionDTO)
    {
        //Step 1. Update Document
        var _documentDTO = Document_Repository.GetDocumentByID((int)DocumentRevisionDTO.DocumentID);
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.LastUpdateByID;
        _documentDTO.LastUpdate = DateTime.Now;
        var _validationResultDTO = Document_Service.UpdateDocument_Global(_documentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step 2. Update Document status
        var _documentRevisionDTO = GetDocumentRevisionByID_Global(DocumentRevisionDTO);
        _documentRevisionDTO.LastUpdate = DateTime.Now;
        _documentRevisionDTO.LastUpdateByID = DocumentRevisionDTO.LastUpdateByID;
        _validationResultDTO = UpdateDocumentRevision_Global(_documentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step 3. Upload a file
        DocumentRevisionDTO.FileDTO.URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{_documentDTO.TypeName}\\{_documentDTO.Number}\\{_documentRevisionDTO.Revision}\\";
        _validationResultDTO = File_Service.UpdateFile_Global(DocumentRevisionDTO.FileDTO);
        return _validationResultDTO;
    }
    #endregion
}
