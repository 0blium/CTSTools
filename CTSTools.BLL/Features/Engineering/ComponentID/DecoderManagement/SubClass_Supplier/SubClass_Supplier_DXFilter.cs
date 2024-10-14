using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;

internal class SubClass_Supplier_DXFilter{
    public static GroupOperator GetSubClass_Supplier_DXFilter(SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SubClass_SupplierDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClass_SupplierXPO.Oid), SubClass_SupplierDTO.ID));
            if (SubClass_SupplierDTO.SupplierIDArray != null && SubClass_SupplierDTO.SupplierIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SubClass_SupplierXPO.Oid), SubClass_SupplierDTO.SupplierIDArray));
            if (SubClass_SupplierDTO.SupplierID != null && SubClass_SupplierDTO.SupplierID != 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClass_SupplierXPO.Supplier), SubClass_SupplierDTO.SupplierID));
            if (SubClass_SupplierDTO.AddedByID != null && SubClass_SupplierDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClass_SupplierXPO.AddedBy), SubClass_SupplierDTO.AddedByID));
            if (SubClass_SupplierDTO.LastUpdateByID != null && SubClass_SupplierDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClass_SupplierXPO.LastUpdateBy), SubClass_SupplierDTO.LastUpdateByID));
            if (SubClass_SupplierDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClass_SupplierXPO.IsActive), SubClass_SupplierDTO.SubClassID));
            if (SubClass_SupplierDTO.SubClassID != null && SubClass_SupplierDTO.SubClassID != 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SubClass_SupplierXPO.SubClass), SubClass_SupplierDTO.SubClassID));
            if (SubClass_SupplierDTO.SubClassIDArray != null && SubClass_SupplierDTO.SubClassIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(SubClass_SupplierXPO.SubClass), SubClass_SupplierDTO.SubClassIDArray));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }

}
