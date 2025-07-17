using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;

namespace CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement
{
    public partial class PrintDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int _dashboardID = (Request.QueryString["DashboardID"] != null
                                  ? Convert.ToInt32(Request.QueryString["DashboardID"].ToString().Trim()) : 0);
                if (_dashboardID != 0)
                {
                    if (!IsCallback && !IsPostBack)
                    {
                        Session["_edashboardList"] = null;
                    }
                    if (Session["_edashboardList"] == null)
                    {
                        Session["_edashboardList"] = Dashboard_KPI_Service.GetDashboardReportList(_dashboardID);
                    }

                    XtraReport _report = new XtraReport();
                    _report = new BLL.Features.Management.Edashboard.DashboardManagement.Report.DashboardReport();
                    _report.DataSource = Session["_edashboardList"] as List<Dashboard_KPIDTO>;

                    PrintDashboardFormat.Report = _report;
                    PrintDashboardFormat.DataBind();
                }

            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
                throw ex;
            }
        }
    }
}