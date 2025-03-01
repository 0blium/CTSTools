using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.ChangeLog;

[Persistent(@"ChangeLog")]
public class ChangeLogXPO : XPObject
{
    public ChangeLogXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    int fRecordID;
    public int RecordID
    {
        get { return fRecordID; }
        set { SetPropertyValue<int>(nameof(RecordID), ref fRecordID, value); }
    }

    string fTable;
    public string Table
    {
        get { return fTable; }
        set { SetPropertyValue<string>(nameof(Table), ref fTable, value); }
    }
    string fField;
    public string Field
    {
        get { return fField; }
        set { SetPropertyValue<string>(nameof(Field), ref fField, value); }
    }
    string fOldValue;
    public string OldValue
    {
        get { return fOldValue; }
        set { SetPropertyValue<string>(nameof(OldValue), ref fOldValue, value); }
    }
    string fNewValue;
    public string NewValue
    {
        get { return fNewValue; }
        set { SetPropertyValue<string>(nameof(NewValue), ref fNewValue, value); }
    }
    UserXPO fUser;
    public UserXPO User
    {
        get { return fUser; }
        set { SetPropertyValue<UserXPO>(nameof(User), ref fUser, value); }
    }
    string fAction;
    public string Action
    {
        get { return fAction; }
        set { SetPropertyValue<string>(nameof(Action), ref fAction, value); }
    }
    string fChangeGroup;
    public string ChangeGroup
    {
        get { return fChangeGroup; }
        set { SetPropertyValue<string>(nameof(ChangeGroup), ref fChangeGroup, value); }
    }
    DateTime? fAddedDate;
    public DateTime? AddedDate
    {
        get { return fAddedDate; }
        set { SetPropertyValue<DateTime?>(nameof(AddedDate), ref fAddedDate, value); }
    }
}