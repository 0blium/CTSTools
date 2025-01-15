<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="KPISettings.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPISettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Edashboard</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
                    <li class="breadcrumb-item active"></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Settings</h1>
            </div>
            <!-- END page-header -->
        </div>
        <!-- BEGIN DashboardCategory Catalog -->

        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">

                    <div class="panel-body">
                        <div class="row mb-3">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="">
                                    <div class="">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-12">
                                                <label for="exampleFormControlInput1" class="form-label">Catalog  (<span class="text-danger">*</span>)</label>
                                                <div id="dxCatalogSelectBox"></div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3">
                            <a id="NewCatalogItemBtn" href="#SaveDashboardCategoryRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>Add New</a>
                        </div>
                        <div id="MessageAlert" class="alert alert-green alert-dismissible fade show h-100 mb-1">
                            <b>Please first select a catalog .</b>
                        </div>
                        <div id="dxCatalogGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END DashboardCategory Catalog -->
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveDashboardCategoryRecordModal" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="DashboardCategoryModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseDashboardCategoryModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px" id="CatalogItemNameField">
                        <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-md-12">
                            <div id="dxCatalogItemNameTextBox"></div>
                            <div class="invalid-feedback" id="CatalogItemNameValidation"></div>
                        </div>
                    </div>

                    <div class="row mb-15px" id="GoalRangeValueField">
                        <label class="form-label col-form-label col-md-12">Value (<span class="text-danger">*</span>)</label>
                        <div class="col-md-12">
                            <div id="dxGoalRangeValueNumberBox"></div>
                            <div class="invalid-feedback" id="GoalRangeValueValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="CatalogItemDescriptionField">
                        <label class="form-label col-form-label col-md-12">Description</label>
                        <div class="col-md-12">
                            <div id="dxCatalogItemDescription"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-auto">Is Active?</label>
                        <div class="col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxCatalogItemIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="DashboardCategoryActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenCatalogItemID" hidden />
    <script type="module" src="/App/Features/Management/Edashboard/Settings/KPISettings.js"></script>
</asp:Content>
