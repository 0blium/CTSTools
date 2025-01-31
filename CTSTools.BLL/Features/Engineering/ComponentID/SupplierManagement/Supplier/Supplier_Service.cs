using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

public class Supplier_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSupplier_Global(SupplierDTO SupplierDTO)
    {

        var _validationResultDTO = Supplier_Validator.CreateSupplier_Validation(SupplierDTO);
        if (_validationResultDTO.Result)
        {
            SupplierDTO.AddedDate = DateTime.Now;
            _validationResultDTO = Supplier_Repository.CreateSupplier(SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateSupplier_Global(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Supplier_Validator.UpdateSupplier_Validation(SupplierDTO);
        if (_validationResultDTO.Result)
        {
            SupplierDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Supplier_Repository.UpdateSupplier(SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteSupplier_Global(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Supplier_Validator.DeleteSupplier_Validation(SupplierDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Supplier_Repository.DeleteSupplier(SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static List<SupplierDTO> GetSupplierList_Global(SupplierDTO SupplierDTO, PagedResultDTO<SupplierDTO> PagedSupplierDTO = null)
    {
        var _facilityGlobalList = new List<SupplierDTO>();
        try
        {
            var _facilityList = Supplier_Repository.GetSupplierList(SupplierDTO, PagedSupplierDTO);
            _facilityGlobalList = _facilityList;
            return _facilityGlobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _facilityGlobalList;
    }

    public static int GetTotalCount(PagedResultDTO<SupplierDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Supplier_Repository.GetSupplierCount(PagedResultDTO.Filter, PagedResultDTO);
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
    public static ValidationResultDTO SupplierFileValidation_Global(FileDTO FileDTO)
    {
        ExcelSupplierDTO _excelSupplierDTO = new ExcelSupplierDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        try
        {
            byte[] _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "Comparission was made successfully";

            var _excelSupplierRowsValidation = new ExcelSupplierDTO();
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
            _excelSupplierDTO = ValidateSupplierFileColumns(_fileBytes, FileDTO.FileName, _extension);
            _validationResultDTO = _excelSupplierDTO.ValidationResultDTO;

            if (_validationResultDTO.Result == true)
            {
                //Step 2. Read the file and get the rows in vendordto format
                var _excelSupplierList = new List<ExcelSupplierDTO>();
                if (_extension == ".xls" || _extension == ".xlsx")
                {
                    _validationResultDTO = GetSupplierInfoListFromExcelFile(_fileBytes);
                }
                if (_validationResultDTO.Data != null)
                {
                    _excelSupplierRowsValidation = SupplierFileRowsValidation(_validationResultDTO.Data);

                    if (_excelSupplierRowsValidation.SupplierGoodLinesList.Count > 0)
                    {
                        foreach (var _supplierDTO in _excelSupplierRowsValidation.SupplierGoodLinesList)
                        {
                            _supplierDTO.AddedByID = FileDTO.ID;
                            var _validationResulDTO = CreateSupplier_Global(_supplierDTO);
                        }
                    }
                }
                else
                {
                    return _validationResultDTO;
                }

                _validationResultDTO.Result = _excelSupplierRowsValidation.ValidationResultDTO.Result;
                _validationResultDTO.Message = _excelSupplierRowsValidation.ValidationResultDTO.Message;
                _validationResultDTO.Description = _excelSupplierRowsValidation.ValidationResultDTO.Description;
            }
            _validationResultDTO.Data = _excelSupplierRowsValidation;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }

    private static ExcelSupplierDTO ValidateSupplierFileColumns(byte[] _fileBytes, string Filename, string Extension)
    {
        string[] _fileheaders = new string[0];
        ExcelSupplierDTO _excelSupplierFileValidationDTO = new ExcelSupplierDTO();
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
        _missingHeader += (_fileheaders.Contains("name") == true) ? string.Empty : "name, <br>";
        _missingHeader += (_fileheaders.Contains("is vendor") == true || _fileheaders.Contains("isvendor") == true) ? string.Empty : "isvendor, <br>";
        _missingHeader += (_fileheaders.Contains("is manufacturer") == true || _fileheaders.Contains("ismanufacturer") == true) ? string.Empty : "ismanufacturer, <br>";
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
        _excelSupplierFileValidationDTO.ValidationResultDTO = _validationResultDTO;

        return _excelSupplierFileValidationDTO;
    }

    private static ValidationResultDTO GetSupplierInfoListFromExcelFile(byte[] FileBytes)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            List<ExcelSupplierDTO> _excelSupplierDTOList = new List<ExcelSupplierDTO>();
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
                        headerName.Equals("ISMANUFACTURER", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("IS MANUFACTURER", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("ISVENDOR", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("IS VENDOR", StringComparison.OrdinalIgnoreCase) ||
                        headerName.Equals("DESCRIPTION", StringComparison.OrdinalIgnoreCase))
                    {
                        _columnHeaderMap[headerName.ToUpper()] = colIndex;
                    }
                }
                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _excelSupplierDTO = new ExcelSupplierDTO();
                    bool _haveInfo = false;
                    // Assign values ​​directly using the dictionary
                    if (_columnHeaderMap.TryGetValue("NAME", out int NameIndex))
                    {
                        string _nameValue = row[NameIndex].ToString().Trim();
                        _nameValue = System.Text.RegularExpressions.Regex.Replace(_nameValue, @"\s+", " ");
                        _excelSupplierDTO.SupplierDTO.Name = _nameValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("DESCRIPTION", out int DescriptionIndex))
                    {
                        string _descriptionValue = row[DescriptionIndex].ToString().Trim();
                        _descriptionValue = System.Text.RegularExpressions.Regex.Replace(_descriptionValue, @"\s+", " ");
                        _excelSupplierDTO.SupplierDTO.Description = _descriptionValue;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("IS MANUFACTURER", out int IsManufacturerIndex) ||
                        _columnHeaderMap.TryGetValue("ISMANUFACTURER", out IsManufacturerIndex))
                    {
                        string _isManufacturerValue = row[IsManufacturerIndex].ToString().Trim();
                        _isManufacturerValue = System.Text.RegularExpressions.Regex.Replace(_isManufacturerValue, @"\s+", " ");
                        if (bool.TryParse(_isManufacturerValue, out bool IsManufacturer))
                        {
                            _excelSupplierDTO.SupplierDTO.IsManufacturer = IsManufacturer;
                            _haveInfo = true;
                        }
                        else
                        {
                            throw new InvalidCastException($"The value to Is Manufacturer: '{_isManufacturerValue}', is not a valid boolean.");
                        }
                    }
                    if (_columnHeaderMap.TryGetValue("IS VENDOR", out int IsVendorIndex) ||
                        _columnHeaderMap.TryGetValue("ISVENDOR", out IsVendorIndex))
                    {
                        string _isVendorIndexValue = row[IsVendorIndex].ToString().Trim();
                        _isVendorIndexValue = System.Text.RegularExpressions.Regex.Replace(_isVendorIndexValue, @"\s+", " ");
                        if (bool.TryParse(_isVendorIndexValue, out bool IsVendor))
                        {
                            _excelSupplierDTO.SupplierDTO.IsVendor = IsVendor;
                            _haveInfo = true;
                        }
                        else
                        {
                            throw new InvalidCastException($"The value to Is Vendor: '{_isVendorIndexValue}', is not a valid boolean.");
                        }
                    }
                    if (_haveInfo)
                    {
                        _excelSupplierDTOList.Add(_excelSupplierDTO);
                    }
                }
            }
            if (_excelSupplierDTOList.Count > 0)
            {
                _validationResultDTO.Data = _excelSupplierDTOList;
                return _validationResultDTO;
            }
            else {
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

    private static ExcelSupplierDTO SupplierFileRowsValidation(List<ExcelSupplierDTO> ExcelSupplierFileDataList)
    {
        ExcelSupplierDTO _excelSupplierFileValidationDTO = new ExcelSupplierDTO();
        ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Result = true;
        _validationResultDTO.Message = "Success";
        _validationResultDTO.Description = "";
        try
        {

            foreach (var _excelSupplierFileData in ExcelSupplierFileDataList)
            {
                SupplierDTO _supplierDTO = new SupplierDTO();
                bool isSucces = true;
                if (_excelSupplierFileData.SupplierDTO.Name == string.Empty || _excelSupplierFileData.SupplierDTO.Name == null)
                {
                    _excelSupplierFileData.SupplierDTO.Name = "Error, The name is null or empty";
                    isSucces = false;
                }

                _supplierDTO.Name = _excelSupplierFileData.SupplierDTO.Name;
                _supplierDTO.Description = _excelSupplierFileData.SupplierDTO.Description;
                _supplierDTO.IsActive = true;
                _supplierDTO.IsVendor = _excelSupplierFileData.SupplierDTO.IsVendor;
                _supplierDTO.IsManufacturer = _excelSupplierFileData.SupplierDTO.IsManufacturer;

                if (isSucces == true)
                {
                    _excelSupplierFileValidationDTO.ValidationResultDTO.Message = "Success";
                    _excelSupplierFileValidationDTO.SupplierGoodLinesList.Add(_supplierDTO);
                }
                else
                {
                    _excelSupplierFileValidationDTO.ValidationResultDTO.Message = _excelSupplierFileValidationDTO.ValidationResultDTO.Message;
                    _excelSupplierFileValidationDTO.SupplierBadLinesList.Add(_supplierDTO);
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
        _excelSupplierFileValidationDTO.ValidationResultDTO = _validationResultDTO;
        return _excelSupplierFileValidationDTO;
    }
    #endregion

    #endregion
}
