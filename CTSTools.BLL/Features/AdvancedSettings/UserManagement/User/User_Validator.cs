using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;

public class User_Validator
{
    public static ValidationResultDTO CreateUser_Validation(UserDTO UserDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();


            //if (UserDTO.Login == "\\" || UserDTO.Login == string.Empty)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Empty Account",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(User)}{nameof(UserDTO.Login)}",
            //    });
            //}
            // Validate if user exist in  DB
            var _userDTO = User_Service.GetUserList_Global(new UserDTO { Login = UserDTO.Login }).FirstOrDefault();
            if (_userDTO != null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "The record is already in the DB.",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.Login)}",
                });
            }

            

            //Validate if the Employee_Tress is already used on another account               

            if (UserDTO.FacilityID == null || UserDTO.FacilityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.FacilityID)}",
                });
            }
            if (UserDTO.DepartmentID == null || UserDTO.DepartmentID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.DepartmentID)}",
                });
            }            
            if (UserDTO.AddedByID == null || UserDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateUser_Validation(UserDTO UserDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (UserDTO.ID == null || UserDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.ID)}"
                });
            }
            if (string.IsNullOrEmpty(UserDTO.Login))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Login Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.Login)}",
                });
            }
            if (string.IsNullOrEmpty(UserDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.Name)}",
                });
            }
            if (string.IsNullOrEmpty(UserDTO.Email))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Email Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.Email)}",
                });
            }
            if (string.IsNullOrEmpty(UserDTO.Position))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Position Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.Position)}",
                });
            }
            if (UserDTO.FacilityID == null || UserDTO.FacilityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.FacilityID)}",
                });
            }
            if (UserDTO.DepartmentID == null || UserDTO.DepartmentID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.DepartmentID)}",
                });
            }
            
            //if (UserDTO.LastUpdateByID == null || UserDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteUser_Validation(UserDTO UserDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (UserDTO.ID == null || UserDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User)}{nameof(UserDTO.ID)}"
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
