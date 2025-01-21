using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _documentDTO.OwnerID = (DocumentXPO.Owner != null) ? DocumentXPO.Owner.Oid : 0;
            _documentDTO.OwnerName = (DocumentXPO.Owner != null) ? DocumentXPO.Owner.Name : "Unnassigned";
            _documentDTO.DepartmentID = (DocumentXPO.Department != null) ? DocumentXPO.Department.Oid : 0;
            _documentDTO.DepartmentName = (DocumentXPO.Department != null) ? DocumentXPO.Department.Name : "Unnassigned";
            _documentDTO.TypeID = (DocumentXPO.Type != null) ? DocumentXPO.Type.Oid : 0;
            _documentDTO.TypeName = (DocumentXPO.Type != null) ? DocumentXPO.Type.Name : "Unnassigned";
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
            _documentXPO.Owner = (_documentXPO.Owner != null && _documentXPO.Owner.Oid == DocumentDTO.OwnerID) ? _documentXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(DocumentDTO.OwnerID);
            _documentXPO.Department = (_documentXPO.Department != null && _documentXPO.Department.Oid == DocumentDTO.DepartmentID) ? _documentXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(DocumentDTO.DepartmentID);
            _documentXPO.Type = (_documentXPO.Type != null && _documentXPO.Type.Oid == DocumentDTO.TypeID) ? _documentXPO.Type : UnitOfWork.GetObjectByKey<DocumentTypeXPO>(DocumentDTO.TypeID);
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
}
