using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLine_DXFilter
{
    public static GroupOperator GetDashboardLine_DXFilter(DashboardLineDTO DashboardLineDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DashboardLineDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Oid), DashboardLineDTO.ID));
            }
            if (DashboardLineDTO.DashboardLineIDArray != null && DashboardLineDTO.DashboardLineIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.Oid), DashboardLineDTO.DashboardLineIDArray));
            }
            if (DashboardLineDTO.DashboardMetricDTO?.ID != null || DashboardLineDTO.DashboardMetricDTO?.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.DashboardMetric), DashboardLineDTO.DashboardMetricDTO.ID));
            }
            if (DashboardLineDTO.DashboardMetricIDArray != null && DashboardLineDTO.DashboardMetricIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.DashboardMetric), DashboardLineDTO.DashboardMetricIDArray));
            }
            if (DashboardLineDTO.MetricDTO.ID != null || DashboardLineDTO.MetricDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Metric), DashboardLineDTO.MetricDTO.ID));
            }
            if (DashboardLineDTO.MetricIDArray != null && DashboardLineDTO.MetricIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.Metric), DashboardLineDTO.MetricIDArray));
            }
            if (DashboardLineDTO.DashboardCategoryDTO.ID != null || DashboardLineDTO.DashboardCategoryDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.DashboardCategory), DashboardLineDTO.DashboardCategoryDTO.ID));
            }
            if (DashboardLineDTO.DashboardCategoryIDArray != null && DashboardLineDTO.DashboardCategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.DashboardCategory), DashboardLineDTO.DashboardCategoryIDArray));
            }
            if (DashboardLineDTO.DashboardDTO.ID != null || DashboardLineDTO.DashboardDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Dashboard), DashboardLineDTO.DashboardDTO.ID));
            }
            if (DashboardLineDTO.DashboardIDArray != null && DashboardLineDTO.DashboardIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.Dashboard), DashboardLineDTO.DashboardIDArray));
            }
            if (DashboardLineDTO.Month != null && DashboardLineDTO.Month > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Month), DashboardLineDTO.Month));
            }
            if (DashboardLineDTO.Year != null && DashboardLineDTO.Year > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Year), DashboardLineDTO.Year));
            }
            if (DashboardLineDTO.FiscalYear != null && DashboardLineDTO.FiscalYear > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.FiscalYear), DashboardLineDTO.FiscalYear));
            }
            //if (DashboardLineDTO.IsTemporalValue != null)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.IsTemporalValue), DashboardLineDTO.IsTemporalValue));
            //}
            //if (DashboardLineDTO.IgnoreMetric != null)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.IgnoreMetric), DashboardLineDTO.IgnoreMetric));
            //}
            //if (DashboardLineDTO.Validated != null)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Validated), DashboardLineDTO.Validated));
            //}
            if (DashboardLineDTO.ValidatedByDTO.ID != null || DashboardLineDTO.ValidatedByDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.ValidatedBy), DashboardLineDTO.ValidatedByDTO.ID));
            }
            if (DashboardLineDTO.AddedByID != null && DashboardLineDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.AddedBy), DashboardLineDTO.AddedByID));
            }
            if (DashboardLineDTO.LastUpdateByID != null && DashboardLineDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.LastUpdateBy), DashboardLineDTO.LastUpdateByID));
            }
            if (DashboardLineDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.IsActive), DashboardLineDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
