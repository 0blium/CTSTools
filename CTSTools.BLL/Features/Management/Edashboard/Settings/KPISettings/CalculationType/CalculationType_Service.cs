using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.CalculationType;

public class CalculationType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateCalculationType_Global(CalculationTypeDTO CalculationTypeDTO)
    {
        var _ValidationResultDTO = CalculationType_Validator.CreateCalculationType_Validation(CalculationTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            CalculationTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = CalculationType_Repository.CreateCalculationType(CalculationTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateCalculationType_Global(CalculationTypeDTO CalculationTypeDTO)
    {
        var _ValidationResultDTO = CalculationType_Validator.UpdateCalculationType_Validation(CalculationTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            CalculationTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = CalculationType_Repository.UpdateCalculationType(CalculationTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteCalculationType_Global(CalculationTypeDTO CalculationTypeDTO)
    {
        var _ValidationResultDTO = CalculationType_Validator.DeleteCalculationType_Validation(CalculationTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = CalculationType_Repository.DeleteCalculationType(CalculationTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<CalculationTypeDTO> GetCalculationTypeList_Global(CalculationTypeDTO CalculationTypeDTO, PagedResultDTO<CalculationTypeDTO> PagedResultDTO = null)
    {
        var _calculationtypeglobalList = new List<CalculationTypeDTO>();
        try
        {
            var _calculationtypeList = CalculationType_Repository.GetCalculationTypeList(CalculationTypeDTO, PagedResultDTO);
            // if CalculationType is empty, return list
            _calculationtypeglobalList = _calculationtypeList;


        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _calculationtypeglobalList;
    }



    public static int GetCalculationTypeTotalCount(PagedResultDTO<CalculationTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = CalculationType_Repository.GetCalculationTypeCount(PagedResultDTO.Filter, PagedResultDTO);
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
