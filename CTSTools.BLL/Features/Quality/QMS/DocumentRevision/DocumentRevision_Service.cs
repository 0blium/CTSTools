using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Directories;
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
    public static ValidationResultDTO Create_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        // Step 1. Assign Revision
        var _documentRevisionDTO = new DocumentRevisionDTO { DocumentID = DocumentRevisionDTO.DocumentID };
        var _documentRevisionList = GetList_Global(_documentRevisionDTO);
        var _lastDocumentRevisionDTO = _documentRevisionList.LastOrDefault();
        DocumentRevisionDTO.Revision = (_lastDocumentRevisionDTO == null) ? "A" : GenerateNewRevisionSequence(_lastDocumentRevisionDTO.Revision);
        DocumentRevisionDTO.StatusID = (int)QMS_Document.Released;
        //Step 2. Validate fields
        _validationResultDTO = DocumentRevision_Validator.Create_Validation(DocumentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Change statuses if needed
        if (_documentRevisionList.Count != 0)
        {
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
            _validationResultDTO = DocumentRevision_Repository.UpdateMultiple(_revisionToBeObsoleteList);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
        }
        // Step 4. Create new document revision
        DocumentRevisionDTO.AddedDate = DateTime.Now;
        _validationResultDTO = DocumentRevision_Repository.Create(DocumentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 5. Update Document status

        var _documentDTO = new DocumentDTO
        {
            ID = DocumentRevisionDTO.DocumentID,
            GetTypeDTO = true
        };
        _documentDTO = Document_Service.GetByID_Global(_documentDTO);
        _documentDTO.StatusID = DocumentRevisionDTO.StatusID;
        _documentDTO.LastRevision = DocumentRevisionDTO.Revision;
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.AddedByID;
        _documentDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Document_Service.Update_Global(_documentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 6. Upload a file
        DocumentRevisionDTO.FileDTO.URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{_documentDTO.FolderName}\\{_documentDTO.Number}\\{DocumentRevisionDTO.Revision}\\";
        _validationResultDTO = File_Service.CreateFile(DocumentRevisionDTO.FileDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO Update_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        // Step 1. Validate Fields
        var _ValidationResultDTO = DocumentRevision_Validator.Update_Validation(DocumentRevisionDTO);
        if (!_ValidationResultDTO.Result)
            return _ValidationResultDTO;

        //Step 2. Update Document Revision
        DocumentRevisionDTO.LastUpdate = DateTime.Now;
        _ValidationResultDTO = DocumentRevision_Repository.Update(DocumentRevisionDTO);
        if (!_ValidationResultDTO.Result)
            return _ValidationResultDTO;

        //Step 3. Update Document  status
        var _documentDTO = Document_Repository.GetByID((int)DocumentRevisionDTO.DocumentID);
        _documentDTO.StatusID = DocumentRevisionDTO.StatusID;
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.LastUpdateByID;
        _documentDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Document_Service.Update_Global(_documentDTO);
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Delete_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        // Step 1. Validate Fields
        var _validationResultDTO = DocumentRevision_Validator.Delete_Validation(DocumentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2. Delete Revision Directory
        DocumentRevisionDTO.GetDocumentDTO = true;
        DocumentRevisionDTO = GetByID_Global(DocumentRevisionDTO);
        DocumentRevisionDTO.FileDTO.URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{DocumentRevisionDTO.DocumentDTO.FolderName}\\{DocumentRevisionDTO.DocumentDTO.Number}\\{DocumentRevisionDTO.Revision}\\";
        _validationResultDTO = Directory_Service.Delete(DocumentRevisionDTO.FileDTO.URL);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 3. Delete Document Revision
        _validationResultDTO = DocumentRevision_Repository.Delete(DocumentRevisionDTO);

        return _validationResultDTO;
    }
    public static List<DocumentRevisionDTO> GetList_Global(DocumentRevisionDTO DocumentRevisionDTO, PagedResultDTO<DocumentRevisionDTO> PagedResultDTO = null)
    {
        var _documentRevisionList = new List<DocumentRevisionDTO>();
        try
        {
            _documentRevisionList = DocumentRevision_Repository.GetList(DocumentRevisionDTO, PagedResultDTO);
            if (_documentRevisionList.Count() == 0 || !DocumentRevisionDTO.GetDocumentDTO && !DocumentRevisionDTO.GetStatusDTO && DocumentRevisionDTO.GetFileDTO)
                return _documentRevisionList;

            DocumentRevisionDTO = GetRelatedData(DocumentRevisionDTO, _documentRevisionList);
            _documentRevisionList = DocumentRevisionMap.DictionariesToList(DocumentRevisionDTO, _documentRevisionList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentRevisionList;
    }
    public static DocumentRevisionDTO GetByID_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        try
        {
            DocumentRevisionDTO = DocumentRevision_Repository.GetByID((int)DocumentRevisionDTO.ID);
            // if DocumentRevision is empty, return list
            if (DocumentRevisionDTO == null || !DocumentRevisionDTO.GetDocumentDTO && !DocumentRevisionDTO.GetStatusDTO && !DocumentRevisionDTO.GetFileDTO)
                return DocumentRevisionDTO;
            DocumentRevisionDTO = GetRelatedData(DocumentRevisionDTO);
            DocumentRevisionDTO = DocumentRevisionMap.DictionaryToDTO(DocumentRevisionDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return DocumentRevisionDTO;
    }
    internal static DocumentRevisionDTO GetRelatedData(DocumentRevisionDTO DocumentRevisionDTO, List<DocumentRevisionDTO> DocumentRevisionList = null)
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

                DocumentRevisionDTO.DocumentDict = Document_Service.GetList_Global(DocumentRevisionDTO.DocumentDTO)
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
    public static int GetTotalCount(PagedResultDTO<DocumentRevisionDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DocumentRevision_Repository.GetCount(PagedResultDTO.Filter, PagedResultDTO);
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

        var _documentDTO = new DocumentDTO
        {
            ID = DocumentRevisionDTO.DocumentID,
            GetTypeDTO = true
        };
        _documentDTO = Document_Service.GetByID_Global(_documentDTO);
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.LastUpdateByID;
        _documentDTO.LastUpdate = DateTime.Now;
        var _validationResultDTO = Document_Service.Update_Global(_documentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step 2. Update Document status
        var _documentRevisionDTO = GetByID_Global(DocumentRevisionDTO);
        _documentRevisionDTO.LastUpdate = DateTime.Now;
        _documentRevisionDTO.LastUpdateByID = DocumentRevisionDTO.LastUpdateByID;
        _validationResultDTO = Update_Global(_documentRevisionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //Step 3. Upload a file
        DocumentRevisionDTO.FileDTO.URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{_documentDTO.FolderName}\\{_documentDTO.Number}\\{_documentRevisionDTO.Revision}\\";
        _validationResultDTO = File_Service.UpdateFile_Global(DocumentRevisionDTO.FileDTO);
        return _validationResultDTO;
    }
    #endregion
}
