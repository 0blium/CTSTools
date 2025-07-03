using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;

public class Module_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateModule_Global(ModuleDTO ModuleDTO)
    {
        var _ValidationResultDTO = Module_Validator.CreateModule_Validation(ModuleDTO);
        if (_ValidationResultDTO.Result)
        {
            ModuleDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Module_Repository.CreateModule(ModuleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateModule_Global(ModuleDTO ModuleDTO)
    {
        var _ValidationResultDTO = Module_Validator.UpdateModule_Validation(ModuleDTO);
        if (_ValidationResultDTO.Result)
        {
            ModuleDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Module_Repository.UpdateModule(ModuleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteModule_Global(ModuleDTO ModuleDTO)
    {
        var _ValidationResultDTO = Module_Validator.DeleteModule_Validation(ModuleDTO);
        if (_ValidationResultDTO.Result)
        {
            //Get Permissions by Module
            var _permisionsIDArray = Permission_Service.GetPermissionList_Global(new PermissionDTO { ModuleID = ModuleDTO.ID })
                .Select(x=>x.ID).ToArray();
            if (_permisionsIDArray.Count() > 0)
            {
                //Delete Role  Permission
                var _role_PermissionList = Role_Permission_Service.GetRole_PermissionList_Global(
                    new Role_PermissionDTO { PermissionIDArray = _permisionsIDArray });
                if(_role_PermissionList.Count()>0)
                    Role_Permission_Service.DeleteMultiple_Global(_role_PermissionList);
                
                //Delete User Permission
                var _user_PermissionList = User_Permission_Service.GetUser_PermissionList_Global(
                    new User_PermissionDTO { PermissionIDArray = _permisionsIDArray });
                if(_user_PermissionList.Count()>0)
                    User_Permission_Service.DeleteMultiple_Global(_user_PermissionList);                                
                
                //Delete Permission
                var _permissionList = Permission_Service.GetPermissionList_Global(
                    new PermissionDTO { PermissionIDArray= _permisionsIDArray });
                if(_permissionList.Count>0)
                    Permission_Service.DeleteMultiple_Global(_permissionList);
            }

            _ValidationResultDTO = Module_Repository.DeleteModule(ModuleDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ModuleDTO> GetModuleList_Global(ModuleDTO ModuleDTO, PagedResultDTO<ModuleDTO> PagedResultDTO = null)
    {
        var _ModuleglobalList = new List<ModuleDTO>();
        try
        {
            var _ModuleList = Module_Repository.GetModuleList(ModuleDTO, PagedResultDTO);
            // if Module is empty, return list
            _ModuleglobalList = _ModuleList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ModuleglobalList;
    }


    public static int GetModuleTotalCount(PagedResultDTO<ModuleDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Module_Repository.GetModuleCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    #region Advanced Module Set Up
    public static ValidationResultDTO CreateAdvancedModule_Global(ModuleDTO ModuleDTO)
    {
        var _actionIDList = new List<int>();
        //step 1 create module
        var _validationResultDTO = Module_Validator.CreateModuleSetUp_Validation(ModuleDTO);


        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        _validationResultDTO = CreateModule_Global(ModuleDTO);

        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //step 2 create permissions
        

        var _actionDTOList = Action_Service.GetActionList_Global(new ActionDTO { ActionIDArray=ModuleDTO.ActionIDArray });
        
        var _permissionDTOList = _actionDTOList.Select(ActionDTO => new PermissionDTO {
            Name = $"Allow to {(ActionDTO.ID == (int)Action_Enum.Read ? "see" : ActionDTO.ID == (int)Action_Enum.Update ? "edit" : ActionDTO.Name.ToLower())} " +
            $"{System.Text.RegularExpressions.Regex.Replace(ModuleDTO.Name, "(?<!^)(?=[A-Z])", " ").ToLower()}",
            ModuleID = ModuleDTO.ID,
            ActionID = ActionDTO.ID,
            AddedByID = ModuleDTO.AddedByID
           }).ToList();
        if(_permissionDTOList.Count() > 0)
            _validationResultDTO = Permission_Service.CreateMultiple_Global(_permissionDTOList);

        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //step 3 assign permission to role
        var _permissionIDArray = Permission_Service.GetPermissionList_Global(new PermissionDTO { ModuleID = ModuleDTO.ID })
            .Select(x=>x.ID).ToArray();

        var _rolePermissionDTOList = ModuleDTO.RoleIDArray.SelectMany(roleID => _permissionIDArray,(roleId,permissionID) => 
        new Role_PermissionDTO
        {
            RoleID=roleId,
            PermissionID=permissionID,
            AddedByID = ModuleDTO.AddedByID
        }).ToList();

        if (_rolePermissionDTOList.Count() > 0)
            _validationResultDTO = Role_Permission_Service.CreateMultiple_Global(_rolePermissionDTOList);

        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //step 4 assign permission to users
        var _userIDArray = User_Role_Service.GetUser_RoleList_Global(new User_RoleDTO { RoleIDArray = ModuleDTO.RoleIDArray })
            .GroupBy(g => g.UserID).Select(s => s.Key).ToArray();

        var _user_PermissionDTOList = _userIDArray.SelectMany(userID => _permissionIDArray, (userID, permissionID) => 
        new User_PermissionDTO
        {
            UserID = userID,
            PermissionID = permissionID,
            AddedByID = ModuleDTO.AddedByID
        }).ToList();

        if (_user_PermissionDTOList.Count() > 0)
            _validationResultDTO = User_Permission_Service.CreateMultiple_Global(_user_PermissionDTOList);



        return _validationResultDTO;

    }
    public static ValidationResultDTO UpdateSetUpModule_Global(ModuleDTO ModuleDTO)
    {
        var _actionIDList = new List<int>();
        //step 1 Update module
        
        var _validationResultDTO = UpdateModule_Global(ModuleDTO);

        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //step 2 create permissions

        
        var _currentActionsIDArray = Permission_Service.GetPermissionList_Global(new PermissionDTO { ModuleID = ModuleDTO.ID })
            .Select(x => x.ActionID).ToArray();

        ModuleDTO.ActionIDArray = ModuleDTO.ActionIDArray.Except(_currentActionsIDArray).ToArray();

        if (ModuleDTO.ActionIDArray == null || ModuleDTO.ActionIDArray.Count() == 0)
            return _validationResultDTO;


        var _actionDTOList = Action_Service.GetActionList_Global(new ActionDTO { ActionIDArray = ModuleDTO.ActionIDArray });



        var _permissionDTOList = _actionDTOList.Select(ActionDTO => new PermissionDTO
        {
            Name = $"Allow to {(ActionDTO.ID == (int)Action_Enum.Read ? "see" : ActionDTO.ID == (int)Action_Enum.Update ? "edit" : ActionDTO.Name.ToLower())} " +
            $"{System.Text.RegularExpressions.Regex.Replace(ModuleDTO.Name, "(?<!^)(?=[A-Z])", " ").ToLower()}",
            ModuleID = ModuleDTO.ID,
            ActionID = ActionDTO.ID,
            AddedByID = ModuleDTO.LastUpdateByID
        }).ToList();
        if (_permissionDTOList.Count() > 0)
            Permission_Service.CreateMultiple_Global(_permissionDTOList);

        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //step 3 assign permission to role
        var _permissionIDArray = Permission_Service.GetPermissionList_Global(new PermissionDTO { ModuleID = ModuleDTO.ID,ActionIDArray=ModuleDTO.ActionIDArray })
            .Select(x => x.ID).ToArray();

        var _rolePermissionDTOList = ModuleDTO.RoleIDArray.SelectMany(roleID => _permissionIDArray, (roleId, permissionID) =>
        new Role_PermissionDTO
        {
            RoleID = roleId,
            PermissionID = permissionID,
            AddedByID = ModuleDTO.LastUpdateByID
        }).ToList();

        if (_rolePermissionDTOList.Count() > 0)
            _validationResultDTO = Role_Permission_Service.CreateMultiple_Global(_rolePermissionDTOList);

        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        //step 4 assign permission to users
        var _userIDArray = User_Role_Service.GetUser_RoleList_Global(new User_RoleDTO { RoleIDArray = ModuleDTO.RoleIDArray })
            .GroupBy(g => g.UserID).Select(s => s.Key).ToArray();

        var _user_PermissionDTOList = _userIDArray.SelectMany(userID => _permissionIDArray, (userID, permissionID) =>
        new User_PermissionDTO
        {
            UserID = userID,
            PermissionID = permissionID,
            AddedByID = ModuleDTO.LastUpdateByID
        }).ToList();

        if (_user_PermissionDTOList.Count() > 0)
            _validationResultDTO = User_Permission_Service.CreateMultiple_Global(_user_PermissionDTOList);



        return _validationResultDTO;

    }

    #endregion

    #endregion
}
