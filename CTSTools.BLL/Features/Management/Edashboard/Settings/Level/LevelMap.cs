using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.Level;

public class LevelMap
{
    public static LevelDTO XPOToDTO(LevelXPO LevelXPO)
    {
        var _levelDTO = new LevelDTO();
        try
        {
            _levelDTO.ID = LevelXPO.Oid;
            _levelDTO.Name = LevelXPO.Name;
            _levelDTO.Description = LevelXPO.Description;
            _levelDTO.AddedDate = (LevelXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? LevelXPO.AddedDate : (DateTime?)null;
            _levelDTO.AddedByID = (LevelXPO.AddedBy != null) ? LevelXPO.AddedBy.Oid : 0;
            _levelDTO.AddedByName = (LevelXPO.AddedBy != null) ? LevelXPO.AddedBy.Name : "Unnassigned";
            _levelDTO.LastUpdate = (LevelXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? LevelXPO.LastUpdate : (DateTime?)null;
            _levelDTO.LastUpdateByID = (LevelXPO.LastUpdateBy != null) ? LevelXPO.LastUpdateBy.Oid : 0;
            _levelDTO.LastUpdateByName = (LevelXPO.LastUpdateBy != null) ? LevelXPO.LastUpdateBy.Name : "Unnassigned";
            _levelDTO.IsActive = LevelXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _levelDTO;
    }

    public static LevelXPO DTOtoXPO(LevelDTO LevelDTO, UnitOfWork UnitOfWork)
    {
        LevelXPO _levelXPO;
        try
        {
            _levelXPO = LevelDTO.ID == null || LevelDTO.ID == 0 ? new LevelXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<LevelXPO>(LevelDTO.ID);
            _levelXPO.Name = _levelXPO.Name == LevelDTO.Name ? _levelXPO.Name : LevelDTO.Name;
            _levelXPO.Description = _levelXPO.Description == LevelDTO.Description ? _levelXPO.Description : LevelDTO.Description;
            _levelXPO.AddedDate = _levelXPO.AddedDate != null ? _levelXPO.AddedDate : LevelDTO.AddedDate;
            _levelXPO.AddedBy = (_levelXPO.AddedBy != null) ? _levelXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(LevelDTO.AddedByID);
            _levelXPO.LastUpdate = _levelXPO.LastUpdate == LevelDTO.LastUpdate ? _levelXPO.LastUpdate : LevelDTO.LastUpdate;
            _levelXPO.LastUpdateBy = (_levelXPO.LastUpdateBy != null && _levelXPO.LastUpdateBy.Oid == LevelDTO.LastUpdateByID) ? _levelXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(LevelDTO.LastUpdateByID);
            _levelXPO.IsActive = _levelXPO.IsActive == LevelDTO.IsActive ? (bool)_levelXPO.IsActive : (bool)LevelDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _levelXPO;
    }

}
