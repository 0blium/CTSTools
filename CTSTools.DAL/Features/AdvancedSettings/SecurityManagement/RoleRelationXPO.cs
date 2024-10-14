using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Security.Role;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

[Persistent(@"RoleRelation")]
public class RoleRelationXPO : XPObject
{
    public RoleRelationXPO() : base()
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public RoleRelationXPO(Session session) : base(session)
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place here your initialization code.
    }

    RoleXPO fRole;
    public RoleXPO Role
    {
        get { return fRole; }
        set { SetPropertyValue<RoleXPO>(nameof(Role), ref fRole, value); }
    }
    RoleTypeXPO fRoleType;
    public RoleTypeXPO RoleType
    {
        get { return fRoleType; }
        set { SetPropertyValue<RoleTypeXPO>(nameof(RoleType), ref fRoleType, value); }
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