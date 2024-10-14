using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using System;
using System.Collections.Generic;
namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;

public class AttributeDTO
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
    public bool? IsActive { get; set; }
    public bool? HasMultipleOptions { get; set; }

    #endregion

    #region Extended Properties

    public int?[] AttributeIDArray { get; set; }
    public bool GetValueList { get; set; }
    public List<ValueDTO> ValueList { get; set; }
    public ValueDTO ValueDTO { get; set; }
    #endregion
    #region Constructor
    public AttributeDTO()
    {
        AttributeIDArray = new int?[] { };
        ValueList = new List<ValueDTO>();
        ValueDTO = new ValueDTO(); 

    }
    #endregion
}

