namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class MonthDTO
{
    public int? ID { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public string MonthlyValue { get; set; }
    public string KPIBackgroundColor { get; set; }
    public string ProvitionalValueColumnProperty { get; set; }
    public string FontColor { get; set; }
    public string TooltipText { get; set; }
}
