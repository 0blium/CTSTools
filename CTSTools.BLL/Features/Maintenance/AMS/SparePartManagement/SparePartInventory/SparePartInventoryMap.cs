using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.SparePart;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;

public class SparePartInventoryMap
{
    public static SparePartInventoryDTO XPOToDTO(SparePartInventoryXPO SparePartInventoryXPO)
    {
        var _sparepartinventoryDTO = new SparePartInventoryDTO();
        try
        {
            _sparepartinventoryDTO.ID = SparePartInventoryXPO.Oid;
            _sparepartinventoryDTO.MaxQty = SparePartInventoryXPO.MaxQty;
            _sparepartinventoryDTO.MinQty = SparePartInventoryXPO.MinQty;
            _sparepartinventoryDTO.SparePartDTO.ID = (SparePartInventoryXPO.SparePart != null) ? SparePartInventoryXPO.SparePart.Oid : 0;
            _sparepartinventoryDTO.SparePartDTO.Name = (SparePartInventoryXPO.SparePart != null) ? SparePartInventoryXPO.SparePart.Name : "Unnassigned";
            _sparepartinventoryDTO.SupportGroupDTO.ID = (SparePartInventoryXPO.SupportGroup != null) ? SparePartInventoryXPO.SupportGroup.Oid : 0;
            _sparepartinventoryDTO.SupportGroupDTO.EnglishName = (SparePartInventoryXPO.SupportGroup != null) ? SparePartInventoryXPO.SupportGroup.EnglishName : "Unnassigned";
            _sparepartinventoryDTO.AvailableQty = SparePartInventoryXPO.AvailableQty;
            _sparepartinventoryDTO.AddedDate = (SparePartInventoryXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SparePartInventoryXPO.AddedDate : (DateTime?)null;
            _sparepartinventoryDTO.AddedByID = (SparePartInventoryXPO.AddedBy != null) ? SparePartInventoryXPO.AddedBy.Oid : 0;
            _sparepartinventoryDTO.AddedByName = (SparePartInventoryXPO.AddedBy != null) ? SparePartInventoryXPO.AddedBy.Name : "Unnassigned";
            _sparepartinventoryDTO.LastUpdate = (SparePartInventoryXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SparePartInventoryXPO.LastUpdate : (DateTime?)null;
            _sparepartinventoryDTO.LastUpdateByID = (SparePartInventoryXPO.LastUpdateBy != null) ? SparePartInventoryXPO.LastUpdateBy.Oid : 0;
            _sparepartinventoryDTO.LastUpdateByName = (SparePartInventoryXPO.LastUpdateBy != null) ? SparePartInventoryXPO.LastUpdateBy.Name : "Unnassigned";
            _sparepartinventoryDTO.IsActive = SparePartInventoryXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepartinventoryDTO;
    }

    public static SparePartInventoryXPO DTOtoXPO(SparePartInventoryDTO SparePartInventoryDTO, UnitOfWork UnitOfWork)
    {
        SparePartInventoryXPO _sparepartinventoryXPO;
        try
        {
            _sparepartinventoryXPO = SparePartInventoryDTO.ID == null || SparePartInventoryDTO.ID == 0 ? new SparePartInventoryXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SparePartInventoryXPO>(SparePartInventoryDTO.ID);
            _sparepartinventoryXPO.MaxQty = _sparepartinventoryXPO.MaxQty == SparePartInventoryDTO.MaxQty ? _sparepartinventoryXPO.MaxQty : SparePartInventoryDTO.MaxQty;
            _sparepartinventoryXPO.MinQty = _sparepartinventoryXPO.MinQty == SparePartInventoryDTO.MinQty ? _sparepartinventoryXPO.MinQty : SparePartInventoryDTO.MinQty;
            _sparepartinventoryXPO.SparePart = (_sparepartinventoryXPO.SparePart?.Oid == SparePartInventoryDTO.SparePartDTO.ID) ? _sparepartinventoryXPO.SparePart : UnitOfWork.GetObjectByKey<SparePartXPO>(SparePartInventoryDTO.SparePartDTO.ID);
            _sparepartinventoryXPO.SupportGroup = (_sparepartinventoryXPO.SupportGroup?.Oid == SparePartInventoryDTO.SupportGroupDTO.ID) ? _sparepartinventoryXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(SparePartInventoryDTO.SupportGroupDTO.ID);
            _sparepartinventoryXPO.AvailableQty = _sparepartinventoryXPO.AvailableQty == SparePartInventoryDTO.AvailableQty ? _sparepartinventoryXPO.AvailableQty : SparePartInventoryDTO.AvailableQty;
            _sparepartinventoryXPO.AddedDate = _sparepartinventoryXPO.AddedDate ?? SparePartInventoryDTO.AddedDate;
            _sparepartinventoryXPO.AddedBy = _sparepartinventoryXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(SparePartInventoryDTO.AddedByID);
            _sparepartinventoryXPO.LastUpdate = _sparepartinventoryXPO.LastUpdate == SparePartInventoryDTO.LastUpdate ? _sparepartinventoryXPO.LastUpdate : SparePartInventoryDTO.LastUpdate;
            _sparepartinventoryXPO.LastUpdateBy = (_sparepartinventoryXPO.LastUpdateBy?.Oid == SparePartInventoryDTO.LastUpdateByID) ? _sparepartinventoryXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SparePartInventoryDTO.LastUpdateByID);
            _sparepartinventoryXPO.IsActive = (bool)(_sparepartinventoryXPO.IsActive == SparePartInventoryDTO.IsActive ? (bool)_sparepartinventoryXPO.IsActive : (bool)SparePartInventoryDTO.IsActive);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepartinventoryXPO;
    }
}
