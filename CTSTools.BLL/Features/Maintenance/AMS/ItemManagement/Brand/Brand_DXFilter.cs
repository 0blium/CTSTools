using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand
{
    public class Brand_DXFilter
    {
        public static GroupOperator GetBrand_DXFilter(BrandDTO BrandDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                if (BrandDTO.ID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(BrandXPO.Oid), BrandDTO.ID));
                if (!string.IsNullOrEmpty(BrandDTO.Name))
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(BrandXPO.Name), BrandDTO.Name));
                if (BrandDTO.BrandIDArray != null && BrandDTO.BrandIDArray.Count() > 0)
                    _groupOperator.Operands.Add(new InOperator(nameof(BrandXPO.Oid), BrandDTO.BrandIDArray));
                if (BrandDTO.AddedByID != null && BrandDTO.AddedByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(BrandXPO.AddedBy), BrandDTO.AddedByID));
                if (BrandDTO.LastUpdateByID != null && BrandDTO.LastUpdateByID > 0)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(BrandXPO.LastUpdateBy), BrandDTO.LastUpdateByID));
                if (BrandDTO.IsActive != null)
                    _groupOperator.Operands.Add(new BinaryOperator(nameof(BrandXPO.IsActive), BrandDTO.IsActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }
    }
}
