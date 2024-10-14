using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;

public class ValueLink_Validator
{
    public static ValidationResultDTO CreateValueLink_Validation(ValueLinkDTO ValueLinkDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation

            if (ValueLinkDTO.ChildValueID == null || ValueLinkDTO.ChildValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Child Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ChildAttributeID == null || ValueLinkDTO.ChildAttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Child Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ParentValueID == null || ValueLinkDTO.ParentValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Parent Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ParentAttributeID == null || ValueLinkDTO.ParentAttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Parent Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            //if (ValueLinkDTO.AddedByID == null || ValueLinkDTO.AddedByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "AddedByID Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}

            //var _isduplicatedValidationResultDTO = IsItNotDuplicated(ValueLinkDTO);
            //if (!_isduplicatedValidationResultDTO.Result)
            //{
            //    _validation_ResultList.Add(_isduplicatedValidationResultDTO);
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
    public static ValidationResultDTO UpdateValueLink_Validation(ValueLinkDTO ValueLinkDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (ValueLinkDTO.ID == null || ValueLinkDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ChildValueID == null || ValueLinkDTO.ChildValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Child Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ChildAttributeID == null || ValueLinkDTO.ChildAttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Child Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ParentValueID == null || ValueLinkDTO.ParentValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Parent Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ParentAttributeID == null || ValueLinkDTO.ParentAttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Parent Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            //if (ValueLinkDTO.LastUpdateByID == null || ValueLinkDTO.LastUpdateByID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "LastUpdateByID Field Empty",
            //        Description = "Please, complete the missing information ",
            //    });
            //}
            //var _isduplicatedValidationResultDTO = IsItNotDuplicated(ValueLinkDTO);
            //if (!_isduplicatedValidationResultDTO.Result)
            //{
            //    _validation_ResultDTO.Result = false;
            //    _validation_ResultList.Add(_isduplicatedValidationResultDTO);
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
    public static ValidationResultDTO DeleteValueLink_Validation(ValueLinkDTO ValueLinkDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (ValueLinkDTO.ID == null || ValueLinkDTO.ID == 0)
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

    public static ValidationResultDTO CreateMultipleValueLink_Validation(ValueLinkDTO ValueLinkDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation

            if (ValueLinkDTO.ChildValueIDArray == null || ValueLinkDTO.ChildValueIDArray.Length == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Child Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ChildAttributeID == null || ValueLinkDTO.ChildAttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Child Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ParentValueID == null || ValueLinkDTO.ParentValueID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Parent Value Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (ValueLinkDTO.ParentAttributeID == null || ValueLinkDTO.ParentAttributeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Parent Attribute Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            //if (ValueLinkDTO.AddedByID == null || ValueLinkDTO.AddedByID == 0)
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
    public static ValidationResultDTO IsItNotDuplicated(ValueLinkDTO ValueLinkDTO)
    {
        var ValidationResultDTO = new ValidationResultDTO();
        try
        {
            var _valueLinkDTO = new ValueLinkDTO
            {
                ChildAttributeID = ValueLinkDTO.ChildAttributeID,
                ChildValueID = ValueLinkDTO.ChildValueID,
                ParentValueID = ValueLinkDTO.ParentValueID,
                ParentAttributeID = ValueLinkDTO.ParentAttributeID
            };
            var _valueLinkList = ValueLink_Service.GetValueLinkList_Global(_valueLinkDTO);
            if (_valueLinkList.Count == 0)
                return ValidationResultDTO;
            //Remove value with the same ID
            var _duplicatedvalueLinkList = _valueLinkList.Where(w => w.ID != ValueLinkDTO.ID).ToList();
            if (_duplicatedvalueLinkList.Count > 0)
                return new ValidationResultDTO
                {
                    Result = false,
                    Message = "Duplicated Records" ,
                    Description = $" Parent Value: {_duplicatedvalueLinkList[0].ParentValueName} with Child Value {_duplicatedvalueLinkList[0].ChildValueName} are already in the database"
                   
                };
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return ValidationResultDTO;
    }
}
