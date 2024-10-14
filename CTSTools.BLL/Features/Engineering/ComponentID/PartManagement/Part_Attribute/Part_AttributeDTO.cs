using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;

public class Part_AttributeDTO
{
    #region Base Properties
    public int? ID { get; set; }   
    public int? DecoderID { get; set; }
    public int? ValueID { get; set; }
    public string ValueName { get; set; }
    public int? PartID { get; set; }
    public int? AttributeID { get; set; }
    public string AttributeName { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public int?[] Part_AttributeIDArray { get; set; }
    public int?[] DecoderIDArray { get; set; }
    public DecoderDTO DecoderDTO { get; set; }
    public bool GetDecoderDTO { get; set; }

    public int?[] AttributeIDArray { get; set; }
    public AttributeDTO AttributeDTO { get; set; }
    public bool GetAttributeDTO { get; set; }

    public int?[] PartIDArray { get; set; }
    public PartDTO PartDTO { get; set; }
    public bool GetPartDTO { get; set; }

    public int?[] ValueIDArray { get; set; }
    public ValueDTO ValueDTO { get; set; }
    public bool GetValueDTO { get; set; }
    #endregion

    #region Constructor
    public Part_AttributeDTO()
    {
        DecoderIDArray = [];
        PartIDArray = [];
        ValueIDArray = [];
        AttributeIDArray = [];
        Part_AttributeIDArray = [];

        DecoderDTO = new DecoderDTO();
        ValueDTO = new ValueDTO();
        PartDTO = new PartDTO();
        AttributeDTO = new AttributeDTO();
    }
    #endregion

}
