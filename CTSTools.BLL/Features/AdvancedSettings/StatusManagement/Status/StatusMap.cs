using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;

public class StatusMap
{
    public static StatusDTO XPOToDTO(StatusXPO StatusXPO)
    {
        var _statusDTO = new StatusDTO();
        try
        {
            _statusDTO.ID = StatusXPO.Oid;
            _statusDTO.Name = StatusXPO.Name;
            _statusDTO.Description = StatusXPO.Description;
            _statusDTO.AddedDate = (StatusXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? StatusXPO.AddedDate : (DateTime?)null;
            _statusDTO.AddedByID = (StatusXPO.AddedBy != null) ? StatusXPO.AddedBy.Oid : 0;
            _statusDTO.AddedByName = (StatusXPO.AddedBy != null) ? StatusXPO.AddedBy.Name : "Unassigned";
            _statusDTO.LastUpdate = (StatusXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? StatusXPO.LastUpdate : (DateTime?)null;
            _statusDTO.LastUpdateByID = (StatusXPO.LastUpdateBy != null) ? StatusXPO.LastUpdateBy.Oid : 0;
            _statusDTO.LastUpdateByName = (StatusXPO.LastUpdateBy != null) ? StatusXPO.LastUpdateBy.Name : "Unassigned";
            _statusDTO.IsActive = StatusXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _statusDTO;
    }

    public static StatusXPO DTOtoXPO(StatusDTO StatusDTO, UnitOfWork UnitOfWork)
    {
        StatusXPO _statusXPO;
        try
        {
            _statusXPO = StatusDTO.ID == null || StatusDTO.ID == 0 ? new StatusXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<StatusXPO>(StatusDTO.ID);
            _statusXPO.Name = _statusXPO.Name == StatusDTO.Name ? _statusXPO.Name : StatusDTO.Name;
            _statusXPO.Description = _statusXPO.Description == StatusDTO.Description ? _statusXPO.Description : StatusDTO.Description;
            _statusXPO.AddedDate = (_statusXPO.AddedDate != null) ? _statusXPO.AddedDate : StatusDTO.AddedDate;
            _statusXPO.AddedBy = _statusXPO.AddedBy != null ? _statusXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(StatusDTO.AddedByID);
            _statusXPO.LastUpdate = _statusXPO.LastUpdate == StatusDTO.LastUpdate ? _statusXPO.LastUpdate : StatusDTO.LastUpdate;
            _statusXPO.LastUpdateBy = _statusXPO.LastUpdateBy != null &&  _statusXPO.LastUpdateBy.Oid == StatusDTO.LastUpdateByID ? _statusXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(StatusDTO.LastUpdateByID);
            _statusXPO.IsActive = _statusXPO.IsActive == StatusDTO.IsActive ? (bool)_statusXPO.IsActive : (bool)StatusDTO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _statusXPO;
    }

}

