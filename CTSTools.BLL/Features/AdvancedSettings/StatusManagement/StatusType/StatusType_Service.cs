using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.StatusType;

public class StatusType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateStatusType_Global(StatusTypeDTO StatusTypeDTO)
    {
        var _validationResultDTO = StatusType_Validator.CreateStatusType_Validation(StatusTypeDTO);
        if (_validationResultDTO.Result)
        {
            StatusTypeDTO.AddedDate = DateTime.Now;
            _validationResultDTO = StatusType_Repository.CreateStatusType(StatusTypeDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateStatusType_Global(StatusTypeDTO StatusTypeDTO)
    {
        var _validationResultDTO = StatusType_Validator.UpdateStatusType_Validation(StatusTypeDTO);
        if (_validationResultDTO.Result)
        {
            StatusTypeDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = StatusType_Repository.UpdateStatusType(StatusTypeDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteStatusType_Global(StatusTypeDTO StatusTypeDTO)
    {
        var _validationResultDTO = StatusType_Validator.DeleteStatusType_Validation(StatusTypeDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = StatusType_Repository.DeleteStatusType(StatusTypeDTO);
        }
        return _validationResultDTO;
    }
    public static List<StatusTypeDTO> GetStatusTypeList_Global(StatusTypeDTO StatusTypeDTO, PagedResultDTO<StatusTypeDTO> PagedResultDTO = null)
    {
        var _statustypeglobalList = new List<StatusTypeDTO>();
        try
        {
            var _statustypeList = StatusType_Repository.GetStatusTypeList(StatusTypeDTO, PagedResultDTO);
            _statustypeglobalList = _statustypeList;
            return _statustypeglobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _statustypeglobalList;
    }

    public static int GetTotalCount(PagedResultDTO<StatusTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =StatusType_Repository.GetStatusTypeCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion
}
