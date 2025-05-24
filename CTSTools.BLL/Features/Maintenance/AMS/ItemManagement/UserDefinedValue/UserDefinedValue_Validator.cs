using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;

public class UserDefinedValue_Validator
{
    public static ValidationResultDTO CreateUserDefinedValue_Validation(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(UserDefinedValueDTO.Value))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Value Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.Value)}", 
                });
            }
            if (UserDefinedValueDTO.Item_LineID == null || UserDefinedValueDTO.Item_LineID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Line Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.Item_LineDTO)}", 
                });
            }
            if (UserDefinedValueDTO.SupportGroupID == null || UserDefinedValueDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.SupportGroupDTO)}", 
                });
            }
            if (UserDefinedValueDTO.UserDefinedID == null || UserDefinedValueDTO.UserDefinedID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "UserDefined Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.UserDefinedDTO)}", 
                });
            }

            if (UserDefinedValueDTO.AddedByID == null || UserDefinedValueDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateUserDefinedValue_Validation(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (UserDefinedValueDTO.ID == null || UserDefinedValueDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(UserDefinedValueDTO.Value))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Value Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.Value)}", 
                });
            }
            if (UserDefinedValueDTO.Item_LineID == null || UserDefinedValueDTO.Item_LineID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Line Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.Item_LineDTO)}", 
                });
            }
            if (UserDefinedValueDTO.SupportGroupID == null || UserDefinedValueDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.SupportGroupDTO)}", 
                });
            }
            if (UserDefinedValueDTO.UserDefinedID == null || UserDefinedValueDTO.UserDefinedID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "UserDefined Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(UserDefinedValue)}{nameof(UserDefinedValueDTO.UserDefinedDTO)}", 
                });
            }

            if (UserDefinedValueDTO.LastUpdateByID == null || UserDefinedValueDTO.LastUpdateByID == 0)
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
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO DeleteUserDefinedValue_Validation(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (UserDefinedValueDTO.ID == null || UserDefinedValueDTO.ID == 0)
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
