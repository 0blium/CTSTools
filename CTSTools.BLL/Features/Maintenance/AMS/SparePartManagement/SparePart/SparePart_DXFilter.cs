using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart;

public class SparePart_DXFilter
{
    public static GroupOperator GetSparePart_DXFilter(SparePartDTO SparePartDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (SparePartDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartXPO.Oid), SparePartDTO.ID));
            }
            if (SparePartDTO.SparePartIDArray != null && SparePartDTO.SparePartIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(SparePartXPO.Oid), SparePartDTO.SparePartIDArray));
            }
            if (!string.IsNullOrEmpty(SparePartDTO.Name))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartXPO.Name), SparePartDTO.Name));
            }
            if (SparePartDTO.ManufactureID != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartXPO.ManufactureID), SparePartDTO.ManufactureID));
            }
            if (SparePartDTO.AddedByID != null && SparePartDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartXPO.AddedBy), SparePartDTO.AddedByName));
            }
            if (SparePartDTO.LastUpdateByID != null && SparePartDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartXPO.LastUpdateBy), SparePartDTO.LastUpdateByName));
            }
            if (SparePartDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(SparePartXPO.IsActive), SparePartDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
