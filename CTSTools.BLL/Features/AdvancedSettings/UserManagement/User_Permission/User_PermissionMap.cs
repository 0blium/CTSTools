using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_PermissionMap
{
    public static User_PermissionDTO XPOToDTO(User_PermissionXPO User_PermissionXPO)
    {
        var _user_permissionDTO = new User_PermissionDTO();
        try
        {
            _user_permissionDTO.ID = User_PermissionXPO.Oid;
            _user_permissionDTO.PermissionID = (User_PermissionXPO.Permission != null) ? User_PermissionXPO.Permission.Oid : 0;
            _user_permissionDTO.PermissionName = (User_PermissionXPO.Permission != null) ? User_PermissionXPO.Permission.Name : "Unnassigned";
            _user_permissionDTO.UserID = (User_PermissionXPO.User != null) ? User_PermissionXPO.User.Oid : 0;
            _user_permissionDTO.UserName = (User_PermissionXPO.User != null) ? User_PermissionXPO.User.Name : "Unnassigned";
            _user_permissionDTO.AddedDate = (User_PermissionXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? User_PermissionXPO.AddedDate : (DateTime?)null;
            _user_permissionDTO.AddedByID = (User_PermissionXPO.AddedBy != null) ? User_PermissionXPO.AddedBy.Oid : 0;
            _user_permissionDTO.AddedByName = (User_PermissionXPO.AddedBy != null) ? User_PermissionXPO.AddedBy.Name : "Unnassigned";
            _user_permissionDTO.LastUpdate = (User_PermissionXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? User_PermissionXPO.LastUpdate : (DateTime?)null;
            _user_permissionDTO.LastUpdateByID = (User_PermissionXPO.LastUpdateBy != null) ? User_PermissionXPO.LastUpdateBy.Oid : 0;
            _user_permissionDTO.LastUpdateByName = (User_PermissionXPO.LastUpdateBy != null) ? User_PermissionXPO.LastUpdateBy.Name : "Unnassigned";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _user_permissionDTO;
    }

    public static User_PermissionXPO DTOtoXPO(User_PermissionDTO User_PermissionDTO, UnitOfWork UnitOfWork)
    {
        User_PermissionXPO _user_permissionXPO;
        try
        {
            _user_permissionXPO = User_PermissionDTO.ID == null || User_PermissionDTO.ID == 0 ? new User_PermissionXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<User_PermissionXPO>(User_PermissionDTO.ID);
            _user_permissionXPO.Permission = (_user_permissionXPO.Permission != null && _user_permissionXPO.Permission.Oid == User_PermissionDTO.PermissionID) ? _user_permissionXPO.Permission : UnitOfWork.GetObjectByKey<PermissionXPO>(User_PermissionDTO.PermissionID);
            _user_permissionXPO.User = (_user_permissionXPO.User != null && _user_permissionXPO.User.Oid == User_PermissionDTO.UserID) ? _user_permissionXPO.User : UnitOfWork.GetObjectByKey<UserXPO>(User_PermissionDTO.UserID);
            _user_permissionXPO.AddedDate = _user_permissionXPO.AddedDate != null ? _user_permissionXPO.AddedDate : User_PermissionDTO.AddedDate;
            _user_permissionXPO.AddedBy = (_user_permissionXPO.AddedBy != null && _user_permissionXPO.AddedBy.Oid == User_PermissionDTO.AddedByID) ? _user_permissionXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(User_PermissionDTO.AddedByID);
            _user_permissionXPO.LastUpdate = _user_permissionXPO.LastUpdate == User_PermissionDTO.LastUpdate ? _user_permissionXPO.LastUpdate : User_PermissionDTO.LastUpdate;
            _user_permissionXPO.LastUpdateBy = (_user_permissionXPO.LastUpdateBy != null && _user_permissionXPO.LastUpdateBy.Oid == User_PermissionDTO.LastUpdateByID) ? _user_permissionXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(User_PermissionDTO.LastUpdateByID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _user_permissionXPO;
    }

}
