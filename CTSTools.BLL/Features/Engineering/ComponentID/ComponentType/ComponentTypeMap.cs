using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.ComponentType;
using CTSTools.DAL.Features.Engineering.ComponentID.PartType;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.ComponentType
{
    public class ComponentTypeMap
    {
        public static ComponentTypeDTO XPOToDTO(ComponentTypeXPO ComponentTypeXPO)
        {
            var _componentTypeDTO = new ComponentTypeDTO();
            try
            {
                _componentTypeDTO.ID = ComponentTypeXPO.Oid;
                _componentTypeDTO.Name = ComponentTypeXPO.Name;
                _componentTypeDTO.Code = ComponentTypeXPO.Code;
                _componentTypeDTO.Description = ComponentTypeXPO.Description;
                _componentTypeDTO.AttributeID = (ComponentTypeXPO.Attribute != null) ? ComponentTypeXPO.Attribute.Oid : 0;
                _componentTypeDTO.AttributeName = (ComponentTypeXPO.Attribute != null) ? ComponentTypeXPO.Attribute.Name : "Unnassigned";
                _componentTypeDTO.ValueID = (ComponentTypeXPO.Value != null) ? ComponentTypeXPO.Value.Oid : 0;
                _componentTypeDTO.ValueName = (ComponentTypeXPO.Value != null) ? ComponentTypeXPO.Value.Name : "Unnassigned";
                _componentTypeDTO.PartTypeID = (ComponentTypeXPO.PartType != null) ? ComponentTypeXPO.PartType.Oid : 0;
                _componentTypeDTO.PartTypeName = (ComponentTypeXPO.PartType != null) ? ComponentTypeXPO.PartType.Name : "Unnassigned";
                _componentTypeDTO.AddedDate = (ComponentTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ComponentTypeXPO.AddedDate : (DateTime?)null;
                _componentTypeDTO.AddedByID = (ComponentTypeXPO.AddedBy != null) ? ComponentTypeXPO.AddedBy.Oid : 0;
                _componentTypeDTO.AddedByName = (ComponentTypeXPO.AddedBy != null) ? ComponentTypeXPO.AddedBy.Name : "Unnassigned";
                _componentTypeDTO.LastUpdate = (ComponentTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ComponentTypeXPO.LastUpdate : (DateTime?)null;
                _componentTypeDTO.LastUpdateByID = (ComponentTypeXPO.LastUpdateBy != null) ? ComponentTypeXPO.LastUpdateBy.Oid : 0;
                _componentTypeDTO.LastUpdateByName = (ComponentTypeXPO.LastUpdateBy != null) ? ComponentTypeXPO.LastUpdateBy.Name : "Unnassigned";
                _componentTypeDTO.IsActive = ComponentTypeXPO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _componentTypeDTO;
        }

        public static ComponentTypeXPO DTOtoXPO(ComponentTypeDTO ComponentTypeDTO, UnitOfWork UnitOfWork)
        {
            ComponentTypeXPO _componentTypeXPO;
            try
            {
                _componentTypeXPO = ComponentTypeDTO.ID == null || ComponentTypeDTO.ID == 0 ? new ComponentTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ComponentTypeXPO>(ComponentTypeDTO.ID);
                _componentTypeXPO.Name = _componentTypeXPO.Name == ComponentTypeDTO.Name ? _componentTypeXPO.Name : ComponentTypeDTO.Name;
                _componentTypeXPO.Code = _componentTypeXPO.Code == ComponentTypeDTO.Code ? _componentTypeXPO.Code : ComponentTypeDTO.Code;
                _componentTypeXPO.Description = _componentTypeXPO.Description == ComponentTypeDTO.Description ? _componentTypeXPO.Description : ComponentTypeDTO.Description;
                _componentTypeXPO.Attribute = (_componentTypeXPO.Attribute != null && _componentTypeXPO.Attribute.Oid == ComponentTypeDTO.AttributeID) ? _componentTypeXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(ComponentTypeDTO.AttributeID);
                _componentTypeXPO.Value = (_componentTypeXPO.Value != null && _componentTypeXPO.Value.Oid == ComponentTypeDTO.ValueID) ? _componentTypeXPO.Value : UnitOfWork.GetObjectByKey<ValueXPO>(ComponentTypeDTO.ValueID);
                _componentTypeXPO.PartType = (_componentTypeXPO.PartType != null && _componentTypeXPO.PartType.Oid == ComponentTypeDTO.PartTypeID) ? _componentTypeXPO.PartType : UnitOfWork.GetObjectByKey<PartTypeXPO>(ComponentTypeDTO.PartTypeID);
                _componentTypeXPO.AddedDate = _componentTypeXPO.AddedDate != null ? _componentTypeXPO.AddedDate : ComponentTypeDTO.AddedDate;
                _componentTypeXPO.AddedBy = (_componentTypeXPO.AddedBy != null) ? _componentTypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ComponentTypeDTO.AddedByID);
                _componentTypeXPO.LastUpdate = _componentTypeXPO.LastUpdate == ComponentTypeDTO.LastUpdate ? _componentTypeXPO.LastUpdate : ComponentTypeDTO.LastUpdate;
                _componentTypeXPO.LastUpdateBy = (_componentTypeXPO.LastUpdateBy != null && _componentTypeXPO.LastUpdateBy.Oid == ComponentTypeDTO.LastUpdateByID) ? _componentTypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ComponentTypeDTO.LastUpdateByID);
                _componentTypeXPO.IsActive = _componentTypeXPO.IsActive == ComponentTypeDTO.IsActive ? (bool)_componentTypeXPO.IsActive : (bool)ComponentTypeDTO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _componentTypeXPO;
        }
    }
}
