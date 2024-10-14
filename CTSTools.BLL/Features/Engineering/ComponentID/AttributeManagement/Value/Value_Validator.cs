using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;

public class Value_Validator
{
    public static ValidationResultDTO CreateValue_Validation(ValueDTO ValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (ValueDTO.AttributeID == null || ValueDTO.AttributeID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Attribute Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}", 
                });
            }
            if (string.IsNullOrEmpty(ValueDTO.Name) )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Name Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.Name)}", 
                });
            }
            
            if (ValueDTO.AddedByID == null || ValueDTO.AddedByID == 0)
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
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO UpdateValue_Validation(ValueDTO ValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (ValueDTO.ID == null || ValueDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (ValueDTO.AttributeID == null || ValueDTO.AttributeID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Attribute Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}", 
                });
            }
            if (string.IsNullOrEmpty(ValueDTO.Name) )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Name Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.Name)}", 
                });
            }
            
            //if (ValueDTO.LastUpdateByID == null || ValueDTO.LastUpdateByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "LastUpdateByID Field Empty",
            //        Description = "Please, complete the missing information ",
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
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO DeleteValue_Validation(ValueDTO ValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (ValueDTO.ID == null || ValueDTO.ID == 0)
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
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }

}
