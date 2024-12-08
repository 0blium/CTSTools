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
            _metricDTO.UnitOfMeasureDTO.ID = (MetricXPO.UnitOfMeasure != null) ? MetricXPO.UnitOfMeasure.Oid : 0;
            _metricDTO.UnitOfMeasureDTO.Name = (MetricXPO.UnitOfMeasure != null) ? MetricXPO.UnitOfMeasure.Name : "Unnassigned";
            _metricDTO.ValueTypeDTO.ID = (MetricXPO.ValueType != null) ? MetricXPO.ValueType.Oid : 0;
            _metricDTO.ValueTypeDTO.Name = (MetricXPO.ValueType != null) ? MetricXPO.ValueType.Name : "Unnassigned";
            _metricDTO.OwnerDTO.ID = (MetricXPO.Owner != null) ? MetricXPO.Owner.Oid : 0;
            _metricDTO.OwnerDTO.Name = (MetricXPO.Owner != null) ? MetricXPO.Owner.Name : "Unnassigned";
            _metricDTO.ResponsibleDTO.ID = (MetricXPO.Responsible != null) ? MetricXPO.Responsible.Oid : 0;
            _metricDTO.ResponsibleDTO.Name = (MetricXPO.Responsible != null) ? MetricXPO.Responsible.Name : "Unnassigned";
            _metricDTO.OwnerDepartmentDTO.ID = (MetricXPO.OwnerDepartment != null) ? MetricXPO.OwnerDepartment.Oid : 0;
            _metricDTO.OwnerDepartmentDTO.Name = (MetricXPO.OwnerDepartment != null) ? MetricXPO.OwnerDepartment.Name : "Unnassigned";
            _metricDTO.ResponsibleDepartmentDTO.ID = (MetricXPO.ResponsibleDepartment != null) ? MetricXPO.ResponsibleDepartment.Oid : 0;
            _metricDTO.ResponsibleDepartmentDTO.Name = (MetricXPO.ResponsibleDepartment != null) ? MetricXPO.ResponsibleDepartment.Name : "Unnassigned";
            _metricDTO.Shared = MetricXPO.Shared;
            _metricDTO.GoalRangeDTO.ID = (MetricXPO.GoalRange != null) ? MetricXPO.GoalRange.Oid : 0;
            _metricDTO.GoalRangeDTO.Value = (MetricXPO.GoalRange != null) ? MetricXPO.GoalRange.Value : 0;
            _metricDTO.FacilityDTO.ID = (MetricXPO.Facility != null) ? MetricXPO.Facility.Oid : 0;
            _metricDTO.FacilityDTO.Name = (MetricXPO.Facility != null) ? MetricXPO.Facility.Name : "Unnassigned";
            _metricDTO.EquivalenceDTO.ID = (MetricXPO.Equivalence != null) ? MetricXPO.Equivalence.Oid : 0;
            _metricDTO.EquivalenceDTO.Name = (MetricXPO.Equivalence != null) ? MetricXPO.Equivalence.Name : "Unnassigned";
            _metricDTO.StatusDTO.ID = (MetricXPO.Status != null) ? MetricXPO.Status.Oid : 0;
            _metricDTO.StatusDTO.Name = (MetricXPO.Status != null) ? MetricXPO.Status.Name : "Unnassigned";
            _metricDTO.IsParent = MetricXPO.IsParent;
            _metricDTO.CalculationTypeDTO.ID = (MetricXPO.CalculationType != null) ? MetricXPO.CalculationType.Oid : 0;
            _metricDTO.CalculationTypeDTO.Name = (MetricXPO.CalculationType != null) ? MetricXPO.CalculationType.Name : "Unnassigned";
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
            _metricXPO.OwnerDepartment = (_metricXPO.OwnerDepartment != null && _metricXPO.OwnerDepartment.Oid == MetricDTO.OwnerDepartmentDTO.ID) ? _metricXPO.OwnerDepartment : UnitOfWork.GetObjectByKey<DepartmentXPO>(MetricDTO.OwnerDepartmentDTO?.ID);
            _metricXPO.ResponsibleDepartment = (_metricXPO.ResponsibleDepartment != null && _metricXPO.ResponsibleDepartment.Oid == MetricDTO.ResponsibleDepartmentDTO.ID) ? _metricXPO.ResponsibleDepartment : UnitOfWork.GetObjectByKey<DepartmentXPO>(MetricDTO.ResponsibleDepartmentDTO.ID);
            _metricXPO.UnitOfMeasure = (_metricXPO.UnitOfMeasure != null && _metricXPO.UnitOfMeasure.Oid == MetricDTO.UnitOfMeasureDTO.ID) ? _metricXPO.UnitOfMeasure : UnitOfWork.GetObjectByKey<UnitOfMeasureXPO>(MetricDTO.UnitOfMeasureDTO.ID);
            _metricXPO.ValueType = (_metricXPO.ValueType != null && _metricXPO.ValueType.Oid == MetricDTO.ValueTypeDTO.ID) ? _metricXPO.ValueType : UnitOfWork.GetObjectByKey<ValueTypeXPO>(MetricDTO.ValueTypeDTO.ID);
            _metricXPO.Owner = (_metricXPO.Owner != null && _metricXPO.Owner.Oid == MetricDTO.OwnerDTO.ID) ? _metricXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(MetricDTO.OwnerDTO.ID);
            _metricXPO.Responsible = (_metricXPO.Responsible != null && _metricXPO.Responsible.Oid == MetricDTO.ResponsibleDTO.ID) ? _metricXPO.Responsible : UnitOfWork.GetObjectByKey<UserXPO>(MetricDTO.ResponsibleDTO.ID);
            //_metricXPO.Shared = (bool)(_metricXPO.Shared == MetricDTO.Shared ? _metricXPO.Shared : MetricDTO.Shared);
            _metricXPO.Goal = _metricXPO.Goal == MetricDTO.Goal ? _metricXPO.Goal : MetricDTO.Goal;
            _metricXPO.GoalRange = (_metricXPO.GoalRange != null && _metricXPO.GoalRange.Oid == MetricDTO.GoalRangeDTO.ID) ? _metricXPO.GoalRange : UnitOfWork.GetObjectByKey<GoalRangeXPO>(MetricDTO.GoalRangeDTO.ID);
            _metricXPO.Facility = (_metricXPO.Facility != null && _metricXPO.Facility.Oid == MetricDTO.FacilityDTO.ID) ? _metricXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(MetricDTO.FacilityDTO.ID);
            _metricXPO.Equivalence = (_metricXPO.Equivalence != null && _metricXPO.Equivalence.Oid == MetricDTO.EquivalenceDTO.ID) ? _metricXPO.Equivalence : UnitOfWork.GetObjectByKey<EquivalenceXPO>(MetricDTO.EquivalenceDTO.ID);
            _metricXPO.Status = (_metricXPO.Status != null && _metricXPO.Status.Oid == MetricDTO.StatusDTO.ID) ? _metricXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(MetricDTO.StatusDTO.ID);
            //_metricXPO.IsParent = (bool)(_metricXPO.IsParent == MetricDTO.IsParent ? _metricXPO.IsParent : MetricDTO.IsParent);
            _metricXPO.CalculationType = (_metricXPO.CalculationType != null && _metricXPO.CalculationType.Oid == MetricDTO.CalculationTypeDTO.ID) ? _metricXPO.CalculationType : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(MetricDTO.CalculationTypeDTO.ID);
            _metricXPO.FiscalYearCalculationType = (_metricXPO.FiscalYearCalculationType != null && _metricXPO.FiscalYearCalculationType.Oid == MetricDTO.FiscalYearCalculationTypeDTO?.ID) ? _metricXPO.FiscalYearCalculationType : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(MetricDTO.FiscalYearCalculationTypeDTO?.ID);
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
