using CTSTools.DAL.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.DAL.Features.User;

public class TressSQL
{
    private static string GetTRESSConnectionString()
    {
        var _sqlString = ConfigurationManager.ConnectionStrings["TRESSConnectionString"].ToString();
        return _sqlString;
    }
    public static DataTable GetTRESSEmployeeSQL()
    {
        var _dataSet = new DataSet();
        var _queryString = " SELECT COLABORA.CB_NOMBRES + ' ' + COLABORA.CB_APE_PAT + ' '+ " +
                           " COLABORA.CB_APE_MAT AS Name, COLABORA.CB_CODIGO AS EmployeeNumber, " +
                           " IIF(COLABORA.[CB_ACTIVO] = 'S', 'true', 'false') AS IsActive " +
                           " FROM COLABORA ";
        DataTable _table = new DataTable();
        try
        {
            _dataSet = SQLCommonHelper.SQLQueryCommand(GetTRESSConnectionString(), _queryString, "SQL Server Exception");
            if (_dataSet.Tables.Count > 0)
            {
                _table = _dataSet.Tables[0];
            }
            return _table;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            throw ex;
        }
    }
}
