using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.ComponentType
{
    public class ComponentType_Validator
    {
        public static ValidationResultDTO CreateComponentType_Validation(ComponentTypeDTO ComponentTypeDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (string.IsNullOrEmpty(ComponentTypeDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(ComponentType)}{nameof(ComponentTypeDTO.Name)}",
                    });
                }
                else 
                {
                    var _componentTypeDTO = new ComponentTypeDTO { Name = ComponentTypeDTO.Name };
                    var _sameComponentTypeDTO = ComponentType_Service.GetComponentTypeList_Global(_componentTypeDTO).FirstOrDefault();
                    if (_sameComponentTypeDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an component type with the same name"
                        };
                    }
                }
                if (ComponentTypeDTO.PartTypeID == null || ComponentTypeDTO.PartTypeID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Part Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = "PartType",
                    });
                }

                if (ComponentTypeDTO.AddedByID == null || ComponentTypeDTO.AddedByID == 0)
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
        public static ValidationResultDTO UpdateComponentType_Validation(ComponentTypeDTO ComponentTypeDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (ComponentTypeDTO.ID == null || ComponentTypeDTO.ID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "ID Field Empty",
                        Description = "Please, complete the missing information ",
                    });
                }
                if (string.IsNullOrEmpty(ComponentTypeDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(ComponentType)}{nameof(ComponentTypeDTO.Name)}",
                    });
                }
                else 
                {
                    var _componentTypeDTO = new ComponentTypeDTO { Name = ComponentTypeDTO.Name };
                    var _sameComponentTypeDTO = ComponentType_Service.GetComponentTypeList_Global(_componentTypeDTO).Where(w => w.ID != ComponentTypeDTO.ID).FirstOrDefault();
                    if (_sameComponentTypeDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an brand with the same name."
                        };
                    }
                }
                if (ComponentTypeDTO.PartTypeID == null || ComponentTypeDTO.PartTypeID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Part Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(ComponentTypeDTO)}{nameof(ComponentTypeDTO.PartTypeID)}",
                    });
                }

                if (ComponentTypeDTO.LastUpdateByID == null || ComponentTypeDTO.LastUpdateByID == 0)
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
        public static ValidationResultDTO DeleteComponentType_Validation(ComponentTypeDTO ComponentTypeDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (ComponentTypeDTO.ID == null || ComponentTypeDTO.ID == 0)
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
