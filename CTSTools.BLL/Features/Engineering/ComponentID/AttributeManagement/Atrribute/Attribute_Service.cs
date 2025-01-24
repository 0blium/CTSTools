using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.BLL.Common.Files;
using System.Data;
using System.IO;
using ExcelDataReader;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
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

    #region Update Excel functions
    public static ValidationResultDTO AttributeFileValidation_Global(FileDTO FileDTO)
    {
        ExcelAttributeDTO _excelAttributeDTO = new ExcelAttributeDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        try
        {
            byte[] _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "Comparission was made successfully";

            var _excelAttributeRowsValidation = new ExcelAttributeDTO();
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
            _excelAttributeDTO = ValidateAttributeFileColumns(_fileBytes, FileDTO.FileName, _extension);
            _validationResultDTO = _excelAttributeDTO.ValidationResultDTO;

            if (_validationResultDTO.Result == true)
            {
                //Step 2. Read the file and get the rows in vendordto format
                if (_extension == ".xls" || _extension == ".xlsx")
                {
                    _validationResultDTO = GetAttributeInfoListFromExcelFile(_fileBytes);
                }
                if (_validationResultDTO.Data != null)
                {
                    _excelAttributeRowsValidation = AttributeFileRowsValidation(_validationResultDTO.Data);

                    if (_excelAttributeRowsValidation.AttributeGoodLinesList.Count > 0)
                    {
                        foreach (var _attributeDTO in _excelAttributeRowsValidation.AttributeGoodLinesList)
                        {
                            _attributeDTO.AddedByID = FileDTO.ID;
                            _validationResultDTO = CreateAttribute_Global(_attributeDTO);
                        }
                    }
                }
                else
                {
                    return _validationResultDTO;
                }

                _validationResultDTO.Result = _excelAttributeRowsValidation.ValidationResultDTO.Result;
                _validationResultDTO.Message = _excelAttributeRowsValidation.ValidationResultDTO.Message;
                _validationResultDTO.Description = _excelAttributeRowsValidation.ValidationResultDTO.Description;
            }
            _validationResultDTO.Data = _excelAttributeRowsValidation;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ExcelAttributeDTO ValidateAttributeFileColumns(byte[] _fileBytes, string Filename, string Extension)
    {
        string[] _fileheaders = new string[0];
        ExcelAttributeDTO _excelAttributeFileValidationDTO = new ExcelAttributeDTO();
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
        _missingHeader += (_fileheaders.Contains("has multiple options") == true || _fileheaders.Contains("hasmultipleoptions") == true) ? string.Empty : "has multiple options,<br>";

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
        _excelAttributeFileValidationDTO.ValidationResultDTO = _validationResultDTO;

        return _excelAttributeFileValidationDTO;
    }

    private static ValidationResultDTO GetAttributeInfoListFromExcelFile(byte[] FileBytes)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            List<ExcelAttributeDTO> _excelAttributeDTOList = new List<ExcelAttributeDTO>();
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
                        headerName.Equals("HASMULTIPLEOPTIONS", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("HAS MULTIPLE OPTIONS", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("DESCRIPTION", StringComparison.OrdinalIgnoreCase))
                    {
                        _columnHeaderMap[headerName.ToUpper()] = colIndex;
                    }
                }
                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _excelAttributeDTO = new ExcelAttributeDTO();
                    bool _haveInfo = false;
                    // Assign values ​​directly using the dictionary
                    if (_columnHeaderMap.TryGetValue("NAME", out int NameIndex))
                    {
                        string _nameValue = row[NameIndex].ToString().Trim();
                        _nameValue = System.Text.RegularExpressions.Regex.Replace(_nameValue, @"\s+", " ");
                        _excelAttributeDTO.AttributeDTO.Name = _nameValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("DESCRIPTION", out int DescriptionIndex))
                    {
                        string _descriptionValue = row[DescriptionIndex].ToString().Trim();
                        _descriptionValue = System.Text.RegularExpressions.Regex.Replace(_descriptionValue, @"\s+", " ");
                        _excelAttributeDTO.AttributeDTO.Description = _descriptionValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("HAS MULTIPLE OPTIONS", out int HasMultipleOptionsIndex) || 
                        _columnHeaderMap.TryGetValue("HASMULTIPLEOPTIONS", out HasMultipleOptionsIndex))
                    {
                        string _hasMultipleOptionsValue = row[HasMultipleOptionsIndex].ToString().Trim();
                        _hasMultipleOptionsValue = System.Text.RegularExpressions.Regex.Replace(_hasMultipleOptionsValue, @"\s+", " ");
                        if (bool.TryParse(_hasMultipleOptionsValue, out bool HasMultipleOptions))
                        {
                            _excelAttributeDTO.AttributeDTO.HasMultipleOptions = HasMultipleOptions;
                            _haveInfo = true;
                        }
                        else
                        {
                            throw new InvalidCastException($"The value for Has Multiple Options in Row {rowIndex + 1} is not a valid boolean.");
                        }
                    }
                    if (_haveInfo)
                    {
                        _excelAttributeDTO.RowIteration = rowIndex + 1;
                        _excelAttributeDTOList.Add(_excelAttributeDTO);
                    }
                }
            }
            if (_excelAttributeDTOList.Count > 0)
            {
                _validationResultDTO.Data = _excelAttributeDTOList;
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
    private static ExcelAttributeDTO AttributeFileRowsValidation(List<ExcelAttributeDTO> ExcelAttributeFileDataList)
    {
        ExcelAttributeDTO _excelAttributeFileValidationDTO = new ExcelAttributeDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Result = true;
        _validationResultDTO.Message = "Success";
        _validationResultDTO.Description = "";
        try
        {

            foreach (var _excelAttributeFileData in ExcelAttributeFileDataList)
            {
                AttributeDTO _attributeDTO = new AttributeDTO();
                bool isSucces = true;
                if (_excelAttributeFileData.AttributeDTO.Name == string.Empty || _excelAttributeFileData.AttributeDTO.Name == null)
                {
                    _excelAttributeFileData.AttributeDTO.Name = "Error, The name is null or empty";
                    isSucces = false;
                }
                if (_excelAttributeFileData.AttributeDTO.HasMultipleOptions == null)
                {
                    _excelAttributeFileData.AttributeDTO.HasMultipleOptions = true;
                }

                _attributeDTO.Name = _excelAttributeFileData.AttributeDTO.Name;
                _attributeDTO.Description = _excelAttributeFileData.AttributeDTO.Description;
                _attributeDTO.HasMultipleOptions = _excelAttributeFileData.AttributeDTO.HasMultipleOptions;
                _attributeDTO.IsActive = true;

                if (isSucces == true)
                {
                    _excelAttributeFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelAttributeFileValidationDTO.AttributeGoodLinesList.Add(_attributeDTO);
                }
                else
                {
                    _attributeDTO.ID = _excelAttributeFileData.RowIteration;
                    _excelAttributeFileValidationDTO.ValidationResultDTO.Message = _excelAttributeFileValidationDTO.ValidationResultDTO.Message;
                    _excelAttributeFileValidationDTO.AttributeBadLinesList.Add(_attributeDTO);
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
        _excelAttributeFileValidationDTO.ValidationResultDTO = _validationResultDTO;
        return _excelAttributeFileValidationDTO;
    }
    #endregion 

    #endregion
}
