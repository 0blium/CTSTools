using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Files
{
    public class ExcelKPIDTO
    {
        #region Base Properties
        public KPIDTO KPIDTO { get; set; }
        public List<KPIDTO> KPIGoodLinesList { get; set; }
        public List<KPIDTO> KPIBadLinesList { get; set; }
        #endregion

        #region Extended Properties
        public ValidationResultDTO ValidationResultDTO { get; set; }
        #endregion

        #region Constructor
        public ExcelKPIDTO()
        {
            ValidationResultDTO = new ValidationResultDTO { };
            KPIDTO = new KPIDTO();
            KPIGoodLinesList = new List<KPIDTO>();
            KPIBadLinesList = new List<KPIDTO>();
        }
        #endregion
    }
}
