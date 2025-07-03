using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartType
{
    public class PartType_Validator
    {
        public static ValidationResultDTO CreatePartType_Validation(PartTypeDTO PartTypeDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (string.IsNullOrEmpty(PartTypeDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(PartType)}{nameof(PartTypeDTO.Name)}",
                    });
                }
                else 
                {
                    var _partTypeDTO = new PartTypeDTO { Name = PartTypeDTO.Name };
                    var _samePartTypeDTO = PartType_Service.GetPartTypeList_Global(_partTypeDTO).FirstOrDefault();
                    if (_samePartTypeDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an part type with the same name"
                        };
                    }
                }

                if (PartTypeDTO.AddedByID == null || PartTypeDTO.AddedByID == 0)
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
        public static ValidationResultDTO UpdatePartType_Validation(PartTypeDTO PartTypeDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (PartTypeDTO.ID == null || PartTypeDTO.ID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "ID Field Empty",
                        Description = "Please, complete the missing information ",
                    });
                }
                if (string.IsNullOrEmpty(PartTypeDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(PartType)}{nameof(PartTypeDTO.Name)}",
                    });
                }
                else 
                {
                    var _partTypeDTO = new PartTypeDTO { Name = PartTypeDTO.Name };
                    var _samePartTypeDTO = PartType_Service.GetPartTypeList_Global(_partTypeDTO).Where(w => w.ID != PartTypeDTO.ID).FirstOrDefault();
                    if (_samePartTypeDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an part type with the same name."
                        };
                    }
                }

                if (PartTypeDTO.LastUpdateByID == null || PartTypeDTO.LastUpdateByID == 0)
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
        public static ValidationResultDTO DeletePartType_Validation(PartTypeDTO PartTypeDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (PartTypeDTO.ID == null || PartTypeDTO.ID == 0)
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
