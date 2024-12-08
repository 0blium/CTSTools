using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;

public class Dashboard_Validator
{
    public static ValidationResultDTO CreateDashboard_Validation(DashboardDTO DashboardDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(DashboardDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.Name)}",
                });
            }
            if (string.IsNullOrEmpty(DashboardDTO.Revision))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Revision Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.Revision)}",
                });
            }
            if (DashboardDTO.Year == null || DashboardDTO.Year == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Year Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.Year)}DTO",
                });
            }
            else if (DashboardDTO.Year > DateTime.Now.Year)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Year Not valid",
                    Description = "The year cannot be greater than the current year",
                });
            }
           
            if (DashboardDTO.DepartmentDTO.ID == null || DashboardDTO.DepartmentDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.DepartmentDTO)}",
                });
            }
            if (DashboardDTO.LevelDTO.ID == null || DashboardDTO.LevelDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Level Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.LevelDTO)}",
                });
            }
           


            if (DashboardDTO.AddedByID == null || DashboardDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateDashboard_Validation(DashboardDTO DashboardDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardDTO.ID == null || DashboardDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(DashboardDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.Name)}",
                });
            }
            if (string.IsNullOrEmpty(DashboardDTO.Revision))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Revision Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.Revision)}",
                });
            }
            if (DashboardDTO.OwnerDTO.ID == null || DashboardDTO.OwnerDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Owner Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.OwnerDTO)}",
                });
            }
            if (DashboardDTO.Year == null || DashboardDTO.Year == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Year Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.Year)}DTO",
                });
            }
            else if (DashboardDTO.Year > DateTime.Now.Year)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Year Not valid",
                    Description = "The year cannot be greater than the current year",
                });
            }
            if (DashboardDTO.DepartmentDTO.ID == null || DashboardDTO.DepartmentDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.DepartmentDTO)}",
                });
            }
            if (DashboardDTO.LevelDTO.ID == null || DashboardDTO.LevelDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Level Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.LevelDTO)}",
                });
            }
            //if (DashboardDTO.StatusDTO.ID == null || DashboardDTO.StatusDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Status Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(Dashboard)}{nameof(DashboardDTO.StatusDTO)}",
            //    });
            //}

            if (DashboardDTO.LastUpdateByID == null || DashboardDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteDashboard_Validation(DashboardDTO DashboardDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DashboardDTO.ID == null || DashboardDTO.ID == 0)
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
