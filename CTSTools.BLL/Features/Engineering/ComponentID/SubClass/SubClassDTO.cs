using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SubClass
{
    public class SubClassDTO
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
        public int? ClassID { get; set; }
        public string ClassName { get; set; }
        public ClassDTO ClassDTO { get; set; }
        public int?[] ClassIDArray { get; set; }
        public bool GetClassDTO { get; set; }
        public int?[] SubClassIDArray { get; set; }
        public string[] SubClassNameArray { get; set; }

        #endregion

        #region Constructor
        public SubClassDTO()
        {
            AttributeDTO = new AttributeDTO();
            AttributeIDArray = new int?[] { };
            ValueDTO = new ValueDTO();
            ValueIDArray = new int?[] { };
            ClassDTO = new ClassDTO();
            ClassIDArray = new int?[] { };
            SubClassIDArray = new int?[] { };
        }
        #endregion
    }
}
