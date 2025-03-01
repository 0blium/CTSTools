using CTSTools.DAL.Features.Currency;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Currency;

public class Currency_DXFilter
{
    public static GroupOperator GetCurrency_DXFilter(CurrencyDTO CurrencyDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (CurrencyDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CurrencyXPO.Oid), CurrencyDTO.ID));
            }
            if (CurrencyDTO.CurrencyIDArray != null && CurrencyDTO.CurrencyIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(CurrencyXPO.Oid), CurrencyDTO.CurrencyIDArray));
            }
            if (CurrencyDTO.AddedByID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CurrencyXPO.AddedBy), CurrencyDTO.AddedByID));
            }
            if (CurrencyDTO.LastUpdateByID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CurrencyXPO.LastUpdateBy), CurrencyDTO.LastUpdateByID));
            }
            if (CurrencyDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(CurrencyXPO.IsActive), CurrencyDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
