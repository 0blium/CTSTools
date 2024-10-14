using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.Domain;

public class DomainDTO
{
    #region Base Properties
    public int ID { get; set; }
    public string IP { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? FacilityID { get; set; }
    public string FacilityName { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }
    #endregion

    #region Extended Properties
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public int?[] FacilityIDArray { get; set; }
    public int?[] DomainIDArray { get; set; }
    public ValidationResultDTO Validation_ResultDTO { get; set; }

    #endregion

    #region Constructor
    public DomainDTO()
    {
        DomainIDArray = [];
        FacilityIDArray = [];
        FacilityDTO = new FacilityDTO();
    }
    #endregion

}
