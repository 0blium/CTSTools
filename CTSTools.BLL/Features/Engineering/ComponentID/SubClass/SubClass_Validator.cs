using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.SubClass;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SubClass
{
    public class SubClass_Validator
    {
        public static ValidationResultDTO CreateSubClass_Validation(SubClassDTO SubClassDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (string.IsNullOrEmpty(SubClassDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(SubClass)}{nameof(SubClassDTO.Name)}",
                    });
                }
                else
                {
                    var _SubClassDTO = new SubClassDTO { Name = SubClassDTO.Name };
                    var _sameSubClassDTO = SubClass_Service.GetSubClassList_Global(_SubClassDTO).FirstOrDefault();
                    if (_sameSubClassDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an component type with the same name"
                        };
                    }
                }
                if (SubClassDTO.ClassID == null || SubClassDTO.ClassID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Part Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = "Class",
                    });
                }

                if (SubClassDTO.AddedByID == null || SubClassDTO.AddedByID == 0)
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
        public static ValidationResultDTO UpdateSubClass_Validation(SubClassDTO SubClassDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (SubClassDTO.ID == null || SubClassDTO.ID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "ID Field Empty",
                        Description = "Please, complete the missing information ",
                    });
                }
                if (string.IsNullOrEmpty(SubClassDTO.Name))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Name Field Empty",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(SubClass)}{nameof(SubClassDTO.Name)}",
                    });
                }
                else
                {
                    var _SubClassDTO = new SubClassDTO { Name = SubClassDTO.Name };
                    var _sameSubClassDTO = SubClass_Service.GetSubClassList_Global(_SubClassDTO).Where(w => w.ID != SubClassDTO.ID).FirstOrDefault();
                    if (_sameSubClassDTO != null)
                    {
                        return new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Duplicated",
                            Description = "There is an brand with the same name."
                        };
                    }
                }
                if (SubClassDTO.ClassID == null || SubClassDTO.ClassID == 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Part Type is not selected",
                        Description = " Please, complete the missing information ",
                        Data = $"{nameof(SubClassDTO)}{nameof(SubClassDTO.ClassID)}",
                    });
                }

                if (SubClassDTO.LastUpdateByID == null || SubClassDTO.LastUpdateByID == 0)
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
        public static ValidationResultDTO DeleteSubClass_Validation(SubClassDTO SubClassDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                // Field Validation
                if (SubClassDTO.ID == null || SubClassDTO.ID == 0)
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
