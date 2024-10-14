
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;
namespace CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;

[Persistent(@"Value")]
public class ValueXPO : XPObject
{
    public ValueXPO(Session session) : base(session)
    {
    }
    // XPObject relationships

    AttributeXPO fAttribute;
    public AttributeXPO Attribute
    {
        get { return fAttribute; }
        set { SetPropertyValue<AttributeXPO>(nameof(Attribute), ref fAttribute, value); }
    }
    // Default XPObject (VC)

    string fName;
    public string Name
    {
        get { return fName; }
        set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
    }
    string fDescription;
    public string Description
    {
        get { return fDescription; }
        set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
    }
    string fCode;
    public string Code
    {
        get { return fCode; }
        set { SetPropertyValue<string>(nameof(Code), ref fCode, value); }
    }
    bool fIsCounter;
    public bool IsCounter
    {
        get { return fIsCounter; }
        set { SetPropertyValue<bool>(nameof(IsCounter), ref fIsCounter, value); }
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
