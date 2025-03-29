using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Maintenance.AMS.Item;

[Persistent(@"Item_Header")]
public class Item_HeaderXPO : XPObject
{
    public Item_HeaderXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    string fEnglishName;
    public string EnglishName
    {
        get { return fEnglishName; }
        set { SetPropertyValue<string>(nameof(EnglishName), ref fEnglishName, value); }
    }
    string fModel;
    public string Model
    {
        get { return fModel; }
        set { SetPropertyValue<string>(nameof(Model), ref fModel, value); }
    }
    string fBrand;
    public string Brand
    {
        get { return fBrand; }
        set { SetPropertyValue<string>(nameof(Brand), ref fBrand, value); }
    }
    bool fIsESD;
    public bool IsESD
    {
        get { return fIsESD; }
        set { SetPropertyValue<bool>(nameof(IsESD), ref fIsESD, value); }
    }

    ItemClassificationXPO fItemClassification;
    public ItemClassificationXPO ItemClassification
    {
        get { return fItemClassification; }
        set { SetPropertyValue<ItemClassificationXPO>(nameof(ItemClassification), ref fItemClassification, value); }
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