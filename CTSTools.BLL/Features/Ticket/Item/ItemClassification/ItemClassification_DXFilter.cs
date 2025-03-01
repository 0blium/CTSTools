using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.ItemClassification;

public class ItemClassification_DXFilter
{
    public static GroupOperator GetItemClassification_DXFilter(ItemClassificationDTO ItemClassificationDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ItemClassificationDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ItemClassificationXPO.Oid), ItemClassificationDTO.ID));
            }
            if (ItemClassificationDTO.ItemClassificationIDArray != null && ItemClassificationDTO.ItemClassificationIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ItemClassificationXPO.Oid), ItemClassificationDTO.ItemClassificationIDArray));
            }
            if (ItemClassificationDTO.AddedByID != null && ItemClassificationDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ItemClassificationXPO.AddedBy), ItemClassificationDTO.AddedByID));
            }
            if (ItemClassificationDTO.LastUpdateByID != null && ItemClassificationDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ItemClassificationXPO.LastUpdateBy), ItemClassificationDTO.LastUpdateByID));
            }
            if (ItemClassificationDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ItemClassificationXPO.IsActive), ItemClassificationDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
