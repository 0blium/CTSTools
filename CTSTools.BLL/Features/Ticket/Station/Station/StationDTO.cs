using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Ticket.Station.StationType;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Station.Station;

public class StationDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Serial { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }
    public string NameWithSerial { get; set; }

    #endregion

    #region Extended Properties

    public int?[] StationIDArray { get; set; }
    public int?[] Item_LineIDArray { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    public DepartmentDTO DepartmentDTO { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public StationTypeDTO StationTypeDTO { get; set; }
    public bool GetStationTypeDTO { get; set; }
    public int?[] StationTypeIDArray { get; set; }

    #endregion
    #region Constructor
    public StationDTO()
    {
        StationIDArray = new int?[] { };
        FacilityDTO = new FacilityDTO();
        FacilityIDArray = new int?[] { };
        DepartmentDTO = new DepartmentDTO();
        DepartmentIDArray = new int?[] { };
        StationTypeDTO = new StationTypeDTO();
        StationTypeIDArray = new int?[] { };
        Item_LineIDArray = new int?[] { };

    }
    #endregion
}
