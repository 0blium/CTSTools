using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;

public class ValueType_DXFilter
{
    public static GroupOperator GetValueType_DXFilter(ValueTypeDTO ValueTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ValueTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueTypeXPO.Oid), ValueTypeDTO.ID));
            }
            if (!string.IsNullOrEmpty(ValueTypeDTO.Name))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueTypeXPO.Name), ValueTypeDTO.Name));
            }
            if (ValueTypeDTO.ValueTypeIDArray != null && ValueTypeDTO.ValueTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueTypeXPO.Oid), ValueTypeDTO.ValueTypeIDArray));
            }
            if (ValueTypeDTO.ValueTypeNameArray != null && ValueTypeDTO.ValueTypeNameArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ValueTypeXPO.Name), ValueTypeDTO.ValueTypeNameArray));
            }
            if (ValueTypeDTO.AddedByID != null && ValueTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueTypeXPO.AddedBy), ValueTypeDTO.AddedByID));
            }
            if (ValueTypeDTO.LastUpdateByID != null && ValueTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueTypeXPO.LastUpdateBy), ValueTypeDTO.LastUpdateByName));
            }
            if (ValueTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ValueTypeXPO.IsActive), ValueTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
