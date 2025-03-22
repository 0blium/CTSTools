using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.Item;
using Elmah;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_Line_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateItem_Line_Global(Item_LineDTO Item_LineDTO)
    {

        //validate fields
        var _ValidationResultDTO = Item_Line_Validator.CreateItem_Line_Validation(Item_LineDTO);
        if (_ValidationResultDTO.Result)
        {
            //save item line
            Item_LineDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Item_Line_Repository.CreateItem_Line(Item_LineDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //Save UserDefined Value
            if (Item_LineDTO.UserDefinedValueList != null && Item_LineDTO.ID > 0)
            {
                foreach (var _userDefinedValueDTO in Item_LineDTO.UserDefinedValueList)
                {
                    _userDefinedValueDTO.Item_LineDTO = Item_LineDTO;
                    _userDefinedValueDTO.AddedByID = Item_LineDTO.AddedByID;
                    _ValidationResultDTO = UserDefinedValue_Service.CreateUserDefinedValue_Global(_userDefinedValueDTO);
                }
            }
        }
        //Save Attachments
        if (_ValidationResultDTO.Result && Item_LineDTO.FileDTO?.FileList != null)
        {
            Item_LineDTO.FileDTO.ID = (int)Item_LineDTO.ID;
            Item_LineDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemLineAttachmentsDirectory;
            _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(Item_LineDTO.FileDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<Item_LineDTO>(Item_LineDTO, (int)Item_LineDTO.AddedByID, (int)Item_LineDTO.ID);
        }

        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateItem_Line_Global(Item_LineDTO Item_LineDTO)
    {

        var _ValidationResultDTO = Item_Line_Validator.UpdateItem_Line_Validation(Item_LineDTO);
        var _previousItem_lineDTO = GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            Item_LineDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Item_Line_Repository.UpdateItem_Line(Item_LineDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //Save UserDefined Value
            if (Item_LineDTO.UserDefinedValueList != null && Item_LineDTO.UserDefinedValueList.Count() > 0 && Item_LineDTO.ID > 0)
            {
                foreach (var _userDefinedValueDTO in Item_LineDTO.UserDefinedValueList)
                {
                    //Set item line ID and Added by for each useer defined value 
                    _userDefinedValueDTO.Item_LineDTO = Item_LineDTO;
                    //get userDefineValueOldDTO record

                    var _userDefinedValueOldDTO = UserDefinedValue_Service.GetUserDefinedValueList_Global(_userDefinedValueDTO).FirstOrDefault();
                    // 5. Check if exist record
                    if (_userDefinedValueOldDTO != null)
                    {
                        // 6. If exist update record
                        _userDefinedValueDTO.ID = _userDefinedValueOldDTO.ID;
                        _userDefinedValueDTO.LastUpdateByID = Item_LineDTO.LastUpdateByID;
                        _ValidationResultDTO = UserDefinedValue_Service.UpdateUserDefinedValue_Global(_userDefinedValueDTO);
                    }
                    else
                    {
                        _userDefinedValueDTO.AddedByID = Item_LineDTO.LastUpdateByID;
                        _ValidationResultDTO = UserDefinedValue_Service.CreateUserDefinedValue_Global(_userDefinedValueDTO);
                    }


                }
            }

        }
        //Save Attachments
        if (_ValidationResultDTO.Result && Item_LineDTO.FileDTO != null && Item_LineDTO.FileDTO.FileList != null)
        {
            Item_LineDTO.FileDTO.ID = (int)Item_LineDTO.ID;
            Item_LineDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemLineAttachmentsDirectory;
            _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(Item_LineDTO.FileDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<Item_LineDTO>(_previousItem_lineDTO, Item_LineDTO, (int)Item_LineDTO.LastUpdateByID, (int)Item_LineDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteItem_Line_Global(Item_LineDTO Item_LineDTO)
    {

        var _ValidationResultDTO = Item_Line_Validator.DeleteItem_Line_Validation(Item_LineDTO);
        var _previousItem_lineDTO = GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            //get userdefined values
            var _userDefinedValuesList = UserDefinedValue_Service.GetUserDefinedValueList_Global(new UserDefinedValueDTO { Item_LineDTO = Item_LineDTO });
            // Check if User defined values exist
            if (_userDefinedValuesList != null)
            {
                foreach (var _userDefinedValueDTO in _userDefinedValuesList)
                {
                    //Delete User Defined values
                    _ValidationResultDTO = UserDefinedValue_Service.DeleteUserDefinedValue_Global(_userDefinedValueDTO);
                }
            }
        }
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Item_Line_Repository.DeleteItem_Line(Item_LineDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<Item_LineDTO>(_previousItem_lineDTO, (int)Item_LineDTO.LastUpdateByID, (int)Item_LineDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<Item_LineDTO> GetItem_LineList_Global(Item_LineDTO Item_LineDTO, PagedResultDTO<Item_LineDTO> PagedResultDTO = null)
    {
        var _item_lineglobalList = new List<Item_LineDTO>();
        try
        {
            var _item_lineList = Item_Line_Repository.GetItem_LineList(Item_LineDTO, PagedResultDTO);
            // if Item_Line is empty, return list
            if (_item_lineList.Count() == 0)
            {
                _item_lineglobalList = _item_lineList;
                return _item_lineglobalList;
            }
            if (!Item_LineDTO.GetItem_HeaderDTO && !Item_LineDTO.GetItem_SupportGroupDTO && !Item_LineDTO.GetStatusDTO && !Item_LineDTO.GetFileList && !Item_LineDTO.GetStationDTO)
            {
                _item_lineglobalList = _item_lineList;
                return _item_lineglobalList;
            }
            _item_lineglobalList = GetItem_LineRelatedData(Item_LineDTO, _item_lineList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_lineglobalList;
    }
    public static List<Item_LineDTO> GetItem_LineRelatedData(Item_LineDTO Item_LineDTO, List<Item_LineDTO> Item_LineList)
    {
        var _item_lineglobalList = new List<Item_LineDTO>();
        var _item_headerDict = new Dictionary<int?, Item_HeaderDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _item_supportgroupDict = new Dictionary<int?, Item_SupportGroupDTO>();
        var _stationDict = new Dictionary<int?, StationDTO>();

        try
        {
            if (Item_LineDTO.GetItem_HeaderDTO)
            {
                Item_LineDTO.Item_HeaderDTO.Item_HeaderIDArray = Item_LineList.GroupBy(g => g.Item_HeaderDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_headerDict = Item_Header_Service.GetItem_HeaderList_Global(Item_LineDTO.Item_HeaderDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_LineDTO.GetStatusDTO)
            {
                Item_LineDTO.StatusDTO.StatusIDArray = Item_LineList.GroupBy(g => g.StatusDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(Item_LineDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_LineDTO.GetItem_SupportGroupDTO)
            {
                Item_LineDTO.Item_SupportGroupDTO.Item_SupportGroupIDArray = Item_LineList.GroupBy(g => g.Item_SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_supportgroupDict = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(Item_LineDTO.Item_SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_LineDTO.GetStationDTO)
            {
                Item_LineDTO.StationDTO.StationIDArray = Item_LineList.GroupBy(g => g.StationDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();
                _stationDict = Station_Service.GetStationList_Global(Item_LineDTO.StationDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _item_lineDTO in Item_LineList)
            {
                if (Item_LineDTO.GetItem_HeaderDTO && _item_headerDict.ContainsKey(_item_lineDTO.Item_HeaderDTO.ID))
                {
                    _item_lineDTO.Item_HeaderDTO = _item_headerDict[_item_lineDTO.Item_HeaderDTO.ID];
                }
                if (Item_LineDTO.GetStatusDTO && _statusDict.ContainsKey(_item_lineDTO.StatusDTO.ID))
                {
                    _item_lineDTO.StatusDTO = _statusDict[_item_lineDTO.StatusDTO.ID];
                }
                if (Item_LineDTO.GetStationDTO && _stationDict.ContainsKey(_item_lineDTO.StationDTO.ID))
                {
                    _item_lineDTO.StationDTO = _stationDict[_item_lineDTO.StationDTO.ID];
                }
                if (Item_LineDTO.GetItem_SupportGroupDTO && _item_supportgroupDict.ContainsKey(_item_lineDTO.Item_SupportGroupDTO.ID))
                {
                    _item_lineDTO.Item_SupportGroupDTO = _item_supportgroupDict[_item_lineDTO.Item_SupportGroupDTO.ID];
                }
                //if (Item_LineDTO.GetFileList)
                //{
                //    _item_lineDTO.FileList = Item_Line_Service.GetItem_LineFileList(_item_lineDTO);
                //}
                _item_lineglobalList.Add(_item_lineDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_lineglobalList;
    }
    public static int GetItem_LineTotalCount(PagedResultDTO<Item_LineDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Item_Line_Repository.GetItem_LineCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic


    public static List<Item_LineDTO> GetItem_LineListBySupportGroup(Item_LineDTO Item_LineDTO, PagedResultDTO<Item_LineDTO> PagedResultDTO = null)
    {
        var _item_lineglobalList = new List<Item_LineDTO>();
        try
        {
            //The SupportGroup ID is in Item_SupportGroupDTO.SupportGroupIDArray
            if (Item_LineDTO.Item_SupportGroupDTO?.SupportGroupIDArray.Length > 0)
            {
                var _item_lineDTO = new Item_LineDTO();
                _item_lineDTO.IsActive = true;

                _item_lineDTO.Item_SupportGroupIDArray = Item_SupportGroup.Item_SupportGroup_Service.GetItem_SupportGroupList_Global(Item_LineDTO.Item_SupportGroupDTO).Select(s => s.ID).ToArray();

                if (_item_lineDTO.Item_SupportGroupIDArray.Length > 0)
                {

                    var _item_lineList = Item_Line_Repository.GetItem_LineList(_item_lineDTO, PagedResultDTO);
                    // if Item_Line is empty, return list
                    if (_item_lineList.Count() == 0)
                    {
                        _item_lineglobalList = _item_lineList;
                        return _item_lineglobalList;
                    }
                    if (!Item_LineDTO.GetItem_HeaderDTO && !Item_LineDTO.GetItem_SupportGroupDTO && !Item_LineDTO.GetStatusDTO && !Item_LineDTO.GetFileList && !Item_LineDTO.GetStationDTO)
                    {
                        _item_lineglobalList = _item_lineList;
                        return _item_lineglobalList;
                    }
                    _item_lineglobalList = GetItem_LineRelatedData(Item_LineDTO, _item_lineList);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_lineglobalList;
    }


    public static ValidationResultDTO GetSerialSecuenceForItem(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO() { Result = false };
        try
        {
            var _itemSerial = string.Empty;
            while (_validationResultDTO.Result == false)
            {
                _itemSerial = string.Format("AMS{0}", AssetManagementSQL.GetItemLineSerial());
                var _item_LineDTO = GetItem_LineList_Global(new Item_LineDTO { Serial = _itemSerial }).FirstOrDefault();
                if (_item_LineDTO == null)
                {
                    _validationResultDTO.Result = true;
                }
            }
            Item_LineDTO.Serial = _itemSerial;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha ocurrido un error. {0}", ex.Message);
        }
        return _validationResultDTO;
    }

    public static List<IDictionary<string, Object>> GetItem_LineWithUserDefined(Item_LineDTO Item_LineDTO)
    {
        var _item_LineExpandoObjList = new List<IDictionary<string, Object>>();
        var _userDefinedList = new List<UserDefinedDTO>();
        try
        {
            _userDefinedList = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO
            {
                Item_SupportGroupDTO = Item_LineDTO.Item_SupportGroupDTO,
                GetUserDefinedDTO = true
            }).Select(s => s.UserDefinedDTO).ToList();


            var _item_lineList = GetItem_LineList_Global(new Item_LineDTO { Item_SupportGroupDTO = Item_LineDTO.Item_SupportGroupDTO, GetItem_SupportGroupDTO = true, IsActive = true });
            if (_item_lineList.Count() > 0)
            {
                foreach (var _item_lineDTO in _item_lineList)
                {
                    var properties = new ExpandoObject() as IDictionary<string, Object>;
                    properties.Add(nameof(_item_lineDTO.ID), _item_lineDTO.ID);
                    properties.Add(nameof(_item_lineDTO.ManufactureSerialID), _item_lineDTO.ManufactureSerialID);
                    properties.Add(nameof(_item_lineDTO.LegacyID), _item_lineDTO.LegacyID);
                    properties.Add(nameof(_item_lineDTO.Serial), _item_lineDTO.Serial);
                    properties.Add(nameof(_item_lineDTO.IsActive), _item_lineDTO.IsActive);
                    properties.Add(nameof(Item_LineXPO.Owner), _item_lineDTO.OwnerDTO.Name);
                    properties.Add(nameof(Item_LineXPO.SupplyType), _item_lineDTO.SupplyTypeDTO.Name);
                    properties.Add($"{nameof(_item_lineDTO.SupplyTypeDTO)}{nameof(_item_lineDTO.SupplyTypeDTO.ID)}", _item_lineDTO.SupplyTypeDTO.ID);
                    properties.Add(nameof(_item_lineDTO.IntroductionDate), _item_lineDTO.IntroductionDate);
                    properties.Add(nameof(_item_lineDTO.BasePriceMXN), _item_lineDTO.BasePriceMXN);
                    properties.Add(nameof(_item_lineDTO.BasePriceUSD), _item_lineDTO.BasePriceUSD);
                    properties.Add(nameof(_item_lineDTO.COO), _item_lineDTO.COO);
                    properties.Add(nameof(Item_LineXPO.Station), _item_lineDTO.StationDTO.Name);
                    properties.Add($"{nameof(_item_lineDTO.StationDTO)}{nameof(_item_lineDTO.StationDTO.ID)}", _item_lineDTO.StationDTO.ID);
                    properties.Add(nameof(_item_lineDTO.Comments), _item_lineDTO.Comments);
                    properties.Add(nameof(Item_LineXPO.TransactionOrigin), _item_lineDTO.TransactionOriginDTO.Name);
                    properties.Add(nameof(_item_lineDTO.TransactionNumber), _item_lineDTO.TransactionNumber);
                    properties.Add(nameof(_item_lineDTO.TransactionLine), _item_lineDTO.TransactionLine);
                    properties.Add($"{nameof(_item_lineDTO.OwnerDTO)}{nameof(_item_lineDTO.OwnerDTO.ID)}", _item_lineDTO.OwnerDTO.ID);
                    properties.Add($"{nameof(_item_lineDTO.Item_SupportGroupDTO.SupportGroupDTO)}{nameof(_item_lineDTO.Item_HeaderDTO.ID)}", _item_lineDTO.Item_SupportGroupDTO.SupportGroupDTO.ID);
                    properties.Add($"{nameof(_item_lineDTO.Item_SupportGroupDTO)}{nameof(_item_lineDTO.Item_SupportGroupDTO.ID)}", _item_lineDTO.Item_SupportGroupDTO.ID);
                    properties.Add(nameof(Item_LineXPO.Status), _item_lineDTO.StatusDTO.Name);
                    properties.Add($"{nameof(_item_lineDTO.StatusDTO)}{nameof(_item_lineDTO.StatusDTO.ID)}", _item_lineDTO.StatusDTO.ID);
                    properties.Add(nameof(Item_LineXPO.ImportInvoice), _item_lineDTO.ImportInvoice);
                    properties.Add(nameof(Item_LineXPO.ImportInvoiceLine), _item_lineDTO.ImportInvoiceLine);
                    properties.Add(nameof(Item_LineXPO.DeclarationNumber), _item_lineDTO.DeclarationNumber);
                    properties.Add(nameof(Item_LineXPO.ShipmentReceiptNumber), _item_lineDTO.ShipmentReceiptNumber);
                    properties.Add($"{nameof(_item_lineDTO.Item_HeaderDTO)}{nameof(_item_lineDTO.Item_HeaderDTO.ID)}", _item_lineDTO.Item_HeaderDTO.ID);
                    properties.Add(nameof(Item_LineXPO.AddedBy), _item_lineDTO.AddedByName);
                    properties.Add(nameof(_item_lineDTO.AddedDate), _item_lineDTO.AddedDate);
                    properties.Add(nameof(Item_LineXPO.LastUpdateBy), _item_lineDTO.LastUpdateByName);
                    properties.Add(nameof(_item_lineDTO.DeliveredDate), _item_lineDTO.DeliveredDate);
                    //properties.Add(nameof(Item_LineXPO.DeliveredTo), _item_lineDTO.DeliveredToDTO.Name);

                    foreach (var _userdefinedDTO in _userDefinedList)
                    {
                        var _userDefinedValueDTO = UserDefinedValue_Service.GetUserDefinedValueList_Global(new UserDefinedValueDTO
                        {
                            UserDefinedDTO = _userdefinedDTO,
                            Item_LineDTO = _item_lineDTO
                        }).FirstOrDefault();

                        properties.Add(_userdefinedDTO.Name, (_userDefinedValueDTO is null) ? string.Empty : _userDefinedValueDTO.Value);
                    }
                    _item_LineExpandoObjList.Add(properties);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_LineExpandoObjList;
    }


    public static List<IDictionary<string, Object>> GetItem_LineWithUserDefinedByStation(Item_LineDTO Item_LineDTO)
    {
        var _item_LineExpandoObjList = new List<IDictionary<string, Object>>();
        var _userDefinedList = new List<UserDefinedDTO>();
        try
        {
            var _item_lineList = GetItem_LineList_Global(new Item_LineDTO { StationDTO = Item_LineDTO.StationDTO, GetItem_SupportGroupDTO = true, IsActive = true });

            var _item_SupportGroupIDArray = _item_lineList.Select(s => s.Item_SupportGroupDTO.ID).ToArray();

            _userDefinedList = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO
            { Item_SupportGroupIDArray = _item_SupportGroupIDArray, GetUserDefinedDTO = true }).Select(s => s.UserDefinedDTO).ToList();


            _userDefinedList = _userDefinedList.GroupBy(g => g.ID).Select(s => s.First()).ToList();


            if (_item_lineList.Count() > 0)
            {
                foreach (var _item_lineDTO in _item_lineList)
                {
                    var properties = new ExpandoObject() as IDictionary<string, Object>;
                    properties.Add(nameof(_item_lineDTO.ID), _item_lineDTO.ID);
                    properties.Add($"{nameof(Item_LineXPO.Item_Header)}{nameof(_item_lineDTO.Item_HeaderDTO.SpanishName)}", _item_lineDTO.Item_HeaderDTO.SpanishName);
                    properties.Add($"{nameof(Item_LineXPO.Item_Header)}{nameof(_item_lineDTO.Item_HeaderDTO.EnglishName)}", _item_lineDTO.Item_HeaderDTO.EnglishName);
                    properties.Add(nameof(_item_lineDTO.ManufactureSerialID), _item_lineDTO.ManufactureSerialID);
                    properties.Add(nameof(_item_lineDTO.LegacyID), _item_lineDTO.LegacyID);
                    properties.Add(nameof(_item_lineDTO.Serial), _item_lineDTO.Serial);
                    properties.Add(nameof(_item_lineDTO.IntroductionDate), _item_lineDTO.IntroductionDate);
                    properties.Add(nameof(_item_lineDTO.BasePriceMXN), _item_lineDTO.BasePriceMXN);
                    properties.Add(nameof(_item_lineDTO.BasePriceUSD), _item_lineDTO.BasePriceUSD);
                    properties.Add(nameof(_item_lineDTO.COO), _item_lineDTO.COO);
                    properties.Add(nameof(Item_LineXPO.TransactionOrigin), _item_lineDTO.TransactionOriginDTO.Name);
                    properties.Add(nameof(_item_lineDTO.TransactionNumber), _item_lineDTO.TransactionNumber);
                    properties.Add(nameof(_item_lineDTO.TransactionLine), _item_lineDTO.TransactionLine);
                    properties.Add(nameof(Item_LineXPO.Owner), _item_lineDTO.OwnerDTO.Name);
                    properties.Add($"{nameof(_item_lineDTO.OwnerDTO)}{nameof(_item_lineDTO.OwnerDTO.ID)}", _item_lineDTO.OwnerDTO.ID);
                    properties.Add(nameof(Item_LineXPO.Status), _item_lineDTO.StatusDTO.Name);
                    properties.Add(nameof(Item_LineXPO.SupplyType), _item_lineDTO.SupplyTypeDTO.Name);
                    properties.Add($"{nameof(_item_lineDTO.SupplyTypeDTO)}{nameof(_item_lineDTO.SupplyTypeDTO.ID)}", _item_lineDTO.SupplyTypeDTO.ID); properties.Add(nameof(Item_LineXPO.Station), _item_lineDTO.StationDTO.Name);
                    properties.Add(nameof(Item_LineXPO.ImportInvoice), _item_lineDTO.ImportInvoice);
                    properties.Add(nameof(Item_LineXPO.ImportInvoiceLine), _item_lineDTO.ImportInvoiceLine);
                    properties.Add(nameof(Item_LineXPO.DeclarationNumber), _item_lineDTO.DeclarationNumber);
                    properties.Add(nameof(Item_LineXPO.ShipmentReceiptNumber), _item_lineDTO.ShipmentReceiptNumber);
                    properties.Add($"{nameof(_item_lineDTO.StationDTO)}{nameof(_item_lineDTO.StationDTO.ID)}", _item_lineDTO.StationDTO.ID);
                    properties.Add($"{nameof(_item_lineDTO.Item_HeaderDTO)}{nameof(_item_lineDTO.Item_HeaderDTO.ID)}", _item_lineDTO.Item_HeaderDTO.ID);
                    properties.Add($"{nameof(_item_lineDTO.Item_SupportGroupDTO.SupportGroupDTO)}{nameof(_item_lineDTO.Item_SupportGroupDTO.ID)}", _item_lineDTO.Item_SupportGroupDTO.SupportGroupDTO.ID);
                    properties.Add(nameof(Item_LineXPO.AddedBy), _item_lineDTO.AddedByName);
                    properties.Add(nameof(Item_LineXPO.AddedDate), _item_lineDTO.AddedDate);
                    properties.Add(nameof(Item_LineXPO.LastUpdateBy), _item_lineDTO.LastUpdateByName);
                    properties.Add(nameof(Item_LineXPO.DeliveredDate), _item_lineDTO.DeliveredDate);
                    //properties.Add(nameof(Item_LineXPO.DeliveredTo), _item_lineDTO.DeliveredToDTO.Name);

                    foreach (var _userdefinedDTO in _userDefinedList)
                    {
                        var _userDefinedValueDTO = UserDefinedValue_Service.GetUserDefinedValueList_Global(new UserDefinedValueDTO
                        {
                            UserDefinedDTO = _userdefinedDTO,
                            Item_LineDTO = _item_lineDTO
                        }).FirstOrDefault();

                        properties.Add(_userdefinedDTO.Name, (_userDefinedValueDTO is null) ? string.Empty : _userDefinedValueDTO.Value);
                    }
                    _item_LineExpandoObjList.Add(properties);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_LineExpandoObjList;
    }

    #region Station Line
    public static ValidationResultDTO CreateItem_StationByArrayGlobal(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Item_Line_Validator.CreateItem_StationValidation(Item_LineDTO);
        if (_validationResultDTO.Result)
        {
            if (Item_LineDTO.Item_LineIDArray != null && Item_LineDTO.Item_LineIDArray.Length > 0)
            {
                var _item_LineList = GetItem_LineList_Global(new Item_LineDTO { Item_LineIDArray = Item_LineDTO.Item_LineIDArray });
                foreach (var _item_lineDTO in _item_LineList)
                {
                    _item_lineDTO.StationDTO = Item_LineDTO.StationDTO;
                    _item_lineDTO.LastUpdateByID = Item_LineDTO.AddedByID;
                    _validationResultDTO = UpdateItem_Line_Global(_item_lineDTO);
                }
            }
        }
        return _validationResultDTO;
    }


    public static ValidationResultDTO UpdateItem_StationGlobal(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Item_Line_Validator.UpdateItem_StationValidation(Item_LineDTO);
        if (_validationResultDTO.Result)
        {
            var _item_lineDTO = GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
            if (_item_lineDTO != null)
            {
                _item_lineDTO.StationDTO = Item_LineDTO.StationDTO;
                _item_lineDTO.LastUpdateByID = Item_LineDTO.LastUpdateByID;
                _validationResultDTO = UpdateItem_Line_Global(_item_lineDTO);
            }

        }
        return _validationResultDTO;
    }

    #endregion

    #region reassign Support Group
    public static ValidationResultDTO ReassignSupportGroupToItemGlobal(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Item_SupportGroup_Validator.ReassignSupportGroupToItemValidation(Item_LineDTO);
        if (_validationResultDTO.Result)
        {
            //If relation beetwen Item Header and support group dont exist, create it
            if (_validationResultDTO.Data == null)
            {
                var _item_supportGroupDTO = new Item_SupportGroupDTO
                {
                    Item_HeaderDTO = Item_LineDTO.Item_HeaderDTO,
                    SupportGroupDTO = Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO,
                    AddedByID = Item_LineDTO.LastUpdateByID,
                    IsActive = true
                };
                _validationResultDTO = Item_SupportGroup_Service.CreateItem_SupportGroup_Global(_item_supportGroupDTO);
            }
            if (_validationResultDTO.Result == true && _validationResultDTO.Data != null)
            {
                //get current information of item line
                var _currentItem_lineDTO = GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
                //update the new relation into item line
                _currentItem_lineDTO.Item_SupportGroupDTO.ID = _validationResultDTO.Data;
                _currentItem_lineDTO.LastUpdateByID = Item_LineDTO.LastUpdateByID;
                _validationResultDTO = UpdateItem_Line_Global(_currentItem_lineDTO);

            }

        }
        return _validationResultDTO;
    }
    #endregion
    #region Item Delivery
    public static ValidationResultDTO ItemDeliveryGlobal(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Item_Line_Validator.ItemDeliveryValidation(Item_LineDTO);
        if (_validationResultDTO.Result)
        {
            if (_validationResultDTO.Data == null)
            {
                //If relation beetwen Item Header and support group dont exist, create it
                var _item_supportGroupDTO = new Item_SupportGroupDTO
                {
                    Item_HeaderDTO = Item_LineDTO.Item_HeaderDTO,
                    SupportGroupDTO = Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO,
                    AddedByID = Item_LineDTO.LastUpdateByID,
                    IsActive = true
                };
                _validationResultDTO = Item_SupportGroup_Service.CreateItem_SupportGroup_Global(_item_supportGroupDTO);
            }
            if (_validationResultDTO.Result == true && _validationResultDTO.Data != null)
            {
                //get current information of item line
                var _currentItem_lineDTO = GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
                //get current information of support group
                //Note:  _validationResultDTO.Data = Item_SupportGroup ID 
                var _item_supportGroupDTO = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(
                    new Item_SupportGroupDTO
                    {
                        ID = _validationResultDTO.Data,
                        GetSupportGroupDTO = true
                    }).FirstOrDefault();

                //update the new relation into item line
                //_currentItem_lineDTO.DeliveredToDTO = Item_LineDTO.DeliveredToDTO;
                _currentItem_lineDTO.DeliveredDate = DateTime.Now;
                _currentItem_lineDTO.Item_SupportGroupDTO = _item_supportGroupDTO;
                _currentItem_lineDTO.StationDTO = _item_supportGroupDTO.SupportGroupDTO.StationDTO;
                _currentItem_lineDTO.LastUpdateByID = Item_LineDTO.LastUpdateByID;
                _validationResultDTO = UpdateItem_Line_Global(_currentItem_lineDTO);
            }
        }
        return _validationResultDTO;
    }
    #endregion


    #region Reassign Owner
    public static ValidationResultDTO ReassignOwnerToItemGlobal(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Item_Line_Validator.ReassignOwnerToItemValidation(Item_LineDTO);
        if (_validationResultDTO.Result)
        {
            //get current information of item line
            var _currentItem_lineDTO = GetItem_LineList_Global(new Item_LineDTO { ID = Item_LineDTO.ID }).FirstOrDefault();
            _currentItem_lineDTO.OwnerDTO = Item_LineDTO.OwnerDTO;
            _currentItem_lineDTO.LastUpdateByID = Item_LineDTO.LastUpdateByID;
            _validationResultDTO = UpdateItem_Line_Global(_currentItem_lineDTO);
        }
        return _validationResultDTO;
    }
    #endregion
    #endregion


    #region Files

    public static List<FileDTO> GetItem_LineFileList(Item_LineDTO Item_LineDTO)
    {
        List<FileDTO> _item_LineFileList = new List<FileDTO>();
        try
        {
            if (Item_LineDTO.ID != null && Item_LineDTO.ID > 0)
            {
                var _fileDTO = new FileDTO { ID = (int)Item_LineDTO.ID, FileDirectory = (int)FileDirectory_Enum.ItemLineAttachmentsDirectory };
                _item_LineFileList = File_Service.GetFileList(_fileDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_LineFileList;
    }

    public static ValidationResultDTO DeleteItem_LineFile(FileDTO FileDTO)
    {
        FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemLineAttachmentsDirectory;
        var _validationResultDTO = File_Service.DeleteFile(FileDTO);
        return _validationResultDTO;
    }

    public static List<FileDTO> GetItem_LineFileTreeView(Item_LineDTO Item_LineDTO)
    {
        List<FileDTO> _fileList = new List<FileDTO>();
        try
        {
            if (Item_LineDTO.Item_HeaderDTO.ID != null && Item_LineDTO.Item_HeaderDTO.ID > 0)
            {
                var _item_LineList = GetItem_LineList_Global(new Item_LineDTO { Item_HeaderDTO = Item_LineDTO.Item_HeaderDTO, GetFileList = true });
                int _count = 1;
                foreach (var _item_LineDTO in _item_LineList)
                {
                    foreach (var _attachment in _item_LineDTO.FileList)
                    {
                        if (_fileList.Find(x => x.ID == (int)_item_LineDTO.ID) == null)
                        {
                            var _parentDTO = new FileDTO();
                            _parentDTO.ID = (int)_item_LineDTO.ID;
                            _parentDTO.TreeViewID = _item_LineDTO.ID.ToString();
                            _parentDTO.Name = _item_LineDTO.ManufactureSerialID;
                            _fileList.Add(_parentDTO);
                        }
                        var _fileDTO = new FileDTO();
                        _fileDTO.TreeViewID = $"{_item_LineDTO.ID}_{_count}";
                        _fileDTO.Name = _attachment.Name;
                        _fileDTO.ParentID = _item_LineDTO.ID;
                        _fileDTO.Icon = _attachment.Icon;
                        _fileDTO.URL = _attachment.URL;
                        _fileList.Add(_fileDTO);
                        _count++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _fileList;
    }
    #endregion
}
