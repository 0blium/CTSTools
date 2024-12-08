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
            //_dashboardlineDTO.DashboardMetricDTO.ID = (DashboardLineXPO.DashboardMetric != null) ? DashboardLineXPO.DashboardMetric.Oid : 0;
            _dashboardlineDTO.DashboardMetricID = (DashboardLineXPO.DashboardMetric != null) ? DashboardLineXPO.DashboardMetric.Oid : 0;
            _dashboardlineDTO.MetricDTO.ID = (DashboardLineXPO.Metric != null) ? DashboardLineXPO.Metric.Oid : 0;
            _dashboardlineDTO.MetricDTO.Name = (DashboardLineXPO.Metric != null) ? DashboardLineXPO.Metric.Name : "Unnassigned";
            _dashboardlineDTO.DashboardCategoryDTO.ID = (DashboardLineXPO.DashboardCategory != null) ? DashboardLineXPO.DashboardCategory.Oid : 0;
            _dashboardlineDTO.DashboardCategoryDTO.Name = (DashboardLineXPO.DashboardCategory != null) ? DashboardLineXPO.DashboardCategory.Name : "Unnassigned";
            _dashboardlineDTO.DashboardDTO.ID = (DashboardLineXPO.Dashboard != null) ? DashboardLineXPO.Dashboard.Oid : 0;
            _dashboardlineDTO.DashboardDTO.Name = (DashboardLineXPO.Dashboard != null) ? DashboardLineXPO.Dashboard.Name : "Unnassigned";
            _dashboardlineDTO.Month = DashboardLineXPO.Month;
            _dashboardlineDTO.Year = DashboardLineXPO.Year;
            _dashboardlineDTO.FiscalYear = DashboardLineXPO.FiscalYear;
            _dashboardlineDTO.IsTemporalValue = DashboardLineXPO.IsTemporalValue;
            _dashboardlineDTO.Value = DashboardLineXPO.Value;
            _dashboardlineDTO.Comment = DashboardLineXPO.Comment;
            _dashboardlineDTO.IgnoreMetric = DashboardLineXPO.IgnoreMetric;
            _dashboardlineDTO.Validated = DashboardLineXPO.Validated;
            _dashboardlineDTO.ValidatedByDTO.ID = (DashboardLineXPO.ValidatedBy != null) ? DashboardLineXPO.ValidatedBy.Oid : 0;
            _dashboardlineDTO.ValidatedByDTO.Name = (DashboardLineXPO.ValidatedBy != null) ? DashboardLineXPO.ValidatedBy.Name : "Unnassigned";
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
            _dashboardlineXPO.DashboardMetric = (_dashboardlineXPO.DashboardMetric != null && _dashboardlineXPO.DashboardMetric.Oid == DashboardLineDTO.DashboardMetricDTO?.ID) ? _dashboardlineXPO.DashboardMetric : UnitOfWork.GetObjectByKey<DashboardMetricXPO>(DashboardLineDTO.DashboardMetricDTO?.ID);
            _dashboardlineXPO.Metric = (_dashboardlineXPO.Metric != null && _dashboardlineXPO.Metric.Oid == DashboardLineDTO.MetricDTO.ID) ? _dashboardlineXPO.Metric : UnitOfWork.GetObjectByKey<MetricXPO>(DashboardLineDTO.MetricDTO.ID);
            _dashboardlineXPO.DashboardCategory = (_dashboardlineXPO.DashboardCategory != null && _dashboardlineXPO.DashboardCategory.Oid == DashboardLineDTO.DashboardCategoryDTO.ID) ? _dashboardlineXPO.DashboardCategory : UnitOfWork.GetObjectByKey<DashboardCategoryXPO>(DashboardLineDTO.DashboardCategoryDTO.ID);
            _dashboardlineXPO.Dashboard = (_dashboardlineXPO.Dashboard != null && _dashboardlineXPO.Dashboard.Oid == DashboardLineDTO.DashboardDTO.ID) ? _dashboardlineXPO.Dashboard : UnitOfWork.GetObjectByKey<DashboardXPO>(DashboardLineDTO.DashboardDTO.ID);
            _dashboardlineXPO.Goal = _dashboardlineXPO.Goal == DashboardLineDTO.Goal ? _dashboardlineXPO.Goal : DashboardLineDTO.Goal;
            _dashboardlineXPO.Month = _dashboardlineXPO.Month == DashboardLineDTO.Month ? _dashboardlineXPO.Month : DashboardLineDTO.Month;
            _dashboardlineXPO.Year = _dashboardlineXPO.Year == DashboardLineDTO.Year ? _dashboardlineXPO.Year : DashboardLineDTO.Year;
            _dashboardlineXPO.FiscalYear = _dashboardlineXPO.FiscalYear == DashboardLineDTO.FiscalYear ? _dashboardlineXPO.FiscalYear : DashboardLineDTO.FiscalYear;
            _dashboardlineXPO.Decimal = _dashboardlineXPO.Decimal == DashboardLineDTO.Decimal ? _dashboardlineXPO.Decimal : DashboardLineDTO.Decimal;
            _dashboardlineXPO.Value = (float)(_dashboardlineXPO.Value == DashboardLineDTO.Value ? _dashboardlineXPO.Value : DashboardLineDTO.Value);
            _dashboardlineXPO.IsTemporalValue = (bool)(_dashboardlineXPO.IsTemporalValue == (DashboardLineDTO.IsTemporalValue ?? false) ? _dashboardlineXPO.IsTemporalValue : DashboardLineDTO.IsTemporalValue ?? false);
            _dashboardlineXPO.Comment = _dashboardlineXPO.Comment == DashboardLineDTO.Comment ? _dashboardlineXPO.Comment : DashboardLineDTO.Comment;
            _dashboardlineXPO.IgnoreMetric = (bool)(_dashboardlineXPO.IgnoreMetric == (DashboardLineDTO.IgnoreMetric ?? false) ? _dashboardlineXPO.IgnoreMetric : DashboardLineDTO.IgnoreMetric ?? false);
            _dashboardlineXPO.Validated = (bool)(_dashboardlineXPO.Validated == (DashboardLineDTO.Validated ?? false) ? _dashboardlineXPO.Validated : DashboardLineDTO.Validated ?? false);
            _dashboardlineXPO.ValidatedBy = (_dashboardlineXPO.ValidatedBy != null && _dashboardlineXPO.ValidatedBy?.Oid == DashboardLineDTO.ValidatedByDTO?.ID) ? _dashboardlineXPO.ValidatedBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardLineDTO.ValidatedByDTO?.ID);
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
