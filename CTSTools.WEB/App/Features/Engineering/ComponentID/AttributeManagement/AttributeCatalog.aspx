<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="AttributeCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.AttributeCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- BEGIN breadcrumb -->
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">ComponentID</a></li>
                <li class="breadcrumb-item active"></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Attributes</h1>
        </div>
        <!-- END page-header -->
    </div>
    <div class="panel panel-inverse">
        <div class="panel-body">
            <div class="row" style="margin-bottom: 30px">
                <div class="col-lg-4">
                    <label class="form-label">Attribute (<span class="text-danger">*</span>)</label>
                    <div class="col-xl-9 col-md-12">
                        <div id="dxAttributeSelectBox"></div>
                    </div>
                    <div style="margin-top: 10px">
                        <a href="javascript:;" data-bs-toggle="modal" data-bs-target="#MailGroupMemberModal">Attribute is not there? Create it here. </a>
                    </div>
                </div>
                <div class="col-lg-8">
                    <a class="btn btn-success mb-2 float-end" id="UploadExcelValueModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelValueModal"><i class="fa-solid fa-file-import"></i> Excel</a>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <a id="valuemodalbutton" class="btn btn-success mb-2" style="visibility: hidden" data-bs-toggle="modal" data-bs-target="#ValueModal"></a>
                    <div id="dxValueGrid"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- Value modal Start--%>
    <div class="modal fade" id="ValueModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="ValueModalTitle"></h1>
                    <button id="ValueModalCloseButton" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxValueNameTextBox"></div>
                            <div class="invalid-feedback" id="ValueNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Code (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxCodeTextBox"></div>
                            <div class="invalid-feedback" id="CodeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxValueDescriptionTextArea"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxValueIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="ValueActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- Attribute List modal End--%>
    <div class="modal fade" id="MailGroupMemberModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Attribute List</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-25px">
                        <div class="col-md-12 col-lg-4">
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-9 col-md-12">
                                    <div id="dxAttributeNameTextBox"></div>
                                    <div class="invalid-feedback" id="AttributeNameValidation"></div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                                <div class="col-xl-9 col-md-12">
                                    <div id="dxAttributeDescriptionTextArea"></div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-4 col-md-auto">Has Multiple Options?</label>
                                <div class="col-xl-8 col-md-auto">
                                    <div class="mt-2 mb-2">
                                        <div type="text" id="dxHasMultipleOptionsCheckBox"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-4 col-md-auto">Is Active?</label>
                                <div class="col-xl-8 col-md-auto">
                                    <div class="mt-2 mb-2">
                                        <div type="text" id="dxAttributeIsActiveCheckBox"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="AttributeActionButtons"></div>
                        </div>
                        <div class="col-md-12 col-lg-8">
                            <div class="panel-body row">
                                <div type="text" id="dxAttributeGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal update excel -->
    <div class="modal fade" id="UploadExcelAttributeModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Upload file excel</h1>
                    <button type="button" id="UploadExcelAttributeCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <div id="dxAttributeFileUploader"></div>
                    </div>
                    <div id="successAttributeMessage" class="alert alert-success" hidden></div>
                    <div id="errorAttributeMessages" class="alert alert-danger" hidden></div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary float-end" id="ClearExcelAttributeButton" type="button">Clear</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Value update excel -->
    <div class="modal fade" id="UploadExcelValueModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Upload file excel</h1>
                    <button type="button" id="UploadExcelValueCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <div id="dxValueFileUploader"></div>
                    </div>
                    <div id="successValueMessage" class="alert alert-success" hidden></div>
                    <div id="errorValueMessages" class="alert alert-danger" hidden></div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary float-end" id="ClearExcelValueButton" type="button">Clear</button>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenAttributeID" hidden />
    <input type="hidden" id="hiddenValueID" hidden />
    <script type="module" src="/App/Features/Engineering/ComponentID/AttributeManagement/AttributeCatalog.js"></script>
</asp:Content>
