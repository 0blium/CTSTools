<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="UnitOfMeasureCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.UnitOfMeasure.UnitOfMeasureCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Unit Of Measure</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Unit Of Measure</h1>
            </div>
            <!-- END page-header -->
        </div>
        <!-- BEGIN UnitOfMeasure Catalog -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">                    
                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a id="NewUnitOfMeasureBtn" href="#SaveUnitOfMeasureRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>Unit of measure</a>
                        </div>
                        <div id="dxUnitOfMeasureGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END UnitOfMeasure Catalog -->
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveUnitOfMeasureRecordModal"  tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="UnitOfMeasureModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseUnitOfMeasureModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-md-12">
                            <div id="dxUnitOfMeasureNameTextBox"></div>
                            <div class="invalid-feedback" id="UnitOfMeasureNameValidation"></div>
                        </div>
                    </div>
                    
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Description</label>
                        <div class="col-md-12">
                            <div id="dxUnitOfMeasureDescription"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-auto">Is Active?</label>
                        <div class="col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxUnitOfMeasureIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <div class="row" id="UnitOfMeasureActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenUnitOfMeasureID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/UnitOfMeasure/UnitOfMeasureCatalog.js"></script>
</asp:Content>
