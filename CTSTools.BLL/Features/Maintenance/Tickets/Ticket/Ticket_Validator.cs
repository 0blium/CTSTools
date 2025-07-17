using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartUsage;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using static CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status.Status_Enum;

namespace CTSTools.BLL.Features.Maintenance.Tickets.Ticket;

public class Ticket_Validator
{
    public static ValidationResultDTO CreateTicket_Validation(TicketDTO TicketDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(TicketDTO.Title))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Title Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.Title)}",
                });
            }
            if (TicketDTO.CreatedByID == null || TicketDTO.CreatedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "CreatedBy Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (TicketDTO.FacilityID == null || TicketDTO.FacilityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.FacilityID)}",
                });
            }
            if (TicketDTO.DepartmentID == null || TicketDTO.DepartmentID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.DepartmentID)}",
                });
            }
            if (string.IsNullOrEmpty(TicketDTO.Description))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Description Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.Description)}",
                });
            }
            if (TicketDTO.StatusID == null || TicketDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.StatusID)}",
                });
            }
            if (TicketDTO.PriorityID == null || TicketDTO.PriorityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Priority Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.PriorityID)}",
                });
            }
            if (TicketDTO.CategoryID == null || TicketDTO.CategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Category Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.CategoryID)}",
                });
            }
            if (TicketDTO.SupportGroupID == null || TicketDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.SupportGroupID)}",
                });
            }
            

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO UpdateTicket_Validation(TicketDTO TicketDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (TicketDTO.ID == null || TicketDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(TicketDTO.Title))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Title Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.Title)}",
                });
            }
            if (TicketDTO.FacilityID == null || TicketDTO.FacilityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Facility Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.FacilityID)}",
                });
            }
            if (TicketDTO.DepartmentID == null || TicketDTO.DepartmentID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Department Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.DepartmentID)}",
                });
            }
            if (TicketDTO.AssignedToID == null || TicketDTO.AssignedToID == 0)
            {

            }
            if (TicketDTO.StatusID == null || TicketDTO.StatusID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Status Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.StatusID)}",
                });
            }
            if (TicketDTO.PriorityID == null || TicketDTO.PriorityID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Priority Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.PriorityID)}",
                });
            }
            if (TicketDTO.CategoryID == null || TicketDTO.CategoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Category Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.CategoryID)}",
                });
            }
            if (TicketDTO.SupportGroupID == null || TicketDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.SupportGroupID)}",
                });
            }

            if (TicketDTO.LastUpdateByID == null || TicketDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "LastUpdateByID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }

            //Aditional Validations 

            //Validate if ticket not as closed
            var _previousTicket = Ticket_Service.GetTicketList_Global(new TicketDTO { ID = TicketDTO.ID }).FirstOrDefault();
            if (_previousTicket.StatusID == (int)Statuses_Enum.Closed)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "The ticket has already been closed",
                    Description = "It is not possible to modify the closed ticket ",
                });

            }

            //Validate if solution & resolution fields are empty when ticket change to closed
            if (string.IsNullOrEmpty(TicketDTO.Solution) && TicketDTO.StatusID == (int)Statuses_Enum.Closed)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Solution Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.Solution)}",
                });
            }
            if (string.IsNullOrEmpty(TicketDTO.Resolution) && TicketDTO.StatusID == (int)Statuses_Enum.Closed)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Resolution Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.Resolution)}",
                });
            }
            //Validate if assignedTo is empty when ticket status change to closed
            if (TicketDTO.StatusID == (int)Statuses_Enum.Closed && (TicketDTO.AssignedToID == null || TicketDTO.AssignedToID == 0))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AssignedTo Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Ticket)}{nameof(TicketDTO.AssignedToDTO)}",
                });
            }
            // Validate if request quantity of SparePart is greater than available quantity of SparePartSupportGroup
            // Instance TicketSparePartDTO
            var SparePartUsageDTO = new SparePartUsageDTO { TicketDTO = TicketDTO };
            SparePartUsageDTO.GetSparePartInventoryDTO = true;
            //Get SparePartUsage lines of ticket
            var _sparePartUsageList = SparePartUsage_Service.GetSparePartUsageList_Global(SparePartUsageDTO);
            if (_sparePartUsageList.Count() > 0)
            {
                foreach (var _sparePartUsageDTO in _sparePartUsageList)
                {
                    //Validate if spare part usage qty is greater than spare part inventory available quantity
                    if (_sparePartUsageDTO.Quantity > _sparePartUsageDTO.SparePartInventoryDTO.AvailableQty)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Error",
                            Description = "Spare parts with quantities greater than the inventory cannot be processed."
                        });
                    }
                }
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
    public static ValidationResultDTO DeleteTicket_Validation(TicketDTO TicketDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (TicketDTO.ID == null || TicketDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;

            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validation_ResultDTO.Result = false;
            _validation_ResultDTO.Message = "Error!";
            _validation_ResultDTO.Description = string.Format("There was an error trying to validate the fields. {0}", ex.Message);
        }
        return _validation_ResultDTO;
    }
}
