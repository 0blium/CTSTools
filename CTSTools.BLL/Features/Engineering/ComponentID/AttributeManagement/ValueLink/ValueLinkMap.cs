using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;

public class ValueLinkMap
{
    public static ValueLinkDTO XPOToDTO(ValueLinkXPO ValueLinkXPO)
    {
        var _valuelinkDTO = new ValueLinkDTO();
        try
        {
            _valuelinkDTO.ID = ValueLinkXPO.Oid;
            _valuelinkDTO.Description = ValueLinkXPO.Description;
            _valuelinkDTO.AddedDate = (ValueLinkXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ValueLinkXPO.AddedDate : (DateTime?)null;
            _valuelinkDTO.ParentAttributeID = (ValueLinkXPO.ParentAttribute != null) ? ValueLinkXPO.ParentAttribute.Oid : 0;
            _valuelinkDTO.ParentAttributeName = (ValueLinkXPO.ParentAttribute != null) ? ValueLinkXPO.ParentAttribute.Name : "Unnassigned";
            _valuelinkDTO.ParentValueID = (ValueLinkXPO.ParentValue != null) ? ValueLinkXPO.ParentValue.Oid : 0;
            _valuelinkDTO.ParentValueName = (ValueLinkXPO.ParentValue != null) ? ValueLinkXPO.ParentValue.Name : "Unnassigned";
            _valuelinkDTO.ChildValueID = (ValueLinkXPO.ChildValue != null) ? ValueLinkXPO.ChildValue.Oid : 0;
            _valuelinkDTO.ChildValueName = (ValueLinkXPO.ChildValue != null) ? ValueLinkXPO.ChildValue.Name : "Unnassigned";
            _valuelinkDTO.ChildAttributeID = (ValueLinkXPO.ChildAttribute != null) ? ValueLinkXPO.ChildAttribute.Oid : 0;
            _valuelinkDTO.ChildAttributeName = (ValueLinkXPO.ChildAttribute != null) ? ValueLinkXPO.ChildAttribute.Name : "Unnassigned";
            _valuelinkDTO.AddedByID = (ValueLinkXPO.AddedBy != null) ? ValueLinkXPO.AddedBy.Oid : 0;
            _valuelinkDTO.AddedByName = (ValueLinkXPO.AddedBy != null) ? ValueLinkXPO.AddedBy.Name : "Unnassigned";
            _valuelinkDTO.LastUpdate = (ValueLinkXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ValueLinkXPO.LastUpdate : (DateTime?)null;
            _valuelinkDTO.LastUpdateByID = (ValueLinkXPO.LastUpdateBy != null) ? ValueLinkXPO.LastUpdateBy.Oid : 0;
            _valuelinkDTO.LastUpdateByName = (ValueLinkXPO.LastUpdateBy != null) ? ValueLinkXPO.LastUpdateBy.Name : "Unnassigned";
            _valuelinkDTO.IsActive = ValueLinkXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valuelinkDTO;
    }
    public static ValueLinkXPO DTOtoXPO(ValueLinkDTO ValueLinkDTO, UnitOfWork UnitOfWork)
    {
        ValueLinkXPO _valuelinkXPO;
        try
        {
            _valuelinkXPO = ValueLinkDTO.ID == null || ValueLinkDTO.ID == 0 ? new ValueLinkXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ValueLinkXPO>(ValueLinkDTO.ID);
            _valuelinkXPO.Description = _valuelinkXPO.Description == ValueLinkDTO.Description ? _valuelinkXPO.Description : ValueLinkDTO.Description;
            _valuelinkXPO.AddedDate = _valuelinkXPO.AddedDate != null ? _valuelinkXPO.AddedDate : ValueLinkDTO.AddedDate;
            _valuelinkXPO.AddedBy = (_valuelinkXPO.AddedBy != null && _valuelinkXPO.AddedBy.Oid == ValueLinkDTO.AddedByID) ? _valuelinkXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ValueLinkDTO.AddedByID);
            _valuelinkXPO.LastUpdateBy = (_valuelinkXPO.LastUpdateBy != null && _valuelinkXPO.LastUpdateBy.Oid == ValueLinkDTO.LastUpdateByID) ? _valuelinkXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ValueLinkDTO.LastUpdateByID);
            _valuelinkXPO.LastUpdate = _valuelinkXPO.LastUpdate == ValueLinkDTO.LastUpdate ? _valuelinkXPO.LastUpdate : ValueLinkDTO.LastUpdate;
            _valuelinkXPO.IsActive = _valuelinkXPO.IsActive == ValueLinkDTO.IsActive ? (bool)_valuelinkXPO.IsActive : (bool)ValueLinkDTO.IsActive;
            _valuelinkXPO.ParentAttribute = (_valuelinkXPO.ParentAttribute != null && _valuelinkXPO.ParentAttribute.Oid == ValueLinkDTO.ParentAttributeID) ? _valuelinkXPO.ParentAttribute : UnitOfWork.GetObjectByKey<AttributeXPO>(ValueLinkDTO.ParentAttributeID);
            _valuelinkXPO.ParentValue = (_valuelinkXPO.ParentValue != null && _valuelinkXPO.ParentValue.Oid == ValueLinkDTO.ParentValueID) ? _valuelinkXPO.ParentValue : UnitOfWork.GetObjectByKey<ValueXPO>(ValueLinkDTO.ParentValueID);
            _valuelinkXPO.ChildValue = (_valuelinkXPO.ChildValue != null && _valuelinkXPO.ChildValue.Oid == ValueLinkDTO.ChildValueID) ? _valuelinkXPO.ChildValue : UnitOfWork.GetObjectByKey<ValueXPO>(ValueLinkDTO.ChildValueID);
            _valuelinkXPO.ChildAttribute = (_valuelinkXPO.ChildAttribute != null && _valuelinkXPO.ChildAttribute.Oid == ValueLinkDTO.ChildAttributeID) ? _valuelinkXPO.ChildAttribute : UnitOfWork.GetObjectByKey<AttributeXPO>(ValueLinkDTO.ChildAttributeID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valuelinkXPO;
    }

    public static List<ValueLinkDTO> XPCollectionToList(XPCollection<ValueLinkXPO> ValueLinkXPCollection)
    {
        var _dTOList = new List<ValueLinkDTO>();
        try
        {
            foreach (var _valueLinkXPO in ValueLinkXPCollection)
            {
                _dTOList.Add(XPOToDTO(_valueLinkXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
    public static List<ValueLinkXPO> DTOListToXPOList(List<ValueLinkDTO> ValueLinkList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<ValueLinkXPO>();
        try
        {
            foreach (var _valueLinkDTO in ValueLinkList)
            {
                var _xPO = DTOtoXPO(_valueLinkDTO, UnitOfWork);
                _xPOList.Add(_xPO);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
}
