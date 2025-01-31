using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.BLL.Features.Quality.QMS.DocumentRevision;
using CTSTools.DAL.Features.AdvancedSettings.LocationManagement;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;

public class KPIMap
{
    public static KPIDTO XPOToDTO(KPIXPO KPIXPO)
    {
        var _KPIDTO = new KPIDTO();
        try
        {
            _KPIDTO.ID = KPIXPO.Oid;
            _KPIDTO.Name = KPIXPO.Name;
            _KPIDTO.Description = KPIXPO.Description;
            _KPIDTO.Goal = KPIXPO.Goal;
            _KPIDTO.UnitOfMeasureID = (KPIXPO.UnitOfMeasure != null) ? KPIXPO.UnitOfMeasure.Oid : 0;
            _KPIDTO.UnitOfMeasureName = (KPIXPO.UnitOfMeasure != null) ? KPIXPO.UnitOfMeasure.Name : "Unnassigned";
            _KPIDTO.ValueTypeID = (KPIXPO.ValueType != null) ? KPIXPO.ValueType.Oid : 0;
            _KPIDTO.ValueTypeName = (KPIXPO.ValueType != null) ? KPIXPO.ValueType.Name : "Unnassigned";
            _KPIDTO.OwnerID = (KPIXPO.Owner != null) ? KPIXPO.Owner.Oid : 0;
            _KPIDTO.OwnerName = (KPIXPO.Owner != null) ? KPIXPO.Owner.Name : "Unnassigned";
            _KPIDTO.ResponsibleID = (KPIXPO.Responsible != null) ? KPIXPO.Responsible.Oid : 0;
            _KPIDTO.ResponsibleName = (KPIXPO.Responsible != null) ? KPIXPO.Responsible.Name : "Unnassigned";
            _KPIDTO.OwnerDepartmentID = (KPIXPO.OwnerDepartment != null) ? KPIXPO.OwnerDepartment.Oid : 0;
            _KPIDTO.OwnerDepartmentName = (KPIXPO.OwnerDepartment != null) ? KPIXPO.OwnerDepartment.Name : "Unnassigned";
            _KPIDTO.ResponsibleDepartmentID = (KPIXPO.ResponsibleDepartment != null) ? KPIXPO.ResponsibleDepartment.Oid : 0;
            _KPIDTO.ResponsibleDepartmentName = (KPIXPO.ResponsibleDepartment != null) ? KPIXPO.ResponsibleDepartment.Name : "Unnassigned";
            _KPIDTO.DashboardCategoryID = (KPIXPO.DashboardCategory != null) ? KPIXPO.DashboardCategory.Oid : 0;
            _KPIDTO.DashboardCategoryName = (KPIXPO.DashboardCategory != null) ? KPIXPO.DashboardCategory.Name : "Unnassigned";
            _KPIDTO.Shared = KPIXPO.Shared;          
            _KPIDTO.FacilityID = (KPIXPO.Facility != null) ? KPIXPO.Facility.Oid : 0;
            _KPIDTO.FacilityName = (KPIXPO.Facility != null) ? KPIXPO.Facility.Name : "Unnassigned";
            _KPIDTO.EquivalenceID = (KPIXPO.Equivalence != null) ? KPIXPO.Equivalence.Oid : 0;
            _KPIDTO.EquivalenceName = (KPIXPO.Equivalence != null) ? KPIXPO.Equivalence.Name : "Unnassigned";
            _KPIDTO.StatusID = (KPIXPO.Status != null) ? KPIXPO.Status.Oid : 0;
            _KPIDTO.StatusName = (KPIXPO.Status != null) ? KPIXPO.Status.Name : "Unnassigned";
            _KPIDTO.IsParent = KPIXPO.IsParent;
            _KPIDTO.CalculationTypeID = (KPIXPO.CalculationType != null) ? KPIXPO.CalculationType.Oid : 0;
            _KPIDTO.CalculationTypeName = (KPIXPO.CalculationType != null) ? KPIXPO.CalculationType.Name : "Unnassigned";
            _KPIDTO.AddedDate = (KPIXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? KPIXPO.AddedDate : (DateTime?)null;
            _KPIDTO.AddedByID = (KPIXPO.AddedBy != null) ? KPIXPO.AddedBy.Oid : 0;
            _KPIDTO.AddedByName = (KPIXPO.AddedBy != null) ? KPIXPO.AddedBy.Name : "Unnassigned";
            _KPIDTO.LastUpdate = (KPIXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? KPIXPO.LastUpdate : (DateTime?)null;
            _KPIDTO.LastUpdateByID = (KPIXPO.LastUpdateBy != null) ? KPIXPO.LastUpdateBy.Oid : 0;
            _KPIDTO.LastUpdateByName = (KPIXPO.LastUpdateBy != null) ? KPIXPO.LastUpdateBy.Name : "Unnassigned";
            _KPIDTO.IsActive = KPIXPO.IsActive;
            if (_KPIDTO.EquivalenceID > 0)
            {
                if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Greater_Than_Or_Equal)
                {
                    _KPIDTO.EquivalenceIcon = "&ge;";
                }
                else if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Less_Then_Or_Equal)
                {
                    _KPIDTO.EquivalenceIcon = "&le;";
                }
                else if (_KPIDTO.EquivalenceID == (int)Equivalence_Enum.Equal)
                {
                    _KPIDTO.EquivalenceIcon = "=";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _KPIDTO;
    }

    public static KPIXPO DTOtoXPO(KPIDTO KPIDTO, UnitOfWork UnitOfWork)
    {
        KPIXPO _KPIXPO;
        try
        {
            _KPIXPO = KPIDTO.ID == null || KPIDTO.ID == 0 ? new KPIXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<KPIXPO>(KPIDTO.ID);
            _KPIXPO.Name = _KPIXPO.Name == KPIDTO.Name ? _KPIXPO.Name : KPIDTO.Name;
            _KPIXPO.Description = _KPIXPO.Description == KPIDTO.Description ? _KPIXPO.Description : KPIDTO.Description;
            _KPIXPO.OwnerDepartment = (_KPIXPO.OwnerDepartment != null && _KPIXPO.OwnerDepartment.Oid == KPIDTO.OwnerDepartmentID) ? _KPIXPO.OwnerDepartment : UnitOfWork.GetObjectByKey<DepartmentXPO>(KPIDTO.OwnerDepartmentID);
            _KPIXPO.ResponsibleDepartment = (_KPIXPO.ResponsibleDepartment != null && _KPIXPO.ResponsibleDepartment.Oid == KPIDTO.ResponsibleDepartmentID) ? _KPIXPO.ResponsibleDepartment : UnitOfWork.GetObjectByKey<DepartmentXPO>(KPIDTO.ResponsibleDepartmentID);
            _KPIXPO.UnitOfMeasure = (_KPIXPO.UnitOfMeasure != null && _KPIXPO.UnitOfMeasure.Oid == KPIDTO.UnitOfMeasureID) ? _KPIXPO.UnitOfMeasure : UnitOfWork.GetObjectByKey<UnitOfMeasureXPO>(KPIDTO.UnitOfMeasureID);
            _KPIXPO.ValueType = (_KPIXPO.ValueType != null && _KPIXPO.ValueType.Oid == KPIDTO.ValueTypeDTO.ID) ? _KPIXPO.ValueType : UnitOfWork.GetObjectByKey<ValueTypeXPO>(KPIDTO.ValueTypeID);
            _KPIXPO.Owner = (_KPIXPO.Owner != null && _KPIXPO.Owner.Oid == KPIDTO.OwnerID) ? _KPIXPO.Owner : UnitOfWork.GetObjectByKey<UserXPO>(KPIDTO.OwnerID);
            _KPIXPO.Responsible = (_KPIXPO.Responsible != null && _KPIXPO.Responsible.Oid == KPIDTO.ResponsibleDTO.ID) ? _KPIXPO.Responsible : UnitOfWork.GetObjectByKey<UserXPO>(KPIDTO.ResponsibleID);
            _KPIXPO.DashboardCategory = (_KPIXPO.DashboardCategory != null && _KPIXPO.DashboardCategory.Oid == KPIDTO.DashboardCategoryID) ? _KPIXPO.DashboardCategory : UnitOfWork.GetObjectByKey<DashboardCategoryXPO>(KPIDTO.DashboardCategoryID);
            //_KPIXPO.Shared = (bool)(_KPIXPO.Shared == KPIDTO.Shared ? _KPIXPO.Shared : KPIDTO.Shared);
            _KPIXPO.Goal = _KPIXPO.Goal == KPIDTO.Goal ? _KPIXPO.Goal : KPIDTO.Goal;
            _KPIXPO.Facility = (_KPIXPO.Facility != null && _KPIXPO.Facility.Oid == KPIDTO.FacilityID) ? _KPIXPO.Facility : UnitOfWork.GetObjectByKey<FacilityXPO>(KPIDTO.FacilityID);
            _KPIXPO.Equivalence = (_KPIXPO.Equivalence != null && _KPIXPO.Equivalence.Oid == KPIDTO.EquivalenceID) ? _KPIXPO.Equivalence : UnitOfWork.GetObjectByKey<EquivalenceXPO>(KPIDTO.EquivalenceID);
            _KPIXPO.Status = (_KPIXPO.Status != null && _KPIXPO.Status.Oid == KPIDTO.StatusID) ? _KPIXPO.Status : UnitOfWork.GetObjectByKey<StatusXPO>(KPIDTO.StatusID);
            //_KPIXPO.IsParent = (bool)(_KPIXPO.IsParent == KPIDTO.IsParent ? _KPIXPO.IsParent : KPIDTO.IsParent);
            _KPIXPO.CalculationType = (_KPIXPO.CalculationType != null && _KPIXPO.CalculationType.Oid == KPIDTO.CalculationTypeDTO.ID) ? _KPIXPO.CalculationType : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(KPIDTO.CalculationTypeID);
            _KPIXPO.FiscalYearCalculationType = (_KPIXPO.FiscalYearCalculationType != null && _KPIXPO.FiscalYearCalculationType.Oid == KPIDTO.FiscalYearCalculationTypeID) ? _KPIXPO.FiscalYearCalculationType : UnitOfWork.GetObjectByKey<CalculationTypeXPO>(KPIDTO.FiscalYearCalculationTypeID);
            _KPIXPO.AddedDate = _KPIXPO.AddedDate != null ? _KPIXPO.AddedDate : KPIDTO.AddedDate;
            _KPIXPO.AddedBy = (_KPIXPO.AddedBy != null) ? _KPIXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(KPIDTO.AddedByID);
            _KPIXPO.LastUpdate = _KPIXPO.LastUpdate == KPIDTO.LastUpdate ? _KPIXPO.LastUpdate : KPIDTO.LastUpdate;
            _KPIXPO.LastUpdateBy = (_KPIXPO.LastUpdateBy != null && _KPIXPO.LastUpdateBy.Oid == KPIDTO.LastUpdateByID) ? _KPIXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(KPIDTO.LastUpdateByID);
            _KPIXPO.IsActive = _KPIXPO.IsActive == KPIDTO.IsActive ? (bool)_KPIXPO.IsActive : (bool)KPIDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _KPIXPO;
    }

    public static List<KPIXPO> DTOListToXPOList(List<KPIDTO> KPIList, UnitOfWork UnitOfWork)
    {
        var _kPIXPOList = new List<KPIXPO>();
        try
        {
            foreach (var _kPIDTO in KPIList)
            {
                _kPIXPOList.Add(DTOtoXPO(_kPIDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _kPIXPOList;
    }
    public static List<KPIDTO> XPCollectionToList(XPCollection<KPIXPO> KPIXPCollection)
    {
        var _kPIList = new List<KPIDTO>();
        try
        {
            foreach (var _kPIXPO in KPIXPCollection)
            {
                _kPIList.Add(XPOToDTO(_kPIXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _kPIList;
    }

}
