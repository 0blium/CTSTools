using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Ticket.Item;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;

public class UserDefinedMap
{
    public static UserDefinedDTO XPOToDTO(UserDefinedXPO UserDefinedXPO)
    {
        var _userdefinedDTO = new UserDefinedDTO();
        try
        {
            _userdefinedDTO.ID = UserDefinedXPO.Oid;
            _userdefinedDTO.Name = UserDefinedXPO.Name;
            _userdefinedDTO.SupportGroupDTO.ID = (UserDefinedXPO.SupportGroup != null) ? UserDefinedXPO.SupportGroup.Oid : 0;
            _userdefinedDTO.SupportGroupDTO.SpanishName = (UserDefinedXPO.SupportGroup != null) ? UserDefinedXPO.SupportGroup.SpanishName : "Unnassigned";
            _userdefinedDTO.SupportGroupDTO.EnglishName = (UserDefinedXPO.SupportGroup != null) ? UserDefinedXPO.SupportGroup.EnglishName : "Unnassigned";
            _userdefinedDTO.IsMandatory = UserDefinedXPO.IsMandatory;
            _userdefinedDTO.DataTypeDTO.ID = (UserDefinedXPO.DataType != null) ? UserDefinedXPO.DataType.Oid : 0;
            _userdefinedDTO.DataTypeDTO.Name = (UserDefinedXPO.DataType != null) ? UserDefinedXPO.DataType.Name : "Unnassigned";
            _userdefinedDTO.NameWithDataType = (UserDefinedXPO.DataType != null) ? $"{UserDefinedXPO.Name} - {UserDefinedXPO.DataType.Name}" : $"{UserDefinedXPO.Name}";
            _userdefinedDTO.AddedDate = (UserDefinedXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? UserDefinedXPO.AddedDate : (DateTime?)null;
            _userdefinedDTO.AddedByID = (UserDefinedXPO.AddedBy != null) ? UserDefinedXPO.AddedBy.Oid : 0;
            _userdefinedDTO.AddedByName = (UserDefinedXPO.AddedBy != null) ? UserDefinedXPO.AddedBy.Name : "Unnassigned";
            _userdefinedDTO.LastUpdate = (UserDefinedXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? UserDefinedXPO.LastUpdate : (DateTime?)null;
            _userdefinedDTO.LastUpdateByID = (UserDefinedXPO.LastUpdateBy != null) ? UserDefinedXPO.LastUpdateBy.Oid : 0;
            _userdefinedDTO.LastUpdateByName = (UserDefinedXPO.LastUpdateBy != null) ? UserDefinedXPO.LastUpdateBy.Name : "Unnassigned";
            _userdefinedDTO.IsActive = UserDefinedXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _userdefinedDTO;
    }

    public static UserDefinedXPO DTOtoXPO(UserDefinedDTO UserDefinedDTO, UnitOfWork UnitOfWork)
    {
        UserDefinedXPO _userdefinedXPO;
        try
        {
            _userdefinedXPO = UserDefinedDTO.ID == null || UserDefinedDTO.ID == 0 ? new UserDefinedXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<UserDefinedXPO>(UserDefinedDTO.ID);
            _userdefinedXPO.Name = _userdefinedXPO.Name == UserDefinedDTO.Name ? _userdefinedXPO.Name : UserDefinedDTO.Name;
            _userdefinedXPO.SupportGroup = (_userdefinedXPO.SupportGroup != null && _userdefinedXPO.SupportGroup.Oid == UserDefinedDTO.SupportGroupDTO.ID) ? _userdefinedXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(UserDefinedDTO.SupportGroupDTO.ID);
            _userdefinedXPO.IsMandatory = _userdefinedXPO.IsMandatory == UserDefinedDTO.IsMandatory ? (bool)_userdefinedXPO.IsMandatory : (bool)UserDefinedDTO.IsMandatory;
            _userdefinedXPO.DataType = (_userdefinedXPO.DataType != null && _userdefinedXPO.DataType.Oid == UserDefinedDTO.DataTypeDTO.ID) ? _userdefinedXPO.DataType : UnitOfWork.GetObjectByKey<DataTypeXPO>(UserDefinedDTO.DataTypeDTO.ID);
            _userdefinedXPO.AddedDate = _userdefinedXPO.AddedDate != null ? _userdefinedXPO.AddedDate : UserDefinedDTO.AddedDate;
            _userdefinedXPO.AddedBy = (_userdefinedXPO.AddedBy != null) ? _userdefinedXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDefinedDTO.AddedByID);
            _userdefinedXPO.LastUpdate = _userdefinedXPO.LastUpdate == UserDefinedDTO.LastUpdate ? _userdefinedXPO.LastUpdate : UserDefinedDTO.LastUpdate;
            _userdefinedXPO.LastUpdateBy = (_userdefinedXPO.LastUpdateBy != null && _userdefinedXPO.LastUpdateBy.Oid == UserDefinedDTO.LastUpdateByID) ? _userdefinedXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDefinedDTO.LastUpdateByID);
            _userdefinedXPO.IsActive = _userdefinedXPO.IsActive == UserDefinedDTO.IsActive ? (bool)_userdefinedXPO.IsActive : (bool)UserDefinedDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _userdefinedXPO;
    }
}
