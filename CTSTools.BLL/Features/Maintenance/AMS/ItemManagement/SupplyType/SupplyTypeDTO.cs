using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.SupplyType;

public class SupplyTypeDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] SupplyTypeIDArray { get; set; }

    #endregion
    #region Constructor
    public SupplyTypeDTO()
    {
        SupplyTypeIDArray = new int?[] { };

    }
    #endregion
}
