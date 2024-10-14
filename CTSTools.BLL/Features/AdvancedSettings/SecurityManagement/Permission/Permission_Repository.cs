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

namespace CTSTools.BLL.Features.Security.Permissions.Permission
{
    public class Permission_Repository
    {
        public static List<PermissionDTO> GetPermissionList(PermissionDTO PermissionDTO, PagedResultDTO<PermissionDTO> PagedResultDTO = null)
        {
            var _permissionList = new List<PermissionDTO>();
            try
            {
                // Permission Filters
                var _groupOperator = Permission_DXFilter.GetPermission_DXFilter(PermissionDTO);
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
                    var _permissionCollection = new XPCollection<PermissionXPO>(_session, _groupOperator, _sortProperty)
                    {
                        TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                        SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                        Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(PermissionXPO.Oid), SortingDirection.Ascending))
                    };

                    if (_permissionCollection.AsQueryable().Count() > 0)
                    {
                        _permissionList = _permissionCollection.Select(PermissionXPO => PermissionMap.XPOToDTO(PermissionXPO)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _permissionList;
        }
        public static int GetPermissionCount(PermissionDTO PermissionDTO, PagedResultDTO<PermissionDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = Permission_DXFilter.GetPermission_DXFilter(PermissionDTO);
                if ( PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<PermissionXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreatePermission(PermissionDTO PermissionDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _permissionXPO = PermissionMap.DTOtoXPO(PermissionDTO, _unit);
                    _unit.Save(_permissionXPO);
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
        public static ValidationResultDTO UpdatePermission(PermissionDTO PermissionDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _permissionXPO = PermissionMap.DTOtoXPO(PermissionDTO, _unit);
                    _unit.Save(_permissionXPO);
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
        public static ValidationResultDTO DeletePermission(PermissionDTO PermissionDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _permissionXPO = PermissionMap.DTOtoXPO(PermissionDTO, _unit);
                    _unit.Delete(_permissionXPO);
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
}
