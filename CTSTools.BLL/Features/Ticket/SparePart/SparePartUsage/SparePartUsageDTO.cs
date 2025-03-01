using CTSTools.BLL.Features.Ticket.Item.Item_Line;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.BLL.Features.Ticket.SparePart.SparePart_Lot;
using CTSTools.BLL.Features.Ticket.SparePart.SparePartInventory;
using CTSTools.BLL.Features.Ticket.Tickets.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePartUsage;

public class SparePartUsageDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int Quantity { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] SparePartUsageIDArray { get; set; }
    public TicketDTO TicketDTO { get; set; }
    public bool GetTicketDTO { get; set; }
    public int?[] TicketIDArray { get; set; }
    public SparePartInventoryDTO SparePartInventoryDTO { get; set; }
    public bool GetSparePartInventoryDTO { get; set; }
    public int?[] SparePartInventoryIDArray { get; set; }
    public Item_LineDTO Item_LineDTO { get; set; }
    public bool GetItem_LineDTO { get; set; }
    public int?[] Item_LineIDArray { get; set; }
    public SparePart_LotDTO SparePart_LotDTO { get; set; }
    public bool GetSparePart_LotDTO { get; set; }
    public int?[] SparePart_LotIDArray { get; set; }
    #region Filters
    public DateTime StartAddedDate { get; set; }
    public DateTime EndAddedDate { get; set; }
    public int?[] SparePartIDArray { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    #endregion
    #endregion
    #region Constructor
    public SparePartUsageDTO()
    {
        SparePartUsageIDArray = new int?[] { };
        TicketDTO = new TicketDTO();
        TicketIDArray = new int?[] { };
        SparePartInventoryDTO = new SparePartInventoryDTO();
        SparePartInventoryIDArray = new int?[] { };
        Item_LineDTO = new Item_LineDTO();
        Item_LineIDArray = new int?[] { };
        SparePart_LotDTO = new SparePart_LotDTO();
        SparePart_LotIDArray = new int?[] { };
        SparePartIDArray = new int?[] { };
        SupportGroupIDArray = new int?[] { };

    }
    #endregion
}
