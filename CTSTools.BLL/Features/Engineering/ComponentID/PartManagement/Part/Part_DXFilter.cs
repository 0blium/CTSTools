using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;

public class Part_DXFilter
{
    public static GroupOperator GetPart_DXFilter(PartDTO PartDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (PartDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.Oid), PartDTO.ID));
            if (!string.IsNullOrEmpty(PartDTO.Description))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.Description), PartDTO.Description));
            if (!string.IsNullOrEmpty(PartDTO.Number))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.Number), PartDTO.Number));
            if (PartDTO.SupplierID != null && PartDTO.SupplierID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.Supplier), PartDTO.SupplierID));
            if (PartDTO.DecoderID != null && PartDTO.DecoderID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.Decoder), PartDTO.DecoderID));
            if (!string.IsNullOrEmpty(PartDTO.MfgPartNumber))
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.MfgPartNumber), PartDTO.MfgPartNumber));
            if (PartDTO.MfgPartNumberArray != null && PartDTO.MfgPartNumberArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(PartXPO.MfgPartNumber), PartDTO.MfgPartNumberArray));
            if (PartDTO.PartIDArray != null && PartDTO.PartIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(PartXPO.Oid), PartDTO.PartIDArray));
            if (PartDTO.SupplierIDArray != null && PartDTO.SupplierIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(PartXPO.Supplier), PartDTO.SupplierIDArray));
            if (PartDTO.DecoderIDArray != null && PartDTO.DecoderIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(PartXPO.Decoder), PartDTO.DecoderIDArray));
            if (PartDTO.AddedByID != null && PartDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.AddedBy), PartDTO.AddedByID));
            if (PartDTO.LastUpdateByID != null && PartDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.LastUpdateBy), PartDTO.LastUpdateByID));
            if (PartDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(PartXPO.IsActive), PartDTO.IsActive));              
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
