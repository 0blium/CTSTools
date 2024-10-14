using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
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



}
