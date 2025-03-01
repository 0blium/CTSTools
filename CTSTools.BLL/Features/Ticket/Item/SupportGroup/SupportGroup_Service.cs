using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Ticket.Station.Station;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.SupportGroup;

public class SupportGroup_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSupportGroup_Global(SupportGroupDTO SupportGroupDTO)
    {
        var _ValidationResultDTO = SupportGroup_Validator.CreateSupportGroup_Validation(SupportGroupDTO);
        if (_ValidationResultDTO.Result)
        {
            SupportGroupDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = SupportGroup_Repository.CreateSupportGroup(SupportGroupDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<SupportGroupDTO>(SupportGroupDTO, (int)SupportGroupDTO.AddedByID, (int)SupportGroupDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateSupportGroup_Global(SupportGroupDTO SupportGroupDTO)
    {
        var _ValidationResultDTO = SupportGroup_Validator.UpdateSupportGroup_Validation(SupportGroupDTO);
        var _previousSupportGroupDTO = GetSupportGroupList_Global(new SupportGroupDTO { ID = SupportGroupDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            SupportGroupDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = SupportGroup_Repository.UpdateSupportGroup(SupportGroupDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<SupportGroupDTO>(_previousSupportGroupDTO, SupportGroupDTO, (int)SupportGroupDTO.LastUpdateByID, (int)SupportGroupDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteSupportGroup_Global(SupportGroupDTO SupportGroupDTO)
    {
        var _ValidationResultDTO = SupportGroup_Validator.DeleteSupportGroup_Validation(SupportGroupDTO);
        var _previousSupportGroupDTO = GetSupportGroupList_Global(new SupportGroupDTO { ID = SupportGroupDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = SupportGroup_Repository.DeleteSupportGroup(SupportGroupDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<SupportGroupDTO>(SupportGroupDTO, (int)SupportGroupDTO.LastUpdateByID, (int)SupportGroupDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<SupportGroupDTO> GetSupportGroupList_Global(SupportGroupDTO SupportGroupDTO, PagedResultDTO<SupportGroupDTO> PagedResultDTO = null)
    {
        var _supportgroupglobalList = new List<SupportGroupDTO>();
        try
        {
            var _supportgroupList = SupportGroup_Repository.GetSupportGroupList(SupportGroupDTO, PagedResultDTO);
            // if SupportGroup is empty, return list
            if (_supportgroupList.Count() == 0)
            {
                _supportgroupglobalList = _supportgroupList;
                return _supportgroupglobalList;
            }
            if (!SupportGroupDTO.GetFacilityDTO & !SupportGroupDTO.GetStationDTO)
            {
                _supportgroupglobalList = _supportgroupList;
                return _supportgroupglobalList;
            }
            _supportgroupglobalList = GetSupportGroupRelatedData(SupportGroupDTO, _supportgroupList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _supportgroupglobalList;
    }
    public static List<SupportGroupDTO> GetSupportGroupRelatedData(SupportGroupDTO SupportGroupDTO, List<SupportGroupDTO> SupportGroupList)
    {
        var _supportgroupglobalList = new List<SupportGroupDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _stationDict = new Dictionary<int?, StationDTO>();
        try
        {
            if (SupportGroupDTO.GetFacilityDTO)
            {
                SupportGroupDTO.FacilityDTO.FacilityIDArray = SupportGroupList.GroupBy(g => g.FacilityDTO.ID)
                        .Select(s => s.Key)
                        .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(SupportGroupDTO.FacilityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (SupportGroupDTO.GetStationDTO)
            {
                SupportGroupDTO.StationDTO.StationIDArray = SupportGroupList.GroupBy(g => g.StationDTO.ID)
                            .Select(s => s.Key)
                            .ToArray();

                _stationDict = Station_Service.GetStationList_Global(SupportGroupDTO.StationDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _supportgroupDTO in SupportGroupList)
            {
                if (SupportGroupDTO.GetFacilityDTO && _facilityDict.ContainsKey(_supportgroupDTO.FacilityDTO.ID))
                {
                    _supportgroupDTO.FacilityDTO = _facilityDict[_supportgroupDTO.FacilityDTO.ID];
                }
                if (SupportGroupDTO.GetStationDTO && _stationDict.ContainsKey(_supportgroupDTO.StationDTO.ID))
                {
                    _supportgroupDTO.StationDTO = _stationDict[_supportgroupDTO.StationDTO.ID];
                }
                _supportgroupglobalList.Add(_supportgroupDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _supportgroupglobalList;
    }
    public static int GetSupportGroupTotalCount(PagedResultDTO<SupportGroupDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SupportGroup_Repository.GetSupportGroupCount(PagedResultDTO.Filter, PagedResultDTO);
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
    public static List<SupportGroupDTO> GetSupportGroupByUser(UserDTO UserDTO)
    {
        var _supportGroupList = new List<SupportGroupDTO>();
        try
        {
            //Get User DTO
            UserDTO.GetSupportGroupArray = true;
            UserDTO.GetRoleArray = true;

            var _userDTO = User_Service.GetUserList_Global(UserDTO).FirstOrDefault();
            if (_userDTO != null && _userDTO.RoleIDArray.Contains((int)Role_Enum.SystemAdmin))
            {
                _supportGroupList = GetSupportGroupList_Global(new SupportGroupDTO { });
            }
            else
            {
                if (_userDTO != null && _userDTO.SupportGroupIDArray.Count() > 0)
                {
                    _supportGroupList = GetSupportGroupList_Global(new SupportGroupDTO { SupportGroupIDArray = _userDTO.SupportGroupIDArray });
                }
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _supportGroupList;
    }

    #endregion
}
