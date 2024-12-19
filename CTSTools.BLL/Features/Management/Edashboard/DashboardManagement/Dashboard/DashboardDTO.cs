using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Level;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;

public class DashboardDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public string Revision { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] DashboardIDArray { get; set; }
    public UserDTO OwnerDTO { get; set; }
    public int? OwnerID { get; set; }
    public string OwnerName { get; set; }
    public DepartmentDTO DepartmentDTO { get; set; }
    public int? DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public LevelDTO LevelDTO { get; set; }
    public int? LevelID { get; set; }
    public string LevelName { get; set; }
    public bool GetLevelDTO { get; set; }
    public int?[] LevelIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public int? StatusID { get; set; }
    public string StatusName { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }

    #endregion
    #region Constructor
    public DashboardDTO()
    {
        DashboardIDArray = new int?[] { };
        OwnerDTO = new UserDTO();
        DepartmentDTO = new DepartmentDTO();
        DepartmentIDArray = new int?[] { };
        LevelDTO = new LevelDTO();
        LevelIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };

    }
    #endregion
}
