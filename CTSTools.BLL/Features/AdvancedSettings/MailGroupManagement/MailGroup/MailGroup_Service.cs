using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;

public class MailGroup_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateMailGroup_Global(MailGroupDTO MailGroupDTO)
    {
        var _ValidationResultDTO = MailGroup_Validator.CreateMailGroup_Validation(MailGroupDTO);
        if (_ValidationResultDTO.Result)
        {
            MailGroupDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = MailGroup_Repository.CreateMailGroup(MailGroupDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateMailGroup_Global(MailGroupDTO MailGroupDTO)
    {
        var _ValidationResultDTO = MailGroup_Validator.UpdateMailGroup_Validation(MailGroupDTO);
        if (_ValidationResultDTO.Result)
        {
            MailGroupDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = MailGroup_Repository.UpdateMailGroup(MailGroupDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteMailGroup_Global(MailGroupDTO MailGroupDTO)
    {
        var _ValidationResultDTO = MailGroup_Validator.DeleteMailGroup_Validation(MailGroupDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = MailGroup_Repository.DeleteMailGroup(MailGroupDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<MailGroupDTO> GetMailGroupList_Global(MailGroupDTO MailGroupDTO,PagedResultDTO<MailGroupDTO> PagedResultDTO = null)
    {
        var _mailgroupglobalList = new List<MailGroupDTO>();
        try
        {
            var _mailgroupList = MailGroup_Repository.GetMailGroupList(MailGroupDTO,PagedResultDTO);
            // if MailGroup is empty, return list
            _mailgroupglobalList = _mailgroupList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _mailgroupglobalList;
    }



     public static int GetMailGroupTotalCount(PagedResultDTO<MailGroupDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =MailGroup_Repository.GetMailGroupCount(PagedResultDTO.Filter, PagedResultDTO);
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
