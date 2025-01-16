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
        private static List<PartNumberDTO> GetPartNumberInfoListFromExcelFile(byte[] FileBytes)
        {
            try
            {
                List<ExcelFileDTO> _excelFileDTOList = new List<ExcelFileDTO>();
                List<PartNumberDTO> _partNumberDTOList = new List<PartNumberDTO>();

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
                                case "ID":
                                    _excelFileDTO.HeaderName = row[column].ToString();
                                    break;
                                case "PARTNUMBER":
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
                        PartNumberDTO _partNumberDTO = new PartNumberDTO();
                        bool _haveInfo = false;
                        foreach (var item in _excelFileDTOList)
                        {
                            if (item.HeaderName != row[item.ColumnName].ToString() && row[item.ColumnName].ToString() != string.Empty)
                            {
                                switch (item.HeaderName.ToUpper())
                                {
                                    case "ID":
                                        _partNumberDTO.ID = Convert.ToInt32(row[item.ColumnName].ToString());
                                        _haveInfo = true;
                                        break;
                                    case "PARTNUMBER":
                                        _partNumberDTO.PartNumber = row[item.ColumnName].ToString();
                                        _haveInfo = true;
                                        break;
                                    case "DESCRIPTION":
                                        _partNumberDTO.Description = row[item.ColumnName].ToString();
                                        _haveInfo = true;
                                        break;
                                    case "ISACTIVE":
                                        _partNumberDTO.IsActive = Convert.ToBoolean(row[item.ColumnName].ToString());
                                        _haveInfo = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (_haveInfo != false)
                        {
                            _partNumberDTOList.Add(_partNumberDTO);
                        }
                    }
                }
                return _partNumberDTOList;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }

        #endregion

        #region Business Logic

        public static PartNumberDTO PartNumberFileValidation_Global(string Base64File, string Filename)
        {
            PartNumberDTO _partNumberDTO = new PartNumberDTO();
            try
            {
                byte[] _fileBytes = Convert.FromBase64String(Base64File.Split(',')[1]);
                ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
                _validationResultDTO.Result = true;
                _validationResultDTO.Message = "Success";
                _validationResultDTO.Description = "Comparission was made successfully";

                var _partNumberRowsValidation = new PartNumberDTO();
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
                _partNumberDTO = ValidatePartNumberFileColumns(_fileBytes, Filename, _extension);
                _validationResultDTO = _partNumberDTO.ValidationResultDTO;

                if (_validationResultDTO.Result == true)
                {
                    //Step 2. Read the file and get the rows in vendordto format
                    var _partNumberList = new List<PartNumberDTO>();
                    if (_extension == ".xls" || _extension == ".xlsx")
                    {
                        _partNumberList = GetPartNumberInfoListFromExcelFile(_fileBytes);
                    }
                    //else
                    //{
                    //    _vendorFileDataList = GetVendorListFromTxtFile(_fileBytes);
                    //}
                    _partNumberRowsValidation = PartNumberFileRowsValidation(_partNumberList);

                    _validationResultDTO.Result = _partNumberRowsValidation.ValidationResultDTO.Result;
                    _validationResultDTO.Message = _partNumberRowsValidation.ValidationResultDTO.Message;
                    _validationResultDTO.Description = _partNumberRowsValidation.ValidationResultDTO.Description;
                }
                _partNumberDTO.ValidationResultDTO = _validationResultDTO;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
            return _partNumberDTO;
        }

        private static PartNumberDTO ValidatePartNumberFileColumns(byte[] _fileBytes, string Filename, string Extension)
        {
            string[] _fileheaders = new string[0];
            PartNumberDTO _partNumberFileValidationDTO = new PartNumberDTO();
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
            _missingHeader += (_fileheaders.Contains("id") == true) ? string.Empty : "id,";
            _missingHeader += (_fileheaders.Contains("partnumber") == true) ? string.Empty : "partnumber,";
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
            _partNumberFileValidationDTO.ValidationResultDTO = _validationResultDTO;

            return _partNumberFileValidationDTO;
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

        private static PartNumberDTO PartNumberFileRowsValidation(List<PartNumberDTO> PartNumberFileDataList)
        {
            PartNumberDTO _partNumberFileValidationDTO = new PartNumberDTO();
            List<PartNumberDTO> _partNumberFileValidationList = new List<PartNumberDTO>();
            ValidationResultDTO _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "";
            try
            {

                foreach (var _partNumberFileData in PartNumberFileDataList)
                {
                    PartNumberDTO _partNumberDTO = new PartNumberDTO();
                    bool isSucces = true;
                    if (_partNumberFileData.ID == 0 || _partNumberFileData.ID == null)
                    {
                        _partNumberDTO.ValidationResultDTO.Message = "Error, The PartNumber ID is null or empty.";
                        isSucces = false;
                    }
                    if (_partNumberFileData.PartNumber == string.Empty || _partNumberFileData.PartNumber == null)
                    {
                        _partNumberDTO.ValidationResultDTO.Message = "Error, The PartNumber number is null or empty";
                        isSucces = false;
                    }

                    // Validate if the vendor file rows are not repited
                    foreach (var _partNumberFileRows in PartNumberFileDataList)
                    {
                        if (_partNumberFileRows != _partNumberFileData)
                        {
                            if (_partNumberFileRows.ID == _partNumberFileData.ID)
                            {
                                _partNumberDTO.ValidationResultDTO.Message = "Error, this ID is repeated with other inside the file.";
                                isSucces = false;
                            }
                            else if (_partNumberFileRows.PartNumber == _partNumberFileData.PartNumber)
                            {
                                _partNumberDTO.ValidationResultDTO.Message = "Error, this PartNumber is repeated with other inside the file.";
                                isSucces = false;
                            }
                        }
                    }
                    _partNumberDTO.ID = _partNumberFileData.ID;
                    _partNumberDTO.PartNumber = _partNumberFileData.PartNumber;
                    _partNumberDTO.IsActive = true;

                    if (isSucces == true)
                    {
                        _partNumberDTO.ValidationResultDTO.Message = "Success";
                        _partNumberFileValidationList.Add(_partNumberDTO);
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
            _partNumberFileValidationDTO.ValidationResultDTO = _validationResultDTO;
            return _partNumberFileValidationDTO;
        }

        #endregion
    }
}
