using DevExpress.Xpo;
using Elmah;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CTSTools.BLL.Common.Files
{
    public class ExcelDataImport_Service
    {
        #region CRUD
        private static List<GenericCatalogDTO> GetCatalogInfoListFromExcelFile(byte[] FileBytes)
        {
            try
            {
                List<ExcelFileDTO> _excelFileDTOList = new List<ExcelFileDTO>();
                List<GenericCatalogDTO> _genericCatalogDTOList = new List<GenericCatalogDTO>();

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
                                case "NAME":
                                    _excelFileDTO.HeaderName = row[column].ToString();
                                    break;
                                case "DESCRIPTION":
                                    _excelFileDTO.HeaderName = row[column].ToString();
                                    break;
                                case "ISACTIVE":
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
                        GenericCatalogDTO _genericCatalogDTO = new GenericCatalogDTO();
                        bool _haveInfo = false;
                        foreach (var item in _excelFileDTOList)
                        {
                            if (item.HeaderName != row[item.ColumnName].ToString() && row[item.ColumnName].ToString() != string.Empty)
                            {
                                switch (item.HeaderName.ToUpper())
                                {
                                    case "NAME":
                                        _genericCatalogDTO.Name = row[item.ColumnName].ToString();
                                        _haveInfo = true;
                                        break;
                                    case "DESCRIPTION":
                                        _genericCatalogDTO.Description = row[item.ColumnName].ToString();
                                        _haveInfo = true;
                                        break;
                                    case "ISACTIVE":
                                        _genericCatalogDTO.IsActive = Convert.ToBoolean(row[item.ColumnName].ToString());
                                        _haveInfo = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (_haveInfo != false)
                        {
                            _genericCatalogDTOList.Add(_genericCatalogDTO);
                        }
                    }
                }
                return _genericCatalogDTOList;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }

        #endregion

        #region Business Logic

        public static GenericCatalogDTO GenericCatalogFileValidation_Global(string Base64File, string Filename)
        {
            GenericCatalogDTO _genericCatalogDTO = new GenericCatalogDTO();
            try
            {
                byte[] _fileBytes = Convert.FromBase64String(Base64File.Split(',')[1]);
                ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
                _validationResultDTO.Result = true;
                _validationResultDTO.Message = "Success";
                _validationResultDTO.Description = "Comparission was made successfully";

                var _genericCatalogRowsValidation = new GenericCatalogDTO();
                string _extension = "";
                if (Filename.Contains(".xlsx"))
                {
                    _extension = ".xlsx";
                }
                else if (Filename.Contains(".xls"))
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
                _genericCatalogDTO = ValidateGenericCatalogFileColumns(_fileBytes, Filename, _extension);
                _validationResultDTO = _genericCatalogDTO.ValidationResultDTO;

                if (_validationResultDTO.Result == true)
                {
                    //Step 2. Read the file and get the rows in vendordto format
                    var _genericCatalogList = new List<GenericCatalogDTO>();
                    if (_extension == ".xls" || _extension == ".xlsx")
                    {
                        _genericCatalogList = GetCatalogInfoListFromExcelFile(_fileBytes);
                    }
                    //else
                    //{
                    //    _vendorFileDataList = GetVendorListFromTxtFile(_fileBytes);
                    //}
                    _genericCatalogRowsValidation = GenericCatalogFileRowsValidation(_genericCatalogList);

                    _validationResultDTO.Result = _genericCatalogRowsValidation.ValidationResultDTO.Result;
                    _validationResultDTO.Message = _genericCatalogRowsValidation.ValidationResultDTO.Message;
                    _validationResultDTO.Description = _genericCatalogRowsValidation.ValidationResultDTO.Description;
                }
                _genericCatalogDTO.ValidationResultDTO = _validationResultDTO;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
            return _genericCatalogDTO;
        }

        private static GenericCatalogDTO ValidateGenericCatalogFileColumns(byte[] _fileBytes, string Filename, string Extension)
        {
            string[] _fileheaders = new string[0];
            GenericCatalogDTO _genericCatalogFileValidationDTO = new GenericCatalogDTO();
            ValidationResultDTO _validationResultDTO = new ValidationResultDTO();

            if (Extension == ".xlsx" || Extension == ".xls")
            {
                _fileheaders = GetHeadersFromExcel(_fileBytes);
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Description = "Input only excel files.";
                _validationResultDTO.Message = "Invalid file";
            }
            string _missingHeader = string.Empty;
            _missingHeader += (_fileheaders.Contains("name") == true) ? string.Empty : "Name,";
            _missingHeader += (_fileheaders.Contains("description") == true) ? string.Empty : "description,";
            _missingHeader += (_fileheaders.Contains("isactive") == true) ? string.Empty : "isactive,";

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
            _genericCatalogFileValidationDTO.ValidationResultDTO = _validationResultDTO;

            return _genericCatalogFileValidationDTO;
        }

        private static string[] GetHeadersFromExcel(byte[] FileBytes)
        {
            try
            {
                string[] _fileheaders;
                Stream _fileStream = new MemoryStream(FileBytes);
                using (IExcelDataReader _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
                {
                    var _excelDataSet = _excelReader.AsDataSet();
                    int _headerRow = 0;
                    int _excelRowCounter = _excelDataSet.Tables[0].Rows.Count;

                    for (int i = 0; i < _excelRowCounter; i++)
                    {
                        var _excelRow = _excelDataSet.Tables[0].Rows[i].ItemArray.Where(f => f.ToString() != string.Empty);
                        if (_excelRow.Count() > 0)
                        {
                            _headerRow = i;
                            break;
                        }
                    }
                    _fileheaders = _excelDataSet.Tables[0].Rows[_headerRow].ItemArray.Select(f => f.ToString().ToLower()).ToArray();
                }
                return _fileheaders;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }

        private static GenericCatalogDTO GenericCatalogFileRowsValidation(List<GenericCatalogDTO> VendorFileDataList)
        {
            GenericCatalogDTO _vendorFileValidationDTO = new GenericCatalogDTO();
            List<GenericCatalogDTO> _genericCatalogFileValidationDTO = new List<GenericCatalogDTO>();
            ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "";
            try
            {

                foreach (var _vendorFileData in VendorFileDataList)
                {
                    GenericCatalogDTO _genericCatalogDTO = new GenericCatalogDTO();
                    bool isSucces = true;
                    if (_vendorFileData.Name == string.Empty || _vendorFileData.Name == null)
                    {
                        _genericCatalogDTO.ValidationResultDTO.Message = "Error, The vendor name is null or empty.";
                        isSucces = false;
                    }
                    if (_vendorFileData.Description == string.Empty || _vendorFileData.Description == null)
                    {
                        _genericCatalogDTO.ValidationResultDTO.Message = "Error, The vendor number is null or empty";
                        isSucces = false;
                    }

                    // Validate if the vendor file rows are not repited
                    foreach (var _vendorFileRows in VendorFileDataList)
                    {
                        if (_vendorFileRows != _vendorFileData)
                        {
                            if (_vendorFileRows.Name == _vendorFileData.Name)
                            {
                                _genericCatalogDTO.ValidationResultDTO.Message = "Error, this name is repeated with other inside the file.";
                                isSucces = false;
                            }
                            else if (_vendorFileRows.Description == _vendorFileData.Description)
                            {
                                _genericCatalogDTO.ValidationResultDTO.Message = "Error, this number is repeated with other inside the file.";
                                isSucces = false;
                            }
                        }
                    }
                    _genericCatalogDTO.Name = _vendorFileData.Name;
                    _genericCatalogDTO.Description = _vendorFileData.Description;
                    _genericCatalogDTO.IsActive = true;

                    if (isSucces == true)
                    {
                        _genericCatalogDTO.ValidationResultDTO.Message = "Success";
                        _genericCatalogFileValidationDTO.Add(_genericCatalogDTO);
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
            _vendorFileValidationDTO.ValidationResultDTO = _validationResultDTO;
            return _vendorFileValidationDTO;
        }

        #endregion
    }
}
