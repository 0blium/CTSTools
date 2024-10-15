<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DomainCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.UserManagement.DomainCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item">Advanced Settings</li>
                <li class="breadcrumb-item">Org. Management</li>
                <li class="breadcrumb-item"></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Domain Catalog</h1>
        </div>
        <!-- END page-header -->
    </div>
    <div class="row">
        <div class="col-xl-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="row ">
                        <div class="col-12">
                            <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#DomainModal"><i class="fa-solid fa-circle-plus"></i>  Domain</a>
                            <div id="dxDomainGrid"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="DomainModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Domain Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" id="DomainModalCloseButton"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-25px">
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">IP (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxDomainIPTextBox"></div>
                                <div class="invalid-feedback" id="DomainIPValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Facility (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxDomainFacilityLookup"></div>
                                <div class="invalid-feedback" id="DomainFacilityValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxDomainDescriptionTextArea"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                            <div class="col-xl-9 col-md-auto">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxDomainIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="DomainActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDomainID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/UserManagement/DomainCatalog.js"></script>

</asp:Content>
