using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;

public class UnitOfMeasure_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateUnitOfMeasure_Global(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _ValidationResultDTO = UnitOfMeasure_Validator.CreateUnitOfMeasure_Validation(UnitOfMeasureDTO);
        if (_ValidationResultDTO.Result)
        {
            UnitOfMeasureDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = UnitOfMeasure_Repository.CreateUnitOfMeasure(UnitOfMeasureDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateUnitOfMeasure_Global(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _ValidationResultDTO = UnitOfMeasure_Validator.UpdateUnitOfMeasure_Validation(UnitOfMeasureDTO);
        if (_ValidationResultDTO.Result)
        {
            UnitOfMeasureDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = UnitOfMeasure_Repository.UpdateUnitOfMeasure(UnitOfMeasureDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteUnitOfMeasure_Global(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _ValidationResultDTO = UnitOfMeasure_Validator.DeleteUnitOfMeasure_Validation(UnitOfMeasureDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = UnitOfMeasure_Repository.DeleteUnitOfMeasure(UnitOfMeasureDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<UnitOfMeasureDTO> GetUnitOfMeasureList_Global(UnitOfMeasureDTO UnitOfMeasureDTO, PagedResultDTO<UnitOfMeasureDTO> PagedResultDTO = null)
    {
        var _unitofmeasureglobalList = new List<UnitOfMeasureDTO>();
        try
        {
            var _unitofmeasureList = UnitOfMeasure_Repository.GetUnitOfMeasureList(UnitOfMeasureDTO, PagedResultDTO);
            // if UnitOfMeasure is empty, return list
            _unitofmeasureglobalList = _unitofmeasureList;


        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _unitofmeasureglobalList;
    }


    public static int GetUnitOfMeasureTotalCount(PagedResultDTO<UnitOfMeasureDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = UnitOfMeasure_Repository.GetUnitOfMeasureCount(PagedResultDTO.Filter, PagedResultDTO);
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
