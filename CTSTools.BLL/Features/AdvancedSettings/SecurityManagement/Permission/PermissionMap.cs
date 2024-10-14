using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;


namespace CTSTools.BLL.Features.Security.Permissions.Permission
{
    public class PermissionMap
    {
        public static PermissionDTO XPOToDTO(PermissionXPO PermissionXPO)
        {
            var _permissionDTO = new PermissionDTO();
            try
            {
                _permissionDTO.ID = PermissionXPO.Oid;
               _permissionDTO.Name = PermissionXPO.Name; 
               _permissionDTO.Module = PermissionXPO.Module; 
               _permissionDTO.Description = PermissionXPO.Description; 
               _permissionDTO.ActionID = (PermissionXPO.Action != null) ? PermissionXPO.Action.Oid : 0;
               _permissionDTO.ActionName = (PermissionXPO.Action != null) ? PermissionXPO.Action.Name : "Unnassigned"; 
               _permissionDTO.AddedDate = (PermissionXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? PermissionXPO.AddedDate : (DateTime?)null; 
               _permissionDTO.AddedByID = (PermissionXPO.AddedBy != null) ? PermissionXPO.AddedBy.Oid : 0;
               _permissionDTO.AddedByName = (PermissionXPO.AddedBy != null) ? PermissionXPO.AddedBy.Name : "Unnassigned";
               _permissionDTO.LastUpdate = (PermissionXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? PermissionXPO.LastUpdate : (DateTime?)null; 
               _permissionDTO.LastUpdateByID = (PermissionXPO.LastUpdateBy != null) ? PermissionXPO.LastUpdateBy.Oid : 0;
               _permissionDTO.LastUpdateByName = (PermissionXPO.LastUpdateBy != null) ? PermissionXPO.LastUpdateBy.Name : "Unnassigned";
               _permissionDTO.IsActive = PermissionXPO.IsActive; 
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _permissionDTO;
        }

        public static PermissionXPO DTOtoXPO(PermissionDTO PermissionDTO, UnitOfWork UnitOfWork)
        {
            PermissionXPO _permissionXPO;
            try
            {
                _permissionXPO = PermissionDTO.ID == null || PermissionDTO.ID == 0 ? new PermissionXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<PermissionXPO>(PermissionDTO.ID);
                _permissionXPO.Name = _permissionXPO.Name == PermissionDTO.Name ? _permissionXPO.Name : PermissionDTO.Name;
               _permissionXPO.Module = _permissionXPO.Module == PermissionDTO.Module ? _permissionXPO.Module : PermissionDTO.Module;
               _permissionXPO.Description = _permissionXPO.Description == PermissionDTO.Description ? _permissionXPO.Description : PermissionDTO.Description;
               _permissionXPO.Action = (_permissionXPO.Action != null && _permissionXPO.Action.Oid == PermissionDTO.ActionID ) ? _permissionXPO.Action : UnitOfWork.GetObjectByKey<ActionXPO>(PermissionDTO.ActionID);
               _permissionXPO.AddedDate = _permissionXPO.AddedDate != null ? _permissionXPO.AddedDate : PermissionDTO.AddedDate;
               _permissionXPO.AddedBy = (_permissionXPO.AddedBy != null ) ? _permissionXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(PermissionDTO.AddedByID);
               _permissionXPO.LastUpdate = _permissionXPO.LastUpdate == PermissionDTO.LastUpdate ? _permissionXPO.LastUpdate : PermissionDTO.LastUpdate;
               _permissionXPO.LastUpdateBy = (_permissionXPO.LastUpdateBy != null && _permissionXPO.LastUpdateBy.Oid == PermissionDTO.LastUpdateByID ) ? _permissionXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(PermissionDTO.LastUpdateByID);
               _permissionXPO.IsActive = _permissionXPO.IsActive == PermissionDTO.IsActive ? (bool)_permissionXPO.IsActive : (bool)PermissionDTO.IsActive;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _permissionXPO;
        }

    }
}
