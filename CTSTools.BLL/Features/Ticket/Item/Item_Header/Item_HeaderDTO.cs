using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Ticket.Item.ItemClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.Item_Header;

public class Item_HeaderDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string EnglishName { get; set; }
    public string SpanishName { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public bool? IsESD { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }


    #endregion

    #region Extended Properties

    public int?[] Item_HeaderIDArray { get; set; }
    public ItemClassificationDTO ItemClassificationDTO { get; set; }
    public bool GetItemClassificationDTO { get; set; }
    public int?[] ItemClassificationIDArray { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public FileDTO FileDTO { get; set; }
    public bool GetItemHeaderPicture { get; set; }
    public string ItemImg { get; set; }
    public string Names { get; set; }
    public string NamesWithModel { get; set; }

    #endregion
    #region Constructor
    public Item_HeaderDTO()
    {
        Item_HeaderIDArray = new int?[] { };
        ItemClassificationDTO = new ItemClassificationDTO();
        ItemClassificationIDArray = new int?[] { };
        SupportGroupIDArray = new int?[] { };

    }
    #endregion
}
