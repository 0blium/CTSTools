using CTSTools.DAL.Features.Ticket.Provider;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.Provider;

public class Provider_DXFilter
{
    public static GroupOperator GetProvider_DXFilter(ProviderDTO ProviderDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ProviderDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProviderXPO.Oid), ProviderDTO.ID));
            }
            if (!string.IsNullOrEmpty(ProviderDTO.Name))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProviderXPO.Name), ProviderDTO.Name));
            }
            if (ProviderDTO.ProviderIDArray != null && ProviderDTO.ProviderIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(ProviderXPO.Oid), ProviderDTO.ProviderIDArray));
            }
            if (ProviderDTO.AddedByID != null && ProviderDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProviderXPO.AddedBy), ProviderDTO.AddedByName));
            }
            if (ProviderDTO.LastUpdateByID != null && ProviderDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProviderXPO.LastUpdateBy), ProviderDTO.LastUpdateByName));
            }
            if (ProviderDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProviderXPO.IsActive), ProviderDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
