using CTSTools.BLL.Features.AdvancedSettings.TransactionOrigin;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.Provider;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;

public class SparePart_LotDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int Quantity { get; set; }
    public int AvailableQty { get; set; }
    public string PartNumber { get; set; }
    public string TransactionNumber { get; set; }
    public int TransactionLine { get; set; }
    public double Cost { get; set; }
    public double UnitCost { get; set; }
    public string Serial { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties
    public string LotSerialWithSparePartName { get; set; }
    public int?[] SparePart_LotIDArray { get; set; }
    public ProviderDTO ProviderDTO { get; set; }
    public bool GetProviderDTO { get; set; }
    public int?[] ProviderIDArray { get; set; }
    public SparePartDTO SparePartDTO { get; set; }
    public bool GetSparePartDTO { get; set; }
    public int?[] SparePartIDArray { get; set; }
    public SparePartInventoryDTO SparePartInventoryDTO { get; set; }
    public bool GetSparePartInventoryDTO { get; set; }
    public int?[] SparePartInventoryIDArray { get; set; }
    public SupportGroupDTO SupportGroupDTO { get; set; }
    public bool GetSupportGroupDTO { get; set; }
    public int?[] SupportGroupIDArray { get; set; }
    public TransactionOriginDTO TransactionOriginDTO { get; set; }
    public bool GetTransactionOriginDTO { get; set; }
    public int?[] TransactionOriginIDArray { get; set; }

    #endregion
    #region Constructor
    public SparePart_LotDTO()
    {
        SparePart_LotIDArray = new int?[] { };
        ProviderDTO = new ProviderDTO();
        ProviderIDArray = new int?[] { };
        SparePartDTO = new SparePartDTO();
        SparePartIDArray = new int?[] { };
        SparePartInventoryDTO = new SparePartInventoryDTO();
        SparePartInventoryIDArray = new int?[] { };
        SupportGroupDTO = new SupportGroupDTO();
        SupportGroupIDArray = new int?[] { };
        TransactionOriginDTO = new TransactionOriginDTO();
        TransactionOriginIDArray = new int?[] { };

    }
    #endregion
}
