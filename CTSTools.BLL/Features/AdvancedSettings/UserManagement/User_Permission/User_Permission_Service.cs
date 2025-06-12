using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_Permission_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUser_Permission_Global(User_PermissionDTO User_PermissionDTO)
    {
        var _ValidationResultDTO = User_Permission_Validator.CreateUser_Permission_Validation(User_PermissionDTO);
        if (_ValidationResultDTO.Result)
        {
            User_PermissionDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = User_Permission_Repository.CreateUser_Permission(User_PermissionDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteUser_Permission_Global(User_PermissionDTO User_PermissionDTO)
    {
        var _ValidationResultDTO = User_Permission_Validator.DeleteUser_Permission_Validation(User_PermissionDTO);
        if (_ValidationResultDTO.Result)
        {
            User_PermissionDTO.IsActive = true;
            _ValidationResultDTO = User_Permission_Repository.DeleteUser_Permission(User_PermissionDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<User_PermissionDTO> GetUser_PermissionList_Global(User_PermissionDTO User_PermissionDTO, PagedResultDTO<User_PermissionDTO> PagedResultDTO = null)
    {
        var _user_permissionglobalList = new List<User_PermissionDTO>();
        try
        {
            var _user_permissionList = User_Permission_Repository.GetUser_PermissionList(User_PermissionDTO, PagedResultDTO);
            // if User_Permission is empty, return list
            if (_user_permissionList.Count() == 0)
            {
                _user_permissionglobalList = _user_permissionList;
                return _user_permissionglobalList;
            }
            if (!User_PermissionDTO.GetPermissionDTO && !User_PermissionDTO.GetUserDTO)
            {
                _user_permissionglobalList = _user_permissionList;
                return _user_permissionglobalList;
            }
            _user_permissionglobalList = GetUser_PermissionRelatedData(User_PermissionDTO, _user_permissionList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _user_permissionglobalList;
    }
    public static List<User_PermissionDTO> GetUser_PermissionRelatedData(User_PermissionDTO User_PermissionDTO, List<User_PermissionDTO> User_PermissionList)
    {
        var _user_permissionglobalList = new List<User_PermissionDTO>();
        var _permissionDict = new Dictionary<int?, PermissionDTO>();
        var _userDict = new Dictionary<int?, UserDTO>();

        try
        {
            if (User_PermissionDTO.GetPermissionDTO)
            {
                User_PermissionDTO.PermissionDTO.PermissionIDArray = User_PermissionList.GroupBy(g => g.PermissionID)
                                                                                        .Select(s => s.Key)
                                                                                        .ToArray();

                _permissionDict = Permission_Service.GetPermissionList_Global(User_PermissionDTO.PermissionDTO)
                                                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (User_PermissionDTO.GetUserDTO)
            {
                User_PermissionDTO.UserDTO.UserIDArray = User_PermissionList.GroupBy(g => g.UserID)
                                                                            .Select(s => s.Key)
                                                                            .ToArray();

                _userDict = User_Service.GetUserList_Global(User_PermissionDTO.UserDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _user_permissionDTO in User_PermissionList)
            {
                if (User_PermissionDTO.GetPermissionDTO && _permissionDict.ContainsKey(_user_permissionDTO.PermissionID))
                {
                    _user_permissionDTO.PermissionDTO = _permissionDict[_user_permissionDTO.PermissionID];
                }
                if (User_PermissionDTO.GetUserDTO && _userDict.ContainsKey(_user_permissionDTO.UserID))
                {
                    _user_permissionDTO.UserDTO = _userDict[_user_permissionDTO.UserID];
                }
                _user_permissionglobalList.Add(_user_permissionDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _user_permissionglobalList;
    }
    public static int GetUser_PermissionTotalCount(PagedResultDTO<User_PermissionDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = User_Permission_Repository.GetUser_PermissionCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }

    public static ValidationResultDTO CreateMultiple_Global(List<User_PermissionDTO> User_PermissionList)
    {
        // Step 1. 
        var _validationResultDTO = User_Permission_Validator.CreateMultiple_Validation(User_PermissionList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2.
        _validationResultDTO = User_Permission_Repository.CreateMultiple(User_PermissionList);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }

    public static ValidationResultDTO DeleteMultiple_Global(List<User_PermissionDTO> User_PermissionList)
    {
        var _validationResultDTO = User_Permission_Validator.DeleteMultiple_Validation(User_PermissionList);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = User_Permission_Repository.DeleteMultiple(User_PermissionList);
        }
        return _validationResultDTO;
    }
    #endregion

    #region Business Logic
    public static ValidationResultDTO CreateUser_PermissionByArray(User_PermissionDTO User_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            _validationResultDTO = User_Permission_Validator.CreateUser_PermissionByArray_Validation(User_PermissionDTO);
            if (_validationResultDTO.Result)
            {
                // Retrieve current records to prevent duplicate insertions
                var _userPermissionIDArray = GetUser_PermissionList_Global(new User_PermissionDTO { UserID = User_PermissionDTO.UserID })
                    .GroupBy(x => x.PermissionID).Select(x => x.Key).ToArray();

                User_PermissionDTO.PermissionIDArray = User_PermissionDTO.PermissionIDArray.Except(_userPermissionIDArray).ToArray();

                //Create new objects
                var _user_PermissionDTOList = User_PermissionDTO.PermissionIDArray.Select(permissionID => 
                new User_PermissionDTO
                {
                    PermissionID = permissionID,
                    UserID = User_PermissionDTO.UserID,
                    AddedByID = User_PermissionDTO.AddedByID,
                    IsActive = true,
                }).ToList();


                if (_user_PermissionDTOList.Count() > 0)
                    CreateMultiple_Global(_user_PermissionDTOList);


            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to create the record.");
        }
        return _validationResultDTO;
    }

    public static ValidationResultDTO UpdatePermisssionToAllUsersByRole(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();
        try
        {
            //Get Users by role
            var _user_roleDTO = new User_RoleDTO { RoleID = Role_PermissionDTO.RoleID };
            var _user_roleList = User_Role_Service.GetUser_RoleList_Global(_user_roleDTO);
            foreach (var user_roleDTO in _user_roleList)
            {
                var _user_PermissionDTO = new User_PermissionDTO
                {
                    UserID = user_roleDTO.UserID,
                    PermissionID = Role_PermissionDTO.PermissionID,
                    IsActive = true,
                    AddedByID = Role_PermissionDTO.AddedByID
                };
                _validation_ResultDTO = CreateUser_Permission_Global(_user_PermissionDTO);
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

    public static ValidationResultDTO DeletePermisssionToAllUsersByRole(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();
        try
        {
            //Get Users by role
            var _user_roleList = User_Role_Service.GetUser_RoleList_Global(new User_RoleDTO { RoleDTO = Role_PermissionDTO.RoleDTO });
            if (_user_roleList.Count() > 0)
            {
                var _userIDArray = _user_roleList.Select(s => s.UserDTO.ID).ToArray();
                var _user_PermissionList = User_Permission_Service.GetUser_PermissionList_Global(
                  new User_PermissionDTO
                  {
                      PermissionDTO = Role_PermissionDTO.PermissionDTO,
                      UserIDArray = _userIDArray
                  });
                //Delete User Permission relation
                foreach (var _user_PermissionDTO in _user_PermissionList)
                {
                    _validation_ResultDTO = User_Permission_Validator.ValidateRemovePermissionToUserByRole(_user_PermissionDTO);
                    if (_validation_ResultDTO.Result)
                    {
                        _validation_ResultDTO = User_Permission_Service.DeleteUser_Permission_Global(_user_PermissionDTO);
                    }
                }
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
