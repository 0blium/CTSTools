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

    #region Upload Excel functions

    public static ValidationResultDTO GenerateSupplierFromExcel(FileDTO FileDTO)
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
            _validationResultDTO = GetSupplierListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = Supplier_Validator.ExcelSupplierInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create Supplier
            _validationResultDTO = Supplier_Repository.CreateMultiple(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetSupplierListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<SupplierDTO>(),
                BadRowLinesList = new List<SupplierDTO>()
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
                    var _supplierDTO = new SupplierDTO();
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _supplierDTO.ID = rowIndex + 1;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _supplierDTO.Name = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["NAME"]].ToString());
                    _supplierDTO.Description = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DESCRIPTION"]].ToString());
                    _supplierDTO.AddedByID = FileDTO.ID;

                    // Gets the value of the "ISVENDOR" column from the Excel file and cleans it of unwanted characters.
                    string _rowIsVendorValue = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["ISVENDOR"]]?.ToString());
                    // bool.TryParse try to convert the value (_rowIsVendorValue) to a text of type boolean
                    // Note: TryParse will not throw an exception if the conversion fails.
                    // The 'out' keyword indicates that IsVendor is an output parameter.
                    if (bool.TryParse(_rowIsVendorValue, out bool IsVendor))
                        // If the conversion is successful, the converted value will be assigned to the Goal variable.
                        _supplierDTO.IsVendor = IsVendor;
                    else
                        _supplierDTO.IsVendor = false;

                    string _rowIsManufacturerValue = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["ISMANUFACTURER"]]?.ToString());
                    if (bool.TryParse(_rowIsManufacturerValue, out bool IsManufacturer))
                        _supplierDTO.IsManufacturer = IsManufacturer;
                    else
                        _supplierDTO.IsManufacturer = false;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = Supplier_Validator.ExcelSupplierRows_Validation(_supplierDTO);

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
