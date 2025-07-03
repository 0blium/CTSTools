using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using CTSTools.DAL.Features.Maintenance.Ticket;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.Tickets.Priority;

public class PriorityMap
{
    public static PriorityDTO XPOToDTO(PriorityXPO PriorityXPO)
    {
        var _priorityDTO = new PriorityDTO();
        try
        {
            _priorityDTO.ID = PriorityXPO.Oid;
            _priorityDTO.Name = PriorityXPO.Name;
            _priorityDTO.Description = PriorityXPO.Description;
            _priorityDTO.SupportGroupDTO.ID = (PriorityXPO.SupportGroup != null) ? PriorityXPO.SupportGroup.Oid : 0;
            _priorityDTO.SupportGroupDTO.Name = (PriorityXPO.SupportGroup != null) ? PriorityXPO.SupportGroup.Name : "Unnassigned";
            _priorityDTO.PriorityWithSupportGroup = (PriorityXPO.SupportGroup != null) ? $"{PriorityXPO.Name} - {PriorityXPO.SupportGroup.Name}" : "Unnassigned";
            _priorityDTO.AddedDate = (PriorityXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? PriorityXPO.AddedDate : (DateTime?)null;
            _priorityDTO.AddedByID = (PriorityXPO.AddedBy != null) ? PriorityXPO.AddedBy.Oid : 0;
            _priorityDTO.AddedByName = (PriorityXPO.AddedBy != null) ? PriorityXPO.AddedBy.Name : "Unnassigned";
            _priorityDTO.LastUpdate = (PriorityXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? PriorityXPO.LastUpdate : (DateTime?)null;
            _priorityDTO.LastUpdateByID = (PriorityXPO.LastUpdateBy != null) ? PriorityXPO.LastUpdateBy.Oid : 0;
            _priorityDTO.LastUpdateByName = (PriorityXPO.LastUpdateBy != null) ? PriorityXPO.LastUpdateBy.Name : "Unnassigned";
            _priorityDTO.IsActive = PriorityXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _priorityDTO;
    }

    public static PriorityXPO DTOtoXPO(PriorityDTO PriorityDTO, UnitOfWork UnitOfWork)
    {
        PriorityXPO _priorityXPO;
        try
        {
            _priorityXPO = PriorityDTO.ID == null || PriorityDTO.ID == 0 ? new PriorityXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<PriorityXPO>(PriorityDTO.ID);
            _priorityXPO.Name = _priorityXPO.Name == PriorityDTO.Name ? _priorityXPO.Name : PriorityDTO.Name;
            _priorityXPO.Description = _priorityXPO.Description == PriorityDTO.Description ? _priorityXPO.Description : PriorityDTO.Description;
            _priorityXPO.SupportGroup = (_priorityXPO.SupportGroup != null && _priorityXPO.SupportGroup.Oid == PriorityDTO.SupportGroupDTO.ID) ? _priorityXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(PriorityDTO.SupportGroupDTO.ID);
            _priorityXPO.AddedDate = _priorityXPO.AddedDate ?? PriorityDTO.AddedDate;
            _priorityXPO.AddedBy = _priorityXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(PriorityDTO.AddedByID);
            _priorityXPO.LastUpdate = _priorityXPO.LastUpdate == PriorityDTO.LastUpdate ? _priorityXPO.LastUpdate : PriorityDTO.LastUpdate;
            _priorityXPO.LastUpdateBy = (_priorityXPO.LastUpdateBy?.Oid == PriorityDTO.LastUpdateByID) ? _priorityXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(PriorityDTO.LastUpdateByID);
            _priorityXPO.IsActive = (bool)(_priorityXPO.IsActive == PriorityDTO.IsActive ? (bool)_priorityXPO.IsActive : (bool)PriorityDTO.IsActive);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _priorityXPO;
    }
}
