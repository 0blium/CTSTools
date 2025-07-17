<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="PriorityCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.Tickets.Priority.PriorityCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Tickets</a></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Priority</h1>
        </div>
    </div>
    <!-- END page-header -->
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="col-md-12 ">
                        <a id="NewPriorityBtn" href="#SavePriorityRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i> Priority</a>
                    </div>
                    <div id="dxPriorityGrid"></div>
                </div>
            </div>

        </div>
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SavePriorityRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="PriorityModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnClosePriorityModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Name(<span class="text-danger">*</span>)</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxPriorityNameTextBox"></div>
                            <div class="invalid-feedback" id="PriorityNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxPrioritySupportGroupSelectBox"></div>
                            <div class="invalid-feedback" id="PrioritySupportGroupValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Description</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxPriorityDescriptionTextArea"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-auto">Is Active?</label>
                        <div class="col-xl-8 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxPriorityIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="PriorityActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenPriorityID" hidden />
    <script type="module" src="/App/Features/Maintenance/Tickets/Priority/PriorityCatalog.js"></script>
</asp:Content>
