using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;

public class ValueMap
{
    public static ValueDTO XPOToDTO(ValueXPO ValueXPO)
    {
        var _valueDTO = new ValueDTO();
        try
        {
            _valueDTO.ID = ValueXPO.Oid;
            _valueDTO.AttributeID = (ValueXPO.Attribute != null) ? ValueXPO.Attribute.Oid : 0;
            _valueDTO.AttributeName = (ValueXPO.Attribute != null) ? ValueXPO.Attribute.Name : "Unnassigned";
            _valueDTO.Name = ValueXPO.Name;
            _valueDTO.Code = ValueXPO.Code;
            _valueDTO.Description = ValueXPO.Description;
            _valueDTO.AddedDate = (ValueXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ValueXPO.AddedDate : (DateTime?)null;
            _valueDTO.AddedByID = (ValueXPO.AddedBy != null) ? ValueXPO.AddedBy.Oid : 0;
            _valueDTO.AddedByName = (ValueXPO.AddedBy != null) ? ValueXPO.AddedBy.Name : "Unnassigned";
            _valueDTO.LastUpdate = (ValueXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ValueXPO.LastUpdate : (DateTime?)null;
            _valueDTO.LastUpdateByID = (ValueXPO.LastUpdateBy != null) ? ValueXPO.LastUpdateBy.Oid : 0;
            _valueDTO.LastUpdateByName = (ValueXPO.LastUpdateBy != null) ? ValueXPO.LastUpdateBy.Name : "Unnassigned";
            _valueDTO.IsActive = ValueXPO.IsActive;
            _valueDTO.IsCounter = ValueXPO.IsCounter;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valueDTO;
    }

    public static ValueXPO DTOtoXPO(ValueDTO ValueDTO, UnitOfWork UnitOfWork)
    {
        ValueXPO _valueXPO;
        try
        {
            _valueXPO = ValueDTO.ID == null || ValueDTO.ID == 0 ? new ValueXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ValueXPO>(ValueDTO.ID);
            _valueXPO.Attribute = (_valueXPO.Attribute != null && _valueXPO.Attribute.Oid == ValueDTO.AttributeID) ? _valueXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(ValueDTO.AttributeID);
            _valueXPO.Name = _valueXPO.Name == ValueDTO.Name ? _valueXPO.Name : ValueDTO.Name;
            _valueXPO.Code = _valueXPO.Code == ValueDTO.Code ? _valueXPO.Code : ValueDTO.Code;
            _valueXPO.Description = _valueXPO.Description == ValueDTO.Description ? _valueXPO.Description : ValueDTO.Description;
            _valueXPO.AddedDate = _valueXPO.AddedDate != null ? _valueXPO.AddedDate : ValueDTO.AddedDate;
            _valueXPO.AddedBy = (_valueXPO.AddedBy != null) ? _valueXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ValueDTO.AddedByID);
            _valueXPO.LastUpdate = _valueXPO.LastUpdate == ValueDTO.LastUpdate ? _valueXPO.LastUpdate : ValueDTO.LastUpdate;
            _valueXPO.LastUpdateBy = (_valueXPO.LastUpdateBy != null) ? _valueXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ValueDTO.LastUpdateByID);
            _valueXPO.IsActive = _valueXPO.IsActive == ValueDTO.IsActive ? (bool)_valueXPO.IsActive : (bool)ValueDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valueXPO;
    }
    public static List<ValueXPO> DTOListToXPOList(List<ValueDTO> ValueDTOList, UnitOfWork UnitOfWork)
    {
        var _valueXPOList = new List<ValueXPO>();
        try
        {
            foreach (var _valueDTO in ValueDTOList)
            {
                _valueXPOList.Add(DTOtoXPO(_valueDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valueXPOList;
    }
    public static List<ValueDTO> XPCollectionToList(XPCollection<ValueXPO> ValueXPCollection)
    {
        var _valueDTOList = new List<ValueDTO>();
        try
        {
            foreach (var _ValueXPO in ValueXPCollection)
            {
                _valueDTOList.Add(XPOToDTO(_ValueXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valueDTOList;
    }
}

