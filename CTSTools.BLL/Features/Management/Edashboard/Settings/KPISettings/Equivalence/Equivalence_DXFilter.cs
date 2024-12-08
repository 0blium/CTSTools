using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;

public class Equivalence_DXFilter
{
    public static GroupOperator GetEquivalence_DXFilter(EquivalenceDTO EquivalenceDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (EquivalenceDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(EquivalenceXPO.Oid), EquivalenceDTO.ID));
            }
            if (EquivalenceDTO.EquivalenceIDArray != null && EquivalenceDTO.EquivalenceIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(EquivalenceXPO.Oid), EquivalenceDTO.EquivalenceIDArray));
            }
            if (EquivalenceDTO.AddedByID != null && EquivalenceDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(EquivalenceXPO.AddedBy), EquivalenceDTO.AddedByID));
            }
            if (EquivalenceDTO.LastUpdateByID != null && EquivalenceDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(EquivalenceXPO.LastUpdateBy), EquivalenceDTO.LastUpdateByID));
            }
            if (EquivalenceDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(EquivalenceXPO.IsActive), EquivalenceDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
