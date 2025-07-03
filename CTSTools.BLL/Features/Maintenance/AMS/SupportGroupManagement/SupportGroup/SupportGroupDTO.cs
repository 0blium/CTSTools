using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;

public class SupportGroupDTO
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
    public string Names { get; set; }
    #endregion

    #region Extended Properties

    public int?[] SupportGroupIDArray { get; set; }
    public string[] SupportGroupNameArray { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    #endregion
    #region Constructor
    public SupportGroupDTO()
    {
        SupportGroupIDArray = new int?[] { };
        FacilityDTO = new FacilityDTO();
        FacilityIDArray = new int?[] { };

    }
    #endregion
}
