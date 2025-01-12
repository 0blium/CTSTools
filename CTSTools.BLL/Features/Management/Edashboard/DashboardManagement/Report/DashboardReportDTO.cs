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
        public int MetricID { get; set; }
        public string DashboardCategoryName { get; set; }
        public string DashboardCategoryLetter { get; set; }
        public int DashboardCategoryID { get; set; }
        public string DepartmentInteraction { get; set; }
        public string MetricName { get; set; }
        public string Goal { get; set; }
        public Decimal? OctuberValue { get; set; }
        public Decimal OctuberGoalValue { get; set; }
        public string OctuberBackgroundColor { get; set; }
        public Boolean OctuberIgnoreMetric { get; set; }
        public Decimal? NovemberValue { get; set; }
        public Decimal NovemberGoalValue { get; set; }
        public string NovemberBackgroundColor { get; set; }
        public Boolean NovemberIgnoreMetric { get; set; }
        public Decimal? DecemberValue { get; set; }
        public Decimal DecemberGoalValue { get; set; }
        public string DecemberBackgroundColor { get; set; }
        public Boolean DecemberIgnoreMetric { get; set; }
        public Decimal? JanuaryValue { get; set; }
        public Decimal JanuaryGoalValue { get; set; }
        public string JanuaryBackgroundColor { get; set; }
        public Boolean JanuaryIgnoreMetric { get; set; }
        public Decimal? FebruaryValue { get; set; }
        public Decimal FebruaryGoalValue { get; set; }
        public string FebruaryBackgroundColor { get; set; }
        public Boolean FebruaryIgnoreMetric { get; set; }
        public Decimal? MarchValue { get; set; }
        public Decimal MarchGoalValue { get; set; }
        public string MarchBackgroundColor { get; set; }
        public Boolean MarchIgnoreMetric { get; set; }
        public Decimal? AprilValue { get; set; }
        public Decimal AprilGoalValue { get; set; }
        public string AprilBackgroundColor { get; set; }
        public Boolean AprilIgnoreMetric { get; set; }
        public Decimal? MayValue { get; set; }
        public Decimal MayGoalValue { get; set; }
        public string MayBackgroundColor { get; set; }
        public Boolean MayIgnoreMetric { get; set; }
        public Decimal? JuneValue { get; set; }
        public Decimal JuneGoalValue { get; set; }
        public string JuneBackgroundColor { get; set; }
        public Boolean JuneIgnoreMetric { get; set; }
        public Decimal? JulyValue { get; set; }
        public Decimal JulyGoalValue { get; set; }
        public string JulyBackgroundColor { get; set; }
        public Boolean JulyIgnoreMetric { get; set; }
        public Decimal? AugustValue { get; set; }
        public Decimal AugustGoalValue { get; set; }
        public string AugustBackgroundColor { get; set; }
        public Boolean AugustIgnoreMetric { get; set; }
        public Decimal? SepValue { get; set; }
        public Decimal SepGoalValue { get; set; }
        public string SepBackgroundColor { get; set; }
        public Boolean SepIgnoreMetric { get; set; }
        public string Responsible { get; set; }
        public Boolean IsShared { get; set; }
        public int EquivalenceID { get; set; }
        public int RowSpan { get; set; }
        public int Order { get; set; }
        public string MetricBackgroundColor { get; set; }
        public int FiscalYear { get; set; }
        public Boolean IsRedMetric { get; set; }
        public int FiscalYearCalculationTypeID { get; set; }
        public Decimal GoalRange { get; set; }
        public Boolean IgnoreMetric { get; set; }
        public Boolean IsReferenceOnly { get; set; }
        public int ValueType { get; set; }
    }
}
