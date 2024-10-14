using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.Domain;

public class Domain_DXFilter
{
    public static GroupOperator GetDomainFilters(DomainDTO DomainDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DomainDTO.ID > 0 && DomainDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DomainXPO.Oid), DomainDTO.ID));
            }
            if (DomainDTO.DomainIDArray != null && DomainDTO.DomainIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DomainXPO.Oid), DomainDTO.DomainIDArray));
            }
            if (DomainDTO.FacilityID != null || DomainDTO.FacilityID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DomainXPO.Facility), DomainDTO.FacilityID));
            }
            if (DomainDTO.AddedByID != null || DomainDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DomainXPO.AddedBy), DomainDTO.AddedByID));
            }
            if (DomainDTO.LastUpdateByID != null || DomainDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DomainXPO.LastUpdateBy), DomainDTO.LastUpdateByID));
            }
            if (DomainDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DomainXPO.IsActive), DomainDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}

