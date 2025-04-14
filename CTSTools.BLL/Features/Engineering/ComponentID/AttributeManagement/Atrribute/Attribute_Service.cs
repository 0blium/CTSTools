using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;

public class Attribute_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateAttribute_Global(AttributeDTO AttributeDTO)
    {
        //Step 1. Validate fields
        var _validationResultDTO = Attribute_Validator.CreateAttribute_Validation(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Save records
        AttributeDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Attribute_Repository.CreateAttribute(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 3. Create the variant record
        var _valueDTO = new ValueDTO
        {
            Name = AttributeDTO.Name,
            AttributeID = (int)Attribute_Enum.Variant,
            Code = Convert.ToString(_validationResultDTO.Data),
            IsActive = AttributeDTO.IsActive,
            AddedByID = AttributeDTO.AddedByID,
            AddedDate = AttributeDTO.AddedDate
        };
        _validationResultDTO = Value_Service.CreateValue_Global(_valueDTO);
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateAttribute_Global(AttributeDTO AttributeDTO)
    {
        //Step 1. Validate the attribute
        var _validationResultDTO = Attribute_Validator.UpdateAttribute_Validation(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Update the attribute
        AttributeDTO.LastUpdate = DateTime.Now;
        _validationResultDTO = Attribute_Repository.UpdateAttribute(AttributeDTO);
        //Step 3. Update Variant 
        var _valueDTO = new ValueDTO { AttributeID = (int)Attribute_Enum.Variant, Code = AttributeDTO.ID.ToString() };
        var _variantValueDTO = Value_Service.GetValueList_Global(_valueDTO).FirstOrDefault();
        if (_variantValueDTO != null)
        {
            _variantValueDTO.Name = AttributeDTO.Name;
            _validationResultDTO = Value_Service.UpdateValue_Global(_variantValueDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteAttribute_Global(AttributeDTO AttributeDTO)
    {
        //Step 1. Validate fields
        var _validationResultDTO = Attribute_Validator.DeleteAttribute_Validation(AttributeDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        //Step 2. Delete Attribute
        _validationResultDTO = Attribute_Repository.DeleteAttribute(AttributeDTO);
        return _validationResultDTO;
    }
    public static List<AttributeDTO> GetAttributeList_Global(AttributeDTO AttributeDTO, PagedResultDTO<AttributeDTO> PagedResultDTO = null)
    {
        var _attributeglobalList = new List<AttributeDTO>();
        try
        {
            var _attributeList = Attribute_Repository.GetAttributeList(AttributeDTO, PagedResultDTO);
            if (_attributeList.Count() == 0 || (!AttributeDTO.GetValueList))
                return _attributeList;
            else
                _attributeglobalList = GetAttributeRelatedData(AttributeDTO, _attributeList);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _attributeglobalList;
    }
    public static List<AttributeDTO> GetAttributeRelatedData(AttributeDTO AttributeDTO, List<AttributeDTO> AttributeList)
    {
        var _attributeglobalList = new List<AttributeDTO>();
        var _valueList = new List<ValueDTO>();
        try
        {
            if ((bool)AttributeDTO.GetValueList)
            {
                AttributeDTO.ValueDTO.AttributeIDArray = AttributeList.GroupBy(g => g.ID)
                        .Select(s => s.Key)
                        .ToArray();
                _valueList = Value_Service.GetValueList_Global(AttributeDTO.ValueDTO);
            }
            foreach (var _attributeDTO in AttributeList)
            {
                if ((bool)AttributeDTO.GetValueList && _valueList.Count() > 0)
                    _attributeDTO.ValueList = _valueList.Where(w => w.AttributeID == _attributeDTO.ID).ToList();
                _attributeglobalList.Add(_attributeDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _attributeglobalList;
    }
    public static int GetAttributeTotalCount(PagedResultDTO<AttributeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Attribute_Repository.GetAttributeCount(PagedResultDTO.Filter, PagedResultDTO);
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

    public static ValidationResultDTO GenerateAttributeFromExcel(FileDTO FileDTO)
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
            _validationResultDTO = GetAttributeListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = Attribute_Validator.ExcelAttributeInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create Attribute
            _validationResultDTO = Attribute_Repository.CreateMultipleAttribute(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetAttributeListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<AttributeDTO>(),
                BadRowLinesList = new List<AttributeDTO>()
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
                    var _AttributeDTO = new AttributeDTO();
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _AttributeDTO.ID = rowIndex + 1;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _AttributeDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["NAME"]].ToString());
                    _AttributeDTO.Description = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DESCRIPTION"]].ToString());
                    _AttributeDTO.AddedByID = FileDTO.ID;

                    // Gets the value of the "HASMULTIPLEOPTIONS" column from the Excel file and cleans it of unwanted characters.
                    string _rowHasMultipleOptionsValue = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["HASMULTIPLEOPTIONS"]]?.ToString());
                    // bool.TryParse try to convert the value (_rowHasMultipleOptionsValue) to a text of type boolean
                    // Note: TryParse will not throw an exception if the conversion fails.
                    // The 'out' keyword indicates that HasMultipleOptions is an output parameter.
                    if (bool.TryParse(_rowHasMultipleOptionsValue, out bool HasMultipleOptions))
                        // If the conversion is successful, the converted value will be assigned to the Goal variable.
                        _AttributeDTO.HasMultipleOptions = HasMultipleOptions;
                    else
                        _AttributeDTO.HasMultipleOptions = false;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = Attribute_Validator.ExcelAttributeRows_Validation(_AttributeDTO);

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
