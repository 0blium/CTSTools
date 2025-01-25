using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.GoalRange;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;

public class KPI_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateKPI_Global(KPIDTO KPIDTO)
    {
        var _ValidationResultDTO = KPI_Validator.CreateKPI_Validation(KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            KPIDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = KPI_Repository.CreateKPI(KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateKPI_Global(KPIDTO KPIDTO)
    {
        var _ValidationResultDTO = KPI_Validator.UpdateKPI_Validation(KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            KPIDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = KPI_Repository.UpdateKPI(KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteKPI_Global(KPIDTO KPIDTO)
    {
        var _ValidationResultDTO = KPI_Validator.DeleteKPI_Validation(KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = KPI_Repository.DeleteKPI(KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<KPIDTO> GetKPIList_Global(KPIDTO KPIDTO, PagedResultDTO<KPIDTO> PagedResultDTO = null)
    {
        var _kpiglobalList = new List<KPIDTO>();
        try
        {
            var _kpiList = KPI_Repository.GetKPIList(KPIDTO, PagedResultDTO);
            // if KPI is empty, return list
            if (_kpiList.Count() == 0)
            {
                _kpiglobalList = _kpiList;
                return _kpiglobalList;
            }
            if (!KPIDTO.GetUnitOfMeasureDTO && !KPIDTO.GetValueTypeDTO && !KPIDTO.GetGoalRangeDTO && !KPIDTO.GetFacilityDTO && !KPIDTO.GetEquivalenceDTO && !KPIDTO.GetStatusDTO && !KPIDTO.GetCalculationTypeDTO)
            {
                _kpiglobalList = _kpiList;
                return _kpiglobalList;
            }
            _kpiglobalList = GetKPIRelatedData(KPIDTO, _kpiList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _kpiglobalList;
    }



    public static List<KPIDTO> GetKPIRelatedData(KPIDTO KPIDTO, List<KPIDTO> KPIList)
    {
        var _kpiglobalList = new List<KPIDTO>();
        var _unitofmeasureDict = new Dictionary<int?, UnitOfMeasureDTO>();
        var _valuetypeDict = new Dictionary<int?, ValueTypeDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _equivalenceDict = new Dictionary<int?, EquivalenceDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _calculationtypeDict = new Dictionary<int?, CalculationTypeDTO>();

        try
        {
            if (KPIDTO.GetUnitOfMeasureDTO)
            {
                KPIDTO.UnitOfMeasureDTO.UnitOfMeasureIDArray = KPIList.GroupBy(g => g.UnitOfMeasureID)
                        .Select(s => s.Key)
                        .ToArray();

                _unitofmeasureDict = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(KPIDTO.UnitOfMeasureDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetValueTypeDTO)
            {
                KPIDTO.ValueTypeDTO.ValueTypeIDArray = KPIList.GroupBy(g => g.ValueTypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _valuetypeDict = ValueType_Service.GetValueTypeList_Global(KPIDTO.ValueTypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetFacilityDTO)
            {
                KPIDTO.FacilityDTO.FacilityIDArray = KPIList.GroupBy(g => g.FacilityID)
                        .Select(s => s.Key)
                        .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(KPIDTO.FacilityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetEquivalenceDTO)
            {
                KPIDTO.EquivalenceDTO.EquivalenceIDArray = KPIList.GroupBy(g => g.EquivalenceID)
                        .Select(s => s.Key)
                        .ToArray();

                _equivalenceDict = Equivalence_Service.GetEquivalenceList_Global(KPIDTO.EquivalenceDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetStatusDTO)
            {
                KPIDTO.StatusDTO.StatusIDArray = KPIList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(KPIDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetCalculationTypeDTO)
            {
                KPIDTO.CalculationTypeDTO.CalculationTypeIDArray = KPIList.GroupBy(g => g.CalculationTypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _calculationtypeDict = CalculationType_Service.GetCalculationTypeList_Global(KPIDTO.CalculationTypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _kpiDTO in KPIList)
            {
                if (KPIDTO.GetUnitOfMeasureDTO && _unitofmeasureDict.ContainsKey(_kpiDTO.UnitOfMeasureID))
                {
                    _kpiDTO.UnitOfMeasureDTO = _unitofmeasureDict[_kpiDTO.UnitOfMeasureID];
                }
                if (KPIDTO.GetValueTypeDTO && _valuetypeDict.ContainsKey(_kpiDTO.ValueTypeDTO.ID))
                {
                    _kpiDTO.ValueTypeDTO = _valuetypeDict[_kpiDTO.ValueTypeID];
                }
                if (KPIDTO.GetFacilityDTO && _facilityDict.ContainsKey(_kpiDTO.FacilityID))
                {
                    _kpiDTO.FacilityDTO = _facilityDict[_kpiDTO.FacilityID];
                }
                if (KPIDTO.GetEquivalenceDTO && _equivalenceDict.ContainsKey(_kpiDTO.EquivalenceID))
                {
                    _kpiDTO.EquivalenceDTO = _equivalenceDict[_kpiDTO.EquivalenceID];
                }
                if (KPIDTO.GetStatusDTO && _statusDict.ContainsKey(_kpiDTO.StatusID))
                {
                    _kpiDTO.StatusDTO = _statusDict[_kpiDTO.StatusID];
                }
                if (KPIDTO.GetCalculationTypeDTO && _calculationtypeDict.ContainsKey(_kpiDTO.CalculationTypeID))
                {
                    _kpiDTO.CalculationTypeDTO = _calculationtypeDict[_kpiDTO.CalculationTypeID];
                }
                _kpiglobalList.Add(_kpiDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _kpiglobalList;
    }




    public static int GetKPITotalCount(PagedResultDTO<KPIDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = KPI_Repository.GetKPICount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    #region Upload Excel functions
    public static ValidationResultDTO GenerateKPIsFromExcel(FileDTO FileDTO)
    {
        var _excelKPIDTO = new ExcelKPIDTO();
        var _validationResultDTO = new ValidationResultDTO 
        {
            Description = "The record has been create successfully.."
        };
        try
        {
            // step 1. Validate that it is an excel file
            _validationResultDTO = ExcelDataImport_Service.ExcelFile_Validation(FileDTO);
            if (_validationResultDTO.Result) 
            {
                FileDTO.FileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
                _validationResultDTO = KPI_Validator.ExcelKPIHeaderColumns_Validation(FileDTO);
            }
            // step 2. Bring the information from excel
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = GetKPIListFromExcel(FileDTO.FileBytes);
            }
            // step 3. Validate that the list of data comes
            if (_validationResultDTO.Data != null)
            {
                // step 4. Validate which columns have information
                _validationResultDTO = KPI_Validator.ExcelKPIRows_Validation(_validationResultDTO.Data);
                // step 5. Validate that the column information exists in the db
                _validationResultDTO = KPI_Validator.ExcelKPIInformation_Validation(_validationResultDTO.Data);
                _excelKPIDTO = _validationResultDTO.Data;
                if (_excelKPIDTO.KPIGoodLinesList.Count > 0)
                {
                    foreach (var _kPIDTO in _excelKPIDTO.KPIGoodLinesList)
                    {
                        // Step 6. Create records
                        _kPIDTO.AddedByID = FileDTO.ID;
                        _validationResultDTO = CreateKPI_Global(_kPIDTO);
                    }
                }
            }
            else
            {
                return _validationResultDTO;
            }
            _validationResultDTO.Result = _excelKPIDTO.ValidationResultDTO.Result;
            _validationResultDTO.Message = _excelKPIDTO.ValidationResultDTO.Message;
            _validationResultDTO.Description = _excelKPIDTO.ValidationResultDTO.Description;
            _validationResultDTO.Data = _excelKPIDTO;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ValidationResultDTO GetKPIListFromExcel(byte[] FileBytes)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            List<ExcelKPIDTO> _excelKPIDTOList = new List<ExcelKPIDTO>();
            using (var _fileStream = new MemoryStream(FileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                // Fill the dictionary with headings
                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    string headerName = _firstTable.Rows[0][colIndex].ToString().Trim();
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", " ");
                    if (headerName.Equals("NAME", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("DESCRIPTION", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("UNITOFMEASURE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("UNIT OF MEASURE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("VALUETYPE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("VALUE TYPE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("GOAL", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("OWNER", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("RESPONSIBLE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("FACILITY", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("EQUIVALENCE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("CATEGORY", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("OWNERDEPARTMENT", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("OWNER DEPARTMENT", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("RESPONSIBLEDEPARTMENT", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("RESPONSIBLE DEPARTMENT", StringComparison.OrdinalIgnoreCase))
                    {
                        _columnHeaderMap[headerName.ToUpper()] = colIndex;
                    }
                }
                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _excelKPIDTO = new ExcelKPIDTO();
                    bool _haveInfo = false;
                    // Assign values ​​directly using the dictionary
                    if (_columnHeaderMap.TryGetValue("NAME", out int NameIndex))
                    {
                        string _nameValue = row[NameIndex].ToString().Trim();
                        _nameValue = System.Text.RegularExpressions.Regex.Replace(_nameValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.Name = _nameValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("DESCRIPTION", out int DescriptionIndex))
                    {
                        string _descriptionValue = row[DescriptionIndex].ToString().Trim();
                        _descriptionValue = System.Text.RegularExpressions.Regex.Replace(_descriptionValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.Description = _descriptionValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("UNIT OF MEASURE", out int UnitOfMeasureIndex) ||
                        _columnHeaderMap.TryGetValue("UNITOFMEASURE", out UnitOfMeasureIndex))
                    {
                        string _unitOfMeasureIndexValue = row[UnitOfMeasureIndex].ToString().Trim();
                        _unitOfMeasureIndexValue = System.Text.RegularExpressions.Regex.Replace(_unitOfMeasureIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.UnitOfMeasureName = _unitOfMeasureIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("VALUE TYPE", out int ValueTypeIndex) ||
                        _columnHeaderMap.TryGetValue("VALUETYPE", out ValueTypeIndex))
                    {
                        string _valueTypeIndexValue = row[ValueTypeIndex].ToString().Trim();
                        _valueTypeIndexValue = System.Text.RegularExpressions.Regex.Replace(_valueTypeIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.ValueTypeName = _valueTypeIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("GOAL", out int GoalIndex))
                    {
                        string _goalValue = row[GoalIndex].ToString().Trim();
                        _goalValue = System.Text.RegularExpressions.Regex.Replace(_goalValue, @"\s+", " ");
                        if (float.TryParse(_goalValue, out float Goal))
                        {
                            _excelKPIDTO.KPIDTO.Goal = Goal;
                            _haveInfo = true;
                        }
                        else
                        {
                            throw new InvalidCastException($"The value for Goal in Row {rowIndex + 1} is not a valid number.");
                        }
                    }
                    if (_columnHeaderMap.TryGetValue("OWNER", out int OwnerIndex))
                    {
                        string _ownerIndexValue = row[OwnerIndex].ToString().Trim();
                        _ownerIndexValue = System.Text.RegularExpressions.Regex.Replace(_ownerIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.OwnerName = _ownerIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("RESPONSIBLE", out int ResponsibleIndex))
                    {
                        string _responsibleIndexValue = row[ResponsibleIndex].ToString().Trim();
                        _responsibleIndexValue = System.Text.RegularExpressions.Regex.Replace(_responsibleIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.ResponsibleName = _responsibleIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("FACILITY", out int FacilityIndex))
                    {
                        string _facilityIndexValue = row[FacilityIndex].ToString().Trim();
                        _facilityIndexValue = System.Text.RegularExpressions.Regex.Replace(_facilityIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.FacilityName = _facilityIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("EQUIVALENCE", out int EquivalenceIndex))
                    {
                        string _equivalenceIndexValue = row[EquivalenceIndex].ToString().Trim();
                        _equivalenceIndexValue = System.Text.RegularExpressions.Regex.Replace(_equivalenceIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.EquivalenceName = _equivalenceIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("CATEGORY", out int CategoryIndex))
                    {
                        string _categoryIndexValue = row[CategoryIndex].ToString().Trim();
                        _categoryIndexValue = System.Text.RegularExpressions.Regex.Replace(_categoryIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.DashboardCategoryName = _categoryIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("OWNER DEPARTMENT", out int OwnerDepartmentIndex) ||
                        _columnHeaderMap.TryGetValue("OWNERDEPARTMENT", out OwnerDepartmentIndex))
                    {
                        string _ownerDepartmentIndexValue = row[OwnerDepartmentIndex].ToString().Trim();
                        _ownerDepartmentIndexValue = System.Text.RegularExpressions.Regex.Replace(_ownerDepartmentIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.OwnerDepartmentName = _ownerDepartmentIndexValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("RESPONSIBLE DEPARTMENT", out int ResponsibleDepartmentIndex) ||
                        _columnHeaderMap.TryGetValue("RESPONSIBLEDEPARTMENT", out ResponsibleDepartmentIndex))
                    {
                        string _responsibleDepartmentIndexValue = row[ResponsibleDepartmentIndex].ToString().Trim();
                        _responsibleDepartmentIndexValue = System.Text.RegularExpressions.Regex.Replace(_responsibleDepartmentIndexValue, @"\s+", " ");
                        _excelKPIDTO.KPIDTO.ResponsibleDepartmentName = _responsibleDepartmentIndexValue;
                        _haveInfo = true;
                    }
                    if (_haveInfo)
                    {
                        _excelKPIDTO.RowIteration = rowIndex + 1;
                        _excelKPIDTOList.Add(_excelKPIDTO);
                    }
                }
            }
            if (_excelKPIDTOList.Count > 0)
            {
                _validationResultDTO.Data = _excelKPIDTOList;
                return _validationResultDTO;
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Description = "Column records have no data.";
                _validationResultDTO.Message = "Error";
                return _validationResultDTO;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error";
            _validationResultDTO.Description = ex.Message;
            return _validationResultDTO;
        }
    }
    #endregion


    #endregion
}
