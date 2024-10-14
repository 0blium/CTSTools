using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;

public class DecoderMap
{
    public static DecoderDTO XPOToDTO(DecoderXPO DecoderXPO)
    {
        var _decoderDTO = new DecoderDTO();
        try
        {
            _decoderDTO.ID = DecoderXPO.Oid;
            _decoderDTO.StatusID = (DecoderXPO.Status != null) ? DecoderXPO.Status.Oid : 0;
            _decoderDTO.StatusName = (DecoderXPO.Status != null) ? DecoderXPO.Status.Name : "Unnassigned";
            _decoderDTO.Description = DecoderXPO.Description;
            _decoderDTO.AddedDate = (DecoderXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DecoderXPO.AddedDate : (DateTime?)null;
            _decoderDTO.AddedByID = (DecoderXPO.AddedBy != null) ? DecoderXPO.AddedBy.Oid : 0;
            _decoderDTO.AddedByName = (DecoderXPO.AddedBy != null) ? DecoderXPO.AddedBy.Name : "Unnassigned";
            _decoderDTO.ClassID = (DecoderXPO.Class != null) ? DecoderXPO.Class.Oid : 0;
            _decoderDTO.ClassName = (DecoderXPO.Class != null) ? DecoderXPO.Class.Name : "Unnassigned";
            _decoderDTO.SubClassID = (DecoderXPO.SubClass != null) ? DecoderXPO.SubClass.Oid : 0;
            _decoderDTO.SubClassName = (DecoderXPO.SubClass != null) ? DecoderXPO.SubClass.Name : "Unnassigned";
            _decoderDTO.PartTypeID = (DecoderXPO.PartType != null) ? DecoderXPO.PartType.Oid : 0;
            _decoderDTO.PartTypeName = (DecoderXPO.PartType != null) ? DecoderXPO.PartType.Name : "Unnassigned";
            _decoderDTO.ComponentTypeID = (DecoderXPO.ComponentType != null) ? DecoderXPO.ComponentType.Oid : 0;
            _decoderDTO.ComponentTypeName = (DecoderXPO.ComponentType != null) ? DecoderXPO.ComponentType.Name : "Unnassigned";
            _decoderDTO.LastUpdate = (DecoderXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DecoderXPO.LastUpdate : (DateTime?)null;
            _decoderDTO.LastUpdateByID = (DecoderXPO.LastUpdateBy != null) ? DecoderXPO.LastUpdateBy.Oid : 0;
            _decoderDTO.LastUpdateByName = (DecoderXPO.LastUpdateBy != null) ? DecoderXPO.LastUpdateBy.Name : "Unnassigned";
            _decoderDTO.IsActive = DecoderXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderDTO;
    }

    public static DecoderXPO DTOtoXPO(DecoderDTO DecoderDTO, UnitOfWork UnitOfWork)
    {
        DecoderXPO _decoderXPO;
        try
        {
            _decoderXPO = DecoderDTO.ID == null || DecoderDTO.ID == 0 ? new DecoderXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DecoderXPO>(DecoderDTO.ID);
            _decoderXPO.Status = (_decoderXPO.Status != null && _decoderXPO.Status.Oid == DecoderDTO.StatusDTO.ID) ? _decoderXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(DecoderDTO.StatusID);
            _decoderXPO.Class = (_decoderXPO.Class != null && _decoderXPO.Class.Oid == DecoderDTO.ClassID) ? _decoderXPO.Class : UnitOfWork.GetObjectByKey<ValueXPO>(DecoderDTO.ClassID);
            _decoderXPO.SubClass = (_decoderXPO.SubClass != null && _decoderXPO.SubClass.Oid == DecoderDTO.SubClassID) ? _decoderXPO.SubClass : UnitOfWork.GetObjectByKey<ValueXPO>(DecoderDTO.SubClassID);
            _decoderXPO.ComponentType = (_decoderXPO.ComponentType != null && _decoderXPO.ComponentType.Oid == DecoderDTO.ComponentTypeID) ? _decoderXPO.ComponentType : UnitOfWork.GetObjectByKey<ValueXPO>(DecoderDTO.ComponentTypeID);
            _decoderXPO.PartType = (_decoderXPO.PartType != null && _decoderXPO.PartType.Oid == DecoderDTO.PartTypeID) ? _decoderXPO.PartType : UnitOfWork.GetObjectByKey<ValueXPO>(DecoderDTO.PartTypeID);
            _decoderXPO.Description = _decoderXPO.Description == DecoderDTO.Description ? _decoderXPO.Description : DecoderDTO.Description;
            _decoderXPO.AddedDate = _decoderXPO.AddedDate != null ? _decoderXPO.AddedDate : DecoderDTO.AddedDate;
            _decoderXPO.AddedBy = (_decoderXPO.AddedBy != null) ? _decoderXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DecoderDTO.AddedByID);
            _decoderXPO.LastUpdate = _decoderXPO.LastUpdate == DecoderDTO.LastUpdate ? _decoderXPO.LastUpdate : DecoderDTO.LastUpdate;
            _decoderXPO.LastUpdateBy = (_decoderXPO.LastUpdateBy != null && _decoderXPO.LastUpdateBy.Oid == DecoderDTO.LastUpdateByID) ? _decoderXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DecoderDTO.LastUpdateByID);
            _decoderXPO.IsActive = _decoderXPO.IsActive == DecoderDTO.IsActive ? (bool)_decoderXPO.IsActive : (bool)DecoderDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderXPO;
    }

}
