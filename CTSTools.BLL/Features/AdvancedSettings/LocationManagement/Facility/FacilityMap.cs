using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;

public class FacilityMap
{
    public static FacilityDTO XPOToDTO(FacilityXPO FacilityXPO)
    {
        var _facilityDTO = new FacilityDTO();
        try
        {
            _facilityDTO.ID = FacilityXPO.Oid;
            _facilityDTO.Name = FacilityXPO.Name;
            _facilityDTO.Description = FacilityXPO.Description;
            _facilityDTO.AddedDate = (FacilityXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? FacilityXPO.AddedDate : (DateTime?)null;
            _facilityDTO.AddedByID = (FacilityXPO.AddedBy != null) ? FacilityXPO.AddedBy.Oid : 0;
            _facilityDTO.AddedByName = (FacilityXPO.AddedBy != null) ? FacilityXPO.AddedBy.Name : "Unassigned";
            _facilityDTO.LastUpdate = (FacilityXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? FacilityXPO.LastUpdate : (DateTime?)null;
            _facilityDTO.LastUpdateByID = (FacilityXPO.LastUpdateBy != null) ? FacilityXPO.LastUpdateBy.Oid : 0;
            _facilityDTO.LastUpdateByName = (FacilityXPO.LastUpdateBy != null) ? FacilityXPO.LastUpdateBy.Name : "Unassigned";
            _facilityDTO.IsActive = FacilityXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _facilityDTO;
    }

    public static FacilityXPO DTOtoXPO(FacilityDTO FacilityDTO, UnitOfWork UnitOfWork)
    {
        FacilityXPO _facilityXPO;
        try
        {
            _facilityXPO = FacilityDTO.ID == null || FacilityDTO.ID == 0 ? new FacilityXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<FacilityXPO>(FacilityDTO.ID);
            _facilityXPO.Name = _facilityXPO.Name == FacilityDTO.Name ? _facilityXPO.Name : FacilityDTO.Name;
            _facilityXPO.Description = _facilityXPO.Description == FacilityDTO.Description ? _facilityXPO.Description : FacilityDTO.Description;
            _facilityXPO.AddedDate = _facilityXPO.AddedDate != null ? _facilityXPO.AddedDate : FacilityDTO.AddedDate;
            _facilityXPO.AddedBy = _facilityXPO.AddedBy != null ? _facilityXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(FacilityDTO.AddedByID);
            _facilityXPO.LastUpdate = _facilityXPO.LastUpdate == FacilityDTO.LastUpdate ? _facilityXPO.LastUpdate : FacilityDTO.LastUpdate;
            _facilityXPO.LastUpdateBy = _facilityXPO.LastUpdateBy != null && _facilityXPO.LastUpdateBy.Oid == FacilityDTO.LastUpdateByID ? _facilityXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(FacilityDTO.LastUpdateByID);
            _facilityXPO.IsActive = _facilityXPO.IsActive == FacilityDTO.IsActive ? (bool)_facilityXPO.IsActive : (bool)FacilityDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _facilityXPO;
    }

}
