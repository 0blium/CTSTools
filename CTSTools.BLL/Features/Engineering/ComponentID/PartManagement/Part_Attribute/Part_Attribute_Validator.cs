using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;

internal class Part_Attribute_Validator
{
    public static ValidationResultDTO CreatePart_Attribute_Validation(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Part_AttributeDTO.DecoderID == null || Part_AttributeDTO.DecoderID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Decoder Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.PartID == null || Part_AttributeDTO.PartID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.AttributeID == null || Part_AttributeDTO.AttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.ValueID == null || Part_AttributeDTO.ValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.AddedByID == null || Part_AttributeDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdatePart_Attribute_Validation(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Part_AttributeDTO.ID == null || Part_AttributeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            // Field Validation
            if (Part_AttributeDTO.DecoderID == null || Part_AttributeDTO.DecoderID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Decoder Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.PartID == null || Part_AttributeDTO.PartID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.AttributeID == null || Part_AttributeDTO.AttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Part_AttributeDTO.ValueID == null || Part_AttributeDTO.ValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (Part_AttributeDTO.LastUpdateByID == null || Part_AttributeDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeletePart_Attribute_Validation(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Part_AttributeDTO.ID == null || Part_AttributeDTO.ID == 0)
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


    public static ValidationResultDTO ValidatePart_Attribute(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Step 2. Validate Attribute Values

            var _part_attributeList = Part_Attribute_Service.GetPart_AttributeList_Global(Part_AttributeDTO);
            var _part_attributeGroup = _part_attributeList.GroupBy(g => g.PartID);
            foreach (var item in _part_attributeGroup)
            {
                var _attributeArray = item.Select(s => (int)s.ValueID).OrderBy(o => o).ToArray();
                var _b = Part_AttributeDTO.ValueIDArray.Select(s => (int)s.Value).OrderBy(o => o).ToArray();
                var _c = _attributeArray.Intersect(_b);
                if (_c.SequenceEqual(_b))
                {
                    _validation_ResultDTO = new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated",
                        Description = "This part number is already in the database."
                    };
                    break;
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
