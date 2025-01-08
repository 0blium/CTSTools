using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLineMap
{
    public static DashboardLineDTO XPOToDTO(DashboardLineXPO DashboardLineXPO)
    {
        var _dashboardlineDTO = new DashboardLineDTO();
        try
        {
            _dashboardlineDTO.ID = DashboardLineXPO.Oid;
            //_dashboardlineDTO.DashboardKPIDTO.ID = (DashboardLineXPO.DashboardKPI != null) ? DashboardLineXPO.DashboardKPI.Oid : 0;
            _dashboardlineDTO.Dashboard_KPIID = (DashboardLineXPO.Dashboard_KPI != null) ? DashboardLineXPO.Dashboard_KPI.Oid : 0;
            _dashboardlineDTO.KPIID = (DashboardLineXPO.KPI != null) ? DashboardLineXPO.KPI.Oid : 0;
            _dashboardlineDTO.KPIName = (DashboardLineXPO.KPI != null) ? DashboardLineXPO.KPI.Name : "Unnassigned";
            _dashboardlineDTO.DashboardCategoryID = (DashboardLineXPO.DashboardCategory != null) ? DashboardLineXPO.DashboardCategory.Oid : 0;
            _dashboardlineDTO.DashboardCategoryName = (DashboardLineXPO.DashboardCategory != null) ? DashboardLineXPO.DashboardCategory.Name : "Unnassigned";
            _dashboardlineDTO.DashboardID = (DashboardLineXPO.Dashboard != null) ? DashboardLineXPO.Dashboard.Oid : 0;
            _dashboardlineDTO.DashboardName = (DashboardLineXPO.Dashboard != null) ? DashboardLineXPO.Dashboard.Name : "Unnassigned";
            _dashboardlineDTO.Month = DashboardLineXPO.Month;
            _dashboardlineDTO.Year = DashboardLineXPO.Year;
            _dashboardlineDTO.FiscalYear = DashboardLineXPO.FiscalYear;
            _dashboardlineDTO.IsTemporalValue = DashboardLineXPO.IsTemporalValue;
            _dashboardlineDTO.Value = DashboardLineXPO.Value;
            _dashboardlineDTO.Comment = DashboardLineXPO.Comment;
            _dashboardlineDTO.IgnoreKPI = DashboardLineXPO.IgnoreKPI;
            _dashboardlineDTO.Validated = DashboardLineXPO.Validated;
            _dashboardlineDTO.ValidatedByID = (DashboardLineXPO.ValidatedBy != null) ? DashboardLineXPO.ValidatedBy.Oid : 0;
            _dashboardlineDTO.ValidatedByName = (DashboardLineXPO.ValidatedBy != null) ? DashboardLineXPO.ValidatedBy.Name : "Unnassigned";
            _dashboardlineDTO.ValidatedDate = (DashboardLineXPO.ValidatedDate.ToString() != DateTime.MinValue.ToString()) ? DashboardLineXPO.ValidatedDate : (DateTime?)null;
            _dashboardlineDTO.AddedDate = (DashboardLineXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DashboardLineXPO.AddedDate : (DateTime?)null;
            _dashboardlineDTO.AddedByID = (DashboardLineXPO.AddedBy != null) ? DashboardLineXPO.AddedBy.Oid : 0;
            _dashboardlineDTO.AddedByName = (DashboardLineXPO.AddedBy != null) ? DashboardLineXPO.AddedBy.Name : "Unnassigned";
            _dashboardlineDTO.LastUpdate = (DashboardLineXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DashboardLineXPO.LastUpdate : (DateTime?)null;
            _dashboardlineDTO.LastUpdateByID = (DashboardLineXPO.LastUpdateBy != null) ? DashboardLineXPO.LastUpdateBy.Oid : 0;
            _dashboardlineDTO.LastUpdateByName = (DashboardLineXPO.LastUpdateBy != null) ? DashboardLineXPO.LastUpdateBy.Name : "Unnassigned";
            _dashboardlineDTO.IsActive = DashboardLineXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardlineDTO;
    }

    public static DashboardLineXPO DTOtoXPO(DashboardLineDTO DashboardLineDTO, UnitOfWork UnitOfWork)
    {
        DashboardLineXPO _dashboardlineXPO;
        try
        {
            _dashboardlineXPO = DashboardLineDTO.ID == null || DashboardLineDTO.ID == 0 ? new DashboardLineXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DashboardLineXPO>(DashboardLineDTO.ID);
            _dashboardlineXPO.Dashboard_KPI = (_dashboardlineXPO.Dashboard_KPI != null && _dashboardlineXPO.Dashboard_KPI.Oid == DashboardLineDTO.Dashboard_KPIID) ? _dashboardlineXPO.Dashboard_KPI : UnitOfWork.GetObjectByKey<Dashboard_KPIXPO>(DashboardLineDTO.Dashboard_KPIID);
            _dashboardlineXPO.KPI = (_dashboardlineXPO.KPI != null && _dashboardlineXPO.KPI.Oid == DashboardLineDTO.KPIID) ? _dashboardlineXPO.KPI : UnitOfWork.GetObjectByKey<KPIXPO>(DashboardLineDTO.KPIID);
            _dashboardlineXPO.DashboardCategory = (_dashboardlineXPO.DashboardCategory != null && _dashboardlineXPO.DashboardCategory.Oid == DashboardLineDTO.DashboardCategoryDTO.ID) ? _dashboardlineXPO.DashboardCategory : UnitOfWork.GetObjectByKey<DashboardCategoryXPO>(DashboardLineDTO.DashboardCategoryID);
            _dashboardlineXPO.Dashboard = (_dashboardlineXPO.Dashboard != null && _dashboardlineXPO.Dashboard.Oid == DashboardLineDTO.DashboardID) ? _dashboardlineXPO.Dashboard : UnitOfWork.GetObjectByKey<DashboardXPO>(DashboardLineDTO.DashboardID);
            _dashboardlineXPO.Goal = _dashboardlineXPO.Goal == DashboardLineDTO.Goal ? _dashboardlineXPO.Goal : DashboardLineDTO.Goal;
            _dashboardlineXPO.Month = _dashboardlineXPO.Month == DashboardLineDTO.Month ? _dashboardlineXPO.Month : DashboardLineDTO.Month;
            _dashboardlineXPO.Year = _dashboardlineXPO.Year == DashboardLineDTO.Year ? _dashboardlineXPO.Year : DashboardLineDTO.Year;
            _dashboardlineXPO.FiscalYear = _dashboardlineXPO.FiscalYear == DashboardLineDTO.FiscalYear ? _dashboardlineXPO.FiscalYear : DashboardLineDTO.FiscalYear;
            _dashboardlineXPO.Decimal = _dashboardlineXPO.Decimal == DashboardLineDTO.Decimal ? _dashboardlineXPO.Decimal : DashboardLineDTO.Decimal;
            _dashboardlineXPO.Value = (float)(_dashboardlineXPO.Value == DashboardLineDTO.Value ? _dashboardlineXPO.Value : DashboardLineDTO.Value);
            _dashboardlineXPO.IsTemporalValue = (bool)(_dashboardlineXPO.IsTemporalValue == (DashboardLineDTO.IsTemporalValue ?? false) ? _dashboardlineXPO.IsTemporalValue : DashboardLineDTO.IsTemporalValue ?? false);
            _dashboardlineXPO.Comment = _dashboardlineXPO.Comment == DashboardLineDTO.Comment ? _dashboardlineXPO.Comment : DashboardLineDTO.Comment;
            _dashboardlineXPO.IgnoreKPI = (bool)(_dashboardlineXPO.IgnoreKPI == (DashboardLineDTO.IgnoreKPI ?? false) ? _dashboardlineXPO.IgnoreKPI : DashboardLineDTO.IgnoreKPI ?? false);
            _dashboardlineXPO.Validated = (bool)(_dashboardlineXPO.Validated == (DashboardLineDTO.Validated ?? false) ? _dashboardlineXPO.Validated : DashboardLineDTO.Validated ?? false);
            _dashboardlineXPO.ValidatedBy = (_dashboardlineXPO.ValidatedBy != null && _dashboardlineXPO.ValidatedBy?.Oid == DashboardLineDTO.ValidatedByID) ? _dashboardlineXPO.ValidatedBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardLineDTO.ValidatedByID);
            _dashboardlineXPO.ValidatedDate = _dashboardlineXPO.ValidatedDate == DashboardLineDTO.ValidatedDate ? _dashboardlineXPO.ValidatedDate : DashboardLineDTO.ValidatedDate;
            _dashboardlineXPO.AddedDate = _dashboardlineXPO.AddedDate != null ? _dashboardlineXPO.AddedDate : DashboardLineDTO.AddedDate;
            _dashboardlineXPO.AddedBy = (_dashboardlineXPO.AddedBy != null) ? _dashboardlineXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardLineDTO.AddedByID);
            _dashboardlineXPO.LastUpdate = _dashboardlineXPO.LastUpdate == DashboardLineDTO.LastUpdate ? _dashboardlineXPO.LastUpdate : DashboardLineDTO.LastUpdate;
            _dashboardlineXPO.LastUpdateBy = (_dashboardlineXPO.LastUpdateBy != null && _dashboardlineXPO.LastUpdateBy.Oid == DashboardLineDTO.LastUpdateByID) ? _dashboardlineXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardLineDTO.LastUpdateByID);
            //_dashboardlineXPO.IsActive = _dashboardlineXPO.IsActive == DashboardLineDTO.IsActive ? (bool)_dashboardlineXPO.IsActive : (bool)DashboardLineDTO.IsActive;
            _dashboardlineXPO.IsActive = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardlineXPO;
    }

}
