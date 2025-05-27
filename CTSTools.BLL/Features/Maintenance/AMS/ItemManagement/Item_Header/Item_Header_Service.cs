using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.ItemClassification;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_Header_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateItem_Header_Global(Item_HeaderDTO Item_HeaderDTO)
    {
        var _ValidationResultDTO = Item_Header_Validator.CreateItem_Header_Validation(Item_HeaderDTO);
        if (_ValidationResultDTO.Result && (Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0))
        {
            Item_HeaderDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Item_Header_Repository.CreateItem_Header(Item_HeaderDTO);
            Item_HeaderDTO.ID = _ValidationResultDTO.Data;
        }
        //Save Item Picture
        if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.Data != null)
        {
            Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
            Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderPictureDirectory;
            _ValidationResultDTO = File_Service.SaveFile_Global(Item_HeaderDTO.FileDTO);
        }
        //Save Item_SupportGroup
        if (_ValidationResultDTO.Result && Item_HeaderDTO.SupportGroupID != 0 && Item_HeaderDTO.SupportGroupID != null) 
        {
            var _item_SupportGroupDTO = new Item_SupportGroupDTO();
            _item_SupportGroupDTO.Item_HeaderDTO.ID = Item_HeaderDTO.ID;
            _item_SupportGroupDTO.SupportGroupDTO.ID = Item_HeaderDTO.SupportGroupID;
            _item_SupportGroupDTO.AddedByID = Item_HeaderDTO.AddedByID;
            _item_SupportGroupDTO.IsActive = Item_HeaderDTO.IsActive;
            _ValidationResultDTO = Item_SupportGroup_Service.CreateItem_SupportGroup_Global(_item_SupportGroupDTO);
        }
        //Save Attachments
        //if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.FileList != null)
        //{
        //    Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
        //    Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory;
        //    _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(Item_HeaderDTO.FileDTO);
        //}
        //Save UserDefined
        if (_ValidationResultDTO.Result && Item_HeaderDTO.UserDefinedIDArray.Length > 0)
        {
            var _userDefinedTemplateDTO = new UserDefinedTemplateDTO();
            _userDefinedTemplateDTO.UserDefinedIDArray = Item_HeaderDTO.UserDefinedIDArray;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.ID = _ValidationResultDTO.Data;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO.ID = Item_HeaderDTO.SupportGroupID;
            _userDefinedTemplateDTO.LastUpdateByID = Item_HeaderDTO.AddedByID;
            _userDefinedTemplateDTO.IsActive = Item_HeaderDTO.IsActive;
            _ValidationResultDTO = UserDefinedTemplate_Service.UpdateUserDefinedTemplate_Global(_userDefinedTemplateDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<Item_HeaderDTO>(Item_HeaderDTO, (int)Item_HeaderDTO.AddedByID, (int)Item_HeaderDTO.ID);
        }
        _ValidationResultDTO.Data = Item_HeaderDTO.ID;
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateItem_Header_Global(Item_HeaderDTO Item_HeaderDTO)
    {
        var _ValidationResultDTO = Item_Header_Validator.UpdateItem_Header_Validation(Item_HeaderDTO);
        var _previousItem_HeaderDTO = GetItem_HeaderList_Global(new Item_HeaderDTO { ID = Item_HeaderDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            Item_HeaderDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Item_Header_Repository.UpdateItem_Header(Item_HeaderDTO);
        }
        //Save Item Picture
        if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.Data != null)
        {
            Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
            Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderPictureDirectory;
            _ValidationResultDTO = File_Service.UpdateFile_Global(Item_HeaderDTO.FileDTO);
        }
        //Save Attachments
        //if (_ValidationResultDTO.Result && Item_HeaderDTO.FileDTO != null && Item_HeaderDTO.FileDTO.FileList != null)
        //{
        //    Item_HeaderDTO.FileDTO.ID = (int)Item_HeaderDTO.ID;
        //    Item_HeaderDTO.FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory;
        //    _ValidationResultDTO = File_Service.SaveMultipleFiles_Global(Item_HeaderDTO.FileDTO);
        //}
        //Save UserDefined
        if (_ValidationResultDTO.Result && Item_HeaderDTO.UserDefinedIDArray.Length >= 0)
        {
            var _userDefinedTemplateDTO = new UserDefinedTemplateDTO();
            _userDefinedTemplateDTO.UserDefinedIDArray = Item_HeaderDTO.UserDefinedIDArray;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.ID = Item_HeaderDTO.Item_SupportGroupID;
            _userDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO.ID = Item_HeaderDTO.SupportGroupID;
            _userDefinedTemplateDTO.LastUpdateByID = Item_HeaderDTO.LastUpdateByID;
            _userDefinedTemplateDTO.IsActive = Item_HeaderDTO.IsActive;
            _ValidationResultDTO = UserDefinedTemplate_Service.UpdateUserDefinedTemplate_Global(_userDefinedTemplateDTO);
        }
        //Save Change log
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<Item_HeaderDTO>(_previousItem_HeaderDTO, Item_HeaderDTO, (int)Item_HeaderDTO.LastUpdateByID, (int)Item_HeaderDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteItem_Header_Global(Item_HeaderDTO Item_HeaderDTO)
    {
        //item validation
        var _ValidationResultDTO = Item_Header_Validator.DeleteItem_Header_Validation(Item_HeaderDTO);
        var _previousItem_HeaderDTO = GetItem_HeaderList_Global(new Item_HeaderDTO { ID = Item_HeaderDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Item_Header_Repository.DeleteItem_Header(Item_HeaderDTO);
        }
        //Save Change log
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<Item_HeaderDTO>(_previousItem_HeaderDTO, (int)Item_HeaderDTO.LastUpdateByID, (int)Item_HeaderDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<Item_HeaderDTO> GetItem_HeaderList_Global(Item_HeaderDTO Item_HeaderDTO, PagedResultDTO<Item_HeaderDTO> PagedResultDTO = null)
    {
        var _item_headerglobalList = new List<Item_HeaderDTO>();
        try
        {
            var _item_headerList = Item_Header_Repository.GetItem_HeaderList(Item_HeaderDTO, PagedResultDTO);
            // if Item_Header is empty, return list
            if (_item_headerList.Count() == 0)
            {
                _item_headerglobalList = _item_headerList;
                return _item_headerglobalList;
            }
            if (!Item_HeaderDTO.GetItemClassificationDTO /*&& !Item_HeaderDTO.GetSupportGroupDTO*/ && !Item_HeaderDTO.GetItemHeaderPicture)
            {
                _item_headerglobalList = _item_headerList;
                return _item_headerglobalList;
            }
            _item_headerglobalList = GetItem_HeaderRelatedData(Item_HeaderDTO, _item_headerList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_headerglobalList;
    }



    public static List<Item_HeaderDTO> GetItem_HeaderRelatedData(Item_HeaderDTO Item_HeaderDTO, List<Item_HeaderDTO> Item_HeaderList)
    {
        var _item_headerglobalList = new List<Item_HeaderDTO>();
        var _itemclassificationDict = new Dictionary<int?, ItemClassificationDTO>();

        try
        {
            if (Item_HeaderDTO.GetItemClassificationDTO)
            {
                Item_HeaderDTO.ItemClassificationDTO.ItemClassificationIDArray = Item_HeaderList.GroupBy(g => g.ItemClassificationDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _itemclassificationDict = ItemClassification_Service.GetItemClassificationList_Global(Item_HeaderDTO.ItemClassificationDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }

            foreach (var _item_headerDTO in Item_HeaderList)
            {
                if (Item_HeaderDTO.GetItemClassificationDTO && _itemclassificationDict.ContainsKey(_item_headerDTO.ItemClassificationDTO.ID))
                {
                    _item_headerDTO.ItemClassificationDTO = _itemclassificationDict[_item_headerDTO.ItemClassificationDTO.ID];
                }

                if (Item_HeaderDTO.GetItemHeaderPicture)
                {
                    var _fileDTO = new FileDTO
                    {
                        ID = (int)_item_headerDTO.ID,
                        FileDirectory = (int)FileDirectory_Enum.ItemHeaderPictureDirectory
                    };
                    _item_headerDTO.ItemImg = File_Service.GetFile(_fileDTO).URL;
                }
                _item_headerglobalList.Add(_item_headerDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_headerglobalList;
    }



    public static int GetItem_HeaderTotalCount(PagedResultDTO<Item_HeaderDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Item_Header_Repository.GetItem_HeaderCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic


    #endregion



    #region Files
    public static List<FileDTO> GetItem_HeaderFileList(Item_HeaderDTO Item_HeaderDTO)
    {
        List<FileDTO> _Item_HeaderFileList = new List<FileDTO>();
        try
        {
            if (Item_HeaderDTO.ID != null && Item_HeaderDTO.ID > 0)
            {
                var _fileDTO = new FileDTO { ID = (int)Item_HeaderDTO.ID, FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory };
                _Item_HeaderFileList = File_Service.GetFileList(_fileDTO);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _Item_HeaderFileList;
    }

    public static ValidationResultDTO DeleteItemHeaderFile(FileDTO FileDTO)
    {
        FileDTO.FileDirectory = (int)FileDirectory_Enum.ItemHeaderAttachmentsDirectory;
        var _validationResultDTO = File_Service.DeleteFile(FileDTO);
        return _validationResultDTO;
    }
    #endregion
}
