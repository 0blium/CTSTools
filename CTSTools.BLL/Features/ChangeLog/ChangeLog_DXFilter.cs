using CTSTools.DAL.Features.ChangeLog;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.ChangeLog;

public class ChangeLog_DXFilter
{
    public static GroupOperator GetChangeLog_DXFilter(ChangeLogDTO ChangeLogDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ChangeLogDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ChangeLogXPO.Oid), ChangeLogDTO.ID));
            }
            if (ChangeLogDTO.ChangeLogIDArray != null && ChangeLogDTO.ChangeLogIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ChangeLogXPO.Oid), ChangeLogDTO.ChangeLogIDArray));
            }
            if (ChangeLogDTO.RecordID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ChangeLogXPO.RecordID), ChangeLogDTO.RecordID));
            }
            if (ChangeLogDTO.RecordID != null && ChangeLogDTO.RecordID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ChangeLogXPO.RecordID), ChangeLogDTO.RecordID));
            }
            if (ChangeLogDTO.UserDTO.ID != null || ChangeLogDTO.UserDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ChangeLogXPO.User), ChangeLogDTO.UserDTO.ID));
            }
            if (ChangeLogDTO.UserIDArray != null && ChangeLogDTO.UserIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ChangeLogXPO.User), ChangeLogDTO.UserIDArray));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
