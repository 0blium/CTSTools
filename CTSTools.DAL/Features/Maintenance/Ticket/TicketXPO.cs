using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Maintenance.Ticket;

[Persistent(@"Ticket")]
public class TicketXPO : XPObject
{
    public TicketXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    string fTitle;
    public string Title
    {
        get { return fTitle; }
        set { SetPropertyValue<string>(nameof(Title), ref fTitle, value); }
    }
    string fDescription;
    [Size(SizeAttribute.Unlimited)]
    public string Description
    {
        get { return fDescription; }
        set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
    }
    string fSolution;
    [Size(SizeAttribute.Unlimited)]
    public string Solution
    {
        get { return fSolution; }
        set { SetPropertyValue<string>(nameof(Solution), ref fSolution, value); }
    }
    string fResolution;
    [Size(SizeAttribute.Unlimited)]
    public string Resolution
    {
        get { return fResolution; }
        set { SetPropertyValue<string>(nameof(Resolution), ref fResolution, value); }
    }
    int fTicketNumber;
    public int TicketNumber
    {
        get { return fTicketNumber; }
        set { SetPropertyValue<int>(nameof(TicketNumber), ref fTicketNumber, value); }
    }
    UserXPO fCreatedBy;
    public UserXPO CreatedBy
    {
        get { return fCreatedBy; }
        set { SetPropertyValue<UserXPO>(nameof(CreatedBy), ref fCreatedBy, value); }
    }
    FacilityXPO fFacility;
    public FacilityXPO Facility
    {
        get { return fFacility; }
        set { SetPropertyValue<FacilityXPO>(nameof(Facility), ref fFacility, value); }
    }

    //BusinessUnitXPO fBusinessUnit;
    //public BusinessUnitXPO BusinessUnit
    //{
    //    get { return fBusinessUnit; }
    //    set { SetPropertyValue<BusinessUnitXPO>(nameof(BusinessUnit), ref fBusinessUnit, value); }
    //}
    DepartmentXPO fDepartment;
    public DepartmentXPO Department
    {
        get { return fDepartment; }
        set { SetPropertyValue<DepartmentXPO>(nameof(Department), ref fDepartment, value); }
    }
    //AreaXPO fArea;
    //public AreaXPO Area
    //{
    //    get { return fArea; }
    //    set { SetPropertyValue<AreaXPO>(nameof(Area), ref fArea, value); }
    //}
    UserXPO fAssignedTo;
    public UserXPO AssignedTo
    {
        get { return fAssignedTo; }
        set { SetPropertyValue<UserXPO>(nameof(AssignedTo), ref fAssignedTo, value); }
    }

    DateTime? fAssignedDate;
    public DateTime? AssignedDate
    {
        get { return fAssignedDate; }
        set { SetPropertyValue<DateTime?>(nameof(AssignedDate), ref fAssignedDate, value); }
    }
    string fNote;
    [Size(SizeAttribute.Unlimited)]
    public string Note
    {
        get { return fNote; }
        set { SetPropertyValue<string>(nameof(Note), ref fNote, value); }
    }
    Item_LineXPO fItem_Line;
    public Item_LineXPO Item_Line
    {
        get { return fItem_Line; }
        set { SetPropertyValue<Item_LineXPO>(nameof(Item_Line), ref fItem_Line, value); }
    }
    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue<StatusXPO>(nameof(Status), ref fStatus, value); }
    }
    PriorityXPO fPriority;
    public PriorityXPO Priority
    {
        get { return fPriority; }
        set { SetPropertyValue<PriorityXPO>(nameof(Priority), ref fPriority, value); }
    }
    CategoryXPO fCategory;
    public CategoryXPO Category
    {
        get { return fCategory; }
        set { SetPropertyValue<CategoryXPO>(nameof(Category), ref fCategory, value); }
    }
    CategoryXPO fSubCategory;
    public CategoryXPO SubCategory
    {
        get { return fSubCategory; }
        set { SetPropertyValue<CategoryXPO>(nameof(SubCategory), ref fSubCategory, value); }
    }
    CategoryXPO fThirdLevelCategory;
    public CategoryXPO ThirdLevelCategory
    {
        get { return fThirdLevelCategory; }
        set { SetPropertyValue<CategoryXPO>(nameof(ThirdLevelCategory), ref fThirdLevelCategory, value); }
    }
    SupportGroupXPO fSupportGroup;
    public SupportGroupXPO SupportGroup
    {
        get { return fSupportGroup; }
        set { SetPropertyValue<SupportGroupXPO>(nameof(SupportGroup), ref fSupportGroup, value); }
    }
    DateTime? fAddedDate;
    public DateTime? AddedDate
    {
        get { return fAddedDate; }
        set { SetPropertyValue<DateTime?>(nameof(AddedDate), ref fAddedDate, value); }
    }
    UserXPO fClosedBy;
    public UserXPO ClosedBy
    {
        get { return fClosedBy; }
        set { SetPropertyValue<UserXPO>(nameof(ClosedBy), ref fClosedBy, value); }
    }
    DateTime? fClosedDate;
    public DateTime? ClosedDate
    {
        get { return fClosedDate; }
        set { SetPropertyValue<DateTime?>(nameof(ClosedDate), ref fClosedDate, value); }
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