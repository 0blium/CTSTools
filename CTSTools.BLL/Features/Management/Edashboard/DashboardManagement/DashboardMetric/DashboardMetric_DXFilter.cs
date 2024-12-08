using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetric_DXFilter
{
    public static GroupOperator GetDashboardMetric_DXFilter(DashboardMetricDTO DashboardMetricDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DashboardMetricDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.Oid), DashboardMetricDTO.ID));
            }
            if (DashboardMetricDTO.DashboardMetricIDArray != null && DashboardMetricDTO.DashboardMetricIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardMetricXPO.Oid), DashboardMetricDTO.DashboardMetricIDArray));
            }
            if (DashboardMetricDTO.StatusDTO.ID != null || DashboardMetricDTO.StatusDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.Status), DashboardMetricDTO.StatusDTO.ID));
            }
            if (DashboardMetricDTO.StatusIDArray != null && DashboardMetricDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardMetricXPO.Status), DashboardMetricDTO.StatusIDArray));
            }
            if (DashboardMetricDTO.DashboardDTO.ID != null || DashboardMetricDTO.DashboardDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.Dashboard), DashboardMetricDTO.DashboardDTO.ID));
            }
            if (DashboardMetricDTO.DashboardIDArray != null && DashboardMetricDTO.DashboardIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardMetricXPO.Dashboard), DashboardMetricDTO.DashboardIDArray));
            }
            if (DashboardMetricDTO.MetricDTO.ID != null || DashboardMetricDTO.MetricDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.Metric), DashboardMetricDTO.MetricDTO.ID));
            }
            if (DashboardMetricDTO.MetricIDArray != null && DashboardMetricDTO.MetricIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardMetricXPO.Metric), DashboardMetricDTO.MetricIDArray));
            }
            if (DashboardMetricDTO.Order != null && DashboardMetricDTO.Order > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.Order), DashboardMetricDTO.Order));
            }
            if (DashboardMetricDTO.DashboardCategoryDTO.ID != null && DashboardMetricDTO.DashboardCategoryDTO?.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.DashboardCategory), DashboardMetricDTO.DashboardCategoryDTO.ID));
            }
            if (DashboardMetricDTO.DashboardCategoryIDArray != null && DashboardMetricDTO.DashboardCategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardMetricXPO.DashboardCategory), DashboardMetricDTO.DashboardCategoryIDArray));
            }
            if (DashboardMetricDTO.AddedByID != null && DashboardMetricDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.AddedBy), DashboardMetricDTO.AddedByID));
            }
            if (DashboardMetricDTO.LastUpdateByID != null && DashboardMetricDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.LastUpdateBy), DashboardMetricDTO.LastUpdateByID));
            }
            if (DashboardMetricDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardMetricXPO.IsActive), DashboardMetricDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
