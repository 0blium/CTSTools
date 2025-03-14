using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.SubClass;

public class SubClass_Validator
{
    public static ValidationResultDTO CreateClassFields_Validation(SubClassDTO SubClassDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            //// Field Validation
            if (SubClassDTO.ParentAttributeID == null || SubClassDTO.ParentValueID == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type or Component Type is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });
            }

            if (SubClassDTO.ChildAttributeID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (string.IsNullOrEmpty(SubClassDTO.SubClassValueDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueID = (int)SubClassDTO.ParentValueID,
                ChildAttributeID = SubClassDTO.SubClassValueDTO.AttributeID,
                GetChildValueDTO = true,
            };
            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            var _sameCodeList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Code.ToUpper().Trim().Replace(" ", "") == SubClassDTO.SubClassValueDTO.Code.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == SubClassDTO.ParentValueID).ToList() : null;
            if (_sameCodeList != null)
            {
                if (_sameCodeList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Code",
                        Description = $"Code: {SubClassDTO.SubClassValueDTO.Code} is already on the database."
                    };
                }

            }
            var _sameNameList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Name.ToUpper().Trim().Replace(" ", "") == SubClassDTO.SubClassValueDTO.Name.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == SubClassDTO.ParentValueID).ToList() : null;
            if (_sameNameList != null)
            {
                if (_sameNameList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Name",
                        Description = $"Name: {SubClassDTO.SubClassValueDTO.Name} is already on the database."
                    };
                }
            }
            //if (ClassDTO.AddedByID == null || ClassDTO.AddedByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "AddedByID Field Empty",
            //        Description = " Please, complete the missing information ",
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
    public static ValidationResultDTO UpdateSubClassFields_Validation(SubClassDTO SubClassDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (SubClassDTO.ID == null || SubClassDTO.SubClassValueDTO.ID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Class is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (SubClassDTO.ParentAttributeID == null || SubClassDTO.ParentValueID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Part Type or Component Type is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (SubClassDTO.ChildAttributeID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (SubClassDTO.ChildValueID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            if (string.IsNullOrEmpty(SubClassDTO.SubClassValueDTO.Name))
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                });


            if (SubClassDTO.LastUpdateByID == null || SubClassDTO.LastUpdateByID == 0)
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
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueID = (int)SubClassDTO.ParentValueID,
                ChildAttributeID = SubClassDTO.SubClassValueDTO.AttributeID,
                GetChildValueDTO = true,
            };
            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            var _sameCodeList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Code.ToUpper().Trim().Replace(" ", "") == SubClassDTO.SubClassValueDTO.Code.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == SubClassDTO.ParentValueID && w.ChildValueID != SubClassDTO.SubClassValueDTO.ID).ToList() : null;
            if (_sameCodeList != null)
            {
                if (_sameCodeList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Code",
                        Description = $"Code: {SubClassDTO.SubClassValueDTO.Code} is already on the database."
                    };
                }

            }
            var _sameNameList = _valueLinkList.Count() > 0 ? _valueLinkList.Where(w => w.ChildValueDTO.Name.ToUpper().Trim().Replace(" ", "") == SubClassDTO.SubClassValueDTO.Name.ToUpper().Trim().Replace(" ", "") && w.ParentValueID == SubClassDTO.ParentValueID && w.ChildValueID != SubClassDTO.SubClassValueDTO.ID).ToList() : null;
            if (_sameNameList != null)
            {
                if (_sameNameList.Count() > 0)
                {
                    return new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Duplicated Name",
                        Description = $"Name: {SubClassDTO.SubClassValueDTO.Name} is already on the database."
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

    public static ValidationResultDTO DeleteSubClassFields_Validation(SubClassDTO SubClassDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (SubClassDTO.ID == null || SubClassDTO.SubClassValueDTO.ID == null)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Sub Class is Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Value)}{nameof(ValueDTO.AttributeDTO)}",
                });

            var _decoderDTO = new DecoderDTO { SubClassID = SubClassDTO.SubClassValueDTO.ID };
            if (Decoder_Service.GetDecoderList_Global(_decoderDTO).Count() > 0)
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Action denied",
                    Description = "There are decoders linked to this sub class. Delete them before to do this action.",
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

    #region Excel SubClass Validation
    public static ValidationResultDTO ExcelSubClassRows_Validation(SubClassDTO SubClassDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<SubClassDTO>(),
            BadRowLinesList = new List<SubClassDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            var _subClassDTO = new SubClassDTO { SubClassValueDTO = new ValueDTO() };

            _subClassDTO.ID = SubClassDTO.ID;
            _subClassDTO.SubClassValueDTO.Name = !string.IsNullOrEmpty(SubClassDTO.SubClassValueDTO.Name) ? SubClassDTO.SubClassValueDTO.Name : "Error, The name is null or empty";
            _subClassDTO.SubClassValueDTO.Code = SubClassDTO.SubClassValueDTO.Code;
            _subClassDTO.SubClassValueDTO.Description = SubClassDTO.SubClassValueDTO.Description;
            _subClassDTO.SubClassValueDTO.AttributeID = (int)Attribute_Enum.SubClass;
            _subClassDTO.ParentValueName = !string.IsNullOrEmpty(SubClassDTO.ParentValueName) ? SubClassDTO.ParentValueName : "Error, The class is null or empty";

            _subClassDTO.ParentAttributeName = !string.IsNullOrEmpty(SubClassDTO.ParentAttributeName) ? SubClassDTO.ParentAttributeName : "Error, The Attributes is null or empty";
            _subClassDTO.ChildValueName = !string.IsNullOrEmpty(SubClassDTO.ChildValueName) ? SubClassDTO.ChildValueName : "Error, The Values is null or empty";
            _subClassDTO.ChildAttributeName = !string.IsNullOrEmpty(SubClassDTO.ChildAttributeName) ? SubClassDTO.ChildAttributeName : "Error, The Manufacturers is null or empty";
            _subClassDTO.LastUpdateByName = !string.IsNullOrEmpty(SubClassDTO.LastUpdateByName) ? SubClassDTO.LastUpdateByName : "Error, The identification number is null or empty";
            _subClassDTO.Description = SubClassDTO.Description;

            _subClassDTO.SubClassValueDTO.AddedDate = DateTime.Now;
            _subClassDTO.SubClassValueDTO.AddedByID = SubClassDTO.SubClassValueDTO.AddedByID;
            _subClassDTO.AddedDate = DateTime.Now;
            _subClassDTO.AddedByID = SubClassDTO.SubClassValueDTO.AddedByID;
            _subClassDTO.SubClassValueDTO.IsActive = true;
            _subClassDTO.IsActive = true;

            // StartsWith checks if any of the properties start with the text 'Error' to identify invalid DTOs.
            if (_subClassDTO.SubClassValueDTO.Name.StartsWith("Error") ||
                _subClassDTO.ParentValueName.StartsWith("Error") ||
                _subClassDTO.LastUpdateByName.StartsWith("Error") ||
                _subClassDTO.ParentAttributeName.StartsWith("Error") ||
                _subClassDTO.ChildValueName.StartsWith("Error") ||
                _subClassDTO.ChildAttributeName.StartsWith("Error"))
                isSucces = false;

            // If it meets all the validations, it saves it in GoodRowLinesList else
            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_subClassDTO);
            else // If not save it BadRowLinesList
                _excelRowDTO.BadRowLinesList.Add(_subClassDTO);
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
    public static ValidationResultDTO ExcelSubClassInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<SubClassDTO>(),
            BadRowLinesList = new List<SubClassDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            // We have to declare the type of the list, because GoodRowLinesList is a dynamic type
            // This list contains the SubClass that passed the first validation
            var _subClassDTOList = (List<SubClassDTO>)ExcelRowDTO.GoodRowLinesList;
            // We add the previous SubClass that did not pass the first validation
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            // If it does not contain data, return _validationResultDTO with _excelRowDTO
            if (_subClassDTOList.Count <= 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We save an array list of the names in lowercase to eliminate names that are repeated with Distinct
            var _subClassDTO = new ValueDTO { AttributeID = (int)Attribute_Enum.Class, ValueNameArray = _subClassDTOList.Select(SubClassDTO => SubClassDTO.ParentValueName.ToLower()).Distinct().ToArray() };
            var _valueNameDTO = new ValueDTO { ValueNameArray = _subClassDTOList.SelectMany(SubClassDTO => SubClassDTO.ChildValueName.Trim().Split(',')).Select(ChildValueName => ChildValueName.Trim()).Distinct().ToArray(), AttributeNameArray = _subClassDTOList.SelectMany(SubClassDTO => SubClassDTO.ParentAttributeName.Trim().Split(',')).Select(ParentAttributeName => ParentAttributeName.Trim()).Distinct().ToArray() };
            var _supplierNameDTO = new SupplierDTO { SupplierNameArray = _subClassDTOList.SelectMany(SubClassDTO => SubClassDTO.ChildAttributeName.Trim().Split(',')).Select(ChildAttributeName => ChildAttributeName.Trim()).Distinct().ToArray() };
            var _partDTO = new PartDTO { MfgPartNumberArray = _subClassDTOList.SelectMany(SubClassDTO => SubClassDTO.LastUpdateByName.Trim().Split(',')).Select(PartNumber => PartNumber.Trim()).Distinct().ToArray() };

            // We send the DTOs to the gets so that it brings the data from the db if it exists
            var _subClassList = Value_Service.GetValueList_Global(_subClassDTO);
            var _valueList = Value_Service.GetValueList_Global(_valueNameDTO);
            var _supplierList = Supplier_Service.GetSupplierList_Global(_supplierNameDTO);
            var _partList = Part_Service.GetPartList_Global(_partDTO);

            // We save an list of the ParentValueID to eliminate ParentValueID that are repeated with Distinct and Where filters the list elements so that it takes those that are not null
            var _parentValueIDList = _subClassDTOList.Where(SubClassDTO => SubClassDTO.ParentValueID != null).Select(SubClassDTO => SubClassDTO.ParentValueID).Distinct().ToList();
            _parentValueIDList.AddRange(_subClassList.Select(SubClassDTO => SubClassDTO.ID).Distinct());

            var _valueLinkDTO = new ValueLinkDTO
            {
                ParentAttributeID = (int)Attribute_Enum.Class,
                ParentValueIDArray = _parentValueIDList.ToArray(),
                ChildAttributeIDArray = _subClassDTOList.Select(SubClassDTO => SubClassDTO.SubClassValueDTO.AttributeID).Distinct().ToArray(),
                GetChildValueDTO = true,
            };

            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);

            // We create the dictionary (key, value), where the key will be the name in lowercase and the value is the ID
            var _classDict = _subClassList.ToDictionary(SubClassDTO => SubClassDTO.Name.ToLower(), SubClassDTO => (int?)SubClassDTO.ID);

            var _attributeGrouped = _valueList
            .GroupBy(item => item.AttributeName) // Agrupamos por AttributeID
            .ToDictionary(group => group.Key, group => group.Select(item => item.Name).ToList());

            var _valueDict = _valueList.ToDictionary(ValueDTO => ValueDTO.Name.ToLower(), ValueDTO => ValueDTO.ID);
            var _supplierDict = _supplierList.ToDictionary(SupplierDTO => SupplierDTO.Name.ToLower(), SupplierDTO => (int?)SupplierDTO.ID);
            var _partDict = _partList.ToDictionary(PartDTO => PartDTO.MfgPartNumber.ToLower());
            var _valueLinkCodeDict = _valueLinkList.ToDictionary(SubClassDTO => SubClassDTO.ChildValueDTO.Code.ToLower().Trim().Replace(" ", ""), SubClassDTO => (int?)SubClassDTO.ParentValueID);
            var _valueLinkNameDict = _valueLinkList.ToDictionary(SubClassDTO => SubClassDTO.ChildValueDTO.Name.ToLower().Trim().Replace(" ", ""), SubClassDTO => (int?)SubClassDTO.ParentValueID);

            foreach (var SubClassDTO in _subClassDTOList)
            {
                bool isSuccess = true;
                var _valueNameList = SubClassDTO.ChildValueName.Trim().Split(',').Select(ChildValueName => ChildValueName.Trim()).ToList();
                var _supplierNameList = SubClassDTO.ChildAttributeName.Trim().Split(',').Select(ChildAttributeName => ChildAttributeName.Trim()).ToList();
                var _partNumberList = SubClassDTO.LastUpdateByName.Trim().Split(',').Select(PartNumber => PartNumber.Trim()).ToList();

                // we use TryGetValue to try to get the value associated with the key from the dictionary,
                // If the value of ParentValueName is found, it is assigned with the corresponding value (ID) from the dictionary.
                // 'out' keyword indicates that ClassID is an output parameter, if the name is not found, save the error message.
                if (_classDict.TryGetValue(SubClassDTO.ParentValueName.ToLower(), out int? ClassID))
                {
                    SubClassDTO.ParentAttributeID = (int)Attribute_Enum.Class;
                    SubClassDTO.ParentValueID = ClassID;
                    SubClassDTO.ChildAttributeID = (int)Attribute_Enum.SubClass;
                }
                else 
                    { SubClassDTO.ParentValueName = "Error: The class does not exist"; isSuccess = false; }

                // The 'out' keyword indicates that ParentValueCodeID is an output parameter; if the name is found, check the dictionary ID with that of the ParentValueID of ClassDTO
                if (_valueLinkCodeDict.TryGetValue(SubClassDTO.SubClassValueDTO.Code.ToLower().Trim().Replace(" ", ""), out int? ParentValueCodeID) && ParentValueCodeID == SubClassDTO.ParentValueID)
                {
                    var _subClassValueName = SubClassDTO.SubClassValueDTO.Code;
                    SubClassDTO.SubClassValueDTO.Code = $"{_subClassValueName} is already on the database";
                    isSuccess = false;
                }
                if (_valueLinkNameDict.TryGetValue(SubClassDTO.SubClassValueDTO.Name.ToLower().Trim().Replace(" ", ""), out int? ParentValueNameID) && ParentValueNameID == SubClassDTO.ParentValueID)
                {
                    var _subClassValueName = SubClassDTO.SubClassValueDTO.Name;
                    SubClassDTO.SubClassValueDTO.Name = $"{_subClassValueName} is already on the database";
                    isSuccess = false;
                }
                if (_partDict.ContainsKey(SubClassDTO.LastUpdateByName)) 
                {
                    var _mfgPartNumber = SubClassDTO.LastUpdateByName;
                    SubClassDTO.LastUpdateByName = $"Error: The {_mfgPartNumber} are already in the database";
                }

                var _valueIDList = new List<int?>();
                for (int i = 0; _valueNameList.Count > i; i++)
                {
                    if (_valueDict.TryGetValue(_valueNameList[i].ToLower(), out int? ValueID))
                    {
                        _valueIDList.Add(ValueID);
                    }
                    else
                    {
                        SubClassDTO.ChildValueName = "";
                        SubClassDTO.ChildValueName += $"Error: The {_valueNameList[i].ToLower()} does not exist ";
                        isSuccess = false; 
                    }
                }
                if(_valueIDList.Count > 0)
                    SubClassDTO.SubClassValueDTO.ValueIDArray = _valueIDList.ToArray();

                var _supplierIDList = new List<int?>();
                for (int i = 0; _supplierNameList.Count > i; i++)
                {
                    if (_supplierDict.TryGetValue(_supplierNameList[i].ToLower(), out int? SupplierID))
                    {
                        _supplierIDList.Add(SupplierID);
                    }
                    else
                    {
                        SubClassDTO.ChildAttributeName = "";
                        SubClassDTO.ChildAttributeName += $"Error: The {_supplierNameList[i].ToLower()} does not exist"; 
                        isSuccess = false;
                    }
                }
                if (_valueIDList.Count > 0)
                    SubClassDTO.ValueLinkIDArray = _supplierIDList.ToArray();

                int _combinations = _attributeGrouped.Values
                .Select(group => group.Count(_valueNameList.Contains))
                .Where(count => count > 0) // Ignoramos los grupos vacíos
                .Aggregate(1, (a, b) => a * b);

                if (_partNumberList.Count < _combinations) 
                {
                    SubClassDTO.LastUpdateByName = $"Error: The part numbers are less than combinations of values";
                    isSuccess = false;
                }

                if (isSuccess)
                {
                    SubClassDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(SubClassDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(SubClassDTO);
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
        var _badRowLinesList = (List<SubClassDTO>)_excelRowDTO.BadRowLinesList;
        _excelRowDTO.BadRowLinesList = _badRowLinesList.OrderBy(SubClassDTO => SubClassDTO.ID).ToList();
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion
}
