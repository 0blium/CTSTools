using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.BLL.Features.Ticket.SparePart.SparePart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePartInventory;

public class SparePartInventoryDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int MaxQty { get; set; }
    public int MinQty { get; set; }
    public int AvailableQty { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] SparePartInventoryIDArray { get; set; }
    public SparePartDTO SparePartDTO { get; set; }
    public bool GetSparePartDTO { get; set; }
    public int?[] SparePartIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }

    #endregion
    #region Constructor
    public SparePartInventoryDTO()
    {
        SparePartInventoryIDArray = new int?[] { };
        SparePartDTO = new SparePartDTO();
        SparePartIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };

    }
    #endregion
}
