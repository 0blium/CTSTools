using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.PartType;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartType
{
    public class PartTypeMap
    {
        public static PartTypeDTO XPOToDTO(PartTypeXPO PartTypeXPO)
        {
            var _partTypeDTO = new PartTypeDTO();
            try
            {
                _partTypeDTO.ID = PartTypeXPO.Oid;
                _partTypeDTO.Name = PartTypeXPO.Name;
                _partTypeDTO.Code = PartTypeXPO.Code;
                _partTypeDTO.AttributeID = (PartTypeXPO.Attribute != null) ? PartTypeXPO.Attribute.Oid : 0;
                _partTypeDTO.AttributeName = (PartTypeXPO.Attribute != null) ? PartTypeXPO.Attribute.Name : "Unnassigned";
                _partTypeDTO.ValueID = (PartTypeXPO.Value != null) ? PartTypeXPO.Value.Oid : 0;
                _partTypeDTO.ValueName = (PartTypeXPO.Value != null) ? PartTypeXPO.Value.Name : "Unnassigned";
                _partTypeDTO.Description = PartTypeXPO.Description;
                _partTypeDTO.AddedDate = (PartTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? PartTypeXPO.AddedDate : (DateTime?)null;
                _partTypeDTO.AddedByID = (PartTypeXPO.AddedBy != null) ? PartTypeXPO.AddedBy.Oid : 0;
                _partTypeDTO.AddedByName = (PartTypeXPO.AddedBy != null) ? PartTypeXPO.AddedBy.Name : "Unnassigned";
                _partTypeDTO.LastUpdate = (PartTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? PartTypeXPO.LastUpdate : (DateTime?)null;
                _partTypeDTO.LastUpdateByID = (PartTypeXPO.LastUpdateBy != null) ? PartTypeXPO.LastUpdateBy.Oid : 0;
                _partTypeDTO.LastUpdateByName = (PartTypeXPO.LastUpdateBy != null) ? PartTypeXPO.LastUpdateBy.Name : "Unnassigned";
                _partTypeDTO.IsActive = PartTypeXPO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _partTypeDTO;
        }

        public static PartTypeXPO DTOtoXPO(PartTypeDTO PartTypeDTO, UnitOfWork UnitOfWork)
        {
            PartTypeXPO _partTypeXPO;
            try
            {
                _partTypeXPO = PartTypeDTO.ID == null || PartTypeDTO.ID == 0 ? new PartTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<PartTypeXPO>(PartTypeDTO.ID);
                _partTypeXPO.Name = _partTypeXPO.Name == PartTypeDTO.Name ? _partTypeXPO.Name : PartTypeDTO.Name;
                _partTypeXPO.Code = _partTypeXPO.Code == PartTypeDTO.Code ? _partTypeXPO.Code : PartTypeDTO.Code;
                _partTypeXPO.Attribute = (_partTypeXPO.Attribute != null && _partTypeXPO.Attribute.Oid == PartTypeDTO.AttributeID) ? _partTypeXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(PartTypeDTO.AttributeID);
                _partTypeXPO.Value = (_partTypeXPO.Value != null && _partTypeXPO.Value.Oid == PartTypeDTO.ValueID) ? _partTypeXPO.Value : UnitOfWork.GetObjectByKey<ValueXPO>(PartTypeDTO.ValueID);
                _partTypeXPO.Description = _partTypeXPO.Description == PartTypeDTO.Description ? _partTypeXPO.Description : PartTypeDTO.Description;
                _partTypeXPO.AddedDate = _partTypeXPO.AddedDate != null ? _partTypeXPO.AddedDate : PartTypeDTO.AddedDate;
                _partTypeXPO.AddedBy = (_partTypeXPO.AddedBy != null) ? _partTypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(PartTypeDTO.AddedByID);
                _partTypeXPO.LastUpdate = _partTypeXPO.LastUpdate == PartTypeDTO.LastUpdate ? _partTypeXPO.LastUpdate : PartTypeDTO.LastUpdate;
                _partTypeXPO.LastUpdateBy = (_partTypeXPO.LastUpdateBy != null && _partTypeXPO.LastUpdateBy.Oid == PartTypeDTO.LastUpdateByID) ? _partTypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(PartTypeDTO.LastUpdateByID);
                _partTypeXPO.IsActive = _partTypeXPO.IsActive == PartTypeDTO.IsActive ? (bool)_partTypeXPO.IsActive : (bool)PartTypeDTO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _partTypeXPO;
        }
    }
}
