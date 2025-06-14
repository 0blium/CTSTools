using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_Header_DXFilter
{
    public static GroupOperator GetItem_Header_DXFilter(Item_HeaderDTO Item_HeaderDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Item_HeaderDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.Oid), Item_HeaderDTO.ID));
            }
            if (Item_HeaderDTO.Item_HeaderIDArray != null && Item_HeaderDTO.Item_HeaderIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Item_HeaderXPO.Oid), Item_HeaderDTO.Item_HeaderIDArray));
            }
            //if (Item_HeaderDTO.IsESD != null)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.IsESD), Item_HeaderDTO.IsESD));
            //}
            if (Item_HeaderDTO.BrandID != null || Item_HeaderDTO.BrandID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.Brand), Item_HeaderDTO.BrandID));
            }
            if (!string.IsNullOrEmpty(Item_HeaderDTO.Model))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.Model), Item_HeaderDTO.Model));
            }
            if (Item_HeaderDTO.AddedByID != null && Item_HeaderDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.AddedBy), Item_HeaderDTO.AddedByID));
            }
            if (Item_HeaderDTO.LastUpdateByID != null && Item_HeaderDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.LastUpdateBy), Item_HeaderDTO.LastUpdateByID));
            }
            if (Item_HeaderDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Item_HeaderXPO.IsActive), Item_HeaderDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
