using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Provider;

public class Provider_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateProvider_Global(ProviderDTO ProviderDTO)
    {
        var _ValidationResultDTO = Provider_Validator.CreateProvider_Validation(ProviderDTO);
        if (_ValidationResultDTO.Result)
        {
            ProviderDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Provider_Repository.CreateProvider(ProviderDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionCreate<ProviderDTO>(ProviderDTO, (int)ProviderDTO.AddedByID, (int)ProviderDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateProvider_Global(ProviderDTO ProviderDTO)
    {
        var _ValidationResultDTO = Provider_Validator.UpdateProvider_Validation(ProviderDTO);
        var _previousProviderDTO = GetProviderList_Global(new ProviderDTO { ID = ProviderDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            ProviderDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Provider_Repository.UpdateProvider(ProviderDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionUpdate<ProviderDTO>(_previousProviderDTO, ProviderDTO, (int)ProviderDTO.LastUpdateByID, (int)ProviderDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteProvider_Global(ProviderDTO ProviderDTO)
    {
        var _ValidationResultDTO = Provider_Validator.DeleteProvider_Validation(ProviderDTO);
        var _previousProviderDTO = GetProviderList_Global(new ProviderDTO { ID = ProviderDTO.ID }).FirstOrDefault();
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Provider_Repository.DeleteProvider(ProviderDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            ChangeLog.ChangeLog_Service.BuildChangeLogActionDelete<ProviderDTO>(_previousProviderDTO, (int)ProviderDTO.LastUpdateByID, (int)ProviderDTO.ID);
        }
        return _ValidationResultDTO;
    }
    public static List<ProviderDTO> GetProviderList_Global(ProviderDTO ProviderDTO, PagedResultDTO<ProviderDTO> PagedResultDTO = null)
    {
        var _providerglobalList = new List<ProviderDTO>();
        try
        {
            var _providerList = Provider_Repository.GetProviderList(ProviderDTO, PagedResultDTO);
            // if Provider is empty, return list
            _providerglobalList = _providerList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _providerglobalList;
    }


    public static int GetProviderTotalCount(PagedResultDTO<ProviderDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Provider_Repository.GetProviderCount(PagedResultDTO.Filter, PagedResultDTO);
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
