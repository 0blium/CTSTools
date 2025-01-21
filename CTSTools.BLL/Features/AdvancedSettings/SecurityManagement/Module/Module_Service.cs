using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;

public class Module_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateModule_Global(ModuleDTO ModuleDTO)
    {
        var _ValidationResultDTO = Module_Validator.CreateModule_Validation(ModuleDTO);
        if (_ValidationResultDTO.Result)
        {
            ModuleDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Module_Repository.CreateModule(ModuleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateModule_Global(ModuleDTO ModuleDTO)
    {
        var _ValidationResultDTO = Module_Validator.UpdateModule_Validation(ModuleDTO);
        if (_ValidationResultDTO.Result)
        {
            ModuleDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Module_Repository.UpdateModule(ModuleDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteModule_Global(ModuleDTO ModuleDTO)
    {
        var _ValidationResultDTO = Module_Validator.DeleteModule_Validation(ModuleDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Module_Repository.DeleteModule(ModuleDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ModuleDTO> GetModuleList_Global(ModuleDTO ModuleDTO, PagedResultDTO<ModuleDTO> PagedResultDTO = null)
    {
        var _ModuleglobalList = new List<ModuleDTO>();
        try
        {
            var _ModuleList = Module_Repository.GetModuleList(ModuleDTO, PagedResultDTO);
            // if Module is empty, return list
            _ModuleglobalList = _ModuleList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ModuleglobalList;
    }


    public static int GetModuleTotalCount(PagedResultDTO<ModuleDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Module_Repository.GetModuleCount(PagedResultDTO.Filter, PagedResultDTO);
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
