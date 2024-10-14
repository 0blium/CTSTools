using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
public class UserMap
{
    public static UserDTO XPOToDTO(UserXPO UserXPO)
    {
        var _userDTO = new UserDTO();
        try
        {
            _userDTO.ID = UserXPO.Oid;
            _userDTO.Name = UserXPO.Name;
            _userDTO.Login = UserXPO.Login;
            _userDTO.Email = UserXPO.Email;
            _userDTO.Position = UserXPO.Position;
            _userDTO.AddedDate = (UserXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? UserXPO.AddedDate : (DateTime?)null;
            _userDTO.AddedByID = (UserXPO.AddedBy != null) ? UserXPO.AddedBy.Oid : 0;
            _userDTO.AddedByName = (UserXPO.AddedBy != null) ? UserXPO.AddedBy.Name : "Unassigned";
            _userDTO.LastUpdate = (UserXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? UserXPO.LastUpdate : (DateTime?)null;
            _userDTO.LastUpdateByID = (UserXPO.LastUpdateBy != null) ? UserXPO.LastUpdateBy.Oid : 0;
            _userDTO.LastUpdateByName = (UserXPO.LastUpdateBy != null) ? UserXPO.LastUpdateBy.Name : "Unassigned";
            _userDTO.IsActive = UserXPO.IsActive;
            _userDTO.FacilityName = (UserXPO.Facility != null) ? UserXPO.Facility.Name : "Unnassigned";
            _userDTO.FacilityID = (UserXPO.Facility != null) ? UserXPO.Facility.Oid : 0;
            _userDTO.DepartmentName = (UserXPO.Department != null) ? UserXPO.Department.Name : "Unnassigned";
            _userDTO.DepartmentID = (UserXPO.Department != null) ? UserXPO.Department.Oid : 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _userDTO;
    }

    public static UserXPO DTOtoXPO(UserDTO UserDTO, UnitOfWork UnitOfWork)
    {
        UserXPO _userXPO;
        try
        {
            _userXPO = UserDTO.ID == null || UserDTO.ID == 0 ? new UserXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<UserXPO>(UserDTO.ID);
            _userXPO.Name = _userXPO.Name == UserDTO.Name ? _userXPO.Name : UserDTO.Name;
            _userXPO.Login = _userXPO.Login == UserDTO.Login ? _userXPO.Login : UserDTO.Login;
            _userXPO.Email = _userXPO.Email == UserDTO.Email ? _userXPO.Email : UserDTO.Email;
            _userXPO.Position = _userXPO.Position == UserDTO.Position ? _userXPO.Position : UserDTO.Position;
            _userXPO.AddedDate = _userXPO.AddedDate != null ? _userXPO.AddedDate : UserDTO.AddedDate;
            _userXPO.AddedBy = _userXPO.AddedBy != null ? _userXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDTO.AddedByID);
            _userXPO.LastUpdate = _userXPO.LastUpdate == UserDTO.LastUpdate ? _userXPO.LastUpdate : UserDTO.LastUpdate;
            _userXPO.LastUpdateBy = _userXPO.LastUpdateBy != null && _userXPO.LastUpdateBy.Oid == UserDTO.LastUpdateByID ? _userXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDTO.LastUpdateByID);
            _userXPO.IsActive = _userXPO.IsActive == UserDTO.IsActive ? (bool)_userXPO.IsActive : (bool)UserDTO.IsActive;
            _userXPO.Facility = _userXPO.Facility != null && _userXPO.Facility.Oid == UserDTO.FacilityID ? _userXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(UserDTO.FacilityID);
            _userXPO.Department = _userXPO.Department != null && _userXPO.Department.Oid == UserDTO.DepartmentID ? _userXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(UserDTO.DepartmentID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _userXPO;
    }

}
