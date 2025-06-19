using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using System;

namespace AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember
{
    public class SupportGroupMemberDTO
    {
        #region Base Properties
        public int? ID { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? AddedByID { get; set; }
        public string AddedByName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? LastUpdateByID { get; set; }
        public string LastUpdateByName { get; set; }
        public bool? IsActive { get; set; }

        #endregion

        #region Extended Properties

        public int?[] SupportGroupMemberIDArray { get; set; }
        public SupportGroupDTO SupportGroupDTO { get; set; }
        public bool GetSupportGroupDTO { get; set; }
        public int?[] SupportGroupIDArray { get; set; }
        public string[] SupportGroupNameArray { get; set; }
        public UserDTO UserDTO { get; set; }
        public bool GetUserDTO { get; set; }
        public int?[] UserIDArray { get; set; }
        public RoleDTO RoleDTO { get; set; }
        public bool GetRoleDTO { get; set; }
        public int?[] RoleIDArray { get; set; }

        #endregion
        #region Constructor
        public SupportGroupMemberDTO()
        {
            SupportGroupMemberIDArray = new int?[] { };
            SupportGroupNameArray = new string[] { };
            SupportGroupDTO = new SupportGroupDTO();
            SupportGroupIDArray = new int?[] { };
            UserDTO = new UserDTO();
            UserIDArray = new int?[] { };
            RoleDTO = new RoleDTO();
            RoleIDArray = new int?[] { };

        }
        #endregion
    }
}
