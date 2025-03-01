using CTSTools.BLL.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePart;

public class SparePartDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ManufactureID { get; set; }
    public string NameWithManufactureID { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] SparePartIDArray { get; set; }
    public bool GetImage { get; set; }
    public string SparePartImage { get; set; }
    public FileDTO FileDTO { get; set; }
    #endregion
    #region Constructor
    public SparePartDTO()
    {
        SparePartIDArray = new int?[] { };

    }
    #endregion
}
