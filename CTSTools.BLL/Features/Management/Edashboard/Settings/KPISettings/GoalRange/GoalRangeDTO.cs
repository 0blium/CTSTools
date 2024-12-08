using System;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.GoalRange;

public class GoalRangeDTO
{
    #region Base Properties
    public int? ID { get; set; }
    public float Value { get; set; }
    public string Description { get; set; }
    public DateTime? AddedDate { get; set; }
    public int? AddedByID { get; set; }
    public string AddedByName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? LastUpdateByID { get; set; }
    public string LastUpdateByName { get; set; }
    public bool? IsActive { get; set; }

    #endregion

    #region Extended Properties

    public int?[] GoalRangeIDArray { get; set; }

    #endregion
    #region Constructor
    public GoalRangeDTO()
    {
        GoalRangeIDArray = new int?[] { };

    }
    #endregion
}
