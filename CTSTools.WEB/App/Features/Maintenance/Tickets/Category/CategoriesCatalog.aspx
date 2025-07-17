<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="CategoriesCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.Tickets.Category.CategoriesCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CategoryCatalog.css" rel="stylesheet" />
    <div>
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Tickets</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Category</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">

                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a class="btn btn-success mb-1" id="AddNewCategoryButton" data-bs-toggle="modal" data-bs-target="#AddNewCategoryModal"><i class="fa-solid fa-circle-plus"></i> Category</a>
                        </div>
                        <div id="dxCategoryGrid"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Add new item Modal -->
    <div class="modal fade" id="AddNewCategoryModal" tabindex="-1" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 id="CategoryModalTitle" class="modal-title fs-5">Add Category</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="btnCloseCategoryModal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                        <div class="panel-body">
                            <div class="row mb-15px">
                                <div class="col-md-12">
                                    <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxCategoryNameTextBox"></div>
                                        <div class="invalid-feedback" id="CategoryNameValidation"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Description</label>
                                <div class="col-md-12">
                                    <div id="dxCategoryDescriptionTextArea"></div>
                                    <div class="invalid-feedback" id="CategoryDescriptionValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">SupportGroup (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxCategorySupportGroupSelectBox"></div>
                                    <div class="invalid-feedback" id="CategorySupportGroupValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Parent</label>
                                <div class="col-md-12">
                                    <div id="dxCategoryParentSelectBox"></div>
                                    <div class="invalid-feedback" id="CategoryParentValidation"></div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-md-3">Is Active?</label>
                                <div class="col-md-9">
                                    <div class="mt-2 mb-2">
                                        <div type="text" id="dxCategoryIsActiveCheckBox"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <div class="d-flex justify-content-end">
                        <div id="CategoryActionButtons"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenCategoryID" hidden />
    <input type="hidden" id="hiddenParentCategoryID" hidden />

    <script type="module" src="/App/Features/Maintenance/Tickets/Category/CategoryCatalog.js?v=1"></script>
</asp:Content>

