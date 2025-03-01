using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using CTSTools.DAL.Features.Ticket.Provider;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Ticket.SparePart;

[Persistent(@"SparePart_Lot")]
public class SparePart_LotXPO : XPObject
{
    public SparePart_LotXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    int fQuantity;
    public int Quantity
    {
        get { return fQuantity; }
        set { SetPropertyValue<int>(nameof(Quantity), ref fQuantity, value); }
    }
    int fAvailableQty;
    public int AvailableQty
    {
        get { return fAvailableQty; }
        set { SetPropertyValue<int>(nameof(AvailableQty), ref fAvailableQty, value); }
    }


    string fPartNumber;
    public string PartNumber
    {
        get { return fPartNumber; }
        set { SetPropertyValue<string>(nameof(PartNumber), ref fPartNumber, value); }
    }
    ProviderXPO fProvider;
    public ProviderXPO Provider
    {
        get { return fProvider; }
        set { SetPropertyValue<ProviderXPO>(nameof(Provider), ref fProvider, value); }
    }
    SparePartXPO fSparePart;
    public SparePartXPO SparePart
    {
        get { return fSparePart; }
        set { SetPropertyValue<SparePartXPO>(nameof(SparePart), ref fSparePart, value); }
    }
    SparePartInventoryXPO fSparePartInventory;
    public SparePartInventoryXPO SparePartInventory
    {
        get { return fSparePartInventory; }
        set { SetPropertyValue<SparePartInventoryXPO>(nameof(SparePartInventory), ref fSparePartInventory, value); }
    }
    SupportGroupXPO fSupportGroup;
    public SupportGroupXPO SupportGroup
    {
        get { return fSupportGroup; }
        set { SetPropertyValue<SupportGroupXPO>(nameof(SupportGroup), ref fSupportGroup, value); }
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

    double fCost;
    public double Cost
    {
        get { return fCost; }
        set { SetPropertyValue<double>(nameof(Cost), ref fCost, value); }
    }

    double fUnitCost;
    public double UnitCost
    {
        get { return fUnitCost; }
        set { SetPropertyValue<double>(nameof(UnitCost), ref fUnitCost, value); }
    }
    string fSerial;
    public string Serial
    {
        get { return fSerial; }
        set { SetPropertyValue<string>(nameof(Serial), ref fSerial, value); }
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
}