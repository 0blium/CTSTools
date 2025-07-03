using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Ticket;

public class Ticket_Repository
{
    public static List<TicketDTO> GetTicketList(TicketDTO TicketDTO, PagedResultDTO<TicketDTO> PagedResultDTO = null)
    {
        var _ticketList = new List<TicketDTO>();
        try
        {
            // Ticket Filters
            var _groupOperator = Ticket_DXFilter.GetTicket_DXFilter(TicketDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _ticketCollection = new XPCollection<TicketXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(TicketXPO.Oid), SortingDirection.Ascending))
                };

                if (_ticketCollection.AsQueryable().Count() > 0)
                {
                    _ticketList = _ticketCollection.Select(TicketXPO => TicketMap.XPOToDTO(TicketXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ticketList;
    }
    public static int GetTicketCount(TicketDTO TicketDTO, PagedResultDTO<TicketDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Ticket_DXFilter.GetTicket_DXFilter(TicketDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<TicketXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateTicket(TicketDTO TicketDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _ticketXPO = TicketMap.DTOtoXPO(TicketDTO, _unit);
                _unit.Save(_ticketXPO);
                _unit.CommitChanges();
                TicketDTO.ID = _ticketXPO.Oid;
                //TicketDTO.CreatedByDTO.Email = _ticketXPO.CreatedBy?.Email;
                TicketDTO.StatusDTO.Name = _ticketXPO.Status?.Name;
                TicketDTO.StatusName = _ticketXPO.Status?.Name;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateTicket(TicketDTO TicketDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _ticketXPO = TicketMap.DTOtoXPO(TicketDTO, _unit);
                _unit.Save(_ticketXPO);
                _unit.CommitChanges();
                TicketDTO.CreatedByDTO.Email = _ticketXPO.CreatedBy?.Email;
                TicketDTO.StatusDTO.Name = _ticketXPO.Status?.Name;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteTicket(TicketDTO TicketDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _ticketXPO = TicketMap.DTOtoXPO(TicketDTO, _unit);
                _unit.Delete(_ticketXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
}
