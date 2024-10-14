using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.Security.Role;

namespace CTSTools.BLL.Features.Security.Roles.RoleType;

public class RoleType_DXFilter
{
    public static GroupOperator GetRoleType_DXFilter(RoleTypeDTO RoleTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (RoleTypeDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleTypeXPO.Oid), RoleTypeDTO.ID));
            if (RoleTypeDTO.RoleTypeIDArray != null && RoleTypeDTO.RoleTypeIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(RoleTypeXPO.Oid), RoleTypeDTO.RoleTypeIDArray));
            if (RoleTypeDTO.AddedByID != null && RoleTypeDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleTypeXPO.AddedBy), RoleTypeDTO.AddedByID));
            if (RoleTypeDTO.LastUpdateByID != null && RoleTypeDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleTypeXPO.LastUpdateBy), RoleTypeDTO.LastUpdateByID));
            if (RoleTypeDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(RoleTypeXPO.IsActive), RoleTypeDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
