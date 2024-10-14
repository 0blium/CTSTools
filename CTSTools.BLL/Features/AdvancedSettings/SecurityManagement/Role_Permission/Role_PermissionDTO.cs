using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using System;


namespace CTSTools.BLL.Features.Security.Permissions.Role_Permission
{
    public class Role_PermissionDTO
    {
        #region Base Properties
        public int? ID { get; set; }
        public int? PermissionID { get; set; }
        public string PermissionName { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; } 
        public DateTime? AddedDate { get; set; }
        public int? AddedByID { get; set; }
        public string AddedByName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? LastUpdateByID { get; set; }
        public string LastUpdateByName { get; set; }
        public bool? IsActive { get; set; }

        #endregion

        #region Extended Properties

        public int?[] Role_PermissionIDArray { get; set; }
        public PermissionDTO PermissionDTO { get; set; }
        public bool GetPermissionDTO { get; set; }
        public int?[] PermissionIDArray { get; set; }
        public RoleDTO RoleDTO { get; set; }
        public bool GetRoleDTO { get; set; }
        public int?[] RoleIDArray { get; set; }

        #endregion
        #region Constructor
        public Role_PermissionDTO()
        {
            Role_PermissionIDArray = new int?[] { };
            PermissionDTO = new PermissionDTO();
            PermissionIDArray = new int?[] { };
            RoleDTO = new RoleDTO();
            RoleIDArray = new int?[] { };

        }
        #endregion
    }
}
