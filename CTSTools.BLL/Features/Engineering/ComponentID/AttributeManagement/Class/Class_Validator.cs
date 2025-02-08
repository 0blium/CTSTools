using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;

public class Class_Validator
{
    public static ValidationResultDTO CreateClassFields_Validation(ClassDTO ClassDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (ClassDTO.ParentAttributeID == null || ClassDTO.ParentValueID == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type or Component Type is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });
            }

            if (ClassDTO.ChildAttributeID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (string.IsNullOrEmpty(ClassDTO.ClassValueDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (ClassDTO.AddedByID == null || ClassDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentValueID = (int)ClassDTO.ParentValueID,
                ChildAttributeID = ClassDTO.ClassValueDTO.AttributeID,
                GetChildValueDTO = true,
            };
            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            var _sameCodeList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Code.ToUpper().Trim().Replace(" ", "") == ClassDTO.ClassValueDTO.Code.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == ClassDTO.ParentValueID).ToList() : null;
            if (_sameCodeList != null)
            {
                if (_sameCodeList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Code",
                        Description = $"Code: {ClassDTO.ClassValueDTO.Code} is already on the database."
                    };
                }

            }
            var _sameNameList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Name.ToUpper().Trim().Replace(" ", "") == ClassDTO.ClassValueDTO.Name.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == ClassDTO.ParentValueID).ToList() : null;
            if (_sameNameList != null)
            {
                if (_sameNameList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Name",
                        Description = $"Name: {ClassDTO.ClassValueDTO.Name} is already on the database."
                    };
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
    public static ValidationResultDTO UpdateClassFields_Validation(ClassDTO ClassDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (ClassDTO.ID == null || ClassDTO.ClassValueDTO.ID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Class is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (ClassDTO.ParentAttributeID == null || ClassDTO.ParentValueID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type or Component Type is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (ClassDTO.ChildAttributeID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (ClassDTO.ChildValueID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (string.IsNullOrEmpty(ClassDTO.ClassValueDTO.Name))
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                });
            if (string.IsNullOrEmpty(ClassDTO.ClassValueDTO.Code))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Code Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentValueID = (int)ClassDTO.ParentValueID,
                ChildAttributeID = ClassDTO.ClassValueDTO.AttributeID,
                GetChildValueDTO = true,
            };
            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            var _sameCodeList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Code.ToUpper().Trim().Replace(" ", "") == ClassDTO.ClassValueDTO.Code.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == ClassDTO.ParentValueID && w.ChildValueID != ClassDTO.ClassValueDTO.ID).ToList() : null;
            if (_sameCodeList != null)
            {
                if (_sameCodeList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Code",
                        Description = $"Code: {ClassDTO.ClassValueDTO.Code} is already on the database."
                    };
                }

            }
            var _sameNameList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Name.ToUpper().Trim().Replace(" ", "") == ClassDTO.ClassValueDTO.Name.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == ClassDTO.ParentValueID && w.ChildValueID != ClassDTO.ClassValueDTO.ID).ToList() : null;
            if (_sameNameList != null)
            {
                if (_sameNameList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Name",
                        Description = $"Name: {ClassDTO.ClassValueDTO.Name} is already on the database."
                    };
                }

            }

            //if (ClassDTO.LastUpdateByID == null || ClassDTO.LastUpdateByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "AddedByID Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}

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

    public static ValidationResultDTO DeleteClassFields_Validation(ClassDTO ClassDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (ClassDTO.ID == null || ClassDTO.ClassValueDTO.ID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Class is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            var _classDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueID = ClassDTO.ClassValueDTO.ID,
                ChildAttributeID = (int)Attribute_Enum.SubClass,
            };
            if (ValueLink_Service.GetValueLinkList_Global(_classDTO).Count() > 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "There are sub classes linked to this class. Delete them before to do this action.",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
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
    public static ValidationResultDTO CreateMultiple_Validation(List<ClassDTO> ClassList)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            foreach (var ClassDTO in ClassList) 
            {
                _validation_ResultDTO = CreateClassFields_Validation(ClassDTO);
                if(!_validation_ResultDTO.Result)
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

    #region Excel Class Validation
    public static ValidationResultDTO ExcelClassRows_Validation(ClassDTO ClassDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<ClassDTO>(),
            BadRowLinesList = new List<ClassDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            var _classDTO = new ClassDTO { ClassValueDTO = new ValueDTO() };

            _classDTO.ID = ClassDTO.ID;
            _classDTO.ClassValueDTO.Name = !string.IsNullOrEmpty(ClassDTO.ClassValueDTO.Name) ? ClassDTO.ClassValueDTO.Name : "Error, The name is null or empty";
            _classDTO.ClassValueDTO.Code = ClassDTO.ClassValueDTO.Code;
            _classDTO.ClassValueDTO.Description = ClassDTO.ClassValueDTO.Description;
            _classDTO.PartTypeDTO.Name = ClassDTO.PartTypeDTO.Name;
            _classDTO.ComponentTypeDTO.Name = ClassDTO.ComponentTypeDTO.Name;
            _classDTO.AddedByID = ClassDTO.AddedByID;
            _classDTO.AddedDate = DateTime.Now;
            _classDTO.ClassValueDTO.AddedByID = ClassDTO.AddedByID;
            _classDTO.ClassValueDTO.AddedDate = DateTime.Now;
            _classDTO.ClassValueDTO.IsActive = true;
            _classDTO.IsActive = true;

            if (string.IsNullOrEmpty(ClassDTO.PartTypeDTO.Name))
            {
                _classDTO.PartTypeDTO.Name = "Error, The Part Type is null or empty";
            }
            else
            {
                _classDTO.ClassValueDTO.AttributeID = (int)Attribute_Enum.Class;
                if (ClassDTO.PartTypeDTO.Name == nameof(Value_Enum.PartTypeValue_Enum.Purchased))
                {
                    _classDTO.ParentAttributeID = (int)Attribute_Enum.PartType;
                    _classDTO.ParentValueID = (int)Value_Enum.PartTypeValue_Enum.Purchased;
                    _classDTO.ChildAttributeID = (int)Attribute_Enum.Class;
                }
                else if (ClassDTO.PartTypeDTO.Name == nameof(Value_Enum.PartTypeValue_Enum.Manufactured))
                {
                    if (string.IsNullOrEmpty(ClassDTO.ComponentTypeDTO.Name))
                    {
                        _classDTO.ComponentTypeDTO.Name = "Error, The Component Type is null or empty";
                    }
                    else 
                    {
                        _classDTO.ParentAttributeID = (int)Attribute_Enum.ComponentType;
                        _classDTO.ChildAttributeID = (int)Attribute_Enum.Class;
                    }
                }
                else
                {
                    _classDTO.PartTypeDTO.Name = "Error, The Part Type is different name of Purchased or Manufactured";
                }
            }

            // StartsWith checks if any of the properties start with the text 'Error' to identify invalid DTOs.
            if (_classDTO.ClassValueDTO.Name.StartsWith("Error")||
                _classDTO.PartTypeDTO.Name.StartsWith("Error")||
                _classDTO.ComponentTypeDTO.Name.StartsWith("Error"))
                isSucces = false;

            // If it meets all the validations, it saves it in GoodRowLinesList else
            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_classDTO);
            else // If not save it BadRowLinesList
                _excelRowDTO.BadRowLinesList.Add(_classDTO);
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
    public static ValidationResultDTO ExcelClassInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<ClassDTO>(),
            BadRowLinesList = new List<ClassDTO>()
        };
        //var _classDTOList = new List<ClassDTO>();
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            // We have to declare the type of the list, because GoodRowLinesList is a dynamic type
            // This list contains the Class that passed the first validation
            var _classDTOGoodLinesList = (List<ClassDTO>)ExcelRowDTO.GoodRowLinesList;
            // We add the previous Class that did not pass the first validation
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            // If it does not contain data, return _validationResultDTO with _excelRowDTO
            if (_classDTOGoodLinesList.Count <= 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We save an array list of the names in lowercase to eliminate names that are repeated with Distinct
            var _componentTypeDTO = new ValueDTO { AttributeID = (int)Attribute_Enum.ComponentType, ValueNameArray = _classDTOGoodLinesList.Select(ClassDTO => ClassDTO.ComponentTypeDTO.Name.ToLower()).Distinct().ToArray() };

            // We send the DTOs to the gets so that it brings the data from the db if it exists
            var _componentTypeList = Value_Service.GetValueList_Global(_componentTypeDTO);

            // We save an list of the ParentValueID to eliminate ParentValueID that are repeated with Distinct and Where filters the list elements so that it takes those that are not null
            var _parentValueIDList = _classDTOGoodLinesList.Where(ClassDTO => ClassDTO.ParentValueID != null).Select(ClassDTO => ClassDTO.ParentValueID).Distinct().ToList();
            _parentValueIDList.AddRange(_componentTypeList.Select(ComponentTypeDTO => ComponentTypeDTO.ID).Distinct());

            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentValueIDArray = _parentValueIDList.ToArray(),
                ChildAttributeIDArray = _classDTOGoodLinesList.Select(ClassDTO => ClassDTO.ClassValueDTO.AttributeID).Distinct().ToArray(),
                GetChildValueDTO = true,
            };

            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);

            // We create the dictionary (key, value), where the key will be the name in lowercase and the value is the ID
            var _componentTypeDict = _componentTypeList.ToDictionary(ClassDTO => ClassDTO.Name.ToLower(), ClassDTO => (int?)ClassDTO.ID);
            var _valueLinkCodeDict = _valueLinkList.ToDictionary(ClassDTO => ClassDTO.ChildValueDTO.Code.ToLower().Trim().Replace(" ", ""), ClassDTO => (int?)ClassDTO.ParentValueID);
            var _valueLinkNameDict = _valueLinkList.ToDictionary(ClassDTO => ClassDTO.ChildValueDTO.Name.ToLower().Trim().Replace(" ", ""), ClassDTO => (int?)ClassDTO.ParentValueID);

            foreach (var ClassDTO in _classDTOGoodLinesList)
            {
                bool isSuccess = true;

                // we use TryGetValue to try to get the value associated with the key from the dictionary,
                // If the value of ComponentTypeDTO.Name is found, it is assigned with the corresponding value (ID) from the dictionary.
                // 'out' keyword indicates that ComponentTypeID is an output parameter, if the name is not found, save the error message.
                if (ClassDTO.PartTypeDTO.Name == nameof(Value_Enum.PartTypeValue_Enum.Manufactured))
                {
                    if (_componentTypeDict.TryGetValue(ClassDTO.ComponentTypeDTO.Name.ToLower(), out int? ComponentTypeID))
                    {
                        ClassDTO.ParentValueID = ComponentTypeID;
                    }
                    else { ClassDTO.ComponentTypeDTO.Name = "Error: The Component Type does not exist"; isSuccess = false; }
                }
                // The 'out' keyword indicates that ParentValueCodeID is an output parameter; if the name is found, check the dictionary ID with that of the ParentValueID of ClassDTO
                if (_valueLinkCodeDict.TryGetValue(ClassDTO.ClassValueDTO.Code.ToLower().Trim().Replace(" ", ""), out int? ParentValueCodeID) && ParentValueCodeID == ClassDTO.ParentValueID) 
                {
                    var _classValueName = ClassDTO.ClassValueDTO.Code;
                    ClassDTO.ClassValueDTO.Code = $"Code: {_classValueName} is already on the database.";
                    isSuccess = false;
                }
                if (_valueLinkNameDict.TryGetValue(ClassDTO.ClassValueDTO.Name.ToLower().Trim().Replace(" ", ""), out int? ParentValueNameID) && ParentValueNameID == ClassDTO.ParentValueID)
                {
                    var _classValueName = ClassDTO.ClassValueDTO.Name;
                    ClassDTO.ClassValueDTO.Name = $"Name: {_classValueName} is already on the database.";
                    isSuccess = false;
                }

                if (isSuccess)
                {
                    ClassDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(ClassDTO);
                }
                else
                        _excelRowDTO.BadRowLinesList.Add(ClassDTO);
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
        var _badRowLinesList = (List<ClassDTO>)_excelRowDTO.BadRowLinesList;
        _excelRowDTO.BadRowLinesList = _badRowLinesList.OrderBy(ClassDTO => ClassDTO.ID).ToList();
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion

}
