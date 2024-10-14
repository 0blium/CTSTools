using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_PermissionDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? PermissionID { get; set; }
    public string PermissionName { get; set; }
    public int? UserID { get; set; }
    public string UserName { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] User_PermissionIDArray { get; set; }
    public PermissionDTO PermissionDTO { get; set; }
    public bool GetPermissionDTO { get; set; }
    public int?[] PermissionIDArray { get; set; }
    public UserDTO UserDTO { get; set; }
    public bool GetUserDTO { get; set; }
    public int?[] UserIDArray { get; set; }

    #endregion
    #region Constructor
    public User_PermissionDTO()
    {
        User_PermissionIDArray = [];
        PermissionDTO = new PermissionDTO();
        PermissionIDArray = [];
        UserDTO = new UserDTO();
        UserIDArray = [];

    }
    #endregion
}
