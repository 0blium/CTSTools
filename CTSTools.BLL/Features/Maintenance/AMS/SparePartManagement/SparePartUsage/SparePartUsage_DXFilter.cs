using CTSTools.DAL.Features.Maintenance.AMS.SparePart;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartUsage;

public class SparePartUsage_DXFilter
{
    public static GroupOperator GetSparePartUsage_DXFilter(SparePartUsageDTO SparePartUsageDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SparePartUsageDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.Oid), SparePartUsageDTO.ID));
            }
            if (SparePartUsageDTO.SparePartUsageIDArray != null && SparePartUsageDTO.SparePartUsageIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartUsageXPO.Oid), SparePartUsageDTO.SparePartUsageIDArray));
            }
            if (SparePartUsageDTO.TicketID != null || SparePartUsageDTO.TicketID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.Ticket), SparePartUsageDTO.TicketID));
            }
            if (SparePartUsageDTO.TicketIDArray != null && SparePartUsageDTO.TicketIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartUsageXPO.Ticket), SparePartUsageDTO.TicketIDArray));
            }
            if (SparePartUsageDTO.SparePartInventoryID != null || SparePartUsageDTO.SparePartInventoryID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.SparePartInventory), SparePartUsageDTO.SparePartInventoryID));
            }
            if (SparePartUsageDTO.SparePartInventoryIDArray != null && SparePartUsageDTO.SparePartInventoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartUsageXPO.SparePartInventory), SparePartUsageDTO.SparePartInventoryIDArray));
            }
            if (SparePartUsageDTO.Item_LineID != null || SparePartUsageDTO.Item_LineID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.Item_Line), SparePartUsageDTO.Item_LineID));
            }
            if (SparePartUsageDTO.Item_LineIDArray != null && SparePartUsageDTO.Item_LineIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartUsageXPO.Item_Line), SparePartUsageDTO.Item_LineIDArray));
            }
            if (SparePartUsageDTO.Quantity != null && SparePartUsageDTO.Quantity > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.Quantity), SparePartUsageDTO.Quantity));
            }
            if (SparePartUsageDTO.SparePart_LotID != null || SparePartUsageDTO.SparePart_LotID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.SparePart_Lot), SparePartUsageDTO.SparePart_LotID));
            }
            if (SparePartUsageDTO.SparePart_LotIDArray != null && SparePartUsageDTO.SparePart_LotIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartUsageXPO.SparePart_Lot), SparePartUsageDTO.SparePart_LotIDArray));
            }
            if (SparePartUsageDTO.SparePartIDArray != null && SparePartUsageDTO.SparePartIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator($"{nameof(SparePartUsageXPO.SparePartInventory)}.{nameof(SparePartUsageXPO.SparePartInventory.SparePart)}", SparePartUsageDTO.SparePartIDArray));
            }
            if (SparePartUsageDTO.SupportGroupIDArray != null && SparePartUsageDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator($"{nameof(SparePartUsageXPO.SparePartInventory)}.{nameof(SparePartUsageXPO.SparePartInventory.SupportGroup)}", SparePartUsageDTO.SupportGroupIDArray));
            }
            if (SparePartUsageDTO.AddedByID != null && SparePartUsageDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.AddedBy), SparePartUsageDTO.AddedByName));
            }
            if (SparePartUsageDTO.LastUpdateByID != null && SparePartUsageDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.LastUpdateBy), SparePartUsageDTO.LastUpdateByName));
            }
            if (SparePartUsageDTO.StartAddedDate != new DateTime())
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.AddedDate), SparePartUsageDTO.StartAddedDate.ToLocalTime(), BinaryOperatorType.GreaterOrEqual));
            }
            if (SparePartUsageDTO.EndAddedDate != new DateTime())
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.AddedDate), SparePartUsageDTO.EndAddedDate.ToLocalTime(), BinaryOperatorType.LessOrEqual));
            }
            if (SparePartUsageDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartUsageXPO.IsActive), SparePartUsageDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
