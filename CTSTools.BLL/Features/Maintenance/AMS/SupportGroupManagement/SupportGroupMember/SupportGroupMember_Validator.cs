using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember
{
    public class SupportGroupMember_Validator
    {
        public static ValidationResultDTO CreateSupportGroupMember_Validation(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (SupportGroupMemberDTO.SupportGroupDTO.ID == null || SupportGroupMemberDTO.SupportGroupDTO.ID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "SupportGroup Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.SupportGroupDTO)}", 
                    });
                }
                if (SupportGroupMemberDTO.UserDTO.ID == null || SupportGroupMemberDTO.UserDTO.ID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "User Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.UserDTO)}", 
                    });
                }
                if (SupportGroupMemberDTO.RoleDTO.ID == null || SupportGroupMemberDTO.RoleDTO.ID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "Role Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.RoleDTO)}", 
                    });
                }

                if(SupportGroupMemberDTO.RoleDTO.ID > 0 && SupportGroupMemberDTO.UserDTO.ID > 0)
                {
                    //validate if user already exist in support Group
                    var _supportGroupMemberDTO = SupportGroupMember_Service.GetSupportGroupMemberList_Global(new SupportGroupMemberDTO
                    {
                        UserDTO = SupportGroupMemberDTO.UserDTO,
                        SupportGroupDTO = SupportGroupMemberDTO.SupportGroupDTO
                    }).FirstOrDefault();

                    if(_supportGroupMemberDTO != null)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "The user is already a member of the group.",
                            Description = " Please, verify the information ",
                            Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.UserDTO)}",
                        });
                    };
                }
                
                
                if (SupportGroupMemberDTO.AddedByID == null || SupportGroupMemberDTO.AddedByID == 0)
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
        public static ValidationResultDTO UpdateSupportGroupMember_Validation(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (SupportGroupMemberDTO.ID == null || SupportGroupMemberDTO.ID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "ID Field Empty",
                        Description = "Please, complete the missing information ",
                    });
                }
                if (SupportGroupMemberDTO.SupportGroupDTO.ID == null || SupportGroupMemberDTO.SupportGroupDTO.ID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "SupportGroup Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.SupportGroupDTO)}", 
                    });
                }
                if (SupportGroupMemberDTO.UserDTO.ID == null || SupportGroupMemberDTO.UserDTO.ID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "User Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.UserDTO)}", 
                    });
                }
                if (SupportGroupMemberDTO.RoleDTO.ID == null || SupportGroupMemberDTO.RoleDTO.ID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "Role Field Empty", 
                        Description = " Please, complete the missing information ", 
                        Data = $"{nameof(SupportGroupMember)}{nameof(SupportGroupMemberDTO.RoleDTO)}", 
                    });
                }
                
                if (SupportGroupMemberDTO.LastUpdateByID == null || SupportGroupMemberDTO.LastUpdateByID == 0)
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


        public static ValidationResultDTO UnnasingRole_User_Validation(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                

                if (SupportGroupMemberDTO.RoleDTO.ID > 0 && SupportGroupMemberDTO.UserDTO.ID > 0)
                {
                    //validate if user already exist in support Group
                    var _supportGroupMemberDTO = SupportGroupMember_Service.GetSupportGroupMemberList_Global(new SupportGroupMemberDTO
                    {
                        UserDTO = SupportGroupMemberDTO.UserDTO,
                        RoleDTO = SupportGroupMemberDTO.RoleDTO,
                        IsActive=true
                    }).FirstOrDefault();

                    if (_supportGroupMemberDTO != null)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "The user still has the assigned role in another support group.",
                            Description = " Please, verify the information ",
                        });
                    };
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


        public static ValidationResultDTO DeleteSupportGroupMember_Validation(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (SupportGroupMemberDTO.ID == null || SupportGroupMemberDTO.ID == 0)
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
}
