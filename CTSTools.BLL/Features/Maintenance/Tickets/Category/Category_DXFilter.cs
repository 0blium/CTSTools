using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.Tickets.Category;

public class Category_DXFilter
{
    public static GroupOperator GetCategory_DXFilter(CategoryDTO CategoryDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (CategoryDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.Oid), CategoryDTO.ID));
            }
            if (CategoryDTO.CategoryIDArray != null && CategoryDTO.CategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(CategoryXPO.Oid), CategoryDTO.CategoryIDArray));
            }
            if (CategoryDTO.HasParent != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.HasParent), CategoryDTO.HasParent));
            }
            if (CategoryDTO.ParentID != null && CategoryDTO.ParentID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.Parent), CategoryDTO.ParentID));
            }
            if (CategoryDTO.SupportGroupID != null || CategoryDTO.SupportGroupID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.SupportGroup), CategoryDTO.SupportGroupID));
            }
            if (CategoryDTO.SupportGroupIDArray != null && CategoryDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(CategoryXPO.SupportGroup), CategoryDTO.SupportGroupIDArray));
            }
            if (CategoryDTO.AddedByID != null && CategoryDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.AddedBy), CategoryDTO.AddedByName));
            }
            if (CategoryDTO.LastUpdateByID != null && CategoryDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.LastUpdateBy), CategoryDTO.LastUpdateByName));
            }
            if (CategoryDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CategoryXPO.IsActive), CategoryDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
