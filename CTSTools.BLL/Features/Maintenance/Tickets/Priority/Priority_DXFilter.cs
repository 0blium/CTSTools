using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.Tickets.Priority;

public class Priority_DXFilter
{
    public static GroupOperator GetPriority_DXFilter(PriorityDTO PriorityDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (PriorityDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PriorityXPO.Oid), PriorityDTO.ID));
            }
            if (PriorityDTO.PriorityIDArray != null && PriorityDTO.PriorityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(PriorityXPO.Oid), PriorityDTO.PriorityIDArray));
            }
            if (PriorityDTO.SupportGroupID != null || PriorityDTO.SupportGroupID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PriorityXPO.SupportGroup), PriorityDTO.SupportGroupID));
            }
            if (PriorityDTO.SupportGroupIDArray != null && PriorityDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(PriorityXPO.SupportGroup), PriorityDTO.SupportGroupIDArray));
            }
            if (PriorityDTO.AddedByID != null && PriorityDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PriorityXPO.AddedBy), PriorityDTO.AddedByName));
            }
            if (PriorityDTO.LastUpdateByID != null && PriorityDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PriorityXPO.LastUpdateBy), PriorityDTO.LastUpdateByName));
            }
            if (PriorityDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PriorityXPO.IsActive), PriorityDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
