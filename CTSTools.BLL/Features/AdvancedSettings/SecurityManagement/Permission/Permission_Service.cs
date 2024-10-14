using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
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
        public static List<PermissionDTO> GetPermissionList_Global(PermissionDTO PermissionDTO,PagedResultDTO<PermissionDTO> PagedResultDTO = null)
        {
            var _permissionglobalList = new List<PermissionDTO>();
            try
            {
                var _permissionList = Permission_Repository.GetPermissionList(PermissionDTO,PagedResultDTO);
                // if Permission is empty, return list
                if (_permissionList.Count() == 0)
                {
                        _permissionglobalList = _permissionList;
                        return _permissionglobalList;
                }
                if (!PermissionDTO.GetActionDTO)
                {
                        _permissionglobalList = _permissionList;
                        return _permissionglobalList;
                }
                _permissionglobalList = GetPermissionRelatedData(PermissionDTO,_permissionList);                        

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _permissionglobalList;
        }



        public static List<PermissionDTO> GetPermissionRelatedData(PermissionDTO PermissionDTO,List<PermissionDTO> PermissionList)
        {
            var _permissionglobalList = new List<PermissionDTO>();
            var _actionDict = new Dictionary<int?,ActionDTO>();
            
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
                foreach (var _permissionDTO in PermissionList)
                {
                        if (PermissionDTO.GetActionDTO && _actionDict.ContainsKey(_permissionDTO.ActionDTO.ID))
                        {
                                _permissionDTO.ActionDTO = _actionDict[_permissionDTO.ActionDTO.ID];
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
                //1. Get permission

                var _permissionDTO = new PermissionDTO
                {
                    Module = UserDTO.PermissionDTO.Module,
                    IsActive = true,
                    ActionDTO = UserDTO.PermissionDTO.ActionDTO
                };
                _permissionDTO = GetPermissionList_Global(_permissionDTO).FirstOrDefault();

                if(_permissionDTO != null)
                {
                    var _user_permissionDTO = User_Permission_Service.GetUser_PermissionList_Global(new User_PermissionDTO
                    {
                        UserDTO = UserDTO,
                        PermissionDTO = _permissionDTO,
                        IsActive=true
                    }).FirstOrDefault();
                    if(_user_permissionDTO != null)
                    {
                        return _validation_ResultDTO;
                    }
                }
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Don't have access to this action.";
                _validation_ResultDTO.Description = "You can't perform this action. In case this is an error, please contact your system administrator.";

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
