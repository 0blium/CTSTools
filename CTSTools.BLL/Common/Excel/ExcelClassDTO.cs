using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Excel
{
    public class ExcelClassDTO
    {
        #region Base Properties
        public ClassDTO ClassDTO { get; set; }
        public List<ClassDTO> ClassGoodLinesList { get; set; }
        public List<ClassDTO> ClassBadLinesList { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public ExcelClassDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };
            ClassDTO = new ClassDTO();
            ClassGoodLinesList = new List<ClassDTO>();
            ClassBadLinesList = new List<ClassDTO>();
        }
        #endregion
    }
}
