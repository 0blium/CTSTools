using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Station;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;

public class SupportGroupMap
{
    public static SupportGroupDTO XPOToDTO(SupportGroupXPO SupportGroupXPO)
    {
        var _supportgroupDTO = new SupportGroupDTO();
        try
        {
            _supportgroupDTO.ID = SupportGroupXPO.Oid;
            _supportgroupDTO.EnglishName = SupportGroupXPO.EnglishName;
            _supportgroupDTO.Names = $"{SupportGroupXPO.EnglishName}";
            _supportgroupDTO.Description = SupportGroupXPO.Description;
            _supportgroupDTO.AddedDate = (SupportGroupXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SupportGroupXPO.AddedDate : (DateTime?)null;
            _supportgroupDTO.FacilityDTO.ID = (SupportGroupXPO.Facility != null) ? SupportGroupXPO.Facility.Oid : 0;
            _supportgroupDTO.FacilityDTO.Name = (SupportGroupXPO.Facility != null) ? SupportGroupXPO.Facility.Name : "Unnassigned";
            _supportgroupDTO.AddedByID = (SupportGroupXPO.AddedBy != null) ? SupportGroupXPO.AddedBy.Oid : 0;
            _supportgroupDTO.AddedByName = (SupportGroupXPO.AddedBy != null) ? SupportGroupXPO.AddedBy.Name : "Unnassigned";
            _supportgroupDTO.LastUpdate = (SupportGroupXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SupportGroupXPO.LastUpdate : (DateTime?)null;
            _supportgroupDTO.LastUpdateByID = (SupportGroupXPO.LastUpdateBy != null) ? SupportGroupXPO.LastUpdateBy.Oid : 0;
            _supportgroupDTO.LastUpdateByName = (SupportGroupXPO.LastUpdateBy != null) ? SupportGroupXPO.LastUpdateBy.Name : "Unnassigned";
            _supportgroupDTO.IsActive = SupportGroupXPO.IsActive;
            _supportgroupDTO.StationDTO.ID = (SupportGroupXPO.Station != null) ? SupportGroupXPO.Station.Oid : 0;
            _supportgroupDTO.StationDTO.Name = (SupportGroupXPO.Station != null) ? SupportGroupXPO.Station.Name : "Unnassigned";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supportgroupDTO;
    }

    public static SupportGroupXPO DTOtoXPO(SupportGroupDTO SupportGroupDTO, UnitOfWork UnitOfWork)
    {
        SupportGroupXPO _supportgroupXPO;
        try
        {
            _supportgroupXPO = SupportGroupDTO.ID == null || SupportGroupDTO.ID == 0 ? new SupportGroupXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SupportGroupXPO>(SupportGroupDTO.ID);
            _supportgroupXPO.EnglishName = _supportgroupXPO.EnglishName == SupportGroupDTO.EnglishName ? _supportgroupXPO.EnglishName : SupportGroupDTO.EnglishName;
            _supportgroupXPO.Description = _supportgroupXPO.Description == SupportGroupDTO.Description ? _supportgroupXPO.Description : SupportGroupDTO.Description;
            _supportgroupXPO.AddedDate = _supportgroupXPO.AddedDate != null ? _supportgroupXPO.AddedDate : SupportGroupDTO.AddedDate;
            _supportgroupXPO.Facility = (_supportgroupXPO.Facility != null && _supportgroupXPO.Facility.Oid == SupportGroupDTO.FacilityDTO.ID) ? _supportgroupXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(SupportGroupDTO.FacilityDTO.ID);
            _supportgroupXPO.AddedBy = (_supportgroupXPO.AddedBy != null) ? _supportgroupXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(SupportGroupDTO.AddedByID);
            _supportgroupXPO.LastUpdate = _supportgroupXPO.LastUpdate == SupportGroupDTO.LastUpdate ? _supportgroupXPO.LastUpdate : SupportGroupDTO.LastUpdate;
            _supportgroupXPO.LastUpdateBy = (_supportgroupXPO.LastUpdateBy != null && _supportgroupXPO.LastUpdateBy.Oid == SupportGroupDTO.LastUpdateByID) ? _supportgroupXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SupportGroupDTO.LastUpdateByID);
            _supportgroupXPO.IsActive = (bool)(_supportgroupXPO.IsActive == SupportGroupDTO.IsActive ? _supportgroupXPO.IsActive : SupportGroupDTO.IsActive);
            _supportgroupXPO.Station = (_supportgroupXPO.Station != null && _supportgroupXPO.Station.Oid == SupportGroupDTO.StationDTO.ID) ? _supportgroupXPO.Station : UnitOfWork.GetObjectByKey<StationXPO>(SupportGroupDTO.StationDTO.ID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supportgroupXPO;
    }
}
