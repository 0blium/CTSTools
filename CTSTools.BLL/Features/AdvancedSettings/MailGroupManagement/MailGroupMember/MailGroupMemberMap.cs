using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

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
               _mailgroupmemberDTO.MailGroupID = (MailGroupMemberXPO.MailGroup != null) ? MailGroupMemberXPO.MailGroup.Oid : 0;
               _mailgroupmemberDTO.MailGroupName = (MailGroupMemberXPO.MailGroup != null) ? MailGroupMemberXPO.MailGroup.Name : "Unnassigned";               
               _mailgroupmemberDTO.UserID = (MailGroupMemberXPO.User != null) ? MailGroupMemberXPO.User.Oid : 0;
               _mailgroupmemberDTO.UserName = (MailGroupMemberXPO.User != null) ? MailGroupMemberXPO.User.Name : "Unnassigned";              
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
               _mailgroupmemberXPO.MailGroup = (_mailgroupmemberXPO.MailGroup != null && _mailgroupmemberXPO.MailGroup.Oid == MailGroupMemberDTO.MailGroupID ) ? _mailgroupmemberXPO.MailGroup : UnitOfWork.GetObjectByKey<MailGroupXPO>(MailGroupMemberDTO.MailGroupID);
               _mailgroupmemberXPO.User = (_mailgroupmemberXPO.User != null && _mailgroupmemberXPO.User.Oid == MailGroupMemberDTO.UserID ) ? _mailgroupmemberXPO.User : UnitOfWork.GetObjectByKey<UserXPO>(MailGroupMemberDTO.UserID);
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


        public static List<MailGroupMemberXPO> DTOListToXPOList(List<MailGroupMemberDTO> MailGroupMemberDTOList, UnitOfWork UnitOfWork)
        {
            var _xPOList = new List<MailGroupMemberXPO>();
            try
            {
                foreach (var _mailGroupMemberDTO in MailGroupMemberDTOList)
                {
                    _xPOList.Add(DTOtoXPO(_mailGroupMemberDTO, UnitOfWork));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _xPOList;
        }

    }
}
