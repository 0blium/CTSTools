using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplateMap
{
    public static UserDefinedTemplateDTO XPOToDTO(UserDefinedTemplateXPO UserDefinedTemplateXPO)
    {
        var _userdefinedtemplateDTO = new UserDefinedTemplateDTO();
        try
        {
            _userdefinedtemplateDTO.ID = UserDefinedTemplateXPO.Oid;
            _userdefinedtemplateDTO.Item_SupportGroupID = (UserDefinedTemplateXPO.Item_SupportGroup != null) ? UserDefinedTemplateXPO.Item_SupportGroup.Oid : 0;
            _userdefinedtemplateDTO.UserDefinedID = (UserDefinedTemplateXPO.UserDefined != null) ? UserDefinedTemplateXPO.UserDefined.Oid : 0;
            _userdefinedtemplateDTO.UserDefinedName = (UserDefinedTemplateXPO.UserDefined != null) ? UserDefinedTemplateXPO.UserDefined.Name : "Unnassigned";
            _userdefinedtemplateDTO.AddedDate = (UserDefinedTemplateXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? UserDefinedTemplateXPO.AddedDate : (DateTime?)null;
            _userdefinedtemplateDTO.AddedByID = (UserDefinedTemplateXPO.AddedBy != null) ? UserDefinedTemplateXPO.AddedBy.Oid : 0;
            _userdefinedtemplateDTO.AddedByName = (UserDefinedTemplateXPO.AddedBy != null) ? UserDefinedTemplateXPO.AddedBy.Name : "Unnassigned";
            _userdefinedtemplateDTO.LastUpdate = (UserDefinedTemplateXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? UserDefinedTemplateXPO.LastUpdate : (DateTime?)null;
            _userdefinedtemplateDTO.LastUpdateByID = (UserDefinedTemplateXPO.LastUpdateBy != null) ? UserDefinedTemplateXPO.LastUpdateBy.Oid : 0;
            _userdefinedtemplateDTO.LastUpdateByName = (UserDefinedTemplateXPO.LastUpdateBy != null) ? UserDefinedTemplateXPO.LastUpdateBy.Name : "Unnassigned";
            _userdefinedtemplateDTO.IsActive = UserDefinedTemplateXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _userdefinedtemplateDTO;
    }

    public static UserDefinedTemplateXPO DTOtoXPO(UserDefinedTemplateDTO UserDefinedTemplateDTO, UnitOfWork UnitOfWork)
    {
        UserDefinedTemplateXPO _userdefinedtemplateXPO;
        try
        {
            _userdefinedtemplateXPO = UserDefinedTemplateDTO.ID == null || UserDefinedTemplateDTO.ID == 0 ? new UserDefinedTemplateXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<UserDefinedTemplateXPO>(UserDefinedTemplateDTO.ID);
            _userdefinedtemplateXPO.Item_SupportGroup = (_userdefinedtemplateXPO.Item_SupportGroup?.Oid == UserDefinedTemplateDTO.Item_SupportGroupDTO.ID) ? _userdefinedtemplateXPO.Item_SupportGroup : UnitOfWork.GetObjectByKey<Item_SupportGroupXPO>(UserDefinedTemplateDTO.Item_SupportGroupDTO.ID);
            _userdefinedtemplateXPO.UserDefined = (_userdefinedtemplateXPO.UserDefined != null && _userdefinedtemplateXPO.UserDefined.Oid == UserDefinedTemplateDTO.UserDefinedDTO.ID) ? _userdefinedtemplateXPO.UserDefined : UnitOfWork.GetObjectByKey<UserDefinedXPO>(UserDefinedTemplateDTO.UserDefinedDTO.ID);
            _userdefinedtemplateXPO.AddedDate = _userdefinedtemplateXPO.AddedDate != null ? _userdefinedtemplateXPO.AddedDate : UserDefinedTemplateDTO.AddedDate;
            _userdefinedtemplateXPO.AddedBy = (_userdefinedtemplateXPO.AddedBy != null) ? _userdefinedtemplateXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDefinedTemplateDTO.AddedByID);
            _userdefinedtemplateXPO.LastUpdate = _userdefinedtemplateXPO.LastUpdate == UserDefinedTemplateDTO.LastUpdate ? _userdefinedtemplateXPO.LastUpdate : UserDefinedTemplateDTO.LastUpdate;
            _userdefinedtemplateXPO.LastUpdateBy = (_userdefinedtemplateXPO.LastUpdateBy != null && _userdefinedtemplateXPO.LastUpdateBy.Oid == UserDefinedTemplateDTO.LastUpdateByID) ? _userdefinedtemplateXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDefinedTemplateDTO.LastUpdateByID);
            _userdefinedtemplateXPO.IsActive = _userdefinedtemplateXPO.IsActive == UserDefinedTemplateDTO.IsActive ? (bool)_userdefinedtemplateXPO.IsActive : (bool)UserDefinedTemplateDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _userdefinedtemplateXPO;
    }
}
