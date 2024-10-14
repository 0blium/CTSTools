using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Security.Role;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;

public class RoleMap
{
    public static RoleDTO XPOToDTO(RoleXPO RoleXPO)
    {
        var _roleDTO = new RoleDTO();
        try
        {
            _roleDTO.ID = RoleXPO.Oid;
            _roleDTO.Name = RoleXPO.Name;
            _roleDTO.Description = RoleXPO.Description;
            _roleDTO.RoleTypeID = (RoleXPO.RoleType != null) ? RoleXPO.RoleType.Oid : 0;
            _roleDTO.RoleTypeName = (RoleXPO.RoleType != null) ? RoleXPO.RoleType.Name : "Unnassigned";
            _roleDTO.AddedDate = (RoleXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? RoleXPO.AddedDate : (DateTime?)null;
            _roleDTO.AddedByID = (RoleXPO.AddedBy != null) ? RoleXPO.AddedBy.Oid : 0;
            _roleDTO.AddedByName = (RoleXPO.AddedBy != null) ? RoleXPO.AddedBy.Name : "Unnassigned";
            _roleDTO.LastUpdate = (RoleXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? RoleXPO.LastUpdate : (DateTime?)null;
            _roleDTO.LastUpdateByID = (RoleXPO.LastUpdateBy != null) ? RoleXPO.LastUpdateBy.Oid : 0;
            _roleDTO.LastUpdateByName = (RoleXPO.LastUpdateBy != null) ? RoleXPO.LastUpdateBy.Name : "Unnassigned";
            _roleDTO.IsActive = RoleXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _roleDTO;
    }

    public static RoleXPO DTOtoXPO(RoleDTO RoleDTO, UnitOfWork UnitOfWork)
    {
        RoleXPO _roleXPO;
        try
        {
            _roleXPO = RoleDTO.ID == null || RoleDTO.ID == 0 ? new RoleXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<RoleXPO>(RoleDTO.ID);
            _roleXPO.Name = _roleXPO.Name == RoleDTO.Name ? _roleXPO.Name : RoleDTO.Name;
            _roleXPO.Description = _roleXPO.Description == RoleDTO.Description ? _roleXPO.Description : RoleDTO.Description;
            _roleXPO.AddedDate = _roleXPO.AddedDate != null ? _roleXPO.AddedDate : RoleDTO.AddedDate;
            _roleXPO.RoleType = (_roleXPO.RoleType != null) ? _roleXPO.RoleType : UnitOfWork.GetObjectByKey<RoleTypeXPO>(RoleDTO.RoleTypeID);
            _roleXPO.AddedBy = (_roleXPO.AddedBy != null) ? _roleXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(RoleDTO.AddedByID);
            _roleXPO.LastUpdate = _roleXPO.LastUpdate == RoleDTO.LastUpdate ? _roleXPO.LastUpdate : RoleDTO.LastUpdate;
            _roleXPO.LastUpdateBy = (_roleXPO.LastUpdateBy != null && _roleXPO.LastUpdateBy.Oid == RoleDTO.LastUpdateByID) ? _roleXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(RoleDTO.LastUpdateByID);
            _roleXPO.IsActive = _roleXPO.IsActive == RoleDTO.IsActive ? (bool)_roleXPO.IsActive : (bool)RoleDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _roleXPO;
    }

}
