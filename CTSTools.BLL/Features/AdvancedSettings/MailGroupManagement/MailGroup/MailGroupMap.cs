using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;

public class MailGroupMap
{
    public static MailGroupDTO XPOToDTO(MailGroupXPO MailGroupXPO)
    {
        var _mailgroupDTO = new MailGroupDTO();
        try
        {
            _mailgroupDTO.ID = MailGroupXPO.Oid;
           _mailgroupDTO.Name = MailGroupXPO.Name; 
           _mailgroupDTO.Description = MailGroupXPO.Description; 
           _mailgroupDTO.AddedDate = (MailGroupXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? MailGroupXPO.AddedDate : (DateTime?)null; 
           _mailgroupDTO.AddedByID = (MailGroupXPO.AddedBy != null) ? MailGroupXPO.AddedBy.Oid : 0;
           _mailgroupDTO.AddedByName = (MailGroupXPO.AddedBy != null) ? MailGroupXPO.AddedBy.Name : "Unnassigned";
           _mailgroupDTO.LastUpdate = (MailGroupXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? MailGroupXPO.LastUpdate : (DateTime?)null; 
           _mailgroupDTO.LastUpdateByID = (MailGroupXPO.LastUpdateBy != null) ? MailGroupXPO.LastUpdateBy.Oid : 0;
           _mailgroupDTO.LastUpdateByName = (MailGroupXPO.LastUpdateBy != null) ? MailGroupXPO.LastUpdateBy.Name : "Unnassigned";
           _mailgroupDTO.IsActive = MailGroupXPO.IsActive; 
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _mailgroupDTO;
    }

    public static MailGroupXPO DTOtoXPO(MailGroupDTO MailGroupDTO, UnitOfWork UnitOfWork)
    {
        MailGroupXPO _mailgroupXPO;
        try
        {
            _mailgroupXPO = MailGroupDTO.ID == null || MailGroupDTO.ID == 0 ? new MailGroupXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<MailGroupXPO>(MailGroupDTO.ID);
            _mailgroupXPO.Name = _mailgroupXPO.Name == MailGroupDTO.Name ? _mailgroupXPO.Name : MailGroupDTO.Name;
           _mailgroupXPO.Description = _mailgroupXPO.Description == MailGroupDTO.Description ? _mailgroupXPO.Description : MailGroupDTO.Description;
           _mailgroupXPO.AddedDate = _mailgroupXPO.AddedDate != null ? _mailgroupXPO.AddedDate : MailGroupDTO.AddedDate;
           _mailgroupXPO.AddedBy = (_mailgroupXPO.AddedBy != null  ) ? _mailgroupXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(MailGroupDTO.AddedByID);
           _mailgroupXPO.LastUpdate = _mailgroupXPO.LastUpdate == MailGroupDTO.LastUpdate ? _mailgroupXPO.LastUpdate : MailGroupDTO.LastUpdate;
           _mailgroupXPO.LastUpdateBy = (_mailgroupXPO.LastUpdateBy != null && _mailgroupXPO.LastUpdateBy.Oid == MailGroupDTO.LastUpdateByID ) ? _mailgroupXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(MailGroupDTO.LastUpdateByID);
           _mailgroupXPO.IsActive = _mailgroupXPO.IsActive == MailGroupDTO.IsActive ? (bool)_mailgroupXPO.IsActive : (bool)MailGroupDTO.IsActive;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _mailgroupXPO;
    }

}
