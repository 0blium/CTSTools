using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;

public class User_Role_DXFilter
{
    public static GroupOperator GetUser_Role_DXFilter(User_RoleDTO User_RoleDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (User_RoleDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_RoleXPO.Oid), User_RoleDTO.ID));

            if (User_RoleDTO.User_RoleIDArray != null && User_RoleDTO.User_RoleIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(User_RoleXPO.Oid), User_RoleDTO.User_RoleIDArray));

            if (User_RoleDTO.RoleID != null || User_RoleDTO.RoleID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_RoleXPO.Role), User_RoleDTO.RoleID));

            if (User_RoleDTO.RoleIDArray != null && User_RoleDTO.RoleIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(User_RoleXPO.Role), User_RoleDTO.RoleIDArray));

            if (User_RoleDTO.UserID != null || User_RoleDTO.UserID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_RoleXPO.User), User_RoleDTO.UserID));

            if (User_RoleDTO.UserIDArray != null && User_RoleDTO.UserIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(User_RoleXPO.User), User_RoleDTO.UserIDArray));

            if (User_RoleDTO.AddedByID != null && User_RoleDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_RoleXPO.AddedBy), User_RoleDTO.AddedByID));

            if (User_RoleDTO.LastUpdateByID != null && User_RoleDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_RoleXPO.LastUpdateBy), User_RoleDTO.LastUpdateByID));

            if (User_RoleDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(User_RoleXPO.IsActive), User_RoleDTO.IsActive));


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
