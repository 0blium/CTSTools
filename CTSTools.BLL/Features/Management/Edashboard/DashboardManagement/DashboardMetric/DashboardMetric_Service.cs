using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetric_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDashboardMetric_Global(DashboardMetricDTO DashboardMetricDTO)
    {
        var _ValidationResultDTO = DashboardMetric_Validator.CreateDashboardMetric_Validation(DashboardMetricDTO);
        if (_ValidationResultDTO.Result)
        {
            DashboardMetricDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DashboardMetric_Repository.CreateDashboardMetric(DashboardMetricDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDashboardMetric_Global(DashboardMetricDTO DashboardMetricDTO)
    {
        var _ValidationResultDTO = DashboardMetric_Validator.UpdateDashboardMetric_Validation(DashboardMetricDTO);
        if (_ValidationResultDTO.Result)
        {
            DashboardMetricDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DashboardMetric_Repository.UpdateDashboardMetric(DashboardMetricDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDashboardMetric_Global(DashboardMetricDTO DashboardMetricDTO)
    {
        var _ValidationResultDTO = DashboardMetric_Validator.DeleteDashboardMetric_Validation(DashboardMetricDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DashboardMetric_Repository.DeleteDashboardMetric(DashboardMetricDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            UpdateOrderConsecutivelyDashboardMetric(new DashboardMetricDTO { DashboardDTO = DashboardMetricDTO.DashboardDTO, DashboardCategoryDTO = DashboardMetricDTO.DashboardCategoryDTO, LastUpdateByID = DashboardMetricDTO.LastUpdateByID });
        }
        return _ValidationResultDTO;
    }
    public static List<DashboardMetricDTO> GetDashboardMetricList_Global(DashboardMetricDTO DashboardMetricDTO, PagedResultDTO<DashboardMetricDTO> PagedResultDTO = null)
    {
        var _dashboardmetricglobalList = new List<DashboardMetricDTO>();
        try
        {
            var _dashboardmetricList = DashboardMetric_Repository.GetDashboardMetricList(DashboardMetricDTO, PagedResultDTO);
            // if DashboardMetric is empty, return list
            if (_dashboardmetricList.Count() == 0)
            {
                _dashboardmetricglobalList = _dashboardmetricList;
                return _dashboardmetricglobalList;
            }
            if (!DashboardMetricDTO.GetStatusDTO && !DashboardMetricDTO.GetDashboardDTO && !DashboardMetricDTO.GetMetricDTO && !DashboardMetricDTO.GetDashboardCategoryDTO)
            {
                _dashboardmetricglobalList = _dashboardmetricList;
                return _dashboardmetricglobalList;
            }
            _dashboardmetricglobalList = GetDashboardMetricRelatedData(DashboardMetricDTO, _dashboardmetricList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardmetricglobalList;
    }



    public static List<DashboardMetricDTO> GetDashboardMetricRelatedData(DashboardMetricDTO DashboardMetricDTO, List<DashboardMetricDTO> DashboardMetricList)
    {
        var _dashboardmetricglobalList = new List<DashboardMetricDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _dashboardDict = new Dictionary<int?, DashboardDTO>();
        var _metricDict = new Dictionary<int?, MetricDTO>();
        var _dashboardcategoryDict = new Dictionary<int?, DashboardCategoryDTO>();

        var _DashboardLineDict = new Dictionary<int?, List<DashboardLineDTO>>();
        try
        {
            if (DashboardMetricDTO.GetStatusDTO)
            {
                DashboardMetricDTO.StatusDTO.StatusIDArray = DashboardMetricList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(DashboardMetricDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardMetricDTO.GetDashboardDTO)
            {
                DashboardMetricDTO.DashboardDTO.DashboardIDArray = DashboardMetricList.GroupBy(g => g.DashboardID)
                        .Select(s => s.Key)
                        .ToArray();

                _dashboardDict = Dashboard_Service.GetDashboardList_Global(DashboardMetricDTO.DashboardDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardMetricDTO.GetMetricDTO)
            {
                DashboardMetricDTO.MetricDTO.MetricIDArray = DashboardMetricList.GroupBy(g => g.MetricID)
                        .Select(s => s.Key)
                        .ToArray();

                _metricDict = Metric_Service.GetMetricList_Global(DashboardMetricDTO.MetricDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardMetricDTO.GetDashboardCategoryDTO)
            {
                DashboardMetricDTO.DashboardCategoryDTO.DashboardCategoryIDArray = DashboardMetricList.GroupBy(g => g.DashboardCategoryID)
                        .Select(s => s.Key)
                        .ToArray();

                _dashboardcategoryDict = DashboardCategory_Service.GetDashboardCategoryList_Global(DashboardMetricDTO.DashboardCategoryDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardMetricDTO.GetDashboardLineList)
            {
                DashboardMetricDTO.DashboardLineDTO.DashboardMetricIDArray = DashboardMetricList.GroupBy(g => g.ID)
                      .Select(s => s.Key)
                      .ToArray();
                _DashboardLineDict = DashboardLine_Service.GetDashboardLineList_Global(DashboardMetricDTO.DashboardLineDTO).GroupBy(g => g.DashboardMetricID)
                                                                     .ToDictionary(keySelector: m => m.Key, elementSelector: m => m.ToList());
            }
            foreach (var _dashboardmetricDTO in DashboardMetricList)
            {
                if (DashboardMetricDTO.GetStatusDTO && _statusDict.ContainsKey(_dashboardmetricDTO.StatusID))
                {
                    _dashboardmetricDTO.StatusDTO = _statusDict[_dashboardmetricDTO.StatusID];
                }
                if (DashboardMetricDTO.GetDashboardDTO && _dashboardDict.ContainsKey(_dashboardmetricDTO.DashboardID))
                {
                    _dashboardmetricDTO.DashboardDTO = _dashboardDict[_dashboardmetricDTO.DashboardID];
                }
                if (DashboardMetricDTO.GetMetricDTO && _metricDict.ContainsKey(_dashboardmetricDTO.MetricID))
                {
                    _dashboardmetricDTO.MetricDTO = _metricDict[_dashboardmetricDTO.MetricID];
                }
                if (DashboardMetricDTO.GetDashboardCategoryDTO && _dashboardcategoryDict.ContainsKey(_dashboardmetricDTO.DashboardCategoryID))
                {
                    _dashboardmetricDTO.DashboardCategoryDTO = _dashboardcategoryDict[_dashboardmetricDTO.DashboardCategoryID];
                }
                if (DashboardMetricDTO.GetDashboardLineList && _DashboardLineDict.ContainsKey(_dashboardmetricDTO.ID))
                {
                    _dashboardmetricDTO.DashboardLineList = _DashboardLineDict[_dashboardmetricDTO.ID];
                }
                _dashboardmetricglobalList.Add(_dashboardmetricDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardmetricglobalList;
    }




    public static int GetDashboardMetricTotalCount(PagedResultDTO<DashboardMetricDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DashboardMetric_Repository.GetDashboardMetricCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic


    public static ValidationResultDTO UpdateDashboardMetricOrder_Global(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = DashboardMetric_Validator.UpdateDashboardMetricOrder_Validation(DashboardMetricDTO);

        if (_validation_ResultDTO.Result)
        {
            _validation_ResultDTO = UpdateOrderDashboardMetric(DashboardMetricDTO);
        }
        if (_validation_ResultDTO.Result)
        {
            _validation_ResultDTO = UpdateOrderConsecutivelyDashboardMetric(DashboardMetricDTO);
        }
        return _validation_ResultDTO;
    }


    public static ValidationResultDTO UpdateOrderDashboardMetric(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();

        var _dashboardMetricList = GetDashboardMetricList_Global(new DashboardMetricDTO { DashboardDTO = DashboardMetricDTO.DashboardDTO, DashboardCategoryDTO = DashboardMetricDTO.DashboardCategoryDTO }).OrderBy(O => O.Order).ToList();
        var _olderDashboardMetricDTO = GetDashboardMetricList_Global(new DashboardMetricDTO { ID = DashboardMetricDTO.ID }).FirstOrDefault();

        if (DashboardMetricDTO.Order < _olderDashboardMetricDTO.Order)
        {

            foreach (var _dashboardMetricDTO in _dashboardMetricList.Where(x => x.Order >= DashboardMetricDTO.Order && x.Order < _olderDashboardMetricDTO.Order))
            {
                _dashboardMetricDTO.LastUpdateByID = DashboardMetricDTO.LastUpdateByID;
                _dashboardMetricDTO.Order++;
                _validation_ResultDTO = UpdateDashboardMetric_Global(_dashboardMetricDTO);
            }
        }
        else if (DashboardMetricDTO.Order > _olderDashboardMetricDTO.Order)
        {
            foreach (var _dashboardMetricDTO in _dashboardMetricList.Where(x => x.Order <= DashboardMetricDTO.Order && x.Order > _olderDashboardMetricDTO.Order))
            {
                _dashboardMetricDTO.LastUpdateByID = DashboardMetricDTO.LastUpdateByID;
                _dashboardMetricDTO.Order--;
                _validation_ResultDTO = UpdateDashboardMetric_Global(_dashboardMetricDTO);
            }

        }
        _olderDashboardMetricDTO.LastUpdateByID = DashboardMetricDTO.LastUpdateByID;
        _olderDashboardMetricDTO.Order = DashboardMetricDTO.Order;
        _validation_ResultDTO = UpdateDashboardMetric_Global(_olderDashboardMetricDTO);

        return _validation_ResultDTO;
    }

    public static ValidationResultDTO UpdateOrderConsecutivelyDashboardMetric(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();

        var _dashboardMetricList = GetDashboardMetricList_Global(new DashboardMetricDTO { DashboardDTO = DashboardMetricDTO.DashboardDTO, DashboardCategoryDTO = DashboardMetricDTO.DashboardCategoryDTO }).OrderBy(O => O.Order);

        var _Order = 1;
        foreach (var _dashboardMetricDTO in _dashboardMetricList)
        {
            _dashboardMetricDTO.Order = _Order;
            _dashboardMetricDTO.LastUpdateByID = DashboardMetricDTO.LastUpdateByID;
            UpdateDashboardMetric_Global(_dashboardMetricDTO);
            _Order++;
        }

        return _validation_ResultDTO;
    }


    public static ValidationResultDTO CreateDashboardMetric_FromMetricList(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO();
        _validation_ResultDTO = DashboardMetric_Validator.CreateDashboardMetric_FromMetricListValidation(DashboardMetricDTO);
        if (_validation_ResultDTO.Result)
        {
            var _metricList = GetDashboardMetricList_Global(new DashboardMetricDTO { DashboardDTO = DashboardMetricDTO.DashboardDTO, DashboardCategoryDTO = DashboardMetricDTO.DashboardCategoryDTO });
            var _order = _metricList.Count > 0 ? _metricList.Max(s => s.Order) : 0;

            foreach (var _metricID in DashboardMetricDTO.MetricIDArray)
            {
                if (_validation_ResultDTO.Result)
                {
                    _order++;
                }
                DashboardMetricDTO.MetricID = _metricID;
                DashboardMetricDTO.Order = _order;
                _validation_ResultDTO = CreateDashboardMetric_Global(DashboardMetricDTO);
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

    public static List<DashboardMetricDTO> GetDashboardMetricWithUI(DashboardMetricDTO DashboardMetricDTO)
    {
        var _dashboardMetricList_Global = new List<DashboardMetricDTO>();
        int _previousMonth = DateTime.Now.AddMonths(-1).Month;
        //int _fiscalYear = CalculateFiscalYear();
        int _fiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardMetricDTO.DashboardID }).FirstOrDefault().Year; ;
        try
        {
            if (_fiscalYear > 0 && _fiscalYear != null)
            {
                var _dashboardLineDTO = new DashboardLineDTO()
                {
                    DashboardDTO = DashboardMetricDTO.DashboardDTO,

                };
                //Validate if Dashboard Line for current month exist              
                var _validation_ResultDTO = DashboardLine_Service.ValidateDashboardLineRecord(_dashboardLineDTO);


                foreach (var _category in new int[] { 1, 2, 3, 4, 5, 6 })
                {
                    DashboardMetricDTO.DashboardLineDTO.FiscalYear = _fiscalYear;
                    DashboardMetricDTO.DashboardCategoryID = _category;
                    DashboardMetricDTO.GetDashboardLineList = true;
                    DashboardMetricDTO.GetDashboardDTO = true;
                    DashboardMetricDTO.GetMetricDTO = true;
                    var _dashboardMetricList = GetDashboardMetricList_Global(DashboardMetricDTO);

                    //Get information for selected dashboard, category and fiscal Year



                    if (_dashboardMetricList.Count > 0)
                    {
                        foreach (var _dashboardMetricDTOResult in _dashboardMetricList)
                        {

                            if (_dashboardMetricDTOResult.DashboardLineList?.Count() > 0)
                            {
                                var _monthlyList = new List<MonthDTO>();
                                //Save each value on Monthly List
                                foreach (var _metricByMonthResult in _dashboardMetricDTOResult.DashboardLineList)
                                {
                                    var _monthlyDTO = new MonthDTO();
                                    _monthlyDTO.ID = _metricByMonthResult.ID;
                                    _monthlyDTO.Month = _metricByMonthResult.Month;
                                    _monthlyDTO.Year = _metricByMonthResult.FiscalYear;
                                    //Validate if goal change
                                    if (_metricByMonthResult.Goal != _dashboardMetricDTOResult.MetricDTO.Goal)
                                    {
                                        _monthlyDTO.FontColor = "00B0F0";
                                        _monthlyDTO.TooltipText = string.Format("Previous Goal {0} <br/> New Goal {1}", _metricByMonthResult.Goal, _dashboardMetricDTOResult.MetricDTO.Goal);
                                    }
                                    else
                                    {
                                        _monthlyDTO.FontColor = "000000";
                                    }

                                    //Logic for child metric
                                    //or diferent to 0
                                    if ((bool)_metricByMonthResult.Validated)
                                    {
                                        _monthlyDTO.MonthlyValue = _metricByMonthResult.Value.ToString();
                                        //Base on Metric goal set column background color

                                        try
                                        {
                                            var _metricBackgroudColor = string.Empty;

                                            if (string.IsNullOrEmpty(_monthlyDTO.MonthlyValue)) { _metricBackgroudColor = "FFFFFF"; }
                                            if (_dashboardMetricDTOResult.MetricDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                                            {
                                                //Color green
                                                if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) == Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal)) { _metricBackgroudColor = "92D050"; }
                                            }
                                            else if (_dashboardMetricDTOResult.MetricDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                                            {
                                                //Color green
                                                if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) >= Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal)) { _metricBackgroudColor = "92D050"; }
                                            }
                                            else if (_dashboardMetricDTOResult.MetricDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                                            {
                                                //Color green
                                                if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) <= Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal)) { _metricBackgroudColor = "92D050"; }
                                            }

                                            if (string.IsNullOrEmpty(_metricBackgroudColor))
                                            {
                                                //Base on goal calculcate goal range
                                                decimal _goalRangeValue = _dashboardMetricDTOResult.MetricDTO?.Goal != 0 ? (Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal) * Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.GoalRangeDTO.Value)) / 100 : Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.GoalRangeDTO.Value);
                                                if (_dashboardMetricDTOResult.MetricDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                                                {
                                                    //If goal needs to be equal to 0 all other values will be red
                                                    if (Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal) == 0 && _goalRangeValue==0)
                                                    {
                                                        _metricBackgroudColor = "FF0000";
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) <= Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal) + _goalRangeValue || Convert.ToDecimal(_monthlyDTO.MonthlyValue) >= Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal) + _goalRangeValue)
                                                        {
                                                            _metricBackgroudColor = "FFFF00";
                                                        }
                                                        else
                                                        {
                                                            _metricBackgroudColor = "FF0000";
                                                        }
                                                    }
                                                }
                                                else if (_dashboardMetricDTOResult.MetricDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                                                {
                                                    if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) >= Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal) - _goalRangeValue)
                                                    {
                                                        _metricBackgroudColor = "FFFF00";
                                                    }
                                                    else
                                                    {
                                                        _metricBackgroudColor = "FF0000";
                                                    }
                                                }
                                                else if (_dashboardMetricDTOResult.MetricDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                                                {
                                                    if (Convert.ToDecimal(_monthlyDTO.MonthlyValue) <= Convert.ToDecimal(_dashboardMetricDTOResult.MetricDTO.Goal) + _goalRangeValue)
                                                    {
                                                        _metricBackgroudColor = "FFFF00";
                                                    }
                                                    else
                                                    {
                                                        _metricBackgroudColor = "FF0000";
                                                    }
                                                }
                                            }
                                            _monthlyDTO.MetricBackgroundColor = _metricBackgroudColor;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw;
                                        }

                                    }
                                    else
                                    {
                                        if (_metricByMonthResult.IgnoreMetric == false) { _monthlyDTO.MonthlyValue = null; _metricByMonthResult.Value = 0; _monthlyDTO.ProvitionalValueColumnProperty = "solid 1px"; } else { _monthlyDTO.MonthlyValue = "N/A"; }
                                    }

                                    //Set column color if value is provitional
                                    if (_metricByMonthResult.IsTemporalValue == true)
                                    {
                                        _monthlyDTO.ProvitionalValueColumnProperty = "dashed 2px #17468F";
                                        _monthlyDTO.TooltipText = "This is a provitional value";
                                    }
                                    else { _monthlyDTO.ProvitionalValueColumnProperty = "solid 1px"; }

                                    // Validate if metric value was validated
                                    if (_metricByMonthResult.Validated == false && _monthlyDTO.MonthlyValue == null && _metricByMonthResult.IgnoreMetric == false)
                                    {
                                        _monthlyDTO.MonthlyValue = null;
                                    }
                                    _metricByMonthResult.MonthValue = _monthlyDTO;
                                    //add month value to linelist
                                    var index = _dashboardMetricDTOResult.DashboardLineList.FindIndex(x => x.ID == _metricByMonthResult.ID);
                                    if (index == -1)
                                    {
                                        _dashboardMetricDTOResult.DashboardLineList[index] = _metricByMonthResult;
                                    }
                                    _monthlyList.Add(_monthlyDTO);
                                }
                                _dashboardMetricDTOResult.MetricDTO.MonthValue = _monthlyList;
                            }

                            _dashboardMetricList_Global.Add(_dashboardMetricDTOResult);
                        }

                    }

                }
            }

            return _dashboardMetricList_Global.OrderBy(_order => _order.DashboardCategoryDTO.ID).ToList().OrderBy(_order => _order.Order).ToList();
            //return _dashboardMetricList_Global;

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    #endregion
}
