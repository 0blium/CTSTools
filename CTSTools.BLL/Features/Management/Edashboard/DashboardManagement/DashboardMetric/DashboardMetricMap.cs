using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetricMap
{
    public static DashboardMetricDTO XPOToDTO(DashboardMetricXPO DashboardMetricXPO)
    {
        var _dashboardmetricDTO = new DashboardMetricDTO();
        try
        {
            _dashboardmetricDTO.ID = DashboardMetricXPO.Oid;
            _dashboardmetricDTO.StatusDTO.ID = (DashboardMetricXPO.Status != null) ? DashboardMetricXPO.Status.Oid : 0;
            _dashboardmetricDTO.StatusDTO.Name = (DashboardMetricXPO.Status != null) ? DashboardMetricXPO.Status.Name : "Unnassigned";
            _dashboardmetricDTO.DashboardDTO.ID = (DashboardMetricXPO.Dashboard != null) ? DashboardMetricXPO.Dashboard.Oid : 0;
            _dashboardmetricDTO.DashboardDTO.Name = (DashboardMetricXPO.Dashboard != null) ? DashboardMetricXPO.Dashboard.Name : "Unnassigned";
            _dashboardmetricDTO.MetricDTO.ID = (DashboardMetricXPO.Metric != null) ? DashboardMetricXPO.Metric.Oid : 0;
            _dashboardmetricDTO.MetricDTO.Name = (DashboardMetricXPO.Metric != null) ? DashboardMetricXPO.Metric.Name : "Unnassigned";
            _dashboardmetricDTO.Order = DashboardMetricXPO.Order;
            _dashboardmetricDTO.DashboardCategoryDTO.ID = (DashboardMetricXPO.DashboardCategory != null) ? DashboardMetricXPO.DashboardCategory.Oid : 0;
            _dashboardmetricDTO.DashboardCategoryDTO.Name = (DashboardMetricXPO.DashboardCategory != null) ? DashboardMetricXPO.DashboardCategory.Name : "Unnassigned";
            _dashboardmetricDTO.AddedDate = (DashboardMetricXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DashboardMetricXPO.AddedDate : (DateTime?)null;
            _dashboardmetricDTO.AddedByID = (DashboardMetricXPO.AddedBy != null) ? DashboardMetricXPO.AddedBy.Oid : 0;
            _dashboardmetricDTO.AddedByName = (DashboardMetricXPO.AddedBy != null) ? DashboardMetricXPO.AddedBy.Name : "Unnassigned";
            _dashboardmetricDTO.LastUpdate = (DashboardMetricXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DashboardMetricXPO.LastUpdate : (DateTime?)null;
            _dashboardmetricDTO.LastUpdateByID = (DashboardMetricXPO.LastUpdateBy != null) ? DashboardMetricXPO.LastUpdateBy.Oid : 0;
            _dashboardmetricDTO.LastUpdateByName = (DashboardMetricXPO.LastUpdateBy != null) ? DashboardMetricXPO.LastUpdateBy.Name : "Unnassigned";
            _dashboardmetricDTO.IsActive = DashboardMetricXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardmetricDTO;
    }

    public static DashboardMetricXPO DTOtoXPO(DashboardMetricDTO DashboardMetricDTO, UnitOfWork UnitOfWork)
    {
        DashboardMetricXPO _dashboardmetricXPO;
        try
        {
            _dashboardmetricXPO = DashboardMetricDTO.ID == null || DashboardMetricDTO.ID == 0 ? new DashboardMetricXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DashboardMetricXPO>(DashboardMetricDTO.ID);
            _dashboardmetricXPO.Status = (_dashboardmetricXPO.Status != null && _dashboardmetricXPO.Status.Oid == DashboardMetricDTO.StatusID) ? _dashboardmetricXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(DashboardMetricDTO.StatusID);
            _dashboardmetricXPO.Dashboard = (_dashboardmetricXPO.Dashboard != null && _dashboardmetricXPO.Dashboard.Oid == DashboardMetricDTO.DashboardID) ? _dashboardmetricXPO.Dashboard : UnitOfWork.GetObjectByKey<DashboardXPO>(DashboardMetricDTO.DashboardID);
            _dashboardmetricXPO.Metric = (_dashboardmetricXPO.Metric != null && _dashboardmetricXPO.Metric.Oid == DashboardMetricDTO.MetricID) ? _dashboardmetricXPO.Metric : UnitOfWork.GetObjectByKey<MetricXPO>(DashboardMetricDTO.MetricID);
            _dashboardmetricXPO.Order = _dashboardmetricXPO.Order == DashboardMetricDTO.Order ? _dashboardmetricXPO.Order : DashboardMetricDTO.Order;
            _dashboardmetricXPO.AddedDate = _dashboardmetricXPO.AddedDate != null ? _dashboardmetricXPO.AddedDate : DashboardMetricDTO.AddedDate;
            _dashboardmetricXPO.AddedBy = (_dashboardmetricXPO.AddedBy != null) ? _dashboardmetricXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardMetricDTO.AddedByID);
            _dashboardmetricXPO.LastUpdate = _dashboardmetricXPO.LastUpdate == DashboardMetricDTO.LastUpdate ? _dashboardmetricXPO.LastUpdate : DashboardMetricDTO.LastUpdate;
            _dashboardmetricXPO.LastUpdateBy = (_dashboardmetricXPO.LastUpdateBy != null && _dashboardmetricXPO.LastUpdateBy.Oid == DashboardMetricDTO.LastUpdateByID) ? _dashboardmetricXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardMetricDTO.LastUpdateByID);
            _dashboardmetricXPO.IsActive = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardmetricXPO;
    }

}
