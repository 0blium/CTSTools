using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using Elmah;
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
                if (!DashboardLineDTO.GetDashboard_KPIDTO && !DashboardLineDTO.GetKPIDTO && !DashboardLineDTO.GetDashboardCategoryDTO && !DashboardLineDTO.GetDashboardDTO)
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
            var _dashboardKPIDict = new Dictionary<int?, Dashboard_KPIDTO>();
            var _KPIDict = new Dictionary<int?, KPIDTO>();
            var _dashboardcategoryDict = new Dictionary<int?, DashboardCategoryDTO>();
            var _dashboardDict = new Dictionary<int?, DashboardDTO>();

            try
            {
                if (DashboardLineDTO.GetDashboard_KPIDTO)
                {
                    DashboardLineDTO.Dashboard_KPIDTO.Dashboard_KPIIDArray = DashboardLineList.GroupBy(g => g.Dashboard_KPIID)
                            .Select(s => s.Key)
                            .ToArray();

                    _dashboardKPIDict = Dashboard_KPI_Service.GetDashboard_KPIList_Global(DashboardLineDTO.Dashboard_KPIDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (DashboardLineDTO.GetKPIDTO)
                {
                    DashboardLineDTO.KPIDTO.KPIIDArray = DashboardLineList.GroupBy(g => g.KPIID)
                            .Select(s => s.Key)
                            .ToArray();

                    _KPIDict = KPI_Service.GetKPIList_Global(DashboardLineDTO.KPIDTO)
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
                    if (DashboardLineDTO.GetDashboard_KPIDTO && _dashboardKPIDict.ContainsKey(_dashboardlineDTO.Dashboard_KPIID))
                    {
                        _dashboardlineDTO.Dashboard_KPIDTO = _dashboardKPIDict[_dashboardlineDTO.Dashboard_KPIID];
                    }
                    if (DashboardLineDTO.GetKPIDTO && _KPIDict.ContainsKey(_dashboardlineDTO.KPIID))
                    {
                        _dashboardlineDTO.KPIDTO = _KPIDict[_dashboardlineDTO.KPIID];
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
            int FiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardLineDTO.DashboardID }).FirstOrDefault().Year;
            try
            {
                if (FiscalYear > 0 && FiscalYear != null)
                {


                    var _dashboardKPIEntity = new Dashboard_KPIDTO
                    {
                        DashboardID = DashboardLineDTO.DashboardID,

                    };
                    var _dashboardMetriTemplateList = Dashboard_KPI_Service.GetDashboard_KPIList_Global(_dashboardKPIEntity);
                    // hace falta crear el IsActive

                    //validar que esten todas las lineas creades de los meses anteriores al mes actual por cada KPIa
                    foreach (var _dashboardKPI in _dashboardMetriTemplateList)
                    {
                        //var _previousMonthList = GetPreviousMonthForDashboardYear(FiscalYear);
                        var _previousMonthList = GetAllMonthForDashboardYear(FiscalYear);
                        foreach (var _previousMonth in _previousMonthList)
                        {
                            //DashboardLineDTO.DashboardKPIDTO = new DashboardKPIDTO { ID = _dashboardKPI.ID };
                            DashboardLineDTO.Dashboard_KPIID = _dashboardKPI.ID;
                            DashboardLineDTO.DashboardID = _dashboardKPI.DashboardID;
                            DashboardLineDTO.KPIID = _dashboardKPI.KPIID;
                            DashboardLineDTO.DashboardCategoryID = _dashboardKPI.DashboardCategoryID;
                            DashboardLineDTO.FiscalYear = FiscalYear;
                            DashboardLineDTO.Month = _previousMonth;

                            //varificar si existe la linea del mes actual para cada KPIa en el dashboard
                            var _dashboardLineList = GetDashboardLineList_Global(DashboardLineDTO);

                            if (_dashboardLineList.Count == 0)
                            {
                                //Si no existe una linea para el mes actual por ese dashboardKPI crearla.

                                DashboardLineDTO.Goal = KPI_Service.GetKPIList_Global(new KPIDTO { ID = DashboardLineDTO.KPIID }).FirstOrDefault().Goal;

                                _validation_ResultDTO = CreateDashboardLine_Global(DashboardLineDTO);
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

        public static List<int> GetAllMonthForDashboardYear(int DashboardYear)
        {
            DateTime _currentDate = DateTime.Now;
            int currentYear = _currentDate.Year; // Año actual
            int currentMonth = _currentDate.Month; // Mes actual

            List<int> _previousMonthList = new List<int>();

            //if (DashboardYear < currentYear)
            //{
            //    // Si el año ingresado es menor al actual, devolver todos los meses (1 a 12)
            //    _previousMonthList.AddRange(Enumerable.Range(1, 12)); // De enero a diciembre
            //}
            //else if (DashboardYear == currentYear)
            //{
            //    // Si el año ingresado es igual al actual, devolver de enero hasta el mes anterior
            //    _previousMonthList.AddRange(Enumerable.Range(1, currentMonth - 1)); // De enero al mes actual - 1
            //}
            //else
            //{
            //    // Si el año ingresado es mayor al actual, no devolver nada
            //    // _previousMonthList ya está vacío por defecto
            //}

            _previousMonthList.AddRange(Enumerable.Range(1, 12)); // De enero a diciembre
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
                int _fiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardLineDTO.DashboardID }).FirstOrDefault().Year;

                if (_fiscalYear > 0 && _fiscalYear != null)
                {
                    var _dashboardLineDTO = GetDashboardLineList_Global(new DashboardLineDTO { ID = DashboardLineDTO.ID }).FirstOrDefault();



                    _dashboardLineDTO.Value = DashboardLineDTO.Value;
                    _dashboardLineDTO.IsTemporalValue = DashboardLineDTO.IsTemporalValue;
                    _dashboardLineDTO.Comment = DashboardLineDTO.Comment;
                    _dashboardLineDTO.Validated = true;
                    _dashboardLineDTO.ValidatedDate = DateTime.Now;
                    _dashboardLineDTO.ValidatedByID = DashboardLineDTO.LastUpdateByID;
                    _dashboardLineDTO.LastUpdateByID = DashboardLineDTO.LastUpdateByID;
                    //_dashboardLineDTO.ValidateBy= ;
                    _validation_ResultDTO = UpdateDashboardLine_Global(_dashboardLineDTO);


                }
                else
                {
                    _validation_ResultDTO.Result = false;
                    _validation_ResultDTO.Description = "Dashboard year not valid";
                }



            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Error!";
                _validation_ResultDTO.Description = string.Format("There was an error trying to save the record. ");
            }
            return _validation_ResultDTO;
        }

        public static List<DashboardChartDTO> GetDashboard_KPITendence(DashboardLineDTO DashboardLineDTO)
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
                    var dashboardLineDTOList = GetDashboardLineList_Global(new DashboardLineDTO { DashboardID = DashboardLineDTO.DashboardID, KPIID = DashboardLineDTO.KPIID, FiscalYear = _fiscalYear, DashboardCategoryID = DashboardLineDTO.DashboardCategoryID, GetKPIDTO = true });
                    var dashboardLineList = dashboardLineDTOList.Where(x => x.Validated == true && x.Value != null).ToList();
                    if (dashboardLineList.Count() > 0)
                    {
                        foreach (var _dashboardLineDTO in dashboardLineList)
                        {
                            var _tendenceDTO = new DashboardChartDTO();
                            _tendenceDTO.Month = _monthInfo.GetMonthName((int)_dashboardLineDTO.Month);
                            _tendenceDTO.Tendence = (decimal?)_dashboardLineDTO.Value;
                            _tendenceDTO.Goal = (decimal)_dashboardLineDTO.KPIDTO.Goal;
                            _tendenceDTO.KPI = _dashboardLineDTO.KPIDTO.Name;
                            //_tendenceDTO.Order = _dashboardLineDTO.Month;
                            if (_dashboardLineDTO.KPIDTO.EquivalenceDTO.ID == (int)Equivalence_Enum.Greater_Than_Or_Equal) { _tendenceDTO.GoalString = string.Format("&ge; {0}", _dashboardLineDTO.KPIDTO.Goal); }
                            else if (_dashboardLineDTO.KPIDTO.EquivalenceDTO.ID == (int)Equivalence_Enum.Less_Then_Or_Equal) { _tendenceDTO.GoalString = string.Format("&le; {0}", _dashboardLineDTO.KPIDTO.Goal); }
                            else if (_dashboardLineDTO.KPIDTO.EquivalenceDTO.ID == (int)Equivalence_Enum.Equal) { _tendenceDTO.GoalString = string.Format("= {0}", _dashboardLineDTO.KPIDTO.Goal); }


                            switch ((int)_dashboardLineDTO.Month)
                            {
                                case 4:
                                    _tendenceDTO.Order = 1;
                                    break;
                                case 5:
                                    _tendenceDTO.Order = 2;
                                    break;
                                case 6:
                                    _tendenceDTO.Order = 3;
                                    break;
                                case 7:
                                    _tendenceDTO.Order = 4;
                                    break;
                                case 8:
                                    _tendenceDTO.Order = 5;
                                    break;
                                case 9:
                                    _tendenceDTO.Order = 6;
                                    break;
                                case 10:
                                    _tendenceDTO.Order = 7;
                                    break;
                                case 11:
                                    _tendenceDTO.Order = 8;
                                    break;
                                case 12:
                                    _tendenceDTO.Order = 9;
                                    break;
                                case 1:
                                    _tendenceDTO.Order = 10;
                                    break;
                                case 2:
                                    _tendenceDTO.Order = 11;
                                    break;
                                case 3:
                                    _tendenceDTO.Order = 12;
                                    break;
                            }

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
