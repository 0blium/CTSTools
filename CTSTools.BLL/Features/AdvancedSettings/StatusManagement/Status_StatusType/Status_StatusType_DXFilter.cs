using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;

public class Status_StatusType_DXFilter
{
    public static GroupOperator GetStatus_StatusTypeFilters(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Status_StatusTypeDTO.ID > 0 || Status_StatusTypeDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Status_StatusTypeXPO.Oid), Status_StatusTypeDTO.ID));
            }
            if (Status_StatusTypeDTO.Status_StatusTypeIDArray != null && Status_StatusTypeDTO.Status_StatusTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Status_StatusTypeXPO.Oid), Status_StatusTypeDTO.Status_StatusTypeIDArray));
            }
            if (Status_StatusTypeDTO.StatusID != null && Status_StatusTypeDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Status_StatusTypeXPO.Status), Status_StatusTypeDTO.StatusID));
            }
            if (Status_StatusTypeDTO.StatusIDArray != null && Status_StatusTypeDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Status_StatusTypeXPO.Status), Status_StatusTypeDTO.StatusIDArray));
            }
            if (Status_StatusTypeDTO.StatusTypeID != null && Status_StatusTypeDTO.StatusTypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Status_StatusTypeXPO.StatusType), Status_StatusTypeDTO.StatusTypeID));
            }
            if (Status_StatusTypeDTO.StatusTypeIDArray != null && Status_StatusTypeDTO.StatusTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Status_StatusTypeXPO.StatusType), Status_StatusTypeDTO.StatusTypeIDArray));
            }
            if (Status_StatusTypeDTO.AddedByID != null || Status_StatusTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Status_StatusTypeXPO.AddedBy), Status_StatusTypeDTO.AddedByID));
            }
            if (Status_StatusTypeDTO.LastUpdateByID != null || Status_StatusTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Status_StatusTypeXPO.LastUpdateBy), Status_StatusTypeDTO.LastUpdateByID));
            }
            if (Status_StatusTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Status_StatusTypeXPO.IsActive), Status_StatusTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
