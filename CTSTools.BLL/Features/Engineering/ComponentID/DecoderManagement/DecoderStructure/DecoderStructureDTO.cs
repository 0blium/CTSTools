using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;


public class DecoderStructureDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? ValueID { get; set; }
    public string ValueName { get; set; }
    public int? AttributeID { get; set; }
    public string AttributeName { get; set; }
    public int? DecoderID { get; set; }
    public bool? DescriptionBody { get; set; }
    public int DescriptionOrder { get; set; }
    public int NumberOrder { get; set; }
    public bool? NumberBody { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }

    #endregion

    #region Extended Properties
    public int NewNumberOrder { get; set; }
    public int NewDescriptionOrder { get; set; }
    public int? NewChangedDecoderStructureID { get; set; }
    public int? SubClassID { get; set; }
    public int?[] DecoderStructureIDArray { get; set; }
    public List<ValueDTO> ValueList { get; set; }
    public DecoderDTO DecoderDTO { get; set; }
    public ValueDTO ValueDTO { get; set; }
    public ValueDTO SubClassDTO { get; set; }
    public AttributeDTO AttributeDTO { get; set; }
    public bool GetValueList { get; set; }
    public bool GetDecoderDTO { get; set; }
    public bool GetAttributeDTO { get; set; }
    public bool GetValueDTO { get; set; }
    public bool GetAttributeValueLinkList { get; set; }
    public int?[] DecoderIDArray { get; set; }
    public int?[] ValueIDArray { get; set; }
    public int?[] AttributeIDArray { get; set; }

    #endregion
    #region Constructor
    public DecoderStructureDTO()
    {
        DecoderStructureIDArray = [];
        DecoderIDArray = [];
        AttributeIDArray = [];
        ValueIDArray = [];
        DecoderDTO = new DecoderDTO();       
        AttributeDTO = new AttributeDTO();
        SubClassDTO = new ValueDTO();
        ValueDTO = new ValueDTO();
    }
    #endregion
}

