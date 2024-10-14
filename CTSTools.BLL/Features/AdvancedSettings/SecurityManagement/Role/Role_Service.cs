using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;

public class Role_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateRole_Global(RoleDTO RoleDTO)
    {
        var _ValidationResultDTO = Role_Validator.CreateRole_Validation(RoleDTO);
        if (_ValidationResultDTO.Result)
        {
            RoleDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Role_Repository.CreateRole(RoleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateRole_Global(RoleDTO RoleDTO)
    {
        var _ValidationResultDTO = Role_Validator.UpdateRole_Validation(RoleDTO);
        if (_ValidationResultDTO.Result)
        {
            RoleDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Role_Repository.UpdateRole(RoleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteRole_Global(RoleDTO RoleDTO)
    {
        var _ValidationResultDTO = Role_Validator.DeleteRole_Validation(RoleDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Role_Repository.DeleteRole(RoleDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<RoleDTO> GetRoleList_Global(RoleDTO RoleDTO,PagedResultDTO<RoleDTO> PagedResultDTO = null)
    {
        var _roleglobalList = new List<RoleDTO>();
        try
        {
            var _roleList = Role_Repository.GetRoleList(RoleDTO,PagedResultDTO);
            // if Role is empty, return list
            _roleglobalList = _roleList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _roleglobalList;
    }


     public static int GetRoleTotalCount(PagedResultDTO<RoleDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =Role_Repository.GetRoleCount(PagedResultDTO.Filter, PagedResultDTO);
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
