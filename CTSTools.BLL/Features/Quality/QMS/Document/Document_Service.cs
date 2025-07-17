using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Directories;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.Customer;
using CTSTools.BLL.Features.Quality.QMS.DocumentRevision;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using CTSTools.BLL.Features.AdvancedSettings.Product;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class Document_Service
{
    #region Global CRUD
    public static ValidationResultDTO Create_Global(DocumentDTO DocumentDTO)
    {
        //Step 1. 
        DocumentDTO.StatusID = (int)Status_Enum.QMS_Document.New;
        var _validationResultDTO = Document_Validator.Create_Validation(DocumentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. 
        DocumentDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Document_Repository.Create(DocumentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 3. 
        _validationResultDTO = CreateDocumentDirectory(DocumentDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO Update_Global(DocumentDTO DocumentDTO)
    {
        var _validationResultDTO = Document_Validator.Update_Validation(DocumentDTO);
        DocumentDTO.LastUpdate = DateTime.Now;
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        _validationResultDTO = Document_Repository.Update(DocumentDTO);

        return _validationResultDTO;
    }
    public static ValidationResultDTO Delete_Global(DocumentDTO DocumentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        // Step 1. Delete Document Revisions
        DocumentDTO = GetByID_Global(DocumentDTO);
        var _documentRevisionDTO = new DocumentRevisionDTO
        {
            DocumentID = DocumentDTO.ID
        };
        var _documentRevisionList = DocumentRevision_Service.GetList_Global(_documentRevisionDTO);
        if (_documentRevisionList.Count() > 0)
            _validationResultDTO = DocumentRevision_Repository.DeleteMultiple(_documentRevisionList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2. Validate fields
        _validationResultDTO = Document_Validator.Delete_Validation(DocumentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step 3. Delete Folder
        DocumentDTO.FileDTO.URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{DocumentDTO.FolderName}\\{DocumentDTO.Number}\\";
        _validationResultDTO = Directory_Service.Delete(DocumentDTO.FileDTO.URL);
        // Step 3. Delete Document
        _validationResultDTO = Document_Repository.Delete(DocumentDTO);

        return _validationResultDTO;
    }
    public static List<DocumentDTO> GetList_Global(DocumentDTO DocumentDTO, PagedResultDTO<DocumentDTO> PagedResultDTO = null)
    {
        var _documentList = new List<DocumentDTO>();
        try
        {
            _documentList = Document_Repository.GetList(DocumentDTO, PagedResultDTO);
            if (_documentList.Count() == 0)
                return _documentList;

            DocumentDTO.GetTypeDTO = DocumentDTO.GetTypeDTO;
            DocumentDTO.GetStatusDTO = DocumentDTO.GetStatusDTO;
            DocumentDTO.GetCustomerDTO = DocumentDTO.GetCustomerDTO;
            DocumentDTO.GetProductDTO = DocumentDTO.GetProductDTO;
            DocumentDTO.GetDepartmentDTO = DocumentDTO.GetDepartmentDTO;
            DocumentDTO.GetFileDTO = DocumentDTO.GetFileDTO;
            DocumentDTO = GetRelatedData(DocumentDTO, _documentList);
            _documentList = DocumentMap.DictionariesToList(DocumentDTO, _documentList);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _documentList;
    }
    public static DocumentDTO GetByID_Global(DocumentDTO DocumentDTO)
    {
        try
        {
            var _documentDTO = Document_Repository.GetByID((int)DocumentDTO.ID);
            if (_documentDTO == null)
                return _documentDTO;

            _documentDTO.GetTypeDTO = DocumentDTO.GetTypeDTO;
            _documentDTO.GetStatusDTO = DocumentDTO.GetStatusDTO;
            _documentDTO.GetCustomerDTO = DocumentDTO.GetCustomerDTO;
            _documentDTO.GetProductDTO = DocumentDTO.GetProductDTO;
            _documentDTO.GetDepartmentDTO = DocumentDTO.GetDepartmentDTO;
            _documentDTO.GetFileDTO = DocumentDTO.GetFileDTO;
            DocumentDTO = GetRelatedData(_documentDTO);
            DocumentDTO = DocumentMap.DictionaryToDTO(DocumentDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return DocumentDTO;
    }

    internal static DocumentDTO GetRelatedData(DocumentDTO DocumentDTO, List<DocumentDTO> DocumentList = null)
    {
        try
        {
            if (DocumentDTO.GetDepartmentDTO)
            {
                DocumentDTO.DepartmentDTO.DepartmentIDArray = DocumentList == null ?
                                                              [DocumentDTO.DepartmentID] :
                                                              DocumentList.GroupBy(g => g.DepartmentID)
                                                                          .Select(s => s.Key)
                                                                          .ToArray();

                DocumentDTO.DepartmentDict = Department_Service.GetDepartmentList_Global(DocumentDTO.DepartmentDTO)
                                                               .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetTypeDTO)
            {
                DocumentDTO.TypeDTO.DocumentTypeIDArray = DocumentList == null ?
                                                          [DocumentDTO.TypeID] :
                                                          DocumentList.GroupBy(g => g.TypeID)
                                                                      .Select(s => s.Key)
                                                                      .ToArray();

                DocumentDTO.DocumentTypeDict = DocumentType_Service.GetList_Global(DocumentDTO.TypeDTO)
                                                                   .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetCustomerDTO)
            {
                DocumentDTO.CustomerDTO.CustomerIDArray = DocumentList == null ?
                                                          [DocumentDTO.CustomerID] :
                                                          DocumentList.GroupBy(g => g.CustomerID)
                                                                      .Select(s => s.Key)
                                                                      .ToArray();

                DocumentDTO.CustomerDict = Customer_Service.GetList_Global(DocumentDTO.CustomerDTO)
                                                           .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetProductDTO)
            {
                DocumentDTO.ProductDTO.ProductIDArray = DocumentList == null ?
                                                        [DocumentDTO.ProductID] :
                                                        DocumentList.GroupBy(g => g.ProductID)
                                                                    .Select(s => s.Key)
                                                                    .ToArray();

                DocumentDTO.ProductDict = Product_Service.GetList_Global(DocumentDTO.ProductDTO)
                                              .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetStatusDTO)
            {
                DocumentDTO.StatusDTO.StatusIDArray = DocumentList == null ?
                                                      [DocumentDTO.StatusID] :
                                                      DocumentList.GroupBy(g => g.StatusID)
                                                                  .Select(s => s.Key)
                                                                  .ToArray();

                DocumentDTO.StatusDict = Status_Service.GetStatusList_Global(DocumentDTO.StatusDTO)
                                                       .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return DocumentDTO;
    }
    public static int GetTotalCount(PagedResultDTO<DocumentDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Document_Repository.GetCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO CreateDocumentDirectory(DocumentDTO DocumentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _documentTypeDTO = new DocumentTypeDTO
            {
                ID = DocumentDTO.TypeID
            };
            _documentTypeDTO = DocumentType_Service.GetByID_Global(_documentTypeDTO);
            string _path = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{_documentTypeDTO.FolderName}\\{DocumentDTO.Number}\\";
            _validationResultDTO = Directory_Service.Create(_path);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }

    #endregion
}
