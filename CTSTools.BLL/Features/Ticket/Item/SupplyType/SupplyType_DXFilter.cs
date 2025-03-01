using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.SupplyType;

public class SupplyType_DXFilter
{
    public static GroupOperator GetSupplyType_DXFilter(SupplyTypeDTO SupplyTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SupplyTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplyTypeXPO.Oid), SupplyTypeDTO.ID));
            }
            if (SupplyTypeDTO.SupplyTypeIDArray != null && SupplyTypeDTO.SupplyTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SupplyTypeXPO.Oid), SupplyTypeDTO.SupplyTypeIDArray));
            }
            if (SupplyTypeDTO.AddedByID != null && SupplyTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplyTypeXPO.AddedBy), SupplyTypeDTO.AddedByID));
            }
            if (SupplyTypeDTO.LastUpdateByID != null && SupplyTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplyTypeXPO.LastUpdateBy), SupplyTypeDTO.LastUpdateByID));
            }
            if (SupplyTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplyTypeXPO.IsActive), SupplyTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
