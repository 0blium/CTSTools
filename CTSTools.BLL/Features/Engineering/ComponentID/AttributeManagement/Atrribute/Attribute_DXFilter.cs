using System;
using System.Linq;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Data.Filtering;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
public class Attribute_DXFilter
{
    public static GroupOperator GetAttribute_DXFilter(AttributeDTO AttributeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (AttributeDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(AttributeXPO.Oid), AttributeDTO.ID));
            if (!string.IsNullOrEmpty(AttributeDTO.Name))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(AttributeXPO.Name), AttributeDTO.Name));
            if (AttributeDTO.AttributeIDArray != null && AttributeDTO.AttributeIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(AttributeXPO.Oid), AttributeDTO.AttributeIDArray));
            if (AttributeDTO.AddedByID != null && AttributeDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(AttributeXPO.AddedBy), AttributeDTO.AddedByID));
            if (AttributeDTO.LastUpdateByID != null && AttributeDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(AttributeXPO.LastUpdateBy), AttributeDTO.LastUpdateByID));
            if (AttributeDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(AttributeXPO.IsActive), AttributeDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
