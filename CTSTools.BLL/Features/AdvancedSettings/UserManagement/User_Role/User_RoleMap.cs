using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;

public class User_RoleMap
{
    public static User_RoleDTO XPOToDTO(User_RoleXPO User_RoleXPO)
    {
        var _user_roleDTO = new User_RoleDTO();
        try
        {
            _user_roleDTO.ID = User_RoleXPO.Oid;
           _user_roleDTO.RoleID = (User_RoleXPO.Role != null) ? User_RoleXPO.Role.Oid : 0;
           _user_roleDTO.RoleName = (User_RoleXPO.Role != null) ? User_RoleXPO.Role.Name : "Unnassigned";
           _user_roleDTO.UserID = (User_RoleXPO.User != null) ? User_RoleXPO.User.Oid : 0;
           _user_roleDTO.UserName = (User_RoleXPO.User != null) ? User_RoleXPO.User.Name : "Unnassigned";
           _user_roleDTO.AddedDate = (User_RoleXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? User_RoleXPO.AddedDate : (DateTime?)null; 
           _user_roleDTO.AddedByID = (User_RoleXPO.AddedBy != null) ? User_RoleXPO.AddedBy.Oid : 0;
           _user_roleDTO.AddedByName = (User_RoleXPO.AddedBy != null) ? User_RoleXPO.AddedBy.Name : "Unnassigned";
           _user_roleDTO.LastUpdate = (User_RoleXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? User_RoleXPO.LastUpdate : (DateTime?)null; 
           _user_roleDTO.LastUpdateByID = (User_RoleXPO.LastUpdateBy != null) ? User_RoleXPO.LastUpdateBy.Oid : 0;
           _user_roleDTO.LastUpdateByName = (User_RoleXPO.LastUpdateBy != null) ? User_RoleXPO.LastUpdateBy.Name : "Unnassigned";
           _user_roleDTO.IsActive = User_RoleXPO.IsActive; 
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _user_roleDTO;
    }

    public static User_RoleXPO DTOtoXPO(User_RoleDTO User_RoleDTO, UnitOfWork UnitOfWork)
    {
        User_RoleXPO _user_roleXPO;
        try
        {
            _user_roleXPO = User_RoleDTO.ID == null || User_RoleDTO.ID == 0 ? new User_RoleXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<User_RoleXPO>(User_RoleDTO.ID);
            _user_roleXPO.Role = (_user_roleXPO.Role != null && _user_roleXPO.Role.Oid == User_RoleDTO.RoleID ) ? _user_roleXPO.Role : UnitOfWork.GetObjectByKey<RoleXPO>(User_RoleDTO.RoleID);
           _user_roleXPO.User = (_user_roleXPO.User != null && _user_roleXPO.User.Oid == User_RoleDTO.UserID ) ? _user_roleXPO.User : UnitOfWork.GetObjectByKey<UserXPO>(User_RoleDTO.UserID);
           _user_roleXPO.AddedDate = _user_roleXPO.AddedDate != null ? _user_roleXPO.AddedDate : User_RoleDTO.AddedDate;
           _user_roleXPO.AddedBy = (_user_roleXPO.AddedBy != null && _user_roleXPO.AddedBy.Oid == User_RoleDTO.AddedByID ) ? _user_roleXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(User_RoleDTO.AddedByID);
           _user_roleXPO.LastUpdate = _user_roleXPO.LastUpdate == User_RoleDTO.LastUpdate ? _user_roleXPO.LastUpdate : User_RoleDTO.LastUpdate;
           _user_roleXPO.LastUpdateBy = (_user_roleXPO.LastUpdateBy != null && _user_roleXPO.LastUpdateBy.Oid == User_RoleDTO.LastUpdateByID ) ? _user_roleXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(User_RoleDTO.LastUpdateByID);
           _user_roleXPO.IsActive = _user_roleXPO.IsActive == User_RoleDTO.IsActive ? (bool)_user_roleXPO.IsActive : (bool)User_RoleDTO.IsActive;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _user_roleXPO;
    }

}
