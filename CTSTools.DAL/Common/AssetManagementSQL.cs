using Elmah;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.DAL.Common
{
    public class AssetManagementSQL
    {
        public static string GetItemLineSerial()
        {
            string _selectSerialSequence = string.Format(" DECLARE @ItemLineSerialSequence BIGINT " +
                                                         " SET @ItemLineSerialSequence  = NEXT VALUE FOR ItemLineSerialSequence; " +
                                                         " Select @ItemLineSerialSequence As Serial;");
            try
            {
                DataSet _ds = SQLCommonHelper.SQLQueryCommandNoParameterList(_selectSerialSequence, "Error on getting data from server");
                if (_ds.Tables.Count > 0)
                {
                    return Convert.ToString(_ds.Tables[0].Rows[0]["Serial"]);
                }
                return "0";
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
        }
        public static string GetStationSerial()
        {
            string _selectSerialSequence = string.Format(" DECLARE @StationSerialSequence BIGINT " +
                                                          " SET @StationSerialSequence = NEXT VALUE FOR StationSerialSequence; " +
                                                          " Select @StationSerialSequence As Serial;");
            try
            {
                DataSet _ds = SQLCommonHelper.SQLQueryCommandNoParameterList(_selectSerialSequence, "Error on getting data from server");
                if (_ds.Tables.Count > 0)
                {
                    return Convert.ToString(_ds.Tables[0].Rows[0]["Serial"]);
                }
                return "0";
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
        }
        public static string GetChangeGroupSequence()
        {
            string _selectChangeGroupSequence = string.Format(" DECLARE @ChangeGroupSequence BIGINT " +
                                                          " SET @ChangeGroupSequence = NEXT VALUE FOR ChangeGroupSequence; " +
                                                          " Select @ChangeGroupSequence As Serial;");
            try
            {
                DataSet _ds = SQLCommonHelper.SQLQueryCommandNoParameterList(_selectChangeGroupSequence, "Error on getting data from server");
                if (_ds.Tables.Count > 0)
                {
                    return Convert.ToString(_ds.Tables[0].Rows[0]["Serial"]);
                }
                return "0";
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
        }
    }
}
