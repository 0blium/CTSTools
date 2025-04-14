using CTSTools.BLL.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common.Excel
{
    public class ExcelImport_Validator
    {
        public static ValidationResultDTO ExcelFile_Validation(FileDTO FileDTO)
        {
            var _validation_ResultDTO = new ValidationResultDTO
            {
                Description = "The record has been validated successfully.."
            };
            try
            {
                var _validation_ResultList = new List<ValidationResultDTO>();

                if (!FileDTO.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error",
                        Description = "Invalid file extension, only .xlsx files are allowed."
                    });
                }
                if (string.IsNullOrEmpty(FileDTO.Data))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error",
                        Description = "Empty file, the file does not have information"
                    });
                }

                // if list contains a error, update main validation result
                if (_validation_ResultList.Count > 0)
                {
                    _validation_ResultDTO.Result = false;
                    _validation_ResultDTO.Message = "Errors!";
                    _validation_ResultDTO.Description = "There is a list of errors exist.";
                    _validation_ResultDTO.ValidationResultList = _validation_ResultList;
                }
            }
            catch (Exception ex)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Error";
                _validation_ResultDTO.Description = $"An error occurred while validating the file: {ex.Message}";
            }
            return _validation_ResultDTO;
        }
        public static ValidationResultDTO ExcelHeaderColumns_Validation(FileDTO FileDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The file has the correct format."
            };
            // Remove "ID" from properties that have "ID" at the end (e.g. "UnitOfMeasureID" -> "UnitOfMeasure")
            var _headerList = FileDTO.DirectoryArray.Select(header => header.Replace("ID", string.Empty)).ToList();
            // Load headers from Excel file
            var _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            var _fileHeaders = ExcelImport_Service.GetHeadersFromExcel(_fileBytes);
            var _missingHeader = string.Empty;
            foreach (var Header in _headerList)
            {
                if (!_fileHeaders.Contains(Header, StringComparer.OrdinalIgnoreCase))
                {
                    // Looks for capital letters that are not at the beginning of the string and inserts a space before them.
                    // e.g. converts "UnitOfMeasure" to "Unit Of Measure" by finding "U", "O" and "M".
                    var _header = System.Text.RegularExpressions.Regex.Replace(Header, "(\\B[A-Z])", " $1");
                    _missingHeader += _header + ",<br>";
                }
            }
            // Check for missing headers
            if (_missingHeader.Length > 0)
            {
                // Delete the last comma and replace it with a period
                var _lastComma = _missingHeader.LastIndexOf(',');
                _missingHeader = _missingHeader.Remove(_lastComma, 1).Insert(_lastComma, ".");
                return new ValidationResultDTO
                {
                    Result = false,
                    Description = $"The following columns are missing:<br> {_missingHeader}",
                    Message = "Error"
                };
            }
            _validationResultDTO.Data = _headerList.ToArray();
            return _validationResultDTO;
        }
    }
}
