using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.DAL.Features.Management.Edashboard.Dashboard;

[Persistent(@"DashboardKPI")]
public class Dashboard_KPIXPO : XPObject
{

    public Dashboard_KPIXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue(nameof(Status), ref fStatus, value); }
    }

    DashboardXPO fDashboard;
    public DashboardXPO Dashboard
    {
        get { return fDashboard; }
        set { SetPropertyValue(nameof(Dashboard), ref fDashboard, value); }
    }

    KPIXPO fKPI;
    public KPIXPO KPI
    {
        get { return fKPI; }
        set { SetPropertyValue<KPIXPO>(nameof(KPI), ref fKPI, value); }
    }

    int fOrder;
    public int Order
    {
        get { return fOrder; }
        set { SetPropertyValue<int>(nameof(Order), ref fOrder, value); }
    }

    DashboardCategoryXPO fDashboardCategory;
    public DashboardCategoryXPO DashboardCategory
    {
        get { return fDashboardCategory; }
        set { SetPropertyValue(nameof(DashboardCategory), ref fDashboardCategory, value); }
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
