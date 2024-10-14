using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;

public class User_Role_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUser_Role_Global(User_RoleDTO User_RoleDTO)
    {
        var _ValidationResultDTO = User_Role_Validator.CreateUser_Role_Validation(User_RoleDTO);
        if (_ValidationResultDTO.Result)
        {
            User_RoleDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = User_Role_Repository.CreateUser_Role(User_RoleDTO);
        }

        if (_ValidationResultDTO.Result)
            AssignPermissionToUserByRole(User_RoleDTO);
        //if (_ValidationResultDTO.Result)
        //{
        //    User_RoleDTO.ID = _ValidationResultDTO.Data;
        //    ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<User_RoleDTO>(User_RoleDTO, (int)User_RoleDTO.AddedByID, (int)User_RoleDTO.ID);
        //}
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateUser_Role_Global(User_RoleDTO User_RoleDTO)
    {
        var _ValidationResultDTO = User_Role_Validator.UpdateUser_Role_Validation(User_RoleDTO);
        var _previousUser_RoleDTO = GetUser_RoleList_Global(new User_RoleDTO { ID = User_RoleDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            User_RoleDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = User_Role_Repository.UpdateUser_Role(User_RoleDTO);
        }
        //if (_ValidationResultDTO.Result)
        //{
        //    ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<User_RoleDTO>(_previousUser_RoleDTO,User_RoleDTO, (int)User_RoleDTO.LastUpdateByID, (int)User_RoleDTO.ID);
        //}
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteUser_Role_Global(User_RoleDTO User_RoleDTO)
    {
        var _ValidationResultDTO = User_Role_Validator.DeleteUser_Role_Validation(User_RoleDTO);
        if (_ValidationResultDTO.Result)
            _ValidationResultDTO = User_Role_Repository.DeleteUser_Role(User_RoleDTO);
        if (_ValidationResultDTO.Result)
            RemovePermissionToUserByRole(User_RoleDTO);

        //if (_ValidationResultDTO.Result)
        //{
        //    ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<User_RoleDTO>(_previousUser_RoleDTO, (int)User_RoleDTO.LastUpdateByID, (int)User_RoleDTO.ID);
        //}
        return _ValidationResultDTO;
    }
    public static List<User_RoleDTO> GetUser_RoleList_Global(User_RoleDTO User_RoleDTO, PagedResultDTO<User_RoleDTO> PagedResultDTO = null)
    {
        var _user_roleglobalList = new List<User_RoleDTO>();
        try
        {
            var _user_roleList = User_Role_Repository.GetUser_RoleList(User_RoleDTO, PagedResultDTO);
            // if User_Role is empty, return list
            if (_user_roleList.Count() == 0 || (!User_RoleDTO.GetRoleDTO && !User_RoleDTO.GetUserDTO))
                return _user_roleList;

            _user_roleglobalList = GetUser_RoleRelatedData(User_RoleDTO, _user_roleList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _user_roleglobalList;
    }



    public static List<User_RoleDTO> GetUser_RoleRelatedData(User_RoleDTO User_RoleDTO, List<User_RoleDTO> User_RoleList)
    {
        var _user_roleglobalList = new List<User_RoleDTO>();
        var _roleDict = new Dictionary<int?, RoleDTO>();
        var _userDict = new Dictionary<int?, UserDTO>();

        try
        {
            if (User_RoleDTO.GetRoleDTO)
            {
                User_RoleDTO.RoleDTO.RoleIDArray = User_RoleList.GroupBy(g => g.RoleID)
                                                                .Select(s => s.Key)
                                                                .ToArray();

                _roleDict = Role_Service.GetRoleList_Global(User_RoleDTO.RoleDTO)
                                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (User_RoleDTO.GetUserDTO)
            {
                User_RoleDTO.UserDTO.UserIDArray = User_RoleList.GroupBy(g => g.UserID)
                                                                .Select(s => s.Key)
                                                                .ToArray();

                _userDict = User_Service.GetUserList_Global(User_RoleDTO.UserDTO)
                                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _user_roleDTO in User_RoleList)
            {
                if (User_RoleDTO.GetRoleDTO && _roleDict.ContainsKey(_user_roleDTO.RoleID))
                {
                    _user_roleDTO.RoleDTO = _roleDict[_user_roleDTO.RoleID];
                }
                if (User_RoleDTO.GetUserDTO && _userDict.ContainsKey(_user_roleDTO.UserID))
                {
                    _user_roleDTO.UserDTO = _userDict[_user_roleDTO.UserID];
                }
                _user_roleglobalList.Add(_user_roleDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _user_roleglobalList;
    }




    public static int GetUser_RoleTotalCount(PagedResultDTO<User_RoleDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = User_Role_Repository.GetUser_RoleCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO CreateSelectedRole_User(UserDTO UserDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            if (UserDTO.ID != null && UserDTO.ID > 0)
                return _validation_ResultDTO;

            var _currentRole_UserDTO = new User_RoleDTO { UserID = UserDTO.ID };
            var _currentRole_UserList = GetUser_RoleList_Global(_currentRole_UserDTO);
            var _newRoleArray = (from RoleID in UserDTO.RoleIDArray
                                 where _currentRole_UserList.Select(s => s.RoleID).ToArray().Contains(RoleID) != true
                                 select RoleID).ToArray();

            if (_newRoleArray.Count() > 0)
                return _validation_ResultDTO;

            // 1. Add new brands and skips existing brands in validation function
            foreach (var _roleID in _newRoleArray)
            {
                var _user_RoleDTO = new User_RoleDTO
                {
                    UserID = UserDTO.ID,
                    RoleID = _roleID,
                    AddedByID = UserDTO.LastUpdateByID == null ? UserDTO.AddedByID : UserDTO.LastUpdateByID,
                    IsActive = true
                };
                _validation_ResultDTO = CreateUser_Role_Global(_user_RoleDTO);
                if (_validation_ResultDTO.Result == false)
                    break;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return _validation_ResultDTO;
    }

    public static ValidationResultDTO UpdateMultipleRole_User(UserDTO UserDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            if (UserDTO.RoleIDArray.Count() >= 0)
                return _validationResultDTO;

            _validationResultDTO = DeleteUnSelectedRole_User(UserDTO);
            if (_validationResultDTO.Result)
                _validationResultDTO = CreateSelectedRole_User(UserDTO);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. {0}", ex.Message);
        }
        return _validationResultDTO;
    }


    public static ValidationResultDTO DeleteUnSelectedRole_User(UserDTO UserDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            if (UserDTO.ID != null && UserDTO.ID > 0)
            {
                var _userDTO = new UserDTO { ID = UserDTO.ID };
                var _currentRole_UserDTO = new User_RoleDTO { UserDTO = _userDTO };
                var _currentRole_UserList = GetUser_RoleList_Global(_currentRole_UserDTO);
                var _unSelectedRole_UserList = (from Role_UserDTO in _currentRole_UserList
                                                where UserDTO.RoleIDArray.Contains(Role_UserDTO.RoleID) != true
                                                select Role_UserDTO).ToList();

                if (_unSelectedRole_UserList.Count() > 0)
                {
                    foreach (var _role_UserDTO in _unSelectedRole_UserList)
                    {

                        // 2. Delete User_
                        var _role_userDTO = new User_RoleDTO
                        {
                            ID = _role_UserDTO.ID,
                            RoleDTO = _role_UserDTO.RoleDTO,
                            UserDTO = _role_UserDTO.UserDTO,
                            IsActive = UserDTO.IsActive
                        };
                        _validation_ResultDTO = DeleteUser_Role_Global(_role_userDTO);
                        if (_validation_ResultDTO.Result == false)
                        {
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        return _validation_ResultDTO;
    }



    public static ValidationResultDTO AssignPermissionToUserByRole(User_RoleDTO User_RoleDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();
        try
        {
            var _role_permissionDTO = new Role_PermissionDTO
            {
                RoleID = User_RoleDTO.RoleID,
                IsActive = true
            };
            var _role_PermissionInformation = Role_Permission_Service.GetRole_PermissionList_Global(_role_permissionDTO);

            if (_role_PermissionInformation.Count() > 0)
                return _validation_ResultDTO;

            foreach (var role_permissionDTO in _role_PermissionInformation)
            {
                var _user_permissionDTO = new User_PermissionDTO
                {
                    PermissionID = role_permissionDTO.PermissionID,
                    UserID = User_RoleDTO.UserID,
                    AddedByID = User_RoleDTO.AddedByID,
                    IsActive = User_RoleDTO.IsActive
                };
                _validation_ResultDTO = User_Permission_Service.CreateUser_Permission_Global(_user_permissionDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to create the record.");
        }
        return _validation_ResultDTO;
    }


    public static ValidationResultDTO RemovePermissionToUserByRole(User_RoleDTO User_RoleDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();
        try
        {
            //Get Permissions by role
            var _role_permissionDTO = new Role_PermissionDTO
            {
                RoleID = User_RoleDTO.RoleID,
                IsActive = true
            };
            var _role_PermissionInformation = Role_Permission_Service.GetRole_PermissionList_Global(_role_permissionDTO);
            if (_role_PermissionInformation.Count() > 0)
                return _validation_ResultDTO;
            //Get  Permission ID Array 
            var _permissionIDArray = _role_PermissionInformation.Select(s => s.PermissionID).ToArray();
            //Get User and Permissions Relation ,Consult User ID and Permissions ID's
            var _user_permission = new User_PermissionDTO
            {
                UserID = User_RoleDTO.UserID,
                PermissionIDArray = _permissionIDArray
            };
            var _user_PermissionList = User_Permission_Service.GetUser_PermissionList_Global(_user_permission);
            //Delete User Permission relation
            foreach (var _user_PermissionDTO in _user_PermissionList)
            {
                _validation_ResultDTO = User_Permission_Validator.ValidateRemovePermissionToUserByRole(_user_PermissionDTO);
                if (_validation_ResultDTO.Result)
                    _validation_ResultDTO = User_Permission_Service.DeleteUser_Permission_Global(_user_PermissionDTO);

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to create the record.");
        }
        return _validation_ResultDTO;
    }


    #endregion
}
