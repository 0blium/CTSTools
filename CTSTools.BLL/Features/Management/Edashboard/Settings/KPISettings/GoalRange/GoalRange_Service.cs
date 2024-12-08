using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.GoalRange;

public class GoalRange_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateGoalRange_Global(GoalRangeDTO GoalRangeDTO)
    {
        var _ValidationResultDTO = GoalRange_Validator.CreateGoalRange_Validation(GoalRangeDTO);
        if (_ValidationResultDTO.Result)
        {
            GoalRangeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = GoalRange_Repository.CreateGoalRange(GoalRangeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateGoalRange_Global(GoalRangeDTO GoalRangeDTO)
    {
        var _ValidationResultDTO = GoalRange_Validator.UpdateGoalRange_Validation(GoalRangeDTO);
        if (_ValidationResultDTO.Result)
        {
            GoalRangeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = GoalRange_Repository.UpdateGoalRange(GoalRangeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteGoalRange_Global(GoalRangeDTO GoalRangeDTO)
    {
        var _ValidationResultDTO = GoalRange_Validator.DeleteGoalRange_Validation(GoalRangeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = GoalRange_Repository.DeleteGoalRange(GoalRangeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<GoalRangeDTO> GetGoalRangeList_Global(GoalRangeDTO GoalRangeDTO, PagedResultDTO<GoalRangeDTO> PagedResultDTO = null)
    {
        var _goalrangeglobalList = new List<GoalRangeDTO>();
        try
        {
            var _goalrangeList = GoalRange_Repository.GetGoalRangeList(GoalRangeDTO, PagedResultDTO);
            // if GoalRange is empty, return list
            _goalrangeglobalList = _goalrangeList;


        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _goalrangeglobalList;
    }





    public static int GetGoalRangeTotalCount(PagedResultDTO<GoalRangeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = GoalRange_Repository.GetGoalRangeCount(PagedResultDTO.Filter, PagedResultDTO);
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
