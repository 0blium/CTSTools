using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Quality.QMS.Document;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        //Step 2. Change statuses if needed
        if (_documentRevisionList.Count() > 0)
        {
            var _revisionToBeObsoleteList = new List<DocumentRevisionDTO>();
            // Add new status and validate fields
            foreach (var _revisionToBeObsoleteDTO in _documentRevisionList)
            {
                _revisionToBeObsoleteDTO.StatusID = (int)QMS_Document.Obsolete;
                _revisionToBeObsoleteDTO.LastUpdateByID = DocumentRevisionDTO.AddedByID;
                _revisionToBeObsoleteDTO.LastUpdate = DateTime.Now;
                _validationResultDTO = DocumentRevision_Validator.UpdateDocumentRevision_Validation(_revisionToBeObsoleteDTO);
                if (!_validationResultDTO.Result)
                    return _validationResultDTO;
                _revisionToBeObsoleteList.Add(_revisionToBeObsoleteDTO);
            }
            // Update multiple records
            _validationResultDTO = DocumentRevision_Repository.UpdateMultipleDocumentRevision(_revisionToBeObsoleteList);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
        }

        //Step 3. Validate fields
        _validationResultDTO = DocumentRevision_Validator.CreateDocumentRevision_Validation(DocumentRevisionDTO);
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
        _documentDTO.LastUpdateByID = DocumentRevisionDTO.AddedByID;
        _documentDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Document_Service.UpdateDocument_Global(_documentDTO);
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
        var _documentRevisionglobalList = new List<DocumentRevisionDTO>();
        try
        {
            var _documentRevisionList = DocumentRevision_Repository.GetDocumentRevisionList(DocumentRevisionDTO, PagedResultDTO);
            // if DocumentRevision is empty, return list
            if (_documentRevisionList.Count() == 0)
            {
                _documentRevisionglobalList = _documentRevisionList;
                return _documentRevisionglobalList;
            }
            if (!DocumentRevisionDTO.GetDocumentDTO && !DocumentRevisionDTO.GetStatusDTO)
            {
                _documentRevisionglobalList = _documentRevisionList;
                return _documentRevisionglobalList;
            }
            _documentRevisionglobalList = GetDocumentRevisionRelatedData(DocumentRevisionDTO, _documentRevisionList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentRevisionglobalList;
    }



    public static List<DocumentRevisionDTO> GetDocumentRevisionRelatedData(DocumentRevisionDTO DocumentRevisionDTO, List<DocumentRevisionDTO> DocumentRevisionList)
    {
        var _documentRevisionglobalList = new List<DocumentRevisionDTO>();
        var _documentDict = new Dictionary<int?, DocumentDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();

        try
        {
            if (DocumentRevisionDTO.GetDocumentDTO)
            {
                DocumentRevisionDTO.DocumentDTO.DocumentIDArray = DocumentRevisionList.GroupBy(g => g.DocumentID)
                        .Select(s => s.Key)
                        .ToArray();

                _documentDict = Document_Service.GetDocumentList_Global(DocumentRevisionDTO.DocumentDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentRevisionDTO.GetStatusDTO)
            {
                DocumentRevisionDTO.StatusDTO.StatusIDArray = DocumentRevisionList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(DocumentRevisionDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _documentRevisionDTO in DocumentRevisionList)
            {
                if (DocumentRevisionDTO.GetDocumentDTO && _documentDict.ContainsKey(_documentRevisionDTO.DocumentID))
                {
                    _documentRevisionDTO.DocumentDTO = _documentDict[_documentRevisionDTO.DocumentID];
                }
                if (DocumentRevisionDTO.GetStatusDTO && _statusDict.ContainsKey(_documentRevisionDTO.StatusID))
                {
                    _documentRevisionDTO.StatusDTO = _statusDict[_documentRevisionDTO.StatusID];
                }
                _documentRevisionglobalList.Add(_documentRevisionDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentRevisionglobalList;
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

    // Aqui va la logica 
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
    #endregion
}
