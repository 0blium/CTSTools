using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class Dashboard_KPI_DXFilter
{
    public static GroupOperator GetDashboard_KPI_DXFilter(Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Dashboard_KPIDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.Oid), Dashboard_KPIDTO.ID));
            }
            if (Dashboard_KPIDTO.Dashboard_KPIIDArray != null && Dashboard_KPIDTO.Dashboard_KPIIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Dashboard_KPIXPO.Oid), Dashboard_KPIDTO.Dashboard_KPIIDArray));
            }
            if (Dashboard_KPIDTO.StatusID != null || Dashboard_KPIDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.Status), Dashboard_KPIDTO.StatusID));
            }
            if (Dashboard_KPIDTO.StatusIDArray != null && Dashboard_KPIDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Dashboard_KPIXPO.Status), Dashboard_KPIDTO.StatusIDArray));
            }
            if (Dashboard_KPIDTO.DashboardID != null || Dashboard_KPIDTO.DashboardID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.Dashboard), Dashboard_KPIDTO.DashboardID));
            }
            if (Dashboard_KPIDTO.DashboardIDArray != null && Dashboard_KPIDTO.DashboardIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Dashboard_KPIXPO.Dashboard), Dashboard_KPIDTO.DashboardIDArray));
            }
            if (Dashboard_KPIDTO.KPIID != null || Dashboard_KPIDTO.KPIID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.KPI), Dashboard_KPIDTO.KPIID));
            }
            if (Dashboard_KPIDTO.KPIIDArray != null && Dashboard_KPIDTO.KPIIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Dashboard_KPIXPO.KPI), Dashboard_KPIDTO.KPIIDArray));
            }
            if (Dashboard_KPIDTO.Order != null && Dashboard_KPIDTO.Order > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.Order), Dashboard_KPIDTO.Order));
            }
            if (Dashboard_KPIDTO.DashboardCategoryID != null && Dashboard_KPIDTO.DashboardCategoryID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.DashboardCategory), Dashboard_KPIDTO.DashboardCategoryID));
            }
            if (Dashboard_KPIDTO.DashboardCategoryIDArray != null && Dashboard_KPIDTO.DashboardCategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(Dashboard_KPIXPO.DashboardCategory), Dashboard_KPIDTO.DashboardCategoryIDArray));
            }
            if (Dashboard_KPIDTO.AddedByID != null && Dashboard_KPIDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.AddedBy), Dashboard_KPIDTO.AddedByID));
            }
            if (Dashboard_KPIDTO.LastUpdateByID != null && Dashboard_KPIDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.LastUpdateBy), Dashboard_KPIDTO.LastUpdateByID));
            }
            if (Dashboard_KPIDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Dashboard_KPIXPO.IsActive), Dashboard_KPIDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
