using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;

public class UnitOfMeasure_DXFilter
{
    public static GroupOperator GetUnitOfMeasure_DXFilter(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (UnitOfMeasureDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UnitOfMeasureXPO.Oid), UnitOfMeasureDTO.ID));
            }
            if (UnitOfMeasureDTO.UnitOfMeasureIDArray != null && UnitOfMeasureDTO.UnitOfMeasureIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UnitOfMeasureXPO.Oid), UnitOfMeasureDTO.UnitOfMeasureIDArray));
            }
            if (UnitOfMeasureDTO.AddedByID != null && UnitOfMeasureDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UnitOfMeasureXPO.AddedBy), UnitOfMeasureDTO.AddedByID));
            }
            if (UnitOfMeasureDTO.LastUpdateByID != null && UnitOfMeasureDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UnitOfMeasureXPO.LastUpdateBy), UnitOfMeasureDTO.LastUpdateByName));
            }
            if (UnitOfMeasureDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UnitOfMeasureXPO.IsActive), UnitOfMeasureDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
