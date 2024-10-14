using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
public class DecoderStructure_DXFilter
{
    public static GroupOperator GetDecoderStructure_DXFilter(DecoderStructureDTO DecoderStructureDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DecoderStructureDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.Oid), DecoderStructureDTO.ID));
            if (DecoderStructureDTO.DecoderStructureIDArray != null && DecoderStructureDTO.DecoderStructureIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderStructureXPO.Oid), DecoderStructureDTO.DecoderStructureIDArray));
            if (DecoderStructureDTO.DecoderID != null || DecoderStructureDTO.DecoderID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.Decoder), DecoderStructureDTO.DecoderID));
            if (DecoderStructureDTO.DecoderIDArray != null && DecoderStructureDTO.DecoderIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderStructureXPO.Decoder), DecoderStructureDTO.DecoderIDArray));
            if (DecoderStructureDTO.ValueID != null || DecoderStructureDTO.ValueID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.Value), DecoderStructureDTO.ValueID));
            if (DecoderStructureDTO.ValueIDArray != null && DecoderStructureDTO.ValueIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderStructureXPO.Value), DecoderStructureDTO.ValueIDArray));
            if (DecoderStructureDTO.AttributeID != null || DecoderStructureDTO.AttributeID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.Attribute), DecoderStructureDTO.AttributeID));
            if (DecoderStructureDTO.AttributeIDArray != null && DecoderStructureDTO.AttributeIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderStructureXPO.Attribute), DecoderStructureDTO.AttributeIDArray));
            if (DecoderStructureDTO.DescriptionBody != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.DescriptionBody), DecoderStructureDTO.DescriptionBody));
            if (DecoderStructureDTO.DescriptionOrder != 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.DescriptionOrder), DecoderStructureDTO.DescriptionOrder));
            if (DecoderStructureDTO.NumberBody != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.NumberBody), DecoderStructureDTO.NumberBody));
            if (DecoderStructureDTO.NumberOrder != 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.NumberOrder), DecoderStructureDTO.NumberOrder));
            if (DecoderStructureDTO.AddedByID != null && DecoderStructureDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.AddedBy), DecoderStructureDTO.AddedByID));
            if (DecoderStructureDTO.LastUpdateByID != null && DecoderStructureDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderStructureXPO.LastUpdateBy), DecoderStructureDTO.LastUpdateByID));


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
