using CTSTools.BLL.Features.Security.Roles.RoleType;
using System;
namespace CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;

public class RoleDTO
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
    public int? RoleTypeID { get; set; }
    public string RoleTypeName { get; set; }
    #endregion

    #region Extended Properties

    public int?[] RoleIDArray { get; set; }
    public int?[] RoleTypeIDArray { get; set; }
    public RoleTypeDTO RoleTypeDTO { get; set; }
    #endregion
    #region Constructor
    public RoleDTO()
    {
        RoleTypeDTO = new RoleTypeDTO();
        RoleTypeIDArray = [];
        RoleIDArray = [];
    }
    #endregion
}
