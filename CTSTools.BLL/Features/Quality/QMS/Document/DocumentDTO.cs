using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Quality.QMS.Customer;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using CTSTools.BLL.Features.Quality.QMS.Product;
using System;

namespace CTSTools.BLL.Features.Quality.QMS.Document;

public class DocumentDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string URL { get; set; }
    public string LastRevision { get; set; }
    public int? StatusID { get; set; }
    public string StatusName { get; set; }
    public string Description { get; set; }
    public int? OwnerID { get; set; }
    public string OwnerName { get; set; }
    public int? TypeID { get; set; }
    public string TypeName { get; set; }
    public int? DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public string Revision { get; set; }
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
   
    public DepartmentDTO DepartmentDTO { get; set; }
   
    public bool GetDepartmentDTO { get; set; }
    public int?[] DepartmentIDArray { get; set; }
    public DocumentTypeDTO TypeDTO { get; set; }
   
    public bool GetTypeDTO {get; set; }
    public int?[] TypeIDArray { get; set; }
    public CustomerDTO CustomerDTO { get; set; }
    public int? CustomerID { get; set; }
    public string CustomerName { get; set; }
    public bool GetCustomerDTO { get; set; }
    public int?[] CustomerIDArray { get; set; }
    public ProductDTO ProductDTO { get; set; }
    public int? ProductID { get; set; }
    public string ProductName { get; set; }
    public bool GetProductDTO { get; set; }
    public int?[] ProductIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }

    public FileDTO FileDTO { get; set; }
    #endregion

    #region Constructor
    public DocumentDTO()
    {
        DocumentIDArray = [];
        OwnerDTO = new UserDTO();
        DepartmentDTO = new DepartmentDTO();
        DepartmentIDArray = [];
        TypeDTO = new DocumentTypeDTO();
        TypeIDArray = new int?[] { };
        CustomerDTO = new CustomerDTO();
        CustomerIDArray = new int?[] { };
        ProductDTO = new ProductDTO();
        ProductIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = [];
        FileDTO = new FileDTO();
    }
    #endregion
}
