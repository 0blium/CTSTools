using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;

public class DepartmentDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? FacilityID { get; set; }
    public string FacilityName { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public int?[] FacilityIDArray { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public string[] DepartmentNameArray { get; set; }
    public int?[] ResponsiblesIDArray { get; set; }
    public FacilityDTO FacilityDTO { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public bool GetFacilityDTO { get; set; }
    public bool GetAddedBy { get; set; }
    public bool GetDepartmentResponsibleList { get; set; }
    public List<DepartmentResponsibleDTO> Department_ResponsibleList { get; set; }
    public string ResponsibleNames { get; set; }
    #endregion

    #region Constructor
    public DepartmentDTO()
    {
        FacilityIDArray = [];
        DepartmentIDArray = [];
        DepartmentNameArray = [];
        FacilityDTO = new FacilityDTO();
    }
    #endregion
}
