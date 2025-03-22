using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;

public class UserDefinedValue_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUserDefinedValue_Global(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _ValidationResultDTO = UserDefinedValue_Validator.CreateUserDefinedValue_Validation(UserDefinedValueDTO);
        if (_ValidationResultDTO.Result)
        {
            UserDefinedValueDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = UserDefinedValue_Repository.CreateUserDefinedValue(UserDefinedValueDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateUserDefinedValue_Global(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _ValidationResultDTO = UserDefinedValue_Validator.UpdateUserDefinedValue_Validation(UserDefinedValueDTO);
        if (_ValidationResultDTO.Result)
        {
            UserDefinedValueDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = UserDefinedValue_Repository.UpdateUserDefinedValue(UserDefinedValueDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteUserDefinedValue_Global(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _ValidationResultDTO = UserDefinedValue_Validator.DeleteUserDefinedValue_Validation(UserDefinedValueDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = UserDefinedValue_Repository.DeleteUserDefinedValue(UserDefinedValueDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<UserDefinedValueDTO> GetUserDefinedValueList_Global(UserDefinedValueDTO UserDefinedValueDTO, PagedResultDTO<UserDefinedValueDTO> PagedResultDTO = null)
    {
        var _userdefinedvalueglobalList = new List<UserDefinedValueDTO>();
        try
        {
            var _userdefinedvalueList = UserDefinedValue_Repository.GetUserDefinedValueList(UserDefinedValueDTO, PagedResultDTO);
            // if UserDefinedValue is empty, return list
            if (_userdefinedvalueList.Count() == 0)
            {
                _userdefinedvalueglobalList = _userdefinedvalueList;
                return _userdefinedvalueglobalList;
            }
            if (!UserDefinedValueDTO.GetItem_LineDTO && !UserDefinedValueDTO.GetSupportGroupDTO && !UserDefinedValueDTO.GetUserDefinedDTO)
            {
                _userdefinedvalueglobalList = _userdefinedvalueList;
                return _userdefinedvalueglobalList;
            }
            _userdefinedvalueglobalList = GetUserDefinedValueRelatedData(UserDefinedValueDTO, _userdefinedvalueList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedvalueglobalList;
    }



    public static List<UserDefinedValueDTO> GetUserDefinedValueRelatedData(UserDefinedValueDTO UserDefinedValueDTO, List<UserDefinedValueDTO> UserDefinedValueList)
    {
        var _userdefinedvalueglobalList = new List<UserDefinedValueDTO>();
        var _item_lineDict = new Dictionary<int?, Item_LineDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();
        var _userdefinedDict = new Dictionary<int?, UserDefinedDTO>();

        try
        {
            if (UserDefinedValueDTO.GetItem_LineDTO)
            {
                UserDefinedValueDTO.Item_LineDTO.Item_LineIDArray = UserDefinedValueList.GroupBy(g => g.Item_LineDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _item_lineDict = Item_Line_Service.GetItem_LineList_Global(UserDefinedValueDTO.Item_LineDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (UserDefinedValueDTO.GetSupportGroupDTO)
            {
                UserDefinedValueDTO.SupportGroupDTO.SupportGroupIDArray = UserDefinedValueList.GroupBy(g => g.SupportGroupDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(UserDefinedValueDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (UserDefinedValueDTO.GetUserDefinedDTO)
            {
                UserDefinedValueDTO.UserDefinedDTO.UserDefinedIDArray = UserDefinedValueList.GroupBy(g => g.UserDefinedDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _userdefinedDict = UserDefined_Service.GetUserDefinedList_Global(UserDefinedValueDTO.UserDefinedDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _userdefinedvalueDTO in UserDefinedValueList)
            {
                if (UserDefinedValueDTO.GetItem_LineDTO && _item_lineDict.ContainsKey(_userdefinedvalueDTO.Item_LineDTO.ID))
                {
                    _userdefinedvalueDTO.Item_LineDTO = _item_lineDict[_userdefinedvalueDTO.Item_LineDTO.ID];
                }
                if (UserDefinedValueDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_userdefinedvalueDTO.SupportGroupDTO.ID))
                {
                    _userdefinedvalueDTO.SupportGroupDTO = _supportgroupDict[_userdefinedvalueDTO.SupportGroupDTO.ID];
                }
                if (UserDefinedValueDTO.GetUserDefinedDTO && _userdefinedDict.ContainsKey(_userdefinedvalueDTO.UserDefinedDTO.ID))
                {
                    _userdefinedvalueDTO.UserDefinedDTO = _userdefinedDict[_userdefinedvalueDTO.UserDefinedDTO.ID];
                }
                _userdefinedvalueglobalList.Add(_userdefinedvalueDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedvalueglobalList;
    }

    public static int GetUserDefinedValueTotalCount(PagedResultDTO<UserDefinedValueDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = UserDefinedValue_Repository.GetUserDefinedValueCount(PagedResultDTO.Filter, PagedResultDTO);
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
