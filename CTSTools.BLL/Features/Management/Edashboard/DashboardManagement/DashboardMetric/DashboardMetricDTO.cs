using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetricDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int Order { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] DashboardMetricIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public int? StatusID { get; set; }
    public string StatusName { get; set; }
    public bool GetStatusDTO { get; set; }
    public bool GetDashboardLineList { get; set; }
    public int?[] StatusIDArray { get; set; }
    public DashboardDTO DashboardDTO { get; set; }
    public int? DashboardID { get; set; }
    public string DashboardName { get; set; }
    public bool GetDashboardDTO { get; set; }
    public int?[] DashboardIDArray { get; set; }
    public MetricDTO MetricDTO { get; set; }
    public int? MetricID { get; set; }
    public string MetricName { get; set; }
    public bool GetMetricDTO { get; set; }
    public int?[] MetricIDArray { get; set; }
    public DashboardCategoryDTO DashboardCategoryDTO { get; set; }
    public int? DashboardCategoryID { get; set; }
    public string DashboardCategoryName { get; set; }
    public bool GetDashboardCategoryDTO { get; set; }
    public int?[] DashboardCategoryIDArray { get; set; }
    public List<DashboardLineDTO> DashboardLineList { get; set; }
    public DashboardLineDTO DashboardLineDTO { get; set; }
    #endregion
    #region Constructor
    public DashboardMetricDTO()
    {
        DashboardMetricIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };
        DashboardDTO = new DashboardDTO();
        DashboardIDArray = new int?[] { };
        MetricDTO = new MetricDTO();
        MetricIDArray = new int?[] { };
        DashboardCategoryDTO = new DashboardCategoryDTO();
        DashboardCategoryIDArray = new int?[] { };

        DashboardLineDTO = new DashboardLineDTO();
        DashboardLineList = new List<DashboardLineDTO>();

    }
    #endregion
}
