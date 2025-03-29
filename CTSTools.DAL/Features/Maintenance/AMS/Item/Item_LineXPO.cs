using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Station;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Maintenance.AMS.Item;

[Persistent(@"Item_Line")]
public class Item_LineXPO : XPObject
{
    public Item_LineXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    Item_HeaderXPO fItem_Header;
    public Item_HeaderXPO Item_Header
    {
        get { return fItem_Header; }
        set { SetPropertyValue<Item_HeaderXPO>(nameof(Item_Header), ref fItem_Header, value); }
    }
    Item_SupportGroupXPO fItem_SupportGroup;
    public Item_SupportGroupXPO Item_SupportGroup
    {
        get { return fItem_SupportGroup; }
        set { SetPropertyValue<Item_SupportGroupXPO>(nameof(Item_SupportGroup), ref fItem_SupportGroup, value); }
    }

    StationXPO fStation;
    public StationXPO Station
    {
        get { return fStation; }
        set { SetPropertyValue<StationXPO>(nameof(Station), ref fStation, value); }
    }
    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue<StatusXPO>(nameof(Status), ref fStatus, value); }
    }
    UserXPO fOwner;
    public UserXPO Owner
    {
        get { return fOwner; }
        set { SetPropertyValue<UserXPO>(nameof(Owner), ref fOwner, value); }
    }

    string fSerial;
    public string Serial
    {
        get { return fSerial; }
        set { SetPropertyValue<string>(nameof(Serial), ref fSerial, value); }
    }
    string fManufactureSerialID;
    public string ManufactureSerialID
    {
        get { return fManufactureSerialID; }
        set { SetPropertyValue<string>(nameof(ManufactureSerialID), ref fManufactureSerialID, value); }
    }

    int fShipmentReceiptID;
    public int ShipmentReceiptID
    {
        get { return fShipmentReceiptID; }
        set { SetPropertyValue<int>(nameof(ShipmentReceiptID), ref fShipmentReceiptID, value); }
    }

    string fLegacyID;
    public string LegacyID
    {
        get { return fLegacyID; }
        set { SetPropertyValue<string>(nameof(LegacyID), ref fLegacyID, value); }
    }

    double fBasePriceUSD;
    public double BasePriceUSD
    {
        get { return fBasePriceUSD; }
        set { SetPropertyValue<double>(nameof(BasePriceUSD), ref fBasePriceUSD, value); }
    }

    string fComments;
    public string Comments
    {
        get { return fComments; }
        set { SetPropertyValue<string>(nameof(Comments), ref fComments, value); }
    }

    DateTime? fAddedDate;
    public DateTime? AddedDate
    {
        get { return fAddedDate; }
        set { SetPropertyValue<DateTime?>(nameof(AddedDate), ref fAddedDate, value); }
    }
    UserXPO fAddedBy;
    public UserXPO AddedBy
    {
        get { return fAddedBy; }
        set { SetPropertyValue<UserXPO>(nameof(AddedBy), ref fAddedBy, value); }
    }
    DateTime? fLastUpdate;
    public DateTime? LastUpdate
    {
        get { return fLastUpdate; }
        set { SetPropertyValue<DateTime?>(nameof(LastUpdate), ref fLastUpdate, value); }
    }
    UserXPO fLastUpdateBy;
    public UserXPO LastUpdateBy
    {
        get { return fLastUpdateBy; }
        set { SetPropertyValue<UserXPO>(nameof(LastUpdateBy), ref fLastUpdateBy, value); }
    }
    bool fIsActive;
    public bool IsActive
    {
        get { return fIsActive; }
        set { SetPropertyValue<bool>(nameof(IsActive), ref fIsActive, value); }
    }
    string fImportInvoice;
    public string ImportInvoice
    {
        get { return fImportInvoice; }
        set { SetPropertyValue<string>(nameof(ImportInvoice), ref fImportInvoice, value); }
    }
    string fShipmentReceiptNumber;
    public string ShipmentReceiptNumber
    {
        get { return fShipmentReceiptNumber; }
        set { SetPropertyValue<string>(nameof(ShipmentReceiptNumber), ref fShipmentReceiptNumber, value); }
    }
    SupplyTypeXPO fSupplyType;
    public SupplyTypeXPO SupplyType
    {
        get { return fSupplyType; }
        set { SetPropertyValue<SupplyTypeXPO>(nameof(SupplyType), ref fSupplyType, value); }
    }
    DateTime? fDeliveredDate;
    public DateTime? DeliveredDate
    {
        get { return fDeliveredDate; }
        set { SetPropertyValue<DateTime?>(nameof(DeliveredDate), ref fDeliveredDate, value); }
    }
    int fImportInvoiceLine;
    public int ImportInvoiceLine
    {
        get { return fImportInvoiceLine; }
        set { SetPropertyValue<int>(nameof(ImportInvoiceLine), ref fImportInvoiceLine, value); }
    }
    string fDeclarationNumber;
    public string DeclarationNumber
    {
        get { return fDeclarationNumber; }
        set { SetPropertyValue<string>(nameof(DeclarationNumber), ref fDeclarationNumber, value); }
    }
    TransactionOriginXPO fTransactionOrigin;
    public TransactionOriginXPO TransactionOrigin
    {
        get { return fTransactionOrigin; }
        set { SetPropertyValue<TransactionOriginXPO>(nameof(TransactionOrigin), ref fTransactionOrigin, value); }
    }
    string fTransactionNumber;
    public string TransactionNumber
    {
        get { return fTransactionNumber; }
        set { SetPropertyValue<string>(nameof(TransactionNumber), ref fTransactionNumber, value); }
    }

    int fTransactionLine;
    public int TransactionLine
    {
        get { return fTransactionLine; }
        set { SetPropertyValue<int>(nameof(TransactionLine), ref fTransactionLine, value); }
    }
}