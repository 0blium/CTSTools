using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Priority;

public class PriorityDTO
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
    public string PriorityWithSupportGroup { get; set; }
    public int?[] PriorityIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }

    #endregion
    #region Constructor
    public PriorityDTO()
    {
        PriorityIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };

    }
    #endregion
}
