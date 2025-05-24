using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;

public class SparePartInventory_Validator
{
    public static ValidationResultDTO CreateSparePartInventory_Validation(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartInventoryDTO.SparePartID == null || SparePartInventoryDTO.SparePartID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SparePart Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (SparePartInventoryDTO.SupportGroupID == null || SparePartInventoryDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            if (!(SparePartInventoryDTO.SparePartID == null || SparePartInventoryDTO.SparePartID == 0) && !(SparePartInventoryDTO.SupportGroupID == null || SparePartInventoryDTO.SupportGroupID == 0))
            {
                var _sparePartDTO = SparePartInventory_Service.GetSparePartInventoryList_Global(
                    new SparePartInventoryDTO
                    {
                        SparePartID = SparePartInventoryDTO.SparePartID,
                        SupportGroupID = SparePartInventoryDTO.SupportGroupID
                    }).FirstOrDefault();
                if (_sparePartDTO != null)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "The inventory already exist",
                        Description = " Please, Verify the information ",
                    });
                }
            }

            if (SparePartInventoryDTO.AddedByID == null || SparePartInventoryDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateSparePartInventory_Validation(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartInventoryDTO.ID == null || SparePartInventoryDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (SparePartInventoryDTO.SparePartID == null || SparePartInventoryDTO.SparePartID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SparePart Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePartInventory)}{nameof(SparePartInventoryDTO.SparePartDTO)}",
                });
            }
            if (SparePartInventoryDTO.SupportGroupID == null || SparePartInventoryDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "SupportGroup Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(SparePartInventory)}{nameof(SparePartInventoryDTO.SupportGroupDTO)}",
                });
            }

            if (SparePartInventoryDTO.LastUpdateByID == null || SparePartInventoryDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteSparePartInventory_Validation(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (SparePartInventoryDTO.ID == null || SparePartInventoryDTO.ID == 0)
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
