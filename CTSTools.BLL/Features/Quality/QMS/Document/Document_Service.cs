using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class Document_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDocument_Global(DocumentDTO DocumentDTO)
    {
        //test comment
        var _ValidationResultDTO = Document_Validator.CreateDocument_Validation(DocumentDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Document_Repository.CreateDocument(DocumentDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDocument_Global(DocumentDTO DocumentDTO)
    {
        var _ValidationResultDTO = Document_Validator.UpdateDocument_Validation(DocumentDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Document_Repository.UpdateDocument(DocumentDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDocument_Global(DocumentDTO DocumentDTO)
    {
        var _ValidationResultDTO = Document_Validator.DeleteDocument_Validation(DocumentDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Document_Repository.DeleteDocument(DocumentDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DocumentDTO> GetDocumentList_Global(DocumentDTO DocumentDTO, PagedResultDTO<DocumentDTO> PagedResultDTO = null)
    {
        var _documentglobalList = new List<DocumentDTO>();
        try
        {
            var _documentList = Document_Repository.GetDocumentList(DocumentDTO, PagedResultDTO);
            // if Document is empty, return list
            if (_documentList.Count() == 0)
            {
                _documentglobalList = _documentList;
                return _documentglobalList;
            }
            if (!DocumentDTO.GetDepartmentDTO && !DocumentDTO.GetTypeDTO)
            {
                _documentglobalList = _documentList;
                return _documentglobalList;
            }
            _documentglobalList = GetDocumentRelatedData(DocumentDTO, _documentList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentglobalList;
    }


    public static List<DocumentDTO> GetDocumentRelatedData(DocumentDTO DocumentDTO, List<DocumentDTO> DocumentList)
    {
        var _documentglobalList = new List<DocumentDTO>();
        var _departmentDict = new Dictionary<int?, DepartmentDTO>();
        var _documentTypeDict = new Dictionary<int?, DocumentTypeDTO>();

        try
        {
            if (DocumentDTO.GetDepartmentDTO)
            {
                DocumentDTO.DepartmentDTO.DepartmentIDArray = DocumentList.GroupBy(g => g.DepartmentID)
                        .Select(s => s.Key)
                        .ToArray();

                _departmentDict = Department_Service.GetDepartmentList_Global(DocumentDTO.DepartmentDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetTypeDTO)
            {
                DocumentDTO.TypeDTO.DocumentTypeIDArray = DocumentList.GroupBy(g => g.TypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _documentTypeDict = DocumentType_Service.GetDocumentTypeList_Global(DocumentDTO.TypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _documentDTO in DocumentList)
            {
                if (DocumentDTO.GetDepartmentDTO && _departmentDict.ContainsKey(_documentDTO.DepartmentID))
                {
                    _documentDTO.DepartmentDTO = _departmentDict[_documentDTO.DepartmentID];
                }
                if (DocumentDTO.GetTypeDTO && _documentTypeDict.ContainsKey(_documentDTO.TypeID))
                {
                    _documentDTO.TypeDTO = _documentTypeDict[_documentDTO.TypeID];
                }
                
                _documentglobalList.Add(_documentDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentglobalList;
    }

    public static int GetDocumentTotalCount(PagedResultDTO<DocumentDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Document_Repository.GetDocumentCount(PagedResultDTO.Filter, PagedResultDTO);
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
