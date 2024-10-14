using CTSTools.DAL.Common;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Data;

namespace CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;

public class Class_Sequence_SQL
{
    public static string CreateClass_Sequence(string ClassIDName)
    {
        string _result = string.Empty;
        try
        {
            string _namewithoutspaces = $"{ClassIDName.Trim().Replace(" ", "_")}_Sequence";
            string _selectSerialSequence = $" CREATE SEQUENCE [dbo].[{_namewithoutspaces}] MINVALUE 1 ";
            int _ds = SQLCommonHelper.SQLQueryCommandForSequence(_selectSerialSequence, "Error on getting data from server");
            if (_ds == 0)
            {
                _result = "0";
            }
            else
            {
                _result = _namewithoutspaces;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _result;
    }

    public static string GetClass_Sequence(string ClassIDName)
    {
        string _namewithoutspaces = ClassIDName.Replace(" ", "");

        string _selectSerialSequence = string.Format($" DECLARE @{_namewithoutspaces} BIGINT " +
                                                     $" SET @{_namewithoutspaces} = NEXT VALUE FOR {_namewithoutspaces}; " +
                                                     $" Select @{_namewithoutspaces} As ClassID;");
        try
        {
            DataSet _ds = SQLCommonHelper.SQLQueryCommandNoParameterList(_selectSerialSequence, "Error on getting data from server");
            if (_ds.Tables.Count > 0)
            {
                return Convert.ToString(_ds.Tables[0].Rows[0]["ClassID"]);
            }
            return "0";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
    }

    public static string DeleteClass_Sequence(string ClassIDName)
    {
        string _namewithoutspaces = ClassIDName.Replace(" ", "");

        string _selectSerialSequence = string.Format($" DECLARE @{_namewithoutspaces}Sequence BIGINT " +
                                                     $" SET @{_namewithoutspaces}Sequence  = NEXT VALUE FOR {_namewithoutspaces}_Sequence; " +
                                                     $" Select @{_namewithoutspaces}Sequence As ClassID;");
        try
        {
            DataSet _ds = SQLCommonHelper.SQLQueryCommandNoParameterList(_selectSerialSequence, "Error on getting data from server");
            if (_ds.Tables.Count > 0)
            {
                return Convert.ToString(_ds.Tables[0].Rows[0]["ClassID"]);
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
