using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine
{
    public class DashboardLine_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateDashboardLine_Global(DashboardLineDTO DashboardLineDTO)
        {
            var _ValidationResultDTO = DashboardLine_Validator.CreateDashboardLine_Validation(DashboardLineDTO);
            if (_ValidationResultDTO.Result)
            {
                DashboardLineDTO.AddedDate = DateTime.Now;
                _ValidationResultDTO = DashboardLine_Repository.CreateDashboardLine(DashboardLineDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO UpdateDashboardLine_Global(DashboardLineDTO DashboardLineDTO)
        {
            var _ValidationResultDTO = DashboardLine_Validator.UpdateDashboardLine_Validation(DashboardLineDTO);
            if (_ValidationResultDTO.Result)
            {
                DashboardLineDTO.LastUpdate = DateTime.Now;
                _ValidationResultDTO = DashboardLine_Repository.UpdateDashboardLine(DashboardLineDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO DeleteDashboardLine_Global(DashboardLineDTO DashboardLineDTO)
        {
            var _ValidationResultDTO = DashboardLine_Validator.DeleteDashboardLine_Validation(DashboardLineDTO);
            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = DashboardLine_Repository.DeleteDashboardLine(DashboardLineDTO);
            }
            return _ValidationResultDTO;
        }
        public static List<DashboardLineDTO> GetDashboardLineList_Global(DashboardLineDTO DashboardLineDTO, PagedResultDTO<DashboardLineDTO> PagedResultDTO = null)
        {
            var _dashboardlineglobalList = new List<DashboardLineDTO>();
            try
            {
                var _dashboardlineList = DashboardLine_Repository.GetDashboardLineList(DashboardLineDTO, PagedResultDTO);
                // if DashboardLine is empty, return list
                if (_dashboardlineList.Count() == 0)
                {
                    _dashboardlineglobalList = _dashboardlineList;
                    return _dashboardlineglobalList;
                }
                if (!DashboardLineDTO.GetDashboardMetricDTO && !DashboardLineDTO.GetMetricDTO && !DashboardLineDTO.GetDashboardCategoryDTO && !DashboardLineDTO.GetDashboardDTO)
                {
                    _dashboardlineglobalList = _dashboardlineList;
                    return _dashboardlineglobalList;
                }
                _dashboardlineglobalList = GetDashboardLineRelatedData(DashboardLineDTO, _dashboardlineList);

            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _dashboardlineglobalList;
        }



        public static List<DashboardLineDTO> GetDashboardLineRelatedData(DashboardLineDTO DashboardLineDTO, List<DashboardLineDTO> DashboardLineList)
        {
            var _dashboardlineglobalList = new List<DashboardLineDTO>();
            var _dashboardmetricDict = new Dictionary<int?, DashboardMetricDTO>();
            var _metricDict = new Dictionary<int?, MetricDTO>();
            var _dashboardcategoryDict = new Dictionary<int?, DashboardCategoryDTO>();
            var _dashboardDict = new Dictionary<int?, DashboardDTO>();

            try
            {
                if (DashboardLineDTO.GetDashboardMetricDTO)
                {
                    DashboardLineDTO.DashboardMetricDTO.DashboardMetricIDArray = DashboardLineList.GroupBy(g => g.DashboardMetricID)
                            .Select(s => s.Key)
                            .ToArray();

                    _dashboardmetricDict = DashboardMetric_Service.GetDashboardMetricList_Global(DashboardLineDTO.DashboardMetricDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (DashboardLineDTO.GetMetricDTO)
                {
                    DashboardLineDTO.MetricDTO.MetricIDArray = DashboardLineList.GroupBy(g => g.MetricID)
                            .Select(s => s.Key)
                            .ToArray();

                    _metricDict = Metric_Service.GetMetricList_Global(DashboardLineDTO.MetricDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (DashboardLineDTO.GetDashboardCategoryDTO)
                {
                    DashboardLineDTO.DashboardCategoryDTO.DashboardCategoryIDArray = DashboardLineList.GroupBy(g => g.DashboardCategoryID)
                            .Select(s => s.Key)
                            .ToArray();

                    _dashboardcategoryDict = DashboardCategory_Service.GetDashboardCategoryList_Global(DashboardLineDTO.DashboardCategoryDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (DashboardLineDTO.GetDashboardDTO)
                {
                    DashboardLineDTO.DashboardDTO.DashboardIDArray = DashboardLineList.GroupBy(g => g.DashboardID)
                            .Select(s => s.Key)
                            .ToArray();

                    _dashboardDict = Dashboard_Service.GetDashboardList_Global(DashboardLineDTO.DashboardDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _dashboardlineDTO in DashboardLineList)
                {
                    if (DashboardLineDTO.GetDashboardMetricDTO && _dashboardmetricDict.ContainsKey(_dashboardlineDTO.DashboardMetricID))
                    {
                        _dashboardlineDTO.DashboardMetricDTO = _dashboardmetricDict[_dashboardlineDTO.DashboardMetricID];
                    }
                    if (DashboardLineDTO.GetMetricDTO && _metricDict.ContainsKey(_dashboardlineDTO.MetricID))
                    {
                        _dashboardlineDTO.MetricDTO = _metricDict[_dashboardlineDTO.MetricID];
                    }
                    if (DashboardLineDTO.GetDashboardCategoryDTO && _dashboardcategoryDict.ContainsKey(_dashboardlineDTO.DashboardCategoryID))
                    {
                        _dashboardlineDTO.DashboardCategoryDTO = _dashboardcategoryDict[_dashboardlineDTO.DashboardCategoryID];
                    }
                    if (DashboardLineDTO.GetDashboardDTO && _dashboardDict.ContainsKey(_dashboardlineDTO.DashboardID))
                    {
                        _dashboardlineDTO.DashboardDTO = _dashboardDict[_dashboardlineDTO.DashboardID];
                    }
                    _dashboardlineglobalList.Add(_dashboardlineDTO);
                }

            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _dashboardlineglobalList;
        }




        public static int GetDashboardLineTotalCount(PagedResultDTO<DashboardLineDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = DashboardLine_Repository.GetDashboardLineCount(PagedResultDTO.Filter, PagedResultDTO);
            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return PagedResultDTO.TotalCount;
        }
        #endregion

        #region Business Logic

        public static ValidationResultDTO ValidateDashboardLineRecord(DashboardLineDTO DashboardLineDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO { Description = "Dashboard Template saved" };
            //int PreviousMonth = DateTime.Now.AddMonths(-1).Month;
            //int FiscalYear = CalculateFiscalYear();
            int FiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardLineDTO.DashboardDTO.ID }).FirstOrDefault().Year;
            try
            {
                if (FiscalYear > 0 && FiscalYear != null)
                {
                    foreach (var CategoryID in new int[] { 1, 2, 3, 4, 5, 6 })
                    {

                        var _dashboardMetricEntity = new DashboardMetricDTO
                        {
                            DashboardDTO = { ID = DashboardLineDTO.DashboardDTO.ID },
                            DashboardCategoryDTO = { ID = CategoryID }
                        };
                        var _dashboardMetriTemplateList = DashboardMetric_Service.GetDashboardMetricList_Global(_dashboardMetricEntity);
                        // hace falta crear el IsActive

                        //validar que esten todas las lineas creades de los meses anteriores al mes actual por cada metrica
                        foreach (var _dashboardMetric in _dashboardMetriTemplateList)
                        {
                            var _previousMonthList = GetPreviousMonthForDashboardYear(FiscalYear);
                            foreach (var _previousMonth in _previousMonthList)
                            {
                                DashboardLineDTO.DashboardMetricDTO = new DashboardMetricDTO { ID = _dashboardMetric.ID };
                                DashboardLineDTO.DashboardDTO.ID = _dashboardMetric.DashboardDTO.ID;
                                DashboardLineDTO.MetricDTO.ID = _dashboardMetric.MetricDTO.ID;
                                DashboardLineDTO.DashboardCategoryDTO.ID = CategoryID;
                                DashboardLineDTO.FiscalYear = FiscalYear;
                                DashboardLineDTO.Month = _previousMonth;

                                //varificar si existe la linea del mes actual para cada metrica en el dashboard
                                var _dashboardLineList = GetDashboardLineList_Global(DashboardLineDTO);

                                if (_dashboardLineList.Count == 0)
                                {
                                    //Si no existe una linea para el mes actual por ese dashboardMetric crearla.

                                    DashboardLineDTO.Goal = Metric_Service.GetMetricList_Global(new MetricDTO { ID = DashboardLineDTO.MetricDTO.ID }).FirstOrDefault().Goal;

                                    _validation_ResultDTO = CreateDashboardLine_Global(DashboardLineDTO);
                                }
                            }

                        }

                    }
                }
                else
                {
                    _validation_ResultDTO.Result = false;
                    _validation_ResultDTO.Message = "Dashboard Year Not valid";
                }
                //Validate if exist dashboard line for current Month

                return _validation_ResultDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static int CalculateFiscalYear()
        {
            int _fiscalYear = 0;
            try
            {
                DateTime _currentDate = DateTime.Now;

                DateTime _fiscalYearStartDate = new DateTime(_currentDate.Year, 1, 1, 0, 0, 0);
                DateTime _fiscalYearEndDate = new DateTime(_currentDate.Year, 12, 31, 23, 59, 59);


                if (_currentDate >= _fiscalYearStartDate && _currentDate < _fiscalYearEndDate)
                {
                    _fiscalYear = _currentDate.Year;
                }

                return _fiscalYear;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public static List<int> GetPreviousMonthForCurrentYear()
        //{
        //    DateTime _currentDate = DateTime.Now;
        //    DateTime _fiscalYearStartDate = new DateTime(_currentDate.Year, 1, 1, 0, 0, 0);
        //    List<int> _previousMonthList = new List<int>();


        //    // Iteramos por cada mes anterior al mes actual
        //    for (int i = _fiscalYearStartDate.Month; i < _currentDate.Month; i++)
        //    {
        //        DateTime fechaMesAnterior = _currentDate.AddMonths(-i);
        //        int previousMonth = fechaMesAnterior.Month; // Obtenemos el mes como un valor entero
        //        _previousMonthList.Add(previousMonth);
        //    }

        //    return _previousMonthList;
        //}

        public static List<int> GetPreviousMonthForDashboardYear(int DashboardYear)
        {
            DateTime _currentDate = DateTime.Now;
            int currentYear = _currentDate.Year; // Año actual
            int currentMonth = _currentDate.Month; // Mes actual

            List<int> _previousMonthList = new List<int>();

            if (DashboardYear < currentYear)
            {
                // Si el año ingresado es menor al actual, devolver todos los meses (1 a 12)
                _previousMonthList.AddRange(Enumerable.Range(1, 12)); // De enero a diciembre
            }
            else if (DashboardYear == currentYear)
            {
                // Si el año ingresado es igual al actual, devolver de enero hasta el mes anterior
                _previousMonthList.AddRange(Enumerable.Range(1, currentMonth - 1)); // De enero al mes actual - 1
            }
            else
            {
                // Si el año ingresado es mayor al actual, no devolver nada
                // _previousMonthList ya está vacío por defecto
            }

            return _previousMonthList;
        }

        public static ValidationResultDTO AddMonthlyValue(DashboardLineDTO DashboardLineDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO { Description = "DashboardLine saved" };
            try
            {
                //Validation (Access)


                //Calculate fiscal year
                //int _fiscalYear = CalculateFiscalYear();
                int _fiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardLineDTO.DashboardDTO.ID }).FirstOrDefault().Year;

                if (_fiscalYear > 0 && _fiscalYear != null)
                {
                    var _dashboardLineDTO = GetDashboardLineList_Global(new DashboardLineDTO { ID = DashboardLineDTO.ID }).FirstOrDefault();

                    if (_dashboardLineDTO == null)
                    {
                        _validation_ResultDTO.Result = false;
                        _validation_ResultDTO.Message = "Error";
                        _validation_ResultDTO.Description = "Dashboard line record was not found";
                        return _validation_ResultDTO;
                    }

                    //Add Information to dashboard Line
                    if (_dashboardLineDTO.IgnoreMetric == false)
                    {
                        _dashboardLineDTO.Value = DashboardLineDTO.Value;
                    }
                    _dashboardLineDTO.DashboardMetricDTO = new DashboardMetricDTO { ID = _dashboardLineDTO.DashboardMetricID };
                    _dashboardLineDTO.IsTemporalValue = DashboardLineDTO.IsTemporalValue;
                    _dashboardLineDTO.Comment = DashboardLineDTO.Comment;
                    _dashboardLineDTO.Validated = true;
                    _dashboardLineDTO.ValidatedDate = DateTime.Now;
                    _dashboardLineDTO.ValidatedByDTO = new UserDTO { ID = DashboardLineDTO.LastUpdateByID };
                    _dashboardLineDTO.LastUpdateByID = DashboardLineDTO.LastUpdateByID;
                    //_dashboardLineDTO.ValidateBy= ;
                    _validation_ResultDTO = UpdateDashboardLine_Global(_dashboardLineDTO);



                    //Validate if metric is shared 

                    if (_dashboardLineDTO.MetricDTO.Shared == true && _dashboardLineDTO.MetricDTO.ParenMetricDTO.IsParent == false && (_dashboardLineDTO.MetricDTO.ParenMetricDTO.ID == null || _dashboardLineDTO.MetricDTO.ParenMetricDTO.ID == 0))
                    {
                        var dashboardLinesList = GetDashboardLineList_Global(new DashboardLineDTO { Month = DashboardLineDTO.Month, FiscalYear = _fiscalYear, MetricDTO = { ID = DashboardLineDTO.MetricDTO.ID } });
                        var _metricInOtherDashboard = dashboardLinesList.Where(x => x.DashboardDTO.ID != DashboardLineDTO.DashboardDTO.ID).ToList();

                        if (_metricInOtherDashboard.Count() > 0)
                        {
                            foreach (var _dasboardLineResult in _metricInOtherDashboard)
                            {
                                //Add Information to dashboard Line
                                if (DashboardLineDTO.IgnoreMetric == false)
                                {
                                    _dasboardLineResult.Value = DashboardLineDTO.Value;
                                    _dasboardLineResult.IgnoreMetric = false;
                                }
                                else
                                {
                                    _dasboardLineResult.IgnoreMetric = true;
                                }
                                _dasboardLineResult.DashboardMetricDTO = new DashboardMetricDTO { ID = _dasboardLineResult.DashboardMetricID };
                                _dasboardLineResult.IsTemporalValue = DashboardLineDTO.IsTemporalValue;
                                _dasboardLineResult.Comment = DashboardLineDTO.Comment;
                                _validation_ResultDTO = UpdateDashboardLine_Global(_dasboardLineResult);

                            }
                        }
                    }
                }
                else
                {
                    _validation_ResultDTO.Result = false;
                    _validation_ResultDTO.Description = "Dashboard year not valid";
                }


                return _validation_ResultDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<DashboardChartDTO> GetDashboardMetricTendence(DashboardLineDTO DashboardLineDTO)
        {
            var _tendenceList = new List<DashboardChartDTO>();
            try
            {
                DateTimeFormatInfo _monthInfo = new DateTimeFormatInfo();
                //int _fiscalYear = CalculateFiscalYear();
                int _fiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardLineDTO.DashboardDTO.ID }).FirstOrDefault().Year;
                //    string _month = _monthInfo.GetMonthName(_previousMonth);
                if (_fiscalYear > 0 && _fiscalYear != null)
                {
                    var dashboardLineDTOList = GetDashboardLineList_Global(new DashboardLineDTO { DashboardDTO = { ID = DashboardLineDTO.DashboardDTO.ID }, MetricDTO = { ID = DashboardLineDTO.MetricDTO.ID }, FiscalYear = _fiscalYear, DashboardCategoryDTO = { ID = DashboardLineDTO.DashboardCategoryDTO.ID }, GetMetricDTO = true });
                    var dashboardLineList = dashboardLineDTOList.Where(x => x.Validated == true && x.Value != null).ToList();
                    if (dashboardLineList.Count() > 0)
                    {
                        foreach (var _dashboardLineDTO in dashboardLineList)
                        {
                            var _tendenceDTO = new DashboardChartDTO();
                            _tendenceDTO.Month = _monthInfo.GetMonthName((int)_dashboardLineDTO.Month);
                            _tendenceDTO.Tendence = (decimal?)_dashboardLineDTO.Value;
                            _tendenceDTO.Goal = (decimal)_dashboardLineDTO.MetricDTO.Goal;
                            _tendenceDTO.Metric = _dashboardLineDTO.MetricDTO.Name;
                            _tendenceDTO.Order = _dashboardLineDTO.Month;
                            if (_dashboardLineDTO.MetricDTO.EquivalenceDTO.ID == (int)Equivalence_Enum.Greater_Than_Or_Equal) { _tendenceDTO.GoalString = string.Format("&ge; {0}", _dashboardLineDTO.MetricDTO.Goal); }
                            else if (_dashboardLineDTO.MetricDTO.EquivalenceDTO.ID == (int)Equivalence_Enum.Less_Then_Or_Equal) { _tendenceDTO.GoalString = string.Format("&le; {0}", _dashboardLineDTO.MetricDTO.Goal); }
                            else if (_dashboardLineDTO.MetricDTO.EquivalenceDTO.ID == (int)Equivalence_Enum.Equal) { _tendenceDTO.GoalString = string.Format("= {0}", _dashboardLineDTO.MetricDTO.Goal); }

                            
                            _tendenceList.Add(_tendenceDTO);
                        }
                    }
                }


                return _tendenceList.OrderBy(_order => _order.Order).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
