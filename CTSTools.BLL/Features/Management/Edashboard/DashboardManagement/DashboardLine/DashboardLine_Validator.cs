using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

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
            if (DashboardLineDTO.DashboardMetricID == null || DashboardLineDTO.DashboardMetricID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardMetric Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.DashboardMetricDTO)}",
                });
            }
            if (DashboardLineDTO.MetricID == null || DashboardLineDTO.MetricID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Metric Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.MetricDTO)}",
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
            //if (DashboardLineDTO.IgnoreMetric == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IgnoreMetric Field Empty",
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
            if (DashboardLineDTO.DashboardMetricID == null || DashboardLineDTO.DashboardMetricID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DashboardMetric Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.DashboardMetricDTO)}",
                });
            }
            if (DashboardLineDTO.MetricID == null || DashboardLineDTO.MetricID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Metric Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.MetricDTO)}",
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
            if (string.IsNullOrEmpty(DashboardLineDTO.Comment))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Comment Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(DashboardLine)}{nameof(DashboardLineDTO.Comment)}",
                });
            }
            //if (DashboardLineDTO.IgnoreMetric == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IgnoreMetric Field Empty",
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

}
