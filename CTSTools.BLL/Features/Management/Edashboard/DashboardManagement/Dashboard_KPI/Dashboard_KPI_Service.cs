using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class Dashboard_KPI_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDashboard_KPI_Global(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _ValidationResultDTO = Dashboard_KPI_Validator.CreateDashboard_KPI_Validation(Dashboard_KPIDTO);
        if (_ValidationResultDTO.Result && _ValidationResultDTO.Data == null)
        {
            Dashboard_KPIDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Dashboard_KPI_Repository.CreateDashboard_KPI(Dashboard_KPIDTO);
        }
        else 
        {
            UpdateDashboard_KPI_Global(_ValidationResultDTO.Data);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDashboard_KPI_Global(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _ValidationResultDTO = Dashboard_KPI_Validator.UpdateDashboard_KPI_Validation(Dashboard_KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            Dashboard_KPIDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Dashboard_KPI_Repository.UpdateDashboard_KPI(Dashboard_KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDashboard_KPI_Global(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _ValidationResultDTO = Dashboard_KPI_Validator.DeleteDashboard_KPI_Validation(Dashboard_KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            Dashboard_KPIDTO.IsActive = false;
            _ValidationResultDTO = Dashboard_KPI_Repository.DeleteDashboard_KPI(Dashboard_KPIDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            UpdateOrderConsecutivelyDashboard_KPI(new Dashboard_KPIDTO { DashboardID = Dashboard_KPIDTO.DashboardID, DashboardCategoryDTO = Dashboard_KPIDTO.DashboardCategoryDTO, LastUpdateByID = Dashboard_KPIDTO.LastUpdateByID });
        }
        return _ValidationResultDTO;
    }
    public static List<Dashboard_KPIDTO> GetDashboard_KPIList_Global(Dashboard_KPIDTO Dashboard_KPIDTO, PagedResultDTO<Dashboard_KPIDTO> PagedResultDTO = null)
    {
        var _dashboard_kpiglobalList = new List<Dashboard_KPIDTO>();
        try
        {
            var _dashboard_kpiList = Dashboard_KPI_Repository.GetDashboard_KPIList(Dashboard_KPIDTO, PagedResultDTO);
            // if DashboardKPI is empty, return list
            if (_dashboard_kpiList.Count() == 0)
            {
                _dashboard_kpiglobalList = _dashboard_kpiList;
                return _dashboard_kpiglobalList;
            }
            if (!Dashboard_KPIDTO.GetStatusDTO && !Dashboard_KPIDTO.GetDashboardDTO && !Dashboard_KPIDTO.GetKPIDTO && !Dashboard_KPIDTO.GetDashboardCategoryDTO)
            {
                _dashboard_kpiglobalList = _dashboard_kpiList;
                return _dashboard_kpiglobalList;
            }
            _dashboard_kpiglobalList = GetDashboard_KPIRelatedData(Dashboard_KPIDTO, _dashboard_kpiList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboard_kpiglobalList;
    }



    public static List<Dashboard_KPIDTO> GetDashboard_KPIRelatedData(Dashboard_KPIDTO DashboardKPIDTO, List<Dashboard_KPIDTO> DashboardKPIList)
    {
        var _dashboardkpiglobalList = new List<Dashboard_KPIDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _dashboardDict = new Dictionary<int?, DashboardDTO>();
        var _kpiDict = new Dictionary<int?, KPIDTO>();
        var _dashboardcategoryDict = new Dictionary<int?, DashboardCategoryDTO>();

        var _DashboardLineDict = new Dictionary<int?, List<DashboardLineDTO>>();
        try
        {
            if (DashboardKPIDTO.GetStatusDTO)
            {
                DashboardKPIDTO.StatusDTO.StatusIDArray = DashboardKPIList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(DashboardKPIDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardKPIDTO.GetDashboardDTO)
            {
                DashboardKPIDTO.DashboardDTO.DashboardIDArray = DashboardKPIList.GroupBy(g => g.DashboardID)
                        .Select(s => s.Key)
                        .ToArray();

                _dashboardDict = Dashboard_Service.GetDashboardList_Global(DashboardKPIDTO.DashboardDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardKPIDTO.GetKPIDTO)
            {
                DashboardKPIDTO.KPIDTO.KPIIDArray = DashboardKPIList.GroupBy(g => g.KPIID)
                        .Select(s => s.Key)
                        .ToArray();

                _kpiDict = KPI_Service.GetKPIList_Global(DashboardKPIDTO.KPIDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardKPIDTO.GetDashboardCategoryDTO)
            {
                DashboardKPIDTO.DashboardCategoryDTO.DashboardCategoryIDArray = DashboardKPIList.GroupBy(g => g.DashboardCategoryID)
                        .Select(s => s.Key)
                        .ToArray();

                _dashboardcategoryDict = DashboardCategory_Service.GetDashboardCategoryList_Global(DashboardKPIDTO.DashboardCategoryDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardKPIDTO.GetDashboardLineList)
            {
                DashboardKPIDTO.DashboardLineDTO.Dashboard_KPIIDArray = DashboardKPIList.GroupBy(g => g.ID)
                      .Select(s => s.Key)
                      .ToArray();
                _DashboardLineDict = DashboardLine_Service.GetDashboardLineList_Global(DashboardKPIDTO.DashboardLineDTO).GroupBy(g => g.Dashboard_KPIID)
                                                                     .ToDictionary(keySelector: m => m.Key, elementSelector: m => m.ToList());
            }
            foreach (var _dashboardkpiDTO in DashboardKPIList)
            {
                if (DashboardKPIDTO.GetStatusDTO && _statusDict.ContainsKey(_dashboardkpiDTO.StatusID))
                {
                    _dashboardkpiDTO.StatusDTO = _statusDict[_dashboardkpiDTO.StatusID];
                }
                if (DashboardKPIDTO.GetDashboardDTO && _dashboardDict.ContainsKey(_dashboardkpiDTO.DashboardID))
                {
                    _dashboardkpiDTO.DashboardDTO = _dashboardDict[_dashboardkpiDTO.DashboardID];
                }
                if (DashboardKPIDTO.GetKPIDTO && _kpiDict.ContainsKey(_dashboardkpiDTO.KPIID))
                {
                    _dashboardkpiDTO.KPIDTO = _kpiDict[_dashboardkpiDTO.KPIID];
                }
                if (DashboardKPIDTO.GetDashboardCategoryDTO && _dashboardcategoryDict.ContainsKey(_dashboardkpiDTO.DashboardCategoryID))
                {
                    _dashboardkpiDTO.DashboardCategoryDTO = _dashboardcategoryDict[_dashboardkpiDTO.DashboardCategoryID];
                }
                if (DashboardKPIDTO.GetDashboardLineList && _DashboardLineDict.ContainsKey(_dashboardkpiDTO.ID))
                {
                    _dashboardkpiDTO.DashboardLineList = _DashboardLineDict[_dashboardkpiDTO.ID];
                }
                _dashboardkpiglobalList.Add(_dashboardkpiDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardkpiglobalList;
    }




    public static int GetDashboardKPITotalCount(PagedResultDTO<Dashboard_KPIDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Dashboard_KPI_Repository.GetDashboard_KPICount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic


    public static ValidationResultDTO UpdateDashboard_KPIOrder_Global(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _validation_ResultDTO = Dashboard_KPI_Validator.UpdateDashboard_KPIOrder_Validation(Dashboard_KPIDTO);

        if (_validation_ResultDTO.Result)
        {
            _validation_ResultDTO = UpdateOrderDashboard_KPI(Dashboard_KPIDTO);
        }
        if (_validation_ResultDTO.Result)
        {
            _validation_ResultDTO = UpdateOrderConsecutivelyDashboard_KPI(Dashboard_KPIDTO);
        }
        return _validation_ResultDTO;
    }


    public static ValidationResultDTO UpdateOrderDashboard_KPI(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();

        var _dashboard_KPIList = GetDashboard_KPIList_Global(new Dashboard_KPIDTO { DashboardID = Dashboard_KPIDTO.DashboardID, DashboardCategoryDTO = Dashboard_KPIDTO.DashboardCategoryDTO }).OrderBy(O => O.Order).ToList();
        var _olderDashboard_KPIDTO = GetDashboard_KPIList_Global(new Dashboard_KPIDTO { ID = Dashboard_KPIDTO.ID }).FirstOrDefault();

        if (Dashboard_KPIDTO.Order < _olderDashboard_KPIDTO.Order)
        {

            foreach (var _dashboard_KPIDTO in _dashboard_KPIList.Where(x => x.Order >= Dashboard_KPIDTO.Order && x.Order < _olderDashboard_KPIDTO.Order))
            {
                _dashboard_KPIDTO.LastUpdateByID = Dashboard_KPIDTO.LastUpdateByID;
                _dashboard_KPIDTO.Order++;
                _validation_ResultDTO = UpdateDashboard_KPI_Global(_dashboard_KPIDTO);
            }
        }
        else if (Dashboard_KPIDTO.Order > _olderDashboard_KPIDTO.Order)
        {
            foreach (var _dashboardKPIDTO in _dashboard_KPIList.Where(x => x.Order <= Dashboard_KPIDTO.Order && x.Order > _olderDashboard_KPIDTO.Order))
            {
                _dashboardKPIDTO.LastUpdateByID = Dashboard_KPIDTO.LastUpdateByID;
                _dashboardKPIDTO.Order--;
                _validation_ResultDTO = UpdateDashboard_KPI_Global(_dashboardKPIDTO);
            }

        }
        _olderDashboard_KPIDTO.LastUpdateByID = Dashboard_KPIDTO.LastUpdateByID;
        _olderDashboard_KPIDTO.Order = Dashboard_KPIDTO.Order;
        _validation_ResultDTO = UpdateDashboard_KPI_Global(_olderDashboard_KPIDTO);

        return _validation_ResultDTO;
    }

    public static ValidationResultDTO UpdateOrderConsecutivelyDashboard_KPI(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();

        var _dashboardKPIList = GetDashboard_KPIList_Global(new Dashboard_KPIDTO { DashboardID = DashboardKPIDTO.DashboardID, DashboardCategoryDTO = DashboardKPIDTO.DashboardCategoryDTO, IsActive = true }).OrderBy(O => O.Order);

        var _Order = 1;
        foreach (var _dashboardKPIDTO in _dashboardKPIList)
        {
            _dashboardKPIDTO.Order = _Order;
            _dashboardKPIDTO.LastUpdateByID = DashboardKPIDTO.LastUpdateByID;
            UpdateDashboard_KPI_Global(_dashboardKPIDTO);
            _Order++;
        }

        return _validation_ResultDTO;
    }


    public static ValidationResultDTO CreateDashboard_KPI_FromKPIList(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();
        _validation_ResultDTO = Dashboard_KPI_Validator.CreateDashboard_KPI_FromKPIListValidation(DashboardKPIDTO);
        if (_validation_ResultDTO.Result)
        {
            var _kpiList = GetDashboard_KPIList_Global(new Dashboard_KPIDTO { DashboardID = DashboardKPIDTO.DashboardID, DashboardCategoryDTO = DashboardKPIDTO.DashboardCategoryDTO, IsActive = true });
            var _order = _kpiList.Count > 0 ? _kpiList.Max(s => s.Order) : 0;
            int _iterate = 0;

            foreach (var _kpiID in DashboardKPIDTO.KPIIDArray)
            {
                if (_validation_ResultDTO.Result)
                {
                    _order++;
                    if (_iterate < DashboardKPIDTO.DashboardCategoryIDArray.Length)
                    {
                        DashboardKPIDTO.DashboardCategoryID = DashboardKPIDTO.DashboardCategoryIDArray[_iterate];
                        _iterate++;
                    }
                }
                DashboardKPIDTO.KPIID = _kpiID;
                DashboardKPIDTO.Order = _order;
                DashboardKPIDTO.StatusID = 1;
                _validation_ResultDTO = CreateDashboard_KPI_Global(DashboardKPIDTO);
            }
        }

        return _validation_ResultDTO;
    }

    //part 2

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
            else if (_currentDate >= _fiscalYearStartDate && _currentDate > _fiscalYearEndDate)
            {
                _fiscalYear = _currentDate.Year + 1;
            }
            else
            {
                _fiscalYear = _currentDate.Year - 1;
            }
            return _fiscalYear;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static List<Dashboard_KPIDTO> GetDashboard_KPIWithUI(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _dashboardKPIList_Global = new List<Dashboard_KPIDTO>();
        int _previousMonth = DateTime.Now.AddMonths(-1).Month;
        //int _fiscalYear = CalculateFiscalYear();
        int _fiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardKPIDTO.DashboardID }).FirstOrDefault().Year; ;
        try
        {
            if (_fiscalYear > 0 && _fiscalYear != null)
            {
                var _dashboardLineDTO = new DashboardLineDTO()
                {
                    DashboardDTO = DashboardKPIDTO.DashboardDTO,

                };
                //Validate if Dashboard Line for current month exist              
                var _validation_ResultDTO = DashboardLine_Service.ValidateDashboardLineRecord(_dashboardLineDTO);


                foreach (var _category in new int[] { 1, 2, 3, 4, 5, 6 })
                {
                    DashboardKPIDTO.DashboardLineDTO.FiscalYear = _fiscalYear;
                    DashboardKPIDTO.DashboardCategoryID = _category;
                    DashboardKPIDTO.GetDashboardLineList = true;
                    DashboardKPIDTO.GetDashboardDTO = true;
                    DashboardKPIDTO.GetKPIDTO = true;
                    var _dashboardKPIList = GetDashboard_KPIList_Global(DashboardKPIDTO);

                    //Get information for selected dashboard, category and fiscal Year



                    if (_dashboardKPIList.Count > 0)
                    {
                        foreach (var _dashboard_KPIDTOResult in _dashboardKPIList)
                        {

                            if (_dashboard_KPIDTOResult.DashboardLineList?.Count() > 0)
                            {
                                var _monthlyList = new List<MonthDTO>();
                                //Save each value on Monthly List
                                foreach (var _kpiByMonthResult in _dashboard_KPIDTOResult.DashboardLineList)
                                {
                                    var _monthlyDTO = new MonthDTO();
                                    _monthlyDTO.ID = _kpiByMonthResult.ID;
                                    _monthlyDTO.Month = _kpiByMonthResult.Month;
                                    _monthlyDTO.Year = _kpiByMonthResult.FiscalYear;
                                    //Validate if goal change
                                    if (_kpiByMonthResult.Goal != _dashboard_KPIDTOResult.KPIDTO.Goal)
                                    {
                                        _monthlyDTO.FontColor = "00B0F0";
                                        _monthlyDTO.TooltipText = string.Format("Previous Goal {0} <br/> New Goal {1}", _kpiByMonthResult.Goal, _dashboard_KPIDTOResult.KPIDTO.Goal);
                                    }
                                    else
                                    {
                                        _monthlyDTO.FontColor = "000000";
                                    }

                                    //Logic for child kpi
                                    //or diferent to 0
                                    if ((bool)_kpiByMonthResult.Validated)
                                    {
                                        _monthlyDTO.MonthlyValue = _kpiByMonthResult.Value.ToString();
                                        //Base on KPI goal set column background color

                                        try
                                        {
                                            var _kpiBackgroudColor = string.Empty;

                                            if (string.IsNullOrEmpty(_monthlyDTO.MonthlyValue)) { _kpiBackgroudColor = "FFFFFF"; }
                                            if (_dashboard_KPIDTOResult.KPIDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                                            {
                                                //Color green
                                                if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) == Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal)) { _kpiBackgroudColor = "92D050"; }
                                            }
                                            else if (_dashboard_KPIDTOResult.KPIDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                                            {
                                                //Color green
                                                if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) >= Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal)) { _kpiBackgroudColor = "92D050"; }
                                            }
                                            else if (_dashboard_KPIDTOResult.KPIDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                                            {
                                                //Color green
                                                if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) <= Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal)) { _kpiBackgroudColor = "92D050"; }
                                            }

                                            if (string.IsNullOrEmpty(_kpiBackgroudColor))
                                            {
                                                //Base on goal calculcate goal range
                                                decimal _goalRangeValue = _dashboard_KPIDTOResult.KPIDTO?.Goal != 0 ? (Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal) * Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.GoalRangeDTO.Value)) / 100 : Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.GoalRangeDTO.Value);
                                                if (_dashboard_KPIDTOResult.KPIDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                                                {
                                                    //If goal needs to be equal to 0 all other values will be red
                                                    if (Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal) == 0 && _goalRangeValue==0)
                                                    {
                                                        _kpiBackgroudColor = "FF0000";
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) <= Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal) + _goalRangeValue || Convert.ToDecimal(_monthlyDTO.MonthlyValue) >= Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal) + _goalRangeValue)
                                                        {
                                                            _kpiBackgroudColor = "FFFF00";
                                                        }
                                                        else
                                                        {
                                                            _kpiBackgroudColor = "FF0000";
                                                        }
                                                    }
                                                }
                                                else if (_dashboard_KPIDTOResult.KPIDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                                                {
                                                    if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) >= Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal) - _goalRangeValue)
                                                    {
                                                        _kpiBackgroudColor = "FFFF00";
                                                    }
                                                    else
                                                    {
                                                        _kpiBackgroudColor = "FF0000";
                                                    }
                                                }
                                                else if (_dashboard_KPIDTOResult.KPIDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                                                {
                                                    if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) <= Convert.ToDecimal(_dashboard_KPIDTOResult.KPIDTO.Goal) + _goalRangeValue)
                                                    {
                                                        _kpiBackgroudColor = "FFFF00";
                                                    }
                                                    else
                                                    {
                                                        _kpiBackgroudColor = "FF0000";
                                                    }
                                                }
                                            }
                                            _monthlyDTO.KPIBackgroundColor = _kpiBackgroudColor;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw;
                                        }

                                    }
                                    else
                                    {
                                        if (_kpiByMonthResult.IgnoreKPI == false) { _monthlyDTO.MonthlyValue = null; _kpiByMonthResult.Value = 0; _monthlyDTO.ProvitionalValueColumnProperty = "solid 1px"; } else { _monthlyDTO.MonthlyValue = "N/A"; }
                                    }

                                    //Set column color if value is provitional
                                    if (_kpiByMonthResult.IsTemporalValue == true)
                                    {
                                        _monthlyDTO.ProvitionalValueColumnProperty = "dashed 2px #17468F";
                                        _monthlyDTO.TooltipText = "This is a provitional value";
                                    }
                                    else { _monthlyDTO.ProvitionalValueColumnProperty = "solid 1px"; }

                                    // Validate if kpi value was validated
                                    if (_kpiByMonthResult.Validated == false && _monthlyDTO.MonthlyValue == null && _kpiByMonthResult.IgnoreKPI == false)
                                    {
                                        _monthlyDTO.MonthlyValue = null;
                                    }
                                    _kpiByMonthResult.MonthValue = _monthlyDTO;
                                    //add month value to linelist
                                    var index = _dashboard_KPIDTOResult.DashboardLineList.FindIndex(x => x.ID == _kpiByMonthResult.ID);
                                    if (index == -1)
                                    {
                                        _dashboard_KPIDTOResult.DashboardLineList[index] = _kpiByMonthResult;
                                    }
                                    _monthlyList.Add(_monthlyDTO);
                                }
                                _dashboard_KPIDTOResult.KPIDTO.MonthValue = _monthlyList;
                            }

                            _dashboardKPIList_Global.Add(_dashboard_KPIDTOResult);
                        }

                    }

                }
            }

            return _dashboardKPIList_Global.OrderBy(_order => _order.DashboardCategoryID).ToList().OrderBy(_order => _order.Order).ToList();
            //return _dashboardKPIList_Global;

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    #endregion
}
