using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;

public class Item_SupportGroup_Validator
{
    public static ValidationResultDTO CreateItem_SupportGroup_Validation(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_SupportGroupDTO.Item_HeaderDTO.ID == null || Item_SupportGroupDTO.Item_HeaderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Selected Item Empty",
                    Description = " Please, select an item in step 1 ",
                    //Data = $"{nameof(Item_SupportGroup)}{nameof(Item_SupportGroupDTO.Item_HeaderDTO)}",
                });
            }
            if (Item_SupportGroupDTO.SupportGroupDTO.ID == null || Item_SupportGroupDTO.SupportGroupDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Support Group Field Empty",
                    Description = " Please, complete the missing information ",
                    //Data = $"{nameof(Item_SupportGroup)}{nameof(Item_SupportGroupDTO.SupportGroupDTO)}",
                });
            }

            if (Item_SupportGroupDTO.AddedByID == null || Item_SupportGroupDTO.AddedByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "AddedByID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            //validate if relation already exist
            if (!(Item_SupportGroupDTO.Item_HeaderDTO.ID == null || Item_SupportGroupDTO.Item_HeaderDTO.ID == 0) &&
                !(Item_SupportGroupDTO.SupportGroupDTO.ID == null || Item_SupportGroupDTO.SupportGroupDTO.ID == 0))
            {
                var _item_supportGroupDTO = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(
                    new Item_SupportGroupDTO
                    {
                        Item_HeaderDTO = Item_SupportGroupDTO.Item_HeaderDTO,
                        SupportGroupDTO = Item_SupportGroupDTO.SupportGroupDTO
                    }).FirstOrDefault();
                if (_item_supportGroupDTO != null)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Item already exist on the support group",
                        Description = " Please, verify the information ",
                    });
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
    public static ValidationResultDTO UpdateItem_SupportGroup_Validation(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_SupportGroupDTO.ID == null || Item_SupportGroupDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (Item_SupportGroupDTO.Item_HeaderDTO.ID == null || Item_SupportGroupDTO.Item_HeaderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Header Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_SupportGroup)}{nameof(Item_SupportGroupDTO.Item_HeaderDTO)}",
                });
            }

            if (Item_SupportGroupDTO.LastUpdateByID == null || Item_SupportGroupDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteItem_SupportGroup_Validation(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_SupportGroupDTO.ID == null || Item_SupportGroupDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            else
            {
                //Validate if item lines dont contain relation with Item Support Group ID
                var _item_linesDTOList = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { Item_SupportGroupDTO = Item_SupportGroupDTO });
                if (_item_linesDTOList?.Count > 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error!",
                        Description = "To delete the record, there must first be no individualized items that depend on this record. ",
                    });
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
    #region Reassing Support Group To Item
    public static ValidationResultDTO ReassignSupportGroupToItemValidation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            //Validation Relation Item Header & SupportGroup exist 
            if (Item_LineDTO.Item_HeaderDTO?.ID != null && Item_LineDTO.Item_SupportGroupDTO?.SupportGroupDTO?.ID != null)
            {
                var _item_SupportGroupDTO = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(
                new Item_SupportGroupDTO
                {
                    Item_HeaderDTO = Item_LineDTO.Item_HeaderDTO,
                    SupportGroupDTO = Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO
                }).FirstOrDefault();

                if (_item_SupportGroupDTO != null)
                {
                    //confirm if this is the current relationship on the line
                    if (_item_SupportGroupDTO.ID != Item_LineDTO.Item_SupportGroupDTO.ID)
                    {
                        //If a relationship between item and support group already exists
                        //and it is different from the current relationship in the line, return the new relationship ID.
                        _validation_ResultDTO.Data = _item_SupportGroupDTO.ID;
                    }
                    else
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "The item is already at the Support Group.",
                            Description = "Please choose a different SupportGroup."
                        });
                    }
                }
            }
            else
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Select a Support Group.",
                    Description = "Please choose a SupportGroup."
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
    #endregion
}
