using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.ValueType;

public class ValueTypeMap
{
    public static ValueTypeDTO XPOToDTO(ValueTypeXPO ValueTypeXPO)
    {
        var _valuetypeDTO = new ValueTypeDTO();
        try
        {
            _valuetypeDTO.ID = ValueTypeXPO.Oid;
            _valuetypeDTO.Name = ValueTypeXPO.Name;
            _valuetypeDTO.Description = ValueTypeXPO.Description;
            _valuetypeDTO.AddedDate = (ValueTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ValueTypeXPO.AddedDate : (DateTime?)null;
            _valuetypeDTO.AddedByID = (ValueTypeXPO.AddedBy != null) ? ValueTypeXPO.AddedBy.Oid : 0;
            _valuetypeDTO.AddedByName = (ValueTypeXPO.AddedBy != null) ? ValueTypeXPO.AddedBy.Name : "Unnassigned";
            _valuetypeDTO.LastUpdate = (ValueTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ValueTypeXPO.LastUpdate : (DateTime?)null;
            _valuetypeDTO.LastUpdateByID = (ValueTypeXPO.LastUpdateBy != null) ? ValueTypeXPO.LastUpdateBy.Oid : 0;
            _valuetypeDTO.LastUpdateByName = (ValueTypeXPO.LastUpdateBy != null) ? ValueTypeXPO.LastUpdateBy.Name : "Unnassigned";
            _valuetypeDTO.IsActive = ValueTypeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valuetypeDTO;
    }

    public static ValueTypeXPO DTOtoXPO(ValueTypeDTO ValueTypeDTO, UnitOfWork UnitOfWork)
    {
        ValueTypeXPO _valuetypeXPO;
        try
        {
            _valuetypeXPO = ValueTypeDTO.ID == null || ValueTypeDTO.ID == 0 ? new ValueTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ValueTypeXPO>(ValueTypeDTO.ID);
            _valuetypeXPO.Name = _valuetypeXPO.Name == ValueTypeDTO.Name ? _valuetypeXPO.Name : ValueTypeDTO.Name;
            _valuetypeXPO.Description = _valuetypeXPO.Description == ValueTypeDTO.Description ? _valuetypeXPO.Description : ValueTypeDTO.Description;
            _valuetypeXPO.AddedDate = _valuetypeXPO.AddedDate != null ? _valuetypeXPO.AddedDate : ValueTypeDTO.AddedDate;
            _valuetypeXPO.AddedBy = (_valuetypeXPO.AddedBy != null) ? _valuetypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ValueTypeDTO.AddedByID);
            _valuetypeXPO.LastUpdate = _valuetypeXPO.LastUpdate == ValueTypeDTO.LastUpdate ? _valuetypeXPO.LastUpdate : ValueTypeDTO.LastUpdate;
            _valuetypeXPO.LastUpdateBy = (_valuetypeXPO.LastUpdateBy != null && _valuetypeXPO.LastUpdateBy.Oid == ValueTypeDTO.LastUpdateByID) ? _valuetypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ValueTypeDTO.LastUpdateByID);
            _valuetypeXPO.IsActive = _valuetypeXPO.IsActive == ValueTypeDTO.IsActive ? (bool)_valuetypeXPO.IsActive : (bool)ValueTypeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valuetypeXPO;
    }

}
