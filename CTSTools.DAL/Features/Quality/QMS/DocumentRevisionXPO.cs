using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Quality.QMS;

[Persistent(@"DocumentRevision")]
public class DocumentRevisionXPO : XPObject
{
    public DocumentRevisionXPO(Session session) : base(session)
    {
    }
    // XPObject relationships


    // Default XPObject (VC)
    string fRevision;
    public string Revision
    {
        get { return fRevision; }
        set { SetPropertyValue<string>(nameof(Revision), ref fRevision, value); }
    }
    string fChangeReason;
    public string ChangeReason
    {
        get { return fChangeReason; }
        set { SetPropertyValue<string>(nameof(ChangeReason), ref fChangeReason, value); }
    }
    DocumentXPO fDocument;
    public DocumentXPO Document
    {
        get { return fDocument; }
        set { SetPropertyValue(nameof(Document), ref fDocument, value); }
    }
    StatusXPO fStatus;
    public StatusXPO Status
    {
        get { return fStatus; }
        set { SetPropertyValue(nameof(Status), ref fStatus, value); }
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