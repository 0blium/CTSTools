using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Station.StationType;

public class StationType_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateStationType_Global(StationTypeDTO StationTypeDTO)
    {
        var _ValidationResultDTO = StationType_Validator.CreateStationType_Validation(StationTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            StationTypeDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = StationType_Repository.CreateStationType(StationTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateStationType_Global(StationTypeDTO StationTypeDTO)
    {
        var _ValidationResultDTO = StationType_Validator.UpdateStationType_Validation(StationTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            StationTypeDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = StationType_Repository.UpdateStationType(StationTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteStationType_Global(StationTypeDTO StationTypeDTO)
    {
        var _ValidationResultDTO = StationType_Validator.DeleteStationType_Validation(StationTypeDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = StationType_Repository.DeleteStationType(StationTypeDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<StationTypeDTO> GetStationTypeList_Global(StationTypeDTO StationTypeDTO, PagedResultDTO<StationTypeDTO> PagedResultDTO = null)
    {
        var _stationtypeglobalList = new List<StationTypeDTO>();
        try
        {
            var _stationtypeList = StationType_Repository.GetStationTypeList(StationTypeDTO, PagedResultDTO);
            // if StationType is empty, return list
            _stationtypeglobalList = _stationtypeList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _stationtypeglobalList;
    }


    public static int GetStationTypeTotalCount(PagedResultDTO<StationTypeDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = StationType_Repository.GetStationTypeCount(PagedResultDTO.Filter, PagedResultDTO);
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
