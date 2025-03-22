using CTSTools.DAL.Features.Ticket.Station;
using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.StationManagement.StationType;

public class StationType_DXFilter
{
    public static GroupOperator GetStationType_DXFilter(StationTypeDTO StationTypeDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (StationTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationTypeXPO.Oid), StationTypeDTO.ID));
            }
            if (StationTypeDTO.StationTypeIDArray != null && StationTypeDTO.StationTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StationTypeXPO.Oid), StationTypeDTO.StationTypeIDArray));
            }
            if (StationTypeDTO.AddedByID != null && StationTypeDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationTypeXPO.AddedBy), StationTypeDTO.AddedByID));
            }
            if (StationTypeDTO.LastUpdateByID != null && StationTypeDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationTypeXPO.LastUpdateBy), StationTypeDTO.LastUpdateByID));
            }
            if (StationTypeDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationTypeXPO.IsActive), StationTypeDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
