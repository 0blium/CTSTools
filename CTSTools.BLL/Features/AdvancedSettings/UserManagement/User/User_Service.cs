using AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;
using CTSTools.BLL.Features.Security.Roles.RoleType;
using CTSTools.BLL.Features.Users.ActiveDirectory;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;

public class User_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUser_Global(UserDTO UserDTO)
    {
        var _userADDTO = new ActiveDirectoryDTO();
        //Validate user fields
        //var _validationResultDTO = User_Validator.CreateUser_Validation(UserDTO);
        var _validationResultDTO = new ValidationResultDTO();
        //Get User Information
        if (_validationResultDTO.Result)
        {
            _userADDTO = ActiveDirectory_Helper.GetActiveDirectoryUser(UserDTO);
            _validationResultDTO = _userADDTO.Validation_ResultDTO;
        }
        // Assign values from previous function
        if (_validationResultDTO.Result)
        {
            UserDTO.Name = _userADDTO.Fullname;
            UserDTO.Email = _userADDTO.Email;
            UserDTO.Position = _userADDTO.Position;
            UserDTO.IsActive = true;
            UserDTO.AddedDate = DateTime.Now;

        }
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = User_Repository.CreateUser(UserDTO);
            UserDTO.ID = _validationResultDTO.Data;
        }
        //6.- Send Welcome Email for new user
        //if (_validationResultDTO.Result && ((bool)UserDTO.SendWelcomeEmail && UserDTO.Email != null))
        //{
        //    _validation_ResultDTO = Mail_Helper.SendWelcomeEmail(UserDTO);
        //}
       
        //if (_validationResultDTO.Result && UserDTO.RoleIDArray != null)
        //{
        //     User_Role_Service.UpdateMultipleRole_User(UserDTO);
        //}
        //save change log
        //if (_validationResultDTO.Result)
        //{
        //    ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<UserDTO>(UserDTO, (int)UserDTO.AddedByID, (int)UserDTO.ID);
        //}
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateUser_Global(UserDTO UserDTO)
    {
        //var _previousUserDTO = User_Repository.GetUserList(new UserDTO { ID = UserDTO.ID }).FirstOrDefault();
        var _validationResultDTO = User_Validator.UpdateUser_Validation(UserDTO);
        if (_validationResultDTO.Result)
        {
            UserDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = User_Repository.UpdateUser(UserDTO);
        }
        if (_validationResultDTO.Result && UserDTO.RoleIDArray != null)
        {
            User_Role_Service.UpdateMultipleRole_User(UserDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteUser_Global(UserDTO UserDTO)
    {
        var _validationResultDTO = User_Validator.DeleteUser_Validation(UserDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = User_Repository.DeleteUser(UserDTO);
        }
        return _validationResultDTO;
    }
    public static List<UserDTO> GetUserList_Global(UserDTO UserDTO, PagedResultDTO<UserDTO> PagedUserDTO = null)
    {
        var _userglobalList = new List<UserDTO>();
        try
        {
            var _userList = User_Repository.GetUserList(UserDTO, PagedUserDTO);

            if (_userList.Count() == 0)
            {
                _userglobalList = _userList;
                return _userglobalList;
            }
            if (!UserDTO.GetFacilityDTO && !UserDTO.GetDepartmentDTO && !UserDTO.GetRoleArray && !UserDTO.GetSupportGroupArray)
            {
                _userglobalList = _userList;
                return _userglobalList;
            }
            //Get relational DTO's
            _userglobalList = GetUserRelatedData(UserDTO, _userList);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userglobalList;
    }


    public static List<UserDTO> GetUserRelatedData(UserDTO UserDTO, List<UserDTO> UserList)
    {
        var _userglobalList = new List<UserDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _departmentDict = new Dictionary<int?, DepartmentDTO>();
        var _roleArrayDict = new Dictionary<int?, int?[]>();
        var _supportGroupArrayDict = new Dictionary<int?, int?[]>();

        var _systemRoles = Role_Service.GetRoleList_Global(new RoleDTO { RoleTypeDTO = new RoleTypeDTO { ID = (int) RoleType_Enum.System} }).Select(S => S.ID).ToArray();
        try
        {
            if (UserDTO.GetFacilityDTO)
            {
                UserDTO.FacilityDTO.FacilityIDArray = UserList.GroupBy(g => g.FacilityID)
                                                              .Select(s => s.Key)
                                                              .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(UserDTO.FacilityDTO)
                                                .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (UserDTO.GetDepartmentDTO)
            {
                UserDTO.DepartmentDTO.DepartmentIDArray = UserList.GroupBy(g => g.DepartmentID)
                                                                  .Select(s => s.Key)
                                                                  .ToArray();

                _departmentDict = Department_Service.GetDepartmentList_Global(UserDTO.DepartmentDTO)
                                                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }

            if (UserDTO.GetRoleArray)
            {
                var _role_userDTO = new User_RoleDTO { UserID =  UserDTO.ID };
                _roleArrayDict = User_Role_Service.GetUser_RoleList_Global(_role_userDTO).GroupBy(g => g.UserID)
                                                                                         .ToDictionary(keySelector: m => m.Key, elementSelector: m => m.Select(s => s.RoleID)
                                                                                         .ToArray());
            }
            if (UserDTO.GetSupportGroupArray)
            {
                var _supportGroupMemberDTO = new SupportGroupMemberDTO { UserID = UserDTO.ID };
                _supportGroupArrayDict = SupportGroupMember_Service.GetSupportGroupMemberList_Global(_supportGroupMemberDTO).GroupBy(g => g.UserID)
                                                                                         .ToDictionary(keySelector: m => m.Key, elementSelector: m => m.Select(s => s.SupportGroupID)
                                                                                         .ToArray());
            }


            foreach (var _userDTO in UserList)
            {

                if (UserDTO.GetFacilityDTO && _facilityDict.ContainsKey(_userDTO.FacilityID))
                {
                    _userDTO.FacilityDTO = _facilityDict[_userDTO.FacilityID];
                }
                if (UserDTO.GetDepartmentDTO && _departmentDict.ContainsKey(_userDTO.DepartmentID))
                {
                    _userDTO.DepartmentDTO = _departmentDict[_userDTO.DepartmentID];
                }
                if (UserDTO.GetRoleArray && _roleArrayDict.ContainsKey(_userDTO.ID))
                {
                    _userDTO.RoleIDArray = _roleArrayDict[_userDTO.ID];
                    if (_userDTO.RoleIDArray.Intersect(_systemRoles).Any())
                    {
                        _userDTO.hasSystemRole = true;
                    }
                }
                if (UserDTO.GetSupportGroupArray && _supportGroupArrayDict.ContainsKey(_userDTO.ID))
                {
                    _userDTO.SupportGroupIDArray = _supportGroupArrayDict[_userDTO.ID];
                }

                _userglobalList.Add(_userDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userglobalList;
    }

    public static int GetUserTotalCount(PagedResultDTO<UserDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =User_Repository.GetUserCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }

    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
