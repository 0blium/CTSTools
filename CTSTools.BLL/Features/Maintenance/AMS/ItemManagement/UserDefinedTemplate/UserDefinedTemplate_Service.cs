using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplate_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUserDefinedTemplate_Global(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _ValidationResultDTO = UserDefinedTemplate_Validator.CreateUserDefinedTemplate_Validation(UserDefinedTemplateDTO);
        if (_ValidationResultDTO.Result)
        {
            UserDefinedTemplateDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = UserDefinedTemplate_Repository.CreateUserDefinedTemplate(UserDefinedTemplateDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateUserDefinedTemplate_Global(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            if (UserDefinedTemplateDTO.UserDefinedIDArray?.Count() > 0)
            {
                _validationResultDTO = UserDefinedTemplate_Validator.UpdateUserDefinedTemplate_Validation(UserDefinedTemplateDTO);
                if (_validationResultDTO.Result)
                {
                    foreach (var _userdefinedIDObj in UserDefinedTemplateDTO.UserDefinedIDArray)
                    {
                        var _userdefinedTemplateDTO = new UserDefinedTemplateDTO();
                        _userdefinedTemplateDTO.UserDefinedDTO.ID = _userdefinedIDObj;
                        _userdefinedTemplateDTO.Item_HeaderID = UserDefinedTemplateDTO.Item_HeaderID;
                        _userdefinedTemplateDTO.Item_SupportGroupDTO = UserDefinedTemplateDTO.Item_SupportGroupDTO;
                        _userdefinedTemplateDTO.AddedByID = UserDefinedTemplateDTO.LastUpdateByID;
                        _userdefinedTemplateDTO.IsActive = UserDefinedTemplateDTO.IsActive;
                        CreateUserDefinedTemplate_Global(_userdefinedTemplateDTO);
                    }

                    if (_validationResultDTO.Result)
                    {
                        // 3. Delete record if not check in the list
                        DeleteUnselectedUserDefinedTemplate(UserDefinedTemplateDTO);
                    }
                }
            }
            else
            {
                // 4. Validates fields
                _validationResultDTO = UserDefinedTemplate_Validator.UserDefinedTemplate_Validation(UserDefinedTemplateDTO);
                if (_validationResultDTO.Result)
                {
                    // 5. Delete record if not check in the list
                    DeleteUnselectedUserDefinedTemplate(UserDefinedTemplateDTO);
                }
            }


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }

        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteUserDefinedTemplate_Global(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _ValidationResultDTO = UserDefinedTemplate_Validator.DeleteUserDefinedTemplate_Validation(UserDefinedTemplateDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = UserDefinedTemplate_Repository.DeleteUserDefinedTemplate(UserDefinedTemplateDTO);
        }
        return _ValidationResultDTO;
    }

    public static ValidationResultDTO DeleteUnselectedUserDefinedTemplate(UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _ValidationResultDTO = new ValidationResultDTO();
        try
        {
            //var _userDefinedTemplateList = GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO { Item_HeaderDTO = UserDefinedTemplateDTO.Item_HeaderDTO });
            var _userDefinedTemplateList = GetUserDefinedTemplateList_Global(new UserDefinedTemplateDTO { Item_SupportGroupDTO = UserDefinedTemplateDTO.Item_SupportGroupDTO });
            var _unselectedUserDefinedList = (from _userDefinedTemplateDTO in _userDefinedTemplateList
                                              where UserDefinedTemplateDTO.UserDefinedIDArray.Contains(_userDefinedTemplateDTO.UserDefinedDTO.ID) != true
                                              select _userDefinedTemplateDTO).ToList();

            if (_unselectedUserDefinedList.AsQueryable().Count() > 0)
            {
                foreach (var _userDefinedTemplateDTO in _unselectedUserDefinedList)
                {
                    DeleteUserDefinedTemplate_Global(_userDefinedTemplateDTO);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ValidationResultDTO;
    }
    public static List<UserDefinedTemplateDTO> GetUserDefinedTemplateList_Global(UserDefinedTemplateDTO UserDefinedTemplateDTO, PagedResultDTO<UserDefinedTemplateDTO> PagedResultDTO = null)
    {
        var _userdefinedtemplateglobalList = new List<UserDefinedTemplateDTO>();
        try
        {
            var _userdefinedtemplateList = UserDefinedTemplate_Repository.GetUserDefinedTemplateList(UserDefinedTemplateDTO, PagedResultDTO);
            // if UserDefinedTemplate is empty, return list
            if (_userdefinedtemplateList.Count() == 0)
            {
                _userdefinedtemplateglobalList = _userdefinedtemplateList;
                return _userdefinedtemplateglobalList;
            }
            if (!UserDefinedTemplateDTO.GetItem_SupportGroupDTO && !UserDefinedTemplateDTO.GetUserDefinedDTO)
            {
                _userdefinedtemplateglobalList = _userdefinedtemplateList;
                return _userdefinedtemplateglobalList;
            }
            _userdefinedtemplateglobalList = GetUserDefinedTemplateRelatedData(UserDefinedTemplateDTO, _userdefinedtemplateList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedtemplateglobalList;
    }



    public static List<UserDefinedTemplateDTO> GetUserDefinedTemplateRelatedData(UserDefinedTemplateDTO UserDefinedTemplateDTO, List<UserDefinedTemplateDTO> UserDefinedTemplateList)
    {
        var _userdefinedtemplateglobalList = new List<UserDefinedTemplateDTO>();
        var _userdefinedDict = new Dictionary<int?, UserDefinedDTO>();
        var _item_supportgroupDict = new Dictionary<int?, Item_SupportGroupDTO>();
        try
        {

            if (UserDefinedTemplateDTO.GetItem_SupportGroupDTO)
            {
                UserDefinedTemplateDTO.Item_SupportGroupDTO.Item_SupportGroupIDArray = UserDefinedTemplateList.GroupBy(g => g.Item_SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_supportgroupDict = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(UserDefinedTemplateDTO.Item_SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (UserDefinedTemplateDTO.GetUserDefinedDTO)
            {
                UserDefinedTemplateDTO.UserDefinedDTO.UserDefinedIDArray = UserDefinedTemplateList.GroupBy(g => g.UserDefinedDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _userdefinedDict = UserDefined_Service.GetUserDefinedList_Global(UserDefinedTemplateDTO.UserDefinedDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _userdefinedtemplateDTO in UserDefinedTemplateList)
            {

                if (UserDefinedTemplateDTO.GetItem_SupportGroupDTO && _item_supportgroupDict.ContainsKey(_userdefinedtemplateDTO.Item_SupportGroupDTO.ID))
                {
                    _userdefinedtemplateDTO.Item_SupportGroupDTO = _item_supportgroupDict[_userdefinedtemplateDTO.Item_SupportGroupDTO.ID];
                }
                if (UserDefinedTemplateDTO.GetUserDefinedDTO && _userdefinedDict.ContainsKey(_userdefinedtemplateDTO.UserDefinedDTO.ID))
                {
                    _userdefinedtemplateDTO.UserDefinedDTO = _userdefinedDict[_userdefinedtemplateDTO.UserDefinedDTO.ID];
                }
                _userdefinedtemplateglobalList.Add(_userdefinedtemplateDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedtemplateglobalList;
    }




    public static int GetUserDefinedTemplateTotalCount(PagedResultDTO<UserDefinedTemplateDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = UserDefinedTemplate_Repository.GetUserDefinedTemplateCount(PagedResultDTO.Filter, PagedResultDTO);
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
