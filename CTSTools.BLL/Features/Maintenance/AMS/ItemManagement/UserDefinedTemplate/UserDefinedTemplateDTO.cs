using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplateDTO
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

    public int?[] UserDefinedTemplateIDArray { get; set; }
    public Item_HeaderDTO Item_HeaderDTO { get; set; }
    public bool GetItem_HeaderDTO { get; set; }
    public int?[] Item_HeaderIDArray { get; set; }
    public Item_SupportGroupDTO Item_SupportGroupDTO { get; set; }
    public bool GetItem_SupportGroupDTO { get; set; }
    public int?[] Item_SupportGroupIDArray { get; set; }
    public UserDefinedDTO UserDefinedDTO { get; set; }
    public bool GetUserDefinedDTO { get; set; }
    public int?[] UserDefinedIDArray { get; set; }

    #endregion
    #region Constructor
    public UserDefinedTemplateDTO()
    {
        UserDefinedTemplateIDArray = new int?[] { };
        Item_HeaderDTO = new Item_HeaderDTO();
        Item_SupportGroupDTO = new Item_SupportGroupDTO();
        Item_SupportGroupIDArray = new int?[] { };
        Item_HeaderIDArray = new int?[] { };
        UserDefinedDTO = new UserDefinedDTO();
        UserDefinedIDArray = new int?[] { };

    }
    #endregion
}
