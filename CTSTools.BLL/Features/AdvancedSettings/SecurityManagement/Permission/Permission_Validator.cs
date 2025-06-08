using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CTSTools.BLL.Features.Security.Permissions.Permission
{
    public class Permission_Validator
    {
        public static ValidationResultDTO CreatePermission_Validation(PermissionDTO PermissionDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (string.IsNullOrEmpty(PermissionDTO.Name) )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "Name Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(Permission)}{nameof(PermissionDTO.Name)}", 
                    });
                }
                if (PermissionDTO.ModuleID == null || PermissionDTO.ModuleID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Module Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(Permission)}{nameof(PermissionDTO.ModuleID)}",
                    });
                }
                if (PermissionDTO.ActionID == null || PermissionDTO.ActionID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "Action Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(Permission)}{nameof(PermissionDTO.ActionID)}", 
                    });
                }
                
                if (PermissionDTO.AddedByID == null || PermissionDTO.AddedByID == 0)
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
        public static ValidationResultDTO UpdatePermission_Validation(PermissionDTO PermissionDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (PermissionDTO.ID == null || PermissionDTO.ID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "ID Field Empty",
                        Description = "Please, complete the missing information ",
                    });
                }
                if (string.IsNullOrEmpty(PermissionDTO.Name) )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "Name Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(Permission)}{nameof(PermissionDTO.Name)}", 
                    });
                }
                if (PermissionDTO.ModuleID == null || PermissionDTO.ModuleID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Module Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(Permission)}{nameof(PermissionDTO.ModuleID)}",
                    });
                }
                if (PermissionDTO.ActionID == null || PermissionDTO.ActionID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "Action Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(Permission)}{nameof(PermissionDTO.ActionID)}", 
                    });
                }
                
                if (PermissionDTO.LastUpdateByID == null || PermissionDTO.LastUpdateByID == 0)
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
        public static ValidationResultDTO DeletePermission_Validation(PermissionDTO PermissionDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (PermissionDTO.ID == null || PermissionDTO.ID == 0)
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

        public static ValidationResultDTO CreateMultiple_Validation(List<PermissionDTO> PermissionList)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                foreach (var _permissionDTO in PermissionList)
                {
                    // Field Validation
                    _permissionDTO.AddedDate = DateTime.Now;
                    _permissionDTO.IsActive = true;
                    _validation_ResultDTO = CreatePermission_Validation(_permissionDTO);
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
}
