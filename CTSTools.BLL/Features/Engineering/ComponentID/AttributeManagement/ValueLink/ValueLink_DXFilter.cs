using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;


public class ValueLink_DXFilter
{
    public static GroupOperator GetValueLink_DXFilter(ValueLinkDTO ValueLinkDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ValueLinkDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.Oid), ValueLinkDTO.ID));
            }
            if (ValueLinkDTO.ValueLinkIDArray != null && ValueLinkDTO.ValueLinkIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueLinkXPO.Oid), ValueLinkDTO.ValueLinkIDArray));
            }
            if (ValueLinkDTO.ParentValueIDArray != null && ValueLinkDTO.ParentValueIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueLinkXPO.ParentValue), ValueLinkDTO.ParentValueIDArray));
            }
            if (ValueLinkDTO.ChildValueIDArray != null && ValueLinkDTO.ChildValueIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueLinkXPO.ChildValue), ValueLinkDTO.ChildValueIDArray));
            }
            if (ValueLinkDTO.ParentAttributeIDArray != null && ValueLinkDTO.ParentAttributeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueLinkXPO.ParentAttribute), ValueLinkDTO.ParentAttributeIDArray));
            }
            if (ValueLinkDTO.ChildAttributeIDArray != null && ValueLinkDTO.ChildAttributeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueLinkXPO.ChildAttribute), ValueLinkDTO.ChildAttributeIDArray));
            }
            if (ValueLinkDTO.ChildValueID != null && ValueLinkDTO.ChildValueID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.ChildValue), ValueLinkDTO.ChildValueID));
            }
            if (ValueLinkDTO.ChildAttributeID != null && ValueLinkDTO.ChildAttributeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.ChildAttribute), ValueLinkDTO.ChildAttributeID));
            }
            if (ValueLinkDTO.ParentValueID != null && ValueLinkDTO.ParentValueID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.ParentValue), ValueLinkDTO.ParentValueID));
            }
            if (ValueLinkDTO.ParentAttributeID != null && ValueLinkDTO.ParentAttributeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.ParentAttribute), ValueLinkDTO.ParentAttributeID));
            }
            //if (ValueLinkDTO.AddedByID != null && ValueLinkDTO.AddedByID > 0)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.AddedBy), ValueLinkDTO.AddedBy));
            //}
            //if (ValueLinkDTO.LastUpdateByID != null && ValueLinkDTO.LastUpdateByID > 0)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.LastUpdateBy), ValueLinkDTO.LastUpdateBy));
            //}
            if (ValueLinkDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueLinkXPO.IsActive), ValueLinkDTO.IsActive));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}