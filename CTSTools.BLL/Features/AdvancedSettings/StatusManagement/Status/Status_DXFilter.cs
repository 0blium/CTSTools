using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;

public class Status_DXFilter
{
    public static GroupOperator GetStatusFilters(StatusDTO StatusDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (StatusDTO.ID > 0 || StatusDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusXPO.Oid), StatusDTO.ID));
            }
            if (StatusDTO.StatusIDArray != null && StatusDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StatusXPO.Oid), StatusDTO.StatusIDArray));
            }
            if (StatusDTO.StatusNameArray != null && StatusDTO.StatusNameArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StatusXPO.Name), StatusDTO.StatusNameArray));
            }
            if (StatusDTO.AddedByID != null || StatusDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusXPO.AddedBy), StatusDTO.AddedByID));
            }
            if (StatusDTO.LastUpdateByID != null || StatusDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusXPO.LastUpdateBy), StatusDTO.LastUpdateByID));
            }
            if (StatusDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusXPO.IsActive), StatusDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}

