using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;

public class UserDefinedValueDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Value { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] UserDefinedValueIDArray { get; set; }
    public Item_LineDTO Item_LineDTO { get; set; }
    public bool GetItem_LineDTO { get; set; }
    public int?[] Item_LineIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public UserDefinedDTO UserDefinedDTO { get; set; }
    public bool GetUserDefinedDTO { get; set; }
    public int?[] UserDefinedIDArray { get; set; }

    #endregion
    #region Constructor
    public UserDefinedValueDTO()
    {
        UserDefinedValueIDArray = new int?[] { };
        Item_LineDTO = new Item_LineDTO();
        Item_LineIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };
        UserDefinedDTO = new UserDefinedDTO();
        UserDefinedIDArray = new int?[] { };

    }
    #endregion
}
