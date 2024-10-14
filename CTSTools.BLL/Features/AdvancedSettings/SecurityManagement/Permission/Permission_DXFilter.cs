using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;

namespace CTSTools.BLL.Features.Security.Permissions.Permission;

public class Permission_DXFilter
{
    public static GroupOperator GetPermission_DXFilter(PermissionDTO PermissionDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (PermissionDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PermissionXPO.Oid), PermissionDTO.ID));
            if (!string.IsNullOrEmpty(PermissionDTO.Module))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PermissionXPO.Module), PermissionDTO.Module));
            if (PermissionDTO.PermissionIDArray != null && PermissionDTO.PermissionIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(PermissionXPO.Oid), PermissionDTO.PermissionIDArray));
            if (PermissionDTO.ActionID != null || PermissionDTO.ActionID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PermissionXPO.Action), PermissionDTO.ActionID));
            if (PermissionDTO.ActionIDArray != null && PermissionDTO.ActionIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(PermissionXPO.Action), PermissionDTO.ActionIDArray));
            if (PermissionDTO.AddedByID != null && PermissionDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PermissionXPO.AddedBy), PermissionDTO.AddedByID));
            if (PermissionDTO.LastUpdateByID != null && PermissionDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PermissionXPO.LastUpdateBy), PermissionDTO.LastUpdateByID));
            if (PermissionDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PermissionXPO.IsActive), PermissionDTO.IsActive));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
