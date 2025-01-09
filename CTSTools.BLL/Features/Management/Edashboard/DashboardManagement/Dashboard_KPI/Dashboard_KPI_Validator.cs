using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class Dashboard_KPI_Validator
{
    public static ValidationResultDTO CreateDashboard_KPI_Validation(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            //if (DashboardKPIDTO.StatusDTO.ID == null || DashboardKPIDTO.StatusDTO.ID == 0 )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "Status Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(DashboardKPI)}{nameof(DashboardKPIDTO.StatusDTO)}", 
            //    });
            //}
            if (DashboardKPIDTO.DashboardID == null || DashboardKPIDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard_KPI)}{nameof(DashboardKPIDTO.DashboardID)}",
                });
            }
            if (DashboardKPIDTO.KPIID == null || DashboardKPIDTO.KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard_KPI)}{nameof(DashboardKPIDTO.KPIID)}",
                });
            }
            //if (DashboardKPIDTO.DashboardCategoryDTO.ID == null || DashboardKPIDTO.DashboardCategoryDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "DashboardCategory Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(DashboardKPI)}{nameof(DashboardKPIDTO.DashboardCategoryDTO)}",
            //    });
            //}
            ////Validate if already exist Record with same Dasboard ,KPI & Dashboard Category
            //if (DashboardKPIDTO.DashboardDTO?.ID != 0 && DashboardKPIDTO.KPIDTO.ID != 0 && DashboardKPIDTO.DashboardCategoryDTO.ID != 0)
            //{
            //    var _dasboardKPIDTO = DashboardKPI_Service.GetDashboardKPIList_Global(
            //        new DashboardKPIDTO
            //        {
            //            DashboardDTO = DashboardKPIDTO.DashboardDTO,
            //            DashboardCategoryDTO = DashboardKPIDTO.DashboardCategoryDTO,
            //            KPIDTO = DashboardKPIDTO.KPIDTO,
            //        }).FirstOrDefault();

            //    if (_dasboardKPIDTO != null)
            //    {
            //        _validation_ResultList.Add(new ValidationResultDTO
            //        {
            //            Result = false,
            //            Message = "KPI already exist in that dashboard",
            //            Description = " Please, verify the  information "
            //        });
            //    }
            //}

            var _dashboardKPIDTO = Dashboard_KPI_Service.GetDashboard_KPIList_Global(new Dashboard_KPIDTO { DashboardID = DashboardKPIDTO.DashboardID, KPIID = DashboardKPIDTO.KPIID, IsActive = false }).FirstOrDefault();
            if (_dashboardKPIDTO != null)
            {
                _dashboardKPIDTO.Order = DashboardKPIDTO.Order;
                _dashboardKPIDTO.IsActive = true;
                _validation_ResultDTO.Data = _dashboardKPIDTO;
            }

            if (DashboardKPIDTO.AddedByID == null || DashboardKPIDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateDashboard_KPI_Validation(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardKPIDTO.ID == null || DashboardKPIDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (DashboardKPIDTO.StatusID == null || DashboardKPIDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard_KPI)}{nameof(DashboardKPIDTO.StatusDTO)}",
                });
            }
            if (DashboardKPIDTO.DashboardID == null || DashboardKPIDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard_KPI)}{nameof(DashboardKPIDTO.DashboardID)}",
                });
            }
            if (DashboardKPIDTO.KPIID == null || DashboardKPIDTO.KPIID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard_KPI)}{nameof(DashboardKPIDTO.KPIDTO)}",
                });
            }
            if (DashboardKPIDTO.DashboardCategoryID == null || DashboardKPIDTO.DashboardCategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardCategory Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard_KPI)}{nameof(DashboardKPIDTO.DashboardCategoryID)}",
                });
            }

            if (DashboardKPIDTO.LastUpdateByID == null || DashboardKPIDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteDashboard_KPI_Validation(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardKPIDTO.ID == null || DashboardKPIDTO.ID == 0)
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
    public static ValidationResultDTO CreateDashboard_KPI_FromKPIListValidation(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            //if (DashboardKPIDTO.StatusDTO.ID == null || DashboardKPIDTO.StatusDTO.ID == 0 )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "Status Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(DashboardKPI)}{nameof(DashboardKPIDTO.StatusDTO)}", 
            //    });
            //}
            if (Dashboard_KPIDTO.DashboardID == null || Dashboard_KPIDTO.DashboardID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Dashboard Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardKPI)}{nameof(DashboardKPIDTO.DashboardDTO)}",
                });
            }
            if (Dashboard_KPIDTO.KPIIDArray == null || Dashboard_KPIDTO.KPIIDArray?.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "KPI not selected",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardKPI)}{nameof(DashboardKPIDTO.KPIDTO)}",
                });
            }
            //if (DashboardKPIDTO.DashboardCategoryDTO.ID == null || DashboardKPIDTO.DashboardCategoryDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "DashboardCategory Field Empty",
            //        Description = " Please, complete the missing information ",
            //        //Data = $"{nameof(DashboardKPI)}{nameof(DashboardKPIDTO.DashboardCategoryDTO)}",
            //    });
            //}
            
            if (Dashboard_KPIDTO.AddedByID == null || Dashboard_KPIDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _dashboardKPIList = Dashboard_KPI_Service.GetDashboard_KPIList_Global(new Dashboard_KPIDTO { DashboardID = Dashboard_KPIDTO.DashboardID, DashboardCategoryIDArray = Dashboard_KPIDTO.DashboardCategoryIDArray, KPIIDArray = Dashboard_KPIDTO.KPIIDArray, IsActive = true});
            if (_dashboardKPIList != null)
            {
                var _dashboardCategoryIDArray = _dashboardKPIList.Select(metric => metric.DashboardCategoryID).ToArray();
                var _metricIDArray = _dashboardKPIList.Select(metric => metric.KPIID).ToArray();
                var _dashboardCategoryIDList = new List<int?>(Dashboard_KPIDTO.DashboardCategoryIDArray);
                foreach (var DashboardCategoryID in _dashboardCategoryIDArray)
                {
                    int _index = _dashboardCategoryIDList.IndexOf(DashboardCategoryID);
                    if (_index >= 0)
                    {
                        _dashboardCategoryIDList.RemoveAt(_index);
                    }
                }
                Dashboard_KPIDTO.DashboardCategoryIDArray = _dashboardCategoryIDList.ToArray();
                Dashboard_KPIDTO.KPIIDArray = Dashboard_KPIDTO.KPIIDArray.Where(ID => !_metricIDArray.Contains(ID)).ToArray();
            }
            if (Dashboard_KPIDTO.DashboardCategoryIDArray == null || Dashboard_KPIDTO.DashboardCategoryIDArray.Length == 0 && (Dashboard_KPIDTO.KPIIDArray == null || Dashboard_KPIDTO.KPIIDArray.Length == 0))
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

    public static ValidationResultDTO UpdateDashboard_KPIOrder_Validation(Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            //Validate if the new value of order is different
            var _dashboardKPIDTO = Dashboard_KPI_Service.GetDashboard_KPIList_Global(new Dashboard_KPIDTO { ID = DashboardKPIDTO.ID }).FirstOrDefault();
            if (_dashboardKPIDTO.Order != DashboardKPIDTO.Order)
            {
                //validate if the new order is a valid order
                var _dashboardKPIList = Dashboard_KPI_Service.GetDashboard_KPIList_Global(new Dashboard_KPIDTO { DashboardID = DashboardKPIDTO.DashboardID, DashboardCategoryDTO = DashboardKPIDTO.DashboardCategoryDTO }).OrderBy(O => O.Order);
                var _maxorder = _dashboardKPIList.OrderBy(x => x.Order).Last().Order;
                var _minorder = _dashboardKPIList.OrderBy(x => x.Order).First().Order;
                if (DashboardKPIDTO.Order <= 0 || DashboardKPIDTO.Order < _minorder || DashboardKPIDTO.Order > _maxorder)
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

            if (DashboardKPIDTO.LastUpdateByID == null || DashboardKPIDTO.LastUpdateByID == 0)
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
