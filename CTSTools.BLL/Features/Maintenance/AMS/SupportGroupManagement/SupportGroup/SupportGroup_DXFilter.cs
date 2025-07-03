using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;

public class SupportGroup_DXFilter
{
    public static GroupOperator GetSupportGroup_DXFilter(SupportGroupDTO SupportGroupDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SupportGroupDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupXPO.Oid), SupportGroupDTO.ID));
            if (SupportGroupDTO.SupportGroupIDArray != null && SupportGroupDTO.SupportGroupIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupXPO.Oid), SupportGroupDTO.SupportGroupIDArray));
            if (SupportGroupDTO.SupportGroupNameArray != null && SupportGroupDTO.SupportGroupNameArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupXPO.EnglishName), SupportGroupDTO.SupportGroupNameArray));
            if (SupportGroupDTO.FacilityDTO.ID != null || SupportGroupDTO.FacilityDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupXPO.Facility), SupportGroupDTO.FacilityDTO.ID));
            if (SupportGroupDTO.FacilityIDArray != null && SupportGroupDTO.FacilityIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SupportGroupXPO.Facility), SupportGroupDTO.FacilityIDArray));
            if (SupportGroupDTO.AddedByID != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupXPO.AddedBy), SupportGroupDTO.AddedByID));
            if (SupportGroupDTO.LastUpdateByID != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupXPO.LastUpdateBy), SupportGroupDTO.LastUpdateByID));
            if (SupportGroupDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupportGroupXPO.IsActive), SupportGroupDTO.IsActive));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
