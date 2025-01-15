using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

[Persistent(@"Permission")]
public class PermissionXPO : XPObject
{
    public PermissionXPO() : base()
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public PermissionXPO(Session session) : base(session)
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place here your initialization code.
    }
    ModuleXPO fModule;
    public ModuleXPO Module
    {
        get { return fModule; }
        set { SetPropertyValue<ModuleXPO>(nameof(Module), ref fModule, value); }
    }
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
    ActionXPO fAction;
    public ActionXPO Action
    {
        get { return fAction; }
        set { SetPropertyValue<ActionXPO>(nameof(Action), ref fAction, value); }
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