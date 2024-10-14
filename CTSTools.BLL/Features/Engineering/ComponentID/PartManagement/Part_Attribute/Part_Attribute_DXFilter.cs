using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;

public class Part_Attribute_DXFilter
{
    public static GroupOperator GetPart_Attribute_DXFilter(Part_AttributeDTO Part_AttributeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (Part_AttributeDTO.ID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.Oid), Part_AttributeDTO.ID));       
            
            if (Part_AttributeDTO.AttributeID != null && Part_AttributeDTO.AttributeID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.Attribute), Part_AttributeDTO.AttributeID));
            if (Part_AttributeDTO.DecoderID != null && Part_AttributeDTO.DecoderID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.Decoder), Part_AttributeDTO.DecoderID));
            if (Part_AttributeDTO.ValueID != null && Part_AttributeDTO.ValueID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.Value), Part_AttributeDTO.ValueID));
            if (Part_AttributeDTO.PartID != null && Part_AttributeDTO.PartID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.Part), Part_AttributeDTO.PartID));
            if (Part_AttributeDTO.PartIDArray != null && Part_AttributeDTO.PartIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Part_AttributeXPO.Oid), Part_AttributeDTO.PartIDArray));          
            if (Part_AttributeDTO.DecoderIDArray != null && Part_AttributeDTO.DecoderIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Part_AttributeXPO.Decoder), Part_AttributeDTO.DecoderIDArray));
            if (Part_AttributeDTO.AttributeIDArray != null && Part_AttributeDTO.AttributeIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Part_AttributeXPO.Attribute), Part_AttributeDTO.AttributeIDArray));
            if (Part_AttributeDTO.ValueIDArray != null && Part_AttributeDTO.ValueIDArray.Count() > 0)
                _groupOperator.Operands.Add(new InOperator(nameof(Part_AttributeXPO.Value), Part_AttributeDTO.ValueIDArray));
            if (Part_AttributeDTO.AddedByID != null && Part_AttributeDTO.AddedByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.AddedBy), Part_AttributeDTO.AddedByID));
            if (Part_AttributeDTO.LastUpdateByID != null && Part_AttributeDTO.LastUpdateByID > 0)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.LastUpdateBy), Part_AttributeDTO.LastUpdateByID));
            if (Part_AttributeDTO.IsActive != null)
                _groupOperator.Operands.Add(new BinaryOperator(nameof(Part_AttributeXPO.IsActive), Part_AttributeDTO.IsActive));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
