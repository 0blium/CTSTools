using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.SupplyType;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.TransactionOrigin;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;
using CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_LineDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public string Serial { get; set; }
    public string ManufactureSerialID { get; set; }
    public int ShipmentReceiptID { get; set; }
    public string LegacyID { get; set; }
    public DateTime? IntroductionDate { get; set; }
    public double BasePriceMXN { get; set; }
    public double BasePriceUSD { get; set; }
    public string COO { get; set; }
    //public string PONumber { get; set; }
    //public int POLine { get; set; }
    public string Comments { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }
    public string ItemHeaderNameWithPartNumberSerial { get; set; }
    public string ItemNameWithManufactureSerial { get; set; }
    public string ImportInvoice { get; set; }
    public string ShipmentReceiptNumber { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public int ImportInvoiceLine { get; set; }
    public string DeclarationNumber { get; set; }
    public string TransactionNumber { get; set; }
    public int TransactionLine { get; set; }

    #endregion

    #region Extended Properties

    public int?[] Item_LineIDArray { get; set; }
    public Item_HeaderDTO Item_HeaderDTO { get; set; }
    public bool GetItem_HeaderDTO { get; set; }
    public int?[] Item_HeaderIDArray { get; set; }
    public StationDTO StationDTO { get; set; }
    public bool GetStationDTO { get; set; }
    public int?[] StationIDArray { get; set; }
    public StatusDTO StatusDTO { get; set; }
    public bool GetStatusDTO { get; set; }
    public int?[] StatusIDArray { get; set; }
    public UserDTO OwnerDTO { get; set; }
    public FileDTO FileDTO { get; set; }
    public SupplyTypeDTO SupplyTypeDTO { get; set; }
    public List<UserDefinedValueDTO> UserDefinedValueList { get; set; }
    public bool GetFileList { get; set; }
    public List<FileDTO> FileList { get; set; }
    public Item_SupportGroupDTO Item_SupportGroupDTO { get; set; }
    public bool GetItem_SupportGroupDTO { get; set; }
    public int?[] Item_SupportGroupIDArray { get; set; }
    public bool GenerateSerial { get; set; }
    public TransactionOriginDTO TransactionOriginDTO { get; set; }
    public string[] TransactionNumberArray { get; set; }
    #region Filters
    public int?[] OwnerIDArray { get; set; }
    public int?[] SupplyTypeIDArray { get; set; }
    public int?[] DeliveredToIDArray { get; set; }
    public DateTime StartIntroductionDate { get; set; }
    public DateTime EndIntroductionDate { get; set; }
    #endregion
    #endregion
    #region Constructor
    public Item_LineDTO()
    {
        Item_LineIDArray = new int?[] { };
        Item_HeaderDTO = new Item_HeaderDTO();
        Item_SupportGroupIDArray = new int?[] { };
        Item_SupportGroupDTO = new Item_SupportGroupDTO();
        Item_HeaderIDArray = new int?[] { };
        StationDTO = new StationDTO();
        StationIDArray = new int?[] { };
        StatusDTO = new StatusDTO();
        StatusIDArray = new int?[] { };
        OwnerDTO = new UserDTO();
        UserDefinedValueList = new List<UserDefinedValueDTO>();
        SupplyTypeDTO = new SupplyTypeDTO();
        OwnerIDArray = new int?[] { };
        SupplyTypeIDArray = new int?[] { };
        TransactionOriginDTO = new TransactionOriginDTO();
    }
    #endregion
}
