using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;

public class Department_Repository
{
    public static List<DepartmentDTO> GetDepartmentList(DepartmentDTO DepartmentDTO, PagedResultDTO<DepartmentDTO> PagedResultDTO = null)
    {
        var _departmentList = new List<DepartmentDTO>();
        try
        {
            // Department Filters
            var _groupOperator = Department_DXFilter.GetDepartmentFilters(DepartmentDTO);
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
                var _departmentCollection = new XPCollection<DepartmentXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DepartmentXPO.Oid), SortingDirection.Ascending))
                };

                if (_departmentCollection.AsQueryable().Count() > 0)
                {
                    _departmentList = _departmentCollection.Select(DepartmentXPO => DepartmentMap.XPOToDTO(DepartmentXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _departmentList;
    }
    public static int GetDepartmentCount(DepartmentDTO DepartmentDTO, PagedResultDTO<DepartmentDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Department_DXFilter.GetDepartmentFilters(DepartmentDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DepartmentXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDepartment(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _departmentXPO = DepartmentMap.DTOtoXPO(DepartmentDTO, _unit);
                _unit.Save(_departmentXPO);
                _unit.CommitChanges();
                DepartmentDTO.ID = _departmentXPO.Oid;
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
    public static ValidationResultDTO UpdateDepartment(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _departmentXPO = DepartmentMap.DTOtoXPO(DepartmentDTO, _unit);
                _unit.Save(_departmentXPO);
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
    public static ValidationResultDTO DeleteDepartment(DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _departmentXPO = DepartmentMap.DTOtoXPO(DepartmentDTO, _unit);
                _unit.Delete(_departmentXPO);
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
