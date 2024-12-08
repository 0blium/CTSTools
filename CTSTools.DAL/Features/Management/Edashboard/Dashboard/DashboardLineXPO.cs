using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.DAL.Features.Management.Edashboard.Dashboard;

[Persistent(@"DashboardLine")]
public class DashboardLineXPO : XPObject
{

    public DashboardLineXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)

    DashboardMetricXPO fDashboardMetric;
    public DashboardMetricXPO DashboardMetric
    {
        get { return fDashboardMetric; }
        set { SetPropertyValue(nameof(DashboardMetric), ref fDashboardMetric, value); }
    }
    MetricXPO fMetric;
    public MetricXPO Metric
    {
        get { return fMetric; }
        set { SetPropertyValue(nameof(Metric), ref fMetric, value); }
    }
    DashboardCategoryXPO fDashboardCategory;
    public DashboardCategoryXPO DashboardCategory
    {
        get { return fDashboardCategory; }
        set { SetPropertyValue(nameof(DashboardCategory), ref fDashboardCategory, value); }
    }
    DashboardXPO fDashboard;
    public DashboardXPO Dashboard
    {
        get { return fDashboard; }
        set { SetPropertyValue(nameof(Dashboard), ref fDashboard, value); }
    }
    float fGoal;
    public float Goal
    {
        get { return fGoal; }
        set { SetPropertyValue(nameof(Goal), ref fGoal, value); }
    }

    int fMonth;
    public int Month
    {
        get { return fMonth; }
        set { SetPropertyValue<int>(nameof(Month), ref fMonth, value); }
    }

    int fYear;
    public int Year
    {
        get { return fYear; }
        set { SetPropertyValue<int>(nameof(Year), ref fYear, value); }
    }

    int fFiscalYear;
    public int FiscalYear
    {
        get { return fFiscalYear; }
        set { SetPropertyValue<int>(nameof(FiscalYear), ref fFiscalYear, value); }
    }

    float fDecimal;
    public float Decimal
    {
        get { return fDecimal; }
        set { SetPropertyValue(nameof(Decimal), ref fDecimal, value); }
    }

    float fValue;
    public float Value
    {
        get { return fValue; }
        set { SetPropertyValue(nameof(Value), ref fValue, value); }
    }

    bool fIsTemporalValue;
    public bool IsTemporalValue
    {
        get { return fIsTemporalValue; }
        set { SetPropertyValue<bool>(nameof(IsTemporalValue), ref fIsTemporalValue, value); }
    }

    string fComment;
    public string Comment
    {
        get { return fComment; }
        set { SetPropertyValue<string>(nameof(Comment), ref fComment, value); }
    }

    bool fIgnoreMetric;
    public bool IgnoreMetric
    {
        get { return fIgnoreMetric; }
        set { SetPropertyValue<bool>(nameof(IgnoreMetric), ref fIgnoreMetric, value); }
    }

    bool fValidated;
    public bool Validated
    {
        get { return fValidated; }
        set { SetPropertyValue<bool>(nameof(Validated), ref fValidated, value); }
    }
    UserXPO fValidatedBy;
    public UserXPO ValidatedBy
    {
        get { return fValidatedBy; }
        set { SetPropertyValue(nameof(ValidatedBy), ref fValidatedBy, value); }
    }
    DateTime? fValidatedDate;
    public DateTime? ValidatedDate
    {
        get { return fValidatedDate; }
        set { SetPropertyValue<DateTime?>(nameof(ValidatedDate), ref fValidatedDate, value); }
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
