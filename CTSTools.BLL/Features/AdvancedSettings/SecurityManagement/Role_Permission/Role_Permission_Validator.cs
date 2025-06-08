using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CTSTools.BLL.Features.Security.Permissions.Role_Permission;

public class Role_Permission_Validator
{
    public static ValidationResultDTO CreateRole_Permission_Validation(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Role_PermissionDTO.PermissionID == null || Role_PermissionDTO.PermissionID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Role_Permission)}{nameof(Role_PermissionDTO.PermissionID)}",
                });
            }
            if (Role_PermissionDTO.RoleID == null || Role_PermissionDTO.RoleID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Role Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Role_Permission)}{nameof(Role_PermissionDTO.RoleID)}",
                });
            }


            if (Role_PermissionDTO.AddedByID == null || Role_PermissionDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            //validate permission role relation already exist

            var _role_permissionDTO = Role_Permission_Service.GetRole_PermissionList_Global(new Role_PermissionDTO { RoleID = Role_PermissionDTO.RoleID, PermissionID = Role_PermissionDTO.PermissionID }).FirstOrDefault();

            if (_role_permissionDTO != null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Relation alraedy exist ",
                    Description = "verify the information",
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

    public static ValidationResultDTO CreateRole_PermissionByIDArray_Validation(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Role_PermissionDTO.PermissionIDArray == null || Role_PermissionDTO.PermissionIDArray.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Role_Permission)}{nameof(Role_PermissionDTO.PermissionIDArray)}",
                });
            }

            if (Role_PermissionDTO.RoleID == null || Role_PermissionDTO.RoleID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Role Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Role_Permission)}{nameof(Role_PermissionDTO.RoleID)}",
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

    public static ValidationResultDTO UpdateRole_Permission_Validation(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Role_PermissionDTO.ID == null || Role_PermissionDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (Role_PermissionDTO.PermissionID == null || Role_PermissionDTO.PermissionID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Permission Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Role_Permission)}{nameof(Role_PermissionDTO.PermissionID)}",
                });
            }
            if (Role_PermissionDTO.RoleID == null || Role_PermissionDTO.RoleID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Role Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Role_Permission)}{nameof(Role_PermissionDTO.RoleID)}",
                });
            }

            if (Role_PermissionDTO.LastUpdateByID == null || Role_PermissionDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteRole_Permission_Validation(Role_PermissionDTO Role_PermissionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Role_PermissionDTO.ID == null || Role_PermissionDTO.ID == 0)
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

    public static ValidationResultDTO CreateMultiple_Validation(List<Role_PermissionDTO> Role_PermissionList)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            foreach (var _rolepermissionDTO in Role_PermissionList)
            {
                // Field Validation
                _rolepermissionDTO.AddedDate = DateTime.Now;
                _rolepermissionDTO.IsActive = true;
                _validation_ResultDTO = CreateRole_Permission_Validation(_rolepermissionDTO);
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
