using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.Customer;

public class Customer_DXFilter
{
    public static GroupOperator GetDXFilter(CustomerDTO CustomerDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (CustomerDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CustomerXPO.Oid), CustomerDTO.ID));
            if (CustomerDTO.CustomerIDArray != null && CustomerDTO.CustomerIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(CustomerXPO.Oid), CustomerDTO.CustomerIDArray));
            if (CustomerDTO.AddedByID != null && CustomerDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CustomerXPO.AddedBy), CustomerDTO.AddedByID));
            if (CustomerDTO.LastUpdateByID != null && CustomerDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CustomerXPO.LastUpdateBy), CustomerDTO.LastUpdateByID));
            if (CustomerDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CustomerXPO.IsActive), CustomerDTO.IsActive));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
