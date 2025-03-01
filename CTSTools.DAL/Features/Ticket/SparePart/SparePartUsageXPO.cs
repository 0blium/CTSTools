using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using CTSTools.DAL.Features.Ticket.Ticket;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Ticket.SparePart;

[Persistent(@"SparePartUsage")]
public class SparePartUsageXPO : XPObject
{
    public SparePartUsageXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    TicketXPO fTicket;
    public TicketXPO Ticket
    {
        get { return fTicket; }
        set { SetPropertyValue<TicketXPO>(nameof(Ticket), ref fTicket, value); }
    }

    SparePartInventoryXPO fSparePartInventory;
    public SparePartInventoryXPO SparePartInventory
    {
        get { return fSparePartInventory; }
        set { SetPropertyValue<SparePartInventoryXPO>(nameof(SparePartInventory), ref fSparePartInventory, value); }
    }
    Item_LineXPO fItem_Line;
    public Item_LineXPO Item_Line
    {
        get { return fItem_Line; }
        set { SetPropertyValue<Item_LineXPO>(nameof(Item_Line), ref fItem_Line, value); }
    }
    int fQuantity;
    public int Quantity
    {
        get { return fQuantity; }
        set { SetPropertyValue<int>(nameof(Quantity), ref fQuantity, value); }
    }

    SparePart_LotXPO fSparePart_Lot;
    public SparePart_LotXPO SparePart_Lot
    {
        get { return fSparePart_Lot; }
        set { SetPropertyValue<SparePart_LotXPO>(nameof(SparePart_Lot), ref fSparePart_Lot, value); }
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