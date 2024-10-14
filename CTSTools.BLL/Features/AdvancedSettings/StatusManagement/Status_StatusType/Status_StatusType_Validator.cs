using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;

class Status_StatusType_Validator
{
    public static ValidationResultDTO CreateStatus_StatusType_Validation(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Status_StatusTypeDTO.StatusDTO.ID == null || Status_StatusTypeDTO.StatusDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Status_StatusType)}{nameof(Status_StatusTypeDTO.StatusDTO)}"
                });
            }
            if (Status_StatusTypeDTO.StatusTypeDTO.ID == null || Status_StatusTypeDTO.StatusTypeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "StatusType Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Status_StatusType)}{nameof(Status_StatusTypeDTO.StatusTypeDTO)}"
                });
            }
            else
            {
                var _statusRelationDTO = Status_StatusType_Service.GetStatus_StatusTypeList_Global(
                    new Status_StatusTypeDTO
                    {
                        StatusDTO = Status_StatusTypeDTO.StatusDTO,
                        StatusTypeDTO = Status_StatusTypeDTO.StatusTypeDTO,
                    }).FirstOrDefault();
                if(_statusRelationDTO != null)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Relation already exist",
                        Description = " Please, Please verify the information",
                    });
                }
            }

            if (Status_StatusTypeDTO.AddedByID == null || Status_StatusTypeDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateStatus_StatusType_Validation(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Status_StatusTypeDTO.ID == null || Status_StatusTypeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                    Data = $"{nameof(Status_StatusType)}{nameof(Status_StatusTypeDTO.ID)}"
                });
            }
            if (Status_StatusTypeDTO.StatusDTO.ID == null || Status_StatusTypeDTO.StatusDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "StatusID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Status_StatusType)}{nameof(Status_StatusTypeDTO.StatusDTO)}"
                });
            }
            if (Status_StatusTypeDTO.StatusTypeDTO.ID == null || Status_StatusTypeDTO.StatusTypeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "StatusTypeID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Status_StatusType)}{nameof(Status_StatusTypeDTO.StatusTypeDTO)}"
                });
            }
            else
            {
                var _statusRelationDTO = Status_StatusType_Service.GetStatus_StatusTypeList_Global(
                    new Status_StatusTypeDTO
                    {
                        StatusDTO = Status_StatusTypeDTO.StatusDTO,
                        StatusTypeDTO = Status_StatusTypeDTO.StatusTypeDTO,
                    }).FirstOrDefault();
                if (_statusRelationDTO != null)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Relation already exist",
                        Description = " Please, Please verify the information",
                    });
                }
            }

            if (Status_StatusTypeDTO.LastUpdateByID == null || Status_StatusTypeDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteStatus_StatusType_Validation(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Status_StatusTypeDTO.ID == null || Status_StatusTypeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Status_StatusType)}{nameof(Status_StatusTypeDTO.ID)}"
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
