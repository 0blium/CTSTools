using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using CTSTools.DAL.Features.Engineering.ComponentID.SubClass;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Header;

public class Item_HeaderMap
{
    public static Item_HeaderDTO XPOToDTO(Item_HeaderXPO Item_HeaderXPO)
    {
        var _item_headerDTO = new Item_HeaderDTO();
        try
        {
            _item_headerDTO.ID = Item_HeaderXPO.Oid;
            _item_headerDTO.ModelWithBrand = $"{Item_HeaderXPO.Model} - {Item_HeaderXPO.Brand.Name}";
            _item_headerDTO.Model = Item_HeaderXPO.Model;
            _item_headerDTO.BrandID = (Item_HeaderXPO.Brand != null) ? Item_HeaderXPO.Brand.Oid : 0;
            _item_headerDTO.BrandName = (Item_HeaderXPO.Brand != null) ? Item_HeaderXPO.Brand.Name : "Unnassigned";
            _item_headerDTO.ClassID = (Item_HeaderXPO.Class != null) ? Item_HeaderXPO.Class.Oid : 0;
            _item_headerDTO.ClassName = (Item_HeaderXPO.Class != null) ? Item_HeaderXPO.Class.Name : "Unnassigned";
            _item_headerDTO.SubClassID = (Item_HeaderXPO.SubClass != null) ? Item_HeaderXPO.SubClass.Oid : 0;
            _item_headerDTO.SubClassName = (Item_HeaderXPO.SubClass != null) ? Item_HeaderXPO.SubClass.Name : "Unnassigned";
            //_item_headerDTO.IsESD = Item_HeaderXPO.IsESD;
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
            _item_headerXPO.Model = _item_headerXPO.Model == Item_HeaderDTO.Model ? _item_headerXPO.Model : Item_HeaderDTO.Model;
            _item_headerXPO.Brand = (_item_headerXPO.Brand != null && _item_headerXPO.Brand.Oid == Item_HeaderDTO.BrandID) ? _item_headerXPO.Brand : UnitOfWork.GetObjectByKey<BrandXPO>(Item_HeaderDTO.BrandID);
            _item_headerXPO.Class = (_item_headerXPO.Class != null && _item_headerXPO.Class.Oid == Item_HeaderDTO.ClassID) ? _item_headerXPO.Class : UnitOfWork.GetObjectByKey<ClassXPO>(Item_HeaderDTO.ClassID);
            _item_headerXPO.SubClass = (_item_headerXPO.SubClass != null && _item_headerXPO.SubClass.Oid == Item_HeaderDTO.SubClassID) ? _item_headerXPO.SubClass : UnitOfWork.GetObjectByKey<SubClassXPO>(Item_HeaderDTO.SubClassID);
            //_item_headerXPO.IsESD = _item_headerXPO.IsESD == Item_HeaderDTO.IsESD ? (bool)_item_headerXPO.IsESD : (bool)Item_HeaderDTO.IsESD;
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
    public static List<Item_HeaderXPO> DTOListToXPOList(List<Item_HeaderDTO> Item_HeaderDTOList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<Item_HeaderXPO>();
        try
        {
            foreach (var _Item_HeaderDTO in Item_HeaderDTOList)
            {
                _xPOList.Add(DTOtoXPO(_Item_HeaderDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
    public static List<Item_HeaderDTO> XPCollectionToList(XPCollection<Item_HeaderXPO> Item_HeaderXPCollection)
    {
        var _dTOList = new List<Item_HeaderDTO>();
        try
        {
            foreach (var _Item_HeaderXPO in Item_HeaderXPCollection)
            {
                _dTOList.Add(XPOToDTO(_Item_HeaderXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
}
