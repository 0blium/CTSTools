using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Files
{
    public class ExcelAttributeDTO
    {
        #region Base Properties
        public AttributeDTO AttributeDTO { get; set; }
        public List<AttributeDTO> AttributeGoodLinesList { get; set; }
        public List<AttributeDTO> AttributeBadLinesList { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public ExcelAttributeDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };
            AttributeDTO = new AttributeDTO();
            AttributeGoodLinesList = new List<AttributeDTO>();
            AttributeBadLinesList = new List<AttributeDTO>();
        }
        #endregion
    }
}
