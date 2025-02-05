using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;
namespace CTSTools.BLL.Features.Quality.QMS.DocumentRevision;
public class DocumentRevision_Validator
{
    public static ValidationResultDTO Create_Validation(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(DocumentRevisionDTO.Revision))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Revision Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DocumentRevision)}{nameof(DocumentRevisionDTO.Revision)}",
                });
            } 
            if (string.IsNullOrEmpty(DocumentRevisionDTO.ChangeReason))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Change Reason Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DocumentRevision)}{nameof(DocumentRevisionDTO.ChangeReason)}",
                });
            } 

            if (DocumentRevisionDTO.DocumentID == null || DocumentRevisionDTO.DocumentID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Document Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DocumentRevisionDTO.StatusID == null || DocumentRevisionDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (DocumentRevisionDTO.AddedByID == null || DocumentRevisionDTO.AddedByID == 0)
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
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO Update_Validation(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DocumentRevisionDTO.ID == null || DocumentRevisionDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(DocumentRevisionDTO.Revision))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Revision Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DocumentRevision)}{nameof(DocumentRevisionDTO.Revision)}",
                });
            }
            if (string.IsNullOrEmpty(DocumentRevisionDTO.ChangeReason))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Change Reason Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DocumentRevision)}{nameof(DocumentRevisionDTO.ChangeReason)}",
                });
            }
            if (DocumentRevisionDTO.DocumentID == null || DocumentRevisionDTO.DocumentID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Document Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DocumentRevisionDTO.StatusID == null || DocumentRevisionDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (DocumentRevisionDTO.LastUpdateByID == null || DocumentRevisionDTO.LastUpdateByID == 0)
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
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO Delete_Validation(DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DocumentRevisionDTO.ID == null || DocumentRevisionDTO.ID == 0)
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
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }

}
