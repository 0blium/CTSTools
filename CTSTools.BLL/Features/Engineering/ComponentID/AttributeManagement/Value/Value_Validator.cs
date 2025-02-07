using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;

public class Value_Validator
{
    public static ValidationResultDTO CreateValue_Validation(ValueDTO ValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (ValueDTO.AttributeID == null || ValueDTO.AttributeID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Attribute Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}", 
                });
            }
            if (string.IsNullOrEmpty(ValueDTO.Name) )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Name Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.Name)}", 
                });
            }

            if (ValueDTO.AddedByID == null || ValueDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateValue_Validation(ValueDTO ValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (ValueDTO.ID == null || ValueDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (ValueDTO.AttributeID == null || ValueDTO.AttributeID == 0 )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Attribute Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}", 
                });
            }
            if (string.IsNullOrEmpty(ValueDTO.Name) )
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false, 
                    Message = "Name Field Empty", 
                    Description = " Please, complete the missing information ", 
                    Data = $"{nameof(Value)}{nameof(ValueDTO.Name)}", 
                });
            }
            
            //if (ValueDTO.LastUpdateByID == null || ValueDTO.LastUpdateByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "LastUpdateByID Field Empty",
            //        Description = "Please, complete the missing information ",
            //    });
            //}

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
    public static ValidationResultDTO DeleteValue_Validation(ValueDTO ValueDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            
            // Field Validation
            if (ValueDTO.ID == null || ValueDTO.ID == 0)
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
    public static ValidationResultDTO CreateMultiple_Validation(List<ValueDTO> ValueList)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            foreach (var ValueDTO in ValueList)
            {
                // Field Validation
                ValueDTO.AddedDate = DateTime.Now;
                _validation_ResultList.Add(CreateValue_Validation(ValueDTO));
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
    public static ValidationResultDTO DeleteMultiple_Validation(List<ValueDTO> ValueList)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            foreach (var ValueDTO in ValueList) 
            {
                // Field Validation
                _validation_ResultList.Add(DeleteValue_Validation(ValueDTO));
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

    #region Excel Value Validation
    public static ValidationResultDTO ExcelValueRows_Validation(ValueDTO ValueDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<ValueDTO>(),
            BadRowLinesList = new List<ValueDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            var _ValueDTO = new ValueDTO
            {
                ID = ValueDTO.ID,
                Name = !string.IsNullOrEmpty(ValueDTO.Name) ? ValueDTO.Name : "Error, The name is null or empty",
                Code = ValueDTO.Code,
                AttributeName = !string.IsNullOrEmpty(ValueDTO.AttributeName) ? ValueDTO.AttributeName : "Error, The attribute is null or empty",
                AddedByID = ValueDTO.AddedByID,
                AddedDate = DateTime.Now,
                IsActive = true
            };

            // StartsWith checks if any of the properties start with the text 'Error' to identify invalid DTOs.
            if (_ValueDTO.Name.StartsWith("Error") || _ValueDTO.AttributeName.StartsWith("Error"))
                isSucces = false;

            // If it meets all the validations, it saves it in GoodRowLinesList else
            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_ValueDTO);
            else // If not save it BadRowLinesList
                _excelRowDTO.BadRowLinesList.Add(_ValueDTO);
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
    public static ValidationResultDTO ExcelValueInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<ValueDTO>(),
            BadRowLinesList = new List<ValueDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            // We have to declare the type of the list, because GoodRowLinesList is a dynamic type
            // This list contains the Value that passed the first validation
            var _valueDTOList = (List<ValueDTO>)ExcelRowDTO.GoodRowLinesList;
            // We add the previous Value that did not pass the first validation
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            // If it does not contain data, return _validationResultDTO with _excelRowDTO
            if (_valueDTOList.Count <= 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We save an array list of the names in lowercase to eliminate names that are repeated with Distinct
            var _attributeDTO = new AttributeDTO { AttributeNameArray = _valueDTOList.Select(ValueDTO => ValueDTO.AttributeName.ToLower()).Distinct().ToArray() };

            // We send the DTOs to the gets so that it brings the data from the db if it exists
            var _attributeList = Attribute_Service.GetAttributeList_Global(_attributeDTO);

            // We create the dictionary (key, value), where the key will be the name in lowercase and the value is the ID
            var _attributeDict = _attributeList.ToDictionary(AttributeDTO => AttributeDTO.Name.ToLower(), AttributeDTO => (int?)AttributeDTO.ID);

            foreach (var ValueDTO in _valueDTOList)
            {
                bool isSuccess = true;

                // we use TryGetValue to try to get the value associated with the key from the dictionary,
                // If the value of AttributeName is found, it is assigned with the corresponding value (ID) from the dictionary.
                // 'out' keyword indicates that AttributeName is an output parameter, if the name is not found, save the error message.
                if (_attributeDict.TryGetValue(ValueDTO.AttributeName.ToLower(), out int? AttributeID)) ValueDTO.AttributeID = AttributeID;
                else { ValueDTO.AttributeName = "Error: The Attribute does not exist"; isSuccess = false; }

                if (isSuccess)
                {
                    ValueDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(ValueDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(ValueDTO);
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
        var _badRowLinesList = (List<ValueDTO>)_excelRowDTO.BadRowLinesList;
        _excelRowDTO.BadRowLinesList = _badRowLinesList.OrderBy(ValueDTO => ValueDTO.ID).ToList();
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion
}
