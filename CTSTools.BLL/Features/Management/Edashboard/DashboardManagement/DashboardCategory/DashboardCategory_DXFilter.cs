using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;

public class DashboardCategory_DXFilter
{
    public static GroupOperator GetDashboardCategory_DXFilter(DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DashboardCategoryDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardCategoryXPO.Oid), DashboardCategoryDTO.ID));
            }
            if (!string.IsNullOrEmpty(DashboardCategoryDTO.Name))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardCategoryXPO.Name), DashboardCategoryDTO.Name));
            }
            if (DashboardCategoryDTO.DashboardCategoryIDArray != null && DashboardCategoryDTO.DashboardCategoryIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardCategoryXPO.Oid), DashboardCategoryDTO.DashboardCategoryIDArray));
            }
            if (DashboardCategoryDTO.AddedByID != null && DashboardCategoryDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardCategoryXPO.AddedBy), DashboardCategoryDTO.AddedByID));
            }
            if (DashboardCategoryDTO.LastUpdateByID != null && DashboardCategoryDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardCategoryXPO.LastUpdateBy), DashboardCategoryDTO.LastUpdateByID));
            }
            if (DashboardCategoryDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardCategoryXPO.IsActive), DashboardCategoryDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
