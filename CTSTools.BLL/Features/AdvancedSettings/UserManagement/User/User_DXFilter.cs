using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;

public class User_DXFilter
{
    public static GroupOperator GetUserFilters(UserDTO UserDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (UserDTO.ID > 0 || UserDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.Oid), UserDTO.ID));
            }
            if (UserDTO.Login != null && UserDTO.Login != string.Empty)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.Login), UserDTO.Login));
            }
            if (UserDTO.UserIDArray != null && UserDTO.UserIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserXPO.Oid), UserDTO.UserIDArray));
            }                
            if (UserDTO.AddedByID != null && UserDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.AddedBy), UserDTO.AddedByID));
            }
            if (UserDTO.LastUpdateByID != null && UserDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.LastUpdateBy), UserDTO.LastUpdateByID));
            }
            if (UserDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.IsActive), UserDTO.IsActive));
            }
            if (UserDTO.FacilityID != null || UserDTO.FacilityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.Facility), UserDTO.FacilityID));
            }
            if (UserDTO.FacilityIDArray != null && UserDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserXPO.Facility), UserDTO.FacilityIDArray));
            }                     
            if (UserDTO.DepartmentID != null || UserDTO.DepartmentID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(UserXPO.Department), UserDTO.DepartmentID));
            }
            if (UserDTO.DepartmentIDArray != null && UserDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(UserXPO.Department), UserDTO.DepartmentIDArray));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
