using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.GoalRange;

public class GoalRangeMap
{
    public static GoalRangeDTO XPOToDTO(GoalRangeXPO GoalRangeXPO)
    {
        var _goalrangeDTO = new GoalRangeDTO();
        try
        {
            _goalrangeDTO.ID = GoalRangeXPO.Oid;
            _goalrangeDTO.Description = GoalRangeXPO.Description;
            _goalrangeDTO.Value = GoalRangeXPO.Value;
            _goalrangeDTO.AddedDate = (GoalRangeXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? GoalRangeXPO.AddedDate : (DateTime?)null;
            _goalrangeDTO.AddedByID = (GoalRangeXPO.AddedBy != null) ? GoalRangeXPO.AddedBy.Oid : 0;
            _goalrangeDTO.AddedByName = (GoalRangeXPO.AddedBy != null) ? GoalRangeXPO.AddedBy.Name : "Unnassigned";
            _goalrangeDTO.LastUpdate = (GoalRangeXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? GoalRangeXPO.LastUpdate : (DateTime?)null;
            _goalrangeDTO.LastUpdateByID = (GoalRangeXPO.LastUpdateBy != null) ? GoalRangeXPO.LastUpdateBy.Oid : 0;
            _goalrangeDTO.LastUpdateByName = (GoalRangeXPO.LastUpdateBy != null) ? GoalRangeXPO.LastUpdateBy.Name : "Unnassigned";
            _goalrangeDTO.IsActive = GoalRangeXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _goalrangeDTO;
    }

    public static GoalRangeXPO DTOtoXPO(GoalRangeDTO GoalRangeDTO, UnitOfWork UnitOfWork)
    {
        GoalRangeXPO _goalrangeXPO;
        try
        {
            _goalrangeXPO = GoalRangeDTO.ID == null || GoalRangeDTO.ID == 0 ? new GoalRangeXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<GoalRangeXPO>(GoalRangeDTO.ID);
            _goalrangeXPO.Value = _goalrangeXPO.Value == GoalRangeDTO.Value ? _goalrangeXPO.Value : GoalRangeDTO.Value;
            _goalrangeXPO.Description = _goalrangeXPO.Description == GoalRangeDTO.Description ? _goalrangeXPO.Description : GoalRangeDTO.Description;
            _goalrangeXPO.AddedDate = _goalrangeXPO.AddedDate != null ? _goalrangeXPO.AddedDate : GoalRangeDTO.AddedDate;
            _goalrangeXPO.AddedBy = (_goalrangeXPO.AddedBy != null) ? _goalrangeXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(GoalRangeDTO.AddedByID);
            _goalrangeXPO.LastUpdate = _goalrangeXPO.LastUpdate == GoalRangeDTO.LastUpdate ? _goalrangeXPO.LastUpdate : GoalRangeDTO.LastUpdate;
            _goalrangeXPO.LastUpdateBy = (_goalrangeXPO.LastUpdateBy != null && _goalrangeXPO.LastUpdateBy.Oid == GoalRangeDTO.LastUpdateByID) ? _goalrangeXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(GoalRangeDTO.LastUpdateByID);
            _goalrangeXPO.IsActive = _goalrangeXPO.IsActive == GoalRangeDTO.IsActive ? (bool)_goalrangeXPO.IsActive : (bool)GoalRangeDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _goalrangeXPO;
    }

}
