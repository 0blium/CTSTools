using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;

public class Facility_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateFacility_Global(FacilityDTO FacilityDTO)
    {
        var _validationResultDTO = Facility_Validator.CreateFacility_Validation(FacilityDTO);
        if (_validationResultDTO.Result)
        {
            FacilityDTO.AddedDate = DateTime.Now;
            _validationResultDTO = Facility_Repository.CreateFacility(FacilityDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateFacility_Global(FacilityDTO FacilityDTO)
    {
        var _validationResultDTO = Facility_Validator.UpdateFacility_Validation(FacilityDTO);
        if (_validationResultDTO.Result)
        {
            FacilityDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Facility_Repository.UpdateFacility(FacilityDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteFacility_Global(FacilityDTO FacilityDTO)
    {
        var _validationResultDTO = Facility_Validator.DeleteFacility_Validation(FacilityDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Facility_Repository.DeleteFacility(FacilityDTO);
        }
        return _validationResultDTO;
    }
    public static List<FacilityDTO> GetFacilityList_Global(FacilityDTO FacilityDTO, PagedResultDTO<FacilityDTO> PagedFacilityDTO = null)
    {
        var _facilityGlobalList = new List<FacilityDTO>();
        try
        {
            var _facilityList = Facility_Repository.GetFacilityList(FacilityDTO, PagedFacilityDTO);                           
            _facilityGlobalList = _facilityList;
            return _facilityGlobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _facilityGlobalList;
    }

    public static int GetTotalCount(PagedResultDTO<FacilityDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount =Facility_Repository.GetFacilityCount(PagedResultDTO.Filter, PagedResultDTO);
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
