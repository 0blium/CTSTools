using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.StatusType;

public class StatusTypeMap
{
    public static StatusTypeDTO XPOToDTO(StatusTypeXPO StatusTypeXPO)
    {
        var _statustypeDTO = new StatusTypeDTO();
        try
        {
            _statustypeDTO.ID = StatusTypeXPO.Oid;
            _statustypeDTO.Name = StatusTypeXPO.Name;
            _statustypeDTO.Description = StatusTypeXPO.Description;
            _statustypeDTO.AddedDate = (StatusTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? StatusTypeXPO.AddedDate : (DateTime?)null;
            _statustypeDTO.AddedByID = (StatusTypeXPO.AddedBy != null) ? StatusTypeXPO.AddedBy.Oid : 0;
            _statustypeDTO.AddedByName = (StatusTypeXPO.AddedBy != null) ? StatusTypeXPO.AddedBy.Name : "Unassigned";
            _statustypeDTO.LastUpdate = (StatusTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? StatusTypeXPO.LastUpdate : (DateTime?)null;
            _statustypeDTO.LastUpdateByID = (StatusTypeXPO.LastUpdateBy != null) ? StatusTypeXPO.LastUpdateBy.Oid : 0;
            _statustypeDTO.LastUpdateByName = (StatusTypeXPO.LastUpdateBy != null) ? StatusTypeXPO.LastUpdateBy.Name : "Unassigned";
            _statustypeDTO.IsActive = StatusTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _statustypeDTO;
    }

    public static StatusTypeXPO DTOtoXPO(StatusTypeDTO StatusTypeDTO, UnitOfWork UnitOfWork)
    {
        StatusTypeXPO _statustypeXPO;
        try
        {
            _statustypeXPO = StatusTypeDTO.ID == null || StatusTypeDTO.ID == 0 ? new StatusTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<StatusTypeXPO>(StatusTypeDTO.ID);
            _statustypeXPO.Name = _statustypeXPO.Name == StatusTypeDTO.Name ? _statustypeXPO.Name : StatusTypeDTO.Name;
            _statustypeXPO.Description = _statustypeXPO.Description == StatusTypeDTO.Description ? _statustypeXPO.Description : StatusTypeDTO.Description;
            _statustypeXPO.AddedDate = _statustypeXPO.AddedDate != null ? _statustypeXPO.AddedDate : StatusTypeDTO.AddedDate;
            _statustypeXPO.AddedBy = _statustypeXPO.AddedBy != null ? _statustypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(StatusTypeDTO.AddedByID);
            _statustypeXPO.LastUpdate = _statustypeXPO.LastUpdate == StatusTypeDTO.LastUpdate ? _statustypeXPO.LastUpdate : StatusTypeDTO.LastUpdate;
            _statustypeXPO.LastUpdateBy = _statustypeXPO.LastUpdateBy != null && _statustypeXPO.LastUpdateBy.Oid == StatusTypeDTO.LastUpdateByID ? _statustypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(StatusTypeDTO.LastUpdateByID);
            _statustypeXPO.IsActive = _statustypeXPO.IsActive == StatusTypeDTO.IsActive ? (bool)_statustypeXPO.IsActive : (bool)StatusTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _statustypeXPO;
    }

}

