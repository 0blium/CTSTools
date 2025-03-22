using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using CTSTools.DAL.Features.Ticket.SparePart;
using CTSTools.DAL.Features.Ticket.Ticket;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePartUsage;

public class SparePartUsageMap
{
    public static SparePartUsageDTO XPOToDTO(SparePartUsageXPO SparePartUsageXPO)
    {
        var _sparepartusageDTO = new SparePartUsageDTO();
        try
        {
            _sparepartusageDTO.ID = SparePartUsageXPO.Oid;
            _sparepartusageDTO.TicketDTO.ID = (SparePartUsageXPO.Ticket != null) ? SparePartUsageXPO.Ticket.Oid : 0;
            _sparepartusageDTO.TicketDTO.TicketNumber = (SparePartUsageXPO.Ticket != null) ? SparePartUsageXPO.Ticket.TicketNumber : 0;
            _sparepartusageDTO.SparePartInventoryDTO.ID = (SparePartUsageXPO.SparePartInventory != null) ? SparePartUsageXPO.SparePartInventory.Oid : 0;
            _sparepartusageDTO.Item_LineDTO.ID = (SparePartUsageXPO.Item_Line != null) ? SparePartUsageXPO.Item_Line.Oid : 0;
            _sparepartusageDTO.Quantity = SparePartUsageXPO.Quantity;
            _sparepartusageDTO.SparePart_LotDTO.ID = (SparePartUsageXPO.SparePart_Lot != null) ? SparePartUsageXPO.SparePart_Lot.Oid : 0;
            _sparepartusageDTO.AddedDate = (SparePartUsageXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SparePartUsageXPO.AddedDate : (DateTime?)null;
            _sparepartusageDTO.AddedByID = (SparePartUsageXPO.AddedBy != null) ? SparePartUsageXPO.AddedBy.Oid : 0;
            _sparepartusageDTO.AddedByName = (SparePartUsageXPO.AddedBy != null) ? SparePartUsageXPO.AddedBy.Name : "Unnassigned";
            _sparepartusageDTO.LastUpdate = (SparePartUsageXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SparePartUsageXPO.LastUpdate : (DateTime?)null;
            _sparepartusageDTO.LastUpdateByID = (SparePartUsageXPO.LastUpdateBy != null) ? SparePartUsageXPO.LastUpdateBy.Oid : 0;
            _sparepartusageDTO.LastUpdateByName = (SparePartUsageXPO.LastUpdateBy != null) ? SparePartUsageXPO.LastUpdateBy.Name : "Unnassigned";
            _sparepartusageDTO.IsActive = SparePartUsageXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepartusageDTO;
    }

    public static SparePartUsageXPO DTOtoXPO(SparePartUsageDTO SparePartUsageDTO, UnitOfWork UnitOfWork)
    {
        SparePartUsageXPO _sparepartusageXPO;
        try
        {
            _sparepartusageXPO = SparePartUsageDTO.ID == null || SparePartUsageDTO.ID == 0 ? new SparePartUsageXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SparePartUsageXPO>(SparePartUsageDTO.ID);
            _sparepartusageXPO.Ticket = (_sparepartusageXPO.Ticket != null && _sparepartusageXPO.Ticket.Oid == SparePartUsageDTO.TicketDTO.ID) ? _sparepartusageXPO.Ticket : UnitOfWork.GetObjectByKey<TicketXPO>(SparePartUsageDTO.TicketDTO.ID);
            _sparepartusageXPO.SparePartInventory = (_sparepartusageXPO.SparePartInventory?.Oid == SparePartUsageDTO.SparePartInventoryDTO.ID) ? _sparepartusageXPO.SparePartInventory : UnitOfWork.GetObjectByKey<SparePartInventoryXPO>(SparePartUsageDTO.SparePartInventoryDTO.ID);
            _sparepartusageXPO.Item_Line = (_sparepartusageXPO.Item_Line?.Oid == SparePartUsageDTO.Item_LineDTO.ID) ? _sparepartusageXPO.Item_Line : UnitOfWork.GetObjectByKey<Item_LineXPO>(SparePartUsageDTO.Item_LineDTO.ID);
            _sparepartusageXPO.Quantity = _sparepartusageXPO.Quantity == SparePartUsageDTO.Quantity ? _sparepartusageXPO.Quantity : SparePartUsageDTO.Quantity;
            _sparepartusageXPO.SparePart_Lot = (_sparepartusageXPO.SparePart_Lot?.Oid == SparePartUsageDTO.SparePart_LotDTO.ID) ? _sparepartusageXPO.SparePart_Lot : UnitOfWork.GetObjectByKey<SparePart_LotXPO>(SparePartUsageDTO.SparePart_LotDTO.ID);
            _sparepartusageXPO.AddedDate = _sparepartusageXPO.AddedDate ?? SparePartUsageDTO.AddedDate;
            _sparepartusageXPO.AddedBy = _sparepartusageXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(SparePartUsageDTO.AddedByID);
            _sparepartusageXPO.LastUpdate = _sparepartusageXPO.LastUpdate == SparePartUsageDTO.LastUpdate ? _sparepartusageXPO.LastUpdate : SparePartUsageDTO.LastUpdate;
            _sparepartusageXPO.LastUpdateBy = (_sparepartusageXPO.LastUpdateBy?.Oid == SparePartUsageDTO.LastUpdateByID) ? _sparepartusageXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SparePartUsageDTO.LastUpdateByID);
            _sparepartusageXPO.IsActive = _sparepartusageXPO.IsActive == SparePartUsageDTO.IsActive ? (bool)_sparepartusageXPO.IsActive : (bool)SparePartUsageDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepartusageXPO;
    }
}
