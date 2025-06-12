using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.MailGroups.MailGroupMember
{
    public class MailGroupMember_Validator
    {
        public static ValidationResultDTO CreateMailGroupMember_Validation(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
               
                if (MailGroupMemberDTO.MailGroupID == null || MailGroupMemberDTO.MailGroupID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "MailGroup Field Empty", 
                        Description = " Please, complete the missing information ", 
                        //Data = $"{nameof(MailGroupMember)}{nameof(MailGroupMemberDTO.MailGroupDTO)}", 
                    });
                }
                if (MailGroupMemberDTO.UserID == null || MailGroupMemberDTO.UserID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "User Field Empty", 
                        Description = " Please, complete the missing information ", 
                        //Data = $"{nameof(MailGroupMember)}{nameof(MailGroupMemberDTO.UserDTO)}", 
                    });
                }
                else
                {
                    var _mailgroupMemberDTO = MailGroupMember_Service.GetMailGroupMemberList_Global(
                        new MailGroupMemberDTO
                        {
                            UserID = MailGroupMemberDTO.UserID,
                            MailGroupID = MailGroupMemberDTO.MailGroupID
                        }).FirstOrDefault();
                    if(_mailgroupMemberDTO != null)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "User already exist in mail group",
                            Description = " Please, complete the missing information ",
                        });
                    }
                }

                if (MailGroupMemberDTO.AddedByID == null || MailGroupMemberDTO.AddedByID == 0)
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
        public static ValidationResultDTO UpdateMailGroupMember_Validation(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                
               
                if (MailGroupMemberDTO.MailGroupID == null || MailGroupMemberDTO.MailGroupID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "MailGroup Field Empty", 
                        Description = " Please, complete the missing information ", 
                        //Data = $"{nameof(MailGroupMember)}{nameof(MailGroupMemberDTO.MailGroupDTO)}", 
                    });
                }
                if (MailGroupMemberDTO.UserID == null || MailGroupMemberDTO.UserID == 0 )
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false, 
                        Message = "User Field Empty", 
                        Description = " Please, complete the missing information ", 
                        //Data = $"{nameof(MailGroupMember)}{nameof(MailGroupMemberDTO.UserDTO)}", 
                    });
                }
                else
                {
                    var _mailgroupMemberDTO = MailGroupMember_Service.GetMailGroupMemberList_Global(
                        new MailGroupMemberDTO
                        {
                            UserID = MailGroupMemberDTO.UserID,
                            MailGroupID = MailGroupMemberDTO.MailGroupID
                        }).FirstOrDefault();
                    if (_mailgroupMemberDTO != null)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "User already exist in mail group",
                            Description = " Please, complete the missing information ",
                        });
                    }
                }

                if (MailGroupMemberDTO.LastUpdateByID == null || MailGroupMemberDTO.LastUpdateByID == 0)
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
        public static ValidationResultDTO DeleteMailGroupMember_Validation(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();
                
                // Field Validation
                if (MailGroupMemberDTO.ID == null || MailGroupMemberDTO.ID == 0)
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

        public static ValidationResultDTO CreateMailGroupMemberByGroups_Validation(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation

                if (MailGroupMemberDTO.MailGroupIDArray == null || MailGroupMemberDTO.MailGroupIDArray.Count() == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "MailGroup Field Empty",
                        Description = " Please, complete the missing information ",
                        //Data = $"{nameof(MailGroupMember)}{nameof(MailGroupMemberDTO.MailGroupDTO)}",
                    });
                }
                if (MailGroupMemberDTO.UserID == null || MailGroupMemberDTO.UserID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "User Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(MailGroupMember)}{nameof(MailGroupMemberDTO.UserDTO)}",
                    });
                }

                if (MailGroupMemberDTO.AddedByID == null || MailGroupMemberDTO.AddedByID == 0)
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


        public static ValidationResultDTO CreateMultiple_Validation(List<MailGroupMemberDTO> MailGroupMemberList)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                foreach (var _mailGroupMemberDTO in MailGroupMemberList)
                {
                    // Field Validation
                    _mailGroupMemberDTO.AddedDate = DateTime.Now;
                    _mailGroupMemberDTO.IsActive = true;
                    _validation_ResultDTO = CreateMailGroupMember_Validation(_mailGroupMemberDTO);
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
