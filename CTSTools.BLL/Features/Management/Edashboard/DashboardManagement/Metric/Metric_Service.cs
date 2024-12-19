using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.CalculationType;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.GoalRange;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.UnitOfMeasure;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.ValueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric
{
    public class Metric_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateMetric_Global(MetricDTO MetricDTO)
        {
            var _ValidationResultDTO = Metric_Validator.CreateMetric_Validation(MetricDTO);
            if (_ValidationResultDTO.Result)
            {
                MetricDTO.AddedDate = DateTime.Now;
                _ValidationResultDTO = Metric_Repository.CreateMetric(MetricDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO UpdateMetric_Global(MetricDTO MetricDTO)
        {
            var _ValidationResultDTO = Metric_Validator.UpdateMetric_Validation(MetricDTO);
            if (_ValidationResultDTO.Result)
            {
                MetricDTO.LastUpdate = DateTime.Now;
                _ValidationResultDTO = Metric_Repository.UpdateMetric(MetricDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO DeleteMetric_Global(MetricDTO MetricDTO)
        {
            var _ValidationResultDTO = Metric_Validator.DeleteMetric_Validation(MetricDTO);
            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = Metric_Repository.DeleteMetric(MetricDTO);
            }
            return _ValidationResultDTO;
        }
        public static List<MetricDTO> GetMetricList_Global(MetricDTO MetricDTO, PagedResultDTO<MetricDTO> PagedResultDTO = null)
        {
            var _metricglobalList = new List<MetricDTO>();
            try
            {
                var _metricList = Metric_Repository.GetMetricList(MetricDTO, PagedResultDTO);
                // if Metric is empty, return list
                if (_metricList.Count() == 0)
                {
                    _metricglobalList = _metricList;
                    return _metricglobalList;
                }
                if (!MetricDTO.GetUnitOfMeasureDTO && !MetricDTO.GetValueTypeDTO && !MetricDTO.GetGoalRangeDTO && !MetricDTO.GetFacilityDTO && !MetricDTO.GetEquivalenceDTO && !MetricDTO.GetStatusDTO && !MetricDTO.GetCalculationTypeDTO)
                {
                    _metricglobalList = _metricList;
                    return _metricglobalList;
                }
                _metricglobalList = GetMetricRelatedData(MetricDTO, _metricList);

            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _metricglobalList;
        }



        public static List<MetricDTO> GetMetricRelatedData(MetricDTO MetricDTO, List<MetricDTO> MetricList)
        {
            var _metricglobalList = new List<MetricDTO>();
            var _unitofmeasureDict = new Dictionary<int?, UnitOfMeasureDTO>();
            var _valuetypeDict = new Dictionary<int?, ValueTypeDTO>();
            var _goalrangeDict = new Dictionary<int?, GoalRangeDTO>();
            var _facilityDict = new Dictionary<int?, FacilityDTO>();
            var _equivalenceDict = new Dictionary<int?, EquivalenceDTO>();
            var _statusDict = new Dictionary<int?, StatusDTO>();
            var _calculationtypeDict = new Dictionary<int?, CalculationTypeDTO>();

            try
            {
                if (MetricDTO.GetUnitOfMeasureDTO)
                {
                    MetricDTO.UnitOfMeasureDTO.UnitOfMeasureIDArray = MetricList.GroupBy(g => g.UnitOfMeasureID)
                            .Select(s => s.Key)
                            .ToArray();

                    _unitofmeasureDict = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(MetricDTO.UnitOfMeasureDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MetricDTO.GetValueTypeDTO)
                {
                    MetricDTO.ValueTypeDTO.ValueTypeIDArray = MetricList.GroupBy(g => g.ValueTypeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _valuetypeDict = ValueType_Service.GetValueTypeList_Global(MetricDTO.ValueTypeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MetricDTO.GetGoalRangeDTO)
                {
                    MetricDTO.GoalRangeDTO.GoalRangeIDArray = MetricList.GroupBy(g => g.GoalRangeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _goalrangeDict = GoalRange_Service.GetGoalRangeList_Global(MetricDTO.GoalRangeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MetricDTO.GetFacilityDTO)
                {
                    MetricDTO.FacilityDTO.FacilityIDArray = MetricList.GroupBy(g => g.FacilityID)
                            .Select(s => s.Key)
                            .ToArray();

                    _facilityDict = Facility_Service.GetFacilityList_Global(MetricDTO.FacilityDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MetricDTO.GetEquivalenceDTO)
                {
                    MetricDTO.EquivalenceDTO.EquivalenceIDArray = MetricList.GroupBy(g => g.EquivalenceID)
                            .Select(s => s.Key)
                            .ToArray();

                    _equivalenceDict = Equivalence_Service.GetEquivalenceList_Global(MetricDTO.EquivalenceDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MetricDTO.GetStatusDTO)
                {
                    MetricDTO.StatusDTO.StatusIDArray = MetricList.GroupBy(g => g.StatusID)
                            .Select(s => s.Key)
                            .ToArray();

                    _statusDict = Status_Service.GetStatusList_Global(MetricDTO.StatusDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MetricDTO.GetCalculationTypeDTO)
                {
                    MetricDTO.CalculationTypeDTO.CalculationTypeIDArray = MetricList.GroupBy(g => g.CalculationTypeID)
                            .Select(s => s.Key)
                            .ToArray();

                    _calculationtypeDict = CalculationType_Service.GetCalculationTypeList_Global(MetricDTO.CalculationTypeDTO)
                            .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _metricDTO in MetricList)
                {
                    if (MetricDTO.GetUnitOfMeasureDTO && _unitofmeasureDict.ContainsKey(_metricDTO.UnitOfMeasureID))
                    {
                        _metricDTO.UnitOfMeasureDTO = _unitofmeasureDict[_metricDTO.UnitOfMeasureID];
                    }
                    if (MetricDTO.GetValueTypeDTO && _valuetypeDict.ContainsKey(_metricDTO.ValueTypeDTO.ID))
                    {
                        _metricDTO.ValueTypeDTO = _valuetypeDict[_metricDTO.ValueTypeID];
                    }
                    if (MetricDTO.GetGoalRangeDTO && _goalrangeDict.ContainsKey(_metricDTO.GoalRangeID))
                    {
                        _metricDTO.GoalRangeDTO = _goalrangeDict[_metricDTO.GoalRangeID];
                    }
                    if (MetricDTO.GetFacilityDTO && _facilityDict.ContainsKey(_metricDTO.FacilityID))
                    {
                        _metricDTO.FacilityDTO = _facilityDict[_metricDTO.FacilityID];
                    }
                    if (MetricDTO.GetEquivalenceDTO && _equivalenceDict.ContainsKey(_metricDTO.EquivalenceID))
                    {
                        _metricDTO.EquivalenceDTO = _equivalenceDict[_metricDTO.EquivalenceID];
                    }
                    if (MetricDTO.GetStatusDTO && _statusDict.ContainsKey(_metricDTO.StatusID))
                    {
                        _metricDTO.StatusDTO = _statusDict[_metricDTO.StatusID];
                    }
                    if (MetricDTO.GetCalculationTypeDTO && _calculationtypeDict.ContainsKey(_metricDTO.CalculationTypeID))
                    {
                        _metricDTO.CalculationTypeDTO = _calculationtypeDict[_metricDTO.CalculationTypeID];
                    }
                    _metricglobalList.Add(_metricDTO);
                }

            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _metricglobalList;
        }




        public static int GetMetricTotalCount(PagedResultDTO<MetricDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount = Metric_Repository.GetMetricCount(PagedResultDTO.Filter, PagedResultDTO);
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
}
