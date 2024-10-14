using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_Permission_DXFilter
{
    public static GroupOperator GetUser_Permission_DXFilter(User_PermissionDTO User_PermissionDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (User_PermissionDTO.ID > 0)

                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_PermissionXPO.Oid), User_PermissionDTO.ID));

            if (User_PermissionDTO.User_PermissionIDArray != null && User_PermissionDTO.User_PermissionIDArray.Count() > 0)

                _groupOperator.Operands.Add(new InOperator(nameof(User_PermissionXPO.Oid), User_PermissionDTO.User_PermissionIDArray));

            if (User_PermissionDTO.PermissionID != null || User_PermissionDTO.PermissionID > 0)

                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_PermissionXPO.Permission), User_PermissionDTO.PermissionID));

            if (User_PermissionDTO.PermissionIDArray != null && User_PermissionDTO.PermissionIDArray.Count() > 0)

                _groupOperator.Operands.Add(new InOperator(nameof(User_PermissionXPO.Permission), User_PermissionDTO.PermissionIDArray));

            if (User_PermissionDTO.UserID != null || User_PermissionDTO.UserID > 0)

                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_PermissionXPO.User), User_PermissionDTO.UserID));

            if (User_PermissionDTO.UserIDArray != null && User_PermissionDTO.UserIDArray.Count() > 0)

                _groupOperator.Operands.Add(new InOperator(nameof(User_PermissionXPO.User), User_PermissionDTO.UserIDArray));

            if (User_PermissionDTO.AddedByID != null && User_PermissionDTO.AddedByID > 0)

                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_PermissionXPO.AddedBy), User_PermissionDTO.AddedByID));

            if (User_PermissionDTO.LastUpdateByID != null && User_PermissionDTO.AddedByID > 0)

                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_PermissionXPO.LastUpdateBy), User_PermissionDTO.LastUpdateByID));

            if (User_PermissionDTO.IsActive != null)

                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_PermissionXPO.IsActive), User_PermissionDTO.IsActive));


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
