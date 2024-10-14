using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;

public class SubClass_SupplierDTO
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
    public int SubClassID { get; set; }
    public string SubClassName { get; set; }
    public int SupplierID { get; set; }
    public string SupplierName { get; set; }
    public bool? IsActive { get; set; }
    #endregion

    #region Extended Properties
    public bool GetSupplierDTO { get; set; }
    public SupplierDTO SupplierDTO { get; set; }
    public int?[] SupplierIDArray { get; set; }
    public bool GetSubClassDTO { get; set; }
    public ValueDTO SubClass { get; set; }
    public int?[] SubClassIDArray { get; set; }
    #endregion

    #region Constructor
    public SubClass_SupplierDTO()
    {
        SubClassIDArray = []; 
        SupplierIDArray = [];
    }
    #endregion

}
