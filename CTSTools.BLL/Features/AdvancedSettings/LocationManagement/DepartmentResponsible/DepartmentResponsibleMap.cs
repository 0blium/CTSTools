using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.LocationManagement.DepartmentResponsible;

public class DepartmentResponsibleMap
{
    public static DepartmentResponsibleDTO XPOToDTO(DepartmentResponsibleXPO DepartmentResponsibleXPO)
    {
        var _departmentResponsibleDTO = new DepartmentResponsibleDTO();
        try
        {
            _departmentResponsibleDTO.ID = DepartmentResponsibleXPO.Oid;
            _departmentResponsibleDTO.DepartmentID = (DepartmentResponsibleXPO.Department != null) ? DepartmentResponsibleXPO.Department.Oid : 0;
            _departmentResponsibleDTO.DepartmentName = (DepartmentResponsibleXPO.Department != null) ? DepartmentResponsibleXPO.Department.Name : "Unassigned";
            _departmentResponsibleDTO.ResponsibleID = (DepartmentResponsibleXPO.Responsible != null)? DepartmentResponsibleXPO.Responsible.Oid : 0;
            _departmentResponsibleDTO.ResponsibleName = (DepartmentResponsibleXPO.Responsible != null) ? DepartmentResponsibleXPO.Responsible.Name : "Unassigned";
            _departmentResponsibleDTO.AddedDate = (DepartmentResponsibleXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DepartmentResponsibleXPO.AddedDate : (DateTime?)null;
            _departmentResponsibleDTO.AddedByID = (DepartmentResponsibleXPO.AddedBy != null) ? DepartmentResponsibleXPO.AddedBy.Oid : 0;
            _departmentResponsibleDTO.AddedByName = (DepartmentResponsibleXPO.AddedBy != null) ? DepartmentResponsibleXPO.AddedBy.Name : "Unassigned";
            _departmentResponsibleDTO.LastUpdate = (DepartmentResponsibleXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DepartmentResponsibleXPO.LastUpdate : (DateTime?)null;
            _departmentResponsibleDTO.LastUpdateByID = (DepartmentResponsibleXPO.LastUpdateBy != null) ? DepartmentResponsibleXPO.LastUpdateBy.Oid : 0;
            _departmentResponsibleDTO.LastUpdateByName = (DepartmentResponsibleXPO.LastUpdateBy != null) ? DepartmentResponsibleXPO.LastUpdateBy.Name : "Unassigned";
            _departmentResponsibleDTO.IsActive = DepartmentResponsibleXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _departmentResponsibleDTO;
    }

    public static DepartmentResponsibleXPO DTOtoXPO(DepartmentResponsibleDTO DepartmentResponsibleDTO, UnitOfWork UnitOfWork)
    {
        DepartmentResponsibleXPO _departmentResponsibleXPO;
        try
        {
            _departmentResponsibleXPO = DepartmentResponsibleDTO.ID == null || DepartmentResponsibleDTO.ID == 0 ? new DepartmentResponsibleXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DepartmentResponsibleXPO>(DepartmentResponsibleDTO.ID);
            _departmentResponsibleXPO.AddedDate = _departmentResponsibleXPO.AddedDate != null ? _departmentResponsibleXPO.AddedDate : DepartmentResponsibleDTO.AddedDate;
            _departmentResponsibleXPO.AddedBy = _departmentResponsibleXPO.AddedBy != null ? _departmentResponsibleXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DepartmentResponsibleDTO.AddedByID);
            _departmentResponsibleXPO.LastUpdate = _departmentResponsibleXPO.LastUpdate == DepartmentResponsibleDTO.LastUpdate ? _departmentResponsibleXPO.LastUpdate : DepartmentResponsibleDTO.LastUpdate;
            _departmentResponsibleXPO.LastUpdateBy = _departmentResponsibleXPO.LastUpdateBy != null && _departmentResponsibleXPO.LastUpdateBy.Oid == DepartmentResponsibleDTO.LastUpdateByID ? _departmentResponsibleXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DepartmentResponsibleDTO.LastUpdateByID);
            _departmentResponsibleXPO.IsActive = _departmentResponsibleXPO.IsActive == DepartmentResponsibleDTO.IsActive ? (bool)_departmentResponsibleXPO.IsActive : (bool)DepartmentResponsibleDTO.IsActive;
            _departmentResponsibleXPO.Department = _departmentResponsibleXPO.Department != null && _departmentResponsibleXPO.Department.Oid == DepartmentResponsibleDTO.DepartmentDTO.ID ? _departmentResponsibleXPO.Department : UnitOfWork.GetObjectByKey<DepartmentXPO>(DepartmentResponsibleDTO.DepartmentDTO.ID);
            _departmentResponsibleXPO.Responsible = _departmentResponsibleXPO.Responsible != null && _departmentResponsibleXPO.Responsible.Oid == DepartmentResponsibleDTO.ResponsibleDTO.ID ? _departmentResponsibleXPO.Responsible : UnitOfWork.GetObjectByKey<UserXPO>(DepartmentResponsibleDTO.ResponsibleDTO.ID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _departmentResponsibleXPO;
    }
}
