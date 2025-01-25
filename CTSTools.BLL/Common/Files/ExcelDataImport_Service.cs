using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
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

        public static string[] GetHeadersFromExcel(byte[] FileBytes)
        {
            try
            {
                string[] _fileheaders = new string[0]; // Valor predeterminado para el caso vacío
                Stream _fileStream = new MemoryStream(FileBytes);
                using (IExcelDataReader _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
                {
                    var _excelDataSet = _excelReader.AsDataSet();

                    // Check if table has rows before processing
                    if (_excelDataSet.Tables.Count > 0 && _excelDataSet.Tables[0].Rows.Count > 0)
                    {
                        int _headerRow = 0;
                        int _excelRowCounter = _excelDataSet.Tables[0].Rows.Count;

                        // Find non-empty header row
                        for (int i = 0; i < _excelRowCounter; i++)
                        {
                            var _excelRow = _excelDataSet.Tables[0].Rows[i].ItemArray.Where(f => f.ToString() != string.Empty);
                            if (_excelRow.Count() > 0)
                            {
                                _headerRow = i;
                                break;
                            }
                        }
                        // Access headers if they exist
                        _fileheaders = _excelDataSet.Tables[0].Rows[_headerRow].ItemArray
                                .Select(f => f.ToString().ToLower())
                                .Select(header => header.Trim())
                                .Select(header => System.Text.RegularExpressions.Regex.Replace(header, @"\s+", " "))
                                .ToArray();
                    }
                }
                return _fileheaders;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }

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
    }
}
