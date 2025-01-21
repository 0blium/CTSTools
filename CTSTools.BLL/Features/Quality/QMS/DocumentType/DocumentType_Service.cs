using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentType;

public class DocumentType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDocumentType_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _ValidationResultDTO = DocumentType_Validator.CreateDocumentType_Validation(DocumentTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DocumentType_Repository.CreateDocumentType(DocumentTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDocumentType_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _ValidationResultDTO = DocumentType_Validator.UpdateDocumentType_Validation(DocumentTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DocumentType_Repository.UpdateDocumentType(DocumentTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDocumentType_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _ValidationResultDTO = DocumentType_Validator.DeleteDocumentType_Validation(DocumentTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DocumentType_Repository.DeleteDocumentType(DocumentTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DocumentTypeDTO> GetDocumentTypeList_Global(DocumentTypeDTO DocumentTypeDTO, PagedResultDTO<DocumentTypeDTO> PagedResultDTO = null)
    {
        var _documentTypeglobalList = new List<DocumentTypeDTO>();
        try
        {
            var _documentTypeList = DocumentType_Repository.GetDocumentTypeList(DocumentTypeDTO, PagedResultDTO);
            // if DocumentType is empty, return list
            _documentTypeglobalList = _documentTypeList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentTypeglobalList;
    }


    public static int GetDocumentTypeTotalCount(PagedResultDTO<DocumentTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DocumentType_Repository.GetDocumentTypeCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
