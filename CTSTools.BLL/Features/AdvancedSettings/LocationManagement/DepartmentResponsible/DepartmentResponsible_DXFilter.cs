using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;

public class DepartmentResponsible_DXFilter
{
    public static GroupOperator GetDepartmentResponsibleFilters(DepartmentResponsibleDTO DepartmentResponsibleDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DepartmentResponsibleDTO.ID > 0 || DepartmentResponsibleDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentResponsibleXPO.Oid), DepartmentResponsibleDTO.ID));
            }
            if (DepartmentResponsibleDTO.ResponsibleDTO.ID != null || DepartmentResponsibleDTO.ResponsibleDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentResponsibleXPO.Responsible), DepartmentResponsibleDTO.ResponsibleDTO.ID));
            }
            if (DepartmentResponsibleDTO.DepartmentDTO.ID != null || DepartmentResponsibleDTO.DepartmentDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentResponsibleXPO.Department), DepartmentResponsibleDTO.DepartmentDTO.ID));
            }
            if(DepartmentResponsibleDTO.DepartmentIDArray != null && DepartmentResponsibleDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DepartmentResponsibleXPO.Department), DepartmentResponsibleDTO.DepartmentIDArray));
            }
            if (DepartmentResponsibleDTO.AddedByID != null || DepartmentResponsibleDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentResponsibleXPO.AddedBy), DepartmentResponsibleDTO.AddedByID));
            }
            if (DepartmentResponsibleDTO.LastUpdateByID != null || DepartmentResponsibleDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentResponsibleXPO.LastUpdateBy), DepartmentResponsibleDTO.LastUpdateByID));
            }
            if (DepartmentResponsibleDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentResponsibleXPO.IsActive), DepartmentResponsibleDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
