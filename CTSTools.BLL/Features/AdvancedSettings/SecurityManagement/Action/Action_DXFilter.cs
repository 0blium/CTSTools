using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;

public class Action_DXFilter
{
    public static GroupOperator GetAction_DXFilter(ActionDTO ActionDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ActionDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ActionXPO.Oid), ActionDTO.ID));
            if (ActionDTO.ActionIDArray != null && ActionDTO.ActionIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(ActionXPO.Oid), ActionDTO.ActionIDArray));
            if (ActionDTO.AddedByID != null && ActionDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ActionXPO.AddedBy), ActionDTO.AddedByID));
            if (ActionDTO.LastUpdateByID != null && ActionDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ActionXPO.LastUpdateBy), ActionDTO.LastUpdateByID));
            if (ActionDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ActionXPO.IsActive), ActionDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
