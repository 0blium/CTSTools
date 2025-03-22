using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;

public class SparePart_Lot_DXFilter
{
    public static GroupOperator GetSparePart_Lot_DXFilter(SparePart_LotDTO SparePart_LotDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SparePart_LotDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.Oid), SparePart_LotDTO.ID));
            }
            if (SparePart_LotDTO.SparePart_LotIDArray != null && SparePart_LotDTO.SparePart_LotIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePart_LotXPO.Oid), SparePart_LotDTO.SparePart_LotIDArray));
            }
            if (SparePart_LotDTO.Quantity != null && SparePart_LotDTO.Quantity > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.Quantity), SparePart_LotDTO.Quantity));
            }
            if (SparePart_LotDTO.AvailableQty != null && SparePart_LotDTO.AvailableQty > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.AvailableQty), SparePart_LotDTO.AvailableQty));
            }
            if (SparePart_LotDTO.ProviderDTO.ID != null || SparePart_LotDTO.ProviderDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.Provider), SparePart_LotDTO.ProviderDTO.ID));
            }
            if (SparePart_LotDTO.ProviderIDArray != null && SparePart_LotDTO.ProviderIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePart_LotXPO.Provider), SparePart_LotDTO.ProviderIDArray));
            }
            if (SparePart_LotDTO.SparePartDTO.ID != null || SparePart_LotDTO.SparePartDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.SparePart), SparePart_LotDTO.SparePartDTO.ID));
            }
            if (SparePart_LotDTO.SparePartIDArray != null && SparePart_LotDTO.SparePartIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePart_LotXPO.SparePart), SparePart_LotDTO.SparePartIDArray));
            }
            if (SparePart_LotDTO.SparePartInventoryDTO.ID != null || SparePart_LotDTO.SparePartInventoryDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.SparePartInventory), SparePart_LotDTO.SparePartInventoryDTO.ID));
            }
            if (SparePart_LotDTO.SparePartInventoryIDArray != null && SparePart_LotDTO.SparePartInventoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePart_LotXPO.SparePartInventory), SparePart_LotDTO.SparePartInventoryIDArray));
            }
            if (SparePart_LotDTO.SupportGroupDTO.ID != null || SparePart_LotDTO.SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.SupportGroup), SparePart_LotDTO.SupportGroupDTO.ID));
            }
            if (SparePart_LotDTO.SupportGroupIDArray != null && SparePart_LotDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePart_LotXPO.SupportGroup), SparePart_LotDTO.SupportGroupIDArray));
            }
            if (SparePart_LotDTO.TransactionOriginDTO.ID != null || SparePart_LotDTO.TransactionOriginDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.TransactionOrigin), SparePart_LotDTO.TransactionOriginDTO.ID));
            }
            if (SparePart_LotDTO.TransactionOriginIDArray != null && SparePart_LotDTO.TransactionOriginIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePart_LotXPO.TransactionOrigin), SparePart_LotDTO.TransactionOriginIDArray));
            }
            if (SparePart_LotDTO.TransactionLine != null && SparePart_LotDTO.TransactionLine > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.TransactionLine), SparePart_LotDTO.TransactionLine));
            }
            if (SparePart_LotDTO.AddedByID != null && SparePart_LotDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.AddedBy), SparePart_LotDTO.AddedByName));
            }
            if (SparePart_LotDTO.LastUpdateByID != null && SparePart_LotDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.LastUpdateBy), SparePart_LotDTO.LastUpdateByName));
            }
            if (SparePart_LotDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePart_LotXPO.IsActive), SparePart_LotDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
