using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
public class UserDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public int? FacilityID { get; set; }
    public string FacilityName { get; set; }
    public int? DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public DateTime? AddedDate { get; set; }
    public DateTime? LastUpdate { get; set; }
    public bool? IsActive { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }

    #endregion

    #region Extended Properties
    public bool hasSystemRole { get; set; }
    public int?[] UserIDArray { get; set; }
    public bool GetRoleArray { get; set; }
    public int?[] RoleIDArray { get; set; }
    public bool GetSupportGroupArray { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    public DepartmentDTO DepartmentDTO { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public PermissionDTO PermissionDTO { get; set; }

    #endregion
    #region Constructor
    public UserDTO()
    {
        UserIDArray = [];
        FacilityDTO = new FacilityDTO();
        FacilityIDArray = [];
        RoleIDArray = [];
        DepartmentDTO = new DepartmentDTO();
        DepartmentIDArray = [];
        PermissionDTO = new PermissionDTO();
    }
    #endregion
}
