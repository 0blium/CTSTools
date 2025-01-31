using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
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

    #region Update Excel functions
    public static ValidationResultDTO ValueFileValidation_Global(FileDTO FileDTO)
    {
        ExcelValueDTO _excelValueDTO = new ExcelValueDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        try
        {
            byte[] _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "Comparission was made successfully";

            var _excelValueRowsValidation = new ExcelValueDTO();
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
            _excelValueDTO = ValidateValueFileColumns(_fileBytes, FileDTO.FileName, _extension);
            _validationResultDTO = _excelValueDTO.ValidationResultDTO;

            if (_validationResultDTO.Result == true)
            {
                //Step 2. Read the file and get the rows in vendordto format
                var _excelValueList = new List<ExcelValueDTO>();
                if (_extension == ".xls" || _extension == ".xlsx")
                {
                    _validationResultDTO = GetValueInfoListFromExcelFile(_fileBytes);
                }
                if (_validationResultDTO.Data != null)
                {
                    _excelValueRowsValidation = ValueFileRowsValidation(_validationResultDTO.Data);

                    if (_excelValueRowsValidation.ValueGoodLinesList.Count > 0)
                    {
                        foreach (var _ValueDTO in _excelValueRowsValidation.ValueGoodLinesList)
                        {
                            _ValueDTO.AddedByID = FileDTO.ID;
                            _validationResultDTO = CreateValue_Global(_ValueDTO);
                        }
                    }
                }
                else
                {
                    return _validationResultDTO;
                }

                _validationResultDTO.Result = _excelValueRowsValidation.ValidationResultDTO.Result;
                _validationResultDTO.Message = _excelValueRowsValidation.ValidationResultDTO.Message;
                _validationResultDTO.Description = _excelValueRowsValidation.ValidationResultDTO.Description;
            }
            _validationResultDTO.Data = _excelValueRowsValidation;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ExcelValueDTO ValidateValueFileColumns(byte[] _fileBytes, string Filename, string Extension)
    {
        string[] _fileheaders = new string[0];
        ExcelValueDTO _excelValueFileValidationDTO = new ExcelValueDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();

        if (Extension == ".xlsx" || Extension == ".xls")
        {
            _fileheaders = ExcelImport_Service.GetHeadersFromExcel(_fileBytes);
        }
        else
        {
            _validationResultDTO.Result = false;
            _validationResultDTO.Description = "Input only excel files.";
            _validationResultDTO.Message = "Invalid file";
        }
        string _missingHeader = string.Empty;
        _missingHeader += (_fileheaders.Contains("name") == true) ? string.Empty : "name, <br>";
        _missingHeader += (_fileheaders.Contains("attribute") == true) ? string.Empty : "attribute, <br>";
        _missingHeader += (_fileheaders.Contains("code") == true) ? string.Empty : "code, <br>";
        _missingHeader += (_fileheaders.Contains("description") == true) ? string.Empty : "description, ";

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
        _excelValueFileValidationDTO.ValidationResultDTO = _validationResultDTO;

        return _excelValueFileValidationDTO;
    }

    private static ValidationResultDTO GetValueInfoListFromExcelFile(byte[] FileBytes)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            List<ExcelValueDTO> _excelValueDTOList = new List<ExcelValueDTO>();
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
                    if (headerName.Equals("ATTRIBUTE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("NAME", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("CODE", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("DESCRIPTION", StringComparison.OrdinalIgnoreCase))
                    {
                        _columnHeaderMap[headerName.ToUpper()] = colIndex;
                    }
                }
                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _excelValueDTO = new ExcelValueDTO();
                    bool _haveInfo = false;
                    // Assign values ​​directly using the dictionary
                    if (_columnHeaderMap.TryGetValue("NAME", out int NameIndex))
                    {
                        string _nameValue = row[NameIndex].ToString().Trim();
                        _nameValue = System.Text.RegularExpressions.Regex.Replace(_nameValue, @"\s+", " ");
                        _excelValueDTO.ValueDTO.Name = _nameValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("ATTRIBUTE", out int AttributeIndex))
                    {
                        string _attributeValue = row[AttributeIndex].ToString().Trim();
                        _attributeValue = System.Text.RegularExpressions.Regex.Replace(_attributeValue, @"\s+", " ");
                        _excelValueDTO.ValueDTO.AttributeName = _attributeValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("CODE", out int CodeIndex))
                    {
                        string _codeValue = row[CodeIndex].ToString().Trim();
                        _codeValue = System.Text.RegularExpressions.Regex.Replace(_codeValue, @"\s+", " ");
                        _excelValueDTO.ValueDTO.Code = _codeValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("DESCRIPTION", out int DescriptionIndex))
                    {
                        string _descriptionValue = row[DescriptionIndex].ToString().Trim();
                        _descriptionValue = System.Text.RegularExpressions.Regex.Replace(_descriptionValue, @"\s+", " ");
                        _excelValueDTO.ValueDTO.Description = _descriptionValue;
                        _haveInfo = true;
                    }
                    if (_haveInfo)
                    {
                        _excelValueDTO.RowIteration = rowIndex + 1;
                        _excelValueDTOList.Add(_excelValueDTO);
                    }
                }
            }
            if (_excelValueDTOList.Count > 0)
            {
                _validationResultDTO.Data = _excelValueDTOList;
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
            _validationResultDTO.Description = "Verify that the column values are correct.";
            return _validationResultDTO;
        }
    }

    private static ExcelValueDTO ValueFileRowsValidation(List<ExcelValueDTO> ExcelValueFileDataList)
    {
        ExcelValueDTO _excelValueFileValidationDTO = new ExcelValueDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Result = true;
        _validationResultDTO.Message = "Success";
        _validationResultDTO.Description = "";
        try
        {

            foreach (var _excelValueFileData in ExcelValueFileDataList)
            {
                ValueDTO _ValueDTO = new ValueDTO();
                bool isSucces = true;
                if (_excelValueFileData.ValueDTO.Name == string.Empty || _excelValueFileData.ValueDTO.Name == null)
                {
                    _excelValueFileData.ValueDTO.Name = "Error, The Name is null or empty";
                    isSucces = false;
                }
                if (_excelValueFileData.ValueDTO.AttributeName == string.Empty || _excelValueFileData.ValueDTO.AttributeName == null)
                {
                    _excelValueFileData.ValueDTO.AttributeName = "Error, The Attribute is null or empty";
                    isSucces = false;
                }
                else
                {
                    var _existAttributeDTO = Attribute_Service.GetAttributeList_Global(new AttributeDTO { Name = _excelValueFileData.ValueDTO.AttributeName }).FirstOrDefault();
                    if (_existAttributeDTO != null)
                    {
                        _ValueDTO.AttributeID = _existAttributeDTO.ID;
                    }
                    else
                    {
                        _excelValueFileData.ValueDTO.AttributeName = "Error, The Attribute not exist";
                        isSucces = false;
                    }
                }
                if (_excelValueFileData.ValueDTO.Code == string.Empty || _excelValueFileData.ValueDTO.Code == null)
                {
                    _excelValueFileData.ValueDTO.Code = "Error, The Code is null or empty";
                    isSucces = false;
                }

                _ValueDTO.Name = _excelValueFileData.ValueDTO.Name;
                _ValueDTO.Code = _excelValueFileData.ValueDTO.Code;
                _ValueDTO.AttributeName = _excelValueFileData.ValueDTO.AttributeName;
                _ValueDTO.Description = _excelValueFileData.ValueDTO.Description;
                _ValueDTO.IsActive = true;

                if (isSucces == true)
                {
                    _excelValueFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelValueFileValidationDTO.ValueGoodLinesList.Add(_ValueDTO);
                }
                else
                {
                    _ValueDTO.ID = _excelValueFileData.RowIteration;
                    _excelValueFileValidationDTO.ValidationResultDTO.Message = _excelValueFileValidationDTO.ValidationResultDTO.Message;
                    _excelValueFileValidationDTO.ValueBadLinesList.Add(_ValueDTO);
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
        _excelValueFileValidationDTO.ValidationResultDTO = _validationResultDTO;
        return _excelValueFileValidationDTO;
    }
    #endregion

    #endregion
}
