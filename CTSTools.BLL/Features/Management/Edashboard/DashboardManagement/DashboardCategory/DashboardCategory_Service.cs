using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;

public class DashboardCategory_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDashboardCategory_Global(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _ValidationResultDTO = DashboardCategory_Validator.CreateDashboardCategory_Validation(DashboardCategoryDTO);
        if (_ValidationResultDTO.Result)
        {
            DashboardCategoryDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = DashboardCategory_Repository.CreateDashboardCategory(DashboardCategoryDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateDashboardCategory_Global(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _ValidationResultDTO = DashboardCategory_Validator.UpdateDashboardCategory_Validation(DashboardCategoryDTO);
        if (_ValidationResultDTO.Result)
        {
            DashboardCategoryDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = DashboardCategory_Repository.UpdateDashboardCategory(DashboardCategoryDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteDashboardCategory_Global(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _ValidationResultDTO = DashboardCategory_Validator.DeleteDashboardCategory_Validation(DashboardCategoryDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = DashboardCategory_Repository.DeleteDashboardCategory(DashboardCategoryDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<DashboardCategoryDTO> GetDashboardCategoryList_Global(DashboardCategoryDTO DashboardCategoryDTO, PagedResultDTO<DashboardCategoryDTO> PagedResultDTO = null)
    {
        var _dashboardcategoryglobalList = new List<DashboardCategoryDTO>();
        try
        {
            var _dashboardcategoryList = DashboardCategory_Repository.GetDashboardCategoryList(DashboardCategoryDTO, PagedResultDTO);
            // if DashboardCategory is empty, return list
            _dashboardcategoryglobalList = _dashboardcategoryList;


        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _dashboardcategoryglobalList;
    }





    public static int GetDashboardCategoryTotalCount(PagedResultDTO<DashboardCategoryDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = DashboardCategory_Repository.GetDashboardCategoryCount(PagedResultDTO.Filter, PagedResultDTO);
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
