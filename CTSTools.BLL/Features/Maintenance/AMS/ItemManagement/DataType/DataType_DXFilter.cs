using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.DataType;

public class DataType_DXFilter
{
    public static GroupOperator GetDataType_DXFilter(DataTypeDTO DataTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DataTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DataTypeXPO.Oid), DataTypeDTO.ID));
            }
            if (DataTypeDTO.DataTypeIDArray != null && DataTypeDTO.DataTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DataTypeXPO.Oid), DataTypeDTO.DataTypeIDArray));
            }
            if (DataTypeDTO.AddedByID != null && DataTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DataTypeXPO.AddedBy), DataTypeDTO.AddedByID));
            }
            if (DataTypeDTO.LastUpdateByID != null && DataTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DataTypeXPO.LastUpdateBy), DataTypeDTO.LastUpdateByID));
            }
            if (DataTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DataTypeXPO.IsActive), DataTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
