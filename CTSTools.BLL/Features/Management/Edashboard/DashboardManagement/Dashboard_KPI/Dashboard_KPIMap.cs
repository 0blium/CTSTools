using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class Dashboard_KPIMap
{
    public static Dashboard_KPIDTO XPOToDTO(Dashboard_KPIXPO Dashboard_KPIXPO)
    {
        var _dashboardmetricDTO = new Dashboard_KPIDTO();
        try
        {
            _dashboardmetricDTO.ID = Dashboard_KPIXPO.Oid;
            _dashboardmetricDTO.StatusID = (Dashboard_KPIXPO.Status != null) ? Dashboard_KPIXPO.Status.Oid : 0;
            _dashboardmetricDTO.StatusName = (Dashboard_KPIXPO.Status != null) ? Dashboard_KPIXPO.Status.Name : "Unnassigned";
            _dashboardmetricDTO.DashboardID = (Dashboard_KPIXPO.Dashboard != null) ? Dashboard_KPIXPO.Dashboard.Oid : 0;
            _dashboardmetricDTO.DashboardName = (Dashboard_KPIXPO.Dashboard != null) ? Dashboard_KPIXPO.Dashboard.Name : "Unnassigned";
            _dashboardmetricDTO.KPIID = (Dashboard_KPIXPO.KPI != null) ? Dashboard_KPIXPO.KPI.Oid : 0;
            _dashboardmetricDTO.KPIName = (Dashboard_KPIXPO.KPI != null) ? Dashboard_KPIXPO.KPI.Name : "Unnassigned";
            _dashboardmetricDTO.Order = Dashboard_KPIXPO.Order;
            _dashboardmetricDTO.DashboardCategoryID = (Dashboard_KPIXPO.KPI?.DashboardCategory != null) ? Dashboard_KPIXPO.KPI?.DashboardCategory?.Oid : 0;
            _dashboardmetricDTO.DashboardCategoryName = (Dashboard_KPIXPO.KPI?.DashboardCategory != null) ? Dashboard_KPIXPO.KPI?.DashboardCategory?.Name : "Unnassigned";
            _dashboardmetricDTO.DashboardCategoryLetter = (Dashboard_KPIXPO.KPI?.DashboardCategory != null) ? Dashboard_KPIXPO.KPI?.DashboardCategory?.PanelName : "Unnassigned";
            _dashboardmetricDTO.AddedDate = (Dashboard_KPIXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Dashboard_KPIXPO.AddedDate : (DateTime?)null;
            _dashboardmetricDTO.AddedByID = (Dashboard_KPIXPO.AddedBy != null) ? Dashboard_KPIXPO.AddedBy.Oid : 0;
            _dashboardmetricDTO.AddedByName = (Dashboard_KPIXPO.AddedBy != null) ? Dashboard_KPIXPO.AddedBy.Name : "Unnassigned";
            _dashboardmetricDTO.LastUpdate = (Dashboard_KPIXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Dashboard_KPIXPO.LastUpdate : (DateTime?)null;
            _dashboardmetricDTO.LastUpdateByID = (Dashboard_KPIXPO.LastUpdateBy != null) ? Dashboard_KPIXPO.LastUpdateBy.Oid : 0;
            _dashboardmetricDTO.LastUpdateByName = (Dashboard_KPIXPO.LastUpdateBy != null) ? Dashboard_KPIXPO.LastUpdateBy.Name : "Unnassigned";
            _dashboardmetricDTO.IsActive = Dashboard_KPIXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardmetricDTO;
    }

    public static Dashboard_KPIXPO DTOtoXPO(Dashboard_KPIDTO Dashboard_KPIDTO, UnitOfWork UnitOfWork)
    {
        Dashboard_KPIXPO _dashboardmetricXPO;
        try
        {
            _dashboardmetricXPO = Dashboard_KPIDTO.ID == null || Dashboard_KPIDTO.ID == 0 ? new Dashboard_KPIXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Dashboard_KPIXPO>(Dashboard_KPIDTO.ID);
            _dashboardmetricXPO.Status = (_dashboardmetricXPO.Status != null && _dashboardmetricXPO.Status.Oid == Dashboard_KPIDTO.StatusID) ? _dashboardmetricXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(Dashboard_KPIDTO.StatusID);
            _dashboardmetricXPO.Dashboard = (_dashboardmetricXPO.Dashboard != null && _dashboardmetricXPO.Dashboard.Oid == Dashboard_KPIDTO.DashboardID) ? _dashboardmetricXPO.Dashboard : UnitOfWork.GetObjectByKey<DashboardXPO>(Dashboard_KPIDTO.DashboardID);
            _dashboardmetricXPO.DashboardCategory = (_dashboardmetricXPO.DashboardCategory != null && _dashboardmetricXPO.DashboardCategory.Oid == Dashboard_KPIDTO.DashboardCategoryID) ? _dashboardmetricXPO.DashboardCategory : UnitOfWork.GetObjectByKey<DashboardCategoryXPO>(Dashboard_KPIDTO.DashboardCategoryID);
            _dashboardmetricXPO.KPI = (_dashboardmetricXPO.KPI != null && _dashboardmetricXPO.KPI.Oid == Dashboard_KPIDTO.KPIID) ? _dashboardmetricXPO.KPI : UnitOfWork.GetObjectByKey<KPIXPO>(Dashboard_KPIDTO.KPIID);
            _dashboardmetricXPO.Order = _dashboardmetricXPO.Order == Dashboard_KPIDTO.Order ? _dashboardmetricXPO.Order : Dashboard_KPIDTO.Order;
            _dashboardmetricXPO.AddedDate = _dashboardmetricXPO.AddedDate != null ? _dashboardmetricXPO.AddedDate : Dashboard_KPIDTO.AddedDate;
            _dashboardmetricXPO.AddedBy = (_dashboardmetricXPO.AddedBy != null) ? _dashboardmetricXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Dashboard_KPIDTO.AddedByID);
            _dashboardmetricXPO.LastUpdate = _dashboardmetricXPO.LastUpdate == Dashboard_KPIDTO.LastUpdate ? _dashboardmetricXPO.LastUpdate : Dashboard_KPIDTO.LastUpdate;
            _dashboardmetricXPO.LastUpdateBy = (_dashboardmetricXPO.LastUpdateBy != null && _dashboardmetricXPO.LastUpdateBy.Oid == Dashboard_KPIDTO.LastUpdateByID) ? _dashboardmetricXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Dashboard_KPIDTO.LastUpdateByID);
            _dashboardmetricXPO.IsActive = (bool)Dashboard_KPIDTO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardmetricXPO;
    }
    public static List<Dashboard_KPIXPO> DTOListToXPOList(List<Dashboard_KPIDTO> Dashboard_KPIDTOList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<Dashboard_KPIXPO>();
        try
        {
            foreach (var _dashboard_KPIDTO in Dashboard_KPIDTOList)
            {
                _xPOList.Add(DTOtoXPO(_dashboard_KPIDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
    public static List<Dashboard_KPIDTO> XPCollectionToList(XPCollection<Dashboard_KPIXPO> Dashboard_KPIXPCollection)
    {
        var _dTOList = new List<Dashboard_KPIDTO>();
        try
        {
            foreach (var _dashboard_KPIXPO in Dashboard_KPIXPCollection)
            {
                _dTOList.Add(XPOToDTO(_dashboard_KPIXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
}
