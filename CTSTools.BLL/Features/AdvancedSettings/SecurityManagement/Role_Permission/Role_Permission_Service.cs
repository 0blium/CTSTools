using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Security.Permissions.Role_Permission;

public class Role_Permission_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateRole_Permission_Global(Role_PermissionDTO Role_PermissionDTO)
    {
        var _ValidationResultDTO = Role_Permission_Validator.CreateRole_Permission_Validation(Role_PermissionDTO);
        if (_ValidationResultDTO.Result)
        {
            Role_PermissionDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Role_Permission_Repository.CreateRole_Permission(Role_PermissionDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateRole_Permission_Global(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = Role_Permission_Validator.UpdateRole_Permission_Validation(Role_PermissionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        Role_PermissionDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Role_Permission_Repository.UpdateRole_Permission(Role_PermissionDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteRole_Permission_Global(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = Role_Permission_Validator.DeleteRole_Permission_Validation(Role_PermissionDTO);
        var _oldRole_PermissionDTO = GetRole_PermissionList_Global(new Role_PermissionDTO { ID = Role_PermissionDTO.ID }).FirstOrDefault();
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        _validationResultDTO = Role_Permission_Repository.DeleteRole_Permission(Role_PermissionDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        _validationResultDTO = User_Permission_Service.DeletePermisssionToAllUsersByRole(_oldRole_PermissionDTO);
        return _validationResultDTO;
    }
    public static List<Role_PermissionDTO> GetRole_PermissionList_Global(Role_PermissionDTO Role_PermissionDTO, PagedResultDTO<Role_PermissionDTO> PagedResultDTO = null)
    {
        var _role_permissionglobalList = new List<Role_PermissionDTO>();
        try
        {
            var _role_permissionList = Role_Permission_Repository.GetRole_PermissionList(Role_PermissionDTO, PagedResultDTO);
            // if Role_Permission is empty, return list
            if (_role_permissionList.Count() == 0)
            {
                _role_permissionglobalList = _role_permissionList;
                return _role_permissionglobalList;
            }
            if (!Role_PermissionDTO.GetPermissionDTO && !Role_PermissionDTO.GetRoleDTO)
            {
                _role_permissionglobalList = _role_permissionList;
                return _role_permissionglobalList;
            }
            _role_permissionglobalList = GetRole_PermissionRelatedData(Role_PermissionDTO, _role_permissionList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _role_permissionglobalList;
    }



    public static List<Role_PermissionDTO> GetRole_PermissionRelatedData(Role_PermissionDTO Role_PermissionDTO, List<Role_PermissionDTO> Role_PermissionList)
    {
        var _role_permissionglobalList = new List<Role_PermissionDTO>();
        var _permissionDict = new Dictionary<int?, PermissionDTO>();
        var _roleDict = new Dictionary<int?, RoleDTO>();

        try
        {
            if (Role_PermissionDTO.GetPermissionDTO)
            {
                Role_PermissionDTO.PermissionDTO.PermissionIDArray = Role_PermissionList.GroupBy(g => g.PermissionDTO.ID)
                                                                                        .Select(s => s.Key)
                                                                                        .ToArray();

                _permissionDict = Permission_Service.GetPermissionList_Global(Role_PermissionDTO.PermissionDTO)
                                                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Role_PermissionDTO.GetRoleDTO)
            {
                Role_PermissionDTO.RoleDTO.RoleIDArray = Role_PermissionList.GroupBy(g => g.RoleDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _roleDict = Role_Service.GetRoleList_Global(Role_PermissionDTO.RoleDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _role_permissionDTO in Role_PermissionList)
            {
                if (Role_PermissionDTO.GetPermissionDTO && _permissionDict.ContainsKey(_role_permissionDTO.PermissionDTO.ID))
                {
                    _role_permissionDTO.PermissionDTO = _permissionDict[_role_permissionDTO.PermissionDTO.ID];
                }
                if (Role_PermissionDTO.GetRoleDTO && _roleDict.ContainsKey(_role_permissionDTO.RoleDTO.ID))
                {
                    _role_permissionDTO.RoleDTO = _roleDict[_role_permissionDTO.RoleDTO.ID];
                }
                _role_permissionglobalList.Add(_role_permissionDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _role_permissionglobalList;
    }




    public static int GetRole_PermissionTotalCount(PagedResultDTO<Role_PermissionDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Role_Permission_Repository.GetRole_PermissionCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    public static ValidationResultDTO CreateRole_PermissionByArray(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            _validationResultDTO = Role_Permission_Validator.CreateRole_PermissionByIDArray_Validation(Role_PermissionDTO);
            if (_validationResultDTO.Result)
            {
                foreach (var _permissionID in Role_PermissionDTO.PermissionIDArray)
                {
                    var _role_PermissionDTO = new Role_PermissionDTO
                    {
                        RoleID = Role_PermissionDTO.RoleID,
                        AddedByID = Role_PermissionDTO.AddedByID,
                        IsActive = Role_PermissionDTO.IsActive,
                        PermissionID = _permissionID
                    };

                    _validationResultDTO = CreateRole_Permission_Global(_role_PermissionDTO);

                    //Update Permission all Users by role
                    if (_validationResultDTO.Result)
                    {
                        _validationResultDTO = User_Permission_Service.UpdatePermisssionToAllUsersByRole(_role_PermissionDTO);
                    }
                }
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

    #endregion
}
