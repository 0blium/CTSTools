using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Priority;

public class Priority_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreatePriority_Global(PriorityDTO PriorityDTO)
    {
        var _ValidationResultDTO = Priority_Validator.CreatePriority_Validation(PriorityDTO);
        if (_ValidationResultDTO.Result)
        {
            PriorityDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Priority_Repository.CreatePriority(PriorityDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<PriorityDTO>(PriorityDTO, (int)PriorityDTO.AddedByID, (int)PriorityDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdatePriority_Global(PriorityDTO PriorityDTO)
    {
        var _ValidationResultDTO = Priority_Validator.UpdatePriority_Validation(PriorityDTO);
        var _previousPriorityDTO = GetPriorityList_Global(new PriorityDTO { ID = PriorityDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            PriorityDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Priority_Repository.UpdatePriority(PriorityDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<PriorityDTO>(_previousPriorityDTO, PriorityDTO, (int)PriorityDTO.LastUpdateByID, (int)PriorityDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeletePriority_Global(PriorityDTO PriorityDTO)
    {
        var _ValidationResultDTO = Priority_Validator.DeletePriority_Validation(PriorityDTO);
        var _previousPriorityDTO = GetPriorityList_Global(new PriorityDTO { ID = PriorityDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Priority_Repository.DeletePriority(PriorityDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<PriorityDTO>(_previousPriorityDTO, (int)PriorityDTO.LastUpdateByID, (int)PriorityDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<PriorityDTO> GetPriorityList_Global(PriorityDTO PriorityDTO, PagedResultDTO<PriorityDTO> PagedResultDTO = null)
    {
        var _priorityglobalList = new List<PriorityDTO>();
        try
        {
            var _priorityList = Priority_Repository.GetPriorityList(PriorityDTO, PagedResultDTO);
            // if Priority is empty, return list
            if (_priorityList.Count() == 0)
            {
                _priorityglobalList = _priorityList;
                return _priorityglobalList;
            }
            if (!PriorityDTO.GetSupportGroupDTO)
            {
                _priorityglobalList = _priorityList;
                return _priorityglobalList;
            }
            _priorityglobalList = GetPriorityRelatedData(PriorityDTO, _priorityList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _priorityglobalList;
    }



    public static List<PriorityDTO> GetPriorityRelatedData(PriorityDTO PriorityDTO, List<PriorityDTO> PriorityList)
    {
        var _priorityglobalList = new List<PriorityDTO>();
        var _supportgroupDict = new Dictionary<int?, SupportGroupDTO>();

        try
        {
            if (PriorityDTO.GetSupportGroupDTO)
            {
                PriorityDTO.SupportGroupDTO.SupportGroupIDArray = PriorityList.GroupBy(g => g.SupportGroupID)
                        .Select(s => s.Key)
                        .ToArray();

                _supportgroupDict = SupportGroup_Service.GetSupportGroupList_Global(PriorityDTO.SupportGroupDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _priorityDTO in PriorityList)
            {
                if (PriorityDTO.GetSupportGroupDTO && _supportgroupDict.ContainsKey(_priorityDTO.SupportGroupID))
                {
                    _priorityDTO.SupportGroupDTO = _supportgroupDict[_priorityDTO.SupportGroupID];
                }
                _priorityglobalList.Add(_priorityDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _priorityglobalList;
    }




    public static int GetPriorityTotalCount(PagedResultDTO<PriorityDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Priority_Repository.GetPriorityCount(PagedResultDTO.Filter, PagedResultDTO);
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
