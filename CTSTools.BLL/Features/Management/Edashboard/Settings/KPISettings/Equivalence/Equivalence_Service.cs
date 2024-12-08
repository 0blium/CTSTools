using CTSTools.BLL.Common;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;

public class Equivalence_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateEquivalence_Global(EquivalenceDTO EquivalenceDTO)
    {
        var _ValidationResultDTO = Equivalence_Validator.CreateEquivalence_Validation(EquivalenceDTO);
        if (_ValidationResultDTO.Result)
        {
            EquivalenceDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Equivalence_Repository.CreateEquivalence(EquivalenceDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateEquivalence_Global(EquivalenceDTO EquivalenceDTO)
    {
        var _ValidationResultDTO = Equivalence_Validator.UpdateEquivalence_Validation(EquivalenceDTO);
        if (_ValidationResultDTO.Result)
        {
            EquivalenceDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Equivalence_Repository.UpdateEquivalence(EquivalenceDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteEquivalence_Global(EquivalenceDTO EquivalenceDTO)
    {
        var _ValidationResultDTO = Equivalence_Validator.DeleteEquivalence_Validation(EquivalenceDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Equivalence_Repository.DeleteEquivalence(EquivalenceDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<EquivalenceDTO> GetEquivalenceList_Global(EquivalenceDTO EquivalenceDTO, PagedResultDTO<EquivalenceDTO> PagedResultDTO = null)
    {
        var _equivalenceglobalList = new List<EquivalenceDTO>();
        try
        {
            var _equivalenceList = Equivalence_Repository.GetEquivalenceList(EquivalenceDTO, PagedResultDTO);
            // if Equivalence is empty, return list
            _equivalenceglobalList = _equivalenceList;


        }
        catch (Exception ex)
        {

        }
        return _equivalenceglobalList;
    }





    public static int GetEquivalenceTotalCount(PagedResultDTO<EquivalenceDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Equivalence_Repository.GetEquivalenceCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {

        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
