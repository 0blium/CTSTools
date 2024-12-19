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
            if (MetricDTO.UnitOfMeasureID != null || MetricDTO.UnitOfMeasureID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.UnitOfMeasure), MetricDTO.UnitOfMeasureID));
            }
            if (MetricDTO.UnitOfMeasureIDArray != null && MetricDTO.UnitOfMeasureIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.UnitOfMeasure), MetricDTO.UnitOfMeasureIDArray));
            }
            if (MetricDTO.ValueTypeID != null || MetricDTO.ValueTypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.ValueType), MetricDTO.ValueTypeID));
            }
            if (MetricDTO.ValueTypeIDArray != null && MetricDTO.ValueTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.ValueType), MetricDTO.ValueTypeIDArray));
            }
            if (MetricDTO.OwnerID != null || MetricDTO.OwnerID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Owner), MetricDTO.OwnerID));
            }
            if (MetricDTO.ResponsibleID != null || MetricDTO.ResponsibleID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Responsible), MetricDTO.ResponsibleID));
            }
            if (MetricDTO.Shared != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Shared), MetricDTO.Shared));
            }
            if (MetricDTO.GoalRangeID != null || MetricDTO.GoalRangeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.GoalRange), MetricDTO.GoalRangeID));
            }
            if (MetricDTO.GoalRangeIDArray != null && MetricDTO.GoalRangeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.GoalRange), MetricDTO.GoalRangeIDArray));
            }
            if (MetricDTO.FacilityID != null || MetricDTO.FacilityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Facility), MetricDTO.FacilityID));
            }
            if (MetricDTO.FacilityIDArray != null && MetricDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Facility), MetricDTO.FacilityIDArray));
            }
            if (MetricDTO.EquivalenceID != null || MetricDTO.EquivalenceID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Equivalence), MetricDTO.EquivalenceID));
            }
            if (MetricDTO.EquivalenceIDArray != null && MetricDTO.EquivalenceIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Equivalence), MetricDTO.EquivalenceIDArray));
            }
            if (MetricDTO.StatusID != null || MetricDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.Status), MetricDTO.StatusID));
            }
            if (MetricDTO.StatusIDArray != null && MetricDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(MetricXPO.Status), MetricDTO.StatusIDArray));
            }
            if (MetricDTO.IsParent != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.IsParent), MetricDTO.IsParent));
            }
            if (MetricDTO.CalculationTypeID != null || MetricDTO.CalculationTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(MetricXPO.CalculationType), MetricDTO.CalculationTypeID));
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
