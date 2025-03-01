using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.ChangeLog;

public class ChangeLogDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public int? RecordID { get; set; }
    public string Table { get; set; }
    public string Field { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public string UserName { get; set; }
    public int? UserID { get; set; }
    public string Action { get; set; }
    public string ChangeGroup { get; set; }
    public DateTime? AddedDate { get; set; }
    #endregion
    #region Extended Properties
    public int?[] ChangeLogIDArray { get; set; }
    public UserDTO UserDTO { get; set; }
    public bool GetUserDTO { get; set; }
    public int?[] UserIDArray { get; set; }

    #endregion
    #region Constructor
    public ChangeLogDTO()
    {
        ChangeLogIDArray = new int?[] { };
        UserDTO = new UserDTO();
        UserIDArray = new int?[] { };
    }
    #endregion
}
