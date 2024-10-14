using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;


namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;

public class DepartmentMap
{
    public static DepartmentDTO XPOToDTO(DepartmentXPO DepartmentXPO)
    {
        var _departmentDTO = new DepartmentDTO();
        try
        {
            _departmentDTO.ID = DepartmentXPO.Oid;
            _departmentDTO.Name = DepartmentXPO.Name;
            _departmentDTO.Description = DepartmentXPO.Description;
            _departmentDTO.AddedDate = (DepartmentXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DepartmentXPO.AddedDate : (DateTime?)null;
            _departmentDTO.AddedByID = (DepartmentXPO.AddedBy != null) ? DepartmentXPO.AddedBy.Oid : 0;
            _departmentDTO.AddedByName = (DepartmentXPO.AddedBy != null) ? DepartmentXPO.AddedBy.Name : "Unassigned";
            _departmentDTO.LastUpdate = (DepartmentXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DepartmentXPO.LastUpdate : (DateTime?)null;
            _departmentDTO.LastUpdateByID = (DepartmentXPO.LastUpdateBy != null) ? DepartmentXPO.LastUpdateBy.Oid : 0;
            _departmentDTO.LastUpdateByName = (DepartmentXPO.LastUpdateBy != null) ? DepartmentXPO.LastUpdateBy.Name : "Unassigned";
            _departmentDTO.IsActive = DepartmentXPO.IsActive;
            _departmentDTO.FacilityName = (DepartmentXPO.Facility != null) ? DepartmentXPO.Facility.Name : "Unnassigned";
            _departmentDTO.FacilityID = (DepartmentXPO.Facility != null) ? DepartmentXPO.Facility.Oid : 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _departmentDTO;
    }

    public static DepartmentXPO DTOtoXPO(DepartmentDTO DepartmentDTO, UnitOfWork UnitOfWork)
    {
        DepartmentXPO _departmentXPO;
        try
        {
            _departmentXPO = DepartmentDTO.ID == null || DepartmentDTO.ID == 0 ? new DepartmentXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DepartmentXPO>(DepartmentDTO.ID);
            _departmentXPO.Name = _departmentXPO.Name == DepartmentDTO.Name ? _departmentXPO.Name : DepartmentDTO.Name;
            _departmentXPO.Description = _departmentXPO.Description == DepartmentDTO.Description ? _departmentXPO.Description : DepartmentDTO.Description;
            _departmentXPO.AddedDate = _departmentXPO.AddedDate != null ? _departmentXPO.AddedDate : DepartmentDTO.AddedDate;
            _departmentXPO.AddedBy = _departmentXPO.AddedBy != null ? _departmentXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DepartmentDTO.AddedByID);
            _departmentXPO.LastUpdate = _departmentXPO.LastUpdate == DepartmentDTO.LastUpdate ? _departmentXPO.LastUpdate : DepartmentDTO.LastUpdate;
            _departmentXPO.LastUpdateBy = _departmentXPO.LastUpdateBy != null && _departmentXPO.LastUpdateBy.Oid == DepartmentDTO.LastUpdateByID ? _departmentXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DepartmentDTO.LastUpdateByID);
            _departmentXPO.IsActive = _departmentXPO.IsActive == DepartmentDTO.IsActive ? (bool)_departmentXPO.IsActive : (bool)DepartmentDTO.IsActive;
            _departmentXPO.Facility = _departmentXPO.Facility != null && _departmentXPO.Facility.Oid == DepartmentDTO.FacilityID ? _departmentXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(DepartmentDTO.FacilityID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _departmentXPO;
    }
}
