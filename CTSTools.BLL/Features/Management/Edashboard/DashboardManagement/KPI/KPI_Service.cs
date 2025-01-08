using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.GoalRange;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;

public class KPI_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateKPI_Global(KPIDTO KPIDTO)
    {
        var _ValidationResultDTO = KPI_Validator.CreateKPI_Validation(KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            KPIDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = KPI_Repository.CreateKPI(KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateKPI_Global(KPIDTO KPIDTO)
    {
        var _ValidationResultDTO = KPI_Validator.UpdateKPI_Validation(KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            KPIDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = KPI_Repository.UpdateKPI(KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteKPI_Global(KPIDTO KPIDTO)
    {
        var _ValidationResultDTO = KPI_Validator.DeleteKPI_Validation(KPIDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = KPI_Repository.DeleteKPI(KPIDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<KPIDTO> GetKPIList_Global(KPIDTO KPIDTO, PagedResultDTO<KPIDTO> PagedResultDTO = null)
    {
        var _kpiglobalList = new List<KPIDTO>();
        try
        {
            var _kpiList = KPI_Repository.GetKPIList(KPIDTO, PagedResultDTO);
            // if KPI is empty, return list
            if (_kpiList.Count() == 0)
            {
                _kpiglobalList = _kpiList;
                return _kpiglobalList;
            }
            if (!KPIDTO.GetUnitOfMeasureDTO && !KPIDTO.GetValueTypeDTO && !KPIDTO.GetGoalRangeDTO && !KPIDTO.GetFacilityDTO && !KPIDTO.GetEquivalenceDTO && !KPIDTO.GetStatusDTO && !KPIDTO.GetCalculationTypeDTO)
            {
                _kpiglobalList = _kpiList;
                return _kpiglobalList;
            }
            _kpiglobalList = GetKPIRelatedData(KPIDTO, _kpiList);

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _kpiglobalList;
    }



    public static List<KPIDTO> GetKPIRelatedData(KPIDTO KPIDTO, List<KPIDTO> KPIList)
    {
        var _kpiglobalList = new List<KPIDTO>();
        var _unitofmeasureDict = new Dictionary<int?, UnitOfMeasureDTO>();
        var _valuetypeDict = new Dictionary<int?, ValueTypeDTO>();
        var _goalrangeDict = new Dictionary<int?, GoalRangeDTO>();
        var _facilityDict = new Dictionary<int?, FacilityDTO>();
        var _equivalenceDict = new Dictionary<int?, EquivalenceDTO>();
        var _statusDict = new Dictionary<int?, StatusDTO>();
        var _calculationtypeDict = new Dictionary<int?, CalculationTypeDTO>();

        try
        {
            if (KPIDTO.GetUnitOfMeasureDTO)
            {
                KPIDTO.UnitOfMeasureDTO.UnitOfMeasureIDArray = KPIList.GroupBy(g => g.UnitOfMeasureID)
                        .Select(s => s.Key)
                        .ToArray();

                _unitofmeasureDict = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(KPIDTO.UnitOfMeasureDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetValueTypeDTO)
            {
                KPIDTO.ValueTypeDTO.ValueTypeIDArray = KPIList.GroupBy(g => g.ValueTypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _valuetypeDict = ValueType_Service.GetValueTypeList_Global(KPIDTO.ValueTypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetGoalRangeDTO)
            {
                KPIDTO.GoalRangeDTO.GoalRangeIDArray = KPIList.GroupBy(g => g.GoalRangeID)
                        .Select(s => s.Key)
                        .ToArray();

                _goalrangeDict = GoalRange_Service.GetGoalRangeList_Global(KPIDTO.GoalRangeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetFacilityDTO)
            {
                KPIDTO.FacilityDTO.FacilityIDArray = KPIList.GroupBy(g => g.FacilityID)
                        .Select(s => s.Key)
                        .ToArray();

                _facilityDict = Facility_Service.GetFacilityList_Global(KPIDTO.FacilityDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetEquivalenceDTO)
            {
                KPIDTO.EquivalenceDTO.EquivalenceIDArray = KPIList.GroupBy(g => g.EquivalenceID)
                        .Select(s => s.Key)
                        .ToArray();

                _equivalenceDict = Equivalence_Service.GetEquivalenceList_Global(KPIDTO.EquivalenceDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetStatusDTO)
            {
                KPIDTO.StatusDTO.StatusIDArray = KPIList.GroupBy(g => g.StatusID)
                        .Select(s => s.Key)
                        .ToArray();

                _statusDict = Status_Service.GetStatusList_Global(KPIDTO.StatusDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            if (KPIDTO.GetCalculationTypeDTO)
            {
                KPIDTO.CalculationTypeDTO.CalculationTypeIDArray = KPIList.GroupBy(g => g.CalculationTypeID)
                        .Select(s => s.Key)
                        .ToArray();

                _calculationtypeDict = CalculationType_Service.GetCalculationTypeList_Global(KPIDTO.CalculationTypeDTO)
                        .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
            }
            foreach (var _kpiDTO in KPIList)
            {
                if (KPIDTO.GetUnitOfMeasureDTO && _unitofmeasureDict.ContainsKey(_kpiDTO.UnitOfMeasureID))
                {
                    _kpiDTO.UnitOfMeasureDTO = _unitofmeasureDict[_kpiDTO.UnitOfMeasureID];
                }
                if (KPIDTO.GetValueTypeDTO && _valuetypeDict.ContainsKey(_kpiDTO.ValueTypeDTO.ID))
                {
                    _kpiDTO.ValueTypeDTO = _valuetypeDict[_kpiDTO.ValueTypeID];
                }
                if (KPIDTO.GetGoalRangeDTO && _goalrangeDict.ContainsKey(_kpiDTO.GoalRangeID))
                {
                    _kpiDTO.GoalRangeDTO = _goalrangeDict[_kpiDTO.GoalRangeID];
                }
                if (KPIDTO.GetFacilityDTO && _facilityDict.ContainsKey(_kpiDTO.FacilityID))
                {
                    _kpiDTO.FacilityDTO = _facilityDict[_kpiDTO.FacilityID];
                }
                if (KPIDTO.GetEquivalenceDTO && _equivalenceDict.ContainsKey(_kpiDTO.EquivalenceID))
                {
                    _kpiDTO.EquivalenceDTO = _equivalenceDict[_kpiDTO.EquivalenceID];
                }
                if (KPIDTO.GetStatusDTO && _statusDict.ContainsKey(_kpiDTO.StatusID))
                {
                    _kpiDTO.StatusDTO = _statusDict[_kpiDTO.StatusID];
                }
                if (KPIDTO.GetCalculationTypeDTO && _calculationtypeDict.ContainsKey(_kpiDTO.CalculationTypeID))
                {
                    _kpiDTO.CalculationTypeDTO = _calculationtypeDict[_kpiDTO.CalculationTypeID];
                }
                _kpiglobalList.Add(_kpiDTO);
            }

        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _kpiglobalList;
    }




    public static int GetKPITotalCount(PagedResultDTO<KPIDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = KPI_Repository.GetKPICount(PagedResultDTO.Filter, PagedResultDTO);
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
