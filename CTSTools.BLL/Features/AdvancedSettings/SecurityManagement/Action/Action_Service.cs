using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;


namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;

public class Action_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateAction_Global(ActionDTO ActionDTO)
    {
        var _ValidationResultDTO = Action_Validator.CreateAction_Validation(ActionDTO);
        if (_ValidationResultDTO.Result)
        {
            ActionDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Action_Repository.CreateAction(ActionDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateAction_Global(ActionDTO ActionDTO)
    {
        var _ValidationResultDTO = Action_Validator.UpdateAction_Validation(ActionDTO);
        if (_ValidationResultDTO.Result)
        {
            ActionDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Action_Repository.UpdateAction(ActionDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteAction_Global(ActionDTO ActionDTO)
    {
        var _ValidationResultDTO = Action_Validator.DeleteAction_Validation(ActionDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Action_Repository.DeleteAction(ActionDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ActionDTO> GetActionList_Global(ActionDTO ActionDTO,PagedResultDTO<ActionDTO> PagedResultDTO = null)
    {
        var _actionglobalList = new List<ActionDTO>();
        try
        {
            var _actionList = Action_Repository.GetActionList(ActionDTO,PagedResultDTO);
            // if Action is empty, return list
            _actionglobalList = _actionList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _actionglobalList;
    }


     public static int GetActionTotalCount(PagedResultDTO<ActionDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =Action_Repository.GetActionCount(PagedResultDTO.Filter, PagedResultDTO);
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
