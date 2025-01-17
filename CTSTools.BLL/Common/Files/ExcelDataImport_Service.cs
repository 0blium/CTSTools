using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
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

        #endregion

        #region Business Logic


        public static string[] GetHeadersFromExcel(byte[] FileBytes)
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

        #endregion
    }
}
