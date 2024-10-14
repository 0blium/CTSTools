using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;

class Facility_DXFilter
{
    public static GroupOperator GetFacilityFilters(FacilityDTO FacilityDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (FacilityDTO.ID > 0 || FacilityDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(FacilityXPO.Oid), FacilityDTO.ID));
            }
            if (FacilityDTO.FacilityIDArray != null && FacilityDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(FacilityXPO.Oid), FacilityDTO.FacilityIDArray));
            }
            if (FacilityDTO.AddedByID != null || FacilityDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(FacilityXPO.AddedBy), FacilityDTO.AddedByID));
            }
            if (FacilityDTO.LastUpdateByID != null || FacilityDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(FacilityXPO.LastUpdateBy), FacilityDTO.LastUpdateByID));
            }
            if (FacilityDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(FacilityXPO.IsActive), FacilityDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
