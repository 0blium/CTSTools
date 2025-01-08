using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Level;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;

public class Dashboard_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDashboard_Global(DashboardDTO DashboardDTO)
    {
        //test comment
        var _ValidationResultDTO = Dashboard_Validator.CreateDashboard_Validation(DashboardDTO);
        if (_ValidationResultDTO.Result)
        {
            DashboardDTO.AddedDate = DateTime.Now;
            DashboardDTO.Year = DashboardDTO.Year == null || DashboardDTO.Year == 0 ? DateTime.Now.Year : DashboardDTO.Year;
            _ValidationResultDTO = Dashboard_Repository.CreateDashboard(DashboardDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDashboard_Global(DashboardDTO DashboardDTO)
    {
        var _ValidationResultDTO = Dashboard_Validator.UpdateDashboard_Validation(DashboardDTO);
        if (_ValidationResultDTO.Result)
        {
            DashboardDTO.LastUpdate = DateTime.Now;
            DashboardDTO.Year = DashboardDTO.Year == null || DashboardDTO.Year == 0 ? DateTime.Now.Year : DashboardDTO.Year;
            _ValidationResultDTO = Dashboard_Repository.UpdateDashboard(DashboardDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDashboard_Global(DashboardDTO DashboardDTO)
    {
        var _ValidationResultDTO = Dashboard_Validator.DeleteDashboard_Validation(DashboardDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Dashboard_Repository.DeleteDashboard(DashboardDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DashboardDTO> GetDashboardList_Global(DashboardDTO DashboardDTO, PagedResultDTO<DashboardDTO> PagedResultDTO = null)
    {
        var _dashboardglobalList = new List<DashboardDTO>();
        try
        {
            var _dashboardList = Dashboard_Repository.GetDashboardList(DashboardDTO, PagedResultDTO);
            // if Dashboard is empty, return list
            if (_dashboardList.Count() == 0)
            {
                _dashboardglobalList = _dashboardList;
                return _dashboardglobalList;
            }
            if (!DashboardDTO.GetDepartmentDTO && !DashboardDTO.GetLevelDTO && !DashboardDTO.GetStatusDTO)
            {
                _dashboardglobalList = _dashboardList;
                return _dashboardglobalList;
            }
            _dashboardglobalList = GetDashboardRelatedData(DashboardDTO, _dashboardList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardglobalList;
    }



    public static List<DashboardDTO> GetDashboardRelatedData(DashboardDTO DashboardDTO, List<DashboardDTO> DashboardList)
    {
        var _dashboardglobalList = new List<DashboardDTO>();
        var _departmentDict = new Dictionary<int?, DepartmentDTO>();
        var _levelDict = new Dictionary<int?, LevelDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();

        try
        {
            if (DashboardDTO.GetDepartmentDTO)
            {
                DashboardDTO.DepartmentDTO.DepartmentIDArray = DashboardList.GroupBy(g => g.DepartmentID)
                        .Select(s => s.Key)
                        .ToArray();

                _departmentDict = Department_Service.GetDepartmentList_Global(DashboardDTO.DepartmentDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardDTO.GetLevelDTO)
            {
                DashboardDTO.LevelDTO.LevelIDArray = DashboardList.GroupBy(g => g.LevelID)
                        .Select(s => s.Key)
                        .ToArray();

                _levelDict = Level_Service.GetLevelList_Global(DashboardDTO.LevelDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (DashboardDTO.GetStatusDTO)
            {
                DashboardDTO.StatusDTO.StatusIDArray = DashboardList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(DashboardDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _dashboardDTO in DashboardList)
            {
                if (DashboardDTO.GetDepartmentDTO && _departmentDict.ContainsKey(_dashboardDTO.DepartmentID))
                {
                    _dashboardDTO.DepartmentDTO = _departmentDict[_dashboardDTO.DepartmentID];
                }
                if (DashboardDTO.GetLevelDTO && _levelDict.ContainsKey(_dashboardDTO.LevelID))
                {
                    _dashboardDTO.LevelDTO = _levelDict[_dashboardDTO.LevelID];
                }
                if (DashboardDTO.GetStatusDTO && _statusDict.ContainsKey(_dashboardDTO.StatusID))
                {
                    _dashboardDTO.StatusDTO = _statusDict[_dashboardDTO.StatusID];
                }
                _dashboardglobalList.Add(_dashboardDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardglobalList;
    }




    public static int GetDashboardTotalCount(PagedResultDTO<DashboardDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Dashboard_Repository.GetDashboardCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
