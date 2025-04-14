using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

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

    #region Excel Attribute Validation
    public static ValidationResultDTO ExcelAttributeRows_Validation(AttributeDTO AttributeDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<AttributeDTO>(),
            BadRowLinesList = new List<AttributeDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            var _attributeDTO = new AttributeDTO
            {
                ID = AttributeDTO.ID,
                Name = !string.IsNullOrEmpty(AttributeDTO.Name) ? AttributeDTO.Name : "Error, The name is null or empty",
                HasMultipleOptions = AttributeDTO.HasMultipleOptions,
                AddedByID = AttributeDTO.AddedByID,
                AddedDate = DateTime.Now,
                IsActive = true
            };

            // StartsWith checks if any of the properties start with the text 'Error' to identify invalid DTOs.
            if (_attributeDTO.Name.StartsWith("Error"))
                isSucces = false;

            // If it meets all the validations, it saves it in GoodRowLinesList else
            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_attributeDTO);
            else // If not save it BadRowLinesList
                _excelRowDTO.BadRowLinesList.Add(_attributeDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw ex;
        }
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO ExcelAttributeInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<AttributeDTO>(),
            BadRowLinesList = new List<AttributeDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            // We have to declare the type of the list, because GoodRowLinesList is a dynamic type
            // This list contains the attribute that passed the first validation
            var _attributeDTOList = (List<AttributeDTO>)ExcelRowDTO.GoodRowLinesList;
            // We add the previous attribute that did not pass the first validation
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            // If it does not contain data, return _validationResultDTO with _excelRowDTO
            if (_attributeDTOList.Count <= 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We save an array list of the names in lowercase to eliminate names that are repeated with Distinct
            var _attributeDTO = new AttributeDTO { AttributeNameArray = _attributeDTOList.Select(AttributeDTO => AttributeDTO.Name.ToLower()).Distinct().ToArray() };

            // We send the DTOs to the gets so that it brings the data from the db if it exists
            var _attributeList = Attribute_Service.GetAttributeList_Global(_attributeDTO);

            // We create the dictionary (key, value), where the key will be the name in lowercase and the value is the ID
            var _attributeDict = _attributeList.ToDictionary(AttributeDTO => AttributeDTO.Name.ToLower(), AttributeDTO => (int?)AttributeDTO.ID);

            // Before sending the list, we will remove the names that are repeated, this is in case they do not exist in the db, so as not to duplicate them
            _attributeDTOList = _attributeDTOList.GroupBy(AttributeDTO => AttributeDTO.Name.ToLower()).Select(group => group.First()).ToList();

            foreach (var AttributeDTO in _attributeDTOList)
            {
                bool isSuccess = true;

                if (_attributeDict.ContainsKey(AttributeDTO.Name.ToLower()))
                {
                    var _name = AttributeDTO.Name;
                    AttributeDTO.Name = $"Error: The Name: {_name}, already exists";
                    isSuccess = false;
                }

                if (isSuccess)
                {
                    AttributeDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(AttributeDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(AttributeDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw;
        }
        // We have to declare the type of the list, because BadRowLinesList is a dynamic type
        // Before sending the list, we have to sort it by ID
        var _badRowLinesList = (List<AttributeDTO>)_excelRowDTO.BadRowLinesList;
        _excelRowDTO.BadRowLinesList = _badRowLinesList.OrderBy(AttributeDTO => AttributeDTO.ID).ToList();
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion
}

