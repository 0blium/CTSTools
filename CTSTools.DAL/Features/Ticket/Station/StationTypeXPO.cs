using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Ticket.Station;

[Persistent(@"StationType")]
public class StationTypeXPO : XPObject
{
    public StationTypeXPO(Session session) : base(session)
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
    [Size(500)]
    public string Description
    {
        get { return fDescription; }
        set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
    }

    FacilityXPO fFacility;
    public FacilityXPO Facility
    {
        get { return fFacility; }
        set { SetPropertyValue<FacilityXPO>(nameof(Facility), ref fFacility, value); }
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
