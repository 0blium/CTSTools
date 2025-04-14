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

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;

public class DashboardCategory_Repository
{
    public static List<DashboardCategoryDTO> GetDashboardCategoryList(DashboardCategoryDTO DashboardCategoryDTO, PagedResultDTO<DashboardCategoryDTO> PagedResultDTO = null)
    {
        var _dashboardcategoryList = new List<DashboardCategoryDTO>();
        try
        {
            // DashboardCategory Filters
            var _groupOperator = DashboardCategory_DXFilter.GetDashboardCategory_DXFilter(DashboardCategoryDTO);
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
                var _dashboardcategoryCollection = new XPCollection<DashboardCategoryXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(DashboardCategoryXPO.Oid), SortingDirection.Ascending))
                };

                if (_dashboardcategoryCollection.AsQueryable().Count() > 0)
                {
                    _dashboardcategoryList = _dashboardcategoryCollection.Select(DashboardCategoryXPO => DashboardCategoryMap.XPOToDTO(DashboardCategoryXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardcategoryList;
    }
    public static int GetDashboardCategoryCount(DashboardCategoryDTO DashboardCategoryDTO, PagedResultDTO<DashboardCategoryDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = DashboardCategory_DXFilter.GetDashboardCategory_DXFilter(DashboardCategoryDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<DashboardCategoryXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateDashboardCategory(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardcategoryXPO = DashboardCategoryMap.DTOtoXPO(DashboardCategoryDTO, _unit);
                _unit.Save(_dashboardcategoryXPO);
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
    public static ValidationResultDTO UpdateDashboardCategory(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardcategoryXPO = DashboardCategoryMap.DTOtoXPO(DashboardCategoryDTO, _unit);
                _unit.Save(_dashboardcategoryXPO);
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
    public static ValidationResultDTO DeleteDashboardCategory(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _dashboardcategoryXPO = DashboardCategoryMap.DTOtoXPO(DashboardCategoryDTO, _unit);
                _unit.Delete(_dashboardcategoryXPO);
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
    public static ValidationResultDTO CreateMultiple(List<DashboardCategoryDTO> DashboardCategoryDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = DashboardCategoryMap.DTOListToXPOList(DashboardCategoryDTOList, _unit);
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
