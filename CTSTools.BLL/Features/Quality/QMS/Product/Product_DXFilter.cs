using CTSTools.BLL.Features.Quality.QMS.Product;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.Product;

public class Product_DXFilter
{
    public static GroupOperator GetProduct_DXFilter(ProductDTO ProductDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (ProductDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProductXPO.Oid), ProductDTO.ID));
            if (ProductDTO.ProductIDArray != null && ProductDTO.ProductIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(ProductXPO.Oid), ProductDTO.ProductIDArray));
            if (ProductDTO.AddedByID != null && ProductDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProductXPO.AddedBy), ProductDTO.AddedByID));
            if (ProductDTO.LastUpdateByID != null && ProductDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProductXPO.LastUpdateBy), ProductDTO.LastUpdateByID));
            if (ProductDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(ProductXPO.IsActive), ProductDTO.IsActive));

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
