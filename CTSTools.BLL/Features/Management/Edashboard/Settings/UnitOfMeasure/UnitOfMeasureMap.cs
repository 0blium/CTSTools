using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;

public class UnitOfMeasureMap
{
    public static UnitOfMeasureDTO XPOToDTO(UnitOfMeasureXPO UnitOfMeasureXPO)
    {
        var _unitofmeasureDTO = new UnitOfMeasureDTO();
        try
        {
            _unitofmeasureDTO.ID = UnitOfMeasureXPO.Oid;
            _unitofmeasureDTO.Name = UnitOfMeasureXPO.Name;
            _unitofmeasureDTO.Description = UnitOfMeasureXPO.Description;
            _unitofmeasureDTO.AddedDate = (UnitOfMeasureXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? UnitOfMeasureXPO.AddedDate : (DateTime?)null;
            _unitofmeasureDTO.AddedByID = (UnitOfMeasureXPO.AddedBy != null) ? UnitOfMeasureXPO.AddedBy.Oid : 0;
            _unitofmeasureDTO.AddedByName = (UnitOfMeasureXPO.AddedBy != null) ? UnitOfMeasureXPO.AddedBy.Name : "Unnassigned";
            _unitofmeasureDTO.LastUpdate = (UnitOfMeasureXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? UnitOfMeasureXPO.LastUpdate : (DateTime?)null;
            _unitofmeasureDTO.LastUpdateByID = (UnitOfMeasureXPO.LastUpdateBy != null) ? UnitOfMeasureXPO.LastUpdateBy.Oid : 0;
            _unitofmeasureDTO.LastUpdateByName = (UnitOfMeasureXPO.LastUpdateBy != null) ? UnitOfMeasureXPO.LastUpdateBy.Name : "Unnassigned";
            _unitofmeasureDTO.IsActive = UnitOfMeasureXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _unitofmeasureDTO;
    }

    public static UnitOfMeasureXPO DTOtoXPO(UnitOfMeasureDTO UnitOfMeasureDTO, UnitOfWork UnitOfWork)
    {
        UnitOfMeasureXPO _unitofmeasureXPO;
        try
        {
            _unitofmeasureXPO = UnitOfMeasureDTO.ID == null || UnitOfMeasureDTO.ID == 0 ? new UnitOfMeasureXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<UnitOfMeasureXPO>(UnitOfMeasureDTO.ID);
            _unitofmeasureXPO.Name = _unitofmeasureXPO.Name == UnitOfMeasureDTO.Name ? _unitofmeasureXPO.Name : UnitOfMeasureDTO.Name;
            _unitofmeasureXPO.Description = _unitofmeasureXPO.Description == UnitOfMeasureDTO.Description ? _unitofmeasureXPO.Description : UnitOfMeasureDTO.Description;
            _unitofmeasureXPO.AddedDate = _unitofmeasureXPO.AddedDate != null ? _unitofmeasureXPO.AddedDate : UnitOfMeasureDTO.AddedDate;
            _unitofmeasureXPO.AddedBy = (_unitofmeasureXPO.AddedBy != null) ? _unitofmeasureXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(UnitOfMeasureDTO.AddedByID);
            _unitofmeasureXPO.LastUpdate = _unitofmeasureXPO.LastUpdate == UnitOfMeasureDTO.LastUpdate ? _unitofmeasureXPO.LastUpdate : UnitOfMeasureDTO.LastUpdate;
            _unitofmeasureXPO.LastUpdateBy = (_unitofmeasureXPO.LastUpdateBy != null && _unitofmeasureXPO.LastUpdateBy.Oid == UnitOfMeasureDTO.LastUpdateByID) ? _unitofmeasureXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(UnitOfMeasureDTO.LastUpdateByID);
            _unitofmeasureXPO.IsActive = _unitofmeasureXPO.IsActive == UnitOfMeasureDTO.IsActive ? (bool)_unitofmeasureXPO.IsActive : (bool)UnitOfMeasureDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _unitofmeasureXPO;
    }

}
