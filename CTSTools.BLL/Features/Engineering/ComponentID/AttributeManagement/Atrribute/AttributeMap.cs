using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;

public class AttributeMap
{
    public static AttributeDTO XPOToDTO(AttributeXPO AttributeXPO)
    {
        var _attributeDTO = new AttributeDTO();
        try
        {
            _attributeDTO.ID = AttributeXPO.Oid;
            _attributeDTO.Name = AttributeXPO.Name;
            _attributeDTO.Description = AttributeXPO.Description;
            _attributeDTO.AddedDate = (AttributeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? AttributeXPO.AddedDate : (DateTime?)null;
            _attributeDTO.AddedByID = (AttributeXPO.AddedBy != null) ? AttributeXPO.AddedBy.Oid : 0;
            _attributeDTO.AddedByName = (AttributeXPO.AddedBy != null) ? AttributeXPO.AddedBy.Name : "Unnassigned";
            _attributeDTO.LastUpdate = (AttributeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? AttributeXPO.LastUpdate : (DateTime?)null;
            _attributeDTO.LastUpdateByID = (AttributeXPO.LastUpdateBy != null) ? AttributeXPO.LastUpdateBy.Oid : 0;
            _attributeDTO.LastUpdateByName = (AttributeXPO.LastUpdateBy != null) ? AttributeXPO.LastUpdateBy.Name : "Unnassigned";
            _attributeDTO.IsActive = AttributeXPO.IsActive;
            _attributeDTO.HasMultipleOptions = AttributeXPO.HasMultipleOptions;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _attributeDTO;
    }

    public static AttributeXPO DTOtoXPO(AttributeDTO AttributeDTO, UnitOfWork UnitOfWork)
    {
        AttributeXPO _attributeXPO;
        try
        {
            _attributeXPO = AttributeDTO.ID == null || AttributeDTO.ID == 0 ? new AttributeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<AttributeXPO>(AttributeDTO.ID);
            _attributeXPO.Name = _attributeXPO.Name == AttributeDTO.Name ? _attributeXPO.Name : AttributeDTO.Name;
            _attributeXPO.Description = _attributeXPO.Description == AttributeDTO.Description ? _attributeXPO.Description : AttributeDTO.Description;
            _attributeXPO.AddedDate = _attributeXPO.AddedDate != null ? _attributeXPO.AddedDate : AttributeDTO.AddedDate;
            _attributeXPO.AddedBy = (_attributeXPO.AddedBy != null) ? _attributeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(AttributeDTO.AddedByID);
            _attributeXPO.LastUpdate = _attributeXPO.LastUpdate == AttributeDTO.LastUpdate ? _attributeXPO.LastUpdate : AttributeDTO.LastUpdate;
            _attributeXPO.LastUpdateBy = (_attributeXPO.LastUpdateBy != null && _attributeXPO.LastUpdateBy.Oid == AttributeDTO.LastUpdateByID) ? _attributeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(AttributeDTO.LastUpdateByID);
            _attributeXPO.IsActive = _attributeXPO.IsActive == AttributeDTO.IsActive ? (bool)_attributeXPO.IsActive : (bool)AttributeDTO.IsActive;
            _attributeXPO.HasMultipleOptions = _attributeXPO.HasMultipleOptions == AttributeDTO.HasMultipleOptions ? (bool)_attributeXPO.HasMultipleOptions : (bool)AttributeDTO.HasMultipleOptions;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _attributeXPO;
    }
    public static List<AttributeXPO> DTOListToXPOList(List<AttributeDTO> AttributeDTOList, UnitOfWork UnitOfWork)
    {
        var _attributeXPOList = new List<AttributeXPO>();
        try
        {
            foreach (var _attributeDTO in AttributeDTOList)
            {
                _attributeXPOList.Add(DTOtoXPO(_attributeDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _attributeXPOList;
    }
    public static List<AttributeDTO> XPCollectionToList(XPCollection<AttributeXPO> AttributeXPCollection)
    {
        var _attributeDTOList = new List<AttributeDTO>();
        try
        {
            foreach (var _attributeXPO in AttributeXPCollection)
            {
                _attributeDTOList.Add(XPOToDTO(_attributeXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _attributeDTOList;
    }
}

