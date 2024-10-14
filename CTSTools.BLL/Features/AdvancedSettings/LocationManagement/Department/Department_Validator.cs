using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Features;
using CTSTools.BLL.Common;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;

public class Department_Validator
{
    public static ValidationResultDTO CreateDepartment_Validation(DepartmentDTO DepartmentDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _ValidationResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (DepartmentDTO.Name == string.Empty)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Name Field Empty", 
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Department)}{nameof(DepartmentDTO.Name)}"
                });
            }
            if (DepartmentDTO.FacilityID == null || DepartmentDTO.FacilityID == 0 )
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Facility Field Empty", 
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Department)}{nameof(DepartmentDTO.FacilityID)}"
                });
            }

            if (DepartmentDTO.AddedByID == null || DepartmentDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateDepartment_Validation(DepartmentDTO DepartmentDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _ValidationResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (DepartmentDTO.ID == null || DepartmentDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                    Data = $"{nameof(Department)}{nameof(DepartmentDTO.ID)}"
                });
            }
            if (DepartmentDTO.Name == string.Empty)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Name Field Empty", 
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Department)}{nameof(DepartmentDTO.Name)}"
                });
            }
            if (DepartmentDTO.FacilityID == null || DepartmentDTO.FacilityID == 0 )
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Facility Field Empty", 
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Department)}{nameof(DepartmentDTO.FacilityID)}"
                });
            }
         
            if (DepartmentDTO.LastUpdateByID == null || DepartmentDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteDepartment_Validation(DepartmentDTO DepartmentDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _ValidationResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (DepartmentDTO.ID == null || DepartmentDTO.ID == 0)
            {
                _ValidationResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Department)}{nameof(DepartmentDTO.ID)}"
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
