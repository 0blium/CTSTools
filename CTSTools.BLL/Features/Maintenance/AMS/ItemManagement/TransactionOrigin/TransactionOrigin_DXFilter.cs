using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.TransactionOrigin;

public class TransactionOrigin_DXFilter
{
    public static GroupOperator GetTransactionOrigin_DXFilter(TransactionOriginDTO TransactionOriginDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (TransactionOriginDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TransactionOriginXPO.Oid), TransactionOriginDTO.ID));
            }
            if (TransactionOriginDTO.TransactionOriginIDArray != null && TransactionOriginDTO.TransactionOriginIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TransactionOriginXPO.Oid), TransactionOriginDTO.TransactionOriginIDArray));
            }
            if (TransactionOriginDTO.AddedByID != null && TransactionOriginDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TransactionOriginXPO.AddedBy), TransactionOriginDTO.AddedByID));
            }
            if (TransactionOriginDTO.LastUpdateByID != null && TransactionOriginDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TransactionOriginXPO.LastUpdateBy), TransactionOriginDTO.LastUpdateByID));
            }
            if (TransactionOriginDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TransactionOriginXPO.IsActive), TransactionOriginDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
