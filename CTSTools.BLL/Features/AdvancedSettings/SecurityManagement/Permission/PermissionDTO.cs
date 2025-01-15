using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;
using System;

namespace CTSTools.BLL.Features.Security.Permissions.Permission
{
    public class PermissionDTO
    {
        #region Base Properties
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public int? ActionID { get; set; }
        public string ActionName { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? AddedByID { get; set; }
        public string AddedByName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? LastUpdateByID { get; set; }
        public string LastUpdateByName { get; set; }
        public bool? IsActive { get; set; }

        #endregion

        #region Extended Properties

        public int?[] PermissionIDArray { get; set; }
        public ActionDTO ActionDTO { get; set; }
        public bool GetActionDTO { get; set; }
        public int?[] ActionIDArray { get; set; }
        public ModuleDTO ModuleDTO { get; set; }
        public bool GetModuleDTO { get; set; }
        public int?[] ModuleIDArray { get; set; }

        #endregion
        #region Constructor
        public PermissionDTO()
        {
            PermissionIDArray = new int?[] { };
            ActionDTO = new ActionDTO();
            ActionIDArray = new int?[] { };
            ModuleDTO = new ModuleDTO();
            ModuleIDArray = new int?[] { };

        }
        #endregion
    }
}
