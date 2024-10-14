using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;

public class DepartmentResponsibleDTO
{
    #region Base properties
    public int? ID { get; set; }
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }

    public int ResponsibleID { get; set; }
    public string ResponsibleName { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? AddedDate { get; set; }
    public string AddedDateString { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public string LastUpdateString { get; set; }
    public bool? IsActive { get; set; }
    #endregion

    #region extended properties
    public DepartmentDTO DepartmentDTO { get; set; }
    public UserDTO ResponsibleDTO { get; set; }
    public bool GetResponsibleDTO { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    #endregion
    #region Constructor
    public DepartmentResponsibleDTO()
    {
        DepartmentDTO = new DepartmentDTO();
        ResponsibleDTO = new UserDTO();
    }
    #endregion
}
