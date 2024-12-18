using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.GoalRange;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.ValueType;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;

public class MetricDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DepartmentDTO OwnerDepartmentDTO { get; set; }
    public DepartmentDTO ResponsibleDepartmentDTO { get; set; }
    public bool? Shared { get; set; }
    public float Goal { get; set; }
    public bool? IsParent { get; set; }
    public MetricDTO ParenMetricDTO { get; set; }
    public CalculationTypeDTO FiscalYearCalculationTypeDTO { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public int? DashboardCategoryID { get; set; }
    public DashboardCategoryDTO DashboardCategoryDTO { get; set; }
    public int?[] DashboardCategoryIDArray { get; set; }
    public bool GetDashboardCategoryDTO { get; set; }

    public int?[] MetricIDArray { get; set; } 
    public UnitOfMeasureDTO UnitOfMeasureDTO { get; set; }
    public bool GetUnitOfMeasureDTO { get; set; }
    public int?[] UnitOfMeasureIDArray { get; set; }
    public ValueTypeDTO ValueTypeDTO { get; set; }
    public bool GetValueTypeDTO { get; set; }
    public int?[] ValueTypeIDArray { get; set; }
    public UserDTO OwnerDTO { get; set; }
    public UserDTO ResponsibleDTO { get; set; }
    public GoalRangeDTO GoalRangeDTO { get; set; }
    public bool GetGoalRangeDTO { get; set; }
    public int?[] GoalRangeIDArray { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    public EquivalenceDTO EquivalenceDTO { get; set; }
    public bool GetEquivalenceDTO { get; set; }
    public int?[] EquivalenceIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    public CalculationTypeDTO CalculationTypeDTO { get; set; }
    public bool GetCalculationTypeDTO { get; set; }
    public int?[] CalculationTypeIDArray { get; set; }
    public List<MonthDTO> MonthValue { get; set; }
    public string EquivalenceIcon { get; set; }

    #endregion
    #region Constructor
    public MetricDTO()
    {
        MetricIDArray = new int?[] { };
        UnitOfMeasureDTO = new UnitOfMeasureDTO();
        UnitOfMeasureIDArray = new int?[] { };
        ValueTypeDTO = new ValueTypeDTO();
        ValueTypeIDArray = new int?[] { };
        OwnerDTO = new UserDTO();
        ResponsibleDTO = new UserDTO();
        OwnerDepartmentDTO = new DepartmentDTO();
        ResponsibleDepartmentDTO = new DepartmentDTO();
        GoalRangeDTO = new GoalRangeDTO();
        GoalRangeIDArray = new int?[] { };
        FacilityDTO = new FacilityDTO();
        FacilityIDArray = new int?[] { };
        EquivalenceDTO = new EquivalenceDTO();
        EquivalenceIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };
        CalculationTypeDTO = new CalculationTypeDTO();
        CalculationTypeIDArray = new int?[] { };
        MonthValue = new List<MonthDTO> { };
    }
    #endregion
}
