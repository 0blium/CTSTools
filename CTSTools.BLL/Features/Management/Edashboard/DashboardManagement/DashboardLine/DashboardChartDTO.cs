namespace CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardChartDTO
{
    public int ID { get; set; }
    public string OwnerName { get; set; }
    public int MetricCount { get; set; }
    public string Status { get; set; }
    public string Month { get; set; }
    public int FiscalYear { get; set; }
    public int DashboardCount { get; set; }
    public string Dashboard { get; set; }
    public int Order { get; set; }
    public decimal? Tendence { get; set; }
    public decimal Goal { get; set; }
    public string Metric { get; set; }
    public string GoalString { get; set; }
}
