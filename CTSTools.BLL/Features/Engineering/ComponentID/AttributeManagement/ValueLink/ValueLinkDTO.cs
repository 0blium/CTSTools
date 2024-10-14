
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;

public class ValueLinkDTO
{
    #region Base Properties
    public int? ID { get; set; }

    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }

    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    public int? ParentAttributeID { get; set; }
    public string ParentAttributeName { get; set; }

    public int? ParentValueID { get; set; }
    public string ParentValueName { get; set; }

    public int? ChildAttributeID { get; set; }
    public string ChildAttributeName { get; set; }

    public int? ChildValueID { get; set; }
    public string ChildValueName { get; set; }

    #endregion

    #region Extended Properties
    public AttributeDTO ParentAttributeDTO { get; set; }
    public AttributeDTO ChildAttributeDTO { get; set; }
    public ValueDTO ChildValueDTO { get; set; }
    public ValueLinkDTO ClassDTO { get; set; }
    public ValueDTO ParentValueDTO { get; set; }
    public int?[] ParentValueIDArray { get; set; }
    public int?[] ParentAttributeIDArray { get; set; }
    public int?[] ChildAttributeIDArray { get; set; }
    public int?[] ChildValueIDArray { get; set; }
    public int?[] ValueLinkIDArray { get; set; }

    public bool GetParentAttributeDTO { get; set; }
    public bool GetParentValueDTO { get; set; }
    public bool GetChildAttributeDTO { get; set; }
    public bool GetChildValueDTO { get; set; }
    public bool GetValueLinkDTO { get; set; }

    #endregion
    #region Constructor
    public ValueLinkDTO()
    {
        ParentValueDTO = new ValueDTO();
        ChildAttributeDTO = new AttributeDTO();
        ParentAttributeDTO = new AttributeDTO();
        ChildValueDTO = new ValueDTO();
        ValueLinkIDArray = [];
        ParentValueIDArray = [];
        ParentAttributeIDArray = [];
        ChildValueIDArray = [];
        ChildAttributeIDArray = [];
    }

    #endregion
}
