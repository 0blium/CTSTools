using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Security.Permissions.Role_Permission;

public class Role_PermissionMap
{
    public static Role_PermissionDTO XPOToDTO(Role_PermissionXPO Role_PermissionXPO)
    {
        var _role_permissionDTO = new Role_PermissionDTO();
        try
        {
            _role_permissionDTO.ID = Role_PermissionXPO.Oid;
            _role_permissionDTO.PermissionID = (Role_PermissionXPO.Permission != null) ? Role_PermissionXPO.Permission.Oid : 0;
            _role_permissionDTO.PermissionName = (Role_PermissionXPO.Permission != null) ? Role_PermissionXPO.Permission.Name : "Unnassigned";
            _role_permissionDTO.RoleID = (Role_PermissionXPO.Role != null) ? Role_PermissionXPO.Role.Oid : 0;
            _role_permissionDTO.RoleName = (Role_PermissionXPO.Role != null) ? Role_PermissionXPO.Role.Name : "Unnassigned";
            _role_permissionDTO.AddedDate = (Role_PermissionXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Role_PermissionXPO.AddedDate : (DateTime?)null;
            _role_permissionDTO.AddedByID = (Role_PermissionXPO.AddedBy != null) ? Role_PermissionXPO.AddedBy.Oid : 0;
            _role_permissionDTO.AddedByName = (Role_PermissionXPO.AddedBy != null) ? Role_PermissionXPO.AddedBy.Name : "Unnassigned";
            _role_permissionDTO.LastUpdate = (Role_PermissionXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Role_PermissionXPO.LastUpdate : (DateTime?)null;
            _role_permissionDTO.LastUpdateByID = (Role_PermissionXPO.LastUpdateBy != null) ? Role_PermissionXPO.LastUpdateBy.Oid : 0;
            _role_permissionDTO.LastUpdateByName = (Role_PermissionXPO.LastUpdateBy != null) ? Role_PermissionXPO.LastUpdateBy.Name : "Unnassigned";
            _role_permissionDTO.IsActive = Role_PermissionXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _role_permissionDTO;
    }

    public static Role_PermissionXPO DTOtoXPO(Role_PermissionDTO Role_PermissionDTO, UnitOfWork UnitOfWork)
    {
        Role_PermissionXPO _role_permissionXPO;
        try
        {
            _role_permissionXPO = Role_PermissionDTO.ID == null || Role_PermissionDTO.ID == 0 ? new Role_PermissionXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Role_PermissionXPO>(Role_PermissionDTO.ID);
            _role_permissionXPO.Permission = (_role_permissionXPO.Permission != null && _role_permissionXPO.Permission.Oid == Role_PermissionDTO.PermissionID) ? _role_permissionXPO.Permission : UnitOfWork.GetObjectByKey<PermissionXPO>(Role_PermissionDTO.PermissionID);
            _role_permissionXPO.Role = (_role_permissionXPO.Role != null && _role_permissionXPO.Role.Oid == Role_PermissionDTO.RoleID) ? _role_permissionXPO.Role : UnitOfWork.GetObjectByKey<RoleXPO>(Role_PermissionDTO.RoleID);
            _role_permissionXPO.AddedDate = _role_permissionXPO.AddedDate != null ? _role_permissionXPO.AddedDate : Role_PermissionDTO.AddedDate;
            _role_permissionXPO.AddedBy = (_role_permissionXPO.AddedBy != null) ? _role_permissionXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Role_PermissionDTO.AddedByID);
            _role_permissionXPO.LastUpdate = _role_permissionXPO.LastUpdate == Role_PermissionDTO.LastUpdate ? _role_permissionXPO.LastUpdate : Role_PermissionDTO.LastUpdate;
            _role_permissionXPO.LastUpdateBy = (_role_permissionXPO.LastUpdateBy != null && _role_permissionXPO.LastUpdateBy.Oid == Role_PermissionDTO.LastUpdateByID) ? _role_permissionXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Role_PermissionDTO.LastUpdateByID);
            _role_permissionXPO.IsActive = _role_permissionXPO.IsActive == Role_PermissionDTO.IsActive ? (bool)_role_permissionXPO.IsActive : (bool)Role_PermissionDTO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _role_permissionXPO;
    }

    public static List<Role_PermissionXPO> DTOListToXPOList(List<Role_PermissionDTO> Role_PermissionDTOList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<Role_PermissionXPO>();
        try
        {
            foreach (var _rolepermissionDTO in Role_PermissionDTOList)
            {
                _xPOList.Add(DTOtoXPO(_rolepermissionDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }

}
