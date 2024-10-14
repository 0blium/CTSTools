using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Security.Role;
using CTSTools.DAL.Features.User;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Security.Roles.RoleType
{
    public class RoleTypeMap
    {
        public static RoleTypeDTO XPOToDTO(RoleTypeXPO RoleTypeXPO)
        {
            var _roletypeDTO = new RoleTypeDTO();
            try
            {
                _roletypeDTO.ID = RoleTypeXPO.Oid;
               _roletypeDTO.Name = RoleTypeXPO.Name; 
               _roletypeDTO.Description = RoleTypeXPO.Description; 
               _roletypeDTO.AddedDate = (RoleTypeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? RoleTypeXPO.AddedDate : (DateTime?)null; 
               _roletypeDTO.AddedByID = (RoleTypeXPO.AddedBy != null) ? RoleTypeXPO.AddedBy.Oid : 0;
               _roletypeDTO.AddedByName = (RoleTypeXPO.AddedBy != null) ? RoleTypeXPO.AddedBy.Name : "Unnassigned";
               _roletypeDTO.LastUpdate = (RoleTypeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? RoleTypeXPO.LastUpdate : (DateTime?)null; 
               _roletypeDTO.LastUpdateByID = (RoleTypeXPO.LastUpdateBy != null) ? RoleTypeXPO.LastUpdateBy.Oid : 0;
               _roletypeDTO.LastUpdateByName = (RoleTypeXPO.LastUpdateBy != null) ? RoleTypeXPO.LastUpdateBy.Name : "Unnassigned";
               _roletypeDTO.IsActive = RoleTypeXPO.IsActive; 
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _roletypeDTO;
        }

        public static RoleTypeXPO DTOtoXPO(RoleTypeDTO RoleTypeDTO, UnitOfWork UnitOfWork)
        {
            RoleTypeXPO _roletypeXPO;
            try
            {
                _roletypeXPO = RoleTypeDTO.ID == null || RoleTypeDTO.ID == 0 ? new RoleTypeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<RoleTypeXPO>(RoleTypeDTO.ID);
                _roletypeXPO.Name = _roletypeXPO.Name == RoleTypeDTO.Name ? _roletypeXPO.Name : RoleTypeDTO.Name;
               _roletypeXPO.Description = _roletypeXPO.Description == RoleTypeDTO.Description ? _roletypeXPO.Description : RoleTypeDTO.Description;
               _roletypeXPO.AddedDate = _roletypeXPO.AddedDate != null ? _roletypeXPO.AddedDate : RoleTypeDTO.AddedDate;
               _roletypeXPO.AddedBy = (_roletypeXPO.AddedBy != null  ) ? _roletypeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(RoleTypeDTO.AddedByID);
               _roletypeXPO.LastUpdate = _roletypeXPO.LastUpdate == RoleTypeDTO.LastUpdate ? _roletypeXPO.LastUpdate : RoleTypeDTO.LastUpdate;
               _roletypeXPO.LastUpdateBy = (_roletypeXPO.LastUpdateBy != null && _roletypeXPO.LastUpdateBy.Oid == RoleTypeDTO.LastUpdateByID ) ? _roletypeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(RoleTypeDTO.LastUpdateByID);
               _roletypeXPO.IsActive = _roletypeXPO.IsActive == RoleTypeDTO.IsActive ? (bool)_roletypeXPO.IsActive : (bool)RoleTypeDTO.IsActive;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _roletypeXPO;
        }

    }
}
