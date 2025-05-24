using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_HeaderMap
{
    public static Item_HeaderDTO XPOToDTO(Item_HeaderXPO Item_HeaderXPO)
    {
        var _item_headerDTO = new Item_HeaderDTO();
        try
        {
            _item_headerDTO.ID = Item_HeaderXPO.Oid;
            _item_headerDTO.Name = Item_HeaderXPO.Name;
            _item_headerDTO.NamesWithModel = $"{Item_HeaderXPO.Model} - {Item_HeaderXPO.Name}";
            _item_headerDTO.Model = Item_HeaderXPO.Model;
            _item_headerDTO.Brand = Item_HeaderXPO.Brand;
            _item_headerDTO.IsESD = Item_HeaderXPO.IsESD;
            _item_headerDTO.ItemClassificationID = (Item_HeaderXPO.ItemClassification != null) ? Item_HeaderXPO.ItemClassification.Oid : 0;
            _item_headerDTO.ItemClassificationName = (Item_HeaderXPO.ItemClassification != null) ? Item_HeaderXPO.ItemClassification.Name : "Unnassigned";
            _item_headerDTO.AddedDate = (Item_HeaderXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Item_HeaderXPO.AddedDate : (DateTime?)null;
            _item_headerDTO.AddedByID = (Item_HeaderXPO.AddedBy != null) ? Item_HeaderXPO.AddedBy.Oid : 0;
            _item_headerDTO.AddedByName = (Item_HeaderXPO.AddedBy != null) ? Item_HeaderXPO.AddedBy.Name : "Unnassigned";
            _item_headerDTO.LastUpdate = (Item_HeaderXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Item_HeaderXPO.LastUpdate : (DateTime?)null;
            _item_headerDTO.LastUpdateByID = (Item_HeaderXPO.LastUpdateBy != null) ? Item_HeaderXPO.LastUpdateBy.Oid : 0;
            _item_headerDTO.LastUpdateByName = (Item_HeaderXPO.LastUpdateBy != null) ? Item_HeaderXPO.LastUpdateBy.Name : "Unnassigned";
            _item_headerDTO.IsActive = Item_HeaderXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _item_headerDTO;
    }

    public static Item_HeaderXPO DTOtoXPO(Item_HeaderDTO Item_HeaderDTO, UnitOfWork UnitOfWork)
    {
        Item_HeaderXPO _item_headerXPO;
        try
        {
            _item_headerXPO = Item_HeaderDTO.ID == null || Item_HeaderDTO.ID == 0 ? new Item_HeaderXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Item_HeaderXPO>(Item_HeaderDTO.ID);
            _item_headerXPO.Name = _item_headerXPO.Name == Item_HeaderDTO.Name ? _item_headerXPO.Name : Item_HeaderDTO.Name;
            _item_headerXPO.Model = _item_headerXPO.Model == Item_HeaderDTO.Model ? _item_headerXPO.Model : Item_HeaderDTO.Model;
            _item_headerXPO.Brand = _item_headerXPO.Brand == Item_HeaderDTO.Brand ? _item_headerXPO.Brand : Item_HeaderDTO.Brand;
            _item_headerXPO.IsESD = _item_headerXPO.IsESD == Item_HeaderDTO.IsESD ? (bool)_item_headerXPO.IsESD : (bool)Item_HeaderDTO.IsESD;
            _item_headerXPO.ItemClassification = (_item_headerXPO.ItemClassification != null && _item_headerXPO.ItemClassification.Oid == Item_HeaderDTO.ItemClassificationID) ? _item_headerXPO.ItemClassification : UnitOfWork.GetObjectByKey<ItemClassificationXPO>(Item_HeaderDTO.ItemClassificationID);
            _item_headerXPO.AddedDate = _item_headerXPO.AddedDate != null ? _item_headerXPO.AddedDate : Item_HeaderDTO.AddedDate;
            _item_headerXPO.AddedBy = (_item_headerXPO.AddedBy != null) ? _item_headerXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Item_HeaderDTO.AddedByID);
            _item_headerXPO.LastUpdate = _item_headerXPO.LastUpdate == Item_HeaderDTO.LastUpdate ? _item_headerXPO.LastUpdate : Item_HeaderDTO.LastUpdate;
            _item_headerXPO.LastUpdateBy = (_item_headerXPO.LastUpdateBy != null && _item_headerXPO.LastUpdateBy.Oid == Item_HeaderDTO.LastUpdateByID) ? _item_headerXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Item_HeaderDTO.LastUpdateByID);
            _item_headerXPO.IsActive = _item_headerXPO.IsActive == Item_HeaderDTO.IsActive ? (bool)_item_headerXPO.IsActive : (bool)Item_HeaderDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _item_headerXPO;
    }
}
