using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.ChangeLog;

public class ChangeLog_Validator
{
    public static ValidationResultDTO CreateChangeLog_Validation(ChangeLogDTO ChangeLogDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(ChangeLogDTO.Table))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Table Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.Table)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.Field))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Field Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.Field)}",
                });
            }

            if (ChangeLogDTO.UserDTO.ID == null || ChangeLogDTO.UserDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "User Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.UserDTO)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.Action))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.Action)}",
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
    public static ValidationResultDTO UpdateChangeLog_Validation(ChangeLogDTO ChangeLogDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (ChangeLogDTO.ID == null || ChangeLogDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.Table))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Table Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.Table)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.Field))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Field Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.Field)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.OldValue))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "OldValue Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.OldValue)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.NewValue))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "NewValue Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.NewValue)}",
                });
            }
            if (ChangeLogDTO.UserDTO.ID == null || ChangeLogDTO.UserDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "User Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.UserDTO)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.Action))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.Action)}",
                });
            }
            if (string.IsNullOrEmpty(ChangeLogDTO.ChangeGroup))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ChangeGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(ChangeLog)}{nameof(ChangeLogDTO.ChangeGroup)}",
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
    public static ValidationResultDTO DeleteChangeLog_Validation(ChangeLogDTO ChangeLogDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (ChangeLogDTO.ID == null || ChangeLogDTO.ID == 0)
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
