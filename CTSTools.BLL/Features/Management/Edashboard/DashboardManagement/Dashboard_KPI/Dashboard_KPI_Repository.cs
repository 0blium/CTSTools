using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class Dashboard_KPI_Repository
{
    public static List<Dashboard_KPIDTO> GetDashboard_KPIList(Dashboard_KPIDTO Dashboard_KPIDTO, PagedResultDTO<Dashboard_KPIDTO> PagedResultDTO = null)
    {
        var _dashboard_kpiList = new List<Dashboard_KPIDTO>();
        try
        {
            // Dashboard_KPI Filters
            var _groupOperator = Dashboard_KPI_DXFilter.GetDashboard_KPI_DXFilter(Dashboard_KPIDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _dashboard_kpiCollection = new XPCollection<Dashboard_KPIXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(Dashboard_KPIXPO.Oid), SortingDirection.Ascending))
                };

                if (_dashboard_kpiCollection.AsQueryable().Count() > 0)
                {
                    _dashboard_kpiList = _dashboard_kpiCollection.Select(Dashboard_KPIXPO => Dashboard_KPIMap.XPOToDTO(Dashboard_KPIXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboard_kpiList;
    }
    public static int GetDashboard_KPICount(Dashboard_KPIDTO Dashboard_KPIDTO, PagedResultDTO<Dashboard_KPIDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Dashboard_KPI_DXFilter.GetDashboard_KPI_DXFilter(Dashboard_KPIDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<Dashboard_KPIXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDashboard_KPI(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboard_kpiXPO = Dashboard_KPIMap.DTOtoXPO(Dashboard_KPIDTO, _unit);
                _unit.Save(_dashboard_kpiXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDashboard_KPI(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboard_kpiXPO = Dashboard_KPIMap.DTOtoXPO(Dashboard_KPIDTO, _unit);
                _unit.Save(_dashboard_kpiXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteDashboard_KPI(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboard_kpiXPO = Dashboard_KPIMap.DTOtoXPO(Dashboard_KPIDTO, _unit);
                _unit.Save(_dashboard_kpiXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateMultiple(List<Dashboard_KPIDTO> Dashboard_KPIDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = Dashboard_KPIMap.DTOListToXPOList(Dashboard_KPIDTOList, _unit);
            _unit.Save(_xPOList);
            _unit.CommitChanges();
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
