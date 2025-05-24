using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.DataType;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;

public class UserDefined_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUserDefined_Global(UserDefinedDTO UserDefinedDTO)
    {
        var _ValidationResultDTO = UserDefined_Validator.CreateUserDefined_Validation(UserDefinedDTO);
        if (_ValidationResultDTO.Result)
        {
            UserDefinedDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = UserDefined_Repository.CreateUserDefined(UserDefinedDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateUserDefined_Global(UserDefinedDTO UserDefinedDTO)
    {
        var _ValidationResultDTO = UserDefined_Validator.UpdateUserDefined_Validation(UserDefinedDTO);
        if (_ValidationResultDTO.Result)
        {
            UserDefinedDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = UserDefined_Repository.UpdateUserDefined(UserDefinedDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteUserDefined_Global(UserDefinedDTO UserDefinedDTO)
    {
        var _ValidationResultDTO = UserDefined_Validator.DeleteUserDefined_Validation(UserDefinedDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = UserDefined_Repository.DeleteUserDefined(UserDefinedDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<UserDefinedDTO> GetUserDefinedList_Global(UserDefinedDTO UserDefinedDTO, PagedResultDTO<UserDefinedDTO> PagedResultDTO = null)
    {
        var _userdefinedglobalList = new List<UserDefinedDTO>();
        try
        {
            var _userdefinedList = UserDefined_Repository.GetUserDefinedList(UserDefinedDTO, PagedResultDTO);
            // if UserDefined is empty, return list
            if (_userdefinedList.Count() == 0)
            {
                _userdefinedglobalList = _userdefinedList;
                return _userdefinedglobalList;
            }
            if (!UserDefinedDTO.GetSupportGroupDTO && !UserDefinedDTO.GetDataTypeDTO)
            {
                _userdefinedglobalList = _userdefinedList;
                return _userdefinedglobalList;
            }
            _userdefinedglobalList = GetUserDefinedRelatedData(UserDefinedDTO, _userdefinedList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedglobalList;
    }



    public static List<UserDefinedDTO> GetUserDefinedRelatedData(UserDefinedDTO UserDefinedDTO, List<UserDefinedDTO> UserDefinedList)
    {
        var _userdefinedglobalList = new List<UserDefinedDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();
        var _datatypeDict = new Dictionary<int?, DataTypeDTO>();

        try
        {
            if (UserDefinedDTO.GetSupportGroupDTO)
            {
                UserDefinedDTO.SupportGroupDTO.SupportGroupIDArray = UserDefinedList.GroupBy(g => g.SupportGroupID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(UserDefinedDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (UserDefinedDTO.GetDataTypeDTO)
            {
                UserDefinedDTO.DataTypeDTO.DataTypeIDArray = UserDefinedList.GroupBy(g => g.DataTypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _datatypeDict = DataType_Service.GetDataTypeList_Global(UserDefinedDTO.DataTypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _userdefinedDTO in UserDefinedList)
            {
                if (UserDefinedDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_userdefinedDTO.SupportGroupID))
                {
                    _userdefinedDTO.SupportGroupDTO = _supportgroupDict[_userdefinedDTO.SupportGroupID];
                }
                if (UserDefinedDTO.GetDataTypeDTO && _datatypeDict.ContainsKey(_userdefinedDTO.DataTypeID))
                {
                    _userdefinedDTO.DataTypeDTO = _datatypeDict[_userdefinedDTO.DataTypeID];
                }
                _userdefinedglobalList.Add(_userdefinedDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedglobalList;
    }




    public static int GetUserDefinedTotalCount(PagedResultDTO<UserDefinedDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = UserDefined_Repository.GetUserDefinedCount(PagedResultDTO.Filter, PagedResultDTO);
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
