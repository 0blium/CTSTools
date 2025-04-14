using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;

public class Part_AttributeMap
{
    public static Part_AttributeDTO XPOToDTO(Part_AttributeXPO Part_AttributeXPO)
    {
        var _part_AttributeDTO = new Part_AttributeDTO();
        try
        {
            _part_AttributeDTO.ID = Part_AttributeXPO.Oid;
            _part_AttributeDTO.DecoderID = (Part_AttributeXPO.Decoder != null) ? Part_AttributeXPO.Decoder.Oid : 0;
            _part_AttributeDTO.PartID = (Part_AttributeXPO.Part != null) ? Part_AttributeXPO.Part.Oid : 0;
            _part_AttributeDTO.AttributeID = (Part_AttributeXPO.Attribute != null) ? Part_AttributeXPO.Attribute.Oid : 0;
            _part_AttributeDTO.AttributeName = (Part_AttributeXPO.Attribute != null) ? Part_AttributeXPO.Attribute.Name : "Unnassigned";
            _part_AttributeDTO.ValueID = (Part_AttributeXPO.Value != null) ? Part_AttributeXPO.Value.Oid : 0;

            _part_AttributeDTO.AddedDate = (Part_AttributeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? Part_AttributeXPO.AddedDate : (DateTime?)null;
            _part_AttributeDTO.AddedByID = (Part_AttributeXPO.AddedBy != null) ? Part_AttributeXPO.AddedBy.Oid : 0;
            _part_AttributeDTO.AddedByName = (Part_AttributeXPO.AddedBy != null) ? Part_AttributeXPO.AddedBy.Name : "Unnassigned";
            _part_AttributeDTO.LastUpdate = (Part_AttributeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? Part_AttributeXPO.LastUpdate : (DateTime?)null;
            _part_AttributeDTO.LastUpdateByID = (Part_AttributeXPO.LastUpdateBy != null) ? Part_AttributeXPO.LastUpdateBy.Oid : 0;
            _part_AttributeDTO.LastUpdateByName = (Part_AttributeXPO.LastUpdateBy != null) ? Part_AttributeXPO.LastUpdateBy.Name : "Unnassigned";
            _part_AttributeDTO.IsActive = Part_AttributeXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _part_AttributeDTO;
    }

    public static Part_AttributeXPO DTOtoXPO(Part_AttributeDTO Part_AttributeDTO, UnitOfWork UnitOfWork)
    {
        Part_AttributeXPO _part_AttributeXPO;
        try
        {
            _part_AttributeXPO = Part_AttributeDTO.ID == null || Part_AttributeDTO.ID == 0 ? new Part_AttributeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<Part_AttributeXPO>(Part_AttributeDTO.ID);
            _part_AttributeXPO.Decoder = (_part_AttributeXPO.Decoder != null) ? _part_AttributeXPO.Decoder : UnitOfWork.GetObjectByKey<DecoderXPO>(Part_AttributeDTO.DecoderID);
            _part_AttributeXPO.Value = (_part_AttributeXPO.Value != null) ? _part_AttributeXPO.Value : UnitOfWork.GetObjectByKey<ValueXPO>(Part_AttributeDTO.ValueID);
            _part_AttributeXPO.Attribute = (_part_AttributeXPO.Attribute != null) ? _part_AttributeXPO.Attribute : UnitOfWork.GetObjectByKey<AttributeXPO>(Part_AttributeDTO.AttributeID);
            _part_AttributeXPO.Part = (_part_AttributeXPO.Part != null) ? _part_AttributeXPO.Part : UnitOfWork.GetObjectByKey<PartXPO>(Part_AttributeDTO.PartID);
            _part_AttributeXPO.AddedDate = _part_AttributeXPO.AddedDate != null ? _part_AttributeXPO.AddedDate : Part_AttributeDTO.AddedDate;
            _part_AttributeXPO.AddedBy = (_part_AttributeXPO.AddedBy != null) ? _part_AttributeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(Part_AttributeDTO.AddedByID);
            _part_AttributeXPO.LastUpdate = _part_AttributeXPO.LastUpdate == Part_AttributeDTO.LastUpdate ? _part_AttributeXPO.LastUpdate : Part_AttributeDTO.LastUpdate;
            _part_AttributeXPO.LastUpdateBy = (_part_AttributeXPO.LastUpdateBy != null && _part_AttributeXPO.LastUpdateBy.Oid == Part_AttributeDTO.LastUpdateByID) ? _part_AttributeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(Part_AttributeDTO.LastUpdateByID);
            _part_AttributeXPO.IsActive = _part_AttributeXPO.IsActive == Part_AttributeDTO.IsActive ? (bool)_part_AttributeXPO.IsActive : (bool)Part_AttributeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _part_AttributeXPO;
    }
    public static List<Part_AttributeDTO> XPCollectionToList(XPCollection<Part_AttributeXPO> Part_AttributeXPCollection)
    {
        var _part_AttributeList = new List<Part_AttributeDTO>();
        try
        {
            foreach (var _part_AttributeXPO in Part_AttributeXPCollection)
            {
                _part_AttributeList.Add(XPOToDTO(_part_AttributeXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _part_AttributeList;
    }
    public static List<Part_AttributeXPO> DTOListToXPOList(List<Part_AttributeDTO> Part_AttributeList, UnitOfWork UnitOfWork)
    {
        var _part_AttributeXPOList = new List<Part_AttributeXPO>();
        try
        {
            foreach (var _part_AttributeDTO in Part_AttributeList)
            {
                _part_AttributeXPOList.Add(DTOtoXPO(_part_AttributeDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _part_AttributeXPOList;
    }
}
