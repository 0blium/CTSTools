using System;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;

public class StatusDTO
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
    public int?[] StatusIDArray { get; set; }
    public string[] StatusNameArray { get; set; }
    #endregion

    #region Constructor
    public StatusDTO()
    {
        StatusIDArray = new int?[] { };
        StatusNameArray = new string[] { };
    }
    #endregion
}
