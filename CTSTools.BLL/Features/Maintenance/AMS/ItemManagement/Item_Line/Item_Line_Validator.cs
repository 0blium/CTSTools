using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_Line_Validator
{
    #region CRUD
    public static ValidationResultDTO CreateItem_Line_Validation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_LineDTO.Item_HeaderID == null || Item_LineDTO.Item_HeaderID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Header Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Item_HeaderID)}",
                });
            }
            if (Item_LineDTO.OwnerID == null || Item_LineDTO.OwnerID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Owner Field Empty",
                    Description = "Please, complete the missing information",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.OwnerDTO)}",
                });
            }
            if (Item_LineDTO.SupplyTypeID == null || Item_LineDTO.SupplyTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Supply Type Field Empty",
                    Description = "Please, complete the missing information",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.SupplyTypeID)}",
                });
            }
            if (Item_LineDTO.SupplyTypeID != null && Item_LineDTO.SupplyTypeID != 0)
            {
                var _supplyTypeValidation = Item_LineValidationBySupplyType(Item_LineDTO);
                foreach (var _validationResultDTO in _supplyTypeValidation)
                {
                    _validation_ResultList.Add(_validationResultDTO);
                }
            }
            if (Item_LineDTO.StationID == null || Item_LineDTO.StationID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Station Field Empty",
                    Description = "Please, complete the missing information",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.StationID)}",
                });
            }
            if (string.IsNullOrEmpty(Item_LineDTO.ManufactureSerialID))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ManufactureSerialID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.ManufactureSerialID)}",
                });
            }
            if (string.IsNullOrEmpty(Item_LineDTO.Serial))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Serial Field Empty",
                    Description = " Please, complete the missing information or check the box to generate the serial",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Serial)}",
                });

            }
            else
            {
                //Validate if serial already exist in the database
                var _item_lineDTO = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { Serial = Item_LineDTO.Serial }).FirstOrDefault();
                if (_item_lineDTO != null && Item_LineDTO.ID != _item_lineDTO.ID)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "The serial already exist in the system",
                        Description = "Please verify the information.",
                        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Serial)}",
                    });
                }
            }
            if (Item_LineDTO.BasePriceUSD == null || Item_LineDTO.BasePriceUSD == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Price Field Empty",
                    Description = "Please, complete the missing information",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.BasePriceUSD)}",
                });
            }
            // Check if the UserDefinedValueList has data
            if (Item_LineDTO.UserDefinedValueList != null && Item_LineDTO.UserDefinedValueList.Count > 0)
            {
                foreach (var _userDefinedValueDTO in Item_LineDTO.UserDefinedValueList)
                {
                    // Get User Defined information
                    var _userDefinedDTO = UserDefined_Service.GetUserDefinedList_Global(new UserDefinedDTO { ID = _userDefinedValueDTO.UserDefinedID }).FirstOrDefault();
                    if ((bool)_userDefinedDTO.IsMandatory)
                    {
                        // If it is mandatory, check that it is not empty
                        if (_userDefinedValueDTO.Value == string.Empty)
                        {
                            _validation_ResultList.Add(new ValidationResultDTO
                            {
                                Result = false,
                                Message = _userDefinedDTO.Name + " Empty",
                                Description = "Please, complete the missing information"
                            });
                        }
                    }
                }
            }

            if (Item_LineDTO.AddedByID == null || Item_LineDTO.AddedByID == 0)
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
    public static ValidationResultDTO UpdateItem_Line_Validation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();
            // Field Validation
            if (Item_LineDTO.ID == null || Item_LineDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            if (Item_LineDTO.Item_HeaderID == null || Item_LineDTO.Item_HeaderID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item_Header Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Item_HeaderID)}",
                });
            }
            if (Item_LineDTO.OwnerID == null || Item_LineDTO.OwnerID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Owner Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.OwnerID)}",
                });
            }
            if (Item_LineDTO.SupplyTypeID == null || Item_LineDTO.SupplyTypeID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Supply Type Field Empty",
                    Description = "Please, complete the missing information",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.SupplyTypeID)}",
                });
            }
            if (string.IsNullOrEmpty(Item_LineDTO.ManufactureSerialID))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Manufacture Serial ID Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.ManufactureSerialID)}",
                });
            }
            if (Item_LineDTO.SupplyTypeID != null && Item_LineDTO.SupplyTypeID != 0)
            {
                var _supplyTypeValidation = Item_LineValidationBySupplyType(Item_LineDTO);
                foreach (var _validationResultDTO in _supplyTypeValidation)
                {
                    _validation_ResultList.Add(_validationResultDTO);
                }
            }
            if (Item_LineDTO.BasePriceUSD == null || Item_LineDTO.BasePriceUSD == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Price Field Empty",
                    Description = "Please, complete the missing information",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.BasePriceUSD)}",
                });
            }
            if (string.IsNullOrEmpty(Item_LineDTO.Serial))
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Serial Field Empty",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Serial)}",
                });
            }
            else
            {
                //Validate if serial already exist in the database
                var _item_lineDTO = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { Serial = Item_LineDTO.Serial }).FirstOrDefault();
                if (_item_lineDTO != null && Item_LineDTO.ID != _item_lineDTO.ID)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "The serial already exist in the system",
                        Description = "Please verify the information.",
                        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Serial)}",
                    });
                }
            }

            if (Item_LineDTO.LastUpdateByID == null || Item_LineDTO.LastUpdateByID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "LastUpdateByID Field Empty",
                    Description = "Please, complete the missing information ",
                });
            }
            // Check if the UserDefinedValueList has data
            if (Item_LineDTO.UserDefinedValueList != null && Item_LineDTO.UserDefinedValueList.Count > 0)
            {
                foreach (var _userDefinedValueDTO in Item_LineDTO.UserDefinedValueList)
                {
                    // Get User Defined information
                    var _userDefinedDTO = UserDefined_Service.GetUserDefinedList_Global(new UserDefinedDTO { ID = _userDefinedValueDTO.UserDefinedID }).FirstOrDefault();
                    if ((bool)_userDefinedDTO.IsMandatory)
                    {
                        // If it is mandatory, check that it is not empty
                        if (_userDefinedValueDTO.Value == string.Empty)
                        {
                            _validation_ResultList.Add(new ValidationResultDTO
                            {
                                Result = false,
                                Message = _userDefinedDTO.Name + " Empty",
                                Description = "Please, complete the missing information"
                            });
                        }
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
    public static ValidationResultDTO DeleteItem_Line_Validation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_LineDTO.ID == null || Item_LineDTO.ID == 0)
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
    #endregion

    #region Station Line Validation
    public static ValidationResultDTO CreateItem_StationValidation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();


            if (Item_LineDTO.Item_LineIDArray != null && Item_LineDTO.Item_LineIDArray.Length > 0)
            {
                foreach (var _item_LineID in Item_LineDTO.Item_LineIDArray)
                {
                    var _item_lineDTO = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { ID = _item_LineID }).FirstOrDefault();
                    if (_item_lineDTO != null)
                    {
                        if (_item_lineDTO.StationID > 0)
                        {
                            _validation_ResultList.Add(new ValidationResultDTO
                            {
                                Result = false,
                                Message = "Item is already at the station ",
                                Description = $"The item {_item_lineDTO.Item_HeaderDTO.Name} is already at the station {_item_lineDTO.StationName}"
                            });
                        }
                    }
                }
            }
            else
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Item not selected",
                    Description = "Please. Select item"
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

    public static ValidationResultDTO UpdateItem_StationValidation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_LineDTO.ID != null && Item_LineDTO.ID > 0)
            {
                var _item_lineDTO = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
                if (Item_LineDTO != null)
                {
                    if (_item_lineDTO.StationID == Item_LineDTO.StationID)
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "The item is already at the station.",
                            Description = "Please choose a different station."
                        });
                    }
                }
            }
            if (Item_LineDTO.StationDTO.ID == null)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Station empty",
                    Description = "Please choose a different station.",
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

    #region Validation by supply type
    public static List<ValidationResultDTO> Item_LineValidationBySupplyType(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultList = new List<ValidationResultDTO>();
        try
        {
            switch (Item_LineDTO.SupplyTypeDTO.ID)
            {
                case (int?)SupplyType.SupplyType_Enum.Temporary_Import:
                case (int?)SupplyType.SupplyType_Enum.Definitive_Import:
                    //if (string.IsNullOrEmpty(Item_LineDTO.ImportInvoice))
                    //{
                    //    _validation_ResultList.Add(new ValidationResultDTO
                    //    {
                    //        Result = false,
                    //        Message = "Import Invoice Field Empty",
                    //        Description = "Please, complete the missing information",
                    //        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.ImportInvoice)}",
                    //    });
                    //}
                    //if (Item_LineDTO.ImportInvoiceLine == 0 || Item_LineDTO.ImportInvoiceLine == null)
                    //{
                    //    _validation_ResultList.Add(new ValidationResultDTO
                    //    {
                    //        Result = false,
                    //        Message = "Import Invoice Line Field Empty",
                    //        Description = "Please, complete the missing information",
                    //        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.ImportInvoiceLine)}",
                    //    });
                    //}
                    if (string.IsNullOrEmpty(Item_LineDTO.ShipmentReceiptNumber))
                    {
                        _validation_ResultList.Add(new ValidationResultDTO
                        {
                            Result = false,
                            Message = "Shipment Receipt Field Empty",
                            Description = "Please, complete the missing information",
                            Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.ShipmentReceiptNumber)}",
                        });
                    }
                    //if (string.IsNullOrEmpty(Item_LineDTO.DeclarationNumber))
                    //{
                    //    _validation_ResultList.Add(new ValidationResultDTO
                    //    {
                    //        Result = false,
                    //        Message = "Declaration number Field Empty",
                    //        Description = "Please, complete the missing information",
                    //        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.DeclarationNumber)}",
                    //    });
                    //}
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _validation_ResultList;
    }
    #endregion

    #region Delivery asset line Validation
    public static ValidationResultDTO ItemDeliveryValidation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_LineDTO.ID == null || Item_LineDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }

            //if (Item_LineDTO.DeliveredToDTO.ID == null || Item_LineDTO.DeliveredToDTO.ID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Deliver To Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.DeliveredToDTO)}",
            //    });
            //}

            // if list contains a error, update main validation result
            if (_validation_ResultList.Count > 0)
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Errors!";
                _validation_ResultDTO.Description = "There is a list of errors";
                _validation_ResultDTO.ValidationResultList = _validation_ResultList;            

            }
            else
            {
                //validate item support group relation
                _validation_ResultDTO = Item_SupportGroup.Item_SupportGroup_Validator.ReassignSupportGroupToItemValidation(Item_LineDTO);
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

    #region Reassign Owner 
    public static ValidationResultDTO ReassignOwnerToItemValidation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            var _validation_ResultList = new List<ValidationResultDTO>();

            // Field Validation
            if (Item_LineDTO.ID == null || Item_LineDTO.ID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "ID Field Empty",
                    Description = " Please, complete the missing information ",
                });
            }
            if (Item_LineDTO.OwnerID == null || Item_LineDTO.OwnerID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Owner Field Empty",
                    Description = " Please, complete the missing information "
                });
            }
            else
            {
                //validate if is the current owner
                var _item_LineDTO = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
                if (_item_LineDTO.OwnerID == Item_LineDTO.OwnerID)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "The item already belongs to you.",
                        Description = "Please choose a different Owner."
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



    public static ValidationResultDTO Item_lineOwnerValidation(Item_LineDTO Item_LineDTO)
    {
        var _validation_ResultDTO = new ValidationResultDTO
        {
            Description = "The record has been validated successfully.."
        };
        try
        {
            // Field Validation
            if (!(Item_LineDTO.ID == null || Item_LineDTO.ID == 0) && !(Item_LineDTO.OwnerID == null || Item_LineDTO.OwnerID == 0))
            {
                //validate if the item belongs to that owner
                var _item_line = Item_Line_Service.GetItem_LineList_Global(
                    new Item_LineDTO
                    {
                        ID = Item_LineDTO.ID,
                        OwnerID = Item_LineDTO.OwnerID
                    }).FirstOrDefault();
                if (_item_line == null)
                {
                    _validation_ResultDTO.Result = false;
                    _validation_ResultDTO.Message = "Error!";
                    _validation_ResultDTO.Description = "You don't have the necessary permissions to perform this action";
                }
            }
            else
            {
                _validation_ResultDTO.Result = false;
                _validation_ResultDTO.Message = "Error!";
                _validation_ResultDTO.Description = "You don't have the necessary permissions to perform this action";
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
