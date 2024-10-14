using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.AdvancedSettings.UserManagement;

[Persistent(@"User")]
public class UserXPO : XPObject
{
    public UserXPO() : base()
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public UserXPO(Session session) : base(session)
    {
        // This constructor is used when an object is loaded from a persistent storage.
        // Do not place any code here.
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place here your initialization code.
    }

    string fLogin;
    public string Login
    {
        get { return fLogin; }
        set { SetPropertyValue<string>(nameof(Login), ref fLogin, value); }
    }

    string fName;
    public string Name
    {
        get { return fName; }
        set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
    }

    string fEmail;
    public string Email
    {
        get { return fEmail; }
        set { SetPropertyValue<string>(nameof(Email), ref fEmail, value); }
    }

    string fPosition;
    public string Position
    {
        get { return fPosition; }
        set { SetPropertyValue<string>(nameof(Position), ref fPosition, value); }
    }

    FacilityXPO fFacility;
    public FacilityXPO Facility
    {
        get { return fFacility; }
        set { SetPropertyValue<FacilityXPO>(nameof(Facility), ref fFacility, value); }
    }

   
    DepartmentXPO fDepartment;
    public DepartmentXPO Department
    {
        get { return fDepartment; }
        set { SetPropertyValue<DepartmentXPO>(nameof(Department), ref fDepartment, value); }
    }     

    UserXPO fAddedBy;
    public UserXPO AddedBy
    {
        get { return fAddedBy; }
        set { SetPropertyValue<UserXPO>(nameof(AddedBy), ref fAddedBy, value); }
    }
    DateTime? fAddedDate;
    public DateTime? AddedDate
    {
        get { return fAddedDate; }
        set { SetPropertyValue<DateTime?>(nameof(AddedDate), ref fAddedDate, value); }
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