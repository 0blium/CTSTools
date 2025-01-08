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
            if (DashboardLineDTO.Dashboard_KPIID != null || DashboardLineDTO.Dashboard_KPIID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Dashboard_KPI), DashboardLineDTO.Dashboard_KPIID));
            }
            if (DashboardLineDTO.Dashboard_KPIIDArray != null && DashboardLineDTO.Dashboard_KPIIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.Dashboard_KPI), DashboardLineDTO.Dashboard_KPIIDArray));
            }
            if (DashboardLineDTO.KPIID != null || DashboardLineDTO.KPIID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.KPI), DashboardLineDTO.KPIID));
            }
            if (DashboardLineDTO.KPIIDArray != null && DashboardLineDTO.KPIIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.KPI), DashboardLineDTO.KPIIDArray));
            }
            if (DashboardLineDTO.DashboardCategoryID != null || DashboardLineDTO.DashboardCategoryID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.DashboardCategory), DashboardLineDTO.DashboardCategoryID));
            }
            if (DashboardLineDTO.DashboardCategoryIDArray != null && DashboardLineDTO.DashboardCategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardLineXPO.DashboardCategory), DashboardLineDTO.DashboardCategoryIDArray));
            }
            if (DashboardLineDTO.DashboardID != null || DashboardLineDTO.DashboardID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Dashboard), DashboardLineDTO.DashboardID));
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
            //if (DashboardLineDTO.IgnoreKPI != null)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.IgnoreKPI), DashboardLineDTO.IgnoreKPI));
            //}
            //if (DashboardLineDTO.Validated != null)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.Validated), DashboardLineDTO.Validated));
            //}
            if (DashboardLineDTO.ValidatedByID != null || DashboardLineDTO.ValidatedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardLineXPO.ValidatedBy), DashboardLineDTO.ValidatedByID));
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
