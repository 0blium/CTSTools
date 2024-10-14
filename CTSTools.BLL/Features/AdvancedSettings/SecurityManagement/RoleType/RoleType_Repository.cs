using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Security.Role;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.Security.Roles.RoleType;

public class RoleType_Repository
{
    public static List<RoleTypeDTO> GetRoleTypeList(RoleTypeDTO RoleTypeDTO, PagedResultDTO<RoleTypeDTO> PagedResultDTO = null)
    {
        var _roletypeList = new List<RoleTypeDTO>();
        try
        {
            // RoleType Filters
            var _groupOperator = RoleType_DXFilter.GetRoleType_DXFilter(RoleTypeDTO);
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
                var _roletypeCollection = new XPCollection<RoleTypeXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(RoleTypeXPO.Oid), SortingDirection.Ascending))
                };

                if (_roletypeCollection.AsQueryable().Count() > 0)
                {
                    _roletypeList = _roletypeCollection.Select(RoleTypeXPO => RoleTypeMap.XPOToDTO(RoleTypeXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _roletypeList;
    }
    public static int GetRoleTypeCount(RoleTypeDTO RoleTypeDTO, PagedResultDTO<RoleTypeDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = RoleType_DXFilter.GetRoleType_DXFilter(RoleTypeDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<RoleTypeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateRoleType(RoleTypeDTO RoleTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _roletypeXPO = RoleTypeMap.DTOtoXPO(RoleTypeDTO, _unit);
                _unit.Save(_roletypeXPO);
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
    public static ValidationResultDTO UpdateRoleType(RoleTypeDTO RoleTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _roletypeXPO = RoleTypeMap.DTOtoXPO(RoleTypeDTO, _unit);
                _unit.Save(_roletypeXPO);
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
    public static ValidationResultDTO DeleteRoleType(RoleTypeDTO RoleTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _roletypeXPO = RoleTypeMap.DTOtoXPO(RoleTypeDTO, _unit);
                _unit.Delete(_roletypeXPO);
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
