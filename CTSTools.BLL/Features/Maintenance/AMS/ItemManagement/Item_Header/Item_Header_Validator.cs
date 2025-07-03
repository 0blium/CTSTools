using AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember;
using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Excel;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Engineering.ComponentID.SubClass;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
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
            if (Item_HeaderDTO.BrandID == null || Item_HeaderDTO.BrandID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Brand is not selected",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.BrandID)}",
                });
            }
            if (Item_HeaderDTO.ClassID == null || Item_HeaderDTO.ClassID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Class is not selected",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.ClassID)}",
                });
            }
            if (Item_HeaderDTO.SubClassID == null || Item_HeaderDTO.SubClassID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Sub Class is not selected",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.SubClassID)}",
                });
            }
            //if (Item_HeaderDTO.IsESD == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IsESD Field Empty",
            //        Description = " Please, complete the missing information ",
            //    });
            //}
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
            if (!string.IsNullOrEmpty(Item_HeaderDTO.Model) && (Item_HeaderDTO.BrandID != null || Item_HeaderDTO.BrandID != 0))
            {
                var _itemHeaderDTO = Item_Header_Service.GetItem_HeaderList_Global(new Item_HeaderDTO { BrandID = Item_HeaderDTO.BrandID, Model = Item_HeaderDTO.Model }).FirstOrDefault();
                if (_itemHeaderDTO != null && _itemHeaderDTO.ID != Item_HeaderDTO.ID)
                {
                    _validation_ResultList.Add(new ValidationResultDTO
                    {
                        Result = false,
                        Message = "Item with same brand and model already exist",
                        Description = " Please, complete the missing information ",
                    });
                }
            }
            if ((Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0) && ( string.IsNullOrEmpty(Item_HeaderDTO.Model) || (Item_HeaderDTO.BrandID == null || Item_HeaderDTO.BrandID == 0)))
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
            if (Item_HeaderDTO.BrandID == null || Item_HeaderDTO.BrandID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Brand is not selected",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.BrandID)}",
                });
            }
            if (Item_HeaderDTO.ClassID == null || Item_HeaderDTO.ClassID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Class is not selected",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.ClassID)}",
                });
            }
            if (Item_HeaderDTO.SubClassID == null || Item_HeaderDTO.SubClassID == 0)
            {
                _validation_ResultList.Add(new ValidationResultDTO
                {
                    Result = false,
                    Message = "Sub Class is not selected",
                    Description = " Please, complete the missing information ",
                    Data = $"{nameof(Item_Header)}{nameof(Item_HeaderDTO.SubClassID)}",
                });
            }
            //if (Item_HeaderDTO.IsESD == null)
            //{
            //    _validation_ResultList.Add(new ValidationResultDTO
            //    {
            //        Result = false,
            //        Message = "IsESD Field Empty",
            //        Description = " Please, complete the missing information ",
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

    #region Excel Item_Header Validation
    public static ValidationResultDTO ExcelItem_HeaderRows_Validation(Item_HeaderDTO Item_HeaderDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<Item_HeaderDTO>(),
            BadRowLinesList = new List<Item_HeaderDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };
        try
        {
            bool isSucces = true;
            var _item_HeaderDTO = new Item_HeaderDTO
            {
                ID = Item_HeaderDTO.ID,
                Model = !string.IsNullOrEmpty(Item_HeaderDTO.Model) ? Item_HeaderDTO.Model : "Error, The model is null or empty",
                BrandName = !string.IsNullOrEmpty(Item_HeaderDTO.BrandName) ? Item_HeaderDTO.BrandName : "Error, The brand is null or empty",
                SupportGroupName = !string.IsNullOrEmpty(Item_HeaderDTO.SupportGroupName) ? Item_HeaderDTO.SupportGroupName : "Error, The support group is null or empty",
                SubClassName = !string.IsNullOrEmpty(Item_HeaderDTO.SubClassName) ? Item_HeaderDTO.SubClassName : "Error, The subclass is null or empty",
                AddedByID = Item_HeaderDTO.AddedByID,
                AddedDate = DateTime.Now,
                IsActive = true
            };

            // StartsWith checks if any of the properties start with the text 'Error' to identify invalid DTOs.
            if (_item_HeaderDTO.Model.StartsWith("Error") || 
                _item_HeaderDTO.BrandName.StartsWith("Error") ||
                _item_HeaderDTO.SupportGroupName.StartsWith("Error") ||
                _item_HeaderDTO.SubClassName.StartsWith("Error"))
                isSucces = false;

            // If it meets all the validations, it saves it in GoodRowLinesList else
            if (isSucces)
                _excelRowDTO.GoodRowLinesList.Add(_item_HeaderDTO);
            else // If not save it BadRowLinesList
                _excelRowDTO.BadRowLinesList.Add(_item_HeaderDTO);
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
    public static ValidationResultDTO ExcelItem_HeaderInformation_Validation(ExcelRowDTO ExcelRowDTO)
    {
        var _excelRowDTO = new ExcelRowDTO
        {
            GoodRowLinesList = new List<Item_HeaderDTO>(),
            BadRowLinesList = new List<Item_HeaderDTO>()
        };
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The file has the correct format."
        };

        try
        {
            // We have to declare the type of the list, because GoodRowLinesList is a dynamic type
            // This list contains the Item_Header that passed the first validation
            var _item_HeaderDTOList = (List<Item_HeaderDTO>)ExcelRowDTO.GoodRowLinesList;
            // We add the previous Item_Header that did not pass the first validation
            _excelRowDTO.BadRowLinesList.AddRange(ExcelRowDTO.BadRowLinesList);
            // If it does not contain data, return _validationResultDTO with _excelRowDTO
            if (_item_HeaderDTOList.Count <= 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }

            // We save an array list of the names in lowercase to eliminate names that are repeated with Distinct
            var _brandDTO = new BrandDTO { BrandNameArray = _item_HeaderDTOList.Select(Item_HeaderDTO => Item_HeaderDTO.BrandName.ToLower()).Distinct().ToArray() };
            var _supportGroupDTO = new SupportGroupDTO { SupportGroupNameArray = _item_HeaderDTOList.Select(Item_HeaderDTO => Item_HeaderDTO.SupportGroupName.ToLower()).Distinct().ToArray() };
            var _supportGroupMemberDTO = new SupportGroupMemberDTO { SupportGroupNameArray = _item_HeaderDTOList.Select(Item_HeaderDTO => Item_HeaderDTO.SupportGroupName.ToLower()).Distinct().ToArray(), UserDTO = new UserDTO { ID = _item_HeaderDTOList.FirstOrDefault().AddedByID } };
            var _subClassDTO = new SubClassDTO { SubClassNameArray = _item_HeaderDTOList.Select(Item_HeaderDTO => Item_HeaderDTO.SubClassName.ToLower()).Distinct().ToArray() };

            // We send the DTOs to the gets so that it brings the data from the db if it exists
            var _brandList = Brand_Service.GetBrandList_Global(_brandDTO);
            var _supportGroupList = SupportGroup_Service.GetSupportGroupList_Global(_supportGroupDTO);
            var _supportGroupMemberList = SupportGroupMember_Service.GetSupportGroupMemberList_Global(_supportGroupMemberDTO);
            var _subClassList = SubClass_Service.GetSubClassList_Global(_subClassDTO);

            // We create the dictionary (key, value), where the key will be the name in lowercase and the value is the ID
            var _brandDict = _brandList.ToDictionary(BrandDTO => BrandDTO.Name.ToLower(), BrandDTO => (int?)BrandDTO.ID);
            var _supportGroupDict = _supportGroupList.ToDictionary(SupportGroupDTO => SupportGroupDTO.EnglishName.ToLower(), SupportGroupDTO => (int?)SupportGroupDTO.ID);
            var _supportGroupMemberDict = _supportGroupMemberList.ToDictionary(SupportGroupMemberDTO => SupportGroupMemberDTO.SupportGroupDTO.EnglishName.ToLower(), SupportGroupMemberDTO => (int?)SupportGroupMemberDTO.ID);
            var _subClassDict = _subClassList.ToDictionary(SubClassDTO => SubClassDTO.Name.ToLower(), SubClassDTO => SubClassDTO);

            foreach (var Item_HeaderDTO in _item_HeaderDTOList)
            {
                bool isSuccess = true;

                // we use TryGetValue to try to get the Item_Header associated with the key from the dictionary,
                // If the Item_Header of BrandName is found, it is assigned with the corresponding Item_Header (ID) from the dictionary.
                // 'out' keyword indicates that BrandName is an output parameter, if the name is not found, save the error message.
                if (_brandDict.TryGetValue(Item_HeaderDTO.BrandName.ToLower(), out int? BrandID)) Item_HeaderDTO.BrandID = BrandID;
                else { Item_HeaderDTO.BrandName = "Error: The Brand does not exist"; isSuccess = false; }

                if (_supportGroupDict.TryGetValue(Item_HeaderDTO.SupportGroupName.ToLower(), out int? SupportGroupID)) Item_HeaderDTO.SupportGroupID = SupportGroupID;
                else { Item_HeaderDTO.SupportGroupName = "Error: The Support Group does not exist"; isSuccess = false; }

                if (!_supportGroupMemberDict.ContainsKey(Item_HeaderDTO.SupportGroupName.ToLower())) 
                {
                    Item_HeaderDTO.SupportGroupName = "Error: the user has no relationship with the support group"; isSuccess = false;
                }

                if (_subClassDict.TryGetValue(Item_HeaderDTO.SubClassName.ToLower(), out SubClassDTO SubClassDTO)) 
                {
                    Item_HeaderDTO.SubClassID = SubClassDTO.ID;
                    Item_HeaderDTO.ClassID = SubClassDTO.ClassID;
                }
                else { Item_HeaderDTO.SubClassName = "Error: The Sub Class does not exist"; isSuccess = false; }

                if (!string.IsNullOrEmpty(Item_HeaderDTO.Model) && (Item_HeaderDTO.BrandID != null || Item_HeaderDTO.BrandID != 0))
                {
                    var _itemHeaderDTO = Item_Header_Service.GetItem_HeaderList_Global(new Item_HeaderDTO { BrandID = Item_HeaderDTO.BrandID, Model = Item_HeaderDTO.Model }).FirstOrDefault();
                    if (_itemHeaderDTO != null)
                    { Item_HeaderDTO.Model = "Error: Item with same brand and model already exist"; isSuccess = false; }
                }

                if (isSuccess)
                {
                    Item_HeaderDTO.ID = null;
                    _excelRowDTO.GoodRowLinesList.Add(Item_HeaderDTO);
                }
                else _excelRowDTO.BadRowLinesList.Add(Item_HeaderDTO);
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
        var _badRowLinesList = (List<Item_HeaderDTO>)_excelRowDTO.BadRowLinesList;
        _excelRowDTO.BadRowLinesList = _badRowLinesList.OrderBy(Item_HeaderDTO => Item_HeaderDTO.ID).ToList();
        _validationResultDTO.Data = _excelRowDTO;
        return _validationResultDTO;
    }
    #endregion

}
