using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Ticket.SparePart;

[Persistent(@"SparePartInventory")]
public class SparePartInventoryXPO : XPObject
{
    public SparePartInventoryXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    int fMaxQty;
    public int MaxQty
    {
        get { return fMaxQty; }
        set { SetPropertyValue<int>(nameof(MaxQty), ref fMaxQty, value); }
    }
    int fMinQty;
    public int MinQty
    {
        get { return fMinQty; }
        set { SetPropertyValue<int>(nameof(MinQty), ref fMinQty, value); }
    }
    SparePartXPO fSparePart;
    public SparePartXPO SparePart
    {
        get { return fSparePart; }
        set { SetPropertyValue<SparePartXPO>(nameof(SparePart), ref fSparePart, value); }
    }
    SupportGroupXPO fSupportGroup;
    public SupportGroupXPO SupportGroup
    {
        get { return fSupportGroup; }
        set { SetPropertyValue<SupportGroupXPO>(nameof(SupportGroup), ref fSupportGroup, value); }
    }
    int fAvailableQty;
    public int AvailableQty
    {
        get { return fAvailableQty; }
        set { SetPropertyValue<int>(nameof(AvailableQty), ref fAvailableQty, value); }
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