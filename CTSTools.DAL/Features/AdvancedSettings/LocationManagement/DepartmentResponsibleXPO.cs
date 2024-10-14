using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.AdvancedSettings.LocationManagement;

[Persistent(@"DepartmentResponsible")]
public class DepartmentResponsibleXPO : XPObject
{
    public DepartmentResponsibleXPO() : base()
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public DepartmentResponsibleXPO(Session session) : base(session)
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place here your initialization code.
    }

   

    DepartmentXPO fDepartment;
    public DepartmentXPO Department
    {
        get { return fDepartment; }
        set { SetPropertyValue<DepartmentXPO>(nameof(Department), ref fDepartment, value); }
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
    UserXPO fResponsible;
    public UserXPO Responsible
    {
        get { return fResponsible; }
        set { SetPropertyValue<UserXPO>(nameof(Responsible), ref fResponsible, value); }
    }
}