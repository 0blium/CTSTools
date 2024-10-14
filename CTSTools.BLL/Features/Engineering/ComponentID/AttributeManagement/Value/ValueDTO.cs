using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
public class ValueDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsCounter { get; set; }

    #endregion

    #region Extended Properties

    public int?[] ValueIDArray { get; set; }
    public AttributeDTO AttributeDTO { get; set; }
    public int? AttributeID { get; set; }
    public string AttributeName { get; set; }
    public bool GetAttributeDTO { get; set; }
    public int?[] AttributeIDArray { get; set; }

    #endregion
    #region Constructor
    public ValueDTO()
    {
        ValueIDArray = new int?[] { };        
        AttributeIDArray = new int?[] { };
    }
    #endregion
}

