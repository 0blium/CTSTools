using CTSTools.BLL.Common;
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
                    _excelSupplierList = GetSupplierInfoListFromExcelFile(_fileBytes);
                }

                _excelSupplierRowsValidation = SupplierFileRowsValidation(_excelSupplierList);

                if (_excelSupplierRowsValidation.SupplierGoodLinesList.Count > 0)
                {
                    foreach (var _supplierDTO in _excelSupplierRowsValidation.SupplierGoodLinesList)
                    {
                        _supplierDTO.AddedByID = FileDTO.ID;
                        var _validationResulDTO = CreateSupplier_Global(_supplierDTO);
                    }
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
        _missingHeader += (_fileheaders.Contains("is active") == true || _fileheaders.Contains("isactive") == true) ? string.Empty : "isactive,";
        _missingHeader += (_fileheaders.Contains("name") == true) ? string.Empty : "name,";
        _missingHeader += (_fileheaders.Contains("is vendor") == true || _fileheaders.Contains("isvendor") == true) ? string.Empty : "isvendor,";
        _missingHeader += (_fileheaders.Contains("is manufacturer") == true || _fileheaders.Contains("ismanufacturer") == true) ? string.Empty : "ismanufacturer,";
        _missingHeader += (_fileheaders.Contains("description") == true) ? string.Empty : "description,";

        if (_missingHeader != string.Empty)
        {
            var lastComma = _missingHeader.LastIndexOf(',');
            _missingHeader = _missingHeader.Remove(lastComma, 1).Insert(lastComma, ".");
            _validationResultDTO.Result = false;
            _validationResultDTO.Description = string.Format("The following columns are missing: {0}", _missingHeader);
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

    private static List<ExcelSupplierDTO> GetSupplierInfoListFromExcelFile(byte[] FileBytes)
    {
        try
        {
            List<ExcelFileDTO> _excelFileDTOList = new List<ExcelFileDTO>();
            List<ExcelSupplierDTO> _excelSupplierDTOList = new List<ExcelSupplierDTO>();

            List<string> _columnName = new List<string>();
            Stream _fileStream = new MemoryStream(FileBytes);
            using (IExcelDataReader _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable firstTable = _excelDataSet.Tables[0];
                foreach (DataColumn column in firstTable.Columns)
                {
                    foreach (DataRow row in firstTable.Rows)
                    {
                        ExcelFileDTO _excelFileDTO = new ExcelFileDTO();

                        switch (row[column].ToString().ToUpper())
                        {
                            case "IS ACTIVE":
                            case "ISACTIVE":
                                _excelFileDTO.HeaderName = row[column].ToString();
                                break;
                            case "NAME":
                                _excelFileDTO.HeaderName = row[column].ToString();
                                break;
                            case "IS VENDOR":
                            case "ISVENDOR":
                                _excelFileDTO.HeaderName = row[column].ToString();
                                break;
                            case "IS MANUFACTURER":
                            case "ISMANUFACTURER":
                                _excelFileDTO.HeaderName = row[column].ToString();
                                break;
                            case "DESCRIPTION":
                                _excelFileDTO.HeaderName = row[column].ToString();
                                break;
                            default:
                                break;
                        }
                        if (_excelFileDTO.HeaderName != string.Empty && _excelFileDTO.HeaderName != null)
                        {
                            _excelFileDTO.ColumnName = column.ColumnName;
                            _excelFileDTOList.Add(_excelFileDTO);
                        }
                    }
                }
                foreach (DataRow row in firstTable.Rows)
                {
                    ExcelSupplierDTO _excelSupplierDTO = new ExcelSupplierDTO();
                    bool _haveInfo = false;
                    foreach (var item in _excelFileDTOList)
                    {
                        if (item.HeaderName != row[item.ColumnName].ToString() && row[item.ColumnName].ToString() != string.Empty)
                        {
                            switch (item.HeaderName.ToUpper())
                            {
                                case "IS ACTIVE":
                                case "ISACTIVE":
                                    _excelSupplierDTO.SupplierDTO.IsActive = Convert.ToBoolean(row[item.ColumnName].ToString());
                                    _haveInfo = true;
                                    break;
                                case "NAME":
                                    _excelSupplierDTO.SupplierDTO.Name = row[item.ColumnName].ToString();
                                    _haveInfo = true;
                                    break;
                                case "IS VENDOR":
                                case "ISVENDOR":
                                    _excelSupplierDTO.SupplierDTO.IsVendor = Convert.ToBoolean(row[item.ColumnName].ToString());
                                    _haveInfo = true;
                                    break;
                                case "IS MANUFACTURER":
                                case "ISMANUFACTURER":
                                    _excelSupplierDTO.SupplierDTO.IsManufacturer = Convert.ToBoolean(row[item.ColumnName].ToString());
                                    _haveInfo = true;
                                    break;
                                case "DESCRIPTION":
                                    _excelSupplierDTO.SupplierDTO.Description = row[item.ColumnName].ToString();
                                    _haveInfo = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (_haveInfo != false)
                    {
                        _excelSupplierDTOList.Add(_excelSupplierDTO);
                    }
                }
            }
            return _excelSupplierDTOList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw ex;
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
                    _excelSupplierFileValidationDTO.ValidationResultDTO.Message = "Error, The name is null or empty";
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
