using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.ItemClassification;

public class ItemClassificationDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string EnglishName { get; set; }
    public string SpanishName { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    public string Names { get; set; }

    #endregion

    #region Extended Properties

    public int?[] ItemClassificationIDArray { get; set; }

    #endregion
    #region Constructor
    public ItemClassificationDTO()
    {
        ItemClassificationIDArray = new int?[] { };

    }
    #endregion
}
