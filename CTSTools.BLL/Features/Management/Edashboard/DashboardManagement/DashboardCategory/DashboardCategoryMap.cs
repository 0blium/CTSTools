using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Dashboard;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;

public class DashboardCategoryMap
{
    public static DashboardCategoryDTO XPOToDTO(DashboardCategoryXPO DashboardCategoryXPO)
    {
        var _dashboardcategoryDTO = new DashboardCategoryDTO();
        try
        {
            _dashboardcategoryDTO.ID = DashboardCategoryXPO.Oid;
            _dashboardcategoryDTO.Name = DashboardCategoryXPO.Name;
            _dashboardcategoryDTO.Description = DashboardCategoryXPO.Description;
            _dashboardcategoryDTO.PanelName = DashboardCategoryXPO.PanelName;
            _dashboardcategoryDTO.AddedDate = (DashboardCategoryXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? DashboardCategoryXPO.AddedDate : (DateTime?)null;
            _dashboardcategoryDTO.AddedByID = (DashboardCategoryXPO.AddedBy != null) ? DashboardCategoryXPO.AddedBy.Oid : 0;
            _dashboardcategoryDTO.AddedByName = (DashboardCategoryXPO.AddedBy != null) ? DashboardCategoryXPO.AddedBy.Name : "Unnassigned";
            _dashboardcategoryDTO.LastUpdate = (DashboardCategoryXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? DashboardCategoryXPO.LastUpdate : (DateTime?)null;
            _dashboardcategoryDTO.LastUpdateByID = (DashboardCategoryXPO.LastUpdateBy != null) ? DashboardCategoryXPO.LastUpdateBy.Oid : 0;
            _dashboardcategoryDTO.LastUpdateByName = (DashboardCategoryXPO.LastUpdateBy != null) ? DashboardCategoryXPO.LastUpdateBy.Name : "Unnassigned";
            _dashboardcategoryDTO.IsActive = DashboardCategoryXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardcategoryDTO;
    }

    public static DashboardCategoryXPO DTOtoXPO(DashboardCategoryDTO DashboardCategoryDTO, UnitOfWork UnitOfWork)
    {
        DashboardCategoryXPO _dashboardcategoryXPO;
        try
        {
            _dashboardcategoryXPO = DashboardCategoryDTO.ID == null || DashboardCategoryDTO.ID == 0 ? new DashboardCategoryXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<DashboardCategoryXPO>(DashboardCategoryDTO.ID);
            _dashboardcategoryXPO.Name = _dashboardcategoryXPO.Name == DashboardCategoryDTO.Name ? _dashboardcategoryXPO.Name : DashboardCategoryDTO.Name;
            _dashboardcategoryXPO.Description = _dashboardcategoryXPO.Description == DashboardCategoryDTO.Description ? _dashboardcategoryXPO.Description : DashboardCategoryDTO.Description;
            _dashboardcategoryXPO.PanelName = _dashboardcategoryXPO.PanelName == DashboardCategoryDTO.PanelName ? _dashboardcategoryXPO.PanelName : DashboardCategoryDTO.PanelName;
            _dashboardcategoryXPO.AddedDate = _dashboardcategoryXPO.AddedDate != null ? _dashboardcategoryXPO.AddedDate : DashboardCategoryDTO.AddedDate;
            _dashboardcategoryXPO.AddedBy = (_dashboardcategoryXPO.AddedBy != null) ? _dashboardcategoryXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardCategoryDTO.AddedByID);
            _dashboardcategoryXPO.LastUpdate = _dashboardcategoryXPO.LastUpdate == DashboardCategoryDTO.LastUpdate ? _dashboardcategoryXPO.LastUpdate : DashboardCategoryDTO.LastUpdate;
            _dashboardcategoryXPO.LastUpdateBy = (_dashboardcategoryXPO.LastUpdateBy != null && _dashboardcategoryXPO.LastUpdateBy.Oid == DashboardCategoryDTO.LastUpdateByID) ? _dashboardcategoryXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(DashboardCategoryDTO.LastUpdateByID);
            _dashboardcategoryXPO.IsActive = _dashboardcategoryXPO.IsActive == DashboardCategoryDTO.IsActive ? (bool)_dashboardcategoryXPO.IsActive : (bool)DashboardCategoryDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dashboardcategoryXPO;
    }
    public static List<DashboardCategoryXPO> DTOListToXPOList(List<DashboardCategoryDTO> DashboardCategoryDTOList, UnitOfWork UnitOfWork)
    {
        var _xPOList = new List<DashboardCategoryXPO>();
        try
        {
            foreach (var _dashboardCategoryDTO in DashboardCategoryDTOList)
            {
                _xPOList.Add(DTOtoXPO(_dashboardCategoryDTO, UnitOfWork));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _xPOList;
    }
    public static List<DashboardCategoryDTO> XPCollectionToList(XPCollection<DashboardCategoryXPO> DashboardCategoryXPCollection)
    {
        var _dTOList = new List<DashboardCategoryDTO>();
        try
        {
            foreach (var _dashboardCategoryXPO in DashboardCategoryXPCollection)
            {
                _dTOList.Add(XPOToDTO(_dashboardCategoryXPO));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _dTOList;
    }
}
