using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.DAL.Features.Management.Edashboard.Dashboard;

[Persistent(@"Dashboard")]
public class DashboardXPO : XPObject
{

    public DashboardXPO(Session session) : base(session)
    {
    }
    // XPObject relationships



    // Default XPObject (VC)

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
    string fRevision;
    public string Revision
    {
        get { return fRevision; }
        set { SetPropertyValue<string>(nameof(Revision), ref fRevision, value); }
    }
    string fComment;
    public string Comment
    {
        get { return fComment; }
        set { SetPropertyValue<string>(nameof(Comment), ref fComment, value); }
    }
    int fYear;
    public int Year
    {
        get { return fYear; }
        set { SetPropertyValue<int>(nameof(Year), ref fYear, value); }
    }
    UserXPO fOwner;
    public UserXPO Owner
    {
        get { return fOwner; }
        set { SetPropertyValue(nameof(Owner), ref fOwner, value); }
    }
    DepartmentXPO fDepartment;
    public DepartmentXPO Department
    {
        get { return fDepartment; }
        set { SetPropertyValue<DepartmentXPO>(nameof(Department), ref fDepartment, value); }
    }
    LevelXPO fLevel;
    public LevelXPO Level
    {
        get { return fLevel; }
        set { SetPropertyValue<LevelXPO>(nameof(Level), ref fLevel, value); }
    }
    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue<StatusXPO>(nameof(Status), ref fStatus, value); }
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
