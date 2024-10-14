using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Xpo;
using System;
namespace CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;

[Persistent(@"SubClass_Supplier")]
public class SubClass_SupplierXPO : XPObject {

    public SubClass_SupplierXPO(Session session) : base(session)
    {

    }
    ValueXPO fSubClass;
    public ValueXPO SubClass
    {
        get { return fSubClass; }
        set { SetPropertyValue<ValueXPO>(nameof(SubClass), ref fSubClass, value); }
    }
    SupplierXPO fSupplier;
    public SupplierXPO Supplier
    {
        get { return fSupplier; }
        set { SetPropertyValue<SupplierXPO>(nameof(Supplier), ref fSupplier, value); }
    }
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
