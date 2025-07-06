using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;
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
                    Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.Item_HeaderDTO)}",
                });
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
                    var _item_lineDTO = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { ID = _item_LineID, GetItem_HeaderDTO = true }).FirstOrDefault();
                    if (_item_lineDTO != null)
                    {
                        if (_item_lineDTO.StationID > 0)
                        {
                            _validation_ResultList.Add(new ValidationResultDTO
                            {
                                Result = false,
                                Message = "Item is already at the station ",
                                Description = $"The item {_item_lineDTO.Item_HeaderDTO.BrandName} is already at the station {_item_lineDTO.StationName}"
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
            if (Item_LineDTO.StationID == null)
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

            //if (Item_LineDTO.DeliveredToID == null || Item_LineDTO.DeliveredToID == 0)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "Deliver To Field Empty",
            //        Description = " Please, complete the missing information ",
            //        Data = $"{nameof(Item_Line)}{nameof(Item_LineDTO.DeliveredToID)}",
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
                        OwnerID = Item_LineDTO.OwnerID,
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

    #region Excel Item_Line Validation
    public static ValidationResultDTO ExcelItem_LineRows_Validation(Item_LineDTO Item_LineDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<Item_LineDTO>(),
            BadRowLinesList = new List<Item_LineDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            var _item_LineDTO = new Item_LineDTO
            {
                ID = Item_LineDTO.ID,
                Item_HeaderID = Item_LineDTO.Item_HeaderID,
                Item_SupportGroupID = Item_LineDTO.Item_SupportGroupID,
                Item_SupportGroupDTO = new Item_SupportGroupDTO 
                {
                    SupportGroupID = Item_LineDTO.Item_SupportGroupDTO.SupportGroupID
                },
                ManufactureSerialID = Item_LineDTO.ManufactureSerialID,
                LegacyID = Item_LineDTO.LegacyID,
                OwnerName = Item_LineDTO.OwnerName,
                BasePriceUSD = Item_LineDTO.BasePriceUSD,
                StatusName = Item_LineDTO.StatusName,
                StationName = Item_LineDTO.StationName,
                Comments = Item_LineDTO.Comments,
                DeliveredToName = Item_LineDTO.DeliveredToName,
                AddedByID = Item_LineDTO.AddedByID,
                AddedDate = DateTime.Now,
                IsActive = true
            };

            // StartsWith checks if any of the properties start with the text 'Error' to identify invalid DTOs.
            if (_item_LineDTO.Item_HeaderID == null || _item_LineDTO.Item_HeaderID == 0 ||
                _item_LineDTO.Item_SupportGroupID == null || _item_LineDTO.Item_SupportGroupID == 0 ||
                _item_LineDTO.Item_SupportGroupDTO.SupportGroupID == null || _item_LineDTO.Item_SupportGroupDTO.SupportGroupID == 0 ||
                _item_LineDTO.BasePriceUSD < 0.0f)
                isSucces = false;

            // If it meets all the validations, it saves it in GoodRowLinesList else
            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_item_LineDTO);
            else // If not save it BadRowLinesList
                _excelRowDTO.BadRowLinesList.Add(_item_LineDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw ex;
        }
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    public static ValidationResultDTO ExcelItem_LineInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<Item_LineDTO>(),
            BadRowLinesList = new List<Item_LineDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            // We have to declare the type of the list, because GoodRowLinesList is a dynamic type
            // This list contains the Item_Line that passed the first validation
            var _Item_LineDTOList = (List<Item_LineDTO>)ExcelRowDTO.GoodRowLinesList;
            // We add the previous Item_Line that did not pass the first validation
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            // If it does not contain data, return _validationResultDTO with _excelRowDTO
            if (_Item_LineDTOList.Count <= 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We save an array list of the names in lowercase to eliminate names that are repeated with Distinct
            var _ownerDTO = new UserDTO { UserNameArray = _Item_LineDTOList.Select(Item_LineDTO => Item_LineDTO.OwnerName.ToLower()).Distinct().ToArray() };
            var _stationDTO = new StationDTO { StationNameArray = _Item_LineDTOList.Select(Item_LineDTO => Item_LineDTO.StationName.ToLower()).Distinct().ToArray() };
            var _statusDTO = new StatusDTO { StatusNameArray = _Item_LineDTOList.Select(Item_LineDTO => Item_LineDTO.StatusName.ToLower()).Distinct().ToArray() };
            var _deliveredToDTO = new UserDTO { UserNameArray = _Item_LineDTOList.Select(Item_LineDTO => Item_LineDTO.DeliveredToName.ToLower()).Distinct().ToArray() };

            // We send the DTOs to the gets so that it brings the data from the db if it exists
            var _ownerList = User_Service.GetUserList_Global(_ownerDTO);
            var _stationList = Station_Service.GetStationList_Global(_stationDTO);
            var _statusList = Status_Service.GetStatusList_Global(_statusDTO);
            var _deliveredToList = User_Service.GetUserList_Global(_deliveredToDTO);

            // We create the dictionary (key, value), where the key will be the name in lowercase and the value is the ID
            var _ownerDict = _ownerList.ToDictionary(UserDTO => UserDTO.Name.ToLower(), UserDTO => (int?)UserDTO.ID);
            var _stationDict = _stationList.ToDictionary(StationDTO => StationDTO.Name.ToLower(), StationDTO => (int?)StationDTO.ID);
            var _statusDict = _statusList.ToDictionary(StatusDTO => StatusDTO.Name.ToLower(), StatusDTO => (int?)StatusDTO.ID);
            var _deliveredToDict = _deliveredToList.ToDictionary(UserDTO => UserDTO.Name.ToLower(), UserDTO => (int?)UserDTO.ID);

            foreach (var Item_LineDTO in _Item_LineDTOList)
            {
                bool isSuccess = true;

                // we use TryGetValue to try to get the Item_Line associated with the key from the dictionary,
                // If the Item_Line of DeliveredToName is found, it is assigned with the corresponding Item_Line (ID) from the dictionary.
                // 'out' keyword indicates that DeliveredToName is an output parameter, if the name is not found, save the error message.
                if (_ownerDict.TryGetValue(Item_LineDTO.OwnerName.ToLower(), out int? OwnerID)) Item_LineDTO.OwnerID = OwnerID;
                if (_stationDict.TryGetValue(Item_LineDTO.StationName.ToLower(), out int? StationID)) Item_LineDTO.StationID = StationID;
                if (_statusDict.TryGetValue(Item_LineDTO.StatusName.ToLower(), out int? StatusID)) Item_LineDTO.StatusID = StatusID;
                if (_deliveredToDict.TryGetValue(Item_LineDTO.DeliveredToName.ToLower(), out int? DeliveredToID)) Item_LineDTO.DeliveredToID = DeliveredToID;

                if (isSuccess)
                {
                    Item_LineDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(Item_LineDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(Item_LineDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = true;
            _validationResultDTO.Message = "Success";
            _validationResultDTO.Description = "The file was read successfully";
            throw;
        }
        // We have to declare the type of the list, because BadRowLinesList is a dynamic type
        // Before sending the list, we have to sort it by ID
        var _badRowLinesList = (List<Item_LineDTO>)_excelRowDTO.BadRowLinesList;
        _excelRowDTO.BadRowLinesList = _badRowLinesList.OrderBy(Item_LineDTO => Item_LineDTO.ID).ToList();
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion

}
