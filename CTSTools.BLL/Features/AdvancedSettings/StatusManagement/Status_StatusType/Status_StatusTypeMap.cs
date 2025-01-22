
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;

public class Status_StatusTypeMap
{
    public static Status_StatusTypeDTO XPOToDTO(Status_StatusTypeXPO Status_StatusTypeXPO)
    {
        var _status_statustypeDTO = new Status_StatusTypeDTO();
        try
        {
            _status_statustypeDTO.ID = Status_StatusTypeXPO.Oid;
            _status_statustypeDTO.StatusName = (Status_StatusTypeXPO.Status != null) ? Status_StatusTypeXPO.Status.Name : "Unnassigned";
            _status_statustypeDTO.StatusID = (Status_StatusTypeXPO.Status != null) ? Status_StatusTypeXPO.Status.Oid : 0;
            _status_statustypeDTO.StatusTypeName = (Status_StatusTypeXPO.StatusType != null) ? Status_StatusTypeXPO.StatusType.Name : "Unnassigned";
            _status_statustypeDTO.StatusTypeID = (Status_StatusTypeXPO.StatusType != null) ? Status_StatusTypeXPO.StatusType.Oid : 0;
            _status_statustypeDTO.Description = Status_StatusTypeXPO.Description;
            _status_statustypeDTO.AddedDate = (Status_StatusTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Status_StatusTypeXPO.AddedDate : (DateTime?)null;
            _status_statustypeDTO.AddedByID = (Status_StatusTypeXPO.AddedBy != null) ? Status_StatusTypeXPO.AddedBy.Oid : 0;
            _status_statustypeDTO.AddedByName = (Status_StatusTypeXPO.AddedBy != null) ? Status_StatusTypeXPO.AddedBy.Name : "Unassigned";
            _status_statustypeDTO.LastUpdate = (Status_StatusTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Status_StatusTypeXPO.LastUpdate : (DateTime?)null;
            _status_statustypeDTO.LastUpdateByID = (Status_StatusTypeXPO.LastUpdateBy != null) ? Status_StatusTypeXPO.LastUpdateBy.Oid : 0;
            _status_statustypeDTO.LastUpdateByName = (Status_StatusTypeXPO.LastUpdateBy != null) ? Status_StatusTypeXPO.LastUpdateBy.Name : "Unassigned";
            _status_statustypeDTO.IsActive = Status_StatusTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _status_statustypeDTO;
    }

    public static Status_StatusTypeXPO DTOtoXPO(Status_StatusTypeDTO Status_StatusTypeDTO, UnitOfWork UnitOfWork)
    {
        Status_StatusTypeXPO _status_statustypeXPO;
        try
        {
            _status_statustypeXPO = Status_StatusTypeDTO.ID == null || Status_StatusTypeDTO.ID == 0 ? new Status_StatusTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Status_StatusTypeXPO>(Status_StatusTypeDTO.ID);
            _status_statustypeXPO.Status =(_status_statustypeXPO.Status != null && _status_statustypeXPO.Status.Oid == Status_StatusTypeDTO.StatusID) ? _status_statustypeXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(Status_StatusTypeDTO.StatusID);
            _status_statustypeXPO.StatusType = (_status_statustypeXPO.StatusType != null && _status_statustypeXPO.StatusType.Oid == Status_StatusTypeDTO.StatusTypeID) ? _status_statustypeXPO.StatusType : UnitOfWork.GetObjectByKey<StatusTypeXPO>(Status_StatusTypeDTO.StatusTypeID);
            _status_statustypeXPO.Description = _status_statustypeXPO.Description == Status_StatusTypeDTO.Description ? _status_statustypeXPO.Description : Status_StatusTypeDTO.Description;
            _status_statustypeXPO.AddedDate = _status_statustypeXPO.AddedDate != null ? _status_statustypeXPO.AddedDate : Status_StatusTypeDTO.AddedDate;
            _status_statustypeXPO.AddedBy = _status_statustypeXPO.AddedBy != null  ? _status_statustypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Status_StatusTypeDTO.AddedByID);
            _status_statustypeXPO.LastUpdate = _status_statustypeXPO.LastUpdate == Status_StatusTypeDTO.LastUpdate ? _status_statustypeXPO.LastUpdate : Status_StatusTypeDTO.LastUpdate;
            _status_statustypeXPO.LastUpdateBy = _status_statustypeXPO.LastUpdateBy != null && _status_statustypeXPO.LastUpdateBy.Oid == Status_StatusTypeDTO.LastUpdateByID ? _status_statustypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Status_StatusTypeDTO.LastUpdateByID);
            _status_statustypeXPO.IsActive = _status_statustypeXPO.IsActive == Status_StatusTypeDTO.IsActive ? (bool)_status_statustypeXPO.IsActive : (bool)Status_StatusTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _status_statustypeXPO;
    }
}
