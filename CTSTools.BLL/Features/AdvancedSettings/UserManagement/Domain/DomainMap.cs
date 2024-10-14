using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.Domain;

public class DomainMap
{
    public static DomainDTO XPOToDTO(DomainXPO DomainXPO)
    {
        var _domainDTO = new DomainDTO();
        try
        {
            _domainDTO.ID = DomainXPO.Oid;
            _domainDTO.IP = DomainXPO.IP;
            _domainDTO.Description = DomainXPO.Description;
            _domainDTO.FacilityName = (DomainXPO.Facility != null) ? DomainXPO.Facility.Name : "Unassigned";
            _domainDTO.FacilityID = (DomainXPO.Facility != null) ? DomainXPO.Facility.Oid : 0;
            _domainDTO.AddedDate = (DomainXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DomainXPO.AddedDate : (DateTime?)null;
            _domainDTO.AddedByID = (DomainXPO.AddedBy != null) ? DomainXPO.AddedBy.Oid : 0;               
            _domainDTO.AddedByName = (DomainXPO.AddedBy != null) ? DomainXPO.AddedBy.Name : "Unassigned";
            _domainDTO.LastUpdate = (DomainXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DomainXPO.LastUpdate : (DateTime?)null;
            _domainDTO.LastUpdateByID = (DomainXPO.LastUpdateBy != null) ? DomainXPO.LastUpdateBy.Oid : 0;
            _domainDTO.LastUpdateByName = (DomainXPO.LastUpdateBy != null) ? DomainXPO.LastUpdateBy.Name : "Unassigned";
            _domainDTO.IsActive = DomainXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _domainDTO;
    }

    public static DomainXPO DTOtoXPO(DomainDTO DomainDTO, UnitOfWork UnitOfWork)
    {
        DomainXPO _domainXPO;
        try
        {
            _domainXPO = DomainDTO.ID == null || DomainDTO.ID == 0 ? new DomainXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DomainXPO>(DomainDTO.ID);
            _domainXPO.IP = _domainXPO.IP == DomainDTO.IP ? _domainXPO.IP : DomainDTO.IP;
            _domainXPO.Description = _domainXPO.Description == DomainDTO.Description ? _domainXPO.Description : DomainDTO.Description;
            _domainXPO.AddedDate = (_domainXPO.AddedDate != null) ? _domainXPO.AddedDate : DomainDTO.AddedDate;
            _domainXPO.AddedBy = _domainXPO.AddedBy != null ? _domainXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DomainDTO.AddedByID);
            _domainXPO.Facility = _domainXPO.Facility != null ? _domainXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(DomainDTO.FacilityID);
            _domainXPO.LastUpdate = _domainXPO.LastUpdate == DomainDTO.LastUpdate ? _domainXPO.LastUpdate : DomainDTO.LastUpdate;
            _domainXPO.LastUpdateBy = _domainXPO.LastUpdateBy != null && _domainXPO.LastUpdateBy.Oid == DomainDTO.LastUpdateByID ? _domainXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DomainDTO.LastUpdateByID);
            _domainXPO.IsActive = _domainXPO.IsActive == DomainDTO.IsActive ? (bool)_domainXPO.IsActive : (bool)DomainDTO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _domainXPO;
    }

}

