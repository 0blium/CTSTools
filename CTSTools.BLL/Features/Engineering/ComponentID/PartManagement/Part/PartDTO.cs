using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
public class PartDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public string MfgPartNumber { get; set; }
    public string Comment { get; set; }
    public int? DecoderID { get; set; }
    public int? SupplierID { get; set; }
    public string SupplierName { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public int? SubClassID { get; set; }
    public int?[] DecoderIDArray { get; set; }
    public DecoderDTO DecoderDTO { get; set; }
    public bool GetDecoderDTO { get; set; }
    public int?[] SupplierIDArray { get; set; }
    public SupplierDTO SupplierDTO { get; set; }
    public bool GetSupplierDTO { get; set; }
    public List<ValueDTO> ValueList { get; set; }
    public int?[] PartIDArray { get; set; }
    #endregion

    #region Constructor
    public PartDTO()
    {
        DecoderIDArray = [];
        SupplierIDArray = [];
        PartIDArray = [];
        DecoderDTO = new DecoderDTO();
        SupplierDTO = new SupplierDTO();
        ValueList = new List<ValueDTO>();
    }
    #endregion
}
