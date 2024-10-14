using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.MailGroups.MailGroupMember
{
    public class MailGroupMemberMap
    {
        public static MailGroupMemberDTO XPOToDTO(MailGroupMemberXPO MailGroupMemberXPO)
        {
            var _mailgroupmemberDTO = new MailGroupMemberDTO();
            try
            {
                _mailgroupmemberDTO.ID = MailGroupMemberXPO.Oid;
               _mailgroupmemberDTO.Name = MailGroupMemberXPO.Name; 
               _mailgroupmemberDTO.Description = MailGroupMemberXPO.Description; 
               _mailgroupmemberDTO.MailGroupDTO.ID = (MailGroupMemberXPO.MailGroup != null) ? MailGroupMemberXPO.MailGroup.Oid : 0;
               _mailgroupmemberDTO.MailGroupDTO.Name = (MailGroupMemberXPO.MailGroup != null) ? MailGroupMemberXPO.MailGroup.Name : "Unnassigned";               
               _mailgroupmemberDTO.UserDTO.ID = (MailGroupMemberXPO.User != null) ? MailGroupMemberXPO.User.Oid : 0;
               _mailgroupmemberDTO.UserDTO.Name = (MailGroupMemberXPO.User != null) ? MailGroupMemberXPO.User.Name : "Unnassigned";              
               _mailgroupmemberDTO.AddedDate = (MailGroupMemberXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? MailGroupMemberXPO.AddedDate : (DateTime?)null; 
               _mailgroupmemberDTO.AddedByID = (MailGroupMemberXPO.AddedBy != null) ? MailGroupMemberXPO.AddedBy.Oid : 0;
               _mailgroupmemberDTO.AddedByName = (MailGroupMemberXPO.AddedBy != null) ? MailGroupMemberXPO.AddedBy.Name : "Unnassigned";
               _mailgroupmemberDTO.LastUpdate = (MailGroupMemberXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? MailGroupMemberXPO.LastUpdate : (DateTime?)null; 
               _mailgroupmemberDTO.LastUpdateByID = (MailGroupMemberXPO.LastUpdateBy != null) ? MailGroupMemberXPO.LastUpdateBy.Oid : 0;
               _mailgroupmemberDTO.LastUpdateByName = (MailGroupMemberXPO.LastUpdateBy != null) ? MailGroupMemberXPO.LastUpdateBy.Name : "Unnassigned";
               _mailgroupmemberDTO.IsActive = MailGroupMemberXPO.IsActive; 
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _mailgroupmemberDTO;
        }

        public static MailGroupMemberXPO DTOtoXPO(MailGroupMemberDTO MailGroupMemberDTO, UnitOfWork UnitOfWork)
        {
            MailGroupMemberXPO _mailgroupmemberXPO;
            try
            {
                _mailgroupmemberXPO = MailGroupMemberDTO.ID == null || MailGroupMemberDTO.ID == 0 ? new MailGroupMemberXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<MailGroupMemberXPO>(MailGroupMemberDTO.ID);
                _mailgroupmemberXPO.Name = _mailgroupmemberXPO.Name == MailGroupMemberDTO.Name ? _mailgroupmemberXPO.Name : MailGroupMemberDTO.Name;
               _mailgroupmemberXPO.Description = _mailgroupmemberXPO.Description == MailGroupMemberDTO.Description ? _mailgroupmemberXPO.Description : MailGroupMemberDTO.Description;
               _mailgroupmemberXPO.MailGroup = (_mailgroupmemberXPO.MailGroup != null && _mailgroupmemberXPO.MailGroup.Oid == MailGroupMemberDTO.MailGroupDTO.ID ) ? _mailgroupmemberXPO.MailGroup : UnitOfWork.GetObjectByKey<MailGroupXPO>(MailGroupMemberDTO.MailGroupDTO.ID);
               _mailgroupmemberXPO.User = (_mailgroupmemberXPO.User != null && _mailgroupmemberXPO.User.Oid == MailGroupMemberDTO.UserDTO.ID ) ? _mailgroupmemberXPO.User : UnitOfWork.GetObjectByKey<UserXPO>(MailGroupMemberDTO.UserDTO.ID);
               _mailgroupmemberXPO.AddedDate = _mailgroupmemberXPO.AddedDate != null ? _mailgroupmemberXPO.AddedDate : MailGroupMemberDTO.AddedDate;
               _mailgroupmemberXPO.AddedBy = (_mailgroupmemberXPO.AddedBy != null ) ? _mailgroupmemberXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(MailGroupMemberDTO.AddedByID);
               _mailgroupmemberXPO.LastUpdate = _mailgroupmemberXPO.LastUpdate == MailGroupMemberDTO.LastUpdate ? _mailgroupmemberXPO.LastUpdate : MailGroupMemberDTO.LastUpdate;
               _mailgroupmemberXPO.LastUpdateBy = (_mailgroupmemberXPO.LastUpdateBy != null && _mailgroupmemberXPO.LastUpdateBy.Oid == MailGroupMemberDTO.LastUpdateByID ) ? _mailgroupmemberXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(MailGroupMemberDTO.LastUpdateByID);
               _mailgroupmemberXPO.IsActive = _mailgroupmemberXPO.IsActive == MailGroupMemberDTO.IsActive ? (bool)_mailgroupmemberXPO.IsActive : (bool)MailGroupMemberDTO.IsActive;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _mailgroupmemberXPO;
        }

    }
}
