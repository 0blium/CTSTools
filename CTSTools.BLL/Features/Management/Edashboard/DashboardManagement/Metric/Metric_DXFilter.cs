using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;

public class Metric_DXFilter
{
    public static GroupOperator GetMetric_DXFilter(MetricDTO MetricDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (MetricDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Oid), MetricDTO.ID));
            }
            if (MetricDTO.MetricIDArray != null && MetricDTO.MetricIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Oid), MetricDTO.MetricIDArray));
            }
            if (MetricDTO.UnitOfMeasureDTO.ID != null || MetricDTO.UnitOfMeasureDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.UnitOfMeasure), MetricDTO.UnitOfMeasureDTO.ID));
            }
            if (MetricDTO.UnitOfMeasureIDArray != null && MetricDTO.UnitOfMeasureIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.UnitOfMeasure), MetricDTO.UnitOfMeasureIDArray));
            }
            if (MetricDTO.ValueTypeDTO.ID != null || MetricDTO.ValueTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.ValueType), MetricDTO.ValueTypeDTO.ID));
            }
            if (MetricDTO.ValueTypeIDArray != null && MetricDTO.ValueTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.ValueType), MetricDTO.ValueTypeIDArray));
            }
            if (MetricDTO.OwnerDTO.ID != null || MetricDTO.OwnerDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Owner), MetricDTO.OwnerDTO.ID));
            }
            if (MetricDTO.ResponsibleDTO.ID != null || MetricDTO.ResponsibleDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Responsible), MetricDTO.ResponsibleDTO.ID));
            }
            if (MetricDTO.Shared != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Shared), MetricDTO.Shared));
            }
            if (MetricDTO.GoalRangeDTO.ID != null || MetricDTO.GoalRangeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.GoalRange), MetricDTO.GoalRangeDTO.ID));
            }
            if (MetricDTO.GoalRangeIDArray != null && MetricDTO.GoalRangeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.GoalRange), MetricDTO.GoalRangeIDArray));
            }
            if (MetricDTO.FacilityDTO.ID != null || MetricDTO.FacilityDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Facility), MetricDTO.FacilityDTO.ID));
            }
            if (MetricDTO.FacilityIDArray != null && MetricDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Facility), MetricDTO.FacilityIDArray));
            }
            if (MetricDTO.EquivalenceDTO.ID != null || MetricDTO.EquivalenceDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Equivalence), MetricDTO.EquivalenceDTO.ID));
            }
            if (MetricDTO.EquivalenceIDArray != null && MetricDTO.EquivalenceIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Equivalence), MetricDTO.EquivalenceIDArray));
            }
            if (MetricDTO.StatusDTO.ID != null || MetricDTO.StatusDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Status), MetricDTO.StatusDTO.ID));
            }
            if (MetricDTO.StatusIDArray != null && MetricDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Status), MetricDTO.StatusIDArray));
            }
            if (MetricDTO.IsParent != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.IsParent), MetricDTO.IsParent));
            }
            if (MetricDTO.CalculationTypeDTO.ID != null || MetricDTO.CalculationTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.CalculationType), MetricDTO.CalculationTypeDTO.ID));
            }
            if (MetricDTO.CalculationTypeIDArray != null && MetricDTO.CalculationTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.CalculationType), MetricDTO.CalculationTypeIDArray));
            }
            if (MetricDTO.AddedByID != null && MetricDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.AddedBy), MetricDTO.AddedByID));
            }
            if (MetricDTO.LastUpdateByID != null && MetricDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.LastUpdateBy), MetricDTO.LastUpdateByID));
            }
            if (MetricDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.IsActive), MetricDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
