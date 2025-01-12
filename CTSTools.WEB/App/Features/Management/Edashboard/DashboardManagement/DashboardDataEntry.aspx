<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DashboardDataEntry.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.DashboardDataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

        <!-- END page-header -->

    </div>
    <div class="col-md-12">
        <div class="row">

            <div class="col-md-6">
                <h3 id="dashboardtitle"></h3>

                </div>
                <div class="col-md-6 mb-3 text-end ">
                    <a id="PrintDashboardBtn" class="btn btn-dark text-righ"><i class="fas fa-print me-2"></i>Print</a>
                    <%--<a id="ExpPDFDashboardBtn" class="btn btn-dark text-righ"><i class="fas fa-file-pdf me-2" style="color: darkred"></i>PDF</a>--%>
                    <%--<a id="ExpExcelDashboardBtn" class="btn btn-dark text-righ"><i class="fas fa-file-excel me-2" style="color: green"></i>Excel</a>--%>                    
                    <a class="btn btn-dark text-righ" id="dashboardButton"><i class="fa-solid fa-arrow-left"></i></a>
                    <a class="btn btn-dark text-righ" id="KPIButton"><i class="fa-solid fa-gear"></i></a>

            </div>
        </div>
    </div>
    <div class="col-lg-12 mt-3 text-center" id="NoDashboardMessage">
        <h5 class="text-muted">No dashboard selected</h5>
    </div>
    <%--<div class="col-md-12 mb-3 d-none" id="PrintDashboard_KPIData">
        <a class="btn btn-primary me-1">Print Document</a>
</div>--%>

    <div class="col-lg-12 col-md-12 col-sm-12 d-none" id="Dashboard_KPIList">

        <ul class="nav nav-pills mb-2" role="tablist" hidden>
            <li id="tab1" class="nav-item" role="presentation">
                <a href="#DashboardTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                    <span class="d-sm-none">Dashboard</span>
                    <span class="d-sm-block d-none">Dashboard</span>
                </a>
            </li>
            <li id="tab2" class="nav-item" role="presentation">
                <a id="asd" href="#KPITab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                    <span class="d-sm-none">KPIs</span>
                    <span class="d-sm-block d-none">KPIs</span>
                </a>
            </li>
        </ul>
        <div class="tab-content rounded-0 m-0">
            <div class="tab-pane fade active show" id="DashboardTab" role="tabpanel">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-inverse">
                            <div class="panel-body">
                                <div class="bg-grey-transparent-2 ConnectedSortable text-center metric-panel" id="DashboardPanel">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="KPITab">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-inverse">
                            <div class="panel-body">
                                <div class="col-md-12 mb-3">
                                    <a id="AddKPIBtn" class="btn btn-success disabled"><i class="fa-solid fa-circle-plus"></i>&nbsp KPI</a>
                                </div>
                                <div id="dxQualityKPIs"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!--KPI Information Modal-->
    <div class="modal fade" id="DataEntryKPIInfoModal">
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
                                    <td id="KPIColumn"></td>
                                </tr>
                                <tr>
                                    <th>Description</th>
                                    <td id="KPIDescriptionColumn"></td>
                                </tr>
                                <tr>
                                    <th scope="row">Goal</th>
                                    <td id="GoalColumn"></td>
                                </tr>
                                <tr>
                                    <th>Month</th>
                                    <td id="KPIValueMonth"></td>
                                </tr>
                                <tr>
                                    <th>Value (<span class="text-danger">*</span>)</th>
                                    <td>
                                        <input class="form-control" id="DashboardDataEntryValue" /></td>
                                </tr>
                                <tr>
                                    <th>Comment <%--(<span class="text-danger">*</span>)--%></th>
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
    <!--KPI Tendency Modal-->
    <div class="modal fade" id="Dashboard_KPITendencyModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div id="dxKPITendenceChart"></div>
                </div>

                <div class="modal-footer">
                    <a class="btn btn-white" data-bs-dismiss="modal">Close</a>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="AddKPIsModal" data-bs-backdrop="static">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title fs-5">Add KPIs to dashboard</h4>
                    <button type="button" id="XBtnModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label class="text-muted">Select the KPIs that you want to add to the selected dashboard</label>
                            <br />
                            <label class="text-muted">(If the value to select doesn´t exits on the list below, create a new value clicking <a class="h6 text-color-link" href="/App/Features/Management/Edashboard/DashboardManagement/KPICatalog.aspx">here</a>)</label>
                            
                        </div>
                        <div class="col-md-12 mt-2">
                            <h6>KPI List(<span class="text-danger">*</span>)</h6>

                            <div id="dxDashboard_KPI_KPIDataGrid"></div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-white" id="CloseBtnModal" data-bs-dismiss="modal">Close</a>
                    <a class="btn btn-success" id="AddKPIButton">Add KPI</a>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDashboardLineID" value="0" />
    <input type="hidden" id="hiddenDashboardID" value="0" />
    <script type="module" src="/App/Features/Management/Edashboard/DashboardManagement/DashboardDataEntry.js"></script>


    <input type="hidden" id="hiddenDashboard_KPIID" />
    <input type="hidden" id="hiddenKPIID" />
    <input type="hidden" id="hiddenDashboardCategoryID" />
    <%--<script type="module" src="/App/Features/Management/Edashboard/DashboardManagement/TemplateAdministration.js"></script>--%>
</asp:Content>
