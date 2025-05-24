using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using CTSTools.DAL.Features.Maintenance.AMS.SupportGroup;
using DevExpress.Xpo;
using Elmah;
using System;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;

public class UserDefinedValueMap
{
    public static UserDefinedValueDTO XPOToDTO(UserDefinedValueXPO UserDefinedValueXPO)
    {
        var _userdefinedvalueDTO = new UserDefinedValueDTO();
        try
        {
            _userdefinedvalueDTO.ID = UserDefinedValueXPO.Oid;
            _userdefinedvalueDTO.Value = UserDefinedValueXPO.Value;
            _userdefinedvalueDTO.Item_LineID = (UserDefinedValueXPO.Item_Line != null) ? UserDefinedValueXPO.Item_Line.Oid : 0;
            _userdefinedvalueDTO.SupportGroupID = (UserDefinedValueXPO.SupportGroup != null) ? UserDefinedValueXPO.SupportGroup.Oid : 0;
            _userdefinedvalueDTO.SupportGroupName = (UserDefinedValueXPO.SupportGroup != null) ? UserDefinedValueXPO.SupportGroup.Name : "Unnassigned";
            _userdefinedvalueDTO.UserDefinedID = (UserDefinedValueXPO.UserDefined != null) ? UserDefinedValueXPO.UserDefined.Oid : 0;
            _userdefinedvalueDTO.UserDefinedName = (UserDefinedValueXPO.UserDefined != null) ? UserDefinedValueXPO.UserDefined.Name : "Unnassigned";
            _userdefinedvalueDTO.AddedDate = (UserDefinedValueXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? UserDefinedValueXPO.AddedDate : (DateTime?)null;
            _userdefinedvalueDTO.AddedByID = (UserDefinedValueXPO.AddedBy != null) ? UserDefinedValueXPO.AddedBy.Oid : 0;
            _userdefinedvalueDTO.AddedByName = (UserDefinedValueXPO.AddedBy != null) ? UserDefinedValueXPO.AddedBy.Name : "Unnassigned";
            _userdefinedvalueDTO.LastUpdate = (UserDefinedValueXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? UserDefinedValueXPO.LastUpdate : (DateTime?)null;
            _userdefinedvalueDTO.LastUpdateByID = (UserDefinedValueXPO.LastUpdateBy != null) ? UserDefinedValueXPO.LastUpdateBy.Oid : 0;
            _userdefinedvalueDTO.LastUpdateByName = (UserDefinedValueXPO.LastUpdateBy != null) ? UserDefinedValueXPO.LastUpdateBy.Name : "Unnassigned";
            _userdefinedvalueDTO.IsActive = UserDefinedValueXPO.IsActive;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw ex;
        }
        return _userdefinedvalueDTO;
    }

    public static UserDefinedValueXPO DTOtoXPO(UserDefinedValueDTO UserDefinedValueDTO, UnitOfWork UnitOfWork)
    {
        UserDefinedValueXPO _userdefinedvalueXPO;
        try
        {
            _userdefinedvalueXPO = UserDefinedValueDTO.ID == null || UserDefinedValueDTO.ID == 0 ? new UserDefinedValueXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<UserDefinedValueXPO>(UserDefinedValueDTO.ID);
            _userdefinedvalueXPO.Value = _userdefinedvalueXPO.Value == UserDefinedValueDTO.Value ? _userdefinedvalueXPO.Value : UserDefinedValueDTO.Value;
            _userdefinedvalueXPO.Item_Line = (_userdefinedvalueXPO.Item_Line != null && _userdefinedvalueXPO.Item_Line.Oid == UserDefinedValueDTO.Item_LineID) ? _userdefinedvalueXPO.Item_Line : UnitOfWork.GetObjectByKey<Item_LineXPO>(UserDefinedValueDTO.Item_LineID);
            _userdefinedvalueXPO.SupportGroup = (_userdefinedvalueXPO.SupportGroup != null && _userdefinedvalueXPO.SupportGroup.Oid == UserDefinedValueDTO.SupportGroupID) ? _userdefinedvalueXPO.SupportGroup : UnitOfWork.GetObjectByKey<SupportGroupXPO>(UserDefinedValueDTO.SupportGroupID);
            _userdefinedvalueXPO.UserDefined = (_userdefinedvalueXPO.UserDefined != null && _userdefinedvalueXPO.UserDefined.Oid == UserDefinedValueDTO.UserDefinedID) ? _userdefinedvalueXPO.UserDefined : UnitOfWork.GetObjectByKey<UserDefinedXPO>(UserDefinedValueDTO.UserDefinedID);
            _userdefinedvalueXPO.AddedDate = _userdefinedvalueXPO.AddedDate != null ? _userdefinedvalueXPO.AddedDate : UserDefinedValueDTO.AddedDate;
            _userdefinedvalueXPO.AddedBy = (_userdefinedvalueXPO.AddedBy != null) ? _userdefinedvalueXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDefinedValueDTO.AddedByID);
            _userdefinedvalueXPO.LastUpdate = _userdefinedvalueXPO.LastUpdate == UserDefinedValueDTO.LastUpdate ? _userdefinedvalueXPO.LastUpdate : UserDefinedValueDTO.LastUpdate;
            _userdefinedvalueXPO.LastUpdateBy = (_userdefinedvalueXPO.LastUpdateBy != null && _userdefinedvalueXPO.LastUpdateBy.Oid == UserDefinedValueDTO.LastUpdateByID) ? _userdefinedvalueXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(UserDefinedValueDTO.LastUpdateByID);
            _userdefinedvalueXPO.IsActive = _userdefinedvalueXPO.IsActive == UserDefinedValueDTO.IsActive ? (bool)_userdefinedvalueXPO.IsActive : (bool)UserDefinedValueDTO.IsActive;

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            throw ex;
        }
        return _userdefinedvalueXPO;
    }
}
