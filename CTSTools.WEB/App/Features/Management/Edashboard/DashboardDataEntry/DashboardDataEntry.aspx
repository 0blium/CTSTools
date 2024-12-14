<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DashboardDataEntry.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardDataEntry.DashboardDataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb float-xl-end">
        <li class="breadcrumb-item"><a href="javascript:;">eDashboard</a></li>
    </ol>
    <h1 class="page-header">Dashboard Data Entry</h1>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div id="dxDashboardMetric_DashboardSelectBox"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12 mt-3 text-center" id="NoDashboardMessage">
        <h5 class="text-muted">No dashboard selected</h5>
    </div>
    <%--<div class="col-md-12 mb-3 d-none" id="PrintDashboardMetricData">
        <a class="btn btn-primary me-1">Print Document</a>
</div>--%>
    <div class="col-lg-12 col-md-12 col-sm-12 d-none" id="DashboardMetricList">
        <!-- Safety Panel -->
        <div class="panel panel-inverse">
            <div class="panel-heading">
                <h4 class="panel-title" style="color: white">S - Safety</h4>
                <div class="panel-heading-btn">
                    <a href="javascript:;" class="btn btn-xs btn-icon btn-warning" data-toggle="panel-collapse"><i class="fa fa-minus"></i></a>
                </div>
            </div>
            <div class="panel-body bg-grey-transparent-2 border ConnectedSortable text-center metric-panel" id="SafetyPanel">
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
            <div class="panel-body bg-grey-transparent-2 border ConnectedSortable text-center metric-panel" id="QualityPanel">
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
            <div class="panel-body bg-grey-transparent-2 border ConnectedSortable text-center metric-panel" id="DeliveryPanel">
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
            <div class="panel-body bg-grey-transparent-2 border ConnectedSortable text-center metric-panel" id="CostPanel">
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
            <div class="panel-body bg-grey-transparent-2 border ConnectedSortable text-center metric-panel" id="MoralPanel">
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
            <div class="panel-body bg-grey-transparent-2 border ConnectedSortable text-center metric-panel" id="Continuos_ImprovementPanel">
            </div>
        </div>
    </div>
    <!--Metric Information Modal-->
    <div class="modal fade" id="DataEntryMetricInfoModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">KPI Information</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body px-5">
                    <div class="row">
                        <table class="table table-bordered">
                            <tbody>
                                <tr>
                                    <th scope="row">KPI</th>
                                    <td id="MetricColumn"></td>
                                </tr>
                                <tr>
                                    <th>Description</th>
                                    <td id="MetricDescriptionColumn"></td>
                                </tr>
                                <tr>
                                    <th scope="row">Goal</th>
                                    <td id="GoalColumn"></td>
                                </tr>
                                <tr>
                                    <th>Month</th>
                                    <td id="MetricValueMonth"></td>
                                </tr>
                                <tr>
                                    <th>Value (<span class="text-danger">*</span>)</th>
                                    <td>
                                        <input class="form-control" id="DashboardDataEntryValue" /></td>
                                </tr>
                                <tr>
                                    <th>Comment (<span class="text-danger">*</span>)</th>
                                    <td>
                                        <textarea class="form-control" id="DashboardDataEntryComments" rows="3"></textarea></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <%--<div class="row">

                    <div class="col-md-2">
                        <label>Value (<span class="text-danger">*</span>)</label>
                        
                        <div class="col-md-2"></div>
                    </div>
                    <div class="col-md-12">
                        <label>Comments (Optional)</label>
                        
                    </div>
                </div>--%>
                </div>

                <div class="modal-footer">
                    <a class="btn btn-white" data-bs-dismiss="modal">Close</a>
                    <a class="btn btn-success" id="SaveDashboardLine">Save</a>
                </div>
            </div>
        </div>
    </div>
    <!--Metric Tendency Modal-->
    <div class="modal fade" id="DashboardMetricTendencyModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div id="dxMetricTendenceChart"></div>
                </div>

                <div class="modal-footer">
                    <a class="btn btn-white" data-bs-dismiss="modal">Close</a>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDashboardLineID" value="0" />
    <script type="module" src="/App/Features/Management/Edashboard/DashboardDataEntry/DashboardDataEntry.js"></script>
</asp:Content>
