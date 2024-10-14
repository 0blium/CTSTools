using Elmah;
using System;
using System.Linq;
using DevExpress.Data.Filtering;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
public class Decoder_DXFilter
{
    public static GroupOperator GetDecoder_DXFilter(DecoderDTO DecoderDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (DecoderDTO.ID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.Oid), DecoderDTO.ID));
            }
            if (DecoderDTO.DecoderIDArray != null && DecoderDTO.DecoderIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderXPO.Oid), DecoderDTO.DecoderIDArray));
            }
            if (DecoderDTO.StatusID != null || DecoderDTO.StatusID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.Status), DecoderDTO.StatusDTO.ID));
            }
            if (DecoderDTO.PartTypeID != null || DecoderDTO.PartTypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.PartType), DecoderDTO.PartTypeID));
            }
            if (DecoderDTO.ClassID != null || DecoderDTO.ClassID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.Class), DecoderDTO.ClassID));
            }
            if (DecoderDTO.ComponentTypeID != null || DecoderDTO.ComponentTypeID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.ComponentType), DecoderDTO.ComponentTypeID));
            }
            if (DecoderDTO.SubClassID != null || DecoderDTO.SubClassID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.SubClass), DecoderDTO.SubClassID));
            }
            if (DecoderDTO.StatusIDArray != null && DecoderDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderXPO.Status), DecoderDTO.StatusIDArray));
            }
            if (DecoderDTO.StatusIDArray != null && DecoderDTO.StatusIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(DecoderXPO.Status), DecoderDTO.StatusIDArray));
            }
            //if (DecoderDTO.AddedByID != null && DecoderDTO.AddedByID > 0)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.AddedBy), DecoderDTO.AddedBy));
            //}
            //if (DecoderDTO.LastUpdateByID != null && DecoderDTO.LastUpdateByID > 0)
            //{
            //    _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.LastUpdateBy), DecoderDTO.LastUpdateBy));
            //}
            if (DecoderDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(DecoderXPO.IsActive), DecoderDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}

