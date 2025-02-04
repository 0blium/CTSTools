using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentType;

public class DocumentType_Service
{
    #region Global CRUD
    public static ValidationResultDTO Create_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _ValidationResultDTO = DocumentType_Validator.Create_Validation(DocumentTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DocumentType_Repository.Create(DocumentTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Update_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _ValidationResultDTO = DocumentType_Validator.Update_Validation(DocumentTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            DocumentTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DocumentType_Repository.Update(DocumentTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Delete_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _ValidationResultDTO = DocumentType_Validator.Delete_Validation(DocumentTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DocumentType_Repository.Delete(DocumentTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DocumentTypeDTO> GetList_Global(DocumentTypeDTO DocumentTypeDTO, PagedResultDTO<DocumentTypeDTO> PagedResultDTO = null)
    {
        var _documentTypeglobalList = new List<DocumentTypeDTO>();
        try
        {
            var _documentTypeList = DocumentType_Repository.GetList(DocumentTypeDTO, PagedResultDTO);
            // if DocumentType is empty, return list
            _documentTypeglobalList = _documentTypeList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentTypeglobalList;
    }
    public static DocumentTypeDTO GetByID_Global(DocumentTypeDTO DocumentTypeDTO)
    {
        var _documentTypeDTO = new DocumentTypeDTO();
        try
        {
            _documentTypeDTO = DocumentType_Repository.GetByID((int)DocumentTypeDTO.ID);
            // if DocumentType is empty, return list


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentTypeDTO;
    }

    public static int GetTotalCount(PagedResultDTO<DocumentTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DocumentType_Repository.GetCount(PagedResultDTO.Filter, PagedResultDTO);
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
