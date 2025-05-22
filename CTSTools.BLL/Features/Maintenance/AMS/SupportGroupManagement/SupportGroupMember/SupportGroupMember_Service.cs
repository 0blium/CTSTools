using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;
using CTSTools.BLL.Features.ChangeLog;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember
{
    public class SupportGroupMember_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateSupportGroupMember_Global(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _ValidationResultDTO = SupportGroupMember_Validator.CreateSupportGroupMember_Validation(SupportGroupMemberDTO);
            if (_ValidationResultDTO.Result)
            {
                SupportGroupMemberDTO.AddedDate = DateTime.Now;
                _ValidationResultDTO = SupportGroupMember_Repository.CreateSupportGroupMember(SupportGroupMemberDTO);
            }
            if (_ValidationResultDTO.Result)
            {
                AssignRoleToUserBySupportGroup(SupportGroupMemberDTO);
                ChangeLog_Service.BuildChangeLogActionCreate<SupportGroupMemberDTO>(SupportGroupMemberDTO, (int)SupportGroupMemberDTO.AddedByID, (int)SupportGroupMemberDTO.ID);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO UpdateSupportGroupMember_Global(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _ValidationResultDTO = SupportGroupMember_Validator.UpdateSupportGroupMember_Validation(SupportGroupMemberDTO);
            var _previousSupportGroupMemberDTO = GetSupportGroupMemberList_Global(new SupportGroupMemberDTO { ID = SupportGroupMemberDTO.ID }).FirstOrDefault();
            if (_ValidationResultDTO.Result)
            {
                SupportGroupMemberDTO.LastUpdate = DateTime.Now;
                _ValidationResultDTO = SupportGroupMember_Repository.UpdateSupportGroupMember(SupportGroupMemberDTO);
            }
            //If IsActive Change, update the userpermissions
            if (_ValidationResultDTO.Result)
            {
                if (_previousSupportGroupMemberDTO.IsActive != SupportGroupMemberDTO.IsActive)
                {
                    //This function applies when a user of a support group becomes active.
                    SupportGroupMemberDTO.AddedByID = SupportGroupMemberDTO.LastUpdateByID;
                    UpdateRoleToUserBySupportGroup(SupportGroupMemberDTO);
                }
            }
            if (_ValidationResultDTO.Result)
            {
                if ((_previousSupportGroupMemberDTO.UserDTO.ID != SupportGroupMemberDTO.UserDTO.ID) || (_previousSupportGroupMemberDTO.RoleDTO.ID != SupportGroupMemberDTO.RoleDTO.ID))
                {
                    UnassingRoleToUserBySupportGroup(_previousSupportGroupMemberDTO);
                    SupportGroupMemberDTO.AddedByID = SupportGroupMemberDTO.LastUpdateByID;
                    AssignRoleToUserBySupportGroup(SupportGroupMemberDTO);
                }
                if (_ValidationResultDTO.Result)
                {
                    ChangeLog_Service.BuildChangeLogActionUpdate<SupportGroupMemberDTO>(_previousSupportGroupMemberDTO,SupportGroupMemberDTO, (int)SupportGroupMemberDTO.LastUpdateByID, (int)SupportGroupMemberDTO.ID);

                }
            }

            return _ValidationResultDTO;
        }
        public static ValidationResultDTO DeleteSupportGroupMember_Global(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _ValidationResultDTO = SupportGroupMember_Validator.DeleteSupportGroupMember_Validation(SupportGroupMemberDTO);
            var _previousSupportGroupMemberDTO = GetSupportGroupMemberList_Global(new SupportGroupMemberDTO { ID = SupportGroupMemberDTO.ID }).FirstOrDefault();
            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = SupportGroupMember_Repository.DeleteSupportGroupMember(SupportGroupMemberDTO);
            }
            if (_ValidationResultDTO.Result)
            {
                UnassingRoleToUserBySupportGroup(SupportGroupMemberDTO);
                if (_ValidationResultDTO.Result)
                {
                    ChangeLog_Service.BuildChangeLogActionDelete<SupportGroupMemberDTO>(_previousSupportGroupMemberDTO, (int)SupportGroupMemberDTO.LastUpdateByID, (int)SupportGroupMemberDTO.ID);

                }
            }
            return _ValidationResultDTO;
        }
        public static List<SupportGroupMemberDTO> GetSupportGroupMemberList_Global(SupportGroupMemberDTO SupportGroupMemberDTO, PagedResultDTO<SupportGroupMemberDTO> PagedResultDTO = null)
        {
            var _supportgroupmemberglobalList = new List<SupportGroupMemberDTO>();
            try
            {
                var _supportgroupmemberList = SupportGroupMember_Repository.GetSupportGroupMemberList(SupportGroupMemberDTO, PagedResultDTO);
                // if SupportGroupMember is empty, return list
                if (_supportgroupmemberList.Count() == 0)
                {
                    _supportgroupmemberglobalList = _supportgroupmemberList;
                    return _supportgroupmemberglobalList;
                }
                if (!SupportGroupMemberDTO.GetSupportGroupDTO && !SupportGroupMemberDTO.GetUserDTO && !SupportGroupMemberDTO.GetRoleDTO)
                {
                    _supportgroupmemberglobalList = _supportgroupmemberList;
                    return _supportgroupmemberglobalList;
                }
                _supportgroupmemberglobalList = GetSupportGroupMemberRelatedData(SupportGroupMemberDTO, _supportgroupmemberList);

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _supportgroupmemberglobalList;
        }



        public static List<SupportGroupMemberDTO> GetSupportGroupMemberRelatedData(SupportGroupMemberDTO SupportGroupMemberDTO, List<SupportGroupMemberDTO> SupportGroupMemberList)
        {
            var _supportgroupmemberglobalList = new List<SupportGroupMemberDTO>();
            var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();
            var _userDict = new Dictionary<int?, UserDTO>();
            var _roleDict = new Dictionary<int?, RoleDTO>();

            try
            {
                if (SupportGroupMemberDTO.GetSupportGroupDTO)
                {
                    SupportGroupMemberDTO.SupportGroupDTO.SupportGroupIDArray = SupportGroupMemberList.GroupBy(g => g.SupportGroupDTO.ID)
                            .Select(s => s.Key)
                            .ToArray();

                    _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(SupportGroupMemberDTO.SupportGroupDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (SupportGroupMemberDTO.GetUserDTO)
                {
                    SupportGroupMemberDTO.UserDTO.UserIDArray = SupportGroupMemberList.GroupBy(g => g.UserDTO.ID)
                            .Select(s => s.Key)
                            .ToArray();

                    _userDict = User_Service.GetUserList_Global(SupportGroupMemberDTO.UserDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (SupportGroupMemberDTO.GetRoleDTO)
                {
                    SupportGroupMemberDTO.RoleDTO.RoleIDArray = SupportGroupMemberList.GroupBy(g => g.RoleDTO.ID)
                            .Select(s => s.Key)
                            .ToArray();

                    _roleDict = Role_Service.GetRoleList_Global(SupportGroupMemberDTO.RoleDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _supportgroupmemberDTO in SupportGroupMemberList)
                {
                    if (SupportGroupMemberDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_supportgroupmemberDTO.SupportGroupDTO.ID))
                    {
                        _supportgroupmemberDTO.SupportGroupDTO = _supportgroupDict[_supportgroupmemberDTO.SupportGroupDTO.ID];
                    }
                    if (SupportGroupMemberDTO.GetUserDTO && _userDict.ContainsKey(_supportgroupmemberDTO.UserDTO.ID))
                    {
                        _supportgroupmemberDTO.UserDTO = _userDict[_supportgroupmemberDTO.UserDTO.ID];
                    }
                    if (SupportGroupMemberDTO.GetRoleDTO && _roleDict.ContainsKey(_supportgroupmemberDTO.RoleDTO.ID))
                    {
                        _supportgroupmemberDTO.RoleDTO = _roleDict[_supportgroupmemberDTO.RoleDTO.ID];
                    }
                    _supportgroupmemberglobalList.Add(_supportgroupmemberDTO);
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _supportgroupmemberglobalList;
        }




        public static int GetSupportGroupMemberTotalCount(PagedResultDTO<SupportGroupMemberDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = SupportGroupMember_Repository.GetSupportGroupMemberCount(PagedResultDTO.Filter, PagedResultDTO);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return PagedResultDTO.TotalCount;
        }
        #endregion

        #region Business Logic

        public static ValidationResultDTO AssignRoleToUserBySupportGroup(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO();
            try
            {
                var _user_RoleDTO = new User_RoleDTO
                {
                    UserID = SupportGroupMemberDTO.UserDTO.ID,
                    RoleID = SupportGroupMemberDTO.RoleDTO.ID,
                    AddedByID = SupportGroupMemberDTO.AddedByID,
                    IsActive = SupportGroupMemberDTO.IsActive
                };
                _validationResultDTO = User_Role_Service.CreateUser_Role_Global(_user_RoleDTO);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _validationResultDTO;
        }

        public static ValidationResultDTO UpdateRoleToUserBySupportGroup(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            //This function applies when a user of a support group becomes active. 
            var _validationResultDTO = new ValidationResultDTO();
            try
            {
                if (SupportGroupMemberDTO.IsActive == false)
                {
                    _validationResultDTO = SupportGroupMember_Validator.UnnasingRole_User_Validation(SupportGroupMemberDTO);
                }
                if (_validationResultDTO.Result)
                {
                    _validationResultDTO = AssignRoleToUserBySupportGroup(SupportGroupMemberDTO);
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _validationResultDTO;
        }

        public static ValidationResultDTO UnassingRoleToUserBySupportGroup(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            //This function applies when a user of a support group becomes active. 
            var _validationResultDTO = new ValidationResultDTO();
            try
            {
                _validationResultDTO = SupportGroupMember_Validator.UnnasingRole_User_Validation(SupportGroupMemberDTO);

                if (_validationResultDTO.Result)
                {
                    var _user_RoleDTO = new User_RoleDTO
                    {
                        UserDTO = SupportGroupMemberDTO.UserDTO,
                        RoleDTO = SupportGroupMemberDTO.RoleDTO,
                        IsActive = true
                    };
                    _user_RoleDTO = User_Role_Service.GetUser_RoleList_Global(_user_RoleDTO).FirstOrDefault();
                    if (_user_RoleDTO != null)
                    {
                        _validationResultDTO = User_Role_Service.DeleteUser_Role_Global(_user_RoleDTO);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _validationResultDTO;
        }

        public static ValidationResultDTO ValidateSupportGroupMemberPermissions(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO();
            try
            {
                //get user roles
                SupportGroupMemberDTO.UserDTO.GetRoleArray = true;
                var _userDTO = User_Service.GetUserList_Global(SupportGroupMemberDTO.UserDTO).FirstOrDefault();

                //validate permission exist and support group to avoid exceptions or errors
                if ((SupportGroupMemberDTO.UserDTO.PermissionDTO != null && SupportGroupMemberDTO.UserDTO.PermissionDTO.ID > 0) &&
                    (SupportGroupMemberDTO.SupportGroupDTO.ID != null && SupportGroupMemberDTO.SupportGroupDTO.ID > 0))
                {
                   
                    if (!_userDTO.RoleIDArray.Contains((int)Role_Enum.SystemAdmin) && !_userDTO.RoleIDArray.Contains((int)Role_Enum.WarehouseReceiver))
                    {
                        //get roles by permission ID
                        var _rolesPermissionIDArray = Role_Permission_Service.GetRole_PermissionList_Global(new Role_PermissionDTO
                        {
                            PermissionDTO = SupportGroupMemberDTO.UserDTO.PermissionDTO,
                        }).Select(s => s.RoleDTO.ID).ToArray();

                        if (_rolesPermissionIDArray.Length > 0)
                        {
                            //check if the user has the role assigned to that support group
                            var _supportGroupMemberDTO = GetSupportGroupMemberList_Global(new SupportGroupMemberDTO
                            {
                                IsActive = true,
                                SupportGroupDTO = SupportGroupMemberDTO.SupportGroupDTO,
                                UserDTO = SupportGroupMemberDTO.UserDTO,
                                RoleIDArray = _rolesPermissionIDArray
                            }).FirstOrDefault();


                            if (_supportGroupMemberDTO == null)
                            {
                                // If the user has the permission but does not have any role related to that permission,
                                //then it is a special permission.
                                if (_userDTO.RoleIDArray.Intersect(_rolesPermissionIDArray).Any())
                                {
                                    _validationResultDTO.Result = false;
                                    _validationResultDTO.Message = "Error!";
                                    _validationResultDTO.Description = "You don't have the necessary permissions to perform this action in this support group.";
                                }
                            }
                        }
                        else
                        {
                            _validationResultDTO.Result = false;
                            _validationResultDTO.Message = "Error!";
                            _validationResultDTO.Description = "The permission is not assigned to any role";
                        }

                    }
                }
                else
                {
                    //This is in the special case in which you are looking to update a station for an item without a support group.
                    if (!_userDTO.RoleIDArray.Contains((int)Role_Enum.SystemAdmin))
                    {
                        _validationResultDTO.Result = false;
                        _validationResultDTO.Message = "Error!";
                        _validationResultDTO.Description = "Something went wrong, please verify if you selected a support group. ";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _validationResultDTO;
        }


        #endregion
    }
}
