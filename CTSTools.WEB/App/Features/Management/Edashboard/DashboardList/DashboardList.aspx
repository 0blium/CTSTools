<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DashboardList.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardList.DashboardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">E-Dashboard</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Dashboard  List</h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Dashboard Catalog -->
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-inverse">

                    <div class="panel-body">
                        <div class="col-md-12 mb-3">
                            <a data-bs-toggle="collapse" data-bs-target="#collapseUser" class="h6 text-color-link">Can't you find the dashboard?</a>
                            <div id="collapseUser" class="accordion-collapse collapse border border-1 mt-1 mb-1" data-bs-parent="#accordion">
                                <div class="accordion-body accordion-height bg-light p-1  text-dark">
                                    <div class="col-md-12 px-2">
                                        <label class="col-md-12 mt-1">If the dashboard doesn´t exits on the list above, create a new dashboard clicking on the next link</label>
                                        <div>
                                            <a class="h6 mt-1 text-color-link" href="/App/Features/Management/Edashboard/Settings/Dashboard/DashboardCatalog.aspx">Go to the dashboard catalog</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dxDashboardGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END Dashboard Catalog -->
    </div>
    <input type="hidden" id="hiddenDashboardID" hidden />
    <script type="module" src="/App/Features/Management/Edashboard/DashboardList/DashboardList.js"></script>
</asp:Content>
