using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using Elmah;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;
using CTSTools.BLL.Common.Excel;
using System.IO;
using ExcelDataReader;
using System.Data;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_Line_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateItem_Line_Global(Item_LineDTO Item_LineDTO)
    {

        // Step. 1 Validate fields
        var _validationResultDTO = Item_Line_Validator.CreateItem_Line_Validation(Item_LineDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step. 2 Get station serial from secuence
        _validationResultDTO = GetSerialSecuenceForItem();
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step. 3 Save item line
        Item_LineDTO.Serial = _validationResultDTO.Data;
        Item_LineDTO.AddedDate = DateTime.Now;
        _validationResultDTO = Item_Line_Repository.CreateItem_Line(Item_LineDTO);
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step. 4 Save UserDefined Value
        if (Item_LineDTO.UserDefinedValueList != null && Item_LineDTO.ID > 0)
        {
            foreach (var _userDefinedValueDTO in Item_LineDTO.UserDefinedValueList)
            {
                _userDefinedValueDTO.Item_LineID = Item_LineDTO.ID;
                _userDefinedValueDTO.AddedByID = Item_LineDTO.AddedByID;
                _validationResultDTO = UserDefinedValue_Service.CreateUserDefinedValue_Global(_userDefinedValueDTO);
            }
        }

        if (!_validationResultDTO.Result)
            return _validationResultDTO;
        // Step. 5 Create record ChangeLog
        ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<Item_LineDTO>(Item_LineDTO, (int)Item_LineDTO.AddedByID, (int)Item_LineDTO.ID);

        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateMassiveList_Global(List<Item_LineDTO> Item_LineList)
    {
        var _item_SupportGroupList = new List<Item_SupportGroupDTO>();
        var _validationResultDTO = new ValidationResultDTO();
        // Step. 1. Get station serial from secuence
        for (int i = 0; i < Item_LineList.Count; i++)
        {
            _validationResultDTO = GetSerialSecuenceForItem();
            // We assign the serial of the Item_LineDTO within the _validationResultDTO corresponding to the properties
            Item_LineList[i].Serial = _validationResultDTO.Data;
        }
        if (!_validationResultDTO.Result)
            return _validationResultDTO;

        // Step. 3 Save item line
        _validationResultDTO = Item_Line_Repository.CreateMultiple(Item_LineList);

        return _validationResultDTO;
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
                    _userDefinedValueDTO.Item_LineID = Item_LineDTO.ID;
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
                Item_LineDTO.Item_HeaderDTO.Item_HeaderIDArray = Item_LineList.GroupBy(g => g.Item_HeaderID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_headerDict = Item_Header_Service.GetItem_HeaderList_Global(Item_LineDTO.Item_HeaderDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_LineDTO.GetStatusDTO)
            {
                Item_LineDTO.StatusDTO.StatusIDArray = Item_LineList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(Item_LineDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_LineDTO.GetItem_SupportGroupDTO)
            {
                Item_LineDTO.Item_SupportGroupDTO.Item_SupportGroupIDArray = Item_LineList.GroupBy(g => g.Item_SupportGroupID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_supportgroupDict = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(Item_LineDTO.Item_SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_LineDTO.GetStationDTO)
            {
                Item_LineDTO.StationDTO.StationIDArray = Item_LineList.GroupBy(g => g.StationID)
                        .Select(s => s.Key)
                        .ToArray();
                _stationDict = Station_Service.GetStationList_Global(Item_LineDTO.StationDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _item_lineDTO in Item_LineList)
            {
                if (Item_LineDTO.GetItem_HeaderDTO && _item_headerDict.ContainsKey(_item_lineDTO.Item_HeaderID))
                {
                    _item_lineDTO.Item_HeaderDTO = _item_headerDict[_item_lineDTO.Item_HeaderID];
                }
                if (Item_LineDTO.GetStatusDTO && _statusDict.ContainsKey(_item_lineDTO.StatusID))
                {
                    _item_lineDTO.StatusDTO = _statusDict[_item_lineDTO.StatusID];
                }
                if (Item_LineDTO.GetStationDTO && _stationDict.ContainsKey(_item_lineDTO.StationID))
                {
                    _item_lineDTO.StationDTO = _stationDict[_item_lineDTO.StationID];
                }
                if (Item_LineDTO.GetItem_SupportGroupDTO && _item_supportgroupDict.ContainsKey(_item_lineDTO.Item_SupportGroupID))
                {
                    _item_lineDTO.Item_SupportGroupDTO = _item_supportgroupDict[_item_lineDTO.Item_SupportGroupID];
                }
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

    public static ValidationResultDTO GetSerialSecuenceForItem()
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _item_LineSerial = string.Format("STN{0}", AssetManagementSQL.GetItemLineSerial());
            _validationResultDTO.Data = _item_LineSerial;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("An error has occurred. {0}", ex.Message);
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
                Item_HeaderID = Item_LineDTO.Item_HeaderID,
                GetUserDefinedDTO = true
            }).Select(s => s.UserDefinedDTO).ToList();
            var _item_lineList = GetItem_LineList_Global(new Item_LineDTO { Item_SupportGroupID = Item_LineDTO.Item_SupportGroupID, GetItem_SupportGroupDTO = true, IsActive = true });
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
                    properties.Add(nameof(Item_LineXPO.Owner), _item_lineDTO.OwnerName);
                    properties.Add(nameof(Item_LineXPO.SupplyType), _item_lineDTO.SupplyTypeName);
                    properties.Add(nameof(_item_lineDTO.SupplyTypeID), _item_lineDTO.SupplyTypeID);
                    properties.Add(nameof(_item_lineDTO.BasePriceUSD), _item_lineDTO.BasePriceUSD);
                    properties.Add(nameof(Item_LineXPO.Station), _item_lineDTO.StationName);
                    properties.Add(nameof(_item_lineDTO.StationID), _item_lineDTO.StationID);
                    properties.Add(nameof(_item_lineDTO.Comments), _item_lineDTO.Comments);
                    properties.Add(nameof(_item_lineDTO.OwnerID), _item_lineDTO.OwnerID);
                    properties.Add(nameof(_item_lineDTO.Item_SupportGroupDTO.SupportGroupID), _item_lineDTO.Item_SupportGroupDTO.SupportGroupID);
                    properties.Add(nameof(_item_lineDTO.Item_SupportGroupID), _item_lineDTO.Item_SupportGroupID);
                    properties.Add(nameof(Item_LineXPO.Status), _item_lineDTO.StatusName);
                    properties.Add(nameof(_item_lineDTO.StatusID), _item_lineDTO.StatusID);
                    properties.Add(nameof(Item_LineXPO.PONumber), _item_lineDTO.PONumber);
                    properties.Add(nameof(Item_LineXPO.POLine), _item_lineDTO.POLine);
                    properties.Add(nameof(Item_LineXPO.ImportInvoice), _item_lineDTO.ImportInvoice);
                    properties.Add(nameof(Item_LineXPO.ImportInvoiceLine), _item_lineDTO.ImportInvoiceLine);
                    properties.Add(nameof(Item_LineXPO.DeclarationNumber), _item_lineDTO.DeclarationNumber);
                    properties.Add(nameof(Item_LineXPO.ShipmentReceiptNumber), _item_lineDTO.ShipmentReceiptNumber);
                    properties.Add(nameof(_item_lineDTO.Item_HeaderID), _item_lineDTO.Item_HeaderID);
                    properties.Add(nameof(Item_LineXPO.AddedBy), _item_lineDTO.AddedByName);
                    properties.Add(nameof(_item_lineDTO.AddedDate), _item_lineDTO.AddedDate);
                    properties.Add(nameof(Item_LineXPO.LastUpdateBy), _item_lineDTO.LastUpdateByName);
                    properties.Add("DeliveredToID", _item_lineDTO.DeliveredToID);
                    properties.Add(nameof(Item_LineXPO.DeliveredTo), _item_lineDTO.DeliveredToName);
                    properties.Add(nameof(_item_lineDTO.DeliveredDate), _item_lineDTO.DeliveredDate);

                    foreach (var _userdefinedDTO in _userDefinedList)
                    {
                        var _userDefinedValueDTO = UserDefinedValue_Service.GetUserDefinedValueList_Global(new UserDefinedValueDTO
                        {
                            UserDefinedID = _userdefinedDTO.ID,
                            Item_LineID = _item_lineDTO.ID
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
            var _item_lineList = GetItem_LineList_Global(new Item_LineDTO { StationID = Item_LineDTO.StationID, GetItem_SupportGroupDTO = true, IsActive = true });
            var _item_SupportGroupIDArray = _item_lineList.Select(s => s.Item_SupportGroupID).ToArray();

            _userDefinedList = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO
            { Item_SupportGroupIDArray = _item_SupportGroupIDArray, GetUserDefinedDTO = true }).Select(s => s.UserDefinedDTO).ToList();
            _userDefinedList = _userDefinedList.GroupBy(g => g.ID).Select(s => s.First()).ToList();

            if (_item_lineList.Count() > 0)
            {
                foreach (var _item_lineDTO in _item_lineList)
                {
                    var properties = new ExpandoObject() as IDictionary<string, Object>;
                    properties.Add(nameof(_item_lineDTO.ID), _item_lineDTO.ID);
                    properties.Add(nameof(_item_lineDTO.ManufactureSerialID), _item_lineDTO.ManufactureSerialID);
                    properties.Add(nameof(_item_lineDTO.LegacyID), _item_lineDTO.LegacyID);
                    properties.Add(nameof(_item_lineDTO.Serial), _item_lineDTO.Serial);
                    properties.Add(nameof(_item_lineDTO.BasePriceUSD), _item_lineDTO.BasePriceUSD);
                    properties.Add(nameof(Item_LineXPO.Owner), _item_lineDTO.OwnerName);
                    properties.Add(nameof(_item_lineDTO.OwnerID), _item_lineDTO.OwnerID);
                    properties.Add(nameof(Item_LineXPO.Status), _item_lineDTO.StatusName);
                    properties.Add(nameof(Item_LineXPO.SupplyType), _item_lineDTO.SupplyTypeName);
                    properties.Add(nameof(_item_lineDTO.SupplyTypeID), _item_lineDTO.SupplyTypeID); 
                    properties.Add(nameof(Item_LineXPO.Station), _item_lineDTO.StationName);
                    properties.Add(nameof(Item_LineXPO.ImportInvoice), _item_lineDTO.ImportInvoice);
                    properties.Add(nameof(Item_LineXPO.ImportInvoiceLine), _item_lineDTO.ImportInvoiceLine);
                    properties.Add(nameof(Item_LineXPO.DeclarationNumber), _item_lineDTO.DeclarationNumber);
                    properties.Add(nameof(Item_LineXPO.ShipmentReceiptNumber), _item_lineDTO.ShipmentReceiptNumber);
                    properties.Add(nameof(_item_lineDTO.StationID), _item_lineDTO.StationID);
                    properties.Add(nameof(_item_lineDTO.Item_HeaderID), _item_lineDTO.Item_HeaderID);
                    properties.Add(nameof(_item_lineDTO.Item_SupportGroupDTO.SupportGroupID), _item_lineDTO.Item_SupportGroupDTO.SupportGroupID);
                    properties.Add(nameof(Item_LineXPO.AddedBy), _item_lineDTO.AddedByName);
                    properties.Add(nameof(Item_LineXPO.AddedDate), _item_lineDTO.AddedDate);
                    properties.Add(nameof(Item_LineXPO.LastUpdateBy), _item_lineDTO.LastUpdateByName);
                    properties.Add("DeliveredToID", _item_lineDTO.DeliveredToID);
                    properties.Add(nameof(Item_LineXPO.DeliveredTo), _item_lineDTO.DeliveredToName);
                    properties.Add(nameof(Item_LineXPO.DeliveredDate), _item_lineDTO.DeliveredDate);

                    foreach (var _userdefinedDTO in _userDefinedList)
                    {
                        var _userDefinedValueDTO = UserDefinedValue_Service.GetUserDefinedValueList_Global(new UserDefinedValueDTO
                        {
                            UserDefinedID = _userdefinedDTO.ID,
                            Item_LineID = _item_lineDTO.ID
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
                    _item_lineDTO.StationID = Item_LineDTO.StationID;
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
                _item_lineDTO.StationID = Item_LineDTO.StationID;
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
                    Item_HeaderID = Item_LineDTO.Item_HeaderID,
                    SupportGroupID = Item_LineDTO.Item_SupportGroupDTO.SupportGroupID,
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
                _currentItem_lineDTO.Item_SupportGroupID = _validationResultDTO.Data;
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
                    Item_HeaderID = Item_LineDTO.Item_HeaderID,
                    SupportGroupID = Item_LineDTO.Item_SupportGroupDTO.SupportGroupID,
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
            _currentItem_lineDTO.OwnerID = Item_LineDTO.OwnerID;
            _currentItem_lineDTO.LastUpdateByID = Item_LineDTO.LastUpdateByID;
            _validationResultDTO = UpdateItem_Line_Global(_currentItem_lineDTO);
        }
        return _validationResultDTO;
    }
    #endregion

    #endregion

    #region Upload Excel functions

    public static ValidationResultDTO GenerateItem_LineFromExcel(FileDTO FileDTO)
    {
        var _excelRowDTO = new ExcelRowDTO();
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been create successfully.."
        };
        try
        {
            // step 1. Validate that it is an excel file
            _validationResultDTO = ExcelImport_Validator.ExcelFile_Validation(FileDTO);
            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 2. Bring the information from excel
            _validationResultDTO = ExcelImport_Validator.ExcelHeaderColumns_Validation(FileDTO);

            if (!_validationResultDTO.Result)
                return _validationResultDTO;
            // step 3. Validate that the list of data comes
            FileDTO.DirectoryArray = _validationResultDTO.Data;
            _validationResultDTO = GetItem_LineListFromExcel(FileDTO);

            if (_validationResultDTO.Data == null)
                return _validationResultDTO;
            // step 4. Validate that the column information exists in the db
            _validationResultDTO = Item_Line_Validator.ExcelItem_LineInformation_Validation(_validationResultDTO.Data);
            _excelRowDTO = _validationResultDTO.Data;

            if (_excelRowDTO.GoodRowLinesList.Count <= 0)
                return _validationResultDTO;
            // step 5. Create Item_Line
            _validationResultDTO = Item_Line_Service.CreateMassiveList_Global(_excelRowDTO.GoodRowLinesList);
            _validationResultDTO.Data = _excelRowDTO;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw;
        }
        return _validationResultDTO;
    }
    private static ValidationResultDTO GetItem_LineListFromExcel(FileDTO FileDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _excelRowDTO = new ExcelRowDTO
            {
                GoodRowLinesList = new List<Item_LineDTO>(),
                BadRowLinesList = new List<Item_LineDTO>()
            };
            // Converts the Base64 string contained in FileDTO.Data to a byte array,
            // To later process it and read the content of the Excel file.
            var _fileBytes = Convert.FromBase64String(FileDTO.Data.Split(',')[1]);
            using (var _fileStream = new MemoryStream(_fileBytes))
            using (var _excelReader = ExcelReaderFactory.CreateReader(_fileStream))
            {
                var _excelDataSet = _excelReader.AsDataSet();
                DataTable _firstTable = _excelDataSet.Tables[0];
                // Create a dictionary to store the excel column indexes
                var _columnHeaderMap = new Dictionary<string, int>();
                // Create the list with the name of the columns that were previously validated
                var _columnHeaderNameList = FileDTO.DirectoryArray.Select(ColumnName => ColumnName.ToUpper()).ToList();
                // This is to identify the columns within the Excel, to later bring the information contained in the row
                for (int colIndex = 0; colIndex < _firstTable.Columns.Count; colIndex++)
                {
                    // We take the column name, put it in capital letters to compare it with our list (_columnHeaderNameList) 
                    // and we remove all the spaces from the name before comparing it with the list. (e.g. " Unit  Of Measure " -> "UnitOfMeasure")
                    string headerName = _firstTable.Rows[0][colIndex].ToString().ToUpper();
                    // The regular expression @"\s+", Removes all whitespace from the column name
                    headerName = System.Text.RegularExpressions.Regex.Replace(headerName, @"\s+", "");
                    if (_columnHeaderNameList.Contains(headerName))
                    {
                        // If the column name exists, it stores it in the dirctory (_columnHeaderMap) and assigns its identifier
                        _columnHeaderMap[headerName] = colIndex;
                    }
                }

                // Process rows starting from the second row (index 1), to take the information to store in each iteration
                for (int rowIndex = 1; rowIndex < _firstTable.Rows.Count; rowIndex++)
                {
                    DataRow row = _firstTable.Rows[rowIndex];
                    // We create a DTO where we will store the content of the excel to later validate it
                    var _item_LineDTO = new Item_LineDTO();
                    // The assigned ID is to have the Excel row identified in case it does not pass the validations.
                    _item_LineDTO.ID = rowIndex + 1;

                    _item_LineDTO.Item_HeaderID = FileDTO.FileDirectory;
                    _item_LineDTO.Item_SupportGroupID = FileDTO.TreeViewID;
                    _item_LineDTO.Item_SupportGroupDTO.SupportGroupID = FileDTO.ParentID;
                    // The row[_columnHeaderMap["NAME"]] returns the index of that directory name (e.g row[0] -> TestName) and turns it into string
                    // What the CleanRowString function does is remove all the spaces on the sides and internal in each word,
                    // Leaving only one space between the words (e.g. " Unit  Of Measure " -> "Unit Of Measure")
                    _item_LineDTO.ManufactureSerialID = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["MANUFACTURESERIAL"]].ToString());
                    _item_LineDTO.LegacyID = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["LEGACY"]].ToString());
                    _item_LineDTO.OwnerName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["OWNER"]].ToString());
                    _item_LineDTO.StationName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["STATION"]].ToString());
                    _item_LineDTO.Comments = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["COMMENTS"]].ToString());
                    _item_LineDTO.StatusName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["STATUS"]].ToString());
                    _item_LineDTO.DeliveredToName = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["DELIVEREDTO"]].ToString());
                    _item_LineDTO.AddedByID = FileDTO.ID;

                    // Gets the value of the "PRICE" column from the Excel file and cleans it of unwanted characters.
                    string _rowValue = ExcelImport_Service.CleanRowString(row[_columnHeaderMap["PRICE"]]?.ToString());
                    // float.TryParse try to convert the value (_rowValue) to a number of type float
                    // Note: TryParse will not throw an exception if the conversion fails. Instead, it will return a boolean value.
                    // The 'out' keyword indicates that Price is an output parameter.
                    // e.g. If _rowValue is "3.14" then TryParse will return true and Price will be set to 3.14.
                    if (float.TryParse(_rowValue, out float Price))
                        // If the conversion is successful, the converted value will be assigned to the Price variable.
                        _item_LineDTO.BasePriceUSD = Price;
                    else
                        // Returns a -1, to identify that Price does not comply with the format or is null
                        _item_LineDTO.BasePriceUSD = -1;

                    // We validate the DTO to verify that our properties are not null
                    _validationResultDTO = Item_Line_Validator.ExcelItem_LineRows_Validation(_item_LineDTO);

                    // We verify if our DTO complied with the validations
                    if (_validationResultDTO.Data.GoodRowLinesList.Count > 0)
                        // If the DTO does not have null properties, it is stored in the GoodRowLines.
                        _excelRowDTO.GoodRowLinesList.AddRange(_validationResultDTO.Data.GoodRowLinesList);
                    else
                        // If any of the DTO properties is null, it is stored on a BadRowLines.
                        _excelRowDTO.BadRowLinesList.AddRange(_validationResultDTO.Data.BadRowLinesList);
                }
            }
            // Verify if there were good or bad lines to return _excelRowDTO
            if (_excelRowDTO.GoodRowLinesList.Count > 0 || _excelRowDTO.BadRowLinesList.Count > 0)
            {
                _validationResultDTO.Data = _excelRowDTO;
                return _validationResultDTO;
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Description = "Column records have no data.";
                _validationResultDTO.Message = "Error";
                return _validationResultDTO;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error";
            _validationResultDTO.Description = ex.Message;
            return _validationResultDTO;
        }
    }

    #endregion

}
