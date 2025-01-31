using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
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
            FileDTO.FileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
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
            //_validationResultDTO = Supplier_Repository.CreateMultipleSupplier(_excelRowDTO.GoodRowLinesList);
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
            using (var _fileStream = new MemoryStream(FileDTO.FileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                var _rowHeaderDic = FileDTO.DirectoryArray.ToDictionary(header => header.ToUpper(), header => header.ToUpper());

                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    string headerName = _firstTable.Rows[0][colIndex].ToString().ToUpper();
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", "");
                    if (_rowHeaderDic.TryGetValue(headerName, out string mappedHeader))
                    {
                        _columnHeaderMap[mappedHeader] = colIndex;
                    }
                }

                // Process rows starting from the second row (index 1)
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    var _supplierDTO = new SupplierDTO();
                    bool _haveInfo = false;

                    // Mapping Dictionary: Map columns to _supplierDTO properties
                    var _propertyMap = new Dictionary<string, Action<string>>
                    {
                        { "NAME", value => _supplierDTO.Name = value },
                        { "DESCRIPTION", value => _supplierDTO.Description = value }
                    };

                    // Iterate over the dictionary and assign values
                    foreach (var entry in _propertyMap)
                    {
                        if (_columnHeaderMap.TryGetValue(entry.Key, out int index))
                        {
                            entry.Value(ExcelImport_Service.CleanRowString(row[index]?.ToString()));
                            _haveInfo = true;
                        }
                    }

                    if (_columnHeaderMap.TryGetValue("ISVENDOR", out int IsVendorIndex))
                    {
                        string _isVendorIndexValue = row[IsVendorIndex].ToString().Trim();
                        _isVendorIndexValue = System.Text.RegularExpressions.Regex.Replace(_isVendorIndexValue, @"\s+", " ");
                        if (bool.TryParse(_isVendorIndexValue, out bool IsVendor))
                            _supplierDTO.IsVendor = IsVendor;
                        else
                            _supplierDTO.IsVendor = false;
                        _haveInfo = true;
                    }
                    if (_columnHeaderMap.TryGetValue("ISMANUFACTURER", out int IsManufacturerIndex))
                    {
                        string _isManufacturerIndexValue = row[IsManufacturerIndex].ToString().Trim();
                        _isManufacturerIndexValue = System.Text.RegularExpressions.Regex.Replace(_isManufacturerIndexValue, @"\s+", " ");
                        if (bool.TryParse(_isManufacturerIndexValue, out bool IsManufacturer))
                            _supplierDTO.IsManufacturer = IsManufacturer;
                        else
                            _supplierDTO.IsManufacturer = false;
                        _haveInfo = true;
                    }

                    if (_haveInfo)
                    {
                        _supplierDTO.ID = rowIndex + 1;
                        _supplierDTO.AddedByID = FileDTO.ID;
                        _validationResultDTO = Supplier_Validator.ExcelSupplierRows_Validation(_supplierDTO);
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
