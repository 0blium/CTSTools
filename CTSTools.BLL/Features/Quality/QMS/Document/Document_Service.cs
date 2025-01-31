using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Directories;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Quality.QMS.Customer;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using CTSTools.BLL.Features.Quality.QMS.Product;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class Document_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDocument_Global(DocumentDTO DocumentDTO)
    {
        //Step 1. 
        DocumentDTO.StatusID = (int)Status_Enum.QMS_Document.New;
        var _validationResultDTO = Document_Validator.CreateDocument_Validation(DocumentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. 
        DocumentDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Document_Repository.CreateDocument(DocumentDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 3. 
        _validationResultDTO = CreateDocumentDirectory(DocumentDTO);
        return _validationResultDTO;
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
            if (!DocumentDTO.GetDepartmentDTO && !DocumentDTO.GetTypeDTO && !DocumentDTO.GetStatusDTO && !DocumentDTO.GetCustomerDTO && !DocumentDTO.GetProductDTO)
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
        var _customerDict = new Dictionary<int?, CustomerDTO>();
        var _productDict = new Dictionary<int?, ProductDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();

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
            if (DocumentDTO.GetCustomerDTO)
            {
                DocumentDTO.CustomerDTO.CustomerIDArray = DocumentList.GroupBy(g => g.CustomerID)
                        .Select(s => s.Key)
                        .ToArray();

                _customerDict = Customer_Service.GetCustomerList_Global(DocumentDTO.CustomerDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetProductDTO)
            {
                DocumentDTO.ProductDTO.ProductIDArray = DocumentList.GroupBy(g => g.ProductID)
                        .Select(s => s.Key)
                        .ToArray();

                _productDict = Product_Service.GetProductList_Global(DocumentDTO.ProductDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DocumentDTO.GetStatusDTO)
            {
                DocumentDTO.StatusDTO.StatusIDArray = DocumentList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(DocumentDTO.StatusDTO)
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
                if (DocumentDTO.GetCustomerDTO && _customerDict.ContainsKey(_documentDTO.CustomerID))
                {
                    _documentDTO.CustomerDTO = _customerDict[_documentDTO.CustomerID];
                }
                if (DocumentDTO.GetProductDTO && _productDict.ContainsKey(_documentDTO.ProductID))
                {
                    _documentDTO.ProductDTO = _productDict[_documentDTO.ProductID];
                }
                if (DocumentDTO.GetStatusDTO && _statusDict.ContainsKey(_documentDTO.StatusID))
                {
                    _documentDTO.StatusDTO = _statusDict[_documentDTO.StatusID];
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
    public static ValidationResultDTO CreateDocumentDirectory(DocumentDTO DocumentDTO) {
        var _validationResultDTO = new ValidationResultDTO();   
        try
        {
            var _documentTypeDTO = new DocumentTypeDTO { ID = DocumentDTO.TypeID };
            _documentTypeDTO = DocumentType_Service.GetDocumentTypeByID_Global(_documentTypeDTO);
            string _path = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{_documentTypeDTO.Name}\\{DocumentDTO.Number}\\";
            _validationResultDTO = Directory_Service.CreateDirectory(_path);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }

    #endregion
}
