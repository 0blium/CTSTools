using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

public class SupplierDTO
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
    public bool? IsVendor { get; set; }
    public bool? IsManufacturer { get; set; }
    public bool? IsActive { get; set; }
    #endregion

    #region Extended Properties

    public int?[] SupplierIDArray { get; set; }

    #endregion
    #region Constructor
    public SupplierDTO()
    {
        SupplierIDArray = [];
    }
    #endregion
}
