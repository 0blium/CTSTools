using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.StatusType;

public class StatusType_DXFilter
{
    public static GroupOperator GetStatusTypeFilters(StatusTypeDTO StatusTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (StatusTypeDTO.ID > 0 || StatusTypeDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusTypeXPO.Oid), StatusTypeDTO.ID));
            }
            if (StatusTypeDTO.StatusTypeIDArray != null && StatusTypeDTO.StatusTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StatusTypeXPO.Oid), StatusTypeDTO.StatusTypeIDArray));
            }
            if (StatusTypeDTO.AddedByID != null || StatusTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusTypeXPO.AddedBy), StatusTypeDTO.AddedByID));
            }
            if (StatusTypeDTO.LastUpdateByID != null || StatusTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusTypeXPO.LastUpdateBy), StatusTypeDTO.LastUpdateByID));
            }
            if (StatusTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StatusTypeXPO.IsActive), StatusTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}

