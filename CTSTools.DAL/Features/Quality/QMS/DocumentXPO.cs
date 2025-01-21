using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Quality.QMS;

[Persistent(@"Document")]
public class DocumentXPO : XPObject
{
    public DocumentXPO(Session session) : base(session)
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
    string fNumber;
    public string Number
    {
        get { return fNumber; }
        set { SetPropertyValue<string>(nameof(Number), ref fNumber, value); }
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
    DocumentTypeXPO fType;
    public DocumentTypeXPO Type
    {
        get { return fType; }
        set { SetPropertyValue<DocumentTypeXPO>(nameof(Type), ref fType, value); }
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

}