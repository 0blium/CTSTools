using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;

public class User_Role_Validator
{
    public static ValidationResultDTO CreateUser_Role_Validation(User_RoleDTO User_RoleDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (User_RoleDTO.RoleID == null || User_RoleDTO.RoleID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Role Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(User_Role)}{nameof(User_RoleDTO.RoleID)}", 
                });
            }
            if (User_RoleDTO.UserID == null || User_RoleDTO.UserID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "User Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(User_Role)}{nameof(User_RoleDTO.UserID)}", 
                });
            }
            //validate if relation already exists
            var _user_roleDTO = User_Role_Service.GetUser_RoleList_Global(User_RoleDTO).FirstOrDefault();            
            if(_user_roleDTO != null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Relation alraedy exist ",
                    Description = "verify the information",
                });
            }


            if (User_RoleDTO.AddedByID == null || User_RoleDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateUser_Role_Validation(User_RoleDTO User_RoleDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (User_RoleDTO.ID == null || User_RoleDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (User_RoleDTO.RoleID == null || User_RoleDTO.RoleID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Role Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(User_Role)}{nameof(User_RoleDTO.RoleID)}", 
                });
            }
            if (User_RoleDTO.UserID == null || User_RoleDTO.UserID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "User Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(User_Role)}{nameof(User_RoleDTO.UserID)}", 
                });
            }
            
            if (User_RoleDTO.LastUpdateByID == null || User_RoleDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteUser_Role_Validation(User_RoleDTO User_RoleDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (User_RoleDTO.ID == null || User_RoleDTO.ID == 0)
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
