using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
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


}
