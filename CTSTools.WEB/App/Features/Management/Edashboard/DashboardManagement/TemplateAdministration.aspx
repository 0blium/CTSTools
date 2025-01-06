<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="TemplateAdministration.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.TemplateAdministration.TemplateAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">E-Dashboard</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Dashboards</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 id="dashboardtitle" class="page-header"></h1>
            </div>
            <!-- END page-header -->

        </div>
        <div class="row">
            <div class="col-md-12">
                <!-- Quality Panel -->
                <div class="panel panel-inverse">
                    <div class="panel-body">

                        <div class="col-md-12 mb-3">
                            <a id="AddMetricBtn" class="btn btn-success disabled"><i class="fa-solid fa-circle-plus"></i>KPI</a>
                        </div>

                        <div id="dxQualityMetrics"></div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Add new metric to Dashboard modal -->
        <div class="modal fade" id="AddMetricsModal" data-bs-backdrop="static">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title fs-5">Add KPIs to dashboard section</h4>
                        <button type="button" id="XBtnModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <p class="text-muted">Select the KPIs that you want to add to the selected dashboard</p>
                            </div>
                            <div class="col-md-12 mt-2">
                                <h6>KPI List(<span class="text-danger">*</span>)</h6>
                                <div id="dxDashboardMetric_MetricDataGrid"></div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <a class="btn btn-white" id="CloseBtnModal" data-bs-dismiss="modal">Close</a>
                        <a class="btn btn-success" id="AddMetricButton">Add KPI</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <input type="hidden" id="hiddenDashboardID" />
    <input type="hidden" id="hiddenDashboardMetricID" />
    <input type="hidden" id="hiddenMetricID" />
    <input type="hidden" id="hiddenDashboardCategoryID" />
    <script type="module" src="/App/Features/Management/Edashboard/DashboardManagement/TemplateAdministration.js"></script>
</asp:Content>
