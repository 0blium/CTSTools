using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;

public class KPI_Validator
{
    public static ValidationResultDTO CreateKPI_Validation(KPIDTO KPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(KPIDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.Name)}",
                });
            }
            if (KPIDTO.UnitOfMeasureID == null || KPIDTO.UnitOfMeasureID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "UnitOfMeasure Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.ValueTypeID == null || KPIDTO.ValueTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ValueType Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.OwnerID == null || KPIDTO.OwnerID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Owner Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.ResponsibleID == null || KPIDTO.ResponsibleID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Responsible Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.ResponsibleDTO)}",
                });
            }

            if (KPIDTO.GoalRangeID == null || KPIDTO.GoalRangeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "GoalRange Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.GoalRangeDTO)}",
                });
            }
            if (KPIDTO.FacilityID == null || KPIDTO.FacilityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.EquivalenceID == null || KPIDTO.EquivalenceID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Equivalence Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.EquivalenceID)}",
                });
            }
            if (KPIDTO.DashboardCategoryID == null || KPIDTO.DashboardCategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Category Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.DashboardCategoryID)}",
                });
            }


            //if (KPIDTO.CalculationTypeDTO.ID == null || KPIDTO.CalculationTypeDTO.ID == 0 )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "CalculationType Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(KPI)}{nameof(KPIDTO.CalculationTypeDTO)}", 
            //    });
            //}

            if (KPIDTO.AddedByID == null || KPIDTO.AddedByID == 0)
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
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO UpdateKPI_Validation(KPIDTO KPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (KPIDTO.ID == null || KPIDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(KPIDTO.Name))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Name Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.Name)}",
                });
            }
            if (KPIDTO.UnitOfMeasureID == null || KPIDTO.UnitOfMeasureID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "UnitOfMeasure Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.ValueTypeID == null || KPIDTO.ValueTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ValueType Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.OwnerID == null || KPIDTO.OwnerDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Owner Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.OwnerDTO)}",
                });
            }
            if (KPIDTO.ResponsibleID == null || KPIDTO.ResponsibleID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Responsible Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (KPIDTO.GoalRangeID == null || KPIDTO.GoalRangeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "GoalRange Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (KPIDTO.FacilityID == null || KPIDTO.FacilityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.FacilityDTO)}",
                });
            }
            if (KPIDTO.EquivalenceID == null || KPIDTO.EquivalenceID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Equivalence Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(KPI)}{nameof(KPIDTO.EquivalenceDTO)}",
                });
            }

            //if (KPIDTO.CalculationTypeDTO.ID == null || KPIDTO.CalculationTypeDTO.ID == 0 )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "CalculationType Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(KPI)}{nameof(KPIDTO.CalculationTypeDTO)}", 
            //    });
            //}

            if (KPIDTO.LastUpdateByID == null || KPIDTO.LastUpdateByID == 0)
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
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO DeleteKPI_Validation(KPIDTO KPIDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (KPIDTO.ID == null || KPIDTO.ID == 0)
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
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }

}
