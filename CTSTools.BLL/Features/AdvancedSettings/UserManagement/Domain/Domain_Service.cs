using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.Domain;

public class Domain_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateDomain_Global(DomainDTO DomainDTO)
    {
        var _validationResultDTO = Domain_Validator.CreateDomain_Validation(DomainDTO);
        if (_validationResultDTO.Result)
        {
            DomainDTO.AddedDate = DateTime.Now;
            _validationResultDTO = Domain_Repository.CreateDomain(DomainDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateDomain_Global(DomainDTO DomainDTO)
    {
        var _validationResultDTO = Domain_Validator.UpdateDomain_Validation(DomainDTO);
        if (_validationResultDTO.Result)
        {
            DomainDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Domain_Repository.UpdateDomain(DomainDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteDomain_Global(DomainDTO DomainDTO)
    {
        var _validationResultDTO = Domain_Validator.DeleteDomain_Validation(DomainDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Domain_Repository.DeleteDomain(DomainDTO);
        }
        return _validationResultDTO;
    }
    public static List<DomainDTO> GetDomainList_Global(DomainDTO DomainDTO,PagedResultDTO<DomainDTO> PagedDomainDTO = null)
    {
        var _domainglobalList = new List<DomainDTO>();
        try
        {
            var _domainList = Domain_Repository.GetDomainList(DomainDTO,PagedDomainDTO);
            if (_domainList.Count() == 0)
            {
                _domainglobalList = _domainList;
                return _domainglobalList;
            }
            if (!DomainDTO.GetFacilityDTO)
            {
                _domainglobalList = GetDomainRelatedData(DomainDTO, _domainList);
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _domainglobalList;
    }

    public static List<DomainDTO> GetDomainRelatedData(DomainDTO DomainDTO, List<DomainDTO> DomainList)
    {
        var _domainglobalList = new List<DomainDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();

        try
        {
            if (DomainDTO.GetFacilityDTO)
            {
                DomainDTO.FacilityDTO.FacilityIDArray = DomainList.GroupBy(g => g.FacilityID)
                                                                  .Select(s => s.Key)
                                                                  .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(DomainDTO.FacilityDTO)
                                                .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _domainDTO in DomainList)
            {
                if (DomainDTO.GetFacilityDTO && _facilityDict.ContainsKey(_domainDTO.FacilityID))
                {
                    _domainDTO.FacilityDTO = _facilityDict[_domainDTO.FacilityID];
                }
                _domainglobalList.Add(_domainDTO);
            }

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _domainglobalList;
    }

    public static int GetDomainTotalCount(PagedResultDTO<DomainDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Domain_Repository.GetDomainCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion
}
