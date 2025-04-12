using CTSTools.DAL.Features.AdvancedSettings.TransactionOrigin;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.SparePart;
using CTSTools.DAL.Features.Maintenance.AMS.SparePart.Provider;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;

public class SparePart_LotMap
{
    public static SparePart_LotDTO XPOToDTO(SparePart_LotXPO SparePart_LotXPO)
    {
        var _sparepart_lotDTO = new SparePart_LotDTO();
        try
        {
            _sparepart_lotDTO.ID = SparePart_LotXPO.Oid;
            _sparepart_lotDTO.Quantity = SparePart_LotXPO.Quantity;
            _sparepart_lotDTO.AvailableQty = SparePart_LotXPO.AvailableQty;
            _sparepart_lotDTO.PartNumber = SparePart_LotXPO.PartNumber;
            _sparepart_lotDTO.ProviderDTO.ID = (SparePart_LotXPO.Provider != null) ? SparePart_LotXPO.Provider.Oid : 0;
            _sparepart_lotDTO.ProviderDTO.Name = (SparePart_LotXPO.Provider != null) ? SparePart_LotXPO.Provider.Name : "Unnassigned";
            _sparepart_lotDTO.SparePartDTO.ID = (SparePart_LotXPO.SparePart != null) ? SparePart_LotXPO.SparePart.Oid : 0;
            _sparepart_lotDTO.SparePartDTO.Name = (SparePart_LotXPO.SparePart != null) ? SparePart_LotXPO.SparePart.Name : "Unnassigned";
            _sparepart_lotDTO.LotSerialWithSparePartName = (SparePart_LotXPO.SparePart != null) ? $"{SparePart_LotXPO.Serial} - {SparePart_LotXPO.SparePart.Name}" : "Unnassigned";
            _sparepart_lotDTO.SparePartInventoryDTO.ID = (SparePart_LotXPO.SparePartInventory != null) ? SparePart_LotXPO.SparePartInventory.Oid : 0;
            _sparepart_lotDTO.SupportGroupDTO.ID = (SparePart_LotXPO.SupportGroup != null) ? SparePart_LotXPO.SupportGroup.Oid : 0;
            _sparepart_lotDTO.SupportGroupDTO.EnglishName = (SparePart_LotXPO.SupportGroup != null) ? SparePart_LotXPO.SupportGroup.EnglishName : "Unnassigned";
            _sparepart_lotDTO.TransactionOriginDTO.ID = (SparePart_LotXPO.TransactionOrigin != null) ? SparePart_LotXPO.TransactionOrigin.Oid : 0;
            _sparepart_lotDTO.TransactionOriginDTO.Name = (SparePart_LotXPO.TransactionOrigin != null) ? SparePart_LotXPO.TransactionOrigin.Name : "Unnassigned";
            _sparepart_lotDTO.TransactionNumber = SparePart_LotXPO.TransactionNumber;
            _sparepart_lotDTO.TransactionLine = SparePart_LotXPO.TransactionLine;
            _sparepart_lotDTO.Cost = SparePart_LotXPO.Cost;
            _sparepart_lotDTO.UnitCost = SparePart_LotXPO.UnitCost;
            _sparepart_lotDTO.Serial = SparePart_LotXPO.Serial;
            _sparepart_lotDTO.AddedDate = (SparePart_LotXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SparePart_LotXPO.AddedDate : (DateTime?)null;
            _sparepart_lotDTO.AddedByID = (SparePart_LotXPO.AddedBy != null) ? SparePart_LotXPO.AddedBy.Oid : 0;
            _sparepart_lotDTO.AddedByName = (SparePart_LotXPO.AddedBy != null) ? SparePart_LotXPO.AddedBy.Name : "Unnassigned";
            _sparepart_lotDTO.LastUpdate = (SparePart_LotXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SparePart_LotXPO.LastUpdate : (DateTime?)null;
            _sparepart_lotDTO.LastUpdateByID = (SparePart_LotXPO.LastUpdateBy != null) ? SparePart_LotXPO.LastUpdateBy.Oid : 0;
            _sparepart_lotDTO.LastUpdateByName = (SparePart_LotXPO.LastUpdateBy != null) ? SparePart_LotXPO.LastUpdateBy.Name : "Unnassigned";
            _sparepart_lotDTO.IsActive = SparePart_LotXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepart_lotDTO;
    }

    public static SparePart_LotXPO DTOtoXPO(SparePart_LotDTO SparePart_LotDTO, UnitOfWork UnitOfWork)
    {
        SparePart_LotXPO _sparepart_lotXPO;
        try
        {
            _sparepart_lotXPO = SparePart_LotDTO.ID == null || SparePart_LotDTO.ID == 0 ? new SparePart_LotXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SparePart_LotXPO>(SparePart_LotDTO.ID);
            _sparepart_lotXPO.Quantity = _sparepart_lotXPO.Quantity == SparePart_LotDTO.Quantity ? _sparepart_lotXPO.Quantity : SparePart_LotDTO.Quantity;
            _sparepart_lotXPO.AvailableQty = _sparepart_lotXPO.AvailableQty == SparePart_LotDTO.AvailableQty ? _sparepart_lotXPO.AvailableQty : SparePart_LotDTO.AvailableQty;
            _sparepart_lotXPO.PartNumber = _sparepart_lotXPO.PartNumber == SparePart_LotDTO.PartNumber ? _sparepart_lotXPO.PartNumber : SparePart_LotDTO.PartNumber;
            _sparepart_lotXPO.Provider = (_sparepart_lotXPO.Provider?.Oid == SparePart_LotDTO.ProviderDTO.ID) ? _sparepart_lotXPO.Provider : UnitOfWork.GetObjectByKey<ProviderXPO>(SparePart_LotDTO.ProviderDTO.ID);
            _sparepart_lotXPO.SparePart = (_sparepart_lotXPO.SparePart?.Oid == SparePart_LotDTO.SparePartDTO.ID) ? _sparepart_lotXPO.SparePart : UnitOfWork.GetObjectByKey<SparePartXPO>(SparePart_LotDTO.SparePartDTO.ID);
            _sparepart_lotXPO.SparePartInventory = (_sparepart_lotXPO.SparePartInventory?.Oid == SparePart_LotDTO.SparePartInventoryDTO.ID) ? _sparepart_lotXPO.SparePartInventory : UnitOfWork.GetObjectByKey<SparePartInventoryXPO>(SparePart_LotDTO.SparePartInventoryDTO.ID);
            _sparepart_lotXPO.SupportGroup = (_sparepart_lotXPO.SupportGroup?.Oid == SparePart_LotDTO.SupportGroupDTO.ID) ? _sparepart_lotXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(SparePart_LotDTO.SupportGroupDTO.ID);
            _sparepart_lotXPO.TransactionOrigin = (_sparepart_lotXPO.TransactionOrigin?.Oid == SparePart_LotDTO.TransactionOriginDTO.ID) ? _sparepart_lotXPO.TransactionOrigin : UnitOfWork.GetObjectByKey<TransactionOriginXPO>(SparePart_LotDTO.TransactionOriginDTO.ID);
            _sparepart_lotXPO.TransactionNumber = _sparepart_lotXPO.TransactionNumber == SparePart_LotDTO.TransactionNumber ? _sparepart_lotXPO.TransactionNumber : SparePart_LotDTO.TransactionNumber;
            _sparepart_lotXPO.TransactionLine = _sparepart_lotXPO.TransactionLine == SparePart_LotDTO.TransactionLine ? _sparepart_lotXPO.TransactionLine : SparePart_LotDTO.TransactionLine;
            _sparepart_lotXPO.Cost = _sparepart_lotXPO.Cost == SparePart_LotDTO.Cost ? _sparepart_lotXPO.Cost : SparePart_LotDTO.Cost;
            _sparepart_lotXPO.UnitCost = _sparepart_lotXPO.UnitCost == SparePart_LotDTO.UnitCost ? _sparepart_lotXPO.UnitCost : SparePart_LotDTO.UnitCost;
            _sparepart_lotXPO.Serial = _sparepart_lotXPO.Serial == SparePart_LotDTO.Serial ? _sparepart_lotXPO.Serial : SparePart_LotDTO.Serial;
            _sparepart_lotXPO.AddedDate = _sparepart_lotXPO.AddedDate ?? SparePart_LotDTO.AddedDate;
            _sparepart_lotXPO.AddedBy = _sparepart_lotXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(SparePart_LotDTO.AddedByID);
            _sparepart_lotXPO.LastUpdate = _sparepart_lotXPO.LastUpdate == SparePart_LotDTO.LastUpdate ? _sparepart_lotXPO.LastUpdate : SparePart_LotDTO.LastUpdate;
            _sparepart_lotXPO.LastUpdateBy = (_sparepart_lotXPO.LastUpdateBy?.Oid == SparePart_LotDTO.LastUpdateByID) ? _sparepart_lotXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SparePart_LotDTO.LastUpdateByID);
            _sparepart_lotXPO.IsActive = (bool)(_sparepart_lotXPO.IsActive == SparePart_LotDTO.IsActive ? (bool)_sparepart_lotXPO.IsActive : (bool)SparePart_LotDTO.IsActive);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _sparepart_lotXPO;
    }
}
