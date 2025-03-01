using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Ticket.Station.Station;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.SupportGroup;

public class SupportGroupDTO
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

    public int?[] SupportGroupIDArray { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    public StationDTO StationDTO { get; set; }
    public bool GetStationDTO { get; set; }
    public bool GetSupportGroupWithStation { get; set; }
    #endregion
    #region Constructor
    public SupportGroupDTO()
    {
        SupportGroupIDArray = new int?[] { };
        FacilityDTO = new FacilityDTO();
        FacilityIDArray = new int?[] { };
        StationDTO = new StationDTO();

    }
    #endregion
}
