using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.ChangeLog;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.ChangeLog;

public class ChangeLogMap
{
    public static ChangeLogDTO XPOToDTO(ChangeLogXPO ChangeLogXPO)
    {
        var _changelogDTO = new ChangeLogDTO();
        try
        {
            _changelogDTO.ID = ChangeLogXPO.Oid;
            _changelogDTO.RecordID = ChangeLogXPO.RecordID;
            _changelogDTO.Table = ChangeLogXPO.Table;
            _changelogDTO.Field = ChangeLogXPO.Field;
            _changelogDTO.OldValue = ChangeLogXPO.OldValue;
            _changelogDTO.NewValue = ChangeLogXPO.NewValue;
            _changelogDTO.UserDTO.ID = (ChangeLogXPO.User != null) ? ChangeLogXPO.User.Oid : 0;
            _changelogDTO.UserDTO.Name = (ChangeLogXPO.User != null) ? ChangeLogXPO.User.Name : "Unnassigned";
            _changelogDTO.AddedDate = (ChangeLogXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ChangeLogXPO.AddedDate : (DateTime?)null;
            _changelogDTO.Action = ChangeLogXPO.Action;
            _changelogDTO.ChangeGroup = ChangeLogXPO.ChangeGroup;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _changelogDTO;
    }

    public static ChangeLogXPO DTOtoXPO(ChangeLogDTO ChangeLogDTO, UnitOfWork UnitOfWork)
    {
        ChangeLogXPO _changelogXPO;
        try
        {
            _changelogXPO = ChangeLogDTO.ID == null || ChangeLogDTO.ID == 0 ? new ChangeLogXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ChangeLogXPO>(ChangeLogDTO.ID);
            _changelogXPO.RecordID = (int)(_changelogXPO.RecordID == ChangeLogDTO.RecordID ? _changelogXPO.RecordID : ChangeLogDTO.RecordID);
            _changelogXPO.Table = _changelogXPO.Table == ChangeLogDTO.Table ? _changelogXPO.Table : ChangeLogDTO.Table;
            _changelogXPO.Field = _changelogXPO.Field == ChangeLogDTO.Field ? _changelogXPO.Field : ChangeLogDTO.Field;
            _changelogXPO.OldValue = _changelogXPO.OldValue == ChangeLogDTO.OldValue ? _changelogXPO.OldValue : ChangeLogDTO.OldValue;
            _changelogXPO.NewValue = _changelogXPO.NewValue == ChangeLogDTO.NewValue ? _changelogXPO.NewValue : ChangeLogDTO.NewValue;
            _changelogXPO.AddedDate = _changelogXPO.AddedDate != null ? _changelogXPO.AddedDate : ChangeLogDTO.AddedDate;
            _changelogXPO.User = (_changelogXPO.User?.Oid == ChangeLogDTO.UserDTO.ID) ? _changelogXPO.User : UnitOfWork.GetObjectByKey<UserXPO>(ChangeLogDTO.UserDTO.ID);
            _changelogXPO.Action = _changelogXPO.Action == ChangeLogDTO.Action ? _changelogXPO.Action : ChangeLogDTO.Action;
            _changelogXPO.ChangeGroup = _changelogXPO.ChangeGroup == ChangeLogDTO.ChangeGroup ? _changelogXPO.ChangeGroup : ChangeLogDTO.ChangeGroup;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _changelogXPO;
    }
}
