using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.GoalRange;

public class GoalRange_DXFilter
{
    public static GroupOperator GetGoalRange_DXFilter(GoalRangeDTO GoalRangeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (GoalRangeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(GoalRangeXPO.Oid), GoalRangeDTO.ID));
            }
            if (GoalRangeDTO.Value > 0.0f)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(GoalRangeXPO.Value), GoalRangeDTO.Value));
            }
            if (GoalRangeDTO.GoalRangeIDArray != null && GoalRangeDTO.GoalRangeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(GoalRangeXPO.Oid), GoalRangeDTO.GoalRangeIDArray));
            }
            if (GoalRangeDTO.AddedByID != null && GoalRangeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(GoalRangeXPO.AddedBy), GoalRangeDTO.AddedByID));
            }
            if (GoalRangeDTO.LastUpdateByID != null && GoalRangeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(GoalRangeXPO.LastUpdateBy), GoalRangeDTO.LastUpdateByID));
            }
            if (GoalRangeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(GoalRangeXPO.IsActive), GoalRangeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
