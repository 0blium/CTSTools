using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Quality.QMS.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;

public class DocumentRevision_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDocumentRevision_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        //test comment
        var _ValidationResultDTO = DocumentRevision_Validator.CreateDocumentRevision_Validation(DocumentRevisionDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentRevisionDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DocumentRevision_Repository.CreateDocumentRevision(DocumentRevisionDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDocumentRevision_Global(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _ValidationResultDTO = DocumentRevision_Validator.UpdateDocumentRevision_Validation(DocumentRevisionDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentRevisionDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DocumentRevision_Repository.UpdateDocumentRevision(DocumentRevisionDTO);
        }
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

    #endregion
}
