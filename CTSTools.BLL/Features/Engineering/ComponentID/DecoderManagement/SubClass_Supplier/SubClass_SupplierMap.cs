using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;

internal class SubClass_SupplierMap
{
    public static SubClass_SupplierDTO XPOToDTO(SubClass_SupplierXPO SubClass_SupplierXPO)
    {
        var _subClass_SupplierDTO = new SubClass_SupplierDTO();
        try
        {
            _subClass_SupplierDTO.ID = SubClass_SupplierXPO.Oid;
            _subClass_SupplierDTO.Description = SubClass_SupplierXPO.Description;
            _subClass_SupplierDTO.SubClassID = (SubClass_SupplierXPO.SubClass != null) ? SubClass_SupplierXPO.SubClass.Oid : 0;
            _subClass_SupplierDTO.SubClassName = (SubClass_SupplierXPO.SubClass != null) ? SubClass_SupplierXPO.SubClass.Name : "Unnassigned";
            _subClass_SupplierDTO.SupplierID = (SubClass_SupplierXPO.Supplier != null) ? SubClass_SupplierXPO.Supplier.Oid : 0;
            _subClass_SupplierDTO.SupplierName = (SubClass_SupplierXPO.Supplier != null) ? SubClass_SupplierXPO.Supplier.Name : "Unnassigned";
            _subClass_SupplierDTO.AddedDate = (SubClass_SupplierXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SubClass_SupplierXPO.AddedDate : (DateTime?)null;
            _subClass_SupplierDTO.AddedByID = (SubClass_SupplierXPO.AddedBy != null) ? SubClass_SupplierXPO.AddedBy.Oid : 0;
            _subClass_SupplierDTO.AddedByName = (SubClass_SupplierXPO.AddedBy != null) ? SubClass_SupplierXPO.AddedBy.Name : "Unnassigned";
            _subClass_SupplierDTO.LastUpdate = (SubClass_SupplierXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SubClass_SupplierXPO.LastUpdate : (DateTime?)null;
            _subClass_SupplierDTO.LastUpdateByID = (SubClass_SupplierXPO.LastUpdateBy != null) ? SubClass_SupplierXPO.LastUpdateBy.Oid : 0;
            _subClass_SupplierDTO.LastUpdateByName = (SubClass_SupplierXPO.LastUpdateBy != null) ? SubClass_SupplierXPO.LastUpdateBy.Name : "Unnassigned";
            _subClass_SupplierDTO.IsActive = SubClass_SupplierXPO.IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _subClass_SupplierDTO;
    }

    public static SubClass_SupplierXPO DTOtoXPO(SubClass_SupplierDTO SubClass_SupplierDTO, UnitOfWork UnitOfWork)
    {
        SubClass_SupplierXPO _subClass_SupplierXPO;
        try
        {
            _subClass_SupplierXPO = SubClass_SupplierDTO.ID == null || SubClass_SupplierDTO.ID == 0 ? new SubClass_SupplierXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SubClass_SupplierXPO>(SubClass_SupplierDTO.ID);
            _subClass_SupplierXPO.Description = _subClass_SupplierXPO.Description == SubClass_SupplierDTO.Description ? _subClass_SupplierXPO.Description : SubClass_SupplierDTO.Description;
            _subClass_SupplierXPO.Supplier = (_subClass_SupplierXPO.Supplier != null && _subClass_SupplierXPO.Supplier.Oid == SubClass_SupplierDTO.SupplierID) ? _subClass_SupplierXPO.Supplier : UnitOfWork.GetObjectByKey<SupplierXPO>(SubClass_SupplierDTO.SupplierID);
            _subClass_SupplierXPO.SubClass = (_subClass_SupplierXPO.SubClass != null && _subClass_SupplierXPO.SubClass.Oid == SubClass_SupplierDTO.SubClassID) ? _subClass_SupplierXPO.SubClass : UnitOfWork.GetObjectByKey<ValueXPO>(SubClass_SupplierDTO.SubClassID);
            _subClass_SupplierXPO.AddedDate = _subClass_SupplierXPO.AddedDate != null ? _subClass_SupplierXPO.AddedDate : SubClass_SupplierDTO.AddedDate;
            _subClass_SupplierXPO.AddedBy = (_subClass_SupplierXPO.AddedBy != null) ? _subClass_SupplierXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(SubClass_SupplierDTO.AddedByID);
            _subClass_SupplierXPO.LastUpdate = _subClass_SupplierXPO.LastUpdate == SubClass_SupplierDTO.LastUpdate ? _subClass_SupplierXPO.LastUpdate : SubClass_SupplierDTO.LastUpdate;
            _subClass_SupplierXPO.LastUpdateBy = (_subClass_SupplierXPO.LastUpdateBy != null && _subClass_SupplierXPO.LastUpdateBy.Oid == SubClass_SupplierDTO.LastUpdateByID) ? _subClass_SupplierXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SubClass_SupplierDTO.LastUpdateByID);
            _subClass_SupplierXPO.IsActive = _subClass_SupplierXPO.IsActive == SubClass_SupplierDTO.IsActive ? (bool)_subClass_SupplierXPO.IsActive : (bool)SubClass_SupplierDTO.IsActive;
         
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _subClass_SupplierXPO;
    }
    public static List<SubClass_SupplierDTO> XPCollectionToList(XPCollection<SubClass_SupplierXPO> SubClass_SupplierXPCollection)
    {
        var _dTOList = new List<SubClass_SupplierDTO>();
        try
        {
            foreach (var _subClass_SupplierXPO in SubClass_SupplierXPCollection)
            {
                _dTOList.Add(XPOToDTO(_subClass_SupplierXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
    public static List<SubClass_SupplierXPO> DTOListToXPOList(List<SubClass_SupplierDTO> SubClass_SupplierList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<SubClass_SupplierXPO>();
        try
        {
            foreach (var _subClass_SupplierDTO in SubClass_SupplierList)
            {
                _xPOList.Add(DTOtoXPO(_subClass_SupplierDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
}
