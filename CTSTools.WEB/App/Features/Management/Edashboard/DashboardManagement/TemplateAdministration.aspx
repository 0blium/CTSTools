<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="TemplateAdministration.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.TemplateAdministration.TemplateAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb float-xl-end">
        <li class="breadcrumb-item"><a href="javascript:;">e-Dashboard</a></li>
    </ol>
    <h1 class="page-header">Dashboard </h1>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="row">
                        <h1 id="dashboardtitle">Text</h1>
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div id="dxDashboardMetric_DashboardSelectBox"></div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 mb-3">
            <a id="AddMetricBtn" class="btn btn-success disabled"><i class="fa-solid fa-circle-plus"></i>Add KPI</a>
            <%--<a id="GetDashboardRevision" class="btn btn-secondary disabled"><i class="fas fa-clipboard-list me-1"></i>View Revision</a>--%>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 d-none" id="DashboardMetricList">
            <!-- Safety Panel -->
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title" style="color: white">S - Safety</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                    </div>
                </div>
                <div class="panel-body bg-grey-transparent-2 border ConnectedSortable" id="SafetyPanel">
                    <div id="dxSafetyMetrics"></div>
                </div>
            </div>
            <!-- Quality Panel -->
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title" style="color: white">Q - Quality</h4>
                    <div class="panel-heading-btn">
                        <div class="panel-heading-btn">
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                        </div>
                    </div>
                </div>
                <div class="panel-body bg-grey-transparent-2 border ConnectedSortable" id="QualityPanel">
                    <div id="dxQualityMetrics"></div>
                </div>
            </div>
            <!-- Delivery Panel -->
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title" style="color: white">D - Delivery</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                    </div>
                </div>
                <div class="panel-body bg-grey-transparent-2 border ConnectedSortable" id="DeliveryPanel">
                    <div id="dxDeliveryMetrics"></div>
                </div>
            </div>
            <!-- Cost Panel -->
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title" style="color: white">C - Cost</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                    </div>
                </div>
                <div class="panel-body bg-grey-transparent-2 border ConnectedSortable" id="CostPanel">
                    <div id="dxCostMetrics"></div>
                </div>
            </div>


            <!-- Moral Panel -->
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title" style="color: white">M - Moral</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                    </div>
                </div>
                <div class="panel-body bg-grey-transparent-2 border ConnectedSortable" id="MoralPanel">
                    <div id="dxMoralMetrics"></div>
                </div>
            </div>
            <!-- Environment Panel -->
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title" style="color: white">E - Emvioramental</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                    </div>
                </div>
                <div class="panel-body bg-grey-transparent-2 border ConnectedSortable" id="EnvironmentPanel">
                    <div id="dxImprovementMetrics"></div>
                </div>
            </div>
        </div>
    </div>

    <!-- Dashboard Update Modal -->
    <div class="modal fade" id="DashboardUpdateModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title fs-5">Update KPI Order</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mb-2">
                            <label>KPI</label>
                            <%--<input class="form-control bg-gray-50" readonly id="DashboardMetricName" />--%>
                            <div id="dxDashboardMetricNameTextBox"></div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <label>Order(<span class="text-danger">*</span>)</label>
                            <%--<input class="form-control" id="DashboardMetricOrder" />--%>
                            <div id="dxDashboardMetricOrderNumberBox"></div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-white" data-bs-dismiss="modal">Close</a>
                    <a class="btn btn-success" id="UpdateOrderMetricInformation">Save</a>
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
    <input type="hidden" id="hiddenDashboardID" />
    <input type="hidden" id="hiddenDashboardMetricID" />
    <input type="hidden" id="hiddenMetricID" />
    <input type="hidden" id="hiddenDashboardCategoryID" />
    <script type="module" src="/App/Features/Management/Edashboard/DashboardManagement/TemplateAdministration.js"></script>
</asp:Content>
