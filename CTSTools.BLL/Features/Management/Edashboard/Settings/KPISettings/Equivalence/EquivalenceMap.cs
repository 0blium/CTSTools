using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Equivalence;

public class EquivalenceMap
{
    public static EquivalenceDTO XPOToDTO(EquivalenceXPO EquivalenceXPO)
    {
        var _equivalenceDTO = new EquivalenceDTO();
        try
        {
            _equivalenceDTO.ID = EquivalenceXPO.Oid;
            _equivalenceDTO.Name = EquivalenceXPO.Name;
            _equivalenceDTO.Description = EquivalenceXPO.Description;
            _equivalenceDTO.AddedDate = (EquivalenceXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? EquivalenceXPO.AddedDate : (DateTime?)null;
            _equivalenceDTO.AddedByID = (EquivalenceXPO.AddedBy != null) ? EquivalenceXPO.AddedBy.Oid : 0;
            _equivalenceDTO.AddedByName = (EquivalenceXPO.AddedBy != null) ? EquivalenceXPO.AddedBy.Name : "Unnassigned";
            _equivalenceDTO.LastUpdate = (EquivalenceXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? EquivalenceXPO.LastUpdate : (DateTime?)null;
            _equivalenceDTO.LastUpdateByID = (EquivalenceXPO.LastUpdateBy != null) ? EquivalenceXPO.LastUpdateBy.Oid : 0;
            _equivalenceDTO.LastUpdateByName = (EquivalenceXPO.LastUpdateBy != null) ? EquivalenceXPO.LastUpdateBy.Name : "Unnassigned";
            _equivalenceDTO.IsActive = EquivalenceXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _equivalenceDTO;
    }

    public static EquivalenceXPO DTOtoXPO(EquivalenceDTO EquivalenceDTO, UnitOfWork UnitOfWork)
    {
        EquivalenceXPO _equivalenceXPO;
        try
        {
            _equivalenceXPO = EquivalenceDTO.ID == null || EquivalenceDTO.ID == 0 ? new EquivalenceXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<EquivalenceXPO>(EquivalenceDTO.ID);
            _equivalenceXPO.Name = _equivalenceXPO.Name == EquivalenceDTO.Name ? _equivalenceXPO.Name : EquivalenceDTO.Name;
            _equivalenceXPO.Description = _equivalenceXPO.Description == EquivalenceDTO.Description ? _equivalenceXPO.Description : EquivalenceDTO.Description;
            _equivalenceXPO.AddedDate = _equivalenceXPO.AddedDate != null ? _equivalenceXPO.AddedDate : EquivalenceDTO.AddedDate;
            _equivalenceXPO.AddedBy = (_equivalenceXPO.AddedBy != null) ? _equivalenceXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(EquivalenceDTO.AddedByID);
            _equivalenceXPO.LastUpdate = _equivalenceXPO.LastUpdate == EquivalenceDTO.LastUpdate ? _equivalenceXPO.LastUpdate : EquivalenceDTO.LastUpdate;
            _equivalenceXPO.LastUpdateBy = (_equivalenceXPO.LastUpdateBy != null && _equivalenceXPO.LastUpdateBy.Oid == EquivalenceDTO.LastUpdateByID) ? _equivalenceXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(EquivalenceDTO.LastUpdateByID);
            _equivalenceXPO.IsActive = _equivalenceXPO.IsActive == EquivalenceDTO.IsActive ? (bool)_equivalenceXPO.IsActive : (bool)EquivalenceDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _equivalenceXPO;
    }

}
