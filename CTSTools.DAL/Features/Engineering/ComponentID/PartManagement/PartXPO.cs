using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;

[Persistent(@"Part")]
public class PartXPO : XPObject
{
    public PartXPO(Session session) : base(session)
    {

    }
    string fNumber;
    public string Number
    {
        get { return fNumber; }
        set { SetPropertyValue<string>(nameof(Number), ref fNumber, value); }
    }
    string fComment;
    [Size(1000)]
    public string Comment
    {
        get { return fComment; }
        set { SetPropertyValue<string>(nameof(Comment), ref fComment, value); }
    }
    string fMfgPartNumber;
    [Size(1000)]
    public string MfgPartNumber
    {
        get { return fMfgPartNumber; }
        set { SetPropertyValue<string>(nameof(MfgPartNumber), ref fMfgPartNumber, value); }
    }
    DecoderXPO fDecoder;
    public DecoderXPO Decoder
    {
        get { return fDecoder; }
        set { SetPropertyValue<DecoderXPO>(nameof(Decoder), ref fDecoder, value); }
    }
    SupplierXPO fSupplier;
    public SupplierXPO Supplier
    {
        get { return fSupplier; }
        set { SetPropertyValue<SupplierXPO>(nameof(Supplier), ref fSupplier, value); }
    }
    string fDescription;
    [Size(1000)]
    public string Description
    {
        get { return fDescription; }
        set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
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
