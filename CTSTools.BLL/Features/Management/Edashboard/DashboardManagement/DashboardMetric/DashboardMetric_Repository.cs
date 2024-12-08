using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetric_Repository
{
    public static List<DashboardMetricDTO> GetDashboardMetricList(DashboardMetricDTO DashboardMetricDTO, PagedResultDTO<DashboardMetricDTO> PagedResultDTO = null)
    {
        var _dashboardmetricList = new List<DashboardMetricDTO>();
        try
        {
            // DashboardMetric Filters
            var _groupOperator = DashboardMetric_DXFilter.GetDashboardMetric_DXFilter(DashboardMetricDTO);
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
                var _dashboardmetricCollection = new XPCollection<DashboardMetricXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DashboardMetricXPO.Oid), SortingDirection.Ascending))
                };

                if (_dashboardmetricCollection.AsQueryable().Count() > 0)
                {
                    _dashboardmetricList = _dashboardmetricCollection.Select(DashboardMetricXPO => DashboardMetricMap.XPOToDTO(DashboardMetricXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardmetricList;
    }
    public static int GetDashboardMetricCount(DashboardMetricDTO DashboardMetricDTO, PagedResultDTO<DashboardMetricDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DashboardMetric_DXFilter.GetDashboardMetric_DXFilter(DashboardMetricDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DashboardMetricXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDashboardMetric(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardmetricXPO = DashboardMetricMap.DTOtoXPO(DashboardMetricDTO, _unit);
                _unit.Save(_dashboardmetricXPO);
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
    public static ValidationResultDTO UpdateDashboardMetric(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardmetricXPO = DashboardMetricMap.DTOtoXPO(DashboardMetricDTO, _unit);
                _unit.Save(_dashboardmetricXPO);
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
    public static ValidationResultDTO DeleteDashboardMetric(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardmetricXPO = DashboardMetricMap.DTOtoXPO(DashboardMetricDTO, _unit);
                _unit.Delete(_dashboardmetricXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
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
}
