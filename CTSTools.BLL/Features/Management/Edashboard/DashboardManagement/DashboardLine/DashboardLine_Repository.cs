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

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLine_Repository
{
    public static List<DashboardLineDTO> GetDashboardLineList(DashboardLineDTO DashboardLineDTO, PagedResultDTO<DashboardLineDTO> PagedResultDTO = null)
    {
        var _dashboardlineList = new List<DashboardLineDTO>();
        try
        {
            // DashboardLine Filters
            var _groupOperator = DashboardLine_DXFilter.GetDashboardLine_DXFilter(DashboardLineDTO);
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
                var _dashboardlineCollection = new XPCollection<DashboardLineXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DashboardLineXPO.Oid), SortingDirection.Ascending))
                };

                if (_dashboardlineCollection.AsQueryable().Count() > 0)
                {
                    _dashboardlineList = _dashboardlineCollection.Select(DashboardLineXPO => DashboardLineMap.XPOToDTO(DashboardLineXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardlineList;
    }
    public static int GetDashboardLineCount(DashboardLineDTO DashboardLineDTO, PagedResultDTO<DashboardLineDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DashboardLine_DXFilter.GetDashboardLine_DXFilter(DashboardLineDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DashboardLineXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDashboardLine(DashboardLineDTO DashboardLineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardlineXPO = DashboardLineMap.DTOtoXPO(DashboardLineDTO, _unit);
                _unit.Save(_dashboardlineXPO);
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
    public static ValidationResultDTO UpdateDashboardLine(DashboardLineDTO DashboardLineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardlineXPO = DashboardLineMap.DTOtoXPO(DashboardLineDTO, _unit);
                _unit.Save(_dashboardlineXPO);
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
    public static ValidationResultDTO DeleteDashboardLine(DashboardLineDTO DashboardLineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardlineXPO = DashboardLineMap.DTOtoXPO(DashboardLineDTO, _unit);
                _unit.Delete(_dashboardlineXPO);
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
