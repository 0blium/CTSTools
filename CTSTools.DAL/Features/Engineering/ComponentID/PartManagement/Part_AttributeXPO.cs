using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;

[Persistent(@"Part_Attribute")]
public class Part_AttributeXPO : XPObject
{
    public Part_AttributeXPO(Session session) : base(session)
    {

    }
    AttributeXPO fAttribute;
    public AttributeXPO Attribute
    {
        get { return fAttribute; }
        set { SetPropertyValue<AttributeXPO>(nameof(Attribute), ref fAttribute, value); }
    }
    ValueXPO fValue;
    public ValueXPO Value
    {
        get { return fValue; }
        set { SetPropertyValue<ValueXPO>(nameof(Value), ref fValue, value); }
    }
    DecoderXPO fDecoder;
    public DecoderXPO Decoder
    {
        get { return fDecoder; }
        set { SetPropertyValue<DecoderXPO>(nameof(Decoder), ref fDecoder, value); }
    }
    PartXPO fPart;
    public PartXPO Part
    {
        get { return fPart; }
        set { SetPropertyValue<PartXPO>(nameof(Part), ref fPart, value); }
    }
    string fDescription;
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
