using Elmah;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.DAL.Common
{
    public class SQLCommonHelper
    {
        static string _CTSToolsConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CTSToolsConnectionString"].ToString();

        public static DataSet SQLQueryCommand(string SQLConnectionString, string SQLString, string ErrorMessage)
        {

            DbProviderFactory Factory;
            DbConnection SQLConnection;
            DbCommand SQLCommand;
            DbDataAdapter DataAdapter;
            DataSet _dataSet = new DataSet();

            Factory = SqlClientFactory.Instance;
            SQLConnection = Factory.CreateConnection();
            try
            {
                SQLConnection.ConnectionString = SQLConnectionString; //_connectionString;
                SQLConnection.Open();

                SQLCommand = Factory.CreateCommand();
                SQLCommand.Connection = SQLConnection;
                SQLCommand.CommandType = CommandType.Text;
                SQLCommand.CommandText = SQLString;

                DataAdapter = Factory.CreateDataAdapter();
                DataAdapter.SelectCommand = SQLCommand;

                DataAdapter.Fill(_dataSet);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                SQLConnection.Close();
            }
            return _dataSet;
        }
        public static DataSet SQLQueryCommandNoParameterList(string SQLString, string ErrorMessage)
        {

            DbProviderFactory Factory;
            DbConnection SQLConnection;
            DbCommand SQLCommand;
            DbDataAdapter DataAdapter;
            DataSet _dataSet = new DataSet();

            Factory = SqlClientFactory.Instance;
            SQLConnection = Factory.CreateConnection();
            try
            {
                SQLConnection.ConnectionString = _CTSToolsConnectionString; //_connectionString;
                SQLConnection.Open();

                SQLCommand = Factory.CreateCommand();
                SQLCommand.Connection = SQLConnection;
                SQLCommand.CommandType = CommandType.Text;
                SQLCommand.CommandText = SQLString;

                DataAdapter = Factory.CreateDataAdapter();
                DataAdapter.SelectCommand = SQLCommand;

                DataAdapter.Fill(_dataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLConnection.Close();
            }
            return _dataSet;
        }
        public static DataSet SQLQueryCommand(string SQLString, string ErrorMessage, List<SqlParameter> ParameterList)
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CTSToolsConnectionString"].ToString();

            DbProviderFactory Factory;
            DbConnection SQLConnection;
            DbCommand SQLCommand;
            DbDataAdapter DataAdapter;
            DataSet _dataSet = new DataSet();

            Factory = SqlClientFactory.Instance;
            SQLConnection = Factory.CreateConnection();
            try
            {
                SQLConnection.ConnectionString = _connectionString;
                SQLConnection.Open();
                SQLCommand = Factory.CreateCommand();
                SQLCommand.Connection = SQLConnection;
                SQLCommand.CommandType = CommandType.Text;
                SQLCommand.CommandText = SQLString;
                if (ParameterList.Count > 0)
                {
                    foreach (SqlParameter _parameter in ParameterList)
                    {
                        SQLCommand.Parameters.Add(_parameter);
                    }
                }

                DataAdapter = Factory.CreateDataAdapter();
                DataAdapter.SelectCommand = SQLCommand;

                DataAdapter.Fill(_dataSet);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                SQLConnection.Close();
            }
            return _dataSet;
        }
        public static DataSet SQLNonQueryCommand(string SQLString, string ErrorMessage, List<SqlParameter> ParameterList)
        {
            DbProviderFactory Factory;
            DbConnection SQLConnection;
            DbCommand SQLCommand;
            DbDataAdapter DataAdapter;
            DataSet _dataSet = new DataSet();

            Factory = SqlClientFactory.Instance;
            SQLConnection = Factory.CreateConnection();
            try
            {
                SQLConnection.ConnectionString = _CTSToolsConnectionString;
                SQLConnection.Open();
                SQLCommand = Factory.CreateCommand();
                SQLCommand.Connection = SQLConnection;
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SQLCommand.CommandText = SQLString;
                SQLCommand.CommandTimeout = 0;
                if (ParameterList.Count > 0)
                {
                    foreach (SqlParameter _parameter in ParameterList)
                    {
                        SQLCommand.Parameters.Add(_parameter);
                    }
                }

                DataAdapter = Factory.CreateDataAdapter();
                DataAdapter.SelectCommand = SQLCommand;

                DataAdapter.Fill(_dataSet);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                SQLConnection.Close();
            }
            return _dataSet;
        }


        public static int SQLQueryCommandForSequence(string SQLString, string ErrorMessage)
        {

            DbProviderFactory Factory;
            DbConnection SQLConnection;
            DbCommand SQLCommand;
            DataSet _dataSet = new DataSet();
            int _result;
            Factory = SqlClientFactory.Instance;
            SQLConnection = Factory.CreateConnection();
            try
            {
                SQLConnection.ConnectionString = _CTSToolsConnectionString; //_connectionString;
                SQLConnection.Open();

                SQLCommand = Factory.CreateCommand();
                SQLCommand.Connection = SQLConnection;
                SQLCommand.CommandType = CommandType.Text;
                SQLCommand.CommandText = SQLString;
                _result = SQLCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                SQLConnection.Close();

            }
            return _result;
        }
    }
}
