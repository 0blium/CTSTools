using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Xpo;
using System;
namespace CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;

[Persistent(@"Decoder")]
public class DecoderXPO : XPObject
{
    public DecoderXPO(Session session) : base(session)
    {
    }
    // XPObject relationships

    ValueXPO fPartType;
    public ValueXPO PartType
    {
        get { return fPartType; }
        set { SetPropertyValue<ValueXPO>(nameof(PartType), ref fPartType, value); }
    }
    ValueXPO fComponentType;
    public ValueXPO ComponentType
    {
        get { return fComponentType; }
        set { SetPropertyValue<ValueXPO>(nameof(ComponentType), ref fComponentType, value); }
    }
    ValueXPO fClass;
    public ValueXPO Class
    {
        get { return fClass; }
        set { SetPropertyValue<ValueXPO>(nameof(Class), ref fClass, value); }
    }
    ValueXPO fSubClass;
    public ValueXPO SubClass
    {
        get { return fSubClass; }
        set { SetPropertyValue<ValueXPO>(nameof(SubClass), ref fSubClass, value); }
    }
    ValueXPO fClassID;
    public ValueXPO ClassID
    {
        get { return fClassID; }
        set { SetPropertyValue<ValueXPO>(nameof(ClassID), ref fClassID, value); }
    }
    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue<StatusXPO>(nameof(Status), ref fStatus, value); }
    }

    // Default XPObject (VC)        
    string fDescription;
    public string Description
    {
        get { return fDescription; }
        set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
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
