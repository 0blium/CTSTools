using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;

public class Dashboard_DXFilter
{
    public static GroupOperator GetDashboard_DXFilter(DashboardDTO DashboardDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DashboardDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.Oid), DashboardDTO.ID));
            }
            if (DashboardDTO.DashboardIDArray != null && DashboardDTO.DashboardIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardXPO.Oid), DashboardDTO.DashboardIDArray));
            }
            if (DashboardDTO.OwnerID != null || DashboardDTO.OwnerID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.Owner), DashboardDTO.OwnerID));
            }
            if (DashboardDTO.DepartmentID != null || DashboardDTO.DepartmentID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.Department), DashboardDTO.DepartmentID));
            }
            if (DashboardDTO.DepartmentIDArray != null && DashboardDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardXPO.Department), DashboardDTO.DepartmentIDArray));
            }
            if (DashboardDTO.LevelID != null || DashboardDTO.LevelID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.Level), DashboardDTO.LevelID));
            }
            if (DashboardDTO.GoalRangeID != null || DashboardDTO.GoalRangeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.GoalRange), DashboardDTO.GoalRangeID));
            }
            if (DashboardDTO.LevelIDArray != null && DashboardDTO.LevelIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardXPO.Level), DashboardDTO.LevelIDArray));
            }
            if (DashboardDTO.StatusID != null || DashboardDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.Status), DashboardDTO.StatusID));
            }
            if (DashboardDTO.StatusIDArray != null && DashboardDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DashboardXPO.Status), DashboardDTO.StatusIDArray));
            }
            if (DashboardDTO.AddedByID != null && DashboardDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.AddedBy), DashboardDTO.AddedByID));
            }
            if (DashboardDTO.LastUpdateByID != null && DashboardDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.LastUpdateBy), DashboardDTO.LastUpdateByID));
            }
            if (DashboardDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DashboardXPO.IsActive), DashboardDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
