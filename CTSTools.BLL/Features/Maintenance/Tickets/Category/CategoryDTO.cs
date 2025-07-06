using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Category;

public class CategoryDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool? HasParent { get; set; }
    public int ParentID { get; set; }
    public string ParentName { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }
    public string NameWithParent { get; set; }
    #endregion

    #region Extended Properties
    public int?[] CategoryIDArray { get; set; }
    public int? SupportGroupID { get; set; }
    public string SupportGroupName { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    #endregion

    #region Constructor
    public CategoryDTO()
    {
        CategoryIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };
    }
    #endregion
}
