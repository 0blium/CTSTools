using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;

public  class PartMap
{
    public static PartDTO XPOToDTO(PartXPO PartXPO)
    {
        var _partDTO = new PartDTO();
        try
        {
            _partDTO.ID = PartXPO.Oid;
            _partDTO.DecoderID = (PartXPO.Decoder != null) ? PartXPO.Decoder.Oid : 0;
            _partDTO.SupplierID = (PartXPO.Supplier != null) ? PartXPO.Supplier.Oid : 0;
            _partDTO.SupplierName = (PartXPO.Supplier != null) ? PartXPO.Supplier.Name : "Unnassigned";
            _partDTO.MfgPartNumber = PartXPO.MfgPartNumber;
            _partDTO.Description = PartXPO.Description;
            _partDTO.Comment = PartXPO.Comment;
            _partDTO.Number = PartXPO.Number;
            _partDTO.AddedDate = (PartXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? PartXPO.AddedDate : (DateTime?)null;
            _partDTO.AddedByID = (PartXPO.AddedBy != null) ? PartXPO.AddedBy.Oid : 0;
            _partDTO.AddedByName = (PartXPO.AddedBy != null) ? PartXPO.AddedBy.Name : "Unnassigned";
            _partDTO.LastUpdate = (PartXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? PartXPO.LastUpdate : (DateTime?)null;
            _partDTO.LastUpdateByID = (PartXPO.LastUpdateBy != null) ? PartXPO.LastUpdateBy.Oid : 0;
            _partDTO.LastUpdateByName = (PartXPO.LastUpdateBy != null) ? PartXPO.LastUpdateBy.Name : "Unnassigned";
            _partDTO.IsActive = PartXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _partDTO;
    }

    public static PartXPO DTOtoXPO(PartDTO PartDTO, UnitOfWork UnitOfWork)
    {
        PartXPO _partXPO;
        try
        {
            _partXPO = PartDTO.ID == null || PartDTO.ID == 0 ? new PartXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<PartXPO>(PartDTO.ID);
            _partXPO.Number = _partXPO.Number == PartDTO.Number ? _partXPO.Number : PartDTO.Number;
            _partXPO.MfgPartNumber = _partXPO.MfgPartNumber == PartDTO.MfgPartNumber ? _partXPO.MfgPartNumber : PartDTO.MfgPartNumber;
            _partXPO.Description = _partXPO.Description == PartDTO.Description ? _partXPO.Description : PartDTO.Description;
            _partXPO.Comment = _partXPO.Comment == PartDTO.Comment ? _partXPO.Comment : PartDTO.Comment;
            _partXPO.Supplier = (_partXPO.Supplier != null) ? _partXPO.Supplier : UnitOfWork.GetObjectByKey<SupplierXPO>(PartDTO.SupplierID);
            _partXPO.Decoder = (_partXPO.Decoder != null) ? _partXPO.Decoder : UnitOfWork.GetObjectByKey<DecoderXPO>(PartDTO.DecoderID);
            _partXPO.AddedDate = _partXPO.AddedDate != null ? _partXPO.AddedDate : PartDTO.AddedDate;
            _partXPO.AddedBy = (_partXPO.AddedBy != null) ? _partXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(PartDTO.AddedByID);
            _partXPO.LastUpdate = _partXPO.LastUpdate == PartDTO.LastUpdate ? _partXPO.LastUpdate : PartDTO.LastUpdate;
            _partXPO.LastUpdateBy = (_partXPO.LastUpdateBy != null && _partXPO.LastUpdateBy.Oid == PartDTO.LastUpdateByID) ? _partXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(PartDTO.LastUpdateByID);
            _partXPO.IsActive = _partXPO.IsActive == PartDTO.IsActive ? (bool)_partXPO.IsActive : (bool)PartDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _partXPO;
    }
    public static List<PartXPO> DTOListToXPOList(List<PartDTO> PartDTOList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<PartXPO>();
        try
        {
            foreach (var _partDTO in PartDTOList)
            {
                _xPOList.Add(DTOtoXPO(_partDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
    public static List<PartDTO> XPCollectionToList(XPCollection<PartXPO> PartXPCollection)
    {
        var _dTOList = new List<PartDTO>();
        try
        {
            foreach (var _partXPO in PartXPCollection)
            {
                _dTOList.Add(XPOToDTO(_partXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
}
