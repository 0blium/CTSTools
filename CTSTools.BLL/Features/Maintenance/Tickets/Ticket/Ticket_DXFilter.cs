using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Ticket;

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
            if (TicketDTO.CreatedByID != null || TicketDTO.CreatedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.CreatedBy), TicketDTO.CreatedByID));
            }
            if (TicketDTO.FacilityID != null || TicketDTO.FacilityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Facility), TicketDTO.FacilityID));
            }
            if (TicketDTO.FacilityIDArray != null && TicketDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Facility), TicketDTO.FacilityIDArray));
            }
            if (TicketDTO.DepartmentID != null || TicketDTO.DepartmentID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Department), TicketDTO.DepartmentID));
            }
            if (TicketDTO.DepartmentIDArray != null && TicketDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Department), TicketDTO.DepartmentIDArray));
            }
            if (TicketDTO.AssignedToID != null || TicketDTO.AssignedToID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.AssignedTo), TicketDTO.AssignedToID));
            }
            if (TicketDTO.Item_LineID != null || TicketDTO.Item_LineID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Item_Line), TicketDTO.Item_LineID));
            }
            if (TicketDTO.Item_LineIDArray != null && TicketDTO.Item_LineIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Item_Line), TicketDTO.Item_LineIDArray));
            }
            if (TicketDTO.StatusID != null || TicketDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Status), TicketDTO.StatusID));
            }
            if (TicketDTO.StatusIDArray != null && TicketDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Status), TicketDTO.StatusIDArray));
            }
            if (TicketDTO.PriorityID != null || TicketDTO.PriorityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Priority), TicketDTO.PriorityID));
            }
            if (TicketDTO.PriorityIDArray != null && TicketDTO.PriorityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Priority), TicketDTO.PriorityIDArray));
            }
            if (TicketDTO.CategoryID != null || TicketDTO.CategoryID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.Category), TicketDTO.CategoryID));
            }
            if (TicketDTO.CategoryIDArray != null && TicketDTO.CategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.Category), TicketDTO.CategoryIDArray));
            }
            if (TicketDTO.SupportGroupID != null || TicketDTO.SupportGroupID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.SupportGroup), TicketDTO.SupportGroupID));
            }
            if (TicketDTO.SupportGroupIDArray != null && TicketDTO.SupportGroupIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(TicketXPO.SupportGroup), TicketDTO.SupportGroupIDArray));
            }
            if (TicketDTO.ClosedByID != null || TicketDTO.ClosedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(TicketXPO.ClosedBy), TicketDTO.ClosedByID));
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
