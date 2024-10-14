
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;

public class DecoderStructureMap
{
    public static DecoderStructureDTO XPOToDTO(DecoderStructureXPO DecoderStructureXPO)
    {
        var _decoderstructureDTO = new DecoderStructureDTO();
        try
        {
            _decoderstructureDTO.ID = DecoderStructureXPO.Oid;
            _decoderstructureDTO.DecoderID = (DecoderStructureXPO.Decoder != null) ? DecoderStructureXPO.Decoder.Oid : 0;
            _decoderstructureDTO.AttributeID = (DecoderStructureXPO.Attribute != null) ? DecoderStructureXPO.Attribute.Oid : 0;
            _decoderstructureDTO.AttributeName = (DecoderStructureXPO.Attribute != null) ? DecoderStructureXPO.Attribute.Name : "Unnassigned";
            _decoderstructureDTO.DescriptionBody = DecoderStructureXPO.DescriptionBody;
            _decoderstructureDTO.DescriptionOrder = DecoderStructureXPO.DescriptionOrder;
            _decoderstructureDTO.NumberOrder = DecoderStructureXPO.NumberOrder;
            _decoderstructureDTO.NumberBody = DecoderStructureXPO.NumberBody;
            _decoderstructureDTO.Description = DecoderStructureXPO.Description;
            _decoderstructureDTO.ValueName = (DecoderStructureXPO.Value != null) ? DecoderStructureXPO.Value.Name : "Unnassigned";
            _decoderstructureDTO.ValueID = (DecoderStructureXPO.Value != null) ? DecoderStructureXPO.Value.Oid : 0;
            _decoderstructureDTO.AddedDate = (DecoderStructureXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DecoderStructureXPO.AddedDate : (DateTime?)null;
            _decoderstructureDTO.AddedByID = (DecoderStructureXPO.AddedBy != null) ? DecoderStructureXPO.AddedBy.Oid : 0;
            _decoderstructureDTO.AddedByName = (DecoderStructureXPO.AddedBy != null) ? DecoderStructureXPO.AddedBy.Name : "Unnassigned";
            _decoderstructureDTO.LastUpdate = (DecoderStructureXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DecoderStructureXPO.LastUpdate : (DateTime?)null;
            _decoderstructureDTO.LastUpdateByID = (DecoderStructureXPO.LastUpdateBy != null) ? DecoderStructureXPO.LastUpdateBy.Oid : 0;
            _decoderstructureDTO.LastUpdateByName = (DecoderStructureXPO.LastUpdateBy != null) ? DecoderStructureXPO.LastUpdateBy.Name : "Unnassigned";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderstructureDTO;
    }
    public static DecoderStructureXPO DTOtoXPO(DecoderStructureDTO DecoderStructureDTO, UnitOfWork UnitOfWork)
    {
        DecoderStructureXPO _decoderstructureXPO;
        try
        {
            _decoderstructureXPO = DecoderStructureDTO.ID == null || DecoderStructureDTO.ID == 0 ? new DecoderStructureXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DecoderStructureXPO>(DecoderStructureDTO.ID);
            _decoderstructureXPO.Decoder = (_decoderstructureXPO.Decoder != null && _decoderstructureXPO.Decoder.Oid == DecoderStructureDTO.DecoderID) ? _decoderstructureXPO.Decoder : UnitOfWork.GetObjectByKey<DecoderXPO>(DecoderStructureDTO.DecoderID);
            _decoderstructureXPO.Attribute = (_decoderstructureXPO.Attribute != null && _decoderstructureXPO.Attribute.Oid == DecoderStructureDTO.AttributeID) ? _decoderstructureXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(DecoderStructureDTO.AttributeID);
            _decoderstructureXPO.DescriptionBody = _decoderstructureXPO.DescriptionBody == DecoderStructureDTO.DescriptionBody ? (bool)_decoderstructureXPO.DescriptionBody : (bool)DecoderStructureDTO.DescriptionBody;
            _decoderstructureXPO.DescriptionOrder = _decoderstructureXPO.DescriptionOrder == DecoderStructureDTO.DescriptionOrder ? _decoderstructureXPO.DescriptionOrder : DecoderStructureDTO.DescriptionOrder;
            _decoderstructureXPO.NumberOrder = _decoderstructureXPO.NumberOrder == DecoderStructureDTO.NumberOrder ? _decoderstructureXPO.NumberOrder : DecoderStructureDTO.NumberOrder;
            _decoderstructureXPO.NumberBody = _decoderstructureXPO.NumberBody == DecoderStructureDTO.NumberBody ? (bool)_decoderstructureXPO.NumberBody : (bool)DecoderStructureDTO.NumberBody;
            _decoderstructureXPO.Description = _decoderstructureXPO.Description == DecoderStructureDTO.Description ? _decoderstructureXPO.Description : DecoderStructureDTO.Description;
            _decoderstructureXPO.AddedDate = _decoderstructureXPO.AddedDate != null ? _decoderstructureXPO.AddedDate : DecoderStructureDTO.AddedDate;
            _decoderstructureXPO.Value = (_decoderstructureXPO.Value != null && _decoderstructureXPO.Value.Oid == DecoderStructureDTO.ValueID) ? _decoderstructureXPO.Value : UnitOfWork.GetObjectByKey<ValueXPO>(DecoderStructureDTO.ValueID);
            _decoderstructureXPO.AddedBy = (_decoderstructureXPO.AddedBy != null) ? _decoderstructureXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DecoderStructureDTO.AddedByID);
            _decoderstructureXPO.LastUpdate = _decoderstructureXPO.LastUpdate == DecoderStructureDTO.LastUpdate ? _decoderstructureXPO.LastUpdate : DecoderStructureDTO.LastUpdate;
            _decoderstructureXPO.LastUpdateBy = (_decoderstructureXPO.LastUpdateBy != null && _decoderstructureXPO.LastUpdateBy.Oid == DecoderStructureDTO.LastUpdateByID) ? _decoderstructureXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DecoderStructureDTO.LastUpdateByID);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderstructureXPO;
    }
    public static List<DecoderStructureDTO> XPCollectionToList(XPCollection<DecoderStructureXPO> DecoderStructureXPCollection)
    {
        var _decoderStructureList = new List<DecoderStructureDTO>();
        try
        {
            foreach (var _decoderStructureXPO in DecoderStructureXPCollection)
            {
                _decoderStructureList.Add(XPOToDTO(_decoderStructureXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderStructureList;
    }
    public static List<DecoderStructureXPO> DTOListToXPOList(List<DecoderStructureDTO> DecoderStructureList, UnitOfWork UnitOfWork)
    {
        var _decoderStructureXPOList = new List<DecoderStructureXPO>();
        try
        {
            foreach (var _decoderStructureDTO in DecoderStructureList)
            {
                _decoderStructureXPOList.Add(DTOtoXPO(_decoderStructureDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _decoderStructureXPOList;
    }
}

