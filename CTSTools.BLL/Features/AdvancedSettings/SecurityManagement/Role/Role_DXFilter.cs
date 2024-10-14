using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;

public class Role_DXFilter
{
    public static GroupOperator GetRole_DXFilter(RoleDTO RoleDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (RoleDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleXPO.Oid), RoleDTO.ID));
            if (RoleDTO.RoleIDArray != null && RoleDTO.RoleIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(RoleXPO.Oid), RoleDTO.RoleIDArray));
            if (RoleDTO.RoleTypeIDArray != null && RoleDTO.RoleTypeIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(RoleXPO.RoleType), RoleDTO.RoleTypeIDArray));
            if (RoleDTO.AddedByID != null && RoleDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleXPO.AddedBy), RoleDTO.AddedByID));
            if (RoleDTO.LastUpdateByID != null && RoleDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleXPO.LastUpdateBy), RoleDTO.LastUpdateByID));
            if (RoleDTO.RoleTypeID != null && RoleDTO.RoleTypeID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleXPO.RoleType), RoleDTO.RoleTypeID));
            if (RoleDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleXPO.IsActive), RoleDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
