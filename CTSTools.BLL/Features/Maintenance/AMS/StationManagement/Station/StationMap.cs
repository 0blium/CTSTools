using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Station;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.StationManagement.Station;

public class StationMap
{
        public static StationDTO XPOToDTO(StationXPO StationXPO)
    {
        var _stationDTO = new StationDTO();
        try
        {
            _stationDTO.ID = StationXPO.Oid;
            _stationDTO.Name = StationXPO.Name;
            _stationDTO.Description = StationXPO.Description;
            _stationDTO.Serial = StationXPO.Serial;
            _stationDTO.NameWithSerial = $"{StationXPO.Serial} - {StationXPO.Name}";
            _stationDTO.FacilityID = (StationXPO.Facility != null) ? StationXPO.Facility.Oid : 0;
            _stationDTO.FacilityName = (StationXPO.Facility != null) ? StationXPO.Facility.Name : "Unnassigned";
            _stationDTO.DepartmentID = (StationXPO.Department != null) ? StationXPO.Department.Oid : 0;
            _stationDTO.DepartmentName = (StationXPO.Department != null) ? StationXPO.Department.Name : "Unnassigned";
            _stationDTO.StationTypeID = (StationXPO.StationType != null) ? StationXPO.StationType.Oid : 0;
            _stationDTO.StationTypeName = (StationXPO.StationType != null) ? StationXPO.StationType.Name : "Unnassigned";
            _stationDTO.AddedDate = (StationXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? StationXPO.AddedDate : (DateTime?)null;
            _stationDTO.AddedByID = (StationXPO.AddedBy != null) ? StationXPO.AddedBy.Oid : 0;
            _stationDTO.AddedByName = (StationXPO.AddedBy != null) ? StationXPO.AddedBy.Name : "Unnassigned";
            _stationDTO.LastUpdate = (StationXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? StationXPO.LastUpdate : (DateTime?)null;
            _stationDTO.LastUpdateByID = (StationXPO.LastUpdateBy != null) ? StationXPO.LastUpdateBy.Oid : 0;
            _stationDTO.LastUpdateByName = (StationXPO.LastUpdateBy != null) ? StationXPO.LastUpdateBy.Name : "Unnassigned";
            _stationDTO.IsActive = StationXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _stationDTO;
    }

    public static StationXPO DTOtoXPO(StationDTO StationDTO, UnitOfWork UnitOfWork)
    {
        StationXPO _stationXPO;
        try
        {
            _stationXPO = StationDTO.ID == null || StationDTO.ID == 0 ? new StationXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<StationXPO>(StationDTO.ID);
            _stationXPO.Name = _stationXPO.Name == StationDTO.Name ? _stationXPO.Name : StationDTO.Name;
            _stationXPO.Description = _stationXPO.Description == StationDTO.Description ? _stationXPO.Description : StationDTO.Description;
            _stationXPO.Serial = _stationXPO.Serial == StationDTO.Serial ? _stationXPO.Serial : StationDTO.Serial;
            _stationXPO.Facility = (_stationXPO.Facility != null && _stationXPO.Facility.Oid == StationDTO.FacilityID) ? _stationXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(StationDTO.FacilityID);
            _stationXPO.Department = (_stationXPO.Department != null && _stationXPO.Department.Oid == StationDTO.DepartmentID) ? _stationXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(StationDTO.DepartmentID);
            _stationXPO.StationType = (_stationXPO.StationType != null && _stationXPO.StationType.Oid == StationDTO.StationTypeID) ? _stationXPO.StationType : UnitOfWork.GetObjectByKey<StationTypeXPO>(StationDTO.StationTypeID);
            _stationXPO.AddedDate = _stationXPO.AddedDate != null ? _stationXPO.AddedDate : StationDTO.AddedDate;
            _stationXPO.AddedBy = (_stationXPO.AddedBy != null) ? _stationXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(StationDTO.AddedByID);
            _stationXPO.LastUpdate = _stationXPO.LastUpdate == StationDTO.LastUpdate ? _stationXPO.LastUpdate : StationDTO.LastUpdate;
            _stationXPO.LastUpdateBy = (_stationXPO.LastUpdateBy != null && _stationXPO.LastUpdateBy.Oid == StationDTO.LastUpdateByID) ? _stationXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(StationDTO.LastUpdateByID);
            _stationXPO.IsActive = _stationXPO.IsActive == StationDTO.IsActive ? (bool)_stationXPO.IsActive : (bool)StationDTO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _stationXPO;
    }
}
