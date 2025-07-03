using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartType
{
    public class PartTypeDTO
    {
        #region Base Properties
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? AddedByID { get; set; }
        public string AddedByName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? LastUpdateByID { get; set; }
        public string LastUpdateByName { get; set; }
        public bool? IsActive { get; set; }

        #endregion

        #region Extended Properties

        public int? AttributeID { get; set; }
        public string AttributeName { get; set; }
        public AttributeDTO AttributeDTO { get; set; }
        public int?[] AttributeIDArray { get; set; }
        public bool GetAttributeDTO { get; set; }
        public int? ValueID { get; set; }
        public string ValueName { get; set; }
        public ValueDTO ValueDTO { get; set; }
        public int?[] ValueIDArray { get; set; }
        public bool GetValueDTO { get; set; }
        public int?[] PartTypeIDArray { get; set; }

        #endregion

        #region Constructor
        public PartTypeDTO()
        {
            AttributeDTO = new AttributeDTO();
            AttributeIDArray = new int?[] { };
            ValueDTO = new ValueDTO();
            ValueIDArray = new int?[] { };
            PartTypeIDArray = new int?[] { };
        }
        #endregion
    }
}
