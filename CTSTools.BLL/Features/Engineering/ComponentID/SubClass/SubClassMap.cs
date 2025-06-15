using CTSTools.BLL.Features.Engineering.ComponentID.SubClass;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SubClass;
using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SubClass
{
    public class SubClassMap
    {
        public static SubClassDTO XPOToDTO(SubClassXPO SubClassXPO)
        {
            var _SubClassDTO = new SubClassDTO();
            try
            {
                _SubClassDTO.ID = SubClassXPO.Oid;
                _SubClassDTO.Name = SubClassXPO.Name;
                _SubClassDTO.Code = SubClassXPO.Code;
                _SubClassDTO.Description = SubClassXPO.Description;
                _SubClassDTO.AttributeID = (SubClassXPO.Attribute != null) ? SubClassXPO.Attribute.Oid : 0;
                _SubClassDTO.AttributeName = (SubClassXPO.Attribute != null) ? SubClassXPO.Attribute.Name : "Unnassigned";
                _SubClassDTO.ValueID = (SubClassXPO.Value != null) ? SubClassXPO.Value.Oid : 0;
                _SubClassDTO.ValueName = (SubClassXPO.Value != null) ? SubClassXPO.Value.Name : "Unnassigned";
                _SubClassDTO.ClassID = (SubClassXPO.Class != null) ? SubClassXPO.Class.Oid : 0;
                _SubClassDTO.ClassName = (SubClassXPO.Class != null) ? SubClassXPO.Class.Name : "Unnassigned";
                _SubClassDTO.AddedDate = (SubClassXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SubClassXPO.AddedDate : (DateTime?)null;
                _SubClassDTO.AddedByID = (SubClassXPO.AddedBy != null) ? SubClassXPO.AddedBy.Oid : 0;
                _SubClassDTO.AddedByName = (SubClassXPO.AddedBy != null) ? SubClassXPO.AddedBy.Name : "Unnassigned";
                _SubClassDTO.LastUpdate = (SubClassXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SubClassXPO.LastUpdate : (DateTime?)null;
                _SubClassDTO.LastUpdateByID = (SubClassXPO.LastUpdateBy != null) ? SubClassXPO.LastUpdateBy.Oid : 0;
                _SubClassDTO.LastUpdateByName = (SubClassXPO.LastUpdateBy != null) ? SubClassXPO.LastUpdateBy.Name : "Unnassigned";
                _SubClassDTO.IsActive = SubClassXPO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _SubClassDTO;
        }

        public static SubClassXPO DTOtoXPO(SubClassDTO SubClassDTO, UnitOfWork UnitOfWork)
        {
            SubClassXPO _SubClassXPO;
            try
            {
                _SubClassXPO = SubClassDTO.ID == null || SubClassDTO.ID == 0 ? new SubClassXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SubClassXPO>(SubClassDTO.ID);
                _SubClassXPO.Name = _SubClassXPO.Name == SubClassDTO.Name ? _SubClassXPO.Name : SubClassDTO.Name;
                _SubClassXPO.Code = _SubClassXPO.Code == SubClassDTO.Code ? _SubClassXPO.Code : SubClassDTO.Code;
                _SubClassXPO.Description = _SubClassXPO.Description == SubClassDTO.Description ? _SubClassXPO.Description : SubClassDTO.Description;
                _SubClassXPO.Attribute = (_SubClassXPO.Attribute != null && _SubClassXPO.Attribute.Oid == SubClassDTO.AttributeID) ? _SubClassXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(SubClassDTO.AttributeID);
                _SubClassXPO.Value = (_SubClassXPO.Value != null && _SubClassXPO.Value.Oid == SubClassDTO.ValueID) ? _SubClassXPO.Value : UnitOfWork.GetObjectByKey<ValueXPO>(SubClassDTO.ValueID);
                _SubClassXPO.Class = (_SubClassXPO.Class != null && _SubClassXPO.Class.Oid == SubClassDTO.ClassID) ? _SubClassXPO.Class : UnitOfWork.GetObjectByKey<ClassXPO>(SubClassDTO.ClassID);
                _SubClassXPO.AddedDate = _SubClassXPO.AddedDate != null ? _SubClassXPO.AddedDate : SubClassDTO.AddedDate;
                _SubClassXPO.AddedBy = (_SubClassXPO.AddedBy != null) ? _SubClassXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(SubClassDTO.AddedByID);
                _SubClassXPO.LastUpdate = _SubClassXPO.LastUpdate == SubClassDTO.LastUpdate ? _SubClassXPO.LastUpdate : SubClassDTO.LastUpdate;
                _SubClassXPO.LastUpdateBy = (_SubClassXPO.LastUpdateBy != null && _SubClassXPO.LastUpdateBy.Oid == SubClassDTO.LastUpdateByID) ? _SubClassXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SubClassDTO.LastUpdateByID);
                _SubClassXPO.IsActive = _SubClassXPO.IsActive == SubClassDTO.IsActive ? (bool)_SubClassXPO.IsActive : (bool)SubClassDTO.IsActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _SubClassXPO;
        }
    }
}
