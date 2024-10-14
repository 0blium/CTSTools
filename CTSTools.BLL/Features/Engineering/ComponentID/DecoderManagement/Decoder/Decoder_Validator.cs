using Elmah;
using System;
using System.Collections.Generic;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;
using System.Linq;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
public class Decoder_Validator
{
    public static ValidationResultDTO CreateDecoder_Validation(DecoderDTO DecoderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation

            if (DecoderDTO.PartTypeID == null || DecoderDTO.PartTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Decoder)}{nameof(DecoderDTO.PartTypeID)}",
                });
            }
            if (DecoderDTO.PartTypeID == (int?)Value_Enum.PartTypeValue_Enum.Manufactured)
            {
                if (DecoderDTO.ComponentTypeID == null || DecoderDTO.ComponentTypeID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Component Type Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(Decoder)}{nameof(DecoderDTO.ComponentTypeID)}",
                    });
                }
            }
            if (DecoderDTO.ClassID == null || DecoderDTO.ClassID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Class Type Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Decoder)}{nameof(DecoderDTO.ClassID)}",
                });
            }
            if (DecoderDTO.SubClassID == null || DecoderDTO.SubClassID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Sub Class Type Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Decoder)}{nameof(DecoderDTO.SubClassID)}",
                });
            }
            if (DecoderDTO.AddedByID == null || DecoderDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _isduplicatedValidationResultDTO = IsItNotDuplicated(DecoderDTO);
            if (!_isduplicatedValidationResultDTO.Result)
            {
                _validation_ResultList.Add(_isduplicatedValidationResultDTO);
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
    public static ValidationResultDTO UpdateDecoder_Validation(DecoderDTO DecoderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DecoderDTO.ID == null || DecoderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (DecoderDTO.StatusID == null || DecoderDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Decoder)}{nameof(DecoderDTO.StatusDTO)}",
                });
            }

            if (DecoderDTO.LastUpdateByID == null || DecoderDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteDecoder_Validation(DecoderDTO DecoderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (DecoderDTO.ID == null || DecoderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (DecoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Draft && DecoderDTO.StatusID != (int)Status_Enum.Part_Number_Configurator.Edit)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = " Decoder configurator must be in Draft status",
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
    public static ValidationResultDTO IsItNotDuplicated(DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _decoderDTO = new DecoderDTO
            {
                ClassID = DecoderDTO.ClassID,
                SubClassID = DecoderDTO.SubClassID
            };
            if (_decoderDTO.ClassID != null && DecoderDTO.SubClassID != null)
            {
                var _decoderList = Decoder_Service.GetDecoderList_Global(_decoderDTO);
                if (_decoderList.Count > 0)
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Records",
                        Description = $" Decoder with class {_decoderList[0].ClassName} and subclass {_decoderList[0].SubClassName} is already in the database"

                    };
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _validationResultDTO;
    }

    public static ValidationResultDTO SubmitDecoder_Validation(DecoderDTO DecoderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation

            if (DecoderDTO.ID == null || DecoderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            // Validate Suppliers 
            var _subClass_SupplierDTO = new SubClass_SupplierDTO { SubClassID = (int)DecoderDTO.SubClassID };
            var _subClass_SupplierList = SubClass_Supplier_Service.GetSubClass_SupplierList_Global(_subClass_SupplierDTO);
            if (_subClass_SupplierList.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Supplier are empty",
                    Description = " Please, complete the missing information ",
                });
            }

            //Validate Decoder Structure Fields
            var _decoderStructureDTO = new DecoderStructureDTO { DecoderID = DecoderDTO.ID, GetAttributeDTO = true };
            var _decoderStructureList = DecoderStructure_Service.GetDecoderStructureList_Global(_decoderStructureDTO);
            if (_decoderStructureList.Count() == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type Field Empty",
                    Description = " Please, complete the missing information ",
                });

            }
            else
            {
                foreach (var DecoderStructureDTO in _decoderStructureList)
                {
                    if ((bool)DecoderStructureDTO.AttributeDTO.HasMultipleOptions)
                    {
                        var _valueLinkDTO = new ValueLinkDTO
                        {
                            ParentAttributeID = (int)Attribute_Enum.SubClass,
                            ParentValueID = DecoderDTO.SubClassID,
                            ChildAttributeID = DecoderStructureDTO.AttributeID                            
                        };
                        var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
                        if (_valueLinkList.Count() == 0 )
                        {
                            _validation_ResultList.Add(new ValidationResultDTO
                            {
                                Result = false,
                                Message = $"{DecoderStructureDTO.AttributeName} needs to be filled",
                                Description = "Please, complete the missing information ",
                            });
                        }
                    }
                    else
                    {
                        if (DecoderStructureDTO.ValueID == null || DecoderStructureDTO.ValueID == 0)
                        {
                            _validation_ResultList.Add(new ValidationResultDTO
                            {
                                Result = false,
                                Message = $"{DecoderStructureDTO.AttributeName} needs to be filled",
                                Description = " Please, complete the missing information ",
                            });
                        }
                    }
                }
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
