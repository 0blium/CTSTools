using CTSTools.BLL.Features.Ticket.Item.Item_Line;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.BLL.Features.Ticket.Item.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.UserDefinedValue;

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
