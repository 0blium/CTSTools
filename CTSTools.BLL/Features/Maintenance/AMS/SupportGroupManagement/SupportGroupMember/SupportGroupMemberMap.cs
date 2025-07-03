using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Xpo;
using System;

namespace AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember
{
    public class SupportGroupMemberMap
    {
        public static SupportGroupMemberDTO XPOToDTO(SupportGroupMemberXPO SupportGroupMemberXPO)
        {
            var _supportgroupmemberDTO = new SupportGroupMemberDTO();
            try
            {
                _supportgroupmemberDTO.ID = SupportGroupMemberXPO.Oid;
               _supportgroupmemberDTO.SupportGroupDTO.ID = (SupportGroupMemberXPO.SupportGroup != null) ? SupportGroupMemberXPO.SupportGroup.Oid : 0;
               _supportgroupmemberDTO.SupportGroupDTO.Name = (SupportGroupMemberXPO.SupportGroup != null) ? SupportGroupMemberXPO.SupportGroup.Name : "Unnassigned";
               _supportgroupmemberDTO.UserDTO.ID = (SupportGroupMemberXPO.User != null) ? SupportGroupMemberXPO.User.Oid : 0;
               _supportgroupmemberDTO.UserDTO.Name = (SupportGroupMemberXPO.User != null) ? SupportGroupMemberXPO.User.Name : "Unnassigned";
               _supportgroupmemberDTO.RoleDTO.ID = (SupportGroupMemberXPO.Role != null) ? SupportGroupMemberXPO.Role.Oid : 0;
               _supportgroupmemberDTO.RoleDTO.Name = (SupportGroupMemberXPO.Role != null) ? SupportGroupMemberXPO.Role.Name : "Unnassigned";
               _supportgroupmemberDTO.AddedDate = (SupportGroupMemberXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SupportGroupMemberXPO.AddedDate : (DateTime?)null; 
               _supportgroupmemberDTO.AddedByID = (SupportGroupMemberXPO.AddedBy != null) ? SupportGroupMemberXPO.AddedBy.Oid : 0;
               _supportgroupmemberDTO.AddedByName = (SupportGroupMemberXPO.AddedBy != null) ? SupportGroupMemberXPO.AddedBy.Name : "Unnassigned";
               _supportgroupmemberDTO.LastUpdate = (SupportGroupMemberXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SupportGroupMemberXPO.LastUpdate : (DateTime?)null; 
               _supportgroupmemberDTO.LastUpdateByID = (SupportGroupMemberXPO.LastUpdateBy != null) ? SupportGroupMemberXPO.LastUpdateBy.Oid : 0;
               _supportgroupmemberDTO.LastUpdateByName = (SupportGroupMemberXPO.LastUpdateBy != null) ? SupportGroupMemberXPO.LastUpdateBy.Name : "Unnassigned";
               _supportgroupmemberDTO.IsActive = SupportGroupMemberXPO.IsActive; 
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _supportgroupmemberDTO;
        }

        public static SupportGroupMemberXPO DTOtoXPO(SupportGroupMemberDTO SupportGroupMemberDTO, UnitOfWork UnitOfWork)
        {
            SupportGroupMemberXPO _supportgroupmemberXPO;
            try
            {
                _supportgroupmemberXPO = SupportGroupMemberDTO.ID == null || SupportGroupMemberDTO.ID == 0 ? new SupportGroupMemberXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SupportGroupMemberXPO>(SupportGroupMemberDTO.ID);
                _supportgroupmemberXPO.SupportGroup = (_supportgroupmemberXPO.SupportGroup != null && _supportgroupmemberXPO.SupportGroup.Oid == SupportGroupMemberDTO.SupportGroupDTO.ID ) ? _supportgroupmemberXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(SupportGroupMemberDTO.SupportGroupDTO.ID);
               _supportgroupmemberXPO.User = (_supportgroupmemberXPO.User != null && _supportgroupmemberXPO.User.Oid == SupportGroupMemberDTO.UserDTO.ID ) ? _supportgroupmemberXPO.User : UnitOfWork.GetObjectByKey<UserXPO>(SupportGroupMemberDTO.UserDTO.ID);
               _supportgroupmemberXPO.Role = (_supportgroupmemberXPO.Role != null && _supportgroupmemberXPO.Role.Oid == SupportGroupMemberDTO.RoleDTO.ID ) ? _supportgroupmemberXPO.Role : UnitOfWork.GetObjectByKey<RoleXPO>(SupportGroupMemberDTO.RoleDTO.ID);
               _supportgroupmemberXPO.AddedDate = _supportgroupmemberXPO.AddedDate != null ? _supportgroupmemberXPO.AddedDate : SupportGroupMemberDTO.AddedDate;
               _supportgroupmemberXPO.AddedBy = (_supportgroupmemberXPO.AddedBy != null ) ? _supportgroupmemberXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(SupportGroupMemberDTO.AddedByID);
               _supportgroupmemberXPO.LastUpdate = _supportgroupmemberXPO.LastUpdate == SupportGroupMemberDTO.LastUpdate ? _supportgroupmemberXPO.LastUpdate : SupportGroupMemberDTO.LastUpdate;
               _supportgroupmemberXPO.LastUpdateBy = (_supportgroupmemberXPO.LastUpdateBy != null && _supportgroupmemberXPO.LastUpdateBy.Oid == SupportGroupMemberDTO.LastUpdateByID ) ? _supportgroupmemberXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SupportGroupMemberDTO.LastUpdateByID);
               _supportgroupmemberXPO.IsActive = _supportgroupmemberXPO.IsActive == SupportGroupMemberDTO.IsActive ? (bool)_supportgroupmemberXPO.IsActive : (bool)SupportGroupMemberDTO.IsActive;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _supportgroupmemberXPO;
        }

    }
}
