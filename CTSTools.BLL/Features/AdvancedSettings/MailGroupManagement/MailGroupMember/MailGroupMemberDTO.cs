using CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using System;
namespace CTSTools.BLL.Features.MailGroups.MailGroupMember
{
    public class MailGroupMemberDTO
    {
        #region Base Properties
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? AddedByID { get; set; }
        public string AddedByName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? LastUpdateByID { get; set; }
        public string LastUpdateByName { get; set; }
        public bool? IsActive { get; set; }

        #endregion

        #region Extended Properties

        public int?[] MailGroupMemberIDArray { get; set; }
        public MailGroupDTO MailGroupDTO { get; set; }
        public bool GetMailGroupDTO { get; set; }
        public int?[] MailGroupIDArray { get; set; }
        public UserDTO UserDTO { get; set; }
        public bool GetUserDTO { get; set; }
        public int?[] UserIDArray { get; set; }

        #endregion
        #region Constructor
        public MailGroupMemberDTO()
        {
            MailGroupMemberIDArray = [];
            MailGroupDTO = new MailGroupDTO();
            MailGroupIDArray = [];
            UserDTO = new UserDTO();
            UserIDArray = [];

        }
        #endregion
    }
}
