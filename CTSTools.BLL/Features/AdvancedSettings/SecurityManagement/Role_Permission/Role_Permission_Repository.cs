using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.Security.Permissions.Role_Permission;

public class Role_Permission_Repository
{
    public static List<Role_PermissionDTO> GetRole_PermissionList(Role_PermissionDTO Role_PermissionDTO, PagedResultDTO<Role_PermissionDTO> PagedResultDTO = null)
    {
        var _role_permissionList = new List<Role_PermissionDTO>();
        try
        {
            // Role_Permission Filters
            var _groupOperator = Role_Permission_DXFilter.GetRole_Permission_DXFilter(Role_PermissionDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);

            if (PagedResultDTO?.dxFilters != null)
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;

            using var _session = XPO_Helper.GetNewSession();
            var _role_permissionCollection = new XPCollection<Role_PermissionXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = PagedResultDTO?.SortPropertyName != null ? new SortingCollection(_sortProperty) :  new SortingCollection(new SortProperty(nameof(Role_PermissionXPO.Oid), SortingDirection.Ascending))
            };

            if (_role_permissionCollection.AsQueryable().Count() > 0)
                _role_permissionList = _role_permissionCollection.Select(Role_PermissionXPO => Role_PermissionMap.XPOToDTO(Role_PermissionXPO)).ToList();
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _role_permissionList;
    }
    public static int GetRole_PermissionCount(Role_PermissionDTO Role_PermissionDTO, PagedResultDTO<Role_PermissionDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Role_Permission_DXFilter.GetRole_Permission_DXFilter(Role_PermissionDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<Role_PermissionXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateRole_Permission(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _role_permissionXPO = Role_PermissionMap.DTOtoXPO(Role_PermissionDTO, _unit);
                _unit.Save(_role_permissionXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateRole_Permission(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _role_permissionXPO = Role_PermissionMap.DTOtoXPO(Role_PermissionDTO, _unit);
                _unit.Save(_role_permissionXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteRole_Permission(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _role_permissionXPO = Role_PermissionMap.DTOtoXPO(Role_PermissionDTO, _unit);
                _unit.Delete(_role_permissionXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
}
