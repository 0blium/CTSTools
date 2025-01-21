using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
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

    #region Update Excel functions
    public static ValidationResultDTO KPIFileValidation_Global(FileDTO FileDTO)
    {
        ExcelKPIDTO _excelKPIDTO = new ExcelKPIDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        try
        {
            byte[] _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "Comparission was made successfully";

            var _excelKPIRowsValidation = new ExcelKPIDTO();
            string _extension = "";
            if (FileDTO.FileName.Contains(".xlsx"))
            {
                _extension = ".xlsx";
            }
            else if (FileDTO.FileName.Contains(".xls"))
            {
                _extension = ".xls";
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Description = "Input only excel files.";
                _validationResultDTO.Message = "Invalid file";
            }
            //Step 1. Validate if the files contains following headers: Number, Name, Address, City, State, Country, Postal Code.
            // WARNING: The validations allows to contain empty rows above the column headers. If something are above of coliumns, the validation
            // will take it as an error.
            _excelKPIDTO = ValidateKPIFileColumns(_fileBytes, FileDTO.FileName, _extension);
            _validationResultDTO = _excelKPIDTO.ValidationResultDTO;

            if (_validationResultDTO.Result == true)
            {
                //Step 2. Read the file and get the rows in vendordto format
                if (_extension == ".xls" || _extension == ".xlsx")
                {
                    _validationResultDTO = GetKPIInfoListFromExcelFile(_fileBytes);
                }
                if (_validationResultDTO.Data != null)
                {
                    _excelKPIRowsValidation = KPIFileRowsValidation(_validationResultDTO.Data);

                    if (_excelKPIRowsValidation.KPIGoodLinesList.Count > 0)
                    {
                        foreach (var _kPIDTO in _excelKPIRowsValidation.KPIGoodLinesList)
                        {
                            _kPIDTO.AddedByID = FileDTO.ID;
                            var _validationResulDTO = CreateKPI_Global(_kPIDTO);
                        }
                    }
                }
                else 
                {
                    return _validationResultDTO;
                }

                _validationResultDTO.Result = _excelKPIRowsValidation.ValidationResultDTO.Result;
                _validationResultDTO.Message = _excelKPIRowsValidation.ValidationResultDTO.Message;
                _validationResultDTO.Description = _excelKPIRowsValidation.ValidationResultDTO.Description;
            }
            _validationResultDTO.Data = _excelKPIRowsValidation;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ExcelKPIDTO ValidateKPIFileColumns(byte[] _fileBytes, string Filename, string Extension)
    {
        string[] _fileheaders = new string[0];
        ExcelKPIDTO _excelKPIFileValidationDTO = new ExcelKPIDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();

        if (Extension == ".xlsx" || Extension == ".xls")
        {
            _fileheaders = ExcelDataImport_Service.GetHeadersFromExcel(_fileBytes);
        }
        else
        {
            _validationResultDTO.Result = false;
            _validationResultDTO.Description = "Input only excel files.";
            _validationResultDTO.Message = "Invalid file";
        }
        string _missingHeader = string.Empty;
        _missingHeader += (_fileheaders.Contains("name") == true) ? string.Empty : "name,<br>";
        _missingHeader += (_fileheaders.Contains("description") == true) ? string.Empty : "description,<br>";
        _missingHeader += (_fileheaders.Contains("unit of measure") == true || _fileheaders.Contains("unitofmeasure")) ? string.Empty : "unitofmeasure,<br>";
        _missingHeader += (_fileheaders.Contains("value type") == true || _fileheaders.Contains("valuetype")) ? string.Empty : "valuetype,<br>";
        _missingHeader += (_fileheaders.Contains("goal") == true) ? string.Empty : "goal,<br>";
        _missingHeader += (_fileheaders.Contains("owner") == true) ? string.Empty : "owner,<br>";
        _missingHeader += (_fileheaders.Contains("responsible") == true) ? string.Empty : "responsible,<br>";
        _missingHeader += (_fileheaders.Contains("goal range") == true || _fileheaders.Contains("goalrange") == true) ? string.Empty : "goalrange,<br>";
        _missingHeader += (_fileheaders.Contains("facility") == true) ? string.Empty : "facility,<br>";
        _missingHeader += (_fileheaders.Contains("equivalence") == true) ? string.Empty : "equivalence,<br>";
        _missingHeader += (_fileheaders.Contains("category") == true) ? string.Empty : "category,<br>";
        _missingHeader += (_fileheaders.Contains("owner department") == true || _fileheaders.Contains("ownerdepartment") == true) ? string.Empty : "ownerdepartment,<br>";
        _missingHeader += (_fileheaders.Contains("responsible department") == true || _fileheaders.Contains("responsibledepartment") == true) ? string.Empty : "responsibledepartment,<br>";

        if (_missingHeader != string.Empty)
        {
            var lastComma = _missingHeader.LastIndexOf(',');
            _missingHeader = _missingHeader.Remove(lastComma, 1).Insert(lastComma, ".");
            _validationResultDTO.Result = false;
            _validationResultDTO.Description = string.Format("The following columns are missing:<br> {0}", _missingHeader);
            _validationResultDTO.Message = "Error";
        }
        else
        {
            _validationResultDTO.Result = true;
            _validationResultDTO.Description = "The file have the correct format ";
            _validationResultDTO.Message = "Success";
        }
        _excelKPIFileValidationDTO.ValidationResultDTO = _validationResultDTO;

        return _excelKPIFileValidationDTO;
    }

    private static ValidationResultDTO GetKPIInfoListFromExcelFile(byte[] FileBytes)
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
                        headerName.Equals("GOALRANGE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("GOAL RANGE", StringComparison.OrdinalIgnoreCase) ||
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
                            throw new InvalidCastException($"The value to Is Goal: '{_goalValue}', is not a valid number.");
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
                        _excelKPIDTOList.Add(_excelKPIDTO);
                    }
                }
            }
            _validationResultDTO.Data = _excelKPIDTOList;
            return _validationResultDTO;
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
    private static ExcelKPIDTO KPIFileRowsValidation(List<ExcelKPIDTO> ExcelKPIFileDataList)
    {
        ExcelKPIDTO _excelKPIFileValidationDTO = new ExcelKPIDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Result = true;
        _validationResultDTO.Message = "Success";
        _validationResultDTO.Description = "";
        try
        {

            foreach (var _excelKPIFileData in ExcelKPIFileDataList)
            {
                KPIDTO _kPIDTO = new KPIDTO();
                bool isSucces = true;
                if (_excelKPIFileData.KPIDTO.Name == string.Empty || _excelKPIFileData.KPIDTO.Name == null)
                {
                    _excelKPIFileData.KPIDTO.Name = "Error, The name is null or empty";
                    isSucces = false;
                }
                if (_excelKPIFileData.KPIDTO.UnitOfMeasureName == string.Empty || _excelKPIFileData.KPIDTO.UnitOfMeasureName == null)
                {
                    _excelKPIFileData.KPIDTO.UnitOfMeasureName = "Error, The Unit Of Measure is null or empty";
                    isSucces = false;
                }
                else 
                {
                    var _existUnitOfMeasureDTO = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(new UnitOfMeasureDTO { Name = _excelKPIFileData.KPIDTO.UnitOfMeasureName }).FirstOrDefault();
                    if (_existUnitOfMeasureDTO != null)
                    {
                        _kPIDTO.UnitOfMeasureID = _existUnitOfMeasureDTO.ID;
                    }
                    else 
                    {
                        _excelKPIFileData.KPIDTO.UnitOfMeasureName = "Error, The Unit Of Measure not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.ValueTypeName == string.Empty || _excelKPIFileData.KPIDTO.ValueTypeName == null)
                {
                    _excelKPIFileData.KPIDTO.ValueTypeName = "Error, The Value Type is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existValueTypeDTO = ValueType_Service.GetValueTypeList_Global(new ValueTypeDTO { Name = _excelKPIFileData.KPIDTO.ValueTypeName }).FirstOrDefault();
                    if (_existValueTypeDTO != null)
                    {
                        _kPIDTO.ValueTypeID = _existValueTypeDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.ValueTypeName = "Error, The Value Type not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.Goal < 0.0f)
                {
                    _kPIDTO.AddedByName = "Error, The Goal is null or less than zero";
                    isSucces = false;
                }
                if (_excelKPIFileData.KPIDTO.OwnerName == string.Empty || _excelKPIFileData.KPIDTO.OwnerName == null)
                {
                    _excelKPIFileData.KPIDTO.OwnerName = "Error, The Owner is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existUserDTO = User_Service.GetUserList_Global(new UserDTO { Name = _excelKPIFileData.KPIDTO.OwnerName }).FirstOrDefault();
                    if (_existUserDTO != null)
                    {
                        _kPIDTO.OwnerID = _existUserDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.OwnerName = "Error, The Owner not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.ResponsibleName == string.Empty || _excelKPIFileData.KPIDTO.ResponsibleName == null)
                {
                    _excelKPIFileData.KPIDTO.ResponsibleName = "Error, The Responsible is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existUserDTO = User_Service.GetUserList_Global(new UserDTO { Name = _excelKPIFileData.KPIDTO.ResponsibleName }).FirstOrDefault();
                    if (_existUserDTO != null)
                    {
                        _kPIDTO.ResponsibleID = _existUserDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.ResponsibleName = "Error, The Responsible not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.FacilityName == string.Empty || _excelKPIFileData.KPIDTO.FacilityName == null)
                {
                    _excelKPIFileData.KPIDTO.FacilityName = "Error, The Facility is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existFacilityDTO = Facility_Service.GetFacilityList_Global(new FacilityDTO { Name = _excelKPIFileData.KPIDTO.FacilityName }).FirstOrDefault();
                    if (_existFacilityDTO != null)
                    {
                        _kPIDTO.FacilityID = _existFacilityDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.FacilityName = "Error, The Facility not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.EquivalenceName == string.Empty || _excelKPIFileData.KPIDTO.EquivalenceName == null)
                {
                    _excelKPIFileData.KPIDTO.EquivalenceName = "Error, The Equivalence is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existEquivalenceDTO = Equivalence_Service.GetEquivalenceList_Global(new EquivalenceDTO { Name = _excelKPIFileData.KPIDTO.EquivalenceName }).FirstOrDefault();
                    if (_existEquivalenceDTO != null)
                    {
                        _kPIDTO.EquivalenceID = _existEquivalenceDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.EquivalenceName = "Error, The Equivalence not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.DashboardCategoryName == string.Empty || _excelKPIFileData.KPIDTO.DashboardCategoryName == null)
                {
                    _excelKPIFileData.KPIDTO.DashboardCategoryName = "Error, The Category is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existDashboardCategoryDTO = DashboardCategory_Service.GetDashboardCategoryList_Global(new DashboardCategoryDTO { Name = _excelKPIFileData.KPIDTO.DashboardCategoryName }).FirstOrDefault();
                    if (_existDashboardCategoryDTO != null)
                    {
                        _kPIDTO.DashboardCategoryID = _existDashboardCategoryDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.DashboardCategoryName = "Error, The Dashboard Category not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.OwnerDepartmentName == string.Empty || _excelKPIFileData.KPIDTO.OwnerDepartmentName == null)
                {
                    _excelKPIFileData.KPIDTO.OwnerDepartmentName = "Error, The Owner Department is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existDepartmentDTO = Department_Service.GetDepartmentList_Global(new DepartmentDTO { Name = _excelKPIFileData.KPIDTO.OwnerDepartmentName }).FirstOrDefault();
                    if (_existDepartmentDTO != null)
                    {
                        _kPIDTO.OwnerDepartmentID = _existDepartmentDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.OwnerDepartmentName = "Error, The Owner Department not exist";
                        isSucces = false;
                    }
                }
                if (_excelKPIFileData.KPIDTO.ResponsibleDepartmentName == string.Empty || _excelKPIFileData.KPIDTO.ResponsibleDepartmentName == null)
                {
                    _excelKPIFileData.KPIDTO.ResponsibleDepartmentName = "Error, The Responsible Department is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existDepartmentDTO = Department_Service.GetDepartmentList_Global(new DepartmentDTO { Name = _excelKPIFileData.KPIDTO.ResponsibleDepartmentName }).FirstOrDefault();
                    if (_existDepartmentDTO != null)
                    {
                        _kPIDTO.ResponsibleDepartmentID = _existDepartmentDTO.ID;
                    }
                    else
                    {
                        _excelKPIFileData.KPIDTO.ResponsibleDepartmentName = "Error, The Responsible Department not exist";
                        isSucces = false;
                    }
                }

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
        _excelKPIFileValidationDTO.ValidationResultDTO = _validationResultDTO;
        return _excelKPIFileValidationDTO;
    }
    #endregion

    #endregion
}
