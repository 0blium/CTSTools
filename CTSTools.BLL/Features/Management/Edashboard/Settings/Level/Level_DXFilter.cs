using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.Level;

public class Level_DXFilter
{
    public static GroupOperator GetLevel_DXFilter(LevelDTO LevelDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (LevelDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(LevelXPO.Oid), LevelDTO.ID));
            }
            if (LevelDTO.LevelIDArray != null && LevelDTO.LevelIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(LevelXPO.Oid), LevelDTO.LevelIDArray));
            }
            if (LevelDTO.AddedByID != null && LevelDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(LevelXPO.AddedBy), LevelDTO.AddedByID));
            }
            if (LevelDTO.LastUpdateByID != null && LevelDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(LevelXPO.LastUpdateBy), LevelDTO.LastUpdateByID));
            }
            if (LevelDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(LevelXPO.IsActive), LevelDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
