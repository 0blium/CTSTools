using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.XtraGauges.Core.Model;
using Elmah;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    #region Excel KPI Validation
    public static ValidationResultDTO ExcelKPIRows_Validation(KPIDTO KPIDTO)
    {
        var _excelRowDTO = new ExcelRowDTO();
        _excelRowDTO.GoodRowLinesList = new List<KPIDTO>();
        _excelRowDTO.BadRowLinesList = new List<KPIDTO>();
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            KPIDTO _kPIDTO = new KPIDTO
            {
                ID = KPIDTO.ID,
                Name = !string.IsNullOrEmpty(KPIDTO.Name) ? KPIDTO.Name : "Error, The name is null or empty",
                UnitOfMeasureName = !string.IsNullOrEmpty(KPIDTO.UnitOfMeasureName) ? KPIDTO.UnitOfMeasureName : "Error, The Unit Of Measure is null or empty",
                ValueTypeName = !string.IsNullOrEmpty(KPIDTO.ValueTypeName) ? KPIDTO.ValueTypeName : "Error, The Value Type is null or empty",
                Goal = KPIDTO.Goal,
                OwnerName = !string.IsNullOrEmpty(KPIDTO.OwnerName) ? KPIDTO.OwnerName : "Error, The Owner is null or empty",
                ResponsibleName = !string.IsNullOrEmpty(KPIDTO.ResponsibleName) ? KPIDTO.ResponsibleName : "Error, The Responsible is null or empty",
                FacilityName = !string.IsNullOrEmpty(KPIDTO.FacilityName) ? KPIDTO.FacilityName : "Error, The Facility is null or empty",
                EquivalenceName = !string.IsNullOrEmpty(KPIDTO.EquivalenceName) ? KPIDTO.EquivalenceName : "Error, The Equivalence is null or empty",
                DashboardCategoryName = !string.IsNullOrEmpty(KPIDTO.DashboardCategoryName) ? KPIDTO.DashboardCategoryName : "Error, The Category is null or empty",
                OwnerDepartmentName = !string.IsNullOrEmpty(KPIDTO.OwnerDepartmentName) ? KPIDTO.OwnerDepartmentName : "Error, The Owner Department is null or empty",
                ResponsibleDepartmentName = !string.IsNullOrEmpty(KPIDTO.ResponsibleDepartmentName) ? KPIDTO.ResponsibleDepartmentName : "Error, The Responsible Department is null or empty",
                IsActive = true
            };

            if (_kPIDTO.Name.StartsWith("Error") ||
                _kPIDTO.UnitOfMeasureName.StartsWith("Error") ||
                _kPIDTO.ValueTypeName.StartsWith("Error") ||
                _kPIDTO.OwnerName.StartsWith("Error") ||
                _kPIDTO.ResponsibleName.StartsWith("Error") ||
                _kPIDTO.FacilityName.StartsWith("Error") ||
                _kPIDTO.EquivalenceName.StartsWith("Error") ||
                _kPIDTO.DashboardCategoryName.StartsWith("Error") ||
                _kPIDTO.OwnerDepartmentName.StartsWith("Error") ||
                _kPIDTO.ResponsibleDepartmentName.StartsWith("Error") ||
                _kPIDTO.Goal < 0.0f)
            {
                isSucces = false;
            }

            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_kPIDTO);
            else
                _excelRowDTO.BadRowLinesList.Add(_kPIDTO);
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
    public static ValidationResultDTO ExcelKPIInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<KPIDTO>(),
            BadRowLinesList = new List<KPIDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            var _kPIDTOList = (List<KPIDTO>)ExcelRowDTO.GoodRowLinesList;
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);

            if (_kPIDTOList.Count <= 0) 
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We normalize names to lowercase only once
            var _unitOfMeasureDTO = new UnitOfMeasureDTO { UnitOfMeasureNameArray = _kPIDTOList.Select(k => k.UnitOfMeasureName?.ToLower()).Distinct().ToArray() };
            var _valueTypeDTO = new ValueTypeDTO { ValueTypeNameArray = _kPIDTOList.Select(k => k.ValueTypeName?.ToLower()).Distinct().ToArray() };
            var _userDTO = new UserDTO { UserNameArray = _kPIDTOList.SelectMany(k => new[] { k.OwnerName?.ToLower(), k.ResponsibleName?.ToLower() }).Distinct().ToArray() };
            var _facilityDTO = new FacilityDTO { FacilityNameArray = _kPIDTOList.Select(k => k.FacilityName?.ToLower()).Distinct().ToArray() };
            var _equivalenceDTO = new EquivalenceDTO { EquivalenceNameArray = _kPIDTOList.Select(k => k.EquivalenceName?.ToLower()).Distinct().ToArray() };
            var _categoryDTO = new DashboardCategoryDTO { DashboardCategoryNameArray = _kPIDTOList.Select(k => k.DashboardCategoryName?.ToLower()).Distinct().ToArray() };
            var _departmentDTO = new DepartmentDTO { DepartmentNameArray = _kPIDTOList.SelectMany(k => new[] { k.OwnerDepartmentName?.ToLower(), k.ResponsibleDepartmentName?.ToLower() }).Distinct().ToArray() };

            // Go for the data in the db
            var _unitOfMeasureList = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(_unitOfMeasureDTO);
            var _valueTypeList = ValueType_Service.GetValueTypeList_Global(_valueTypeDTO);
            var _userList = User_Service.GetUserList_Global(_userDTO);
            var _facilityList = Facility_Service.GetFacilityList_Global(_facilityDTO);
            var _equivalenceList = Equivalence_Service.GetEquivalenceList_Global(_equivalenceDTO);
            var _categoryList = DashboardCategory_Service.GetDashboardCategoryList_Global(_categoryDTO);
            var _departmentList = Department_Service.GetDepartmentList_Global(_departmentDTO);

            // We create dictionaries with normalized keys (in lowercase)
            var _unitOfMeasureDict = _unitOfMeasureList.ToDictionary(UnitOfMeasureDTO => UnitOfMeasureDTO.Name.ToLower(), UnitOfMeasureDTO => (int?)UnitOfMeasureDTO.ID);
            var _valueTypeDict = _valueTypeList.ToDictionary(ValueTypeDTO => ValueTypeDTO.Name.ToLower(), ValueTypeDTO => (int?)ValueTypeDTO.ID);
            var _userDict = _userList.ToDictionary(UserDTO => UserDTO.Name.ToLower(), UserDTO => (int?)UserDTO.ID);
            var _facilityDict = _facilityList.ToDictionary(FacilityDTO => FacilityDTO.Name.ToLower(), FacilityDTO => (int?)FacilityDTO.ID);
            var _equivalenceDict = _equivalenceList.ToDictionary(EquivalenceDTO => EquivalenceDTO.Name.ToLower(), EquivalenceDTO => (int?)EquivalenceDTO.ID);
            var _categoryDict = _categoryList.ToDictionary(CategoryDTO => CategoryDTO.Name.ToLower(), CategoryDTO => (int?)CategoryDTO.ID);
            var _departmentDict = _departmentList.ToDictionary(DepartmentDTO => DepartmentDTO.Name.ToLower(), DepartmentDTO => (int?)DepartmentDTO.ID);

            foreach (var _kPIDTO in _kPIDTOList)
            {
                bool isSuccess = true;

                // We search the normalized dictionaries without using `ToLower()` on each iteration
                if (_unitOfMeasureDict.TryGetValue(_kPIDTO.UnitOfMeasureName.ToLower(), out int? UnitOfMeasureID)) _kPIDTO.UnitOfMeasureID = UnitOfMeasureID;
                else { _kPIDTO.UnitOfMeasureName = "Error: The Unit Of Measure does not exist"; isSuccess = false; }

                if (_valueTypeDict.TryGetValue(_kPIDTO.ValueTypeName.ToLower(), out int? ValueTypeID)) _kPIDTO.ValueTypeID = ValueTypeID;
                else { _kPIDTO.ValueTypeName = "Error: The Value Type does not exist"; isSuccess = false; }

                if (_userDict.TryGetValue(_kPIDTO.OwnerName.ToLower(), out int? OwnerID)) _kPIDTO.OwnerID = OwnerID;
                else { _kPIDTO.OwnerName = "Error: The Owner does not exist"; isSuccess = false; }

                if (_userDict.TryGetValue(_kPIDTO.ResponsibleName.ToLower(), out int? ResponsibleID)) _kPIDTO.ResponsibleID = ResponsibleID;
                else { _kPIDTO.ResponsibleName = "Error: The Responsible does not exist"; isSuccess = false; }

                if (_facilityDict.TryGetValue(_kPIDTO.FacilityName.ToLower(), out int? FacilityID)) _kPIDTO.FacilityID = FacilityID;
                else { _kPIDTO.FacilityName = "Error: The Facility does not exist"; isSuccess = false; }

                if (_equivalenceDict.TryGetValue(_kPIDTO.EquivalenceName.ToLower(), out int? EquivalenceID)) _kPIDTO.EquivalenceID = EquivalenceID;
                else { _kPIDTO.EquivalenceName = "Error: The Equivalence does not exist"; isSuccess = false; }

                if (_categoryDict.TryGetValue(_kPIDTO.DashboardCategoryName.ToLower(), out int? DashboardCategoryID)) _kPIDTO.DashboardCategoryID = DashboardCategoryID;
                else { _kPIDTO.DashboardCategoryName = "Error: The Category does not exist"; isSuccess = false; }

                if (_departmentDict.TryGetValue(_kPIDTO.OwnerDepartmentName.ToLower(), out int? OwnerDepartmentID)) _kPIDTO.OwnerDepartmentID = OwnerDepartmentID;
                else { _kPIDTO.OwnerDepartmentName = "Error: The Owner Department does not exist"; isSuccess = false; }

                if (_departmentDict.TryGetValue(_kPIDTO.ResponsibleDepartmentName.ToLower(), out int? ResponsibleDepartmentID)) _kPIDTO.ResponsibleDepartmentID = ResponsibleDepartmentID;
                else { _kPIDTO.ResponsibleDepartmentName = "Error: The Owner Department does not exist"; isSuccess = false; }

                if (isSuccess)
                {
                    _kPIDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(_kPIDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(_kPIDTO);
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
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion
}
