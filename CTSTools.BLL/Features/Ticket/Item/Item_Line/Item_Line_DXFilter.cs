using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.Item_Line;

public class Item_Line_DXFilter
{
    public static GroupOperator GetItem_Line_DXFilter(Item_LineDTO Item_LineDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Item_LineDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Oid), Item_LineDTO.ID));
            }
            if (!string.IsNullOrEmpty(Item_LineDTO.Serial))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Serial), Item_LineDTO.Serial));
            }
            if (Item_LineDTO.Item_LineIDArray != null && Item_LineDTO.Item_LineIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.Oid), Item_LineDTO.Item_LineIDArray));
            }
            if (Item_LineDTO.Item_HeaderDTO.ID != null || Item_LineDTO.Item_HeaderDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Item_Header), Item_LineDTO.Item_HeaderDTO.ID));
            }
            if (Item_LineDTO.SupplyTypeDTO.ID != null || Item_LineDTO.SupplyTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.SupplyType), Item_LineDTO.SupplyTypeDTO.ID));
            }
            if (Item_LineDTO.Item_HeaderIDArray != null && Item_LineDTO.Item_HeaderIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.Item_Header), Item_LineDTO.Item_HeaderIDArray));
            }
            if (Item_LineDTO.Item_SupportGroupDTO.ID != null || Item_LineDTO.Item_SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Item_SupportGroup), Item_LineDTO.Item_SupportGroupDTO.ID));
            }
            if (Item_LineDTO.Item_SupportGroupIDArray != null && Item_LineDTO.Item_SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.Item_SupportGroup), Item_LineDTO.Item_SupportGroupIDArray));
            }
            if (Item_LineDTO.StationDTO.ID != null || Item_LineDTO.StationDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Station), Item_LineDTO.StationDTO.ID));
            }
            if (Item_LineDTO.StationIDArray != null && Item_LineDTO.StationIDArray.Count() > 0 && Item_LineDTO.StationIDArray[0] != null)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.Station), Item_LineDTO.StationIDArray));
            }
            if (Item_LineDTO.StatusDTO.ID != null || Item_LineDTO.StatusDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Status), Item_LineDTO.StatusDTO.ID));
            }
            if (Item_LineDTO.StatusIDArray != null && Item_LineDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.Status), Item_LineDTO.StatusIDArray));
            }
            if (Item_LineDTO.ManufactureSerialID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.ManufactureSerialID), Item_LineDTO.ManufactureSerialID));
            }
            if (Item_LineDTO.LegacyID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.LegacyID), Item_LineDTO.LegacyID));
            }
            if (Item_LineDTO.ShipmentReceiptID != null && Item_LineDTO.ShipmentReceiptID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.ShipmentReceiptID), Item_LineDTO.ShipmentReceiptID));
            }
            if (Item_LineDTO.AddedByID != null && Item_LineDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.AddedBy), Item_LineDTO.AddedByID));
            }
            if (Item_LineDTO.LastUpdateByID != null && Item_LineDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.LastUpdateBy), Item_LineDTO.LastUpdateByID));
            }
            if (Item_LineDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.IsActive), Item_LineDTO.IsActive));
            }
            if (!string.IsNullOrEmpty(Item_LineDTO.ImportInvoice))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.ImportInvoice), Item_LineDTO.ImportInvoice));
            }
            if (!string.IsNullOrEmpty(Item_LineDTO.ShipmentReceiptNumber))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.ShipmentReceiptNumber), Item_LineDTO.ShipmentReceiptNumber));
            }
            //if (Item_LineDTO.DeliveredToDTO.ID != null && Item_LineDTO.DeliveredToDTO.ID > 0)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.DeliveredTo), Item_LineDTO.DeliveredToDTO.ID));
            //}
            if (Item_LineDTO.OwnerIDArray != null && Item_LineDTO.OwnerIDArray.Count() > 0 && Item_LineDTO.OwnerIDArray[0] != null)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.Owner), Item_LineDTO.OwnerIDArray));
            }
            if (Item_LineDTO.OwnerDTO.ID != null || Item_LineDTO.OwnerDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.Owner), Item_LineDTO.OwnerDTO.ID));
            }
            if (Item_LineDTO.SupplyTypeIDArray != null && Item_LineDTO.SupplyTypeIDArray.Count() > 0 && Item_LineDTO.SupplyTypeIDArray[0] != null)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.SupplyType), Item_LineDTO.SupplyTypeIDArray));
            }
            //if (Item_LineDTO.DeliveredToIDArray != null && Item_LineDTO.DeliveredToIDArray.Count() > 0 && Item_LineDTO.DeliveredToIDArray[0] != null)
            //{
            //    _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.DeliveredTo), Item_LineDTO.DeliveredToIDArray));
            //}
            if (Item_LineDTO.StartIntroductionDate != new DateTime())
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.IntroductionDate), Item_LineDTO.StartIntroductionDate.ToLocalTime(), BinaryOperatorType.GreaterOrEqual));
            }
            if (Item_LineDTO.EndIntroductionDate != new DateTime())
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.IntroductionDate), Item_LineDTO.EndIntroductionDate.ToLocalTime(), BinaryOperatorType.LessOrEqual));
            }
            if (Item_LineDTO.TransactionOriginDTO.ID != null || Item_LineDTO.TransactionOriginDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_LineXPO.TransactionOrigin), Item_LineDTO.TransactionOriginDTO.ID));
            }
            if (Item_LineDTO.TransactionNumberArray != null && Item_LineDTO.TransactionNumberArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_LineXPO.TransactionNumber), Item_LineDTO.TransactionNumberArray));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
