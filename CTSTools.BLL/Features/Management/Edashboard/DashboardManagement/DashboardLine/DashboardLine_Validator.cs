using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLine_Validator
{
    public static ValidationResultDTO CreateDashboardLine_Validation(DashboardLineDTO DashboardLineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardLineDTO.Dashboard_KPIID == null || DashboardLineDTO.Dashboard_KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardKPI Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.Dashboard_KPIDTO)}",
                });
            }
            if (DashboardLineDTO.KPIID == null || DashboardLineDTO.KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.KPIDTO)}",
                });
            }
            if (DashboardLineDTO.DashboardCategoryID == null || DashboardLineDTO.DashboardCategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardCategory Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DashboardLineDTO.DashboardID == null || DashboardLineDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            
            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO UpdateDashboardLine_Validation(DashboardLineDTO DashboardLineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardLineDTO.ID == null || DashboardLineDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            else
            {
                //Validate Month 
                if (ValidateMonth(DashboardLineDTO).Result == false)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Month not available",
                        Description = "This month's date has not yet been met. ",
                    });
                }
            }
            if (DashboardLineDTO.Dashboard_KPIID == null || DashboardLineDTO.Dashboard_KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardKPI Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DashboardLineDTO.KPIID == null || DashboardLineDTO.KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DashboardLineDTO.DashboardCategoryID == null || DashboardLineDTO.DashboardCategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardCategory Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DashboardLineDTO.DashboardID == null || DashboardLineDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }



            //Validate Comment by background colors

            var _kpiDTO = new KPIDTO
            {
                ID = DashboardLineDTO.KPIID
            };
            _kpiDTO = KPI_Service.GetKPIList_Global(_kpiDTO).FirstOrDefault();

            //Format Decimals
            if (_kpiDTO.ValueTypeID == (int)ValueType_Enum.Decimal || _kpiDTO.ValueTypeID == (int)ValueType_Enum.Percent)
            {
                DashboardLineDTO.Value = (float)Math.Round(DashboardLineDTO.Value, 2);
            }
            else if (_kpiDTO.ValueTypeID == (int)ValueType_Enum.Absolute)
            {
                DashboardLineDTO.Value = (float)Math.Truncate(DashboardLineDTO.Value);
            }

            var _dashboardDTO = new DashboardDTO
            {
                ID = DashboardLineDTO.DashboardID
            };
            _dashboardDTO = Dashboard_Service.GetDashboardList_Global(_dashboardDTO).FirstOrDefault();

            var _newValueBackgroundColor = Dashboard_KPI_Service.SetKPIColumnBackground((int)_kpiDTO.EquivalenceID, DashboardLineDTO.Value.ToString(), Convert.ToDecimal(_kpiDTO.Goal), Convert.ToDecimal(_dashboardDTO.GoalRangeValue));

            if (_newValueBackgroundColor != "FFFFFF" && _newValueBackgroundColor != "92D050")
            {

                if (string.IsNullOrEmpty(DashboardLineDTO.Comment))
                {
                    if (_newValueBackgroundColor == "FF0000" || ValidatePreviousMonthsLineYellowColor(DashboardLineDTO).Result == false)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Comment Is Required",
                            Description = " Please, complete the missing information "
                        });

                    }
                }

            }

            


            if (DashboardLineDTO.ValidatedByID == null || DashboardLineDTO.ValidatedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ValidatedBy Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.ValidatedByDTO)}",
                });
            }

            if (DashboardLineDTO.LastUpdateByID == null || DashboardLineDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "LastUpdateByID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO DeleteDashboardLine_Validation(DashboardLineDTO DashboardLineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardLineDTO.ID == null || DashboardLineDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }


    public static ValidationResultDTO ValidatePreviousMonthsLineYellowColor(DashboardLineDTO DashboardLineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            var _KPIBackgroudColor = string.Empty;
            var _kpiDTO = new KPIDTO
            {
                ID = DashboardLineDTO.KPIID
            };
            _kpiDTO = KPI_Service.GetKPIList_Global(_kpiDTO).FirstOrDefault();
            var _dashboardLineDTO = new DashboardLineDTO
            {
                DashboardID = DashboardLineDTO.DashboardID,
                KPIID = DashboardLineDTO.KPIID,
                Dashboard_KPIID = DashboardLineDTO.Dashboard_KPIID,
                FiscalYear = DashboardLineDTO.FiscalYear,
                GetDashboardDTO = true
            };
            var previousMonths = new List<int>();

            // Array that maps each month to its previous months
            Dictionary<int, List<int>> mappedMonths = new Dictionary<int, List<int>>()
            {
                 { 4, new List<int>() }, // April has no previous months
                { 5, new List<int> { 4 } }, // May returns only April
                { 6, new List<int> { 5, 4 } }, // June returns May and April
                { 7, new List<int> { 6, 5 } }, // July returns June and May
                { 8, new List<int> { 7, 6 } }, // August returns July and June
                { 9, new List<int> { 8, 7 } }, // September returns August and July
                { 10, new List<int> { 9, 8 } }, // October returns September and August
                { 11, new List<int> { 10, 9 } }, // November returns October and September
                { 12, new List<int> { 11, 10 } }, // December returns November and October
                { 1, new List<int> { 12, 11 } }, // January returns December and November
                { 2, new List<int> { 1, 12 } }, // February returns January and December
                { 3, new List<int> { 2, 1 } } // March returns February and January
            };


            // We only add the corresponding months for the reference month
            if (mappedMonths.ContainsKey(DashboardLineDTO.Month))
            {
                previousMonths.AddRange(mappedMonths[DashboardLineDTO.Month]);
            }

            var _previousMonthLinesList = DashboardLine_Service.GetDashboardLineList_Global(_dashboardLineDTO).Where(s => previousMonths.Contains(s.Month)).ToList();


            foreach (var _dasboardLineDTO in _previousMonthLinesList)
            {
                _KPIBackgroudColor = string.Empty;

                _KPIBackgroudColor = Dashboard_KPI_Service.SetKPIColumnBackground((int)_kpiDTO.EquivalenceID, _dasboardLineDTO.Value.ToString(), Convert.ToDecimal(_kpiDTO.Goal), Convert.ToDecimal(_dasboardLineDTO.DashboardDTO.GoalRangeValue));


                if (_KPIBackgroudColor == "FFFF00")
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "",
                        Description = "",
                    });
                }
            }
            if (_validation_ResultList.Count > 1)
            {
                _validation_ResultDTO.Result = false;
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }

    public static ValidationResultDTO ValidateMonth(DashboardLineDTO DashboardLineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {

            var _dashboardlineDTO = DashboardLine_Service.GetDashboardLineList_Global(new DashboardLineDTO { ID = DashboardLineDTO.ID }).FirstOrDefault();
            var _month = _dashboardlineDTO.Month;
            //evaluate if month is in January to March  beacuase fiscal year is April Year To March from next year
            var _year = (_month >= 1 && _month <= 3) ? (_dashboardlineDTO.FiscalYear + 1) : _dashboardlineDTO.FiscalYear;

            int _lastDay = DateTime.DaysInMonth(_year, _month);
            DateTime _datetoEvaluate = new DateTime(_year, _month, _lastDay, 23, 59, 59);
            DateTime _currentDate = DateTime.Now;
            _validation_ResultDTO.Result = _currentDate > _datetoEvaluate ? true : false;
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }

}
