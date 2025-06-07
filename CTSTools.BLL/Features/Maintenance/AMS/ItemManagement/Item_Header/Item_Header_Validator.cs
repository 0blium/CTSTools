using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_Header_Validator
{
    public static ValidationResultDTO CreateItem_Header_Validation(Item_HeaderDTO Item_HeaderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (string.IsNullOrEmpty(Item_HeaderDTO.EnglishName))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "EnglishName Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.EnglishName)}",
                });
            }
            if (string.IsNullOrEmpty(Item_HeaderDTO.Model))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Model Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.Model)}",
                });
            }
            if (string.IsNullOrEmpty(Item_HeaderDTO.Brand))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Brand Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.Brand)}",
                });
            }
            if (Item_HeaderDTO.IsESD == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "IsESD Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Item_HeaderDTO.SupportGroupID == null || Item_HeaderDTO.SupportGroupID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Support Group is not selected",
                    Description = " Please, complete the missing information ",
                    Data = "Item_SupportGroupSupportGroup",
                });
            }

            //verify if asset type already exist in the system
            if (!string.IsNullOrEmpty(Item_HeaderDTO.Model) && !string.IsNullOrEmpty(Item_HeaderDTO.Brand))
            {
                var _itemHeaderDTO = Item_Header_Service.GetItem_HeaderList_Global(new Item_HeaderDTO { Brand = Item_HeaderDTO.Brand, Model = Item_HeaderDTO.Model }).FirstOrDefault();
                if (_itemHeaderDTO != null && _itemHeaderDTO.ID != Item_HeaderDTO.ID)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Item  with same brand and model already exist",
                        Description = " Please, complete the missing information ",
                    });
                }
            }
            if ((Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0) && (string.IsNullOrEmpty(Item_HeaderDTO.EnglishName) || string.IsNullOrEmpty(Item_HeaderDTO.Model) || string.IsNullOrEmpty(Item_HeaderDTO.Brand)))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item is not selected",
                    Description = " Please, complete the missing information",
                    Data = $"{nameof(Item_Header)}",
                });
            }
            if (Item_HeaderDTO.ID != null && Item_HeaderDTO.ID != 0)
            {
                if (Item_HeaderDTO.SupportGroupID != null && Item_HeaderDTO.SupportGroupID != 0) 
                {
                    _validation_ResultList.Clear();
                    var _item_SupportGroupDTO = new Item_SupportGroupDTO { Item_HeaderID = Item_HeaderDTO.ID, SupportGroupID = Item_HeaderDTO.SupportGroupID };
                    var _itemSupportGroupDTO = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(_item_SupportGroupDTO).FirstOrDefault();
                    if (_itemSupportGroupDTO != null)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Item is already related to the support group",
                            Description = " Please, relate to a different support group",
                        });
                    }
                }
            }

            if (Item_HeaderDTO.AddedByID == null || Item_HeaderDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateItem_Header_Validation(Item_HeaderDTO Item_HeaderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (string.IsNullOrEmpty(Item_HeaderDTO.EnglishName))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "EnglishName Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.EnglishName)}",
                });
            }
            if (string.IsNullOrEmpty(Item_HeaderDTO.Model))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Model Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.Model)}",
                });
            }
            if (string.IsNullOrEmpty(Item_HeaderDTO.Brand))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Brand Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.Brand)}",
                });
            }
            if (Item_HeaderDTO.IsESD == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "IsESD Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            //if (Item_HeaderDTO.ItemClassificationDTO.ID == null || Item_HeaderDTO.ItemClassificationDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "ItemClassification Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.ItemClassificationDTO)}",
            //    });
            //}

            if (Item_HeaderDTO.LastUpdateByID == null || Item_HeaderDTO.LastUpdateByID == 0)
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
    public static ValidationResultDTO DeleteItem_Header_Validation(Item_HeaderDTO Item_HeaderDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0)
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
                // Validate if Item Header not contain Equipment Lines
                var _itemLineList = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO
                {
                    Item_HeaderDTO = Item_HeaderDTO
                });
                if (_itemLineList != null && _itemLineList.Count > 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error!",
                        Description = "To delete the record, there must first be no individualized items that depend on this record. ",
                    });
                }
                // Validate if Item Header not contain relation with support Group
                var _item_supportGroupList = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(new Item_SupportGroupDTO
                {
                    Item_HeaderDTO = Item_HeaderDTO
                });
                if (_item_supportGroupList != null && _item_supportGroupList.Count > 0)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Error!",
                        Description = "To delete the record, there must first  not have a relationship with support groups. ",
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
}
