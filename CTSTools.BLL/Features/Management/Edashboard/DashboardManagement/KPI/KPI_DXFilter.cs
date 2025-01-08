using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;

public class KPI_DXFilter
{
    public static GroupOperator GetKPI_DXFilter(KPIDTO KPIDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (KPIDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Oid), KPIDTO.ID));
            }
            if (KPIDTO.KPIIDArray != null && KPIDTO.KPIIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.Oid), KPIDTO.KPIIDArray));
            }
            if (KPIDTO.UnitOfMeasureID != null || KPIDTO.UnitOfMeasureID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.UnitOfMeasure), KPIDTO.UnitOfMeasureID));
            }
            if (KPIDTO.UnitOfMeasureIDArray != null && KPIDTO.UnitOfMeasureIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.UnitOfMeasure), KPIDTO.UnitOfMeasureIDArray));
            }
            if (KPIDTO.ValueTypeID != null || KPIDTO.ValueTypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.ValueType), KPIDTO.ValueTypeID));
            }
            if (KPIDTO.ValueTypeIDArray != null && KPIDTO.ValueTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.ValueType), KPIDTO.ValueTypeIDArray));
            }
            if (KPIDTO.OwnerID != null || KPIDTO.OwnerID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Owner), KPIDTO.OwnerID));
            }
            if (KPIDTO.ResponsibleID != null || KPIDTO.ResponsibleID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Responsible), KPIDTO.ResponsibleID));
            }
            if (KPIDTO.Shared != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Shared), KPIDTO.Shared));
            }
            if (KPIDTO.GoalRangeID != null || KPIDTO.GoalRangeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.GoalRange), KPIDTO.GoalRangeID));
            }
            if (KPIDTO.GoalRangeIDArray != null && KPIDTO.GoalRangeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.GoalRange), KPIDTO.GoalRangeIDArray));
            }
            if (KPIDTO.FacilityID != null || KPIDTO.FacilityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Facility), KPIDTO.FacilityID));
            }
            if (KPIDTO.FacilityIDArray != null && KPIDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.Facility), KPIDTO.FacilityIDArray));
            }
            if (KPIDTO.EquivalenceID != null || KPIDTO.EquivalenceID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Equivalence), KPIDTO.EquivalenceID));
            }
            if (KPIDTO.DashboardCategoryID != null || KPIDTO.DashboardCategoryID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.DashboardCategory), KPIDTO.DashboardCategoryID));
            }
            if (KPIDTO.EquivalenceIDArray != null && KPIDTO.EquivalenceIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.Equivalence), KPIDTO.EquivalenceIDArray));
            }
            if (KPIDTO.StatusID != null || KPIDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.Status), KPIDTO.StatusID));
            }
            if (KPIDTO.StatusIDArray != null && KPIDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.Status), KPIDTO.StatusIDArray));
            }
            if (KPIDTO.IsParent != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.IsParent), KPIDTO.IsParent));
            }
            if (KPIDTO.CalculationTypeID != null || KPIDTO.CalculationTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.CalculationType), KPIDTO.CalculationTypeID));
            }
            if (KPIDTO.CalculationTypeIDArray != null && KPIDTO.CalculationTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(KPIXPO.CalculationType), KPIDTO.CalculationTypeIDArray));
            }
            if (KPIDTO.AddedByID != null && KPIDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.AddedBy), KPIDTO.AddedByID));
            }
            if (KPIDTO.LastUpdateByID != null && KPIDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.LastUpdateBy), KPIDTO.LastUpdateByID));
            }
            if (KPIDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(KPIXPO.IsActive), KPIDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
