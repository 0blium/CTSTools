using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using System;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class DocumentDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string LastRevision { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    #endregion

    #region Extended Properties
    public int?[] DocumentIDArray { get; set; }
    public UserDTO OwnerDTO { get; set; }
    public int? OwnerID { get; set; }
    public string OwnerName { get; set; }
    public DepartmentDTO DepartmentDTO { get; set; }
    public int? DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public DocumentTypeDTO TypeDTO { get; set; }
    public int? TypeID { get; set; }
    public string TypeName { get; set; }
    public bool GetTypeDTO {get; set; }
    public int?[] TypeIDArray { get; set; }
    #endregion

    #region Constructor
    public DocumentDTO()
    {
        DocumentIDArray = new int?[] { };
        OwnerDTO = new UserDTO();
        DepartmentDTO = new DepartmentDTO();
        DepartmentIDArray = new int?[] { };
        TypeDTO = new DocumentTypeDTO();
        TypeIDArray = new int?[] { };   
    }
    #endregion
}
