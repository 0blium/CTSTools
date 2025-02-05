using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Quality.QMS.DocumentRevision;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class DocumentMap
{
    public static DocumentDTO XPOToDTO(DocumentXPO DocumentXPO)
    {
        var _documentDTO = new DocumentDTO();
        try
        {
            _documentDTO.ID = DocumentXPO.Oid;
            _documentDTO.Name = DocumentXPO.Name;
            _documentDTO.Number = DocumentXPO.Number;
            _documentDTO.LastRevision = DocumentXPO.LastRevision;
            _documentDTO.Description = DocumentXPO.Description;
            _documentDTO.OwnerID = (DocumentXPO.Owner != null) ? DocumentXPO.Owner.Oid : 0;
            _documentDTO.OwnerName = (DocumentXPO.Owner != null) ? DocumentXPO.Owner.Name : "Unnassigned";
            _documentDTO.DepartmentID = (DocumentXPO.Department != null) ? DocumentXPO.Department.Oid : 0;
            _documentDTO.DepartmentName = (DocumentXPO.Department != null) ? DocumentXPO.Department.Name : "Unnassigned";
            _documentDTO.TypeID = (DocumentXPO.Type != null) ? DocumentXPO.Type.Oid : 0;
            _documentDTO.TypeName = (DocumentXPO.Type != null) ? DocumentXPO.Type.Name : "Unnassigned";
            _documentDTO.FolderName = (DocumentXPO.Type != null) ? DocumentXPO.Type.FolderName : "";
            _documentDTO.CustomerID = (DocumentXPO.Customer != null) ? DocumentXPO.Customer.Oid : 0;
            _documentDTO.CustomerName = (DocumentXPO.Customer != null) ? DocumentXPO.Customer.Name : "Unnassigned";
            _documentDTO.ProductID = (DocumentXPO.Product != null) ? DocumentXPO.Product.Oid : 0;
            _documentDTO.ProductName = (DocumentXPO.Product != null) ? DocumentXPO.Product.Name : "Unnassigned";
            _documentDTO.StatusID = (DocumentXPO.Status != null) ? DocumentXPO.Status.Oid : 0;
            _documentDTO.StatusName = (DocumentXPO.Status != null) ? DocumentXPO.Status.Name : "Unnassigned";
            _documentDTO.AddedDate = (DocumentXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DocumentXPO.AddedDate : (DateTime?)null;
            _documentDTO.AddedByID = (DocumentXPO.AddedBy != null) ? DocumentXPO.AddedBy.Oid : 0;
            _documentDTO.AddedByName = (DocumentXPO.AddedBy != null) ? DocumentXPO.AddedBy.Name : "Unnassigned";
            _documentDTO.LastUpdate = (DocumentXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DocumentXPO.LastUpdate : (DateTime?)null;
            _documentDTO.LastUpdateByID = (DocumentXPO.LastUpdateBy != null) ? DocumentXPO.LastUpdateBy.Oid : 0;
            _documentDTO.LastUpdateByName = (DocumentXPO.LastUpdateBy != null) ? DocumentXPO.LastUpdateBy.Name : "Unnassigned";
            

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentDTO;
    }
    public static DocumentXPO DTOtoXPO(DocumentDTO DocumentDTO, UnitOfWork UnitOfWork)
    {
        DocumentXPO _documentXPO;
        try
        {
            _documentXPO = DocumentDTO.ID == null || DocumentDTO.ID == 0 ? new DocumentXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DocumentXPO>(DocumentDTO.ID);
            _documentXPO.Name = _documentXPO.Name == DocumentDTO.Name ? _documentXPO.Name : DocumentDTO.Name;
            _documentXPO.Number = _documentXPO.Number == DocumentDTO.Number ? _documentXPO.Number : DocumentDTO.Number;
            _documentXPO.LastRevision = _documentXPO.LastRevision == DocumentDTO.LastRevision ? _documentXPO.LastRevision : DocumentDTO.LastRevision;
            _documentXPO.Description = _documentXPO.Description == DocumentDTO.Description ? _documentXPO.Description : DocumentDTO.Description;
            _documentXPO.Owner = (_documentXPO.Owner != null && _documentXPO.Owner.Oid == DocumentDTO.OwnerID) ? _documentXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(DocumentDTO.OwnerID);
            _documentXPO.Department = (_documentXPO.Department != null && _documentXPO.Department.Oid == DocumentDTO.DepartmentID) ? _documentXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(DocumentDTO.DepartmentID);
            _documentXPO.Type = (_documentXPO.Type != null && _documentXPO.Type.Oid == DocumentDTO.TypeID) ? _documentXPO.Type : UnitOfWork.GetObjectByKey<DocumentTypeXPO>(DocumentDTO.TypeID);
            _documentXPO.Customer = (_documentXPO.Customer != null && _documentXPO.Customer.Oid == DocumentDTO.CustomerID) ? _documentXPO.Customer : UnitOfWork.GetObjectByKey<CustomerXPO>(DocumentDTO.CustomerID);
            _documentXPO.Product = (_documentXPO.Product != null && _documentXPO.Product.Oid == DocumentDTO.ProductID) ? _documentXPO.Product : UnitOfWork.GetObjectByKey<ProductXPO>(DocumentDTO.ProductID);
            _documentXPO.Status = (_documentXPO.Status != null && _documentXPO.Status.Oid == DocumentDTO.StatusID) ? _documentXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(DocumentDTO.StatusID);
            _documentXPO.AddedDate = _documentXPO.AddedDate != null ? _documentXPO.AddedDate : DocumentDTO.AddedDate;
            _documentXPO.AddedBy = (_documentXPO.AddedBy != null) ? _documentXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DocumentDTO.AddedByID);
            _documentXPO.LastUpdate = _documentXPO.LastUpdate == DocumentDTO.LastUpdate ? _documentXPO.LastUpdate : DocumentDTO.LastUpdate;
            _documentXPO.LastUpdateBy = (_documentXPO.LastUpdateBy != null && _documentXPO.LastUpdateBy.Oid == DocumentDTO.LastUpdateByID) ? _documentXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DocumentDTO.LastUpdateByID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentXPO;
    }


    public static List<DocumentDTO> DictionariesToList(DocumentDTO DocumentDTO, List<DocumentDTO> DocumentList)
    {
        var _documentList = new List<DocumentDTO>();
        try
        {
            foreach (var _documentDTO in DocumentList)
            {
                _documentDTO.StatusDict =   DocumentDTO.StatusDict;
                _documentDTO.CustomerDict =   DocumentDTO.CustomerDict;
                _documentDTO.DocumentTypeDict = DocumentDTO.DocumentTypeDict;
                _documentDTO.ProductDict = DocumentDTO.ProductDict;
                _documentDTO.DepartmentDict = DocumentDTO.DepartmentDict;
                _documentDTO.GetFileDTO = DocumentDTO.GetFileDTO;
                var _newDocumentDTO = DictionaryToDTO(_documentDTO);
                _documentList.Add(_newDocumentDTO);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentList;
    }
    public static DocumentDTO DictionaryToDTO(DocumentDTO DocumentDTO)
    {
        try
        {
            if (DocumentDTO.DepartmentDict.TryGetValue(DocumentDTO.DepartmentID, out var departmentDTO))
            {
                DocumentDTO.DepartmentDTO = departmentDTO;
                DocumentDTO.DepartmentDict = null;
            }
            if (DocumentDTO.DocumentTypeDict.TryGetValue(DocumentDTO.TypeID, out var typeDTO))
            {
                DocumentDTO.TypeDTO = typeDTO;
                DocumentDTO.DocumentTypeDict = null;
            }
            if (DocumentDTO.CustomerDict.TryGetValue(DocumentDTO.CustomerID, out var customerDTO))
            {
                DocumentDTO.CustomerDTO = customerDTO;
                DocumentDTO.CustomerDict = null;
            }

            if (DocumentDTO.ProductDict.TryGetValue(DocumentDTO.ProductID, out var productDTO))
            {
                DocumentDTO.ProductDTO = productDTO;
                DocumentDTO.ProductDict = null;
            }

            if (DocumentDTO.StatusDict.TryGetValue(DocumentDTO.StatusID, out var statusDTO))
            {
                DocumentDTO.StatusDTO = statusDTO;
                DocumentDTO.StatusDict = null;
            }
            if (DocumentDTO.GetFileDTO && !string.IsNullOrEmpty(DocumentDTO.FolderName) && !string.IsNullOrEmpty(DocumentDTO.Number) && !string.IsNullOrEmpty(DocumentDTO.LastRevision))
            {
                var _fileDTO = new FileDTO
                {
                    URL = $"{ConfigurationManager.AppSettings["QMSDirectory"]}{DocumentDTO.FolderName}\\{DocumentDTO.Number}\\{DocumentDTO.LastRevision}"
                };
                DocumentDTO.FileDTO = File_Service.GetFile(_fileDTO);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return DocumentDTO;
    }
}
