<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Quality.QMS.ProductCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
    <!-- BEGIN breadcrumb -->
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-end">
                <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
                <li class="breadcrumb-item"><a href="javascript:;"></a></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Products</h1>
        </div>
        <!-- END page-header -->
    </div>
    <!-- BEGIN Product Catalog -->
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="col-md-12 ">
                        <a id="NewProductBtn" href="#SaveProductRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i> Product</a>
                    </div>
                    <div id="dxProductGrid"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- END Product Catalog -->
</div>
<!-- Modal save record -->
<div class="modal fade" id="SaveProductRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
    <div class="modal-dialog ">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="ProductModalTitle" class="modal-title fs-5"></h4>
                <button type="button" id="btnCloseProductModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
            </div>
            <div class="modal-body">
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                    <div class="col-md-12">
                        <div id="dxProductNameTextBox"></div>
                        <div class="invalid-feedback" id="ProductNameValidation"></div>
                    </div>
                </div>
                
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-md-12">Description</label>
                    <div class="col-md-12">
                        <div id="dxProductDescription"></div>
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-md-auto">Is Active?</label>
                    <div class="col-md-auto">
                        <div class="mt-2 mb-2">
                            <div type="text" id="dxProductIsActiveCheckBox"></div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="modal-footer">
                <div class="row" id="ProductActionButtons"></div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hiddenProductID" hidden />
<script type="module" src="/App/Features/Quality/QMS/ProductCatalog.js"></script>
</asp:Content>
