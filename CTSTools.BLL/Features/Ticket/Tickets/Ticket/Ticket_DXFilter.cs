using CTSTools.DAL.Features.Ticket.Ticket;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Tickets.Ticket;

public class Ticket_DXFilter
{
    public static GroupOperator GetTicket_DXFilter(TicketDTO TicketDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (TicketDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Oid), TicketDTO.ID));
            }
            if (TicketDTO.TicketIDArray != null && TicketDTO.TicketIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Oid), TicketDTO.TicketIDArray));
            }
            if (TicketDTO.TicketNumber != null && TicketDTO.TicketNumber > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.TicketNumber), TicketDTO.TicketNumber));
            }
            if (TicketDTO.CreatedByDTO.ID != null || TicketDTO.CreatedByDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.CreatedBy), TicketDTO.CreatedByDTO.ID));
            }
            if (TicketDTO.FacilityDTO.ID != null || TicketDTO.FacilityDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Facility), TicketDTO.FacilityDTO.ID));
            }
            if (TicketDTO.FacilityIDArray != null && TicketDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Facility), TicketDTO.FacilityIDArray));
            }
            if (TicketDTO.DepartmentDTO.ID != null || TicketDTO.DepartmentDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Department), TicketDTO.DepartmentDTO.ID));
            }
            if (TicketDTO.DepartmentIDArray != null && TicketDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Department), TicketDTO.DepartmentIDArray));
            }
            if (TicketDTO.AssignedToDTO.ID != null || TicketDTO.AssignedToDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.AssignedTo), TicketDTO.AssignedToDTO.ID));
            }
            if (TicketDTO.Item_LineDTO.ID != null || TicketDTO.Item_LineDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Item_Line), TicketDTO.Item_LineDTO.ID));
            }
            if (TicketDTO.Item_LineIDArray != null && TicketDTO.Item_LineIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Item_Line), TicketDTO.Item_LineIDArray));
            }
            if (TicketDTO.StatusDTO.ID != null || TicketDTO.StatusDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Status), TicketDTO.StatusDTO.ID));
            }
            if (TicketDTO.StatusIDArray != null && TicketDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Status), TicketDTO.StatusIDArray));
            }
            if (TicketDTO.PriorityDTO.ID != null || TicketDTO.PriorityDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Priority), TicketDTO.PriorityDTO.ID));
            }
            if (TicketDTO.PriorityIDArray != null && TicketDTO.PriorityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Priority), TicketDTO.PriorityIDArray));
            }
            if (TicketDTO.CategoryDTO.ID != null || TicketDTO.CategoryDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Category), TicketDTO.CategoryDTO.ID));
            }
            if (TicketDTO.CategoryIDArray != null && TicketDTO.CategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Category), TicketDTO.CategoryIDArray));
            }
            if (TicketDTO.SupportGroupDTO.ID != null || TicketDTO.SupportGroupDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.SupportGroup), TicketDTO.SupportGroupDTO.ID));
            }
            if (TicketDTO.SupportGroupIDArray != null && TicketDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.SupportGroup), TicketDTO.SupportGroupIDArray));
            }
            if (TicketDTO.ClosedByDTO.ID != null || TicketDTO.ClosedByDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.ClosedBy), TicketDTO.ClosedByDTO.ID));
            }
            if (TicketDTO.LastUpdateByID != null && TicketDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.LastUpdateBy), TicketDTO.LastUpdateByName));
            }
            if (TicketDTO.StartAddedDate != new DateTime())
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.AddedDate), TicketDTO.StartAddedDate.ToLocalTime(), BinaryOperatorType.GreaterOrEqual));
            }
            if (TicketDTO.EndAddedDate != new DateTime())
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.AddedDate), TicketDTO.EndAddedDate.ToLocalTime(), BinaryOperatorType.LessOrEqual));
            }
            if (TicketDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.IsActive), TicketDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
