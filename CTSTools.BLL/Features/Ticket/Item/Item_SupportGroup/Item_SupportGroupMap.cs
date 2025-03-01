using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Ticket.Item.Item_SupportGroup;

public class Item_SupportGroupMap
{
    public static Item_SupportGroupDTO XPOToDTO(Item_SupportGroupXPO Item_SupportGroupXPO)
    {
        var _item_supportgroupDTO = new Item_SupportGroupDTO();
        try
        {
            _item_supportgroupDTO.ID = Item_SupportGroupXPO.Oid;
            _item_supportgroupDTO.Item_HeaderDTO.ID = (Item_SupportGroupXPO.Item_Header != null) ? Item_SupportGroupXPO.Item_Header.Oid : 0;
            _item_supportgroupDTO.Item_HeaderDTO.SpanishName = (Item_SupportGroupXPO.Item_Header != null) ? Item_SupportGroupXPO.Item_Header.SpanishName : "Unnassigned";
            _item_supportgroupDTO.Item_HeaderDTO.EnglishName = (Item_SupportGroupXPO.Item_Header != null) ? Item_SupportGroupXPO.Item_Header.EnglishName : "Unnassigned";
            _item_supportgroupDTO.SupportGroupDTO.ID = (Item_SupportGroupXPO.SupportGroup != null) ? Item_SupportGroupXPO.SupportGroup.Oid : 0;
            _item_supportgroupDTO.SupportGroupDTO.SpanishName = (Item_SupportGroupXPO.SupportGroup != null) ? Item_SupportGroupXPO.SupportGroup.SpanishName : "Unnassigned";
            _item_supportgroupDTO.SupportGroupDTO.EnglishName = (Item_SupportGroupXPO.SupportGroup != null) ? Item_SupportGroupXPO.SupportGroup.EnglishName : "Unnassigned";
            _item_supportgroupDTO.AddedDate = (Item_SupportGroupXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Item_SupportGroupXPO.AddedDate : (DateTime?)null;
            _item_supportgroupDTO.AddedByID = (Item_SupportGroupXPO.AddedBy != null) ? Item_SupportGroupXPO.AddedBy.Oid : 0;
            _item_supportgroupDTO.AddedByName = (Item_SupportGroupXPO.AddedBy != null) ? Item_SupportGroupXPO.AddedBy.Name : "Unnassigned";
            _item_supportgroupDTO.LastUpdate = (Item_SupportGroupXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Item_SupportGroupXPO.LastUpdate : (DateTime?)null;
            _item_supportgroupDTO.LastUpdateByID = (Item_SupportGroupXPO.LastUpdateBy != null) ? Item_SupportGroupXPO.LastUpdateBy.Oid : 0;
            _item_supportgroupDTO.LastUpdateByName = (Item_SupportGroupXPO.LastUpdateBy != null) ? Item_SupportGroupXPO.LastUpdateBy.Name : "Unnassigned";
            _item_supportgroupDTO.IsActive = Item_SupportGroupXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _item_supportgroupDTO;
    }

    public static Item_SupportGroupXPO DTOtoXPO(Item_SupportGroupDTO Item_SupportGroupDTO, UnitOfWork UnitOfWork)
    {
        Item_SupportGroupXPO _item_supportgroupXPO;
        try
        {
            _item_supportgroupXPO = Item_SupportGroupDTO.ID == null || Item_SupportGroupDTO.ID == 0 ? new Item_SupportGroupXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Item_SupportGroupXPO>(Item_SupportGroupDTO.ID);
            _item_supportgroupXPO.Item_Header = (_item_supportgroupXPO.Item_Header != null && _item_supportgroupXPO.Item_Header.Oid == Item_SupportGroupDTO.Item_HeaderDTO.ID) ? _item_supportgroupXPO.Item_Header : UnitOfWork.GetObjectByKey<Item_HeaderXPO>(Item_SupportGroupDTO.Item_HeaderDTO.ID);
            _item_supportgroupXPO.SupportGroup = (_item_supportgroupXPO.SupportGroup != null && _item_supportgroupXPO.SupportGroup.Oid == Item_SupportGroupDTO.SupportGroupDTO.ID) ? _item_supportgroupXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(Item_SupportGroupDTO.SupportGroupDTO.ID);
            _item_supportgroupXPO.AddedDate = _item_supportgroupXPO.AddedDate != null ? _item_supportgroupXPO.AddedDate : Item_SupportGroupDTO.AddedDate;
            _item_supportgroupXPO.AddedBy = (_item_supportgroupXPO.AddedBy != null) ? _item_supportgroupXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Item_SupportGroupDTO.AddedByID);
            _item_supportgroupXPO.LastUpdate = _item_supportgroupXPO.LastUpdate == Item_SupportGroupDTO.LastUpdate ? _item_supportgroupXPO.LastUpdate : Item_SupportGroupDTO.LastUpdate;
            _item_supportgroupXPO.LastUpdateBy = (_item_supportgroupXPO.LastUpdateBy != null && _item_supportgroupXPO.LastUpdateBy.Oid == Item_SupportGroupDTO.LastUpdateByID) ? _item_supportgroupXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Item_SupportGroupDTO.LastUpdateByID);
            _item_supportgroupXPO.IsActive = _item_supportgroupXPO.IsActive == Item_SupportGroupDTO.IsActive ? (bool)_item_supportgroupXPO.IsActive : (bool)Item_SupportGroupDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _item_supportgroupXPO;
    }
}
