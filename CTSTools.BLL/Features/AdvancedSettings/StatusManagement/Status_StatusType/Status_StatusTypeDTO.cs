
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.StatusType;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;

public class Status_StatusTypeDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string StatusName { get; set; }
    public int? StatusID { get; set; }
    public string StatusTypeName { get; set; }
    public int? StatusTypeID { get; set; }
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

    public int?[] Status_StatusTypeIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    public StatusTypeDTO StatusTypeDTO { get; set; }
    public bool GetStatusTypeDTO { get; set; }
    public int?[] StatusTypeIDArray { get; set; }
    #endregion
    #region Constructor
    public Status_StatusTypeDTO()
    {
        Status_StatusTypeIDArray = [];
        StatusDTO = new StatusDTO();
        StatusIDArray = [];            
        StatusTypeDTO = new StatusTypeDTO();
        StatusTypeIDArray = [];

    }
    #endregion
}
