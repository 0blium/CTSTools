using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_Permission_Validator
{
    public static ValidationResultDTO CreateUser_Permission_Validation(User_PermissionDTO User_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (User_PermissionDTO.PermissionID == null || User_PermissionDTO.PermissionID == 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User_Permission)}{nameof(User_PermissionDTO.PermissionID)}",
                });

            if (User_PermissionDTO.UserID == null || User_PermissionDTO.UserID == 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "User Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User_Permission)}{nameof(User_PermissionDTO.UserID)}",
                });


            //Validate if the permission already exist
            var _user_permisionDTO = new User_PermissionDTO
            {
                PermissionID = User_PermissionDTO.PermissionID,
                UserID = User_PermissionDTO.UserID
            };
            var _user_PermissionList = User_Permission_Service.GetUser_PermissionList_Global(_user_permisionDTO);
            if (_user_PermissionList.Count > 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission is already created",
                    Description = " Please, verify the information",
                });
            }

            if (User_PermissionDTO.AddedByID == null || User_PermissionDTO.AddedByID == 0)
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

    public static ValidationResultDTO CreateUser_PermissionByArray_Validation(User_PermissionDTO User_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (User_PermissionDTO.PermissionIDArray == null || User_PermissionDTO.PermissionIDArray.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User_Permission)}{nameof(User_PermissionDTO.PermissionIDArray)}",
                });
            }
            if (User_PermissionDTO.UserID == null || User_PermissionDTO.UserID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "User Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User_Permission)}{nameof(User_PermissionDTO.UserID)}",
                });
            }



            if (User_PermissionDTO.AddedByID == null || User_PermissionDTO.AddedByID == 0)
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


    public static ValidationResultDTO UpdateUser_Permission_Validation(User_PermissionDTO User_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (User_PermissionDTO.ID == null || User_PermissionDTO.ID == 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });

            if (User_PermissionDTO.PermissionID == null || User_PermissionDTO.PermissionID == 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User_Permission)}{nameof(User_PermissionDTO.PermissionID)}",
                });
            if (User_PermissionDTO.UserID == null || User_PermissionDTO.UserID == 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "User Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(User_Permission)}{nameof(User_PermissionDTO.UserID)}",
                });

            if (User_PermissionDTO.LastUpdateByID == null || User_PermissionDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteUser_Permission_Validation(User_PermissionDTO User_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (User_PermissionDTO.ID == null || User_PermissionDTO.ID == 0)
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


    public static ValidationResultDTO ValidateRemovePermissionToUserByRole(User_PermissionDTO User_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (User_PermissionDTO.ID == null || User_PermissionDTO.ID == 0)
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
                //validate if user has another role with that permission
                //    var _userDTO = User_Service.GetUserList_Global(new UserDTO { ID = User_PermissionDTO.UserDTO.ID, GetRoleArray = true }).FirstOrDefault();
                //    var _rolespermissionIDArray = Role_Permission_Service.GetRole_PermissionList_Global(new Role_PermissionDTO { PermissionDTO = User_PermissionDTO.PermissionDTO }).Select(s => s.RoleDTO.ID).ToArray();

                //    if (_userDTO.RoleIDArray!= null && _userDTO.RoleIDArray.Intersect(_rolespermissionIDArray).Any())
                //    {
                //        _validation_ResultList.Add(new ValidationResultDTO
                //        {
                //            Result = false,
                //            Message = "User has permission in another role",
                //            Description = "Please, verify the information",
                //        });
                //    }
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

    public static ValidationResultDTO CreateMultiple_Validation(List<User_PermissionDTO> User_PermissionList)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            foreach (var _user_permissionDTO in User_PermissionList)
            {
                // Field Validation
                _user_permissionDTO.AddedDate = DateTime.Now;
                _user_permissionDTO.IsActive = true;
                _validation_ResultDTO = CreateUser_Permission_Validation(_user_permissionDTO);
                if (!_validation_ResultDTO.Result)
                    _validation_ResultList.Add(_validation_ResultDTO);
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


    public static ValidationResultDTO DeleteMultiple_Validation(List<User_PermissionDTO> User_PermissionList)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            foreach (var _user_permissionDTO in User_PermissionList)
            {
                _user_permissionDTO.AddedDate = DateTime.Now;
                _user_permissionDTO.IsActive = true;
                _validation_ResultDTO = DeleteUser_Permission_Validation(_user_permissionDTO);
                if (!_validation_ResultDTO.Result)
                    _validation_ResultList.Add(_validation_ResultDTO);
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
