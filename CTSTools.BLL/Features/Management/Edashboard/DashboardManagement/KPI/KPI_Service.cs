using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

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
        var _excelRowDTO = new ExcelRowDTO();
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been create successfully.."
        };
        try
        {
            // step 1. Validate that it is an excel file
            _validationResultDTO = ExcelImport_Validator.ExcelFile_Validation(FileDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 2. Bring the information from excel
            FileDTO.FileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO = ExcelImport_Validator.ExcelHeaderColumns_Validation(FileDTO);

            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 3. Validate that the list of data comes
            FileDTO.DirectoryArray = _validationResultDTO.Data;
            _validationResultDTO = GetKPIListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = KPI_Validator.ExcelKPIInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create KPIs
            //_validationResultDTO = KPI_Repository.CreateMultipleKPI(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ValidationResultDTO GetKPIListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<KPIDTO>(),
                BadRowLinesList = new List<KPIDTO>()
            };
            using (var _fileStream = new MemoryStream(FileDTO.FileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                var _columnHeaderNameList = FileDTO.DirectoryArray.Select(ColumnName => ColumnName.ToUpper()).ToList();

                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    string headerName = _firstTable.Rows[0][colIndex].ToString().ToUpper();
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", "");
                    if (_columnHeaderNameList.Contains(headerName))
                    {
                        _columnHeaderMap[headerName] = colIndex;
                    }
                }

                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _kPIDTO = new KPIDTO();
                    bool _haveInfo = false;

                    if (_columnHeaderMap.ContainsKey("NAME"))
                    {
                        _kPIDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["NAME"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("DESCRIPTION"))
                    {
                        _kPIDTO.Description = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DESCRIPTION"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("GOAL"))
                    {
                        string _rowValue = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["GOAL"]]?.ToString());
                        if (float.TryParse(_rowValue, out float Goal))
                            _kPIDTO.Goal = Goal;
                        else
                            _kPIDTO.Goal = -1;
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("UNITOFMEASURE"))
                    {
                        _kPIDTO.UnitOfMeasureName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["UNITOFMEASURE"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("VALUETYPE"))
                    {
                        _kPIDTO.ValueTypeName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["VALUETYPE"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("OWNER"))
                    {
                        _kPIDTO.OwnerName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["OWNER"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("RESPONSIBLE"))
                    {
                        _kPIDTO.ResponsibleName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["RESPONSIBLE"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("FACILITY"))
                    {
                        _kPIDTO.FacilityName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["FACILITY"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("EQUIVALENCE"))
                    {
                        _kPIDTO.EquivalenceName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["EQUIVALENCE"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("CATEGORY"))
                    {
                        _kPIDTO.DashboardCategoryName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["CATEGORY"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("OWNERDEPARTMENT"))
                    {
                        _kPIDTO.OwnerDepartmentName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["OWNERDEPARTMENT"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_columnHeaderMap.ContainsKey("RESPONSIBLEDEPARTMENT"))
                    {
                        _kPIDTO.ResponsibleDepartmentName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["RESPONSIBLEDEPARTMENT"]]?.ToString());
                        _haveInfo = true;
                    }

                    if (_haveInfo)
                    {
                        _kPIDTO.ID = rowIndex + 1;
                        _kPIDTO.AddedByID = FileDTO.ID;
                        _validationResultDTO = KPI_Validator.ExcelKPIRows_Validation(_kPIDTO);
                    }

                    if (_validationResultDTO.Data.GoodRowLinesList.Count > 0)
                        _excelRowDTO.GoodRowLinesList.AddRange(_validationResultDTO.Data.GoodRowLinesList);
                    else
                        _excelRowDTO.BadRowLinesList.AddRange(_validationResultDTO.Data.BadRowLinesList);
                }
            }
            if (_excelRowDTO.GoodRowLinesList.Count > 0 || _excelRowDTO.BadRowLinesList.Count > 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
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
