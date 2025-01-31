using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.SubClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Excel
{
    public class ExcelSubClassDTO
    {
        #region Base Properties
        public int? RowIteration { get; set; }
        public SubClassDTO SubClassDTO { get; set; }
        public List<SubClassDTO> SubClassGoodLinesList { get; set; }
        public List<SubClassDTO> SubClassBadLinesList { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public ExcelSubClassDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };
            SubClassDTO = new SubClassDTO();
            SubClassGoodLinesList = new List<SubClassDTO>();
            SubClassBadLinesList = new List<SubClassDTO>();
        }
        #endregion
    }
}
