using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplate_Validator
{
    public static ValidationResultDTO CreateUserDefinedTemplate_Validation(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (UserDefinedTemplateDTO.Item_SupportGroupID == null || UserDefinedTemplateDTO.Item_SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Header Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (UserDefinedTemplateDTO.UserDefinedID == null || UserDefinedTemplateDTO.UserDefinedID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "UserDefined Field Empty",
                    Description = " Please, complete the missing information ", 
                });
            }

            //get user defined template list
            var _userDefinedTemplateDTO = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO
            {
                UserDefinedID = UserDefinedTemplateDTO.UserDefinedID,
                Item_SupportGroupID = UserDefinedTemplateDTO.Item_SupportGroupID,
                IsActive = UserDefinedTemplateDTO.IsActive
            }).FirstOrDefault();

            // Validate if user defined template exist and at least one User defined Check
            if (_userDefinedTemplateDTO != null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Field already exist in the item",
                    Description = "Please, verify the information information ",
                });
            }
            if (UserDefinedTemplateDTO.AddedByID == null || UserDefinedTemplateDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateUserDefinedTemplate_Validation(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (UserDefinedTemplateDTO.Item_SupportGroupID == null || UserDefinedTemplateDTO.Item_SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Header Field Empty",
                    Description = " Please, complete the missing information ", 
                });
            }
            if (UserDefinedTemplateDTO.UserDefinedIDArray == null || UserDefinedTemplateDTO.UserDefinedIDArray.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "UserDefined Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (UserDefinedTemplateDTO.LastUpdateByID == null || UserDefinedTemplateDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteUserDefinedTemplate_Validation(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (UserDefinedTemplateDTO.ID == null || UserDefinedTemplateDTO.ID == 0)
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

    public static ValidationResultDTO UserDefinedTemplate_Validation(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (UserDefinedTemplateDTO.UserDefinedIDArray == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "User Defined ID Object is null",
                    Description = "Please, complete the missing information ",
                });
            }
            if (UserDefinedTemplateDTO.Item_SupportGroupID == null || UserDefinedTemplateDTO.Item_SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Header Field Empty",
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
