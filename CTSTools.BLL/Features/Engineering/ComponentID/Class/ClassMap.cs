using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using CTSTools.DAL.Features.Engineering.ComponentID.ComponentType;
using CTSTools.DAL.Features.Engineering.ComponentID.PartType;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Engineering.ComponentID.Class
{
    public class ClassMap
    {
        public static ClassDTO XPOToDTO(ClassXPO ClassXPO)
        {
            var _classDTO = new ClassDTO();
            try
            {
                _classDTO.ID = ClassXPO.Oid;
                _classDTO.AttributeID = (ClassXPO.Attribute != null) ? ClassXPO.Attribute.Oid : 0;
                _classDTO.AttributeName = (ClassXPO.Attribute != null) ? ClassXPO.Attribute.Name : "Unnassigned";
                _classDTO.ValueID = (ClassXPO.Value != null) ? ClassXPO.Value.Oid : 0;
                _classDTO.ValueName = (ClassXPO.Value != null) ? ClassXPO.Value.Name : "Unnassigned";
                _classDTO.ValueLinkID = (ClassXPO.ValueLink != null) ? ClassXPO.ValueLink.Oid : 0;
                _classDTO.PartTypeID = (ClassXPO.PartType != null) ? ClassXPO.PartType.Oid : 0;
                _classDTO.PartTypeName = (ClassXPO.PartType != null) ? ClassXPO.PartType.Name : "Unnassigned";
                _classDTO.ComponentTypeID = (ClassXPO.ComponentType != null) ? ClassXPO.ComponentType.Oid : 0;
                _classDTO.ComponentTypeName = (ClassXPO.ComponentType != null) ? ClassXPO.ComponentType.Name : "Unnassigned";
                _classDTO.Name = ClassXPO.Name;
                _classDTO.Code = ClassXPO.Code;
                _classDTO.Description = ClassXPO.Description;
                _classDTO.AddedDate = (ClassXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ClassXPO.AddedDate : (DateTime?)null;
                _classDTO.AddedByID = (ClassXPO.AddedBy != null) ? ClassXPO.AddedBy.Oid : 0;
                _classDTO.AddedByName = (ClassXPO.AddedBy != null) ? ClassXPO.AddedBy.Name : "Unnassigned";
                _classDTO.LastUpdate = (ClassXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ClassXPO.LastUpdate : (DateTime?)null;
                _classDTO.LastUpdateByID = (ClassXPO.LastUpdateBy != null) ? ClassXPO.LastUpdateBy.Oid : 0;
                _classDTO.LastUpdateByName = (ClassXPO.LastUpdateBy != null) ? ClassXPO.LastUpdateBy.Name : "Unnassigned";
                _classDTO.IsActive = ClassXPO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _classDTO;
        }

        public static ClassXPO DTOtoXPO(ClassDTO ClassDTO, UnitOfWork UnitOfWork)
        {
            ClassXPO _classXPO;
            try
            {
                _classXPO = ClassDTO.ID == null || ClassDTO.ID == 0 ? new ClassXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ClassXPO>(ClassDTO.ID);
                _classXPO.Attribute = (_classXPO.Attribute != null && _classXPO.Attribute.Oid == ClassDTO.AttributeID) ? _classXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(ClassDTO.AttributeID);
                _classXPO.Value = (_classXPO.Value != null && _classXPO.Value.Oid == ClassDTO.ValueID) ? _classXPO.Value : UnitOfWork.GetObjectByKey<ValueXPO>(ClassDTO.ValueID);
                _classXPO.ValueLink = (_classXPO.ValueLink != null && _classXPO.ValueLink.Oid == ClassDTO.ValueLinkID) ? _classXPO.ValueLink : UnitOfWork.GetObjectByKey<ValueLinkXPO>(ClassDTO.ValueLinkID);
                _classXPO.PartType = (_classXPO.PartType != null && _classXPO.PartType.Oid == ClassDTO.PartTypeID) ? _classXPO.PartType : UnitOfWork.GetObjectByKey<PartTypeXPO>(ClassDTO.PartTypeID);
                _classXPO.ComponentType = (_classXPO.ComponentType != null && _classXPO.ComponentType.Oid == ClassDTO.ComponentTypeID) ? _classXPO.ComponentType : UnitOfWork.GetObjectByKey<ComponentTypeXPO>(ClassDTO.ComponentTypeID);
                _classXPO.Name = _classXPO.Name == ClassDTO.Name ? _classXPO.Name : ClassDTO.Name;
                _classXPO.Code = _classXPO.Code == ClassDTO.Code ? _classXPO.Code : ClassDTO.Code;
                _classXPO.Description = _classXPO.Description == ClassDTO.Description ? _classXPO.Description : ClassDTO.Description;
                _classXPO.AddedDate = _classXPO.AddedDate != null ? _classXPO.AddedDate : ClassDTO.AddedDate;
                _classXPO.AddedBy = (_classXPO.AddedBy != null) ? _classXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ClassDTO.AddedByID);
                _classXPO.LastUpdate = _classXPO.LastUpdate == ClassDTO.LastUpdate ? _classXPO.LastUpdate : ClassDTO.LastUpdate;
                _classXPO.LastUpdateBy = (_classXPO.LastUpdateBy != null && _classXPO.LastUpdateBy.Oid == ClassDTO.LastUpdateByID) ? _classXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ClassDTO.LastUpdateByID);
                _classXPO.IsActive = _classXPO.IsActive == ClassDTO.IsActive ? (bool)_classXPO.IsActive : (bool)ClassDTO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _classXPO;
        }
    }
}
