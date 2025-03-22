using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.SupplyType;

public class SupplyTypeMap
{
    public static SupplyTypeDTO XPOToDTO(SupplyTypeXPO SupplyTypeXPO)
    {
        var _supplyTypeDTO = new SupplyTypeDTO();
        try
        {
            _supplyTypeDTO.ID = SupplyTypeXPO.Oid;
            _supplyTypeDTO.Name = SupplyTypeXPO.Name;
            _supplyTypeDTO.Description = SupplyTypeXPO.Description;
            _supplyTypeDTO.AddedDate = (SupplyTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SupplyTypeXPO.AddedDate : (DateTime?)null;
            _supplyTypeDTO.AddedByID = (SupplyTypeXPO.AddedBy != null) ? SupplyTypeXPO.AddedBy.Oid : 0;
            _supplyTypeDTO.AddedByName = (SupplyTypeXPO.AddedBy != null) ? SupplyTypeXPO.AddedBy.Name : "Unnassigned";
            _supplyTypeDTO.LastUpdate = (SupplyTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SupplyTypeXPO.LastUpdate : (DateTime?)null;
            _supplyTypeDTO.LastUpdateByID = (SupplyTypeXPO.LastUpdateBy != null) ? SupplyTypeXPO.LastUpdateBy.Oid : 0;
            _supplyTypeDTO.LastUpdateByName = (SupplyTypeXPO.LastUpdateBy != null) ? SupplyTypeXPO.LastUpdateBy.Name : "Unnassigned";
            _supplyTypeDTO.IsActive = SupplyTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supplyTypeDTO;
    }

    public static SupplyTypeXPO DTOtoXPO(SupplyTypeDTO SupplyTypeDTO, UnitOfWork UnitOfWork)
    {
        SupplyTypeXPO _supplytypeXPO;
        try
        {
            _supplytypeXPO = SupplyTypeDTO.ID == null || SupplyTypeDTO.ID == 0 ? new SupplyTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SupplyTypeXPO>(SupplyTypeDTO.ID);
            _supplytypeXPO.Name = _supplytypeXPO.Name == SupplyTypeDTO.Name ? _supplytypeXPO.Name : SupplyTypeDTO.Name;
            _supplytypeXPO.Description = _supplytypeXPO.Description == SupplyTypeDTO.Description ? _supplytypeXPO.Description : SupplyTypeDTO.Description;
            _supplytypeXPO.AddedDate = _supplytypeXPO.AddedDate != null ? _supplytypeXPO.AddedDate : SupplyTypeDTO.AddedDate;
            _supplytypeXPO.AddedBy = (_supplytypeXPO.AddedBy != null) ? _supplytypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(SupplyTypeDTO.AddedByID);
            _supplytypeXPO.LastUpdate = _supplytypeXPO.LastUpdate == SupplyTypeDTO.LastUpdate ? _supplytypeXPO.LastUpdate : SupplyTypeDTO.LastUpdate;
            _supplytypeXPO.LastUpdateBy = (_supplytypeXPO.LastUpdateBy != null && _supplytypeXPO.LastUpdateBy.Oid == SupplyTypeDTO.LastUpdateByID) ? _supplytypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SupplyTypeDTO.LastUpdateByID);
            _supplytypeXPO.IsActive = _supplytypeXPO.IsActive == SupplyTypeDTO.IsActive ? (bool)_supplytypeXPO.IsActive : (bool)SupplyTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supplytypeXPO;
    }
}
