using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;

public class MetricMap
{
    public static MetricDTO XPOToDTO(MetricXPO MetricXPO)
    {
        var _metricDTO = new MetricDTO();
        try
        {
            _metricDTO.ID = MetricXPO.Oid;
            _metricDTO.Name = MetricXPO.Name;
            _metricDTO.Description = MetricXPO.Description;
            _metricDTO.Goal = MetricXPO.Goal;
            _metricDTO.UnitOfMeasureID = (MetricXPO.UnitOfMeasure != null) ? MetricXPO.UnitOfMeasure.Oid : 0;
            _metricDTO.UnitOfMeasureName = (MetricXPO.UnitOfMeasure != null) ? MetricXPO.UnitOfMeasure.Name : "Unnassigned";
            _metricDTO.ValueTypeID = (MetricXPO.ValueType != null) ? MetricXPO.ValueType.Oid : 0;
            _metricDTO.ValueTypeName = (MetricXPO.ValueType != null) ? MetricXPO.ValueType.Name : "Unnassigned";
            _metricDTO.OwnerID = (MetricXPO.Owner != null) ? MetricXPO.Owner.Oid : 0;
            _metricDTO.OwnerName = (MetricXPO.Owner != null) ? MetricXPO.Owner.Name : "Unnassigned";
            _metricDTO.ResponsibleID = (MetricXPO.Responsible != null) ? MetricXPO.Responsible.Oid : 0;
            _metricDTO.ResponsibleName = (MetricXPO.Responsible != null) ? MetricXPO.Responsible.Name : "Unnassigned";
            _metricDTO.OwnerDepartmentID = (MetricXPO.OwnerDepartment != null) ? MetricXPO.OwnerDepartment.Oid : 0;
            _metricDTO.OwnerDepartmentName = (MetricXPO.OwnerDepartment != null) ? MetricXPO.OwnerDepartment.Name : "Unnassigned";
            _metricDTO.ResponsibleDepartmentID = (MetricXPO.ResponsibleDepartment != null) ? MetricXPO.ResponsibleDepartment.Oid : 0;
            _metricDTO.ResponsibleDepartmentName = (MetricXPO.ResponsibleDepartment != null) ? MetricXPO.ResponsibleDepartment.Name : "Unnassigned";
            _metricDTO.DashboardCategoryID = (MetricXPO.DashboardCategory != null) ? MetricXPO.DashboardCategory.Oid : 0;
            _metricDTO.DashboardCategoryName = (MetricXPO.DashboardCategory != null) ? MetricXPO.DashboardCategory.Name : "Unnassigned";
            _metricDTO.Shared = MetricXPO.Shared;
            _metricDTO.GoalRangeID = (MetricXPO.GoalRange != null) ? MetricXPO.GoalRange.Oid : 0;
            _metricDTO.GoalRangeValue = (MetricXPO.GoalRange != null) ? MetricXPO.GoalRange.Value : 0;
            _metricDTO.FacilityID = (MetricXPO.Facility != null) ? MetricXPO.Facility.Oid : 0;
            _metricDTO.FacilityName = (MetricXPO.Facility != null) ? MetricXPO.Facility.Name : "Unnassigned";
            _metricDTO.EquivalenceID = (MetricXPO.Equivalence != null) ? MetricXPO.Equivalence.Oid : 0;
            _metricDTO.EquivalenceName = (MetricXPO.Equivalence != null) ? MetricXPO.Equivalence.Name : "Unnassigned";
            _metricDTO.StatusID = (MetricXPO.Status != null) ? MetricXPO.Status.Oid : 0;
            _metricDTO.StatusName = (MetricXPO.Status != null) ? MetricXPO.Status.Name : "Unnassigned";
            _metricDTO.IsParent = MetricXPO.IsParent;
            _metricDTO.CalculationTypeID = (MetricXPO.CalculationType != null) ? MetricXPO.CalculationType.Oid : 0;
            _metricDTO.CalculationTypeName = (MetricXPO.CalculationType != null) ? MetricXPO.CalculationType.Name : "Unnassigned";
            _metricDTO.AddedDate = (MetricXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? MetricXPO.AddedDate : (DateTime?)null;
            _metricDTO.AddedByID = (MetricXPO.AddedBy != null) ? MetricXPO.AddedBy.Oid : 0;
            _metricDTO.AddedByName = (MetricXPO.AddedBy != null) ? MetricXPO.AddedBy.Name : "Unnassigned";
            _metricDTO.LastUpdate = (MetricXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? MetricXPO.LastUpdate : (DateTime?)null;
            _metricDTO.LastUpdateByID = (MetricXPO.LastUpdateBy != null) ? MetricXPO.LastUpdateBy.Oid : 0;
            _metricDTO.LastUpdateByName = (MetricXPO.LastUpdateBy != null) ? MetricXPO.LastUpdateBy.Name : "Unnassigned";
            _metricDTO.IsActive = MetricXPO.IsActive;
            if (_metricDTO.EquivalenceDTO?.ID > 0)
            {
                if (_metricDTO.EquivalenceDTO?.ID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                {
                    _metricDTO.EquivalenceIcon = "&ge;";
                }
                else if (_metricDTO.EquivalenceDTO?.ID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                {
                    _metricDTO.EquivalenceIcon = "&le;";
                }
                else if (_metricDTO.EquivalenceDTO?.ID == (int)Equivalence_Enum.Equal)
                {
                    _metricDTO.EquivalenceIcon = "=";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _metricDTO;
    }

    public static MetricXPO DTOtoXPO(MetricDTO MetricDTO, UnitOfWork UnitOfWork)
    {
        MetricXPO _metricXPO;
        try
        {
            _metricXPO = MetricDTO.ID == null || MetricDTO.ID == 0 ? new MetricXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<MetricXPO>(MetricDTO.ID);
            _metricXPO.Name = _metricXPO.Name == MetricDTO.Name ? _metricXPO.Name : MetricDTO.Name;
            _metricXPO.Description = _metricXPO.Description == MetricDTO.Description ? _metricXPO.Description : MetricDTO.Description;
            _metricXPO.OwnerDepartment = (_metricXPO.OwnerDepartment != null && _metricXPO.OwnerDepartment.Oid == MetricDTO.OwnerDepartmentID) ? _metricXPO.OwnerDepartment : UnitOfWork.GetObjectByKey<DepartmentXPO>(MetricDTO.OwnerDepartmentID);
            _metricXPO.ResponsibleDepartment = (_metricXPO.ResponsibleDepartment != null && _metricXPO.ResponsibleDepartment.Oid == MetricDTO.ResponsibleDepartmentID) ? _metricXPO.ResponsibleDepartment : UnitOfWork.GetObjectByKey<DepartmentXPO>(MetricDTO.ResponsibleDepartmentID);
            _metricXPO.UnitOfMeasure = (_metricXPO.UnitOfMeasure != null && _metricXPO.UnitOfMeasure.Oid == MetricDTO.UnitOfMeasureID) ? _metricXPO.UnitOfMeasure : UnitOfWork.GetObjectByKey<UnitOfMeasureXPO>(MetricDTO.UnitOfMeasureID);
            _metricXPO.ValueType = (_metricXPO.ValueType != null && _metricXPO.ValueType.Oid == MetricDTO.ValueTypeDTO.ID) ? _metricXPO.ValueType : UnitOfWork.GetObjectByKey<ValueTypeXPO>(MetricDTO.ValueTypeID);
            _metricXPO.Owner = (_metricXPO.Owner != null && _metricXPO.Owner.Oid == MetricDTO.OwnerID) ? _metricXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(MetricDTO.OwnerID);
            _metricXPO.Responsible = (_metricXPO.Responsible != null && _metricXPO.Responsible.Oid == MetricDTO.ResponsibleDTO.ID) ? _metricXPO.Responsible : UnitOfWork.GetObjectByKey<UserXPO>(MetricDTO.ResponsibleID);
            _metricXPO.DashboardCategory = (_metricXPO.DashboardCategory != null && _metricXPO.DashboardCategory.Oid == MetricDTO.DashboardCategoryID) ? _metricXPO.DashboardCategory : UnitOfWork.GetObjectByKey<DashboardCategoryXPO>(MetricDTO.DashboardCategoryID);
            //_metricXPO.Shared = (bool)(_metricXPO.Shared == MetricDTO.Shared ? _metricXPO.Shared : MetricDTO.Shared);
            _metricXPO.Goal = _metricXPO.Goal == MetricDTO.Goal ? _metricXPO.Goal : MetricDTO.Goal;
            _metricXPO.GoalRange = (_metricXPO.GoalRange != null && _metricXPO.GoalRange.Oid == MetricDTO.GoalRangeID) ? _metricXPO.GoalRange : UnitOfWork.GetObjectByKey<GoalRangeXPO>(MetricDTO.GoalRangeID);
            _metricXPO.Facility = (_metricXPO.Facility != null && _metricXPO.Facility.Oid == MetricDTO.FacilityID) ? _metricXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(MetricDTO.FacilityID);
            _metricXPO.Equivalence = (_metricXPO.Equivalence != null && _metricXPO.Equivalence.Oid == MetricDTO.EquivalenceID) ? _metricXPO.Equivalence : UnitOfWork.GetObjectByKey<EquivalenceXPO>(MetricDTO.EquivalenceID);
            _metricXPO.Status = (_metricXPO.Status != null && _metricXPO.Status.Oid == MetricDTO.StatusID) ? _metricXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(MetricDTO.StatusID);
            //_metricXPO.IsParent = (bool)(_metricXPO.IsParent == MetricDTO.IsParent ? _metricXPO.IsParent : MetricDTO.IsParent);
            _metricXPO.CalculationType = (_metricXPO.CalculationType != null && _metricXPO.CalculationType.Oid == MetricDTO.CalculationTypeDTO.ID) ? _metricXPO.CalculationType : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(MetricDTO.CalculationTypeID);
            _metricXPO.FiscalYearCalculationType = (_metricXPO.FiscalYearCalculationType != null && _metricXPO.FiscalYearCalculationType.Oid == MetricDTO.FiscalYearCalculationTypeID) ? _metricXPO.FiscalYearCalculationType : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(MetricDTO.FiscalYearCalculationTypeID);
            _metricXPO.AddedDate = _metricXPO.AddedDate != null ? _metricXPO.AddedDate : MetricDTO.AddedDate;
            _metricXPO.AddedBy = (_metricXPO.AddedBy != null) ? _metricXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(MetricDTO.AddedByID);
            _metricXPO.LastUpdate = _metricXPO.LastUpdate == MetricDTO.LastUpdate ? _metricXPO.LastUpdate : MetricDTO.LastUpdate;
            _metricXPO.LastUpdateBy = (_metricXPO.LastUpdateBy != null && _metricXPO.LastUpdateBy.Oid == MetricDTO.LastUpdateByID) ? _metricXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(MetricDTO.LastUpdateByID);
            _metricXPO.IsActive = _metricXPO.IsActive == MetricDTO.IsActive ? (bool)_metricXPO.IsActive : (bool)MetricDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _metricXPO;
    }

}
