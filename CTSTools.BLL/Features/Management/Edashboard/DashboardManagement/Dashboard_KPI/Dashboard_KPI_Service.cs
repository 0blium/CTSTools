using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Report;
using CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using Elmah;
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
            if (!Dashboard_KPIDTO.GetStatusDTO && !Dashboard_KPIDTO.GetDashboardDTO && !Dashboard_KPIDTO.GetKPIDTO && !Dashboard_KPIDTO.GetDashboardCategoryDTO && !Dashboard_KPIDTO.GetDashboardLineList)
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
        if (_validation_ResultDTO.Result)
        {
            DashboardLine_Service.ValidateDashboardLineRecord(new DashboardLineDTO { DashboardID = DashboardKPIDTO.DashboardID });
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
                    DashboardID = DashboardKPIDTO.DashboardID,

                };


                DashboardKPIDTO.DashboardLineDTO.FiscalYear = _fiscalYear;
                //DashboardKPIDTO.GetDashboardLineList = true;
                DashboardKPIDTO.GetDashboardDTO = true;
                DashboardKPIDTO.GetKPIDTO = true;
                DashboardKPIDTO.KPIDTO.GetUnitOfMeasureDTO = true;
                _dashboardKPIList_Global = GetDashboard_KPIList_Global(DashboardKPIDTO);
                DashboardKPIDTO.DashboardLineDTO.Dashboard_KPIIDArray = _dashboardKPIList_Global.Select(s => s.ID).ToArray();
                var _DashboardLineList = DashboardLine_Service.GetDashboardLineList_Global(DashboardKPIDTO.DashboardLineDTO);
                var _dashboard_kpidict = _dashboardKPIList_Global.ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                var _monthlyList = new List<MonthDTO>();


                if (_dashboardKPIList_Global.Count > 0)
                {

                    if (_DashboardLineList?.Count() > 0)
                    {
                        //Save each value on Monthly List
                        foreach (var _kpiByMonthResult in _DashboardLineList)
                        {
                            var _monthlyDTO = new MonthDTO
                            {
                                ID = _kpiByMonthResult.ID,
                                Month = _kpiByMonthResult.Month,
                                Year = _kpiByMonthResult.FiscalYear,
                                ProvitionalValueColumnProperty = "solid 1px",
                                Dashboard_KPIID = _kpiByMonthResult.Dashboard_KPIID,
                            };

                            _kpiByMonthResult.MonthValue = _monthlyDTO;
                            if ((bool)_kpiByMonthResult.Validated)
                            {
                                _monthlyDTO.MonthlyValue = _kpiByMonthResult.Value.ToString();
                                //Base on KPI goal set column background color

                                try
                                {
                                    var _kpiBackgroudColor = string.Empty;
                                    _kpiBackgroudColor = SetKPIColumnBackground((int)_dashboard_kpidict[_kpiByMonthResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _monthlyDTO.MonthlyValue, Convert.ToDecimal(_dashboard_kpidict[_kpiByMonthResult.Dashboard_KPIID].KPIDTO.Goal), Convert.ToDecimal(_dashboard_kpidict[_kpiByMonthResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue));

                                    _monthlyDTO.KPIBackgroundColor = _kpiBackgroudColor;
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }
                            }
                            else
                            {
                                _monthlyDTO.MonthlyValue = "N/A";
                            }
                        }
                        var _dashboarLineListDict = _DashboardLineList.GroupBy(g => g.Dashboard_KPIID).ToDictionary(keySelector: k => k.Key, elementSelector: m => m.ToList());
                        foreach (var _dashboard_KPIDTO in _dashboardKPIList_Global)
                        {
                            if (_dashboarLineListDict.ContainsKey(_dashboard_KPIDTO.ID))
                            {
                                _dashboard_KPIDTO.DashboardLineList = _dashboarLineListDict[_dashboard_KPIDTO.ID];
                            }
                        }
                    }
                }
            }
            return _dashboardKPIList_Global.OrderBy(_order => _order.DashboardCategoryID).ThenBy(_order => _order.Order).ToList();
            //return _dashboardKPIList_Global;

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public static List<Dashboard_KPIDTO> GetDashboardReportList(int DashboardID)
    {
        var _dashboardKPIList_Global = new List<Dashboard_KPIDTO>();
        int _fiscalYear = Dashboard_Service.GetDashboardList_Global(new DashboardDTO { ID = DashboardID }).FirstOrDefault().Year; ;
        try
        {
            if (_fiscalYear > 0 && _fiscalYear != null)
            {
                var _dashboardLineDTO = new DashboardLineDTO()
                {
                    DashboardID = DashboardID,

                };

                var _mainDashboardKPIDTO = new Dashboard_KPIDTO();
                _mainDashboardKPIDTO.DashboardID = DashboardID;
                _mainDashboardKPIDTO.DashboardLineDTO.FiscalYear = _fiscalYear;
                _mainDashboardKPIDTO.GetDashboardDTO = true;
                _mainDashboardKPIDTO.GetKPIDTO = true;
                _mainDashboardKPIDTO.KPIDTO.GetUnitOfMeasureDTO = true;

                var _dashboard_kpidict = GetDashboard_KPIList_Global(_mainDashboardKPIDTO).ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                _mainDashboardKPIDTO.DashboardLineDTO.Dashboard_KPIIDArray = _dashboard_kpidict.Keys.ToArray();
                var _DashboardLineList = DashboardLine_Service.GetDashboardLineList_Global(_mainDashboardKPIDTO.DashboardLineDTO);
                
                _mainDashboardKPIDTO = GetDashboard_KPIList_Global(_mainDashboardKPIDTO).FirstOrDefault();
                
                var _dashboardReportList = new List<DashboardReportDTO>();
                foreach (var _dasboard_KPIID in _dashboard_kpidict.Keys)
                {
                    _dashboardReportList.Add(new DashboardReportDTO { Dashboard_KPIID = _dasboard_KPIID });
                }
                var _dashboardReportDict = _dashboardReportList.ToDictionary(keySelector: m => m.Dashboard_KPIID, elementSelector: m => m);


                if (_dashboard_kpidict.Keys.Count > 0)
                {

                    if (_DashboardLineList.Count() > 0)
                    {

                        foreach (var _dashboardLineResult in _DashboardLineList)
                        {
                            switch (_dashboardLineResult.Month)
                            {
                                case 1:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JanuaryValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JanuaryGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal);
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JanuaryBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JanuaryValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JanuaryGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JanuaryBackgroundColor = "#FFFFFF";
                                    }

                                    break;
                                case 2:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FebruaryValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FebruaryGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FebruaryBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FebruaryValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FebruaryGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FebruaryBackgroundColor = "#FFFFFF";
                                    }

                                    break;
                                case 3:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MarchValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MarchGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal);
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MarchBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MarchValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MarchGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MarchBackgroundColor = "#FFFFFF";
                                    }

                                    break;
                                case 4:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AprilValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AprilGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal);
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AprilBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AprilValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AprilGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AprilBackgroundColor = "#FFFFFF";
                                    }
                                    break;
                                case 5:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MayValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MayGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MayBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MayValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MayGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].MayBackgroundColor = "#FFFFFF";
                                    }
                                    break;
                                case 6:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JuneValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JuneGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JuneBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JuneValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JuneGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JuneBackgroundColor = "#FFFFFF";
                                    }
                                    break;
                                case 7:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JulyValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JulyGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JulyBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JulyValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JulyGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].JulyBackgroundColor = "#FFFFFF";
                                    }
                                    break;
                                case 8:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AugustValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AugustGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AugustBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AugustValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AugustGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].AugustBackgroundColor = "#FFFFFF";
                                    }
                                    break;
                                case 9:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].SepValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].SepGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].SepBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].SepValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].SepGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].SepBackgroundColor = "#FFFFFF";
                                    }
                                    break;
                                case 10:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].OctuberValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].OctuberGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].OctuberBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].OctuberValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].OctuberGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].OctuberBackgroundColor = "#FFFFFF";
                                    }

                                    break;
                                case 11:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].NovemberValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].NovemberGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].NovemberBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].NovemberValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].NovemberGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].NovemberBackgroundColor = "#FFFFFF";
                                    }

                                    break;
                                case 12:
                                    if (_dashboardLineResult.Value != null && _dashboardLineResult.Validated == true)
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DecemberValue = Convert.ToDecimal(_dashboardLineResult.Value);
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DecemberGoalValue = Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal); ;
                                        //Get column background color
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DecemberBackgroundColor = String.Format("#{0}", SetKPIColumnBackground((int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID, _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DecemberValue.ToString(), _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DecemberGoalValue, Convert.ToDecimal(_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardDTO.GoalRangeValue)));
                                    }
                                    else
                                    {
                                        _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DecemberBackgroundColor = "#FFFFFF";
                                    }

                                    break;
                                default:
                                    break;
                            }

                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].KPIName = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Name;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].KPIID = (int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.ID;

                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].ValueType = (int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.ValueTypeID;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].EquivalenceID = (int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.EquivalenceID;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DashboardCategoryLetter = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardCategoryLetter;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DashboardCategoryName = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardCategoryName;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DashboardCategoryID = (int)_dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].DashboardCategoryID;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].Responsible = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.OwnerName;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].DepartmentInteraction = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.OwnerDepartmentName;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].Order = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].Order;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].GoalRange = Convert.ToDecimal(_mainDashboardKPIDTO.DashboardDTO.GoalRangeValue);
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].Goal = _dashboard_kpidict[_dashboardLineResult.Dashboard_KPIID].KPIDTO.Goal.ToString();
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FiscalYear = _fiscalYear;
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].FiscalYearCalculationTypeID = (int)CalculationType_Enum.Average;/*(int)_dashboard_KPIDTOResult.KPIDTO.FiscalYearCalculationTypeID;*/
                            _dashboardReportDict[_dashboardLineResult.Dashboard_KPIID].KPIBackgroundColor = "#FFFFFF";
                        }
                    }

                }
                _mainDashboardKPIDTO.DashboardReportList.AddRange(_dashboardReportDict.Values.ToList());

                _dashboardKPIList_Global.Add(_mainDashboardKPIDTO);
            }
            return _dashboardKPIList_Global;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static string SetKPIColumnBackground(int MetricEquivalenceID, string MonthlyValue, Decimal MetricGoal, Decimal GoalRange)
    {
        string _metricBackgroundColorHex = String.Empty;
        try
        {
            if (String.IsNullOrEmpty(MonthlyValue)) { return "FFFFFF"; }
            if (MetricEquivalenceID == (int)Equivalence_Enum.Equal)
            {
                //Color green
                if (Convert.ToDecimal(MonthlyValue) == MetricGoal) { _metricBackgroundColorHex = "92D050"; }
            }
            else if (MetricEquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
            {
                //Color green
                if (Convert.ToDecimal(MonthlyValue) >= MetricGoal) { _metricBackgroundColorHex = "92D050"; }
            }
            else if (MetricEquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
            {
                //Color green
                if (Convert.ToDecimal(MonthlyValue) <= MetricGoal) { _metricBackgroundColorHex = "92D050"; }
            }

            if (String.IsNullOrEmpty(_metricBackgroundColorHex))
            {
                //Base on goal calculcate goal range
                //Decimal _goalRangeValue = MetricGoal * GoalRange;
                Decimal _goalRangeValue = MetricGoal != 0 ? ((Convert.ToDecimal(MetricGoal) * Convert.ToDecimal(GoalRange)) / 100) : Convert.ToDecimal(GoalRange); ;
                if (MetricEquivalenceID == (int)Equivalence_Enum.Equal)
                {
                    //If goal needs to be equal to 0 all other values will be red
                    if (MetricGoal == 0)
                    {
                        _metricBackgroundColorHex = "FF0000";
                    }
                    else
                    {
                        if ((Convert.ToDecimal(MonthlyValue) <= (MetricGoal + _goalRangeValue)) && (Convert.ToDecimal(MonthlyValue) >= (MetricGoal - _goalRangeValue)))
                        {
                            _metricBackgroundColorHex = "FFFF00";
                        }
                        else
                        {
                            _metricBackgroundColorHex = "FF0000";
                        }
                    }
                }
                else if (MetricEquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                {
                    if (Convert.ToDecimal(MonthlyValue) >= MetricGoal - _goalRangeValue)
                    {
                        _metricBackgroundColorHex = "FFFF00";
                    }
                    else
                    {
                        _metricBackgroundColorHex = "FF0000";
                    }
                }
                else if (MetricEquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                {
                    if (Convert.ToDecimal(MonthlyValue) <= MetricGoal + _goalRangeValue)
                    {
                        _metricBackgroundColorHex = "FFFF00";
                    }
                    else
                    {
                        _metricBackgroundColorHex = "FF0000";
                    }
                }
            }

            return _metricBackgroundColorHex;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
    }
    #endregion
}
