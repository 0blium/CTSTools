using CTSTools.BLL.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common
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
                        Message = "Invalid file extension.",
                        Description = "Only .xlsx files are allowed."
                    });
                }
                if (string.IsNullOrEmpty(FileDTO.Data))
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Empty file",
                        Description = "The file does not have information"
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
            // Delete properties "ID" and "IsActive" or those that we need to remove
            var _headerList = FileDTO.DirectoryArray
                .Where(header => header != "ID" && header != "IsActive")
                .Select(header => header.Contains("DashboardCategoryID") ? "Category" : header)
                .ToList();
            // Remove "ID" from properties that have "ID" at the end (e.g. "UnitOfMeasureID" -> "UnitOfMeasure")
            _headerList = _headerList
                .Select(header => header.Replace("ID", string.Empty))
                .ToList();
            // Load headers from Excel file
            var _fileHeaders = ExcelImport_Service.GetHeadersFromExcel(FileDTO.FileBytes);
            // Use StringBuilder to improve performance when building the message
            var _missingHeaders = new StringBuilder();
            foreach (var Header in _headerList)
            {
                if (!_fileHeaders.Contains(Header, StringComparer.OrdinalIgnoreCase))
                {
                    // Replace underscores with spaces (e.g. "UnitOfMeasure" -> "Unit Of Measure")
                    var _header = System.Text.RegularExpressions.Regex.Replace(Header, "(\\B[A-Z])", " $1");
                    _missingHeaders.Append(_header + ",<br>");
                }
            }
            // Check for missing headers
            if (_missingHeaders.Length > 0)
            {
                // Delete the last comma and replace it with a period
                _missingHeaders.Length -= 5;
                _missingHeaders.Append(".");
                return new ValidationResultDTO
                {
                    Result = false,
                    Description = $"The following columns are missing:<br> {_missingHeaders}",
                    Message = "Error"
                };
            }
            _validationResultDTO.Data = _headerList.ToArray();
            return _validationResultDTO;
        }
    }
}
