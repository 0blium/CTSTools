using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Xpo;
using System;
namespace CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
[Persistent(@"DecoderStructure")]
public class DecoderStructureXPO : XPObject
{
    public DecoderStructureXPO(Session session) : base(session)
    {
    }
    // XPObject relationships

    DecoderXPO fDecoder;
    public DecoderXPO Decoder
    {
        get { return fDecoder; }
        set { SetPropertyValue<DecoderXPO>(nameof(Decoder), ref fDecoder, value); }
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
    // Default XPObject (VC)
    // 

    bool fDescriptionBody;
    public bool DescriptionBody
    {
        get { return fDescriptionBody; }
        set { SetPropertyValue<bool>(nameof(DescriptionBody), ref fDescriptionBody, value); }
    }
    int fDescriptionOrder;
    public int DescriptionOrder
    {
        get { return fDescriptionOrder; }
        set { SetPropertyValue<int>(nameof(DescriptionOrder), ref fDescriptionOrder, value); }
    }
    int fNumberOrder;
    public int NumberOrder
    {
        get { return fNumberOrder; }
        set { SetPropertyValue<int>(nameof(NumberOrder), ref fNumberOrder, value); }
    }
    bool fNumberBody;
    public bool NumberBody
    {
        get { return fNumberBody; }
        set { SetPropertyValue<bool>(nameof(NumberBody), ref fNumberBody, value); }
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
}