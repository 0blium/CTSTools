using Elmah;
using System;
using System.Collections.Generic;
using CTSTools.BLL.Common;
using System.Linq;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
public class Attribute_Validator
{
    public static ValidationResultDTO CreateAttribute_Validation(AttributeDTO AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(AttributeDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Attribute)}{nameof(AttributeDTO.Name)}",
                });
            }

            //if (AttributeDTO.AddedByID == null || AttributeDTO.AddedByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "AddedByID Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
            // if list contains a error, update main validation result
            var _attributeDTO = new AttributeDTO { Name = AttributeDTO.Name };
            var _sameAttributeDTO = Attribute_Service.GetAttributeList_Global(_attributeDTO).FirstOrDefault();
            if (_sameAttributeDTO != null)
            {
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Duplicated",
                    Description = "There is an attribute with the same name"
                };
            }
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
    public static ValidationResultDTO UpdateAttribute_Validation(AttributeDTO AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (AttributeDTO.ID == null || AttributeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(AttributeDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field is Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Attribute)}{nameof(AttributeDTO.Name)}",
                });
            }

            if (AttributeDTO.LastUpdateByID == null || AttributeDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Last Update By is Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            var _attributeDTO = new AttributeDTO { Name = AttributeDTO.Name };
            var _sameAttributeDTO = Attribute_Service.GetAttributeList_Global(_attributeDTO).Where(w => w.ID != AttributeDTO.ID).FirstOrDefault();
            if (_sameAttributeDTO != null)
            {
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Duplicated",
                    Description = "There is an attribute with the same name."
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
    public static ValidationResultDTO DeleteAttribute_Validation(AttributeDTO AttributeDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (AttributeDTO.ID == null || AttributeDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _valueDTO = new ValueDTO { AttributeID = AttributeDTO.ID };
            var _valueList = Value_Service.GetValueList_Global(_valueDTO);
            if (_valueList.Count() > 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "There are values linked to this attribute. Please, delete them first.",
                });

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

