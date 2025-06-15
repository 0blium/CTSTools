using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.Class
{
    public class Class_Validator
    {
        public static ValidationResultDTO CreateClass_Validation(ClassDTO ClassDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (string.IsNullOrEmpty(ClassDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(Class)}{nameof(ClassDTO.Name)}",
                    });
                }
                else
                {
                    var _ClassDTO = new ClassDTO { Name = ClassDTO.Name };
                    var _sameClassDTO = Class_Service.GetClassList_Global(_ClassDTO).FirstOrDefault();
                    if (_sameClassDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an component type with the same name"
                        };
                    }
                }
                if (ClassDTO.PartTypeID == null || ClassDTO.PartTypeID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Part Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = "PartType",
                    });
                }
                if (ClassDTO.PartTypeID == (int)Value_Enum.PartTypeValue_Enum.Manufactured && ClassDTO.ComponentTypeID == null || ClassDTO.ComponentTypeID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Component Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = "ComponentType",
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
        public static ValidationResultDTO UpdateClass_Validation(ClassDTO ClassDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (ClassDTO.ID == null || ClassDTO.ID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "ID Field Empty",
                        Description = "Please, complete the missing information ",
                    });
                }
                if (string.IsNullOrEmpty(ClassDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(Class)}{nameof(ClassDTO.Name)}",
                    });
                }
                else
                {
                    var _ClassDTO = new ClassDTO { Name = ClassDTO.Name };
                    var _sameClassDTO = Class_Service.GetClassList_Global(_ClassDTO).Where(w => w.ID != ClassDTO.ID).FirstOrDefault();
                    if (_sameClassDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an brand with the same name."
                        };
                    }
                }
                if (ClassDTO.PartTypeID == null || ClassDTO.PartTypeID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Part Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(ClassDTO)}{nameof(ClassDTO.PartTypeID)}",
                    });
                }

                if (ClassDTO.LastUpdateByID == null || ClassDTO.LastUpdateByID == 0)
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
        public static ValidationResultDTO DeleteClass_Validation(ClassDTO ClassDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (ClassDTO.ID == null || ClassDTO.ID == 0)
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
    }
}
