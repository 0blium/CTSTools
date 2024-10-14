using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;

public class ActionMap
{
    public static ActionDTO XPOToDTO(ActionXPO ActionXPO)
    {
        var _actionDTO = new ActionDTO();
        try
        {
            _actionDTO.ID = ActionXPO.Oid;
           _actionDTO.Name = ActionXPO.Name; 
           _actionDTO.Description = ActionXPO.Description; 
           _actionDTO.AddedDate = (ActionXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ActionXPO.AddedDate : (DateTime?)null; 
           _actionDTO.AddedByID = (ActionXPO.AddedBy != null) ? ActionXPO.AddedBy.Oid : 0;
           _actionDTO.AddedByName = (ActionXPO.AddedBy != null) ? ActionXPO.AddedBy.Name : "Unnassigned";
           _actionDTO.LastUpdate = (ActionXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ActionXPO.LastUpdate : (DateTime?)null; 
           _actionDTO.LastUpdateByID = (ActionXPO.LastUpdateBy != null) ? ActionXPO.LastUpdateBy.Oid : 0;
           _actionDTO.LastUpdateByName = (ActionXPO.LastUpdateBy != null) ? ActionXPO.LastUpdateBy.Name : "Unnassigned";
           _actionDTO.IsActive = ActionXPO.IsActive; 
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _actionDTO;
    }

    public static ActionXPO DTOtoXPO(ActionDTO ActionDTO, UnitOfWork UnitOfWork)
    {
        ActionXPO _actionXPO;
        try
        {
            _actionXPO = ActionDTO.ID == null || ActionDTO.ID == 0 ? new ActionXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ActionXPO>(ActionDTO.ID);
            _actionXPO.Name = _actionXPO.Name == ActionDTO.Name ? _actionXPO.Name : ActionDTO.Name;
           _actionXPO.Description = _actionXPO.Description == ActionDTO.Description ? _actionXPO.Description : ActionDTO.Description;
           _actionXPO.AddedDate = _actionXPO.AddedDate != null ? _actionXPO.AddedDate : ActionDTO.AddedDate;
           _actionXPO.AddedBy = (_actionXPO.AddedBy != null ) ? _actionXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ActionDTO.AddedByID);
           _actionXPO.LastUpdate = _actionXPO.LastUpdate == ActionDTO.LastUpdate ? _actionXPO.LastUpdate : ActionDTO.LastUpdate;
           _actionXPO.LastUpdateBy = (_actionXPO.LastUpdateBy != null && _actionXPO.LastUpdateBy.Oid == ActionDTO.LastUpdateByID ) ? _actionXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ActionDTO.LastUpdateByID);
           _actionXPO.IsActive = _actionXPO.IsActive == ActionDTO.IsActive ? (bool)_actionXPO.IsActive : (bool)ActionDTO.IsActive;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _actionXPO;
    }

}
