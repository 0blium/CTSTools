using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Level;

public class Level_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateLevel_Global(LevelDTO LevelDTO)
    {
        var _ValidationResultDTO = Level_Validator.CreateLevel_Validation(LevelDTO);
        if (_ValidationResultDTO.Result)
        {
            LevelDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Level_Repository.CreateLevel(LevelDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateLevel_Global(LevelDTO LevelDTO)
    {
        var _ValidationResultDTO = Level_Validator.UpdateLevel_Validation(LevelDTO);
        if (_ValidationResultDTO.Result)
        {
            LevelDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Level_Repository.UpdateLevel(LevelDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteLevel_Global(LevelDTO LevelDTO)
    {
        var _ValidationResultDTO = Level_Validator.DeleteLevel_Validation(LevelDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Level_Repository.DeleteLevel(LevelDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<LevelDTO> GetLevelList_Global(LevelDTO LevelDTO, PagedResultDTO<LevelDTO> PagedResultDTO = null)
    {
        var _levelglobalList = new List<LevelDTO>();
        try
        {
            var _levelList = Level_Repository.GetLevelList(LevelDTO, PagedResultDTO);
            // if Level is empty, return list
            _levelglobalList = _levelList;


        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _levelglobalList;
    }




    public static int GetLevelTotalCount(PagedResultDTO<LevelDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Level_Repository.GetLevelCount(PagedResultDTO.Filter, PagedResultDTO);
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
