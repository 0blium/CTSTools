using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.User;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_Permission_Repository
{
    public static List<User_PermissionDTO> GetUser_PermissionList(User_PermissionDTO User_PermissionDTO, PagedResultDTO<User_PermissionDTO> PagedResultDTO = null)
    {
        var _user_permissionList = new List<User_PermissionDTO>();
        try
        {
            // User_Permission Filters
            var _groupOperator = User_Permission_DXFilter.GetUser_Permission_DXFilter(User_PermissionDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _user_permissionCollection = new XPCollection<User_PermissionXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(User_PermissionXPO.Oid), SortingDirection.Ascending))
                };

                if (_user_permissionCollection.AsQueryable().Count() > 0)
                {
                    _user_permissionList = _user_permissionCollection.Select(User_PermissionXPO => User_PermissionMap.XPOToDTO(User_PermissionXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _user_permissionList;
    }
    public static int GetUser_PermissionCount(User_PermissionDTO User_PermissionDTO, PagedResultDTO<User_PermissionDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = User_Permission_DXFilter.GetUser_Permission_DXFilter(User_PermissionDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<User_PermissionXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateUser_Permission(User_PermissionDTO User_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _user_permissionXPO = User_PermissionMap.DTOtoXPO(User_PermissionDTO, _unit);
                _unit.Save(_user_permissionXPO);
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
    public static ValidationResultDTO UpdateUser_Permission(User_PermissionDTO User_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _user_permissionXPO = User_PermissionMap.DTOtoXPO(User_PermissionDTO, _unit);
                _unit.Save(_user_permissionXPO);
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
    public static ValidationResultDTO DeleteUser_Permission(User_PermissionDTO User_PermissionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _user_permissionXPO = User_PermissionMap.DTOtoXPO(User_PermissionDTO, _unit);
                _unit.Delete(_user_permissionXPO);
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
    public static ValidationResultDTO CreateMultiple(List<User_PermissionDTO> User_PermissionDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = User_PermissionMap.DTOListToXPOList(User_PermissionDTOList, _unit);
            _unit.Save(_xPOList);
            _unit.CommitChanges();
            //_validationResultDTO.Data = _xPOList;
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

    public static ValidationResultDTO DeleteMultiple(List<User_PermissionDTO> User_PermissionDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = User_PermissionMap.DTOListToXPOList(User_PermissionDTOList, _unit);
            _unit.Delete(_xPOList);
            _unit.CommitChanges();
            _unit.PurgeDeletedObjects();
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
