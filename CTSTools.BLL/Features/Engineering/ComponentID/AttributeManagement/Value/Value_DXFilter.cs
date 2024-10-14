using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;

public class Value_DXFilter
{
    public static GroupOperator GetValue_DXFilter(ValueDTO ValueDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ValueDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.Oid), ValueDTO.ID));
            if (!string.IsNullOrEmpty(ValueDTO.Name))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.Name), ValueDTO.Name));
            if (!string.IsNullOrEmpty(ValueDTO.Code))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.Code), ValueDTO.Code));
            if (ValueDTO.ValueIDArray != null && ValueDTO.ValueIDArray.Count() > 0)            
                _groupOperator.Operands.Add(new InOperator(nameof(ValueXPO.Oid), ValueDTO.ValueIDArray));            
            if (ValueDTO.AttributeID != null || ValueDTO.AttributeID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.Attribute), ValueDTO.AttributeID));            
            if (ValueDTO.AttributeIDArray != null && ValueDTO.AttributeIDArray.Count() > 0)            
                _groupOperator.Operands.Add(new InOperator(nameof(ValueXPO.Attribute), ValueDTO.AttributeIDArray));            
            if (ValueDTO.AddedByID != null && ValueDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.AddedBy), ValueDTO.AddedByID));
            if (ValueDTO.LastUpdateByID != null && ValueDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.LastUpdateBy), ValueDTO.LastUpdateByID));
            if (ValueDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.IsActive), ValueDTO.IsActive));
            if (ValueDTO.IsCounter != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueXPO.IsCounter), ValueDTO.IsCounter));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
