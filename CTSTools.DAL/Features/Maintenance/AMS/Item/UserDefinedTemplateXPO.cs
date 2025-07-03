using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Maintenance.AMS.Item;

[Persistent(@"UserDefinedTemplate")]
public class UserDefinedTemplateXPO : XPObject
{
    public UserDefinedTemplateXPO(Session session) : base(session)
    {
    }
    // XPObject relationships

    Item_HeaderXPO fItem_Header;
    public Item_HeaderXPO Item_Header
    {
        get { return fItem_Header; }
        set { SetPropertyValue<Item_HeaderXPO>(nameof(Item_Header), ref fItem_Header, value); }
    }

    // Default XPObject (VC)
    Item_SupportGroupXPO fItem_SupportGroup;
    public Item_SupportGroupXPO Item_SupportGroup
    {
        get { return fItem_SupportGroup; }
        set { SetPropertyValue<Item_SupportGroupXPO>(nameof(Item_SupportGroup), ref fItem_SupportGroup, value); }
    }
    UserDefinedXPO fUserDefined;
    public UserDefinedXPO UserDefined
    {
        get { return fUserDefined; }
        set { SetPropertyValue<UserDefinedXPO>(nameof(UserDefined), ref fUserDefined, value); }
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