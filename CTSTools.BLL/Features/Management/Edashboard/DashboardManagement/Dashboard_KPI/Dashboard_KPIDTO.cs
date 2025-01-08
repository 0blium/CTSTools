using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class Dashboard_KPIDTO
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

    public int?[] Dashboard_KPIIDArray { get; set; }
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
    public KPIDTO KPIDTO { get; set; }
    public int? KPIID { get; set; }
    public string KPIName { get; set; }
    public bool GetKPIDTO { get; set; }
    public int?[] KPIIDArray { get; set; }
    public DashboardCategoryDTO DashboardCategoryDTO { get; set; }
    public int? DashboardCategoryID { get; set; }
    public string DashboardCategoryName { get; set; }
    public bool GetDashboardCategoryDTO { get; set; }
    public int?[] DashboardCategoryIDArray { get; set; }
    public List<DashboardLineDTO> DashboardLineList { get; set; }
    public DashboardLineDTO DashboardLineDTO { get; set; }
    #endregion
    #region Constructor
    public Dashboard_KPIDTO()
    {
        Dashboard_KPIIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };
        DashboardDTO = new DashboardDTO();
        DashboardIDArray = new int?[] { };
        KPIDTO = new KPIDTO();
        KPIIDArray = new int?[] { };
        DashboardCategoryDTO = new DashboardCategoryDTO();
        DashboardCategoryIDArray = new int?[] { };

        DashboardLineDTO = new DashboardLineDTO();
        DashboardLineList = new List<DashboardLineDTO>();

    }
    #endregion
}
