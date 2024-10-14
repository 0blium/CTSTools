using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.AdvancedSettings.UserManagement;

[Persistent(@"User_Permission")]
public class User_PermissionXPO : XPObject
{
    public User_PermissionXPO() : base()
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public User_PermissionXPO(Session session) : base(session)
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place here your initialization code.
    }

    PermissionXPO fPermission;
    public PermissionXPO Permission
    {
        get { return fPermission; }
        set { SetPropertyValue<PermissionXPO>(nameof(Permission), ref fPermission, value); }
    }
    UserXPO fUser;
    public UserXPO User
    {
        get { return fUser; }
        set { SetPropertyValue<UserXPO>(nameof(User), ref fUser, value); }
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