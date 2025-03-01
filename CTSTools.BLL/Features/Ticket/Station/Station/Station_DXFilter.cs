using CTSTools.DAL.Features.Ticket.Station;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Station.Station;

public class Station_DXFilter
{
    public static GroupOperator GetStation_DXFilter(StationDTO StationDTO)
    {
        var _groupOperator = new GroupOperator();
        try
        {
            if (StationDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.Oid), StationDTO.ID));
            }
            if (StationDTO.StationIDArray != null && StationDTO.StationIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StationXPO.Oid), StationDTO.StationIDArray));
            }
            if (!string.IsNullOrEmpty(StationDTO.Serial))
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.Serial), StationDTO.Serial));
            }
            if (StationDTO.FacilityDTO.ID != null || StationDTO.FacilityDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.Facility), StationDTO.FacilityDTO.ID));
            }
            if (StationDTO.FacilityIDArray != null && StationDTO.FacilityIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StationXPO.Facility), StationDTO.FacilityIDArray));
            }
            if (StationDTO.DepartmentDTO.ID != null || StationDTO.DepartmentDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.Department), StationDTO.DepartmentDTO.ID));
            }
            if (StationDTO.DepartmentIDArray != null && StationDTO.DepartmentIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StationXPO.Department), StationDTO.DepartmentIDArray));
            }
            if (StationDTO.StationTypeDTO.ID != null || StationDTO.StationTypeDTO.ID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.StationType), StationDTO.StationTypeDTO.ID));
            }
            if (StationDTO.StationTypeIDArray != null && StationDTO.StationTypeIDArray.Count() > 0)
            {
                _groupOperator.Operands.Add(new InOperator(nameof(StationXPO.StationType), StationDTO.StationTypeIDArray));
            }
            if (StationDTO.AddedByID != null && StationDTO.AddedByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.AddedBy), StationDTO.AddedByID));
            }
            if (StationDTO.LastUpdateByID != null && StationDTO.LastUpdateByID > 0)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.LastUpdateBy), StationDTO.LastUpdateByID));
            }
            if (StationDTO.IsActive != null)
            {
                _groupOperator.Operands.Add(new BinaryOperator(nameof(StationXPO.IsActive), StationDTO.IsActive));
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _groupOperator;
    }
}
