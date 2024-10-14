using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;

public class Status_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateStatus_Global(StatusDTO StatusDTO)
    {
        var _validationResultDTO = Status_Validator.CreateStatus_Validation(StatusDTO);
        if (_validationResultDTO.Result)
        {
            StatusDTO.AddedDate = DateTime.Now;
            _validationResultDTO = Status_Repository.CreateStatus(StatusDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateStatus_Global(StatusDTO StatusDTO)
    {
        var _validationResultDTO = Status_Validator.UpdateStatus_Validation(StatusDTO);
        if (_validationResultDTO.Result)
        {
            StatusDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Status_Repository.UpdateStatus(StatusDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteStatus_Global(StatusDTO StatusDTO)
    {
        var _validationResultDTO = Status_Validator.DeleteStatus_Validation(StatusDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Status_Repository.DeleteStatus(StatusDTO);
        }
        return _validationResultDTO;
    }
    public static List<StatusDTO> GetStatusList_Global(StatusDTO StatusDTO,PagedResultDTO<StatusDTO> PagedStatusDTO = null)
    {
        var _statusglobalList = new List<StatusDTO>();
        try
        {
            var _statusList = Status_Repository.GetStatusList(StatusDTO,PagedStatusDTO);
            _statusglobalList = _statusList;
            return _statusglobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _statusglobalList;
    }

    public static int GetTotalCount(PagedResultDTO<StatusDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Status_Repository.GetStatusCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion
}
