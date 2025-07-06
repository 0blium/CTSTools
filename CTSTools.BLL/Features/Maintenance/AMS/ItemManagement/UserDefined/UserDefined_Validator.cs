using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;

public class UserDefined_Validator
{
    public static ValidationResultDTO CreateUserDefined_Validation(UserDefinedDTO UserDefinedDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (string.IsNullOrEmpty(UserDefinedDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(UserDefined)}{nameof(UserDefinedDTO.Name)}",
                });
            }
            if (UserDefinedDTO.SupportGroupID == null || UserDefinedDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(UserDefined)}{nameof(UserDefinedDTO.SupportGroupID)}",
                });
            }
            if (UserDefinedDTO.IsMandatory == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "IsMandatory Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (UserDefinedDTO.DataTypeID == null || UserDefinedDTO.DataTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DataType Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(UserDefined)}{nameof(UserDefinedDTO.DataTypeID)}",
                });
            }

            if (UserDefinedDTO.AddedByID == null || UserDefinedDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateUserDefined_Validation(UserDefinedDTO UserDefinedDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (UserDefinedDTO.ID == null || UserDefinedDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(UserDefinedDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(UserDefined)}{nameof(UserDefinedDTO.Name)}",
                });
            }
            if (UserDefinedDTO.SupportGroupID == null || UserDefinedDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(UserDefined)}{nameof(UserDefinedDTO.SupportGroupID)}",
                });
            }
            if (UserDefinedDTO.IsMandatory == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "IsMandatory Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (UserDefinedDTO.DataTypeID == null || UserDefinedDTO.DataTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "DataType Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(UserDefined)}{nameof(UserDefinedDTO.DataTypeID)}",
                });
            }

            if (UserDefinedDTO.LastUpdateByID == null || UserDefinedDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteUserDefined_Validation(UserDefinedDTO UserDefinedDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (UserDefinedDTO.ID == null || UserDefinedDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            else
            {
                var _userDefinedTemplateList = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO { UserDefinedID = UserDefinedDTO.ID });
                if (_userDefinedTemplateList != null && _userDefinedTemplateList.Count > 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error",
                        Description = "To delete the record, the custom field must not be assigned to any equipment.",
                    });
                }
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
