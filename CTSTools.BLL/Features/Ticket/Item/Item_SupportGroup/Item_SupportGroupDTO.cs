using CTSTools.BLL.Features.Ticket.Item.Item_Header;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.Item_SupportGroup;

public class Item_SupportGroupDTO
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

    public int?[] Item_SupportGroupIDArray { get; set; }
    public Item_HeaderDTO Item_HeaderDTO { get; set; }
    public bool GetItem_HeaderDTO { get; set; }
    public int?[] Item_HeaderIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }

    #endregion
    #region Constructor
    public Item_SupportGroupDTO()
    {
        Item_SupportGroupIDArray = new int?[] { };
        Item_HeaderDTO = new Item_HeaderDTO();
        Item_HeaderIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };

    }
    #endregion
}
