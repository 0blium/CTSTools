using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;

public class DecoderDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? PartTypeID { get; set; }
    public string PartTypeName { get; set; }
    public string ComponentTypeName { get; set; }
    public int? ComponentTypeID { get; set; }
    public string ClassName { get; set; }
    public int? ClassID { get; set; }
    public string SubClassName { get; set; }
    public int? SubClassID { get; set; }
    public string StatusName { get; set; }
    public int? StatusID { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public List<DecoderStructureDTO> DecoderStructureList { get; set; }
    public List<ValueDTO> ValueList { get; set; }
    public ValueDTO PartTypeDTO { get; set; }
    public ValueDTO ComponentTypeDTO { get; set; }
    public ValueDTO ClassDTO { get; set; }
    public ValueDTO SubClassDTO { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public bool GetStatusDTO { get; set; }
    public bool GetComponentTypeDTO { get; set; }
    public bool GetPartTypeDTO { get; set; }
    public bool GetClassDTO { get; set; }
    public bool GetSubClassDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    public int?[] SubClassIDArray { get; set; }
    public int?[] ClassIDArray { get; set; }
    public int?[] PartTypeIDArray { get; set; }
    public int?[] ComponentTypeIDArray { get; set; }
    public int?[] DecoderIDArray { get; set; }


    #endregion
    #region Constructor
    public DecoderDTO()
    {
        DecoderIDArray = [];
        StatusIDArray = [];
        SubClassIDArray = [];
        StatusIDArray = [];
        PartTypeIDArray = [];
        ComponentTypeIDArray = [];
        StatusDTO = new StatusDTO();
        PartTypeDTO = new ValueDTO();
        ClassDTO = new ValueDTO();
        SubClassDTO = new ValueDTO();
        ComponentTypeDTO = new ValueDTO();

    }
    #endregion
}

