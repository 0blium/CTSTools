using CTSTools.BLL.Common.Files;
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
using System.Text;

<<<<<<<< HEAD:CTSTools.BLL/Common/ExcelImport_Service.cs
namespace CTSTools.BLL.Common
========
namespace CTSTools.BLL.Common.Excel
>>>>>>>> origin/Edashboard:CTSTools.BLL/Common/Excel/ExcelDataImport_Service.cs
{
    public class ExcelImport_Service
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
                                .Select(header => System.Text.RegularExpressions.Regex.Replace(header, @"\s+", ""))
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
        public static string CleanRowString(string RowContent)
        {
            // null, empty, or contains only whitespace
            if (string.IsNullOrWhiteSpace(RowContent)) 
                return string.Empty;
            return System.Text.RegularExpressions.Regex.Replace(RowContent.Trim(), @"\s+", " ");
        }
    }
}
