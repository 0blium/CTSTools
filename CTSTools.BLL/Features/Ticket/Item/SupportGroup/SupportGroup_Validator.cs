using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.SupportGroup;

public class SupportGroup_Validator
{
    public static ValidationResultDTO CreateSupportGroup_Validation(SupportGroupDTO SupportGroupDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(SupportGroupDTO.EnglishName))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "EnglishName Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.EnglishName)}",
                });
            }
            if (string.IsNullOrEmpty(SupportGroupDTO.SpanishName))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SpanishName Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.SpanishName)}",
                });
            }
            if (SupportGroupDTO.FacilityDTO.ID == null || SupportGroupDTO.FacilityDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.FacilityDTO)}",
                });
            }
            if (SupportGroupDTO.StationDTO.ID == null || SupportGroupDTO.StationDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Default Station Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.StationDTO)}",
                });
            }
            if (SupportGroupDTO.AddedByID == null || SupportGroupDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateSupportGroup_Validation(SupportGroupDTO SupportGroupDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SupportGroupDTO.ID == null || SupportGroupDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(SupportGroupDTO.EnglishName))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "EnglishName Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.EnglishName)}",
                });
            }
            if (string.IsNullOrEmpty(SupportGroupDTO.SpanishName))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SpanishName Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.SpanishName)}",
                });
            }
            if (SupportGroupDTO.FacilityDTO.ID == null || SupportGroupDTO.FacilityDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.FacilityDTO)}",
                });
            }
            if (SupportGroupDTO.StationDTO.ID == null || SupportGroupDTO.StationDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Default Station Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SupportGroup)}{nameof(SupportGroupDTO.StationDTO)}",
                });
            }
            if (SupportGroupDTO.LastUpdateByID == null || SupportGroupDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteSupportGroup_Validation(SupportGroupDTO SupportGroupDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SupportGroupDTO.ID == null || SupportGroupDTO.ID == 0)
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
