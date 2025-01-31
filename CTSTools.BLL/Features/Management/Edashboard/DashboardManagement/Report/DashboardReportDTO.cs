using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Report
{
    public class DashboardReportDTO
    {

        public int ID { get; set; }
        public int KPIID { get; set; }
        public int? Dashboard_KPIID { get; set; }
        public string DashboardCategoryName { get; set; }
        public string DashboardCategoryLetter { get; set; }
        public int DashboardCategoryID { get; set; }
        public string DepartmentInteraction { get; set; }
        public string KPIName { get; set; }
        public string Goal { get; set; }
        public Decimal? OctuberValue { get; set; }
        public Decimal OctuberGoalValue { get; set; }
        public string OctuberBackgroundColor { get; set; }
        public Boolean OctuberIgnoreKPI { get; set; }
        public Decimal? NovemberValue { get; set; }
        public Decimal NovemberGoalValue { get; set; }
        public string NovemberBackgroundColor { get; set; }
        public Boolean NovemberIgnoreKPI { get; set; }
        public Decimal? DecemberValue { get; set; }
        public Decimal DecemberGoalValue { get; set; }
        public string DecemberBackgroundColor { get; set; }
        public Boolean DecemberIgnoreKPI { get; set; }
        public Decimal? JanuaryValue { get; set; }
        public Decimal JanuaryGoalValue { get; set; }
        public string JanuaryBackgroundColor { get; set; }
        public Boolean JanuaryIgnoreKPI { get; set; }
        public Decimal? FebruaryValue { get; set; }
        public Decimal FebruaryGoalValue { get; set; }
        public string FebruaryBackgroundColor { get; set; }
        public Boolean FebruaryIgnoreKPI { get; set; }
        public Decimal? MarchValue { get; set; }
        public Decimal MarchGoalValue { get; set; }
        public string MarchBackgroundColor { get; set; }
        public Boolean MarchIgnoreKPI { get; set; }
        public Decimal? AprilValue { get; set; }
        public Decimal AprilGoalValue { get; set; }
        public string AprilBackgroundColor { get; set; }
        public Boolean AprilIgnoreKPI { get; set; }
        public Decimal? MayValue { get; set; }
        public Decimal MayGoalValue { get; set; }
        public string MayBackgroundColor { get; set; }
        public Boolean MayIgnoreKPI { get; set; }
        public Decimal? JuneValue { get; set; }
        public Decimal JuneGoalValue { get; set; }
        public string JuneBackgroundColor { get; set; }
        public Boolean JuneIgnoreKPI { get; set; }
        public Decimal? JulyValue { get; set; }
        public Decimal JulyGoalValue { get; set; }
        public string JulyBackgroundColor { get; set; }
        public Boolean JulyIgnoreKPI { get; set; }
        public Decimal? AugustValue { get; set; }
        public Decimal AugustGoalValue { get; set; }
        public string AugustBackgroundColor { get; set; }
        public Boolean AugustIgnoreKPI { get; set; }
        public Decimal? SepValue { get; set; }
        public Decimal SepGoalValue { get; set; }
        public string SepBackgroundColor { get; set; }
        public Boolean SepIgnoreKPI { get; set; }
        public string Responsible { get; set; }
        public Boolean IsShared { get; set; }
        public int EquivalenceID { get; set; }
        public int RowSpan { get; set; }
        public int Order { get; set; }
        public string KPIBackgroundColor { get; set; }
        public int FiscalYear { get; set; }
        public Boolean IsRedKPI { get; set; }
        public int FiscalYearCalculationTypeID { get; set; }
        public Decimal GoalRange { get; set; }
        public Boolean IgnoreKPI { get; set; }
        public Boolean IsReferenceOnly { get; set; }
        public int ValueType { get; set; }
    }
}
