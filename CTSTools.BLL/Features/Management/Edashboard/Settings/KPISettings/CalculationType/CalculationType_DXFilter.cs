using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.CalculationType;

public class CalculationType_DXFilter
{
    public static GroupOperator GetCalculationType_DXFilter(CalculationTypeDTO CalculationTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (CalculationTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CalculationTypeXPO.Oid), CalculationTypeDTO.ID));
            }
            if (CalculationTypeDTO.CalculationTypeIDArray != null && CalculationTypeDTO.CalculationTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(CalculationTypeXPO.Oid), CalculationTypeDTO.CalculationTypeIDArray));
            }
            if (CalculationTypeDTO.AddedByID != null && CalculationTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CalculationTypeXPO.AddedBy), CalculationTypeDTO.AddedByID));
            }
            if (CalculationTypeDTO.LastUpdateByID != null && CalculationTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CalculationTypeXPO.LastUpdateBy), CalculationTypeDTO.LastUpdateByID));
            }
            if (CalculationTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CalculationTypeXPO.IsActive), CalculationTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
