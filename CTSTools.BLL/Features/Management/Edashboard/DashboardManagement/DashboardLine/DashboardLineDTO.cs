using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLineDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? DashboardMetricID { get; set; }
    public float Goal { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int FiscalYear { get; set; }
    public float Decimal { get; set; }
    public float Value { get; set; }
    public bool? IsTemporalValue { get; set; }
    public string Comment { get; set; }
    public bool? IgnoreMetric { get; set; }
    public bool? Validated { get; set; }
    public DateTime? ValidatedDate { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] DashboardLineIDArray { get; set; }
    public DashboardMetricDTO DashboardMetricDTO { get; set; }
    public bool GetDashboardMetricDTO { get; set; }
    public int?[] DashboardMetricIDArray { get; set; }
    public MetricDTO MetricDTO { get; set; }
    public bool GetMetricDTO { get; set; }
    public int?[] MetricIDArray { get; set; }
    public DashboardCategoryDTO DashboardCategoryDTO { get; set; }
    public bool GetDashboardCategoryDTO { get; set; }
    public int?[] DashboardCategoryIDArray { get; set; }
    public DashboardDTO DashboardDTO { get; set; }
    public bool GetDashboardDTO { get; set; }
    public int?[] DashboardIDArray { get; set; }
    public UserDTO ValidatedByDTO { get; set; }
    public MonthDTO MonthValue { get; set; }

    #endregion
    #region Constructor
    public DashboardLineDTO()
    {
        DashboardLineIDArray = new int?[] { };
        //DashboardMetricDTO = new DashboardMetricDTO();
        DashboardMetricIDArray = new int?[] { };
        MetricDTO = new MetricDTO();
        MetricIDArray = new int?[] { };
        DashboardCategoryDTO = new DashboardCategoryDTO();
        DashboardCategoryIDArray = new int?[] { };
        DashboardDTO = new DashboardDTO();
        DashboardIDArray = new int?[] { };
        ValidatedByDTO = new UserDTO();
        MonthValue = new MonthDTO();
    }
    #endregion
}
