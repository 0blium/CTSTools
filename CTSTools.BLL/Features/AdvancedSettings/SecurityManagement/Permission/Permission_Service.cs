using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Security.Permissions.Permission
{
    public class Permission_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreatePermission_Global(PermissionDTO PermissionDTO)
        {
            var _ValidationResultDTO = Permission_Validator.CreatePermission_Validation(PermissionDTO);
            if (_ValidationResultDTO.Result)
            {
                PermissionDTO.AddedDate = DateTime.Now;
                _ValidationResultDTO = Permission_Repository.CreatePermission(PermissionDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO UpdatePermission_Global(PermissionDTO PermissionDTO)
        {
            var _ValidationResultDTO = Permission_Validator.UpdatePermission_Validation(PermissionDTO);
            if (_ValidationResultDTO.Result)
            {
                PermissionDTO.LastUpdate = DateTime.Now;
                _ValidationResultDTO = Permission_Repository.UpdatePermission(PermissionDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO DeletePermission_Global(PermissionDTO PermissionDTO)
        {
            var _ValidationResultDTO = Permission_Validator.DeletePermission_Validation(PermissionDTO);
            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = Permission_Repository.DeletePermission(PermissionDTO);
            }
            return _ValidationResultDTO;
        }
        public static List<PermissionDTO> GetPermissionList_Global(PermissionDTO PermissionDTO, PagedResultDTO<PermissionDTO> PagedResultDTO = null)
        {
            var _permissionglobalList = new List<PermissionDTO>();
            try
            {
                var _permissionList = Permission_Repository.GetPermissionList(PermissionDTO, PagedResultDTO);
                // if Permission is empty, return list
                if (_permissionList.Count() == 0)
                {
                    _permissionglobalList = _permissionList;
                    return _permissionglobalList;
                }
                if (!PermissionDTO.GetActionDTO && !PermissionDTO.GetModuleDTO && !PermissionDTO.GetPermissionIDArray)
                {
                    _permissionglobalList = _permissionList;
                    return _permissionglobalList;
                }
                _permissionglobalList = GetPermissionRelatedData(PermissionDTO, _permissionList);

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _permissionglobalList;
        }



        public static List<PermissionDTO> GetPermissionRelatedData(PermissionDTO PermissionDTO, List<PermissionDTO> PermissionList)
        {
            var _permissionglobalList = new List<PermissionDTO>();
            var _actionDict = new Dictionary<int?, ActionDTO>();
            var _moduleDict = new Dictionary<int?, ModuleDTO>();

            try
            {
                if (PermissionDTO.GetActionDTO)
                {
                    PermissionDTO.ActionDTO.ActionIDArray = PermissionList.GroupBy(g => g.ActionDTO.ID)
                            .Select(s => s.Key)
                            .ToArray();

                    _actionDict = Action_Service.GetActionList_Global(PermissionDTO.ActionDTO)
                                                .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (PermissionDTO.GetModuleDTO)
                {
                    PermissionDTO.ModuleDTO.ModuleIDArray = PermissionList.GroupBy(g => g.ModuleDTO.ID)
                            .Select(s => s.Key)
                            .ToArray();

                    _moduleDict = Module_Service.GetModuleList_Global(PermissionDTO.ModuleDTO)
                                                .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (PermissionDTO.GetPermissionIDArray)
                {
                    PermissionDTO.PermissionIDArray = PermissionList.GroupBy(g => g.ID)
                                                                  .Select(s => s.Key)
                                                                  .ToArray();
                }
                    foreach (var _permissionDTO in PermissionList)
                {
                    if (PermissionDTO.GetActionDTO && _actionDict.ContainsKey(_permissionDTO.ActionDTO.ID))
                    {
                        _permissionDTO.ActionDTO = _actionDict[_permissionDTO.ActionDTO.ID];
                    }
                    if (PermissionDTO.GetModuleDTO && _moduleDict.ContainsKey(_permissionDTO.ModuleDTO.ID))
                    {
                        _permissionDTO.ModuleDTO = _moduleDict[_permissionDTO.ModuleDTO.ID];
                    }
                    if (PermissionDTO.GetPermissionIDArray)
                    {
                        _permissionDTO.PermissionIDArray = PermissionDTO.PermissionIDArray;
                    }
                    _permissionglobalList.Add(_permissionDTO);
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _permissionglobalList;
        }




        public static int GetPermissionTotalCount(PagedResultDTO<PermissionDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = Permission_Repository.GetPermissionCount(PagedResultDTO.Filter, PagedResultDTO);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return PagedResultDTO.TotalCount;
        }
        #endregion

        #region Business Logic

        public static ValidationResultDTO ValidatePermission(UserDTO UserDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO();
            try
            {
                var _permissionDTO = new PermissionDTO
                {
                    ModuleName = UserDTO.PermissionDTO.ModuleName,
                    IsActive = true,
                    ActionID = UserDTO.PermissionDTO.ActionDTO.ID
                };
                _permissionDTO = GetPermissionList_Global(_permissionDTO).FirstOrDefault();

                if (_permissionDTO == null)
                    return _validation_ResultDTO = new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Don't have access to this action.",
                        Description = "You can't perform this action. In case this is an error, please contact your system administrator."
                    };

                var _user_permissionDTO = new User_PermissionDTO
                {
                    UserID = UserDTO.ID,
                    PermissionID = _permissionDTO.ID,
                };
                _user_permissionDTO = User_Permission_Service.GetUser_PermissionList_Global(_user_permissionDTO).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Error";
                _validation_ResultDTO.Description = string.Format("There was an error trying to validate the permissions {0}", ex.Message);
            }
            return _validation_ResultDTO;
        }




        #endregion
    }
}
