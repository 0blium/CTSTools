using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;

[Persistent(@"ValueLink")]
public class ValueLinkXPO : XPObject
{
    public ValueLinkXPO(Session session) : base(session)
    {

    }

    #region XPObject relationships

    AttributeXPO fParentAttribute;
    public AttributeXPO ParentAttribute
    {
        get { return fParentAttribute; }
        set { SetPropertyValue<AttributeXPO>(nameof(Attribute), ref fParentAttribute, value); }
    }
    ValueXPO fParentValue;
    public ValueXPO ParentValue
    {
        get { return fParentValue; }
        set { SetPropertyValue<ValueXPO>(nameof(ParentValue), ref fParentValue, value); }
    }
    AttributeXPO fChildAttribute;
    public AttributeXPO ChildAttribute
    {
        get { return fChildAttribute; }
        set { SetPropertyValue<AttributeXPO>(nameof(ChildAttribute), ref fChildAttribute, value); }
    }
    ValueXPO fChildValue;
    public ValueXPO ChildValue
    {
        get { return fChildValue; }
        set { SetPropertyValue<ValueXPO>(nameof(ChildValue), ref fChildValue, value); }
    }
    #endregion 

    #region Default XPObject (VC)

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
    #endregion
}
