using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using CTSTools.DAL.Features.Engineering.ComponentID.SubClass;
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
    string fModel;
    public string Model
    {
        get { return fModel; }
        set { SetPropertyValue<string>(nameof(Model), ref fModel, value); }
    }
    BrandXPO fBrand;
    public BrandXPO Brand
    {
        get { return fBrand; }
        set { SetPropertyValue<BrandXPO>(nameof(Brand), ref fBrand, value); }
    }
    ClassXPO fClass;
    public ClassXPO Class
    {
        get { return fClass; }
        set { SetPropertyValue<ClassXPO>(nameof(Class), ref fClass, value); }
    }
    SubClassXPO fSubClass;
    public SubClassXPO SubClass
    {
        get { return fSubClass; }
        set { SetPropertyValue<SubClassXPO>(nameof(SubClass), ref fSubClass, value); }
    }
    //bool fIsESD;
    //public bool IsESD
    //{
    //    get { return fIsESD; }
    //    set { SetPropertyValue<bool>(nameof(IsESD), ref fIsESD, value); }
    //}

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