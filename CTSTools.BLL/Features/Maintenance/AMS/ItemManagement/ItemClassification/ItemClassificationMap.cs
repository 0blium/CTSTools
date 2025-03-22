using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.ItemClassification;

public class ItemClassificationMap
{
    public static ItemClassificationDTO XPOToDTO(ItemClassificationXPO ItemClassificationXPO)
    {
        var _itemclassificationDTO = new ItemClassificationDTO();
        try
        {
            _itemclassificationDTO.ID = ItemClassificationXPO.Oid;
            _itemclassificationDTO.EnglishName = ItemClassificationXPO.EnglishName;
            _itemclassificationDTO.SpanishName = ItemClassificationXPO.SpanishName;
            _itemclassificationDTO.Names = $"{ItemClassificationXPO.EnglishName} - {ItemClassificationXPO.SpanishName}";
            _itemclassificationDTO.Description = ItemClassificationXPO.Description;
            _itemclassificationDTO.AddedDate = (ItemClassificationXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ItemClassificationXPO.AddedDate : (DateTime?)null;
            _itemclassificationDTO.AddedByID = (ItemClassificationXPO.AddedBy != null) ? ItemClassificationXPO.AddedBy.Oid : 0;
            _itemclassificationDTO.AddedByName = (ItemClassificationXPO.AddedBy != null) ? ItemClassificationXPO.AddedBy.Name : "Unnassigned";
            _itemclassificationDTO.LastUpdate = (ItemClassificationXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ItemClassificationXPO.LastUpdate : (DateTime?)null;
            _itemclassificationDTO.LastUpdateByID = (ItemClassificationXPO.LastUpdateBy != null) ? ItemClassificationXPO.LastUpdateBy.Oid : 0;
            _itemclassificationDTO.LastUpdateByName = (ItemClassificationXPO.LastUpdateBy != null) ? ItemClassificationXPO.LastUpdateBy.Name : "Unnassigned";
            _itemclassificationDTO.IsActive = ItemClassificationXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _itemclassificationDTO;
    }

    public static ItemClassificationXPO DTOtoXPO(ItemClassificationDTO ItemClassificationDTO, UnitOfWork UnitOfWork)
    {
        ItemClassificationXPO _itemclassificationXPO;
        try
        {
            _itemclassificationXPO = ItemClassificationDTO.ID == null || ItemClassificationDTO.ID == 0 ? new ItemClassificationXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ItemClassificationXPO>(ItemClassificationDTO.ID);
            _itemclassificationXPO.EnglishName = _itemclassificationXPO.EnglishName == ItemClassificationDTO.EnglishName ? _itemclassificationXPO.EnglishName : ItemClassificationDTO.EnglishName;
            _itemclassificationXPO.SpanishName = _itemclassificationXPO.SpanishName == ItemClassificationDTO.SpanishName ? _itemclassificationXPO.SpanishName : ItemClassificationDTO.SpanishName;
            _itemclassificationXPO.Description = _itemclassificationXPO.Description == ItemClassificationDTO.Description ? _itemclassificationXPO.Description : ItemClassificationDTO.Description;
            _itemclassificationXPO.AddedDate = _itemclassificationXPO.AddedDate != null ? _itemclassificationXPO.AddedDate : ItemClassificationDTO.AddedDate;
            _itemclassificationXPO.AddedBy = _itemclassificationXPO.AddedBy != null ? _itemclassificationXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ItemClassificationDTO.AddedByID);
            _itemclassificationXPO.LastUpdate = _itemclassificationXPO.LastUpdate == ItemClassificationDTO.LastUpdate ? _itemclassificationXPO.LastUpdate : ItemClassificationDTO.LastUpdate;
            _itemclassificationXPO.LastUpdateBy = (_itemclassificationXPO.LastUpdateBy != null && _itemclassificationXPO.LastUpdateBy.Oid == ItemClassificationDTO.LastUpdateByID) ? _itemclassificationXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ItemClassificationDTO.LastUpdateByID);
            _itemclassificationXPO.IsActive = _itemclassificationXPO.IsActive == ItemClassificationDTO.IsActive ? (bool)_itemclassificationXPO.IsActive : (bool)ItemClassificationDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _itemclassificationXPO;
    }
}
