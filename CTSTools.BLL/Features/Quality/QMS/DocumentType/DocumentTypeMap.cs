using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.DocumentType;

public class DocumentTypeMap
{
    public static DocumentTypeDTO XPOToDTO(DocumentTypeXPO DocumentTypeXPO)
    {
        var _documentTypeDTO = new DocumentTypeDTO();
        try
        {
            _documentTypeDTO.ID = DocumentTypeXPO.Oid;
            _documentTypeDTO.Name = DocumentTypeXPO.Name;
            _documentTypeDTO.Description = DocumentTypeXPO.Description;
            _documentTypeDTO.AddedDate = (DocumentTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DocumentTypeXPO.AddedDate : (DateTime?)null;
            _documentTypeDTO.AddedByID = (DocumentTypeXPO.AddedBy != null) ? DocumentTypeXPO.AddedBy.Oid : 0;
            _documentTypeDTO.AddedByName = (DocumentTypeXPO.AddedBy != null) ? DocumentTypeXPO.AddedBy.Name : "Unnassigned";
            _documentTypeDTO.LastUpdate = (DocumentTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DocumentTypeXPO.LastUpdate : (DateTime?)null;
            _documentTypeDTO.LastUpdateByID = (DocumentTypeXPO.LastUpdateBy != null) ? DocumentTypeXPO.LastUpdateBy.Oid : 0;
            _documentTypeDTO.LastUpdateByName = (DocumentTypeXPO.LastUpdateBy != null) ? DocumentTypeXPO.LastUpdateBy.Name : "Unnassigned";
            _documentTypeDTO.IsActive = DocumentTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentTypeDTO;
    }

    public static DocumentTypeXPO DTOtoXPO(DocumentTypeDTO DocumentTypeDTO, UnitOfWork UnitOfWork)
    {
        DocumentTypeXPO _documentTypeXPO;
        try
        {
            _documentTypeXPO = DocumentTypeDTO.ID == null || DocumentTypeDTO.ID == 0 ? new DocumentTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DocumentTypeXPO>(DocumentTypeDTO.ID);
            _documentTypeXPO.Name = _documentTypeXPO.Name == DocumentTypeDTO.Name ? _documentTypeXPO.Name : DocumentTypeDTO.Name;
            _documentTypeXPO.Description = _documentTypeXPO.Description == DocumentTypeDTO.Description ? _documentTypeXPO.Description : DocumentTypeDTO.Description;
            _documentTypeXPO.AddedDate = _documentTypeXPO.AddedDate != null ? _documentTypeXPO.AddedDate : DocumentTypeDTO.AddedDate;
            _documentTypeXPO.AddedBy = (_documentTypeXPO.AddedBy != null) ? _documentTypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DocumentTypeDTO.AddedByID);
            _documentTypeXPO.LastUpdate = _documentTypeXPO.LastUpdate == DocumentTypeDTO.LastUpdate ? _documentTypeXPO.LastUpdate : DocumentTypeDTO.LastUpdate;
            _documentTypeXPO.LastUpdateBy = (_documentTypeXPO.LastUpdateBy != null && _documentTypeXPO.LastUpdateBy.Oid == DocumentTypeDTO.LastUpdateByID) ? _documentTypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DocumentTypeDTO.LastUpdateByID);
            _documentTypeXPO.IsActive = _documentTypeXPO.IsActive == DocumentTypeDTO.IsActive ? (bool)_documentTypeXPO.IsActive : (bool)DocumentTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _documentTypeXPO;
    }
}
