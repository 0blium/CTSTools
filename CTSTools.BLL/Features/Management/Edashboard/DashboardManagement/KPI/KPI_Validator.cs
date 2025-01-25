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
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public static ValidationResultDTO ExcelKPIHeaderColumns_Validation(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO {
            Description = "The file has the correct format."
        };
        // Expected headers
        var _expectedHeaders = new string[]
        {
        "name", "description", "unit of measure", "value type",
        "goal", "owner", "responsible", "facility", "equivalence", "category",
        "owner department", "responsible department"
        };
        // Load headers from Excel file
        var _fileHeaders = ExcelDataImport_Service.GetHeadersFromExcel(FileDTO.FileBytes);
        // Use StringBuilder to improve performance when building the message
        var _missingHeaders = new StringBuilder();
        foreach (var Header in _expectedHeaders)
        {
            if (!_fileHeaders.Contains(Header, StringComparer.OrdinalIgnoreCase))
            {
                _missingHeaders.Append(Header + ",<br>");
            }
        }
        // Check for missing headers
        if (_missingHeaders.Length > 0)
        {
            // Delete the last comma and replace it with a period
            _missingHeaders.Length -= 5;
            _missingHeaders.Append(".");
            return new ValidationResultDTO
            {
                Result = false,
                Description = $"The following columns are missing:<br> {_missingHeaders}",
                Message = "Error"
            };
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO ExcelKPIRows_Validation(List<ExcelKPIDTO> ExcelKPIFileDataList)
    {
        ExcelKPIDTO _excelKPIFileValidationDTO = new ExcelKPIDTO();
        var _validationResultDTO = new ValidationResultDTO {
            Description = "The file has the correct format."
        };
        try
        {

            foreach (var _excelKPIFileData in ExcelKPIFileDataList)
            {
                KPIDTO _kPIDTO = new KPIDTO();
                bool isSucces = true;
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.Name))
                {
                    _excelKPIFileData.KPIDTO.Name = "Error, The name is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.UnitOfMeasureName))
                {
                    _excelKPIFileData.KPIDTO.UnitOfMeasureName = "Error, The Unit Of Measure is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.ValueTypeName))
                {
                    _excelKPIFileData.KPIDTO.ValueTypeName = "Error, The Value Type is null or empty";
                    isSucces = false;
                }
                if (_excelKPIFileData.KPIDTO.Goal < 0.0f)
                {
                    _kPIDTO.AddedByName = "Error, The Goal is null or less than zero";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.OwnerName))
                {
                    _excelKPIFileData.KPIDTO.OwnerName = "Error, The Owner is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.ResponsibleName))
                {
                    _excelKPIFileData.KPIDTO.ResponsibleName = "Error, The Responsible is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.FacilityName))
                {
                    _excelKPIFileData.KPIDTO.FacilityName = "Error, The Facility is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.EquivalenceName))
                {
                    _excelKPIFileData.KPIDTO.EquivalenceName = "Error, The Equivalence is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.DashboardCategoryName))
                {
                    _excelKPIFileData.KPIDTO.DashboardCategoryName = "Error, The Category is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.OwnerDepartmentName))
                {
                    _excelKPIFileData.KPIDTO.OwnerDepartmentName = "Error, The Owner Department is null or empty";
                    isSucces = false;
                }
                if (string.IsNullOrEmpty(_excelKPIFileData.KPIDTO.ResponsibleDepartmentName))
                {
                    _excelKPIFileData.KPIDTO.ResponsibleDepartmentName = "Error, The Responsible Department is null or empty";
                    isSucces = false;
                }

                _kPIDTO.ID = _excelKPIFileData.RowIteration;
                _kPIDTO.Name = _excelKPIFileData.KPIDTO.Name;
                _kPIDTO.Description = _excelKPIFileData.KPIDTO.Description;
                _kPIDTO.UnitOfMeasureName = _excelKPIFileData.KPIDTO.UnitOfMeasureName;
                _kPIDTO.ValueTypeName = _excelKPIFileData.KPIDTO.ValueTypeName;
                _kPIDTO.Goal = _excelKPIFileData.KPIDTO.Goal;
                _kPIDTO.OwnerName = _excelKPIFileData.KPIDTO.OwnerName;
                _kPIDTO.ResponsibleName = _excelKPIFileData.KPIDTO.ResponsibleName;
                _kPIDTO.FacilityName = _excelKPIFileData.KPIDTO.FacilityName;
                _kPIDTO.EquivalenceName = _excelKPIFileData.KPIDTO.EquivalenceName;
                _kPIDTO.DashboardCategoryName = _excelKPIFileData.KPIDTO.DashboardCategoryName;
                _kPIDTO.OwnerDepartmentName = _excelKPIFileData.KPIDTO.OwnerDepartmentName;
                _kPIDTO.ResponsibleDepartmentName = _excelKPIFileData.KPIDTO.ResponsibleDepartmentName;
                _kPIDTO.IsActive = true;

                if (isSucces == true)
                {
                    _excelKPIFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelKPIFileValidationDTO.KPIGoodLinesList.Add(_kPIDTO);
                }
                else
                {
                    _excelKPIFileValidationDTO.ValidationResultDTO.Message = _excelKPIFileValidationDTO.ValidationResultDTO.Message;
                    _excelKPIFileValidationDTO.KPIBadLinesList.Add(_kPIDTO);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw ex;
        }
        _validationResultDTO.Data = _excelKPIFileValidationDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO ExcelKPIInformation_Validation(ExcelKPIDTO ExcelKPIFileDataList)
    {
        ExcelKPIDTO _excelKPIFileValidationDTO = new ExcelKPIDTO();
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            // Get the lists
            var _unitOfMeasureDTO = new UnitOfMeasureDTO();
            var _unitOfMeasureList = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(_unitOfMeasureDTO);
            var _valueTypeDTO = new ValueTypeDTO();
            var _valueTypeList = ValueType_Service.GetValueTypeList_Global(_valueTypeDTO);
            var _userDTO = new UserDTO();
            var _userList = User_Service.GetUserList_Global(_userDTO);
            var _facilityDTO = new FacilityDTO();
            var _facilityList = Facility_Service.GetFacilityList_Global(_facilityDTO);
            var _equivalenceDTO = new EquivalenceDTO();
            var _equivalenceList = Equivalence_Service.GetEquivalenceList_Global(_equivalenceDTO);
            var _categoryDTO = new DashboardCategoryDTO();
            var _categoryList = DashboardCategory_Service.GetDashboardCategoryList_Global(_categoryDTO);
            var _departmentDTO = new DepartmentDTO();
            var _departmentList = Department_Service.GetDepartmentList_Global(_departmentDTO);

            // Directory creation
            var _unitOfMeasureDict = new Dictionary<string, int?>();
            var _valueTypeDict = new Dictionary<string, int?>();
            var _ownerDict = new Dictionary<string, int?>();
            var _responsibleDict = new Dictionary<string, int?>();
            var _facilityDict = new Dictionary<string, int?>();
            var _equivalenceDict = new Dictionary<string, int?>();
            var _categoryDict = new Dictionary<string, int?>();
            var _ownerDepartmentDict = new Dictionary<string, int?>();
            var _responsibleDepartmentDict = new Dictionary<string, int?>();

            // Assign values ​​to dictionaries
            foreach (var UnitOfMeasureDTO in _unitOfMeasureList)
            {
                _unitOfMeasureDict[UnitOfMeasureDTO.Name.ToLower()] = UnitOfMeasureDTO.ID;
            }
            foreach (var ValueTypeDTO in _valueTypeList)
            {
                _valueTypeDict[ValueTypeDTO.Name.ToLower()] = ValueTypeDTO.ID;
            }
            foreach (var UserDTO in _userList)
            {
                _ownerDict[UserDTO.Name.ToLower()] = UserDTO.ID;
                _responsibleDict[UserDTO.Name.ToLower()] = UserDTO.ID;
            }
            foreach (var FacilityDTO in _facilityList)
            {
                _facilityDict[FacilityDTO.Name.ToLower()] = FacilityDTO.ID;
            }
            foreach (var EquivalenceDTO in _equivalenceList)
            {
                _equivalenceDict[EquivalenceDTO.Name.ToLower()] = EquivalenceDTO.ID;
            }
            foreach (var CategoryDTO in _categoryList)
            {
                _categoryDict[CategoryDTO.Name.ToLower()] = CategoryDTO.ID;
            }
            foreach (var DepartmentDTO in _departmentList)
            {
                _ownerDepartmentDict[DepartmentDTO.Name.ToLower()] = DepartmentDTO.ID;
                _responsibleDepartmentDict[DepartmentDTO.Name.ToLower()] = DepartmentDTO.ID;
            }

            // Iterate over the lines of the file and assign corresponding IDs
            foreach (var _excelKPIFileData in ExcelKPIFileDataList.KPIGoodLinesList)
            {
                bool isSucces = true;
                if (_unitOfMeasureDict.ContainsKey(_excelKPIFileData.UnitOfMeasureName.ToLower()))
                {
                    _excelKPIFileData.UnitOfMeasureID = _unitOfMeasureDict[_excelKPIFileData.UnitOfMeasureName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.UnitOfMeasureName = "Error: The Unit Of Measure does not exist";
                    isSucces = false;
                }
                if (_valueTypeDict.ContainsKey(_excelKPIFileData.ValueTypeName.ToLower()))
                {
                    _excelKPIFileData.ValueTypeID = _valueTypeDict[_excelKPIFileData.ValueTypeName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.ValueTypeName = "Error: The Value Type does not exist";
                    isSucces = false;
                }
                if (_ownerDict.ContainsKey(_excelKPIFileData.OwnerName.ToLower()))
                {
                    _excelKPIFileData.OwnerID = _ownerDict[_excelKPIFileData.OwnerName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.OwnerName = "Error: The Owner does not exist";
                    isSucces = false;
                }
                if (_responsibleDict.ContainsKey(_excelKPIFileData.ResponsibleName.ToLower()))
                {
                    _excelKPIFileData.ResponsibleID = _responsibleDict[_excelKPIFileData.ResponsibleName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.ResponsibleName = "Error: The Responsible does not exist";
                    isSucces = false;
                }
                if (_facilityDict.ContainsKey(_excelKPIFileData.FacilityName.ToLower()))
                {
                    _excelKPIFileData.FacilityID = _facilityDict[_excelKPIFileData.FacilityName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.FacilityName = "Error: The Facility does not exist";
                    isSucces = false;
                }
                if (_equivalenceDict.ContainsKey(_excelKPIFileData.EquivalenceName.ToLower()))
                {
                    _excelKPIFileData.EquivalenceID = _equivalenceDict[_excelKPIFileData.EquivalenceName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.EquivalenceName = "Error: The Equivalence does not exist";
                    isSucces = false;
                }
                if (_categoryDict.ContainsKey(_excelKPIFileData.DashboardCategoryName.ToLower()))
                {
                    _excelKPIFileData.DashboardCategoryID = _categoryDict[_excelKPIFileData.DashboardCategoryName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.DashboardCategoryName = "Error: The Category does not exist";
                    isSucces = false;
                }
                if (_ownerDepartmentDict.ContainsKey(_excelKPIFileData.OwnerDepartmentName.ToLower()))
                {
                    _excelKPIFileData.OwnerDepartmentID = _ownerDepartmentDict[_excelKPIFileData.OwnerDepartmentName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.OwnerDepartmentName = "Error: The Owner Department does not exist";
                    isSucces = false;
                }
                if (_responsibleDepartmentDict.ContainsKey(_excelKPIFileData.ResponsibleDepartmentName.ToLower()))
                {
                    _excelKPIFileData.ResponsibleDepartmentID = _responsibleDepartmentDict[_excelKPIFileData.ResponsibleDepartmentName.ToLower()].Value;
                }
                else
                {
                    _excelKPIFileData.ResponsibleDepartmentName = "Error: The Responsible Department does not exist";
                    isSucces = false;
                }

                if (isSucces == true)
                {
                    _excelKPIFileData.ID = null;
                    _excelKPIFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelKPIFileValidationDTO.KPIGoodLinesList.Add(_excelKPIFileData);
                }
                else
                {
                    _excelKPIFileValidationDTO.ValidationResultDTO.Message = _excelKPIFileValidationDTO.ValidationResultDTO.Message;
                    _excelKPIFileValidationDTO.KPIBadLinesList.Add(_excelKPIFileData);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw ex;
        }
        _excelKPIFileValidationDTO.KPIBadLinesList.AddRange(ExcelKPIFileDataList.KPIBadLinesList);
        _validationResultDTO.Data = _excelKPIFileValidationDTO;
        return _validationResultDTO;
    }
    #endregion
}
