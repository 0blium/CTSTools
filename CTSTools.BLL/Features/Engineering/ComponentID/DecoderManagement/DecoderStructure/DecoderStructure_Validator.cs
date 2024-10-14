using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
public class DecoderStructure_Validator
{
    public static ValidationResultDTO CreateDecoderStructure_Validation(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DecoderStructureDTO.DecoderID == null || DecoderStructureDTO.DecoderID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Decoder Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DecoderStructureDTO.AttributeID == null || DecoderStructureDTO.AttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            var _decoderDTO = Decoder_Repository.GetDecoderByID((int)DecoderStructureDTO.DecoderID);
            if (_decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Edit && _decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Draft)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "Decoder must be under 'draft' or 'editing' status. "
                });
            var decoderStructureDTO = new DecoderStructureDTO { AttributeID = DecoderStructureDTO.AttributeID, DecoderID = DecoderStructureDTO.DecoderID };
            var _decoderStructureDTO = DecoderStructure_Service.GetDecoderStructureList_Global(decoderStructureDTO).FirstOrDefault();
            if (_decoderStructureDTO != null && _decoderStructureDTO.AttributeID != (int)Attribute_Enum.Symbol)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "This attribute is already in the decoder."
                });
            }
            if ((DecoderStructureDTO.DescriptionBody == null && DecoderStructureDTO.NumberBody == null) || (DecoderStructureDTO.DescriptionBody == false && DecoderStructureDTO.NumberBody == false))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Number Body Field Empty",
                    Description = " Please, complete the missing information.",
                });
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Description Body Field Empty",
                    Description = " Please, complete the missing information.",
                });
            }
            if ((DecoderStructureDTO.DescriptionBody == null && DecoderStructureDTO.DescriptionOrder == 0) || ((bool)DecoderStructureDTO.DescriptionBody && DecoderStructureDTO.DescriptionOrder == 0))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Description Order Field Empty",
                    Description = " Please, complete the missing information.",
                });
            }
            if ((bool)DecoderStructureDTO.NumberBody && DecoderStructureDTO.NumberOrder == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Number Order Field Empty",
                    Description = " Please, complete the missing information."
                });
            }
            if (DecoderStructureDTO.AddedByID == null || DecoderStructureDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateDecoderStructure_Validation(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            var _attributeDTO = new AttributeDTO();
            // Field Validation
            if (DecoderStructureDTO.ID == null || DecoderStructureDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }

            if (DecoderStructureDTO.AttributeID == null || DecoderStructureDTO.AttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = "Please, complete the missing information ",
                    Data = $"{nameof(DecoderStructure)}{nameof(DecoderStructureDTO.AttributeID)}",
                });
            }
            else
            {
                _attributeDTO = Attribute_Repository.GetAttributeByID((int)DecoderStructureDTO.AttributeID);
            }
            var _decoderDTO = Decoder_Repository.GetDecoderByID((int)DecoderStructureDTO.DecoderID);
            if (_decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Edit && _decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Draft)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "Decoder must be under 'draft' or 'editing' status. ",
                    Data = $"{nameof(DecoderStructure)}{nameof(DecoderStructureDTO.AttributeID)}",
                });
            if (_attributeDTO.HasMultipleOptions == false && (DecoderStructureDTO.ValueID == null || DecoderStructureDTO.ValueID == 0))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Value Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(DecoderStructure)}{nameof(DecoderStructureDTO.ValueID)}",
                });
            }

            if (_attributeDTO.HasMultipleOptions == true && DecoderStructureDTO.ValueIDArray.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Options Tagbox Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DecoderStructureDTO.LastUpdateByID == null || DecoderStructureDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Last Update By Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var decoderStructureDTO = new DecoderStructureDTO { AttributeID = DecoderStructureDTO.AttributeID, DecoderID = DecoderStructureDTO.DecoderID };
            var _decoderStructureDTO = DecoderStructure_Service.GetDecoderStructureList_Global(decoderStructureDTO).Where(w => w.ID != DecoderStructureDTO.ID).FirstOrDefault();
            if (_decoderStructureDTO != null && _decoderStructureDTO.AttributeID != (int)Attribute_Enum.Symbol )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "This attribute is already in the decoder.",
                    Data = $"{nameof(DecoderStructure)}{nameof(DecoderStructureDTO.ValueID)}",
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
    public static ValidationResultDTO DeleteDecoderStructure_Validation(DecoderStructureDTO DecoderStructureDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DecoderStructureDTO.ID == null || DecoderStructureDTO.ID == 0)
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
            var _decoderDTO = Decoder_Repository.GetDecoderByID((int)DecoderStructureDTO.DecoderID);
            if (_decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Edit && _decoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Draft)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "Decoder must be under 'draft' or 'Editing' status. ",
                    Data = $"{nameof(DecoderStructure)}{nameof(DecoderStructureDTO.AttributeID)}",
                });
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
    public static bool IsBaseAttribute(DecoderStructureDTO DecoderStructureDTO)
    {
        bool _isbaseAttribute = true;
        var _decoderStructureDTO = DecoderStructure_Service.GetDecoderStructureList_Global(DecoderStructureDTO).FirstOrDefault();
        if (_decoderStructureDTO != null)
        {
            int?[] _invalidAttributes = [(int)Attribute_Enum.SubClass, (int)Attribute_Enum.Class, (int)Attribute_Enum.ComponentType, (int)Attribute_Enum.PartType];
            if (!_invalidAttributes.Contains(_decoderStructureDTO.AttributeID))
            {
                _isbaseAttribute = false;
            }
        }


        return _isbaseAttribute;
    }
}

