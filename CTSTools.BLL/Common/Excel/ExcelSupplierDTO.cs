using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Excel
{
    public class ExcelSupplierDTO
    {
        #region Base Properties
        public SupplierDTO SupplierDTO { get; set; }
        public List<SupplierDTO> SupplierGoodLinesList { get; set; }
        public List<SupplierDTO> SupplierBadLinesList { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public ExcelSupplierDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };
            SupplierDTO = new SupplierDTO();
            SupplierGoodLinesList = new List<SupplierDTO>();
            SupplierBadLinesList = new List<SupplierDTO>();
        }
        #endregion
    }
}
