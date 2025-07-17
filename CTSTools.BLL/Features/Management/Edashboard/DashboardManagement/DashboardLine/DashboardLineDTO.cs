using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.BLL.Features.Management.Edashboard.KPI;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLineDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? Dashboard_KPIID { get; set; }
    public float Goal { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int FiscalYear { get; set; }
    public float Decimal { get; set; }
    public float Value { get; set; }
    public bool? IsTemporalValue { get; set; }
    public string Comment { get; set; }
    public bool? IgnoreKPI { get; set; }
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
    public Dashboard_KPIDTO Dashboard_KPIDTO { get; set; }
    public string Dashboard_KPIName { get; set; }
    public bool GetDashboard_KPIDTO { get; set; }
    public int?[] Dashboard_KPIIDArray { get; set; }
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
    public DashboardDTO DashboardDTO { get; set; }
    public int? DashboardID { get; set; }
    public string DashboardName { get; set; }
    public bool GetDashboardDTO { get; set; }
    public int?[] DashboardIDArray { get; set; }
    public UserDTO ValidatedByDTO { get; set; }
    public int? ValidatedByID { get; set; }
    public string ValidatedByName { get; set; }
    public MonthDTO MonthValue { get; set; }

    #endregion
    #region Constructor
    public DashboardLineDTO()
    {
        DashboardLineIDArray = new int?[] { };
        //DashboardKPIDTO = new DashboardKPIDTO();
        Dashboard_KPIIDArray = new int?[] { };
        KPIDTO = new KPIDTO();
        KPIIDArray = new int?[] { };
        DashboardCategoryDTO = new DashboardCategoryDTO();
        DashboardCategoryIDArray = new int?[] { };
        DashboardDTO = new DashboardDTO();
        DashboardIDArray = new int?[] { };
        ValidatedByDTO = new UserDTO();
        MonthValue = new MonthDTO();
    }
    #endregion
}
