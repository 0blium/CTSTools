using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;

public class DepartmentResponsible_Repository
{
    public static List<DepartmentResponsibleDTO> GetDepartmentList(DepartmentResponsibleDTO DepartmentResponsibleDTO, PagedResultDTO<DepartmentResponsibleDTO> PagedResultDTO = null)
    {
        var _departmentResponsibleList = new List<DepartmentResponsibleDTO>();
        try
        {
            // Department Filters
            var _groupOperator = DepartmentResponsible_DXFilter.GetDepartmentResponsibleFilters(DepartmentResponsibleDTO);
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
                var _departmentResponsibleCollection = new XPCollection<DepartmentResponsibleXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DepartmentResponsibleXPO.Oid), SortingDirection.Ascending))
                };

                if (_departmentResponsibleCollection.AsQueryable().Count() > 0)
                {
                    _departmentResponsibleList = _departmentResponsibleCollection.Select(DepartmentResponsibleXPO => DepartmentResponsibleMap.XPOToDTO(DepartmentResponsibleXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentResponsibleList;
    }
    public static int GetDepartmentResponsibleCount(DepartmentResponsibleDTO DepartmentResponsibleDTO, PagedResultDTO<DepartmentResponsibleDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DepartmentResponsible_DXFilter.GetDepartmentResponsibleFilters(DepartmentResponsibleDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DepartmentResponsibleXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDepartmentResponsible(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _departmentResponsibleXPO = DepartmentResponsibleMap.DTOtoXPO(DepartmentResponsibleDTO, _unit);
                _unit.Save(_departmentResponsibleXPO);
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
    public static ValidationResultDTO UpdateDepartmentResponsible(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _departmentResponsibleXPO = DepartmentResponsibleMap.DTOtoXPO(DepartmentResponsibleDTO, _unit);
                _unit.Save(_departmentResponsibleXPO);
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
    public static ValidationResultDTO DeleteDepartmentResponsible(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _departmentResponsibleXPO = DepartmentResponsibleMap.DTOtoXPO(DepartmentResponsibleDTO, _unit);
                _unit.Delete(_departmentResponsibleXPO);
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
