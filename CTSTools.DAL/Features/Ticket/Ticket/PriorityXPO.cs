using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Ticket.Ticket;

[Persistent(@"Priority")]
public class PriorityXPO : XPObject
{
    public PriorityXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    string fName;
    public string Name
    {
        get { return fName; }
        set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
    }
    string fDescription;
    [Size(300)]
    public string Description
    {
        get { return fDescription; }
        set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
    }
    SupportGroupXPO fSupportGroup;
    public SupportGroupXPO SupportGroup
    {
        get { return fSupportGroup; }
        set { SetPropertyValue<SupportGroupXPO>(nameof(SupportGroup), ref fSupportGroup, value); }
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