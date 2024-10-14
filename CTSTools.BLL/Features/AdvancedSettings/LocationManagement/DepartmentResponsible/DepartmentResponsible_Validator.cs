using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;

public class DepartmentResponsible_Validator
{
    public static ValidationResultDTO CreateDepartmentResponsible_Validation(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _ValidationResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DepartmentResponsibleDTO.ResponsibleDTO.ID == null || DepartmentResponsibleDTO.ResponsibleDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Responsible Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DepartmentResponsible)}{nameof(DepartmentResponsibleDTO.ResponsibleDTO)}"
                });
            }
            if (DepartmentResponsibleDTO.DepartmentDTO.ID == null || DepartmentResponsibleDTO.DepartmentDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DepartmentResponsibleDTO)}{nameof(DepartmentResponsibleDTO.DepartmentDTO)}"
                });
            }

            if (DepartmentResponsibleDTO.AddedByID == null || DepartmentResponsibleDTO.AddedByID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            // if list contains a error, update main validation result
            if (_ValidationResultList.Count > 0)
            {
                _ValidationResultDTO.Result = false;
                _ValidationResultDTO.Message = "Errors!";
                _ValidationResultDTO.Description = "There is a list of errors";
                _ValidationResultDTO.ValidationResultList = _ValidationResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error!";
            _ValidationResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDepartmentResponsible_Validation(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _ValidationResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DepartmentResponsibleDTO.ID == null || DepartmentResponsibleDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DepartmentResponsible)}{nameof(DepartmentResponsibleDTO.ID)}"
                });
            }
            if (DepartmentResponsibleDTO.ResponsibleDTO.ID == null || DepartmentResponsibleDTO.ResponsibleDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Responsible Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DepartmentResponsible)}{nameof(DepartmentResponsibleDTO.ResponsibleDTO)}"
                });
            }
            if (DepartmentResponsibleDTO.DepartmentDTO.ID == null || DepartmentResponsibleDTO.DepartmentDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DepartmentResponsibleDTO)}{nameof(DepartmentResponsibleDTO.DepartmentDTO)}"
                });
            }

            if (DepartmentResponsibleDTO.LastUpdateByID == null || DepartmentResponsibleDTO.LastUpdateByID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "LastUpdateByID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            // if list contains a error, update main validation result
            if (_ValidationResultList.Count > 0)
            {
                _ValidationResultDTO.Result = false;
                _ValidationResultDTO.Message = "Errors!";
                _ValidationResultDTO.Description = "There is a list of errors";
                _ValidationResultDTO.ValidationResultList = _ValidationResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error!";
            _ValidationResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDepartmentResponsible_Validation(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _ValidationResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DepartmentResponsibleDTO.ID == null || DepartmentResponsibleDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DepartmentResponsible)}{nameof(DepartmentResponsibleDTO.ID)}"
                });
            }
            // if list contains a error, update main validation result
            if (_ValidationResultList.Count > 0)
            {
                _ValidationResultDTO.Result = false;
                _ValidationResultDTO.Message = "Errors!";
                _ValidationResultDTO.Description = "There is a list of errors";
                _ValidationResultDTO.ValidationResultList = _ValidationResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _ValidationResultDTO.Result = false;
            _ValidationResultDTO.Message = "Error!";
            _ValidationResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _ValidationResultDTO;
    }
}
