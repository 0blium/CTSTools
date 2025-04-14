using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

internal class Supplier_DXFilter
{
    public static GroupOperator GetSupplier_DXFilter(SupplierDTO SupplierDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SupplierDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.Oid), SupplierDTO.ID));
            if (!string.IsNullOrEmpty(SupplierDTO.Name))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.Name), SupplierDTO.Name));
            if (SupplierDTO.SupplierIDArray != null && SupplierDTO.SupplierIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SupplierXPO.Oid), SupplierDTO.SupplierIDArray));
            if (SupplierDTO.SupplierNameArray != null && SupplierDTO.SupplierNameArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SupplierXPO.Name), SupplierDTO.SupplierNameArray));
            if (SupplierDTO.AddedByID != null && SupplierDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.AddedBy), SupplierDTO.AddedByID));
            if (SupplierDTO.LastUpdateByID != null && SupplierDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.LastUpdateBy), SupplierDTO.LastUpdateByID));
            if (SupplierDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.IsActive), SupplierDTO.IsActive));
            if (SupplierDTO.IsVendor != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.IsVendor), SupplierDTO.IsVendor));
            if (SupplierDTO.IsManufacturer != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SupplierXPO.IsManufacturer), SupplierDTO.IsManufacturer));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }

}
