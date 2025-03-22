using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;

public class Item_SupportGroup_DXFilter
{
    public static GroupOperator GetItem_SupportGroup_DXFilter(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Item_SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_SupportGroupXPO.Oid), Item_SupportGroupDTO.ID));
            }
            if (Item_SupportGroupDTO.Item_SupportGroupIDArray != null && Item_SupportGroupDTO.Item_SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_SupportGroupXPO.Oid), Item_SupportGroupDTO.Item_SupportGroupIDArray));
            }
            if (Item_SupportGroupDTO.Item_HeaderDTO.ID != null || Item_SupportGroupDTO.Item_HeaderDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_SupportGroupXPO.Item_Header), Item_SupportGroupDTO.Item_HeaderDTO.ID));
            }
            if (Item_SupportGroupDTO.Item_HeaderIDArray != null && Item_SupportGroupDTO.Item_HeaderIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_SupportGroupXPO.Item_Header), Item_SupportGroupDTO.Item_HeaderIDArray));
            }
            if (Item_SupportGroupDTO.SupportGroupDTO.ID != null || Item_SupportGroupDTO.SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_SupportGroupXPO.SupportGroup), Item_SupportGroupDTO.SupportGroupDTO.ID));
            }
            if (Item_SupportGroupDTO.SupportGroupIDArray != null && Item_SupportGroupDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_SupportGroupXPO.SupportGroup), Item_SupportGroupDTO.SupportGroupIDArray));
            }
            if (Item_SupportGroupDTO.AddedByID != null && Item_SupportGroupDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_SupportGroupXPO.AddedBy), Item_SupportGroupDTO.AddedByID));
            }
            if (Item_SupportGroupDTO.LastUpdateByID != null && Item_SupportGroupDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_SupportGroupXPO.LastUpdateBy), Item_SupportGroupDTO.LastUpdateByID));
            }
            if (Item_SupportGroupDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_SupportGroupXPO.IsActive), Item_SupportGroupDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
