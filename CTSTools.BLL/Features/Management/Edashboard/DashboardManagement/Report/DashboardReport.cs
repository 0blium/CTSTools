using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Report
{
    public partial class DashboardReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DashboardReport()
        {
            InitializeComponent();

            //this.BeforePrint += Report_BeforePrint;

        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            List<Dashboard_KPI.Dashboard_KPIDTO> _mainDatasource = new List<Dashboard_KPIDTO>((IEnumerable<Dashboard_KPIDTO>)Report.DataSource);
            List<Edashboard.DashboardManagement.Report.DashboardReportDTO> _reportDataSourceList = ProcessReportDataSource(_mainDatasource);
            if (_reportDataSourceList.Count > 0)
            {
                //Create table
                XRTable _table = CreateTable(_reportDataSourceList[0].FiscalYear);
                
                //Start processing data
                ProcessDashboardInformation(_reportDataSourceList.Where(x => x.DashboardCategoryID == (int)DashboardCategory_Enum.Quality).OrderBy(x => x.Order).ToList(), _table, _mainDatasource[0]);
                ProcessDashboardInformation(_reportDataSourceList.Where(x => x.DashboardCategoryID == (int)DashboardCategory_Enum.Safety).OrderBy(x => x.Order).ToList(), _table, _mainDatasource[0]);
                ProcessDashboardInformation(_reportDataSourceList.Where(x => x.DashboardCategoryID == (int)DashboardCategory_Enum.Delivery).OrderBy(x => x.Order).ToList(), _table, _mainDatasource[0]);
                ProcessDashboardInformation(_reportDataSourceList.Where(x => x.DashboardCategoryID == (int)DashboardCategory_Enum.Moral).OrderBy(x => x.Order).ToList(), _table, _mainDatasource[0]);
                ProcessDashboardInformation(_reportDataSourceList.Where(x => x.DashboardCategoryID == (int)DashboardCategory_Enum.Cost).OrderBy(x => x.Order).ToList(), _table, _mainDatasource[0]);
                ProcessDashboardInformation(_reportDataSourceList.Where(x => x.DashboardCategoryID == (int)DashboardCategory_Enum.Emvioramental).OrderBy(x => x.Order).ToList(), _table, _mainDatasource[0]);

                //Create table total row
                //CreateTableTotalRow(_table, _reportDataSourceList);


            }
        }

        private void CreateTableTotalRow(XRTable Table, List<DashboardReportDTO> ReportList)
        {
            //Calculate total information
            var _monthlyTotalValues = CalculateTotal(ReportList);
            int _counter = 0; decimal _total = 0;
            if (_monthlyTotalValues.OctuberTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.OctuberTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.NovemberTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.NovemberTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.DecemberTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.DecemberTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.JanuaryTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.JanuaryTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.FebruaryTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.FebruaryTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.MarchTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.MarchTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.AprilTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.AprilTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.MayTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.MayTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.JuneTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.JuneTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.JulyTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.JulyTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.AugustTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.AugustTotal.Split('%')[0].Trim()); }
            if (_monthlyTotalValues.SeptemberTotal != null) { _counter++; _total += Convert.ToDecimal(_monthlyTotalValues.SeptemberTotal.Split('%')[0].Trim()); }

            XRTableRow _totalRow = new XRTableRow();
            _totalRow.Font = new Font("Arial", 5, FontStyle.Bold);
            _totalRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            _totalRow.Cells.AddRange(new XRTableCell[] {
            new XRTableCell() { WidthF=130F, BorderWidth = 0 },
            new XRTableCell() { WidthF = 70f , BorderWidth = 0 },
            new XRTableCell() {  WidthF = 70f, BorderWidth = 0 },
            new XRTableCell() {  WidthF = 150f , BorderWidth = 0 },
            new XRTableCell() {  WidthF = 380f, BorderWidth = 0 },
            new XRTableCell() {  WidthF = 90f, BorderWidth = 1, Borders = DevExpress.XtraPrinting.BorderSide.Right },
            new XRTableCell() { Text = _monthlyTotalValues.OctuberTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.NovemberTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.DecemberTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.JanuaryTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.FebruaryTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.MarchTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.AprilTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.MayTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.JuneTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.JulyTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.AugustTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text = _monthlyTotalValues.SeptemberTotal, WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { Text =   String.Format("{0:0.00}%", (_counter == 0 ? 0 : _total / _counter)),  WidthF = 70f , Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular) },
            new XRTableCell() { WidthF = 180f , BorderWidth = 0 }
            });
            Table.Rows.AddRange(new XRTableRow[] { _totalRow });
            Table.EndInit();
        }
        private MonthlyTotalDTO CalculateTotal(List<DashboardReportDTO> ReportList)
        {
            //Get total metric count
            int _totalKPICount = ReportList.Where(_where => _where.IgnoreMetric == false).Count();
            //Get count of red metric 
            int _redKPIOct = ReportList.Where(_where => _where.OctuberBackgroundColor == "#FF0000").Count();
            int _redKPINov = ReportList.Where(_where => _where.NovemberBackgroundColor == "#FF0000").Count();
            int _redKPIDec = ReportList.Where(_where => _where.DecemberBackgroundColor == "#FF0000").Count();
            int _redKPIJan = ReportList.Where(_where => _where.JanuaryBackgroundColor == "#FF0000").Count();
            int _redKPIFeb = ReportList.Where(_where => _where.FebruaryBackgroundColor == "#FF0000").Count();
            int _redKPIMar = ReportList.Where(_where => _where.MarchBackgroundColor == "#FF0000").Count();
            int _redKPIApr = ReportList.Where(_where => _where.AprilBackgroundColor == "#FF0000").Count();
            int _redKPIMay = ReportList.Where(_where => _where.MayBackgroundColor == "#FF0000").Count();
            int _redKPIJun = ReportList.Where(_where => _where.JuneBackgroundColor == "#FF0000").Count();
            int _redKPIJul = ReportList.Where(_where => _where.JulyBackgroundColor == "#FF0000").Count();
            int _redKPIAug = ReportList.Where(_where => _where.AugustBackgroundColor == "#FF0000").Count();
            int _redKPISep = ReportList.Where(_where => _where.SepBackgroundColor == "#FF0000").Count();

            MonthlyTotalDTO _monthlyTotalDTO = new MonthlyTotalDTO();

            //Calculate month total percent
            if (_redKPIOct > 0) { _monthlyTotalDTO.OctuberTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIOct) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.OctuberValue != null)) { _monthlyTotalDTO.OctuberTotal = "100 %"; } }
            if (_redKPINov > 0) { _monthlyTotalDTO.NovemberTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPINov) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.NovemberValue != null)) { _monthlyTotalDTO.NovemberTotal = "100 %"; } }
            if (_redKPIDec > 0) { _monthlyTotalDTO.DecemberTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIDec) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.DecemberValue != null)) { _monthlyTotalDTO.DecemberTotal = "100 %"; } }
            if (_redKPIJan > 0) { _monthlyTotalDTO.JanuaryTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIJan) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.JanuaryValue != null)) { _monthlyTotalDTO.JanuaryTotal = "100 %"; } }
            if (_redKPIFeb > 0) { _monthlyTotalDTO.FebruaryTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIFeb) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.FebruaryValue != null)) { _monthlyTotalDTO.FebruaryTotal = "100 %"; } }
            if (_redKPIMar > 0) { _monthlyTotalDTO.MarchTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIMar) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.MarchValue != null)) { _monthlyTotalDTO.MarchTotal = "100 %"; } }
            if (_redKPIApr > 0) { _monthlyTotalDTO.AprilTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIApr) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.AprilValue != null)) { _monthlyTotalDTO.AprilTotal = "100 %"; } }
            if (_redKPIMay > 0) { _monthlyTotalDTO.MayTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIMay) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.MayValue != null)) { _monthlyTotalDTO.MayTotal = "100 %"; } }
            if (_redKPIJun > 0) { _monthlyTotalDTO.JuneTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIJun) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.JuneValue != null)) { _monthlyTotalDTO.JuneTotal = "100 %"; } }
            if (_redKPIJul > 0) { _monthlyTotalDTO.JulyTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIJul) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.JulyValue != null)) { _monthlyTotalDTO.JulyTotal = "100 %"; } }
            if (_redKPIAug > 0) { _monthlyTotalDTO.AugustTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPIAug) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.AugustValue != null)) { _monthlyTotalDTO.AugustTotal = "100 %"; } }
            if (_redKPISep > 0) { _monthlyTotalDTO.SeptemberTotal = String.Format("{0:0.00} %", (100 - ((Convert.ToDecimal(_redKPISep) * 100) / Convert.ToDecimal(_totalKPICount)))); } else { if (ReportList.All(_where => _where.SepValue != null)) { _monthlyTotalDTO.SeptemberTotal = "100 %"; } }


            return _monthlyTotalDTO;
        }
        private List<DashboardReportDTO> ProcessReportDataSource(List<Dashboard_KPIDTO> ReportDataSourceObjectList)
        {

            List<DashboardReportDTO> _reportDataSourceList = new List<DashboardReportDTO>();
            foreach (var _reportDataSourceObject in ReportDataSourceObjectList)
            {
                if (_reportDataSourceObject.DashboardReportList.Count() > 0)
                {
                    _reportDataSourceList.AddRange(_reportDataSourceObject.DashboardReportList);
                }
            }

            return _reportDataSourceList;
        }
        private XRTable CreateTable(int FiscalYear)
        {
            XRTable _table = new XRTable();
            _table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            _table.BeginInit();
            _table.SizeF = new SizeF(1000f, 30f);
            XRTableRow _rowHeader = new XRTableRow();
            _rowHeader.BackColor = ColorTranslator.FromHtml("#C0C0C0");
            _rowHeader.Font = new Font(this.Font.FontFamily, 6, FontStyle.Bold);
            _rowHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            _rowHeader.Cells.AddRange(new XRTableCell[] {
                new XRTableCell() { Text = "TQC", WidthF=130F},
                new XRTableCell() { Text = "Department", WidthF=150f},
                new XRTableCell() { Text = "KPI (Key Process Indicator)", WidthF=380f},
                new XRTableCell() { Text = String.Format("FY {0} Goal",FiscalYear), WidthF=90f},
                new XRTableCell() { Text = "Apr", WidthF = 50f},
                new XRTableCell() { Text = "May" , WidthF = 50f},
                new XRTableCell() { Text = "Jun" , WidthF = 50f},
                new XRTableCell() { Text = "Jul", WidthF = 50f},
                new XRTableCell() { Text = "Aug" , WidthF = 50f},
                new XRTableCell() { Text = "Sep", WidthF = 50f},
                new XRTableCell() { Text = "Oct", WidthF = 50f},
                new XRTableCell() { Text = "Nov", WidthF = 50f},
                new XRTableCell() { Text = "Dec", WidthF = 50f},
                new XRTableCell() { Text = "Jan", WidthF = 50f},
                new XRTableCell() { Text = "Feb" , WidthF = 50f},
                new XRTableCell() { Text = "Mar", WidthF = 50f},
                new XRTableCell() { Text = String.Format("FY {0} Totals",FiscalYear),  WidthF=70f},
                new XRTableCell() { Text = "Owner", WidthF=180f},
            });
            _table.Rows.AddRange(new XRTableRow[] { _rowHeader });
            _table.EndInit();
            Detail.Controls.Add(_table);
            return _table;
        }
        private void ProcessDashboardInformation(List<DashboardReportDTO> DashboardInformationList, XRTable Table, Dashboard_KPIDTO MainDataSource)
        {
            //Special counter to only create one column with propertie rowspan per Category
            int _qualityCount = 0, _costCount = 0, _deliveryCount = 0, _safetyCount = 0, _moralCount = 0, _environmentCount = 0;

            foreach (DashboardReportDTO _elementInList in DashboardInformationList)
            {
                //Quality
                if (_elementInList.DashboardCategoryID == (int)DashboardCategory_Enum.Quality)
                {
                    if (_qualityCount == 0)
                    {
                        Table = CreateTableRowWithRowSpan(Table, DashboardInformationList.Where(_where => _where.DashboardCategoryID == (int)DashboardCategory_Enum.Quality).Count(), _elementInList, MainDataSource);
                        _qualityCount++;
                    }
                    else { Table = CreateTableRow(Table, _elementInList, MainDataSource); }
                }
                // Safety
                else if (_elementInList.DashboardCategoryID == (int)DashboardCategory_Enum.Safety)
                {
                    if (_safetyCount == 0)
                    {
                        Table = CreateTableRowWithRowSpan(Table, DashboardInformationList.Where(_where => _where.DashboardCategoryID == (int)DashboardCategory_Enum.Safety).Count(), _elementInList, MainDataSource);
                        _safetyCount++;
                    }
                    else { Table = CreateTableRow(Table, _elementInList, MainDataSource); }
                }
                // Delivery
                else if (_elementInList.DashboardCategoryID == (int)DashboardCategory_Enum.Delivery)
                {
                    if (_deliveryCount == 0)
                    {
                        Table = CreateTableRowWithRowSpan(Table, DashboardInformationList.Where(_where => _where.DashboardCategoryID == (int)DashboardCategory_Enum.Delivery).Count(), _elementInList, MainDataSource);
                        _deliveryCount++;
                    }
                    else { Table = CreateTableRow(Table, _elementInList, MainDataSource); }
                }
                //Cost
                else if (_elementInList.DashboardCategoryID == (int)DashboardCategory_Enum.Cost)
                {
                    if (_costCount == 0)
                    {
                        Table = CreateTableRowWithRowSpan(Table, DashboardInformationList.Where(_where => _where.DashboardCategoryID == (int)DashboardCategory_Enum.Cost).Count(), _elementInList, MainDataSource);
                        _costCount++;
                    }
                    else { Table = CreateTableRow(Table, _elementInList, MainDataSource); }
                }


                // Moral
                else if (_elementInList.DashboardCategoryID == (int)DashboardCategory_Enum.Moral)
                {
                    if (_moralCount == 0)
                    {
                        Table = CreateTableRowWithRowSpan(Table, DashboardInformationList.Where(_where => _where.DashboardCategoryID == (int)DashboardCategory_Enum.Moral).Count(), _elementInList, MainDataSource);
                        _moralCount++;
                    }
                    else { Table = CreateTableRow(Table, _elementInList, MainDataSource); }
                }
                //Environment
                else if (_elementInList.DashboardCategoryID == (int)DashboardCategory_Enum.Emvioramental)
                {
                    if (_environmentCount == 0)
                    {
                        Table = CreateTableRowWithRowSpan(Table, DashboardInformationList.Where(_where => _where.DashboardCategoryID == (int)DashboardCategory_Enum.Emvioramental).Count(), _elementInList, MainDataSource);
                        _environmentCount++;
                    }
                    else { Table = CreateTableRow(Table, _elementInList, MainDataSource); }
                }
            }
        }

        private XRTable CreateTableRowWithRowSpan(XRTable Table, int RowSpan, DashboardReportDTO DashboardReportInformation, Dashboard_KPIDTO MainData)
        {
            //Calculate fiscal year total
            string _fiscalYearTotal = CalculateFiscalYearTotal(DashboardReportInformation);
            string _oneYearPastTotal = string.Empty;
            string _twoYearPastTotal = string.Empty;
            string _measurenmentLengends = string.Empty;
            string _goalLengends = string.Empty;
            string _referenceOpacity = string.Empty;
            string _kpigoal = string.Empty;

            //Base on fiscal yeat total set column background color
            string _fiscalYeatBackgroundColor = String.Format("{0}", Dashboard_KPI_Service.SetMetricColumnBackground(DashboardReportInformation.EquivalenceID, _fiscalYearTotal, Convert.ToDecimal(DashboardReportInformation.Goal), DashboardReportInformation.GoalRange));

            if (DashboardReportInformation.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal) { _kpigoal = "≥ " + DashboardReportInformation.Goal; }
            else if (DashboardReportInformation.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal) { _kpigoal = "≤ " + DashboardReportInformation.Goal; }
            else { _kpigoal = "= " + DashboardReportInformation.Goal; }
            var _goalColor = "#85FFFF";            
            _fiscalYeatBackgroundColor = "#" + _fiscalYeatBackgroundColor;            

            XRTableRow _metricRow = new XRTableRow();
            _metricRow.Font = new Font("Arial", 5, FontStyle.Bold);
            _metricRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            _metricRow.Cells.AddRange(new XRTableCell[] {
            // Force new line
            new XRTableCell() { Text = String.Format("{0}          {1}        {2}", DashboardReportInformation.DashboardCategoryLetter, Environment.NewLine, DashboardReportInformation.DashboardCategoryName),
                                RowSpan = RowSpan, Font = new Font("Calibri", 9, FontStyle.Bold), WidthF=130F },
            //new XRTableCell() { Text = (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && _twoYearPastTotal != "") ? _twoYearPastTotal + " %" : _twoYearPastTotal, WidthF = 70f, BackColor = ColorTranslator.FromHtml("#FFFFCC") },
            //new XRTableCell() { Text = (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && _oneYearPastTotal != "") ? _oneYearPastTotal + " %" : _oneYearPastTotal, WidthF = 70f, BackColor = ColorTranslator.FromHtml("#FFFFCC")},
            new XRTableCell() { Text = DashboardReportInformation.DepartmentInteraction, WidthF = 150f, BackColor = ColorTranslator.FromHtml("#FFFFCC") },
            new XRTableCell() { Text = DashboardReportInformation.MetricName+Environment.NewLine+_measurenmentLengends, WidthF = 380f,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.MetricBackgroundColor), WordWrap=true,Multiline=true},
            new XRTableCell() { Text = ((DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent) ? _kpigoal + " %" : _kpigoal)+Environment.NewLine+_goalLengends, WidthF = 90f, BackColor = ColorTranslator.FromHtml(_goalColor), WordWrap=true, Multiline=true },
            new XRTableCell() { Text = (DashboardReportInformation.AprilIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.AprilValue  != null) ? DashboardReportInformation.AprilValue.ToString() + " %" : DashboardReportInformation.AprilValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.AprilBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.MayIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.MayValue  != null) ? DashboardReportInformation.MayValue.ToString() + " %" : DashboardReportInformation.MayValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.MayBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.JuneIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.JuneValue != null) ? DashboardReportInformation.JuneValue.ToString() + " %" : DashboardReportInformation.JuneValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.JuneBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.JulyIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.JulyValue != null) ? DashboardReportInformation.JulyValue.ToString() + " %" : DashboardReportInformation.JulyValue.ToString(), WidthF = 50f , Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.JulyBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.AugustIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.AugustValue != null) ? DashboardReportInformation.AugustValue.ToString() + " %" : DashboardReportInformation.AugustValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.AugustBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.SepIgnoreMetric == true) ? "N/A" :(DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.SepValue != null) ? DashboardReportInformation.SepValue.ToString() + " %" : DashboardReportInformation.SepValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.SepBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.OctuberIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.OctuberValue != null) ? DashboardReportInformation.OctuberValue.ToString() + " %" : DashboardReportInformation.OctuberValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.OctuberBackgroundColor), },
            new XRTableCell() { Text = (DashboardReportInformation.NovemberIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.NovemberValue  != null) ? DashboardReportInformation.NovemberValue.ToString() + " %" : DashboardReportInformation.NovemberValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.NovemberBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.DecemberIgnoreMetric == true) ? "N/A" :(DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.DecemberValue  != null) ? DashboardReportInformation.DecemberValue.ToString() + " %" : DashboardReportInformation.DecemberValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.DecemberBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.JanuaryIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.JanuaryValue  != null) ? DashboardReportInformation.JanuaryValue.ToString() + " %" : DashboardReportInformation.JanuaryValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.JanuaryBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.FebruaryIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.FebruaryValue  != null) ? DashboardReportInformation.FebruaryValue.ToString() + " %" : DashboardReportInformation.FebruaryValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.FebruaryBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.MarchIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent  && DashboardReportInformation.MarchValue  != null) ? DashboardReportInformation.MarchValue.ToString() + " %" : DashboardReportInformation.MarchValue.ToString(),WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.MarchBackgroundColor) },

            new XRTableCell() { Text = (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && !string.IsNullOrEmpty(_fiscalYearTotal)) ? _fiscalYearTotal + " %" :   _fiscalYearTotal, BackColor = ColorTranslator.FromHtml(_fiscalYeatBackgroundColor)  , WidthF = 70f },
            new XRTableCell() { Text = DashboardReportInformation.Responsible, WidthF = 180f },
            });
            Table.Rows.AddRange(new XRTableRow[] { _metricRow });
            Table.EndInit();
            return Table;
        }

        private XRTable CreateTableRow(XRTable Table, DashboardReportDTO DashboardReportInformation, Dashboard_KPIDTO MainData)
        {
            //Calculate fiscal year total
            string _fiscalYearTotal = CalculateFiscalYearTotal(DashboardReportInformation);
            string _oneYearPastTotal = string.Empty;
            string _twoYearPastTotal = string.Empty;
            string _measurenmentLengends = string.Empty;
            string _goalLengends = string.Empty;
            string _referenceOpacity = string.Empty;
            string _kpigoal = string.Empty;


            //Base on fiscal yeat total set column background color
            string _fiscalYeatBackgroundColor = String.Format("{0}", Dashboard_KPI_Service.SetMetricColumnBackground(DashboardReportInformation.EquivalenceID, _fiscalYearTotal, Convert.ToDecimal(DashboardReportInformation.Goal), DashboardReportInformation.GoalRange));

            if (DashboardReportInformation.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal) { _kpigoal = "≥ " + DashboardReportInformation.Goal; }
            else if (DashboardReportInformation.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal) { _kpigoal = "≤ " + DashboardReportInformation.Goal; }
            else { _kpigoal = "= " + DashboardReportInformation.Goal; }
            var _goalColor = "#85FFFF";

            _fiscalYeatBackgroundColor = "#" + _fiscalYeatBackgroundColor;


            XRTableRow _metricRow = new XRTableRow();
            _metricRow.Font = new Font("Arial", 5, FontStyle.Bold);
            _metricRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            _metricRow.Cells.AddRange(new XRTableCell[] {
            new XRTableCell() { /*Empty for Row span*/ Multiline = true, CanGrow = true, WordWrap = true, WidthF=130F },
            //new XRTableCell() { Text = (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && _twoYearPastTotal != "") ? _twoYearPastTotal + " %" : _twoYearPastTotal, WidthF = 70f, BackColor = ColorTranslator.FromHtml("#FFFFCC") },
            //new XRTableCell() { Text = (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && _oneYearPastTotal != "") ? _oneYearPastTotal + " %" : _oneYearPastTotal, WidthF = 70f, BackColor = ColorTranslator.FromHtml("#FFFFCC")},
            new XRTableCell() { Text = DashboardReportInformation.DepartmentInteraction, WidthF = 150f, BackColor = ColorTranslator.FromHtml("#FFFFCC") },
            new XRTableCell() { Text = DashboardReportInformation.MetricName+Environment.NewLine+" "+_measurenmentLengends, WidthF = 380f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.MetricBackgroundColor), WordWrap=true, Multiline=true},
            new XRTableCell() { Text = (DashboardReportInformation.ValueType == ((int)ValueType_Enum.Percent) ? _kpigoal + " %" : _kpigoal)+Environment.NewLine+_goalLengends, WidthF = 90f, BackColor = ColorTranslator.FromHtml(_goalColor), WordWrap=true, Multiline=true },
            new XRTableCell() { Text = (DashboardReportInformation.OctuberIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.OctuberValue != null) ? DashboardReportInformation.OctuberValue.ToString() + " %" : DashboardReportInformation.OctuberValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.OctuberBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.NovemberIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.NovemberValue != null) ? DashboardReportInformation.NovemberValue.ToString() + " %" : DashboardReportInformation.NovemberValue.ToString(), WidthF = 50f,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.NovemberBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.DecemberIgnoreMetric == true) ? "N/A" :(DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.DecemberValue != null) ? DashboardReportInformation.DecemberValue.ToString() + " %" : DashboardReportInformation.DecemberValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.DecemberBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.JanuaryIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.JanuaryValue != null) ? DashboardReportInformation.JanuaryValue.ToString() + " %" : DashboardReportInformation.JanuaryValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.JanuaryBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.FebruaryIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.FebruaryValue != null) ? DashboardReportInformation.FebruaryValue.ToString() + " %" : DashboardReportInformation.FebruaryValue.ToString(), WidthF = 50f ,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.FebruaryBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.MarchIgnoreMetric == true) ? "N/A" :(DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.MarchValue != null) ? DashboardReportInformation.MarchValue.ToString() + " %" : DashboardReportInformation.MarchValue.ToString(), WidthF = 50f,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.MarchBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.AprilIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.AprilValue != null) ? DashboardReportInformation.AprilValue.ToString() + " %" : DashboardReportInformation.AprilValue.ToString(), WidthF = 50f ,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.AprilBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.MayIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.MayValue != null) ? DashboardReportInformation.MayValue.ToString() + " %" : DashboardReportInformation.MayValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.MayBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.JuneIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.JuneValue != null) ? DashboardReportInformation.JuneValue.ToString() + " %" : DashboardReportInformation.JuneValue.ToString(), WidthF = 50f,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.JuneBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.JulyIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.JulyValue != null) ? DashboardReportInformation.JulyValue.ToString() + " %" : DashboardReportInformation.JulyValue.ToString(), WidthF = 50f , Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.JulyBackgroundColor)},
            new XRTableCell() { Text = (DashboardReportInformation.AugustIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.AugustValue != null) ? DashboardReportInformation.AugustValue.ToString() + " %" : DashboardReportInformation.AugustValue.ToString(), WidthF = 50f, Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular),BackColor = ColorTranslator.FromHtml(DashboardReportInformation.AugustBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.SepIgnoreMetric == true) ? "N/A" : (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && DashboardReportInformation.SepValue != null) ? DashboardReportInformation.SepValue.ToString() + " %" : DashboardReportInformation.SepValue.ToString(), WidthF = 50f,Font = new Font(this.Font.FontFamily, 5, FontStyle.Regular), BackColor = ColorTranslator.FromHtml(DashboardReportInformation.SepBackgroundColor) },
            new XRTableCell() { Text = (DashboardReportInformation.ValueType == (int)ValueType_Enum.Percent && !string.IsNullOrEmpty(_fiscalYearTotal)) ? _fiscalYearTotal + " %" :   _fiscalYearTotal, BackColor = ColorTranslator.FromHtml(_fiscalYeatBackgroundColor) , WidthF = 70f },
            new XRTableCell() { Text = DashboardReportInformation.Responsible, WidthF = 180f },
            });
            Table.Rows.AddRange(new XRTableRow[] { _metricRow });
            Table.EndInit();
            return Table;
        }



        private string CalculateFiscalYearTotal(DashboardReportDTO DashboardReportInformation)
        {
            string _fiscalYearTotal = String.Empty;
            //Logic for fiscal year calculation type Average and summary
            if ((DashboardReportInformation.FiscalYearCalculationTypeID == (int)CalculationType_Enum.Summary) || (DashboardReportInformation.FiscalYearCalculationTypeID == (int)CalculationType_Enum.Average))
            {
                Decimal _kpiFiscalYearTotalSummary = 0;
                int _monthCounter = 0;

                if (DashboardReportInformation.JanuaryValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.JanuaryValue); _monthCounter++; }
                if (DashboardReportInformation.FebruaryValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.FebruaryValue); _monthCounter++; }
                if (DashboardReportInformation.MarchValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.MarchValue); _monthCounter++; }
                if (DashboardReportInformation.AprilValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.AprilValue); _monthCounter++; }
                if (DashboardReportInformation.MayValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.MayValue); _monthCounter++; }
                if (DashboardReportInformation.JuneValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.JuneValue); _monthCounter++; }
                if (DashboardReportInformation.JulyValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.JulyValue); _monthCounter++; }
                if (DashboardReportInformation.AugustValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.AugustValue); _monthCounter++; }
                if (DashboardReportInformation.SepValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.SepValue); _monthCounter++; }
                if (DashboardReportInformation.OctuberValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.OctuberValue); _monthCounter++; }
                if (DashboardReportInformation.NovemberValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.NovemberValue); _monthCounter++; }
                if (DashboardReportInformation.DecemberValue != null) { _kpiFiscalYearTotalSummary = _kpiFiscalYearTotalSummary + Convert.ToDecimal(DashboardReportInformation.DecemberValue); _monthCounter++; }

                if (DashboardReportInformation.FiscalYearCalculationTypeID == (int)CalculationType_Enum.Summary)
                {
                    if (_monthCounter > 0) { _fiscalYearTotal = _kpiFiscalYearTotalSummary.ToString(); }
                }
                else if (DashboardReportInformation.FiscalYearCalculationTypeID == (int)CalculationType_Enum.Average)
                {
                    if (_monthCounter > 0) { _fiscalYearTotal = String.Format("{0:0.00}", _kpiFiscalYearTotalSummary / _monthCounter); }
                }
            }
            //Last value
            else if (DashboardReportInformation.FiscalYearCalculationTypeID == (int)CalculationType_Enum.Last_Value)
            {
                int _previousMonth = DateTime.Now.AddMonths(-1).Month;

                switch (_previousMonth)
                {
                    case 1:
                        _fiscalYearTotal = DashboardReportInformation.JanuaryValue.ToString();
                        break;
                    case 2:
                        _fiscalYearTotal = DashboardReportInformation.FebruaryValue.ToString();
                        break;
                    case 3:
                        _fiscalYearTotal = DashboardReportInformation.MarchValue.ToString();
                        break;
                    case 4:
                        _fiscalYearTotal = DashboardReportInformation.AprilValue.ToString();
                        break;
                    case 5:
                        _fiscalYearTotal = DashboardReportInformation.MayValue.ToString();
                        break;
                    case 6:
                        _fiscalYearTotal = DashboardReportInformation.JuneValue.ToString();
                        break;
                    case 7:
                        _fiscalYearTotal = DashboardReportInformation.JulyValue.ToString();
                        break;
                    case 8:
                        _fiscalYearTotal = DashboardReportInformation.AugustValue.ToString();
                        break;
                    case 9:
                        _fiscalYearTotal = DashboardReportInformation.SepValue.ToString();
                        break;
                    case 10:
                        _fiscalYearTotal = DashboardReportInformation.OctuberValue.ToString();
                        break;
                    case 11:
                        _fiscalYearTotal = DashboardReportInformation.NovemberValue.ToString();
                        break;
                    case 12:
                        _fiscalYearTotal = DashboardReportInformation.DecemberValue.ToString();
                        break;
                    default:
                        break;
                }
            }
            return _fiscalYearTotal;
        }

    }
}
