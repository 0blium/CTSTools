using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.StationManagement.StationType;
using CTSTools.DAL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;

public class Station_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateStation_Global(StationDTO StationDTO)
    {
        //validate station fields
        var _ValidationResultDTO = Station_Validator.CreateStation_Validation(StationDTO);
        if (_ValidationResultDTO.Result)
        {
            //get station serial from secuence
            _ValidationResultDTO = GetSerialSequenceforStation(StationDTO);
        }
        if (_ValidationResultDTO.Result)
        {
            //save station 
            StationDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Station_Repository.CreateStation(StationDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateStation_Global(StationDTO StationDTO)
    {
        var _ValidationResultDTO = Station_Validator.UpdateStation_Validation(StationDTO);
        if (_ValidationResultDTO.Result)
        {
            StationDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Station_Repository.UpdateStation(StationDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteStation_Global(StationDTO StationDTO)
    {
        var _ValidationResultDTO = Station_Validator.DeleteStation_Validation(StationDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Station_Repository.DeleteStation(StationDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<StationDTO> GetStationList_Global(StationDTO StationDTO, PagedResultDTO<StationDTO> PagedResultDTO = null)
    {
        var _stationglobalList = new List<StationDTO>();
        try
        {
            //Filter station by items lines
            if (StationDTO.Item_LineIDArray != null && StationDTO.Item_LineIDArray.Length > 0)
            {
                _stationglobalList = GetStationListByItemsLine_Global(StationDTO);
                return _stationglobalList;
            }



            var _stationList = Station_Repository.GetStationList(StationDTO, PagedResultDTO);
            // if Station is empty, return list
            if (_stationList.Count() == 0)
            {
                _stationglobalList = _stationList;
                return _stationglobalList;
            }
            if (!StationDTO.GetFacilityDTO && !StationDTO.GetDepartmentDTO && !StationDTO.GetStationTypeDTO)
            {
                _stationglobalList = _stationList;
                return _stationglobalList;
            }
            _stationglobalList = GetStationRelatedData(StationDTO, _stationList);

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _stationglobalList;
    }



    public static List<StationDTO> GetStationRelatedData(StationDTO StationDTO, List<StationDTO> StationList)
    {
        var _stationglobalList = new List<StationDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _departmentDict = new Dictionary<int?, DepartmentDTO>();
        var _stationtypeDict = new Dictionary<int?, StationTypeDTO>();

        try
        {
            if (StationDTO.GetFacilityDTO)
            {
                StationDTO.FacilityDTO.FacilityIDArray = StationList.GroupBy(g => g.FacilityID)
                        .Select(s => s.Key)
                        .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(StationDTO.FacilityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (StationDTO.GetDepartmentDTO)
            {
                StationDTO.DepartmentDTO.DepartmentIDArray = StationList.GroupBy(g => g.DepartmentID)
                        .Select(s => s.Key)
                        .ToArray();

                _departmentDict = Department_Service.GetDepartmentList_Global(StationDTO.DepartmentDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (StationDTO.GetStationTypeDTO)
            {
                StationDTO.StationTypeDTO.StationTypeIDArray = StationList.GroupBy(g => g.StationTypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _stationtypeDict = StationType_Service.GetStationTypeList_Global(StationDTO.StationTypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _stationDTO in StationList)
            {
                if (StationDTO.GetFacilityDTO && _facilityDict.ContainsKey(_stationDTO.FacilityID))
                {
                    _stationDTO.FacilityDTO = _facilityDict[_stationDTO.FacilityID];
                }
                if (StationDTO.GetDepartmentDTO && _departmentDict.ContainsKey(_stationDTO.DepartmentID))
                {
                    _stationDTO.DepartmentDTO = _departmentDict[_stationDTO.DepartmentID];
                }
                if (StationDTO.GetStationTypeDTO && _stationtypeDict.ContainsKey(_stationDTO.StationTypeID))
                {
                    _stationDTO.StationTypeDTO = _stationtypeDict[_stationDTO.StationTypeID];
                }
                _stationglobalList.Add(_stationDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _stationglobalList;
    }


    public static List<StationDTO> GetStationListByItemsLine_Global(StationDTO StationDTO)
    {
        var _stationglobalList = new List<StationDTO>();
        var _item_lineList = new List<Item_LineDTO>();
        try
        {
            if (StationDTO.Item_LineIDArray != null && StationDTO.Item_LineIDArray.Length > 0)
            {
                //get item lines by items ID's
                _item_lineList = Item_Line_Service.GetItem_LineList_Global(new Item_LineDTO { Item_LineIDArray = StationDTO.Item_LineIDArray });
                //get stations ID's of items where station ID doesnt null
                StationDTO.StationIDArray = _item_lineList.Where(w => w.StationID > 0).GroupBy(g => g.StationID).Select(s => s.Key).ToArray();

                if (StationDTO.StationIDArray != null && StationDTO.StationIDArray.Length > 0)
                {
                    _stationglobalList = Station_Repository.GetStationList(new StationDTO { StationIDArray = StationDTO.StationIDArray });
                }
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _stationglobalList;
    }

    public static int GetStationTotalCount(PagedResultDTO<StationDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Station_Repository.GetStationCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    public static ValidationResultDTO GetSerialSequenceforStation(StationDTO StationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        try
        {
            var _stationSerial = string.Format("STN{0}", AssetManagementSQL.GetStationSerial());
            StationDTO.Serial = _stationSerial;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("Ha ocurrido un error. {0}", ex.Message);
        }
        return _validationResultDTO;

    }
    #endregion
}
