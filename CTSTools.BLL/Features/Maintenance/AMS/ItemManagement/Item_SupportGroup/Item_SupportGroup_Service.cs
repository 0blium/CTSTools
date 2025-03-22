using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;

public class Item_SupportGroup_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateItem_SupportGroup_Global(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _ValidationResultDTO = Item_SupportGroup_Validator.CreateItem_SupportGroup_Validation(Item_SupportGroupDTO);
        if (_ValidationResultDTO.Result)
        {
            Item_SupportGroupDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Item_SupportGroup_Repository.CreateItem_SupportGroup(Item_SupportGroupDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            Item_SupportGroupDTO.ID = _ValidationResultDTO.Data;
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<Item_SupportGroupDTO>(Item_SupportGroupDTO, (int)Item_SupportGroupDTO.AddedByID, (int)Item_SupportGroupDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateItem_SupportGroup_Global(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _ValidationResultDTO = Item_SupportGroup_Validator.UpdateItem_SupportGroup_Validation(Item_SupportGroupDTO);
        var _previousItem_SupportGroupDTO = GetItem_SupportGroupList_Global(new Item_SupportGroupDTO { ID = Item_SupportGroupDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            Item_SupportGroupDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Item_SupportGroup_Repository.UpdateItem_SupportGroup(Item_SupportGroupDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<Item_SupportGroupDTO>(_previousItem_SupportGroupDTO, Item_SupportGroupDTO, (int)Item_SupportGroupDTO.LastUpdateByID, (int)Item_SupportGroupDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteItem_SupportGroup_Global(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _ValidationResultDTO = Item_SupportGroup_Validator.DeleteItem_SupportGroup_Validation(Item_SupportGroupDTO);
        var _previousItem_SupportGroupDTO = GetItem_SupportGroupList_Global(new Item_SupportGroupDTO { ID = Item_SupportGroupDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            var _userDefinedTemplateList = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO { Item_SupportGroupDTO = Item_SupportGroupDTO });
            if (_userDefinedTemplateList != null)
            {
                foreach (var _userdefinedDTO in _userDefinedTemplateList)
                {
                    //Delete user defined template values
                    _ValidationResultDTO = UserDefinedTemplate_Service.DeleteUserDefinedTemplate_Global(_userdefinedDTO);
                }
            }
            _ValidationResultDTO = Item_SupportGroup_Repository.DeleteItem_SupportGroup(Item_SupportGroupDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<Item_SupportGroupDTO>(_previousItem_SupportGroupDTO, (int)Item_SupportGroupDTO.LastUpdateByID, (int)Item_SupportGroupDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<Item_SupportGroupDTO> GetItem_SupportGroupList_Global(Item_SupportGroupDTO Item_SupportGroupDTO, PagedResultDTO<Item_SupportGroupDTO> PagedResultDTO = null)
    {
        var _item_supportgroupglobalList = new List<Item_SupportGroupDTO>();
        try
        {
            var _item_supportgroupList = Item_SupportGroup_Repository.GetItem_SupportGroupList(Item_SupportGroupDTO, PagedResultDTO);
            // if Item_SupportGroup is empty, return list
            if (_item_supportgroupList.Count() == 0)
            {
                _item_supportgroupglobalList = _item_supportgroupList;
                return _item_supportgroupglobalList;
            }
            if (!Item_SupportGroupDTO.GetItem_HeaderDTO && !Item_SupportGroupDTO.GetSupportGroupDTO)
            {
                _item_supportgroupglobalList = _item_supportgroupList;
                return _item_supportgroupglobalList;
            }
            _item_supportgroupglobalList = GetItem_SupportGroupRelatedData(Item_SupportGroupDTO, _item_supportgroupList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_supportgroupglobalList;
    }



    public static List<Item_SupportGroupDTO> GetItem_SupportGroupRelatedData(Item_SupportGroupDTO Item_SupportGroupDTO, List<Item_SupportGroupDTO> Item_SupportGroupList)
    {
        var _item_supportgroupglobalList = new List<Item_SupportGroupDTO>();
        var _item_headerDict = new Dictionary<int?, Item_HeaderDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();

        try
        {
            if (Item_SupportGroupDTO.GetItem_HeaderDTO)
            {
                Item_SupportGroupDTO.Item_HeaderDTO.Item_HeaderIDArray = Item_SupportGroupList.GroupBy(g => g.Item_HeaderDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_headerDict = Item_Header_Service.GetItem_HeaderList_Global(Item_SupportGroupDTO.Item_HeaderDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (Item_SupportGroupDTO.GetSupportGroupDTO)
            {
                Item_SupportGroupDTO.SupportGroupDTO.SupportGroupIDArray = Item_SupportGroupList.GroupBy(g => g.SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(Item_SupportGroupDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _item_supportgroupDTO in Item_SupportGroupList)
            {
                if (Item_SupportGroupDTO.GetItem_HeaderDTO && _item_headerDict.ContainsKey(_item_supportgroupDTO.Item_HeaderDTO.ID))
                {
                    _item_supportgroupDTO.Item_HeaderDTO = _item_headerDict[_item_supportgroupDTO.Item_HeaderDTO.ID];
                }
                if (Item_SupportGroupDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_item_supportgroupDTO.SupportGroupDTO.ID))
                {
                    _item_supportgroupDTO.SupportGroupDTO = _supportgroupDict[_item_supportgroupDTO.SupportGroupDTO.ID];
                }
                _item_supportgroupglobalList.Add(_item_supportgroupDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_supportgroupglobalList;
    }

    public static int GetItem_SupportGroupTotalCount(PagedResultDTO<Item_SupportGroupDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Item_SupportGroup_Repository.GetItem_SupportGroupCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
