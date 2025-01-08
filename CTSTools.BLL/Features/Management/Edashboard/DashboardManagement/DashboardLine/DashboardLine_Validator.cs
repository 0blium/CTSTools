using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.BLL.Features.Management.Edashboard.Settings;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
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
            //if (DashboardLineDTO.IsTemporalValue == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IsTemporalValue Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
            //if (string.IsNullOrEmpty(DashboardLineDTO.Comment))
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Comment Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.Comment)}",
            //    });
            //}
            //if (DashboardLineDTO.IgnoreKPI == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IgnoreKPI Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
            //if (DashboardLineDTO.Validated == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Validated Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
            //if (DashboardLineDTO.ValidatedByDTO.ID == null || DashboardLineDTO.ValidatedByDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "ValidatedBy Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.ValidatedByDTO)}",
            //    });
            //}

            //if (DashboardLineDTO.AddedByID == null || DashboardLineDTO.AddedByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "AddedByID Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
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
            if (DashboardLineDTO.Dashboard_KPIID == null || DashboardLineDTO.Dashboard_KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardKPI Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.DashboardKPIDTO)}",
                });
            }
            if (DashboardLineDTO.KPIID == null || DashboardLineDTO.KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.KPIDTO)}",
                });
            }
            if (DashboardLineDTO.DashboardCategoryID == null || DashboardLineDTO.DashboardCategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardCategory Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.DashboardCategoryDTO)}",
                });
            }
            if (DashboardLineDTO.DashboardID == null || DashboardLineDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.DashboardDTO)}",
                });
            }
            //if (DashboardLineDTO.IsTemporalValue == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IsTemporalValue Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}


            if (ValidatePreviousMonthsLineColor(DashboardLineDTO).Result == false)
            {
                if (string.IsNullOrEmpty(DashboardLineDTO.Comment))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Comment Is Required",
                        Description = " Please, complete the missing information ",
                        //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.Comment)}",
                    });
                }
            }
            
            //if (DashboardLineDTO.IgnoreKPI == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IgnoreKPI Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
            //if (DashboardLineDTO.Validated == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Validated Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
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





    public static ValidationResultDTO ValidatePreviousMonthsLineColor(DashboardLineDTO DashboardLineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            var _KPIBackgroudColor = string.Empty;

            var _KPIDTO = KPI_Service.GetKPIList_Global(new KPIDTO { ID = DashboardLineDTO.KPIID }).FirstOrDefault();
            var _previousMonthLinesList = DashboardLine_Service.GetDashboardLineList_Global(
                new DashboardLineDTO
                {
                    DashboardID = DashboardLineDTO.DashboardID,
                    KPIID = DashboardLineDTO.KPIID,
                    Dashboard_KPIID = DashboardLineDTO.Dashboard_KPIID,
                    FiscalYear = DashboardLineDTO.FiscalYear
                }).Where(s => s.Month >= DashboardLineDTO.Month - 2 && s.Month < DashboardLineDTO.Month).ToList();


            foreach (var _dasboardLineDTO in _previousMonthLinesList)
            {
                _KPIBackgroudColor = string.Empty;
                if (string.IsNullOrEmpty(_dasboardLineDTO.Value.ToString())) { _KPIBackgroudColor = "FFFFFF"; }
                if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                {
                    //Color green
                    if (Convert.ToDecimal(_dasboardLineDTO.Value) == Convert.ToDecimal(_KPIDTO.Goal)) { _KPIBackgroudColor = "92D050"; }
                }
                else if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                {
                    //Color green
                    if (Convert.ToDecimal(_dasboardLineDTO.Value) >= Convert.ToDecimal(_KPIDTO.Goal)) { _KPIBackgroudColor = "92D050"; }
                }
                else if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                {
                    //Color green
                    if (Convert.ToDecimal(_dasboardLineDTO.Value) <= Convert.ToDecimal(_KPIDTO.Goal)) { _KPIBackgroudColor = "92D050"; }
                }

                if (string.IsNullOrEmpty(_KPIBackgroudColor))
                {
                    //Base on goal calculcate goal range
                    decimal _goalRangeValue = _KPIDTO.Goal != 0 ? (Convert.ToDecimal(_KPIDTO.Goal) * Convert.ToDecimal(_KPIDTO.GoalRangeValue)) / 100 : Convert.ToDecimal(_KPIDTO.GoalRangeValue);
                    if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                    {
                        //If goal needs to be equal to 0 all other values will be red
                        if (Convert.ToDecimal(_KPIDTO.Goal) == 0 && _goalRangeValue == 0)
                        {
                            _KPIBackgroudColor = "FF0000";
                        }
                        else
                        {
                            if (Convert.ToDecimal(_dasboardLineDTO.Value) <= Convert.ToDecimal(_KPIDTO.Goal) + _goalRangeValue || Convert.ToDecimal(_dasboardLineDTO.Value) >= Convert.ToDecimal(_KPIDTO.Goal) + _goalRangeValue)
                            {
                                _KPIBackgroudColor = "FFFF00";
                            }
                            else
                            {
                                _KPIBackgroudColor = "FF0000";
                            }
                        }
                    }
                    else if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                    {
                        if (Convert.ToDecimal(_dasboardLineDTO.Value) >= Convert.ToDecimal(_KPIDTO.Goal) - _goalRangeValue)
                        {
                            _KPIBackgroudColor = "FFFF00";
                        }
                        else
                        {
                            _KPIBackgroudColor = "FF0000";
                        }
                    }
                    else if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                    {
                        if (Convert.ToDecimal(_dasboardLineDTO.Value) <= Convert.ToDecimal(_KPIDTO.Goal) + _goalRangeValue)
                        {
                            _KPIBackgroudColor = "FFFF00";
                        }
                        else
                        {
                            _KPIBackgroudColor = "FF0000";
                        }
                    }
                }

                if(_KPIBackgroudColor == "FFFF00" || _KPIBackgroudColor == "FF0000")
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

}
