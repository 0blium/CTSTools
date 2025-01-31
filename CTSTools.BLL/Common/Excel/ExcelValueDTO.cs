using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using System.Collections.Generic;

namespace CTSTools.BLL.Common.Excel
{
    public class ExcelValueDTO
    {
        #region Base Properties
        public int? RowIteration { get; set; }
        public ValueDTO ValueDTO { get; set; }
        public List<ValueDTO> ValueGoodLinesList { get; set; }
        public List<ValueDTO> ValueBadLinesList { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public ExcelValueDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };
            ValueDTO = new ValueDTO();
            ValueGoodLinesList = new List<ValueDTO>();
            ValueBadLinesList = new List<ValueDTO>();
        }
        #endregion
    }
}
