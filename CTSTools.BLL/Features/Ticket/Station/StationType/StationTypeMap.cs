using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Station;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Station.StationType;

public class StationTypeMap
{
    public static StationTypeDTO XPOToDTO(StationTypeXPO StationTypeXPO)
    {
        var _stationtypeDTO = new StationTypeDTO();
        try
        {
            _stationtypeDTO.ID = StationTypeXPO.Oid;
            _stationtypeDTO.Name = StationTypeXPO.Name;
            _stationtypeDTO.Description = StationTypeXPO.Description;
            _stationtypeDTO.AddedDate = (StationTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? StationTypeXPO.AddedDate : (DateTime?)null;
            _stationtypeDTO.AddedByID = (StationTypeXPO.AddedBy != null) ? StationTypeXPO.AddedBy.Oid : 0;
            _stationtypeDTO.AddedByName = (StationTypeXPO.AddedBy != null) ? StationTypeXPO.AddedBy.Name : "Unnassigned";
            _stationtypeDTO.LastUpdate = (StationTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? StationTypeXPO.LastUpdate : (DateTime?)null;
            _stationtypeDTO.LastUpdateByID = (StationTypeXPO.LastUpdateBy != null) ? StationTypeXPO.LastUpdateBy.Oid : 0;
            _stationtypeDTO.LastUpdateByName = (StationTypeXPO.LastUpdateBy != null) ? StationTypeXPO.LastUpdateBy.Name : "Unnassigned";
            _stationtypeDTO.IsActive = StationTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _stationtypeDTO;
    }

    public static StationTypeXPO DTOtoXPO(StationTypeDTO StationTypeDTO, UnitOfWork UnitOfWork)
    {
        StationTypeXPO _stationtypeXPO;
        try
        {
            _stationtypeXPO = StationTypeDTO.ID == null || StationTypeDTO.ID == 0 ? new StationTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<StationTypeXPO>(StationTypeDTO.ID);
            _stationtypeXPO.Name = _stationtypeXPO.Name == StationTypeDTO.Name ? _stationtypeXPO.Name : StationTypeDTO.Name;
            _stationtypeXPO.Description = _stationtypeXPO.Description == StationTypeDTO.Description ? _stationtypeXPO.Description : StationTypeDTO.Description;
            _stationtypeXPO.AddedDate = _stationtypeXPO.AddedDate != null ? _stationtypeXPO.AddedDate : StationTypeDTO.AddedDate;
            _stationtypeXPO.AddedBy = (_stationtypeXPO.AddedBy != null) ? _stationtypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(StationTypeDTO.AddedByID);
            _stationtypeXPO.LastUpdate = _stationtypeXPO.LastUpdate == StationTypeDTO.LastUpdate ? _stationtypeXPO.LastUpdate : StationTypeDTO.LastUpdate;
            _stationtypeXPO.LastUpdateBy = (_stationtypeXPO.LastUpdateBy != null && _stationtypeXPO.LastUpdateBy.Oid == StationTypeDTO.LastUpdateByID) ? _stationtypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(StationTypeDTO.LastUpdateByID);
            _stationtypeXPO.IsActive = _stationtypeXPO.IsActive == StationTypeDTO.IsActive ? (bool)_stationtypeXPO.IsActive : (bool)StationTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _stationtypeXPO;
    }
}
