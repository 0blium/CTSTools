using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

public class SupplierMap
{
    public static SupplierDTO XPOToDTO(SupplierXPO SupplierXPO)
    {
        var _supplierDTO = new SupplierDTO();
        try
        {
            _supplierDTO.ID = SupplierXPO.Oid;
            _supplierDTO.Name = SupplierXPO.Name;
            _supplierDTO.Description = SupplierXPO.Description;
            _supplierDTO.AddedDate = (SupplierXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? SupplierXPO.AddedDate : (DateTime?)null;
            _supplierDTO.AddedByID = (SupplierXPO.AddedBy != null) ? SupplierXPO.AddedBy.Oid : 0;
            _supplierDTO.AddedByName = (SupplierXPO.AddedBy != null) ? SupplierXPO.AddedBy.Name : "Unnassigned";
            _supplierDTO.LastUpdate = (SupplierXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? SupplierXPO.LastUpdate : (DateTime?)null;
            _supplierDTO.LastUpdateByID = (SupplierXPO.LastUpdateBy != null) ? SupplierXPO.LastUpdateBy.Oid : 0;
            _supplierDTO.LastUpdateByName = (SupplierXPO.LastUpdateBy != null) ? SupplierXPO.LastUpdateBy.Name : "Unnassigned";
            _supplierDTO.IsActive = SupplierXPO.IsActive;
            _supplierDTO.IsVendor = SupplierXPO.IsVendor;
            _supplierDTO.IsManufacturer = SupplierXPO.IsManufacturer;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supplierDTO;
    }

    public static SupplierXPO DTOtoXPO(SupplierDTO SupplierDTO, UnitOfWork UnitOfWork)
    {
        SupplierXPO _supplierXPO;
        try
        {
            _supplierXPO = SupplierDTO.ID == null || SupplierDTO.ID == 0 ? new SupplierXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<SupplierXPO>(SupplierDTO.ID);
            _supplierXPO.Name = _supplierXPO.Name == SupplierDTO.Name ? _supplierXPO.Name : SupplierDTO.Name;
            _supplierXPO.Description = _supplierXPO.Description == SupplierDTO.Description ? _supplierXPO.Description : SupplierDTO.Description;
            _supplierXPO.AddedDate = _supplierXPO.AddedDate != null ? _supplierXPO.AddedDate : SupplierDTO.AddedDate;
            _supplierXPO.AddedBy = (_supplierXPO.AddedBy != null) ? _supplierXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(SupplierDTO.AddedByID);
            _supplierXPO.LastUpdate = _supplierXPO.LastUpdate == SupplierDTO.LastUpdate ? _supplierXPO.LastUpdate : SupplierDTO.LastUpdate;
            _supplierXPO.LastUpdateBy = (_supplierXPO.LastUpdateBy != null && _supplierXPO.LastUpdateBy.Oid == SupplierDTO.LastUpdateByID) ? _supplierXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(SupplierDTO.LastUpdateByID);
            _supplierXPO.IsActive = _supplierXPO.IsActive == SupplierDTO.IsActive ? (bool)_supplierXPO.IsActive : (bool)SupplierDTO.IsActive;
            _supplierXPO.IsVendor = _supplierXPO.IsVendor == SupplierDTO.IsVendor ? (bool)_supplierXPO.IsVendor : (bool)SupplierDTO.IsVendor;
            _supplierXPO.IsManufacturer = _supplierXPO.IsManufacturer == SupplierDTO.IsManufacturer ? (bool)_supplierXPO.IsManufacturer : (bool)SupplierDTO.IsManufacturer;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supplierXPO;
    }
    public static List<SupplierXPO> DTOListToXPOList(List<SupplierDTO> SupplierDTOList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<SupplierXPO>();
        try
        {
            foreach (var _supplierDTO in SupplierDTOList)
            {
                _xPOList.Add(DTOtoXPO(_supplierDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
    public static List<SupplierDTO> XPCollectionToList(XPCollection<SupplierXPO> SupplierXPCollection)
    {
        var _dTOList = new List<SupplierDTO>();
        try
        {
            foreach (var _supplierXPO in SupplierXPCollection)
            {
                _dTOList.Add(XPOToDTO(_supplierXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
}
