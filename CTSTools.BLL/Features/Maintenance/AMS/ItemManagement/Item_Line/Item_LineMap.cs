using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.TransactionOrigin;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using CTSTools.DAL.Features.Maintenance.AMS.Station;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_LineMap
{
    public static Item_LineDTO XPOToDTO(Item_LineXPO Item_LineXPO)
    {
        var _item_lineDTO = new Item_LineDTO();
        try
        {
            _item_lineDTO.ID = Item_LineXPO.Oid;
            _item_lineDTO.Item_HeaderDTO.ID = (Item_LineXPO.Item_Header != null) ? Item_LineXPO.Item_Header.Oid : 0;
            _item_lineDTO.Item_HeaderDTO.EnglishName = (Item_LineXPO.Item_Header != null) ? Item_LineXPO.Item_Header.EnglishName : "Unnassigned";
            _item_lineDTO.ItemHeaderNameWithPartNumberSerial = (Item_LineXPO.Item_Header != null) ? $"{Item_LineXPO.Serial} - {Item_LineXPO.Item_Header.EnglishName} - {Item_LineXPO.ManufactureSerialID}" : "Unnassigned";
            _item_lineDTO.ItemNameWithManufactureSerial = (Item_LineXPO.Item_Header != null) ? $"{Item_LineXPO.Item_Header.EnglishName} - {Item_LineXPO.ManufactureSerialID}" : "Unnassigned";
            _item_lineDTO.StationDTO.ID = (Item_LineXPO.Station != null) ? Item_LineXPO.Station.Oid : 0;
            _item_lineDTO.StationDTO.Name = (Item_LineXPO.Station != null) ? Item_LineXPO.Station.Name : "Unnassigned";
            _item_lineDTO.StatusDTO.ID = (Item_LineXPO.Status != null) ? Item_LineXPO.Status.Oid : 0;
            _item_lineDTO.StatusDTO.Name = (Item_LineXPO.Status != null) ? Item_LineXPO.Status.Name : "Unnassigned";
            _item_lineDTO.OwnerDTO.ID = (Item_LineXPO.Owner != null) ? Item_LineXPO.Owner.Oid : 0;
            _item_lineDTO.OwnerDTO.Name = (Item_LineXPO.Owner != null) ? Item_LineXPO.Owner.Name : "Unnassigned";
            _item_lineDTO.Item_SupportGroupDTO.ID = (Item_LineXPO.Item_SupportGroup != null) ? Item_LineXPO.Item_SupportGroup.Oid : 0;
            _item_lineDTO.Serial = Item_LineXPO.Serial;
            _item_lineDTO.ManufactureSerialID = Item_LineXPO.ManufactureSerialID;
            _item_lineDTO.LegacyID = Item_LineXPO.LegacyID;
            _item_lineDTO.ShipmentReceiptID = Item_LineXPO.ShipmentReceiptID;
            _item_lineDTO.BasePriceUSD = Item_LineXPO.BasePriceUSD;
            _item_lineDTO.TransactionNumber = Item_LineXPO.TransactionNumber;
            _item_lineDTO.TransactionLine = Item_LineXPO.TransactionLine;
            _item_lineDTO.Comments = Item_LineXPO.Comments;
            _item_lineDTO.AddedDate = (Item_LineXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Item_LineXPO.AddedDate : (DateTime?)null;
            _item_lineDTO.AddedByID = (Item_LineXPO.AddedBy != null) ? Item_LineXPO.AddedBy.Oid : 0;
            _item_lineDTO.AddedByName = (Item_LineXPO.AddedBy != null) ? Item_LineXPO.AddedBy.Name : "Unnassigned";
            _item_lineDTO.LastUpdate = (Item_LineXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Item_LineXPO.LastUpdate : (DateTime?)null;
            _item_lineDTO.LastUpdateByID = (Item_LineXPO.LastUpdateBy != null) ? Item_LineXPO.LastUpdateBy.Oid : 0;
            _item_lineDTO.LastUpdateByName = (Item_LineXPO.LastUpdateBy != null) ? Item_LineXPO.LastUpdateBy.Name : "Unnassigned";
            _item_lineDTO.IsActive = Item_LineXPO.IsActive;
            _item_lineDTO.ImportInvoice = Item_LineXPO.ImportInvoice;
            _item_lineDTO.ShipmentReceiptNumber = Item_LineXPO.ShipmentReceiptNumber;
            _item_lineDTO.SupplyTypeDTO.ID = (Item_LineXPO.SupplyType != null) ? Item_LineXPO.SupplyType.Oid : 0;
            _item_lineDTO.SupplyTypeDTO.Name = (Item_LineXPO.SupplyType != null) ? Item_LineXPO.SupplyType.Name : "Unnassigned";
            _item_lineDTO.DeliveredDate = (Item_LineXPO.DeliveredDate.ToString() != DateTime.MinValue.ToString()) ? Item_LineXPO.DeliveredDate : (DateTime?)null;
            //_item_lineDTO.DeliveredToDTO.ID = (Item_LineXPO.DeliveredTo != null) ? Item_LineXPO.DeliveredTo.Oid : 0;
            //_item_lineDTO.DeliveredToDTO.Name = (Item_LineXPO.DeliveredTo != null) ? Item_LineXPO.DeliveredTo.Name : "Unassigned";
            _item_lineDTO.ImportInvoiceLine = Item_LineXPO.ImportInvoiceLine;
            _item_lineDTO.DeclarationNumber = Item_LineXPO.DeclarationNumber;
            _item_lineDTO.TransactionOriginDTO.ID = (Item_LineXPO.TransactionOrigin != null) ? Item_LineXPO.TransactionOrigin.Oid : 0;
            _item_lineDTO.TransactionOriginDTO.Name = (Item_LineXPO.TransactionOrigin != null) ? Item_LineXPO.TransactionOrigin.Name : "Unassigned";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _item_lineDTO;
    }

    public static Item_LineXPO DTOtoXPO(Item_LineDTO Item_LineDTO, UnitOfWork UnitOfWork)
    {
        Item_LineXPO _item_lineXPO;
        try
        {
            _item_lineXPO = Item_LineDTO.ID == null || Item_LineDTO.ID == 0 ? new Item_LineXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Item_LineXPO>(Item_LineDTO.ID);
            _item_lineXPO.Item_Header = (_item_lineXPO.Item_Header != null && _item_lineXPO.Item_Header.Oid == Item_LineDTO.Item_HeaderDTO.ID) ? _item_lineXPO.Item_Header : UnitOfWork.GetObjectByKey<Item_HeaderXPO>(Item_LineDTO.Item_HeaderDTO.ID);
            _item_lineXPO.Station = (_item_lineXPO.Station != null && _item_lineXPO.Station.Oid == Item_LineDTO.StationDTO.ID) ? _item_lineXPO.Station : UnitOfWork.GetObjectByKey<StationXPO>(Item_LineDTO.StationDTO.ID);
            _item_lineXPO.Status = (_item_lineXPO.Status != null && _item_lineXPO.Status.Oid == Item_LineDTO.StatusDTO.ID) ? _item_lineXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(Item_LineDTO.StatusDTO.ID);
            _item_lineXPO.Owner = (_item_lineXPO.Owner != null && _item_lineXPO.Owner.Oid == Item_LineDTO.OwnerDTO.ID) ? _item_lineXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(Item_LineDTO.OwnerDTO.ID);
            _item_lineXPO.Serial = _item_lineXPO.Serial == Item_LineDTO.Serial ? _item_lineXPO.Serial : Item_LineDTO.Serial;
            _item_lineXPO.ManufactureSerialID = _item_lineXPO.ManufactureSerialID == Item_LineDTO.ManufactureSerialID ? _item_lineXPO.ManufactureSerialID : Item_LineDTO.ManufactureSerialID;
            _item_lineXPO.LegacyID = _item_lineXPO.LegacyID == Item_LineDTO.LegacyID ? _item_lineXPO.LegacyID : Item_LineDTO.LegacyID;
            _item_lineXPO.ShipmentReceiptID = _item_lineXPO.ShipmentReceiptID == Item_LineDTO.ShipmentReceiptID ? _item_lineXPO.ShipmentReceiptID : Item_LineDTO.ShipmentReceiptID;
            _item_lineXPO.BasePriceUSD = _item_lineXPO.BasePriceUSD == Item_LineDTO.BasePriceUSD ? _item_lineXPO.BasePriceUSD : Item_LineDTO.BasePriceUSD;
            _item_lineXPO.TransactionNumber = _item_lineXPO.TransactionNumber == Item_LineDTO.TransactionNumber ? _item_lineXPO.TransactionNumber : Item_LineDTO.TransactionNumber;
            _item_lineXPO.TransactionLine = _item_lineXPO.TransactionLine == Item_LineDTO.TransactionLine ? _item_lineXPO.TransactionLine : Item_LineDTO.TransactionLine;
            _item_lineXPO.Comments = _item_lineXPO.Comments == Item_LineDTO.Comments ? _item_lineXPO.Comments : Item_LineDTO.Comments;
            _item_lineXPO.AddedDate = _item_lineXPO.AddedDate != null ? _item_lineXPO.AddedDate : Item_LineDTO.AddedDate;
            _item_lineXPO.AddedBy = (_item_lineXPO.AddedBy != null) ? _item_lineXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Item_LineDTO.AddedByID);
            _item_lineXPO.LastUpdate = _item_lineXPO.LastUpdate == Item_LineDTO.LastUpdate ? _item_lineXPO.LastUpdate : Item_LineDTO.LastUpdate;
            _item_lineXPO.LastUpdateBy = (_item_lineXPO.LastUpdateBy != null && _item_lineXPO.LastUpdateBy.Oid == Item_LineDTO.LastUpdateByID) ? _item_lineXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Item_LineDTO.LastUpdateByID);
            _item_lineXPO.IsActive = _item_lineXPO.IsActive == Item_LineDTO.IsActive ? (bool)_item_lineXPO.IsActive : (bool)Item_LineDTO.IsActive;
            _item_lineXPO.ImportInvoice = Item_LineDTO.ImportInvoice;
            _item_lineXPO.ShipmentReceiptNumber = Item_LineDTO.ShipmentReceiptNumber;
            _item_lineXPO.Item_SupportGroup = (_item_lineXPO.Item_SupportGroup != null && _item_lineXPO.Item_SupportGroup.Oid == Item_LineDTO.Item_SupportGroupDTO.ID) ? _item_lineXPO.Item_SupportGroup : UnitOfWork.GetObjectByKey<Item_SupportGroupXPO>(Item_LineDTO.Item_SupportGroupDTO.ID);
            _item_lineXPO.SupplyType = (_item_lineXPO.SupplyType != null && _item_lineXPO.SupplyType.Oid == Item_LineDTO.SupplyTypeDTO.ID) ? _item_lineXPO.SupplyType : UnitOfWork.GetObjectByKey<SupplyTypeXPO>(Item_LineDTO.SupplyTypeDTO.ID);
            _item_lineXPO.DeliveredDate = _item_lineXPO.DeliveredDate != null ? _item_lineXPO.DeliveredDate : Item_LineDTO.DeliveredDate;
            _item_lineXPO.ImportInvoiceLine = _item_lineXPO.ImportInvoiceLine == Item_LineDTO.ImportInvoiceLine ? _item_lineXPO.ImportInvoiceLine : Item_LineDTO.ImportInvoiceLine;
            _item_lineXPO.DeclarationNumber = _item_lineXPO.DeclarationNumber == Item_LineDTO.DeclarationNumber ? _item_lineXPO.DeclarationNumber : Item_LineDTO.DeclarationNumber;
            _item_lineXPO.TransactionOrigin = (_item_lineXPO.TransactionOrigin != null && _item_lineXPO.TransactionOrigin.Oid == Item_LineDTO.TransactionOriginDTO.ID) ? _item_lineXPO.TransactionOrigin : UnitOfWork.GetObjectByKey<TransactionOriginXPO>(Item_LineDTO.TransactionOriginDTO.ID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _item_lineXPO;
    }
}
