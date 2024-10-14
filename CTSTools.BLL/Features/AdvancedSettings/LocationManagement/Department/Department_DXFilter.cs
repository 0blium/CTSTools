using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;

public class Department_DXFilter
{
    public static GroupOperator GetDepartmentFilters(DepartmentDTO DepartmentDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DepartmentDTO.ID > 0 || DepartmentDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentXPO.Oid), DepartmentDTO.ID));
            }
            if (DepartmentDTO.DepartmentIDArray != null && DepartmentDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DepartmentXPO.Oid), DepartmentDTO.DepartmentIDArray));
            }
            if (DepartmentDTO.FacilityDTO.ID != null || DepartmentDTO.FacilityDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentXPO.Facility), DepartmentDTO.FacilityDTO.ID));
            }
            if (DepartmentDTO.FacilityIDArray != null && DepartmentDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DepartmentXPO.Facility), DepartmentDTO.FacilityIDArray));
            }
            if (DepartmentDTO.FacilityID != null || DepartmentDTO.FacilityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentXPO.Facility), DepartmentDTO.FacilityID));
            }
            if (DepartmentDTO.FacilityIDArray != null && DepartmentDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DepartmentXPO.Facility), DepartmentDTO.FacilityIDArray));
            }
            if (DepartmentDTO.AddedByID != null || DepartmentDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentXPO.AddedBy), DepartmentDTO.AddedByID));
            }
            if (DepartmentDTO.LastUpdateByID != null || DepartmentDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentXPO.LastUpdateBy), DepartmentDTO.LastUpdateByID));
            }
            if (DepartmentDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DepartmentXPO.IsActive), DepartmentDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
