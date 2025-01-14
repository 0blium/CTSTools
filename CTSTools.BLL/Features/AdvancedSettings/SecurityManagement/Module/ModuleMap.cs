using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;

public class ModuleMap
{
    public static ModuleDTO XPOToDTO(ModuleXPO ModuleXPO)
    {
        var _moduleDTO = new ModuleDTO();
        try
        {
            _moduleDTO.ID = ModuleXPO.Oid;
            _moduleDTO.Name = ModuleXPO.Name;
            _moduleDTO.Description = ModuleXPO.Description;
            _moduleDTO.AddedDate = (ModuleXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ModuleXPO.AddedDate : (DateTime?)null;
            _moduleDTO.AddedByID = (ModuleXPO.AddedBy != null) ? ModuleXPO.AddedBy.Oid : 0;
            _moduleDTO.AddedByName = (ModuleXPO.AddedBy != null) ? ModuleXPO.AddedBy.Name : "Unnassigned";
            _moduleDTO.LastUpdate = (ModuleXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ModuleXPO.LastUpdate : (DateTime?)null;
            _moduleDTO.LastUpdateByID = (ModuleXPO.LastUpdateBy != null) ? ModuleXPO.LastUpdateBy.Oid : 0;
            _moduleDTO.LastUpdateByName = (ModuleXPO.LastUpdateBy != null) ? ModuleXPO.LastUpdateBy.Name : "Unnassigned";
            _moduleDTO.IsActive = ModuleXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _moduleDTO;
    }

    public static ModuleXPO DTOtoXPO(ModuleDTO ModuleDTO, UnitOfWork UnitOfWork)
    {
        ModuleXPO _moduleXPO;
        try
        {
            _moduleXPO = ModuleDTO.ID == null || ModuleDTO.ID == 0 ? new ModuleXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ModuleXPO>(ModuleDTO.ID);
            _moduleXPO.Name = _moduleXPO.Name == ModuleDTO.Name ? _moduleXPO.Name : ModuleDTO.Name;
            _moduleXPO.Description = _moduleXPO.Description == ModuleDTO.Description ? _moduleXPO.Description : ModuleDTO.Description;
            _moduleXPO.AddedDate = _moduleXPO.AddedDate != null ? _moduleXPO.AddedDate : ModuleDTO.AddedDate;
            _moduleXPO.AddedBy = (_moduleXPO.AddedBy != null) ? _moduleXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ModuleDTO.AddedByID);
            _moduleXPO.LastUpdate = _moduleXPO.LastUpdate == ModuleDTO.LastUpdate ? _moduleXPO.LastUpdate : ModuleDTO.LastUpdate;
            _moduleXPO.LastUpdateBy = (_moduleXPO.LastUpdateBy != null && _moduleXPO.LastUpdateBy.Oid == ModuleDTO.LastUpdateByID) ? _moduleXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ModuleDTO.LastUpdateByID);
            _moduleXPO.IsActive = _moduleXPO.IsActive == ModuleDTO.IsActive ? (bool)_moduleXPO.IsActive : (bool)ModuleDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _moduleXPO;
    }

}
