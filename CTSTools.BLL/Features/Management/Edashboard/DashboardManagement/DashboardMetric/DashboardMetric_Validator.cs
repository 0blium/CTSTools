using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetric_Validator
{
    public static ValidationResultDTO CreateDashboardMetric_Validation(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            //if (DashboardMetricDTO.StatusDTO.ID == null || DashboardMetricDTO.StatusDTO.ID == 0 )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "Status Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.StatusDTO)}", 
            //    });
            //}
            if (DashboardMetricDTO.DashboardID == null || DashboardMetricDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.DashboardID)}",
                });
            }
            if (DashboardMetricDTO.MetricID == null || DashboardMetricDTO.MetricID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Metric Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.MetricID)}",
                });
            }
            //if (DashboardMetricDTO.DashboardCategoryDTO.ID == null || DashboardMetricDTO.DashboardCategoryDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "DashboardCategory Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.DashboardCategoryDTO)}",
            //    });
            //}
            ////Validate if already exist Record with same Dasboard ,Metric & Dashboard Category
            //if (DashboardMetricDTO.DashboardDTO?.ID != 0 && DashboardMetricDTO.MetricDTO.ID != 0 && DashboardMetricDTO.DashboardCategoryDTO.ID != 0)
            //{
            //    var _dasboardMetricDTO = DashboardMetric_Service.GetDashboardMetricList_Global(
            //        new DashboardMetricDTO
            //        {
            //            DashboardDTO = DashboardMetricDTO.DashboardDTO,
            //            DashboardCategoryDTO = DashboardMetricDTO.DashboardCategoryDTO,
            //            MetricDTO = DashboardMetricDTO.MetricDTO,
            //        }).FirstOrDefault();

            //    if (_dasboardMetricDTO != null)
            //    {
            //        _validation_ResultList.Add(new ValidationResultDTO
            //        {
            //            Result = false,
            //            Message = "KPI already exist in that dashboard",
            //            Description = " Please, verify the  information "
            //        });
            //    }
            //}


            if (DashboardMetricDTO.AddedByID == null || DashboardMetricDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
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
    public static ValidationResultDTO UpdateDashboardMetric_Validation(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardMetricDTO.ID == null || DashboardMetricDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (DashboardMetricDTO.StatusID == null || DashboardMetricDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.StatusDTO)}",
                });
            }
            if (DashboardMetricDTO.DashboardID == null || DashboardMetricDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.DashboardID)}",
                });
            }
            if (DashboardMetricDTO.MetricID == null || DashboardMetricDTO.MetricID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Metric Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.MetricDTO)}",
                });
            }
            if (DashboardMetricDTO.DashboardCategoryID == null || DashboardMetricDTO.DashboardCategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardCategory Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.DashboardCategoryID)}",
                });
            }

            if (DashboardMetricDTO.LastUpdateByID == null || DashboardMetricDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteDashboardMetric_Validation(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardMetricDTO.ID == null || DashboardMetricDTO.ID == 0)
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
    public static ValidationResultDTO CreateDashboardMetric_FromMetricListValidation(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            //if (DashboardMetricDTO.StatusDTO.ID == null || DashboardMetricDTO.StatusDTO.ID == 0 )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "Status Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.StatusDTO)}", 
            //    });
            //}
            if (DashboardMetricDTO.DashboardID == null || DashboardMetricDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.DashboardDTO)}",
                });
            }
            if (DashboardMetricDTO.MetricIDArray == null || DashboardMetricDTO.MetricIDArray?.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI not selected",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.MetricDTO)}",
                });
            }
            //if (DashboardMetricDTO.DashboardCategoryDTO.ID == null || DashboardMetricDTO.DashboardCategoryDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "DashboardCategory Field Empty",
            //        Description = " Please, complete the missing information ",
            //        //Data = $"{nameof(DashboardMetric)}{nameof(DashboardMetricDTO.DashboardCategoryDTO)}",
            //    });
            //}
            
            if (DashboardMetricDTO.AddedByID == null || DashboardMetricDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _dashboardMetricList = DashboardMetric_Service.GetDashboardMetricList_Global(new DashboardMetricDTO { DashboardID = DashboardMetricDTO.DashboardID, DashboardCategoryIDArray = DashboardMetricDTO.DashboardCategoryIDArray, MetricIDArray = DashboardMetricDTO.MetricIDArray });
            if (_dashboardMetricList != null)
            {
                var _dashboardCategoryIDArray = _dashboardMetricList.Select(metric => metric.DashboardCategoryID).ToArray();
                var _metricIDArray = _dashboardMetricList.Select(metric => metric.MetricID).ToArray();
                var _dashboardCategoryIDList = new List<int?>(DashboardMetricDTO.DashboardCategoryIDArray);
                foreach (var DashboardCategoryID in _dashboardCategoryIDArray)
                {
                    int _index = _dashboardCategoryIDList.IndexOf(DashboardCategoryID);
                    if (_index >= 0)
                    {
                        _dashboardCategoryIDList.RemoveAt(_index);
                    }
                }
                DashboardMetricDTO.DashboardCategoryIDArray = _dashboardCategoryIDList.ToArray();
                DashboardMetricDTO.MetricIDArray = DashboardMetricDTO.MetricIDArray.Where(ID => !_metricIDArray.Contains(ID)).ToArray();
            }
            if (DashboardMetricDTO.DashboardCategoryIDArray == null || DashboardMetricDTO.DashboardCategoryIDArray.Length == 0 && (DashboardMetricDTO.MetricIDArray == null || DashboardMetricDTO.MetricIDArray.Length == 0))
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "The KPIs are already saved";
                _validation_ResultDTO.Description = " Please, select other different kpis ";
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

    public static ValidationResultDTO UpdateDashboardMetricOrder_Validation(DashboardMetricDTO DashboardMetricDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            //Validate if the new value of order is different
            var _dashboardMetricDTO = DashboardMetric_Service.GetDashboardMetricList_Global(new DashboardMetricDTO { ID = DashboardMetricDTO.ID }).FirstOrDefault();
            if (_dashboardMetricDTO.Order != DashboardMetricDTO.Order)
            {
                //validate if the new order is a valid order
                var _dashboardMetricList = DashboardMetric_Service.GetDashboardMetricList_Global(new DashboardMetricDTO { DashboardDTO = DashboardMetricDTO.DashboardDTO, DashboardCategoryDTO = DashboardMetricDTO.DashboardCategoryDTO }).OrderBy(O => O.Order);
                var _maxorder = _dashboardMetricList.OrderBy(x => x.Order).Last().Order;
                var _minorder = _dashboardMetricList.OrderBy(x => x.Order).First().Order;
                if (DashboardMetricDTO.Order <= 0 || DashboardMetricDTO.Order < _minorder || DashboardMetricDTO.Order > _maxorder)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Order not valid",
                        Description = " Please, change the order",
                    });
                }
            }
            else
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Order not change",
                    Description = " Please, change the order ",
                });
            }

            if (DashboardMetricDTO.LastUpdateByID == null || DashboardMetricDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
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

}
