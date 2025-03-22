using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Provider;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.Provider;

public class ProviderMap
{
    public static ProviderDTO XPOToDTO(ProviderXPO ProviderXPO)
    {
        var _providerDTO = new ProviderDTO();
        try
        {
            _providerDTO.ID = ProviderXPO.Oid;
            _providerDTO.Name = ProviderXPO.Name;
            _providerDTO.Description = ProviderXPO.Description;
            _providerDTO.AddedDate = (ProviderXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ProviderXPO.AddedDate : (DateTime?)null;
            _providerDTO.AddedByID = (ProviderXPO.AddedBy != null) ? ProviderXPO.AddedBy.Oid : 0;
            _providerDTO.AddedByName = (ProviderXPO.AddedBy != null) ? ProviderXPO.AddedBy.Name : "Unnassigned";
            _providerDTO.LastUpdate = (ProviderXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ProviderXPO.LastUpdate : (DateTime?)null;
            _providerDTO.LastUpdateByID = (ProviderXPO.LastUpdateBy != null) ? ProviderXPO.LastUpdateBy.Oid : 0;
            _providerDTO.LastUpdateByName = (ProviderXPO.LastUpdateBy != null) ? ProviderXPO.LastUpdateBy.Name : "Unnassigned";
            _providerDTO.IsActive = ProviderXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _providerDTO;
    }

    public static ProviderXPO DTOtoXPO(ProviderDTO ProviderDTO, UnitOfWork UnitOfWork)
    {
        ProviderXPO _providerXPO;
        try
        {
            _providerXPO = ProviderDTO.ID == null || ProviderDTO.ID == 0 ? new ProviderXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ProviderXPO>(ProviderDTO.ID);
            _providerXPO.Name = _providerXPO.Name == ProviderDTO.Name ? _providerXPO.Name : ProviderDTO.Name;
            _providerXPO.Description = _providerXPO.Description == ProviderDTO.Description ? _providerXPO.Description : ProviderDTO.Description;
            _providerXPO.AddedDate = _providerXPO.AddedDate ?? ProviderDTO.AddedDate;
            _providerXPO.AddedBy = _providerXPO.AddedBy ?? UnitOfWork.GetObjectByKey<UserXPO>(ProviderDTO.AddedByID);
            _providerXPO.LastUpdate = _providerXPO.LastUpdate == ProviderDTO.LastUpdate ? _providerXPO.LastUpdate : ProviderDTO.LastUpdate;
            _providerXPO.LastUpdateBy = (_providerXPO.LastUpdateBy?.Oid == ProviderDTO.LastUpdateByID) ? _providerXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ProviderDTO.LastUpdateByID);
            _providerXPO.IsActive = (bool)(_providerXPO.IsActive == ProviderDTO.IsActive ? (bool)_providerXPO.IsActive : (bool)ProviderDTO.IsActive);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _providerXPO;
    }
}
