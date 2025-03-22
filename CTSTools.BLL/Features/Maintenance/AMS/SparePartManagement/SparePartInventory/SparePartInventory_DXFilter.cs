using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;

public class SparePartInventory_DXFilter
{
    public static GroupOperator GetSparePartInventory_DXFilter(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SparePartInventoryDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.Oid), SparePartInventoryDTO.ID));
            }
            if (SparePartInventoryDTO.SparePartInventoryIDArray != null && SparePartInventoryDTO.SparePartInventoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartInventoryXPO.Oid), SparePartInventoryDTO.SparePartInventoryIDArray));
            }
            if (SparePartInventoryDTO.MaxQty != null && SparePartInventoryDTO.MaxQty > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.MaxQty), SparePartInventoryDTO.MaxQty));
            }
            if (SparePartInventoryDTO.MinQty != null && SparePartInventoryDTO.MinQty > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.MinQty), SparePartInventoryDTO.MinQty));
            }
            if (SparePartInventoryDTO.SparePartDTO.ID != null || SparePartInventoryDTO.SparePartDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.SparePart), SparePartInventoryDTO.SparePartDTO.ID));
            }
            if (SparePartInventoryDTO.SparePartIDArray != null && SparePartInventoryDTO.SparePartIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartInventoryXPO.SparePart), SparePartInventoryDTO.SparePartIDArray));
            }
            if (SparePartInventoryDTO.SupportGroupDTO.ID != null || SparePartInventoryDTO.SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.SupportGroup), SparePartInventoryDTO.SupportGroupDTO.ID));
            }
            if (SparePartInventoryDTO.SupportGroupIDArray != null && SparePartInventoryDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartInventoryXPO.SupportGroup), SparePartInventoryDTO.SupportGroupIDArray));
            }
            if (SparePartInventoryDTO.AvailableQty != null && SparePartInventoryDTO.AvailableQty > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.AvailableQty), SparePartInventoryDTO.AvailableQty));
            }
            if (SparePartInventoryDTO.AddedByID != null && SparePartInventoryDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.AddedBy), SparePartInventoryDTO.AddedByName));
            }
            if (SparePartInventoryDTO.LastUpdateByID != null && SparePartInventoryDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.LastUpdateBy), SparePartInventoryDTO.LastUpdateByName));
            }
            if (SparePartInventoryDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartInventoryXPO.IsActive), SparePartInventoryDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
