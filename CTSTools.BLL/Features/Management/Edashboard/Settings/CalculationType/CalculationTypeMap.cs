using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.CalculationType;

public class CalculationTypeMap
{
    public static CalculationTypeDTO XPOToDTO(CalculationTypeXPO CalculationTypeXPO)
    {
        var _calculationtypeDTO = new CalculationTypeDTO();
        try
        {
            _calculationtypeDTO.ID = CalculationTypeXPO.Oid;
            _calculationtypeDTO.Name = CalculationTypeXPO.Name;
            _calculationtypeDTO.Description = CalculationTypeXPO.Description;
            _calculationtypeDTO.AddedDate = (CalculationTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? CalculationTypeXPO.AddedDate : (DateTime?)null;
            _calculationtypeDTO.AddedByID = (CalculationTypeXPO.AddedBy != null) ? CalculationTypeXPO.AddedBy.Oid : 0;
            _calculationtypeDTO.AddedByName = (CalculationTypeXPO.AddedBy != null) ? CalculationTypeXPO.AddedBy.Name : "Unnassigned";
            _calculationtypeDTO.LastUpdate = (CalculationTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? CalculationTypeXPO.LastUpdate : (DateTime?)null;
            _calculationtypeDTO.LastUpdateByID = (CalculationTypeXPO.LastUpdateBy != null) ? CalculationTypeXPO.LastUpdateBy.Oid : 0;
            _calculationtypeDTO.LastUpdateByName = (CalculationTypeXPO.LastUpdateBy != null) ? CalculationTypeXPO.LastUpdateBy.Name : "Unnassigned";
            _calculationtypeDTO.IsActive = CalculationTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _calculationtypeDTO;
    }

    public static CalculationTypeXPO DTOtoXPO(CalculationTypeDTO CalculationTypeDTO, UnitOfWork UnitOfWork)
    {
        CalculationTypeXPO _calculationtypeXPO;
        try
        {
            _calculationtypeXPO = CalculationTypeDTO.ID == null || CalculationTypeDTO.ID == 0 ? new CalculationTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(CalculationTypeDTO.ID);
            _calculationtypeXPO.Name = _calculationtypeXPO.Name == CalculationTypeDTO.Name ? _calculationtypeXPO.Name : CalculationTypeDTO.Name;
            _calculationtypeXPO.Description = _calculationtypeXPO.Description == CalculationTypeDTO.Description ? _calculationtypeXPO.Description : CalculationTypeDTO.Description;
            _calculationtypeXPO.AddedDate = _calculationtypeXPO.AddedDate != null ? _calculationtypeXPO.AddedDate : CalculationTypeDTO.AddedDate;
            _calculationtypeXPO.AddedBy = (_calculationtypeXPO.AddedBy != null) ? _calculationtypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(CalculationTypeDTO.AddedByID);
            _calculationtypeXPO.LastUpdate = _calculationtypeXPO.LastUpdate == CalculationTypeDTO.LastUpdate ? _calculationtypeXPO.LastUpdate : CalculationTypeDTO.LastUpdate;
            _calculationtypeXPO.LastUpdateBy = (_calculationtypeXPO.LastUpdateBy != null && _calculationtypeXPO.LastUpdateBy.Oid == CalculationTypeDTO.LastUpdateByID) ? _calculationtypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(CalculationTypeDTO.LastUpdateByID);
            _calculationtypeXPO.IsActive = _calculationtypeXPO.IsActive == CalculationTypeDTO.IsActive ? (bool)_calculationtypeXPO.IsActive : (bool)CalculationTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _calculationtypeXPO;
    }

}
