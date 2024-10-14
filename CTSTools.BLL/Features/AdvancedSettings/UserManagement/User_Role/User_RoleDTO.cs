using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;

public class User_RoleDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? RoleID { get; set; }
    public string RoleName { get; set; }
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

    public int?[] User_RoleIDArray { get; set; }
    public RoleDTO RoleDTO { get; set; }
    public bool GetRoleDTO { get; set; }
    public int?[] RoleIDArray { get; set; }
    public UserDTO UserDTO { get; set; }
    public bool GetUserDTO { get; set; }
    public int?[] UserIDArray { get; set; }

    #endregion
    #region Constructor
    public User_RoleDTO()
    {
        User_RoleIDArray = [];
        RoleDTO = new RoleDTO();
        RoleIDArray = [];
        UserDTO = new UserDTO();
        UserIDArray = [];

    }
    #endregion
}
