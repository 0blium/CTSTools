using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

namespace CTSTools.BLL.Features.Security.Permissions.Role_Permission;

public class Role_Permission_DXFilter
{
    public static GroupOperator GetRole_Permission_DXFilter(Role_PermissionDTO Role_PermissionDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Role_PermissionDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Role_PermissionXPO.Oid), Role_PermissionDTO.ID));
            if (Role_PermissionDTO.Role_PermissionIDArray != null && Role_PermissionDTO.Role_PermissionIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Role_PermissionXPO.Oid), Role_PermissionDTO.Role_PermissionIDArray));
            if (Role_PermissionDTO.PermissionID != null || Role_PermissionDTO.PermissionID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Role_PermissionXPO.Permission), Role_PermissionDTO.PermissionID));
            if (Role_PermissionDTO.PermissionIDArray != null && Role_PermissionDTO.PermissionIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Role_PermissionXPO.Permission), Role_PermissionDTO.PermissionIDArray));
            if (Role_PermissionDTO.RoleID != null || Role_PermissionDTO.RoleID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Role_PermissionXPO.Role), Role_PermissionDTO.RoleID));
            if (Role_PermissionDTO.RoleIDArray != null && Role_PermissionDTO.RoleIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Role_PermissionXPO.Role), Role_PermissionDTO.RoleIDArray));
            if (Role_PermissionDTO.AddedByID != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Role_PermissionXPO.AddedBy), Role_PermissionDTO.AddedByID));
            if (Role_PermissionDTO.LastUpdateByID != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Role_PermissionXPO.LastUpdateBy), Role_PermissionDTO.LastUpdateByID));
            if (Role_PermissionDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Role_PermissionXPO.IsActive), Role_PermissionDTO.IsActive));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
