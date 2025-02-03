using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
public class Value_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateValue_Global(ValueDTO ValueDTO)
    {
        // Step 1. 
        var _validationResultDTO = Value_Validator.CreateValue_Validation(ValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step 2.
        ValueDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Value_Repository.CreateValue(ValueDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateValue_Global(ValueDTO ValueDTO)
    {
        var _ValidationResultDTO = Value_Validator.UpdateValue_Validation(ValueDTO);
        if (_ValidationResultDTO.Result)
        {
            ValueDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Value_Repository.UpdateValue(ValueDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteValue_Global(ValueDTO ValueDTO)
    {
        var _validationResultDTO = Value_Validator.DeleteValue_Validation(ValueDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Repository.DeleteValue(ValueDTO);
        }
        return _validationResultDTO;
    }
    public static List<ValueDTO> GetValueList_Global(ValueDTO ValueDTO, PagedResultDTO<ValueDTO> PagedResultDTO = null)
    {
        var _valueglobalList = new List<ValueDTO>();
        try
        {
            var _valueList = Value_Repository.GetValueList(ValueDTO, PagedResultDTO);
            // if Value is empty, return list
            if (_valueList.Count() == 0)
            {
                _valueglobalList = _valueList;
                return _valueglobalList;
            }
            if (!ValueDTO.GetAttributeDTO)
            {
                _valueglobalList = _valueList;
                return _valueglobalList;
            }
            _valueglobalList = GetValueRelatedData(ValueDTO, _valueList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valueglobalList;
    }
    public static List<ValueDTO> GetValueRelatedData(ValueDTO ValueDTO, List<ValueDTO> ValueList)
    {
        var _valueglobalList = new List<ValueDTO>();
        var _attributeDict = new Dictionary<int?, AttributeDTO>();

        try
        {
            if (ValueDTO.GetAttributeDTO)
            {
                ValueDTO.AttributeDTO = new AttributeDTO();
                ValueDTO.AttributeDTO.AttributeIDArray = ValueList.GroupBy(g => g.AttributeID)
                        .Select(s => s.Key)
                        .ToArray();

                _attributeDict = Attribute_Service.GetAttributeList_Global(ValueDTO.AttributeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _valueDTO in ValueList)
            {
                if (ValueDTO.GetAttributeDTO && _attributeDict.ContainsKey(_valueDTO.AttributeID))
                {
                    _valueDTO.AttributeDTO = _attributeDict[_valueDTO.AttributeID];
                }
                _valueglobalList.Add(_valueDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valueglobalList;
    }
    public static int GetValueTotalCount(PagedResultDTO<ValueDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Value_Repository.GetValueCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    #region Upload Excel functions

    public static ValidationResultDTO GenerateValueFromExcel(FileDTO FileDTO)
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
            _validationResultDTO = ExcelImport_Validator.ExcelHeaderColumns_Validation(FileDTO);

            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 3. Validate that the list of data comes
            FileDTO.DirectoryArray = _validationResultDTO.Data;
            _validationResultDTO = GetValueListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = Value_Validator.ExcelValueInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create Value
            _validationResultDTO = Value_Repository.CreateMultipleValue(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetValueListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<ValueDTO>(),
                BadRowLinesList = new List<ValueDTO>()
            };
            // Converts the Base64 string contained in FileDTO.Data to a byte array,
            // To later process it and read the content of the Excel file.
            var _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            using (var _fileStream = new MemoryStream(_fileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store the excel column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                // Create the list with the name of the columns that were previously validated
                var _columnHeaderNameList = FileDTO.DirectoryArray.Select(ColumnName => ColumnName.ToUpper()).ToList();
                // This is to identify the columns within the Excel, to later bring the information contained in the row
                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    // We take the column name, put it in capital letters to compare it with our list (_columnHeaderNameList) 
                    // and we remove all the spaces from the name before comparing it with the list. (e.g. " Unit  Of Measure " -> "UnitOfMeasure")
                    string headerName = _firstTable.Rows[0][colIndex].ToString().ToUpper();
                    // The regular expression @"\s+", Removes all whitespace from the column name
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", "");
                    if (_columnHeaderNameList.Contains(headerName))
                    {
                        // If the column name exists, it stores it in the dirctory (_columnHeaderMap) and assigns its identifier
                        _columnHeaderMap[headerName] = colIndex;
                    }
                }

                // Process rows starting from the second row (index 1), to take the information to store in each iteration
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    // We create a DTO where we will store the content of the excel to later validate it
                    var _ValueDTO = new ValueDTO();
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _ValueDTO.ID = rowIndex + 1;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _ValueDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["NAME"]].ToString());
                    _ValueDTO.Description = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DESCRIPTION"]].ToString());
                    _ValueDTO.Code = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["CODE"]].ToString());
                    _ValueDTO.AttributeName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["ATTRIBUTE"]].ToString());
                    _ValueDTO.AddedByID = FileDTO.ID;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = Value_Validator.ExcelValueRows_Validation(_ValueDTO);

                    // We verify if our DTO complied with the validations
                    if (_validationResultDTO.Data.GoodRowLinesList.Count > 0)
                        // If the DTO does not have null properties, it is stored in the GoodRowLines.
                        _excelRowDTO.GoodRowLinesList.AddRange(_validationResultDTO.Data.GoodRowLinesList);
                    else
                        // If any of the DTO properties is null, it is stored on a BadRowLines.
                        _excelRowDTO.BadRowLinesList.AddRange(_validationResultDTO.Data.BadRowLinesList);
                }
            }
            // Verify if there were good or bad lines to return _excelRowDTO
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
