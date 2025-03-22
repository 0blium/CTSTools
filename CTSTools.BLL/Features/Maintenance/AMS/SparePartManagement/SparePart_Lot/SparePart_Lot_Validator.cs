using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;

public class SparePart_Lot_Validator
{
    public static ValidationResultDTO CreateSparePart_Lot_Validation(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(SparePart_LotDTO.PartNumber))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "PartNumber Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.PartNumber)}",
                });
            }
            if (SparePart_LotDTO.ProviderDTO.ID == null || SparePart_LotDTO.ProviderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Provider Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.ProviderDTO)}",
                });
            }
            if (SparePart_LotDTO.SparePartDTO.ID == null || SparePart_LotDTO.SparePartDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SparePart Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.SparePartDTO)}", 
                });
            }
            if (SparePart_LotDTO.SparePartInventoryDTO.ID == null || SparePart_LotDTO.SparePartInventoryDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SparePartInventory Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.SparePartInventoryDTO)}", 
                });
            }
            if (SparePart_LotDTO.SupportGroupDTO.ID == null || SparePart_LotDTO.SupportGroupDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.SupportGroupDTO)}", 
                });
            }
            if (SparePart_LotDTO.TransactionOriginDTO.ID == null || SparePart_LotDTO.TransactionOriginDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "TransactionOrigin Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.TransactionOriginDTO)}",
                });
            }
            if (string.IsNullOrEmpty(SparePart_LotDTO.TransactionNumber))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "TransactionNumber Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.TransactionNumber)}",
                });
            }
            //if (string.IsNullOrEmpty(SparePart_LotDTO.Serial) )
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false, 
            //        Message = "Serial Field Empty", 
            //        Description = " Please, complete the missing information ", 
            //        Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.Serial)}", 
            //    });
            //}
            if (SparePart_LotDTO.Cost == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Cost Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.Cost)}",
                });
            }
            if (SparePart_LotDTO.UnitCost == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Unit Cost Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.UnitCost)}",
                });
            }

            if (SparePart_LotDTO.AddedByID == null || SparePart_LotDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
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
    public static ValidationResultDTO UpdateSparePart_Lot_Validation(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePart_LotDTO.ID == null || SparePart_LotDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(SparePart_LotDTO.PartNumber))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "PartNumber Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.PartNumber)}",
                });
            }
            if (SparePart_LotDTO.ProviderDTO.ID == null || SparePart_LotDTO.ProviderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Provider Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.ProviderDTO)}",
                });
            }
            if (SparePart_LotDTO.SparePartDTO.ID == null || SparePart_LotDTO.SparePartDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SparePart Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.SparePartDTO)}",
                });
            }
            if (SparePart_LotDTO.SparePartInventoryDTO.ID == null || SparePart_LotDTO.SparePartInventoryDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SparePartInventory Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.SparePartInventoryDTO)}", 
                });
            }
            if (SparePart_LotDTO.SupportGroupDTO.ID == null || SparePart_LotDTO.SupportGroupDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.SupportGroupDTO)}", 
                });
            }
            if (SparePart_LotDTO.TransactionOriginDTO.ID == null || SparePart_LotDTO.TransactionOriginDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "TransactionOrigin Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.TransactionOriginDTO)}",
                });
            }
            if (string.IsNullOrEmpty(SparePart_LotDTO.TransactionNumber))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "TransactionNumber Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.TransactionNumber)}",
                });
            }
            if (string.IsNullOrEmpty(SparePart_LotDTO.Serial))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Serial Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(SparePart_Lot)}{nameof(SparePart_LotDTO.Serial)}", 
                });
            }

            if (SparePart_LotDTO.LastUpdateByID == null || SparePart_LotDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteSparePart_Lot_Validation(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePart_LotDTO.ID == null || SparePart_LotDTO.ID == 0)
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
