using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;

public class DashboardMap
{
    public static DashboardDTO XPOToDTO(DashboardXPO DashboardXPO)
    {
        var _dashboardDTO = new DashboardDTO();
        try
        {
            _dashboardDTO.ID = DashboardXPO.Oid;
            _dashboardDTO.Name = DashboardXPO.Name;
            _dashboardDTO.Description = DashboardXPO.Description;
            _dashboardDTO.Revision = DashboardXPO.Revision;
            _dashboardDTO.Year = DashboardXPO.Year;
            _dashboardDTO.OwnerID = (DashboardXPO.Owner != null) ? DashboardXPO.Owner.Oid : 0;
            _dashboardDTO.OwnerName = (DashboardXPO.Owner != null) ? DashboardXPO.Owner.Name : "Unnassigned";
            _dashboardDTO.DepartmentID = (DashboardXPO.Department != null) ? DashboardXPO.Department.Oid : 0;
            _dashboardDTO.DepartmentName = (DashboardXPO.Department != null) ? DashboardXPO.Department.Name : "Unnassigned";
            _dashboardDTO.LevelID = (DashboardXPO.Level != null) ? DashboardXPO.Level.Oid : 0;
            _dashboardDTO.LevelName = (DashboardXPO.Level != null) ? DashboardXPO.Level.Name : "Unnassigned";
            _dashboardDTO.GoalRangeID = (DashboardXPO.GoalRange != null) ? DashboardXPO.GoalRange.Oid : 0;
            _dashboardDTO.GoalRangeValue = (DashboardXPO.GoalRange != null) ? DashboardXPO.GoalRange.Value : 0;
            _dashboardDTO.StatusID = (DashboardXPO.Status != null) ? DashboardXPO.Status.Oid : 0;
            _dashboardDTO.StatusName = (DashboardXPO.Status != null) ? DashboardXPO.Status.Name : "Unnassigned";
            _dashboardDTO.AddedDate = (DashboardXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DashboardXPO.AddedDate : (DateTime?)null;
            _dashboardDTO.AddedByID = (DashboardXPO.AddedBy != null) ? DashboardXPO.AddedBy.Oid : 0;
            _dashboardDTO.AddedByName = (DashboardXPO.AddedBy != null) ? DashboardXPO.AddedBy.Name : "Unnassigned";
            _dashboardDTO.LastUpdate = (DashboardXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DashboardXPO.LastUpdate : (DateTime?)null;
            _dashboardDTO.LastUpdateByID = (DashboardXPO.LastUpdateBy != null) ? DashboardXPO.LastUpdateBy.Oid : 0;
            _dashboardDTO.LastUpdateByName = (DashboardXPO.LastUpdateBy != null) ? DashboardXPO.LastUpdateBy.Name : "Unnassigned";
            _dashboardDTO.IsActive = DashboardXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardDTO;
    }

    public static DashboardXPO DTOtoXPO(DashboardDTO DashboardDTO, UnitOfWork UnitOfWork)
    {
        DashboardXPO _dashboardXPO;
        try
        {
            _dashboardXPO = DashboardDTO.ID == null || DashboardDTO.ID == 0 ? new DashboardXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DashboardXPO>(DashboardDTO.ID);
            _dashboardXPO.Name = _dashboardXPO.Name == DashboardDTO.Name ? _dashboardXPO.Name : DashboardDTO.Name;
            _dashboardXPO.Description = _dashboardXPO.Description == DashboardDTO.Description ? _dashboardXPO.Description : DashboardDTO.Description;
            _dashboardXPO.Revision = _dashboardXPO.Revision == DashboardDTO.Revision ? _dashboardXPO.Revision : DashboardDTO.Revision;
            _dashboardXPO.Year = _dashboardXPO.Year == DashboardDTO.Year ? _dashboardXPO.Year : DashboardDTO.Year;
            _dashboardXPO.GoalRange = (_dashboardXPO.GoalRange != null && _dashboardXPO.GoalRange.Oid == DashboardDTO.GoalRangeID) ? _dashboardXPO.GoalRange : UnitOfWork.GetObjectByKey<GoalRangeXPO>(DashboardDTO.GoalRangeID);

            _dashboardXPO.Owner = (_dashboardXPO.Owner != null && _dashboardXPO.Owner.Oid == DashboardDTO.OwnerID) ? _dashboardXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(DashboardDTO.OwnerID);
            _dashboardXPO.Department = (_dashboardXPO.Department != null && _dashboardXPO.Department.Oid == DashboardDTO.DepartmentID) ? _dashboardXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(DashboardDTO.DepartmentID);
            _dashboardXPO.Level = (_dashboardXPO.Level != null && _dashboardXPO.Level.Oid == DashboardDTO.LevelID) ? _dashboardXPO.Level : UnitOfWork.GetObjectByKey<LevelXPO>(DashboardDTO.LevelID);
            _dashboardXPO.Status = (_dashboardXPO.Status != null && _dashboardXPO.Status.Oid == DashboardDTO.StatusID) ? _dashboardXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(DashboardDTO.StatusID);
            _dashboardXPO.AddedDate = _dashboardXPO.AddedDate != null ? _dashboardXPO.AddedDate : DashboardDTO.AddedDate;
            _dashboardXPO.AddedBy = (_dashboardXPO.AddedBy != null) ? _dashboardXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardDTO.AddedByID);
            _dashboardXPO.LastUpdate = _dashboardXPO.LastUpdate == DashboardDTO.LastUpdate ? _dashboardXPO.LastUpdate : DashboardDTO.LastUpdate;
            _dashboardXPO.LastUpdateBy = (_dashboardXPO.LastUpdateBy != null && _dashboardXPO.LastUpdateBy.Oid == DashboardDTO.LastUpdateByID) ? _dashboardXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardDTO.LastUpdateByID);
            _dashboardXPO.IsActive = _dashboardXPO.IsActive == DashboardDTO.IsActive ? (bool)_dashboardXPO.IsActive : (bool)DashboardDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardXPO;
    }

}
