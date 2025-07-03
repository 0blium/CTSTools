using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using CTSTools.DAL.Features.Engineering.ComponentID.PartType;
using DevExpress.Xpo;
using System;

namespace CTSTools.DAL.Features.Engineering.ComponentID.SubClass
{
    [Persistent(@"SubClass")]
    public class SubClassXPO : XPObject
    {
        public SubClassXPO() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public SubClassXPO(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        // XPObject relationships
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
        ClassXPO fClass;
        public ClassXPO Class
        {
            get { return fClass; }
            set { SetPropertyValue<ClassXPO>(nameof(Class), ref fClass, value); }
        }

        // Default XPObject (VC)
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
        }
        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>(nameof(Code), ref fCode, value); }
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

}