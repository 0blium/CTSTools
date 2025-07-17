using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;
using CTSTools.BLL.Features.Maintenance.Tickets.Ticket;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using static CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status.Status_Enum;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartUsage;

public class SparePartUsage_Validator
{
    public static ValidationResultDTO CreateSparePartUsage_Validation(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartUsageDTO.TicketID == null || SparePartUsageDTO.TicketID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Ticket Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            else
            {
                //Validate if Ticket is closed
                var _ticketDTO = Ticket_Service.GetTicketList_Global(
                    new TicketDTO
                    {
                        ID = SparePartUsageDTO.TicketID
                    }).FirstOrDefault();

                if (_ticketDTO.StatusID == (int)Statuses_Enum.Closed)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Ticket is closed",
                        Description = " Spare Parts cannot be added if the ticket is closed",
                    });
                }
            }

            if (SparePartUsageDTO.SparePartInventoryID == null || SparePartUsageDTO.SparePartInventoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Spare Part Inventory Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (SparePartUsageDTO.Item_LineID == null || SparePartUsageDTO.Item_LineID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item Line Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (SparePartUsageDTO.SparePart_LotID == null || SparePartUsageDTO.SparePart_LotID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Spare Part Lot Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            else
            {
                //Validate if request quantity is greather than Available QTY of SparePartLot
                var _sparePart_LotDTO = SparePart_Lot_Service.GetSparePart_LotList_Global(
                    new SparePart_LotDTO
                    {
                        ID = SparePartUsageDTO.SparePart_LotID
                    }).FirstOrDefault();

                if (_sparePart_LotDTO?.AvailableQty < SparePartUsageDTO.Quantity)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error",
                        Description = "Spare Parts cannot be added with quantities greater than your available quantity",
                    });
                }
            }
            if (SparePartUsageDTO.SparePart_LotID != null && SparePartUsageDTO.SparePart_LotID != 0)
            {
                // Validate if SparePart_Lot already exist on the ticket
                var _sparePartUsageDTO = SparePartUsage_Service.GetSparePartUsageList_Global(
                    new SparePartUsageDTO
                    {
                        SparePart_LotID = SparePartUsageDTO.SparePart_LotID,
                        TicketID = SparePartUsageDTO.TicketID,
                    }).FirstOrDefault();
                if (_sparePartUsageDTO != null)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error",
                        Description = "The same lot cannot be added more than once.",
                    });
                }
            }

            if (SparePartUsageDTO.AddedByID == null || SparePartUsageDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (SparePartUsageDTO.Quantity == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Quantity Empty",
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
    public static ValidationResultDTO UpdateSparePartUsage_Validation(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartUsageDTO.ID == null || SparePartUsageDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (SparePartUsageDTO.TicketID == null || SparePartUsageDTO.TicketID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Ticket Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePartUsage)}{nameof(SparePartUsageDTO.TicketDTO)}",
                });
            }
            if (SparePartUsageDTO.SparePartInventoryID == null || SparePartUsageDTO.SparePartInventoryID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Spare Part Inventory Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePartUsage)}{nameof(SparePartUsageDTO.SparePartInventoryDTO)}",
                });
            }
            if (SparePartUsageDTO.Item_LineID == null || SparePartUsageDTO.Item_LineID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item Line Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePartUsage)}{nameof(SparePartUsageDTO.Item_LineDTO)}",
                });
            }
            if (SparePartUsageDTO.SparePart_LotID == null || SparePartUsageDTO.SparePart_LotID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Spare Part Lot Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePartUsage)}{nameof(SparePartUsageDTO.SparePart_LotDTO)}",
                });
            }

            if (SparePartUsageDTO.LastUpdateByID == null || SparePartUsageDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "LastUpdateByID Field Empty",
                    Description = "Please, complete the missing information ",
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
    public static ValidationResultDTO DeleteSparePartUsage_Validation(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartUsageDTO.ID == null || SparePartUsageDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            //Validate if Ticket is closed
            var _ticketDTO = Ticket_Service.GetTicketList_Global(
                new TicketDTO
                {
                    ID = SparePartUsageDTO.TicketID
                }).FirstOrDefault();

            if (_ticketDTO.StatusID == (int)Statuses_Enum.Closed)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Ticket is closed",
                    Description = " Spare Parts cannot be deleted if the ticket is closed",
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
