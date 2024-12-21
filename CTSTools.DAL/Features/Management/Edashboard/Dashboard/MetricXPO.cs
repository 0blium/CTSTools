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

[Persistent(@"Metric")]
public class MetricXPO : XPObject
{

    public MetricXPO(Session session) : base(session)
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
    UnitOfMeasureXPO fUnitOfMeasure;
    public UnitOfMeasureXPO UnitOfMeasure
    {
        get { return fUnitOfMeasure; }
        set { SetPropertyValue(nameof(UnitOfMeasure), ref fUnitOfMeasure, value); }
    }

    ValueTypeXPO fValueType;
    public ValueTypeXPO ValueType
    {
        get { return fValueType; }
        set { SetPropertyValue<ValueTypeXPO>(nameof(ValueType), ref fValueType, value); }
    }

    UserXPO fOwner;
    public UserXPO Owner
    {
        get { return fOwner; }
        set { SetPropertyValue(nameof(Owner), ref fOwner, value); }
    }

    UserXPO fResponsible;
    public UserXPO Responsible
    {
        get { return fResponsible; }
        set { SetPropertyValue(nameof(Responsible), ref fResponsible, value); }
    }

    DepartmentXPO fOwnerDepartment;
    public DepartmentXPO OwnerDepartment
    {
        get { return fOwnerDepartment; }
        set { SetPropertyValue(nameof(OwnerDepartment), ref fOwnerDepartment, value); }
    }

    DepartmentXPO fResponsibleDepartment;
    public DepartmentXPO ResponsibleDepartment
    {
        get { return fResponsibleDepartment; }
        set { SetPropertyValue(nameof(ResponsibleDepartment), ref fResponsibleDepartment, value); }
    }

    bool fShared;
    public bool Shared
    {
        get { return fShared; }
        set { SetPropertyValue<bool>(nameof(Shared), ref fShared, value); }
    }

    float fGoal;
    public float Goal
    {
        get { return fGoal; }
        set { SetPropertyValue(nameof(Goal), ref fGoal, value); }
    }

    GoalRangeXPO fGoalRange;
    public GoalRangeXPO GoalRange
    {
        get { return fGoalRange; }
        set { SetPropertyValue(nameof(GoalRange), ref fGoalRange, value); }
    }

    FacilityXPO fFacility;
    public FacilityXPO Facility
    {
        get { return fFacility; }
        set { SetPropertyValue(nameof(Facility), ref fFacility, value); }
    }

    EquivalenceXPO fEquivalence;
    public EquivalenceXPO Equivalence
    {
        get { return fEquivalence; }
        set { SetPropertyValue(nameof(Equivalence), ref fEquivalence, value); }
    }

    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue(nameof(Status), ref fStatus, value); }
    }

    bool fIsParent;
    public bool IsParent
    {
        get { return fIsParent; }
        set { SetPropertyValue<bool>(nameof(IsParent), ref fIsParent, value); }
    }

    MetricXPO fParenMetric;
    public MetricXPO ParenMetric
    {
        get { return fParenMetric; }
        set { SetPropertyValue(nameof(ParenMetric), ref fParenMetric, value); }
    }

    DashboardCategoryXPO fDashboardCategory;
    public DashboardCategoryXPO DashboardCategory
    {
        get { return fDashboardCategory; }
        set { SetPropertyValue(nameof(DashboardCategory), ref fDashboardCategory, value); }
    }

    CalculationTypeXPO fCalculationType;
    public CalculationTypeXPO CalculationType
    {
        get { return fCalculationType; }
        set { SetPropertyValue(nameof(CalculationType), ref fCalculationType, value); }
    }

    CalculationTypeXPO fFiscalYearCalculationType;
    public CalculationTypeXPO FiscalYearCalculationType
    {
        get { return fFiscalYearCalculationType; }
        set { SetPropertyValue(nameof(FiscalYearCalculationType), ref fFiscalYearCalculationType, value); }
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
