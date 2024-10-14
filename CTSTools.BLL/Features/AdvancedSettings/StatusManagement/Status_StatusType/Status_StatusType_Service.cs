using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.StatusType;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;

public class Status_StatusType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateStatus_StatusType_Global(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = Status_StatusType_Validator.CreateStatus_StatusType_Validation(Status_StatusTypeDTO);
        if (_validationResultDTO.Result)
        {
            Status_StatusTypeDTO.AddedDate = DateTime.Now;
            _validationResultDTO = Status_StatusType_Repository.CreateStatus_StatusType(Status_StatusTypeDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateStatus_StatusType_Global(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = Status_StatusType_Validator.UpdateStatus_StatusType_Validation(Status_StatusTypeDTO);
        if (_validationResultDTO.Result)
        {
            Status_StatusTypeDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Status_StatusType_Repository.UpdateStatus_StatusType(Status_StatusTypeDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteStatus_StatusType_Global(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = Status_StatusType_Validator.DeleteStatus_StatusType_Validation(Status_StatusTypeDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Status_StatusType_Repository.DeleteStatus_StatusType(Status_StatusTypeDTO);
        }
        return _validationResultDTO;
    }
    public static List<Status_StatusTypeDTO> GetStatus_StatusTypeList_Global(Status_StatusTypeDTO Status_StatusTypeDTO, PagedResultDTO<Status_StatusTypeDTO> PagedResultDTO = null)
    {
        var _status_statustypeglobalList = new List<Status_StatusTypeDTO>();
        try
        {
            var _status_statustypeList = Status_StatusType_Repository.GetStatus_StatusTypeList(Status_StatusTypeDTO,PagedResultDTO);
            
            if(_status_statustypeList.Count() == 0)
            {
                _status_statustypeglobalList = _status_statustypeList;
                return _status_statustypeglobalList;
            }

            if(!Status_StatusTypeDTO.GetStatusDTO && !Status_StatusTypeDTO.GetStatusTypeDTO)
            {
                _status_statustypeglobalList = _status_statustypeList;
                return _status_statustypeglobalList;
            }
            //Get relational DTO's
            _status_statustypeglobalList = GetStatus_StatusTypeRelatedData(Status_StatusTypeDTO, _status_statustypeList);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _status_statustypeglobalList;
    }

    public static List<Status_StatusTypeDTO> GetStatus_StatusTypeRelatedData(Status_StatusTypeDTO Status_StatusTypeDTO, List<Status_StatusTypeDTO> Status_StatusTypeList )
    {
        var _status_statustypeglobalList = new List<Status_StatusTypeDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _statusTypeDict = new Dictionary<int?, StatusTypeDTO>();
        try
        {
            if (Status_StatusTypeDTO.GetStatusDTO)
            {
                Status_StatusTypeDTO.StatusDTO.StatusIDArray = Status_StatusTypeList.GroupBy(g => g.StatusDTO.ID)
                    .Select(s => s.Key)
                    .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(Status_StatusTypeDTO.StatusDTO)
                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }

            if (Status_StatusTypeDTO.GetStatusTypeDTO)
            {
                Status_StatusTypeDTO.StatusTypeDTO.StatusTypeIDArray = Status_StatusTypeList.GroupBy(g => g.StatusTypeDTO.ID)
                    .Select(s => s.Key)
                    .ToArray();

                _statusTypeDict = StatusType_Service.GetStatusTypeList_Global(Status_StatusTypeDTO.StatusTypeDTO)
                    .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }

            foreach(var _status_statustypeDTO in Status_StatusTypeList)
            {
                if(Status_StatusTypeDTO.GetStatusDTO && _statusDict.ContainsKey(_status_statustypeDTO.StatusDTO.ID))
                {
                    _status_statustypeDTO.StatusDTO = _statusDict[_status_statustypeDTO.StatusDTO.ID];
                }

                if (Status_StatusTypeDTO.GetStatusTypeDTO && _statusTypeDict.ContainsKey(_status_statustypeDTO.StatusTypeDTO.ID))
                {
                    _status_statustypeDTO.StatusTypeDTO = _statusTypeDict[_status_statustypeDTO.StatusTypeDTO.ID];
                }

                _status_statustypeglobalList.Add(_status_statustypeDTO);
            }


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _status_statustypeglobalList;
    }

    public static int GetTotalCount(PagedResultDTO<Status_StatusTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =Status_StatusType_Repository.GetStatus_StatusTypeCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }

    #endregion
}
