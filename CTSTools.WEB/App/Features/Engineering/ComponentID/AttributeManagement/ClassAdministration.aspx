<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ClassAdministration.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.ClassAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Component ID</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;"></a></li>

                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Classes & Sub Classes </h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Status Catalog -->
        <div class="row">
            <div class="col-12">
                <ul class="nav nav-pills mb-2" role="tablist">
                    <li class="nav-item" role="presentation">
                        <a href="#ClassTab" data-bs-toggle="tab" id="ClassPill" class="nav-link active" aria-selected="true" tabindex="-1" role="tab">
                            <span class="d-sm-none">Class</span>
                            <span class="d-sm-block d-none">Class</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#SubClassTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" role="tab">
                            <span class="d-sm-none">Sub Class</span>
                            <span class="d-sm-block d-none">Sub Class</span>
                        </a>
                    </li>

                </ul>
                <div class="tab-content rounded-0 m-0">
                    <div class="tab-pane fade" id="SubClassTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#SubClassModal" id="SubClassButton"><i class="fa-solid fa-circle-plus"></i>
                                            Sub Class</a>
                                        <div id="dxSubClassGrid"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade active show " id="ClassTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#ClassModal" id="ClassButton"><i class="fa-solid fa-circle-plus"></i>Class</a>
                                        <a class="btn btn-success mb-2 float-end" id="UploadExcelModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelClassModal"><i class="fa-solid fa-file-import"></i> Excel</a>
                                        <div id="dxClassGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <!-- END Status Catalog -->
    </div>
    <div class="modal fade" id="ClassModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="">Class Form</h1>
                    <button type="button" id="ClassCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxClassNameTextBox"></div>
                            <div class="invalid-feedback" id="ClassNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Code (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxClassCodeTextBox"></div>
                            <div class="invalid-feedback" id="ClassCodeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Part Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxClassPartTypeLookup"></div>
                            <div class="invalid-feedback" id="ClassPartTypeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="ComponentTypeGroup">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Component Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxClassComponentTypeLookup"></div>
                            <div class="invalid-feedback" id="ClassComponentTypeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxClassDescriptionTextArea"></div>
                            <div class="invalid-feedback" id="ClassDescriptionValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxClassIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="ClassActionButtons"></div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="SubClassModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Sub Class Form</h1>
                    <button type="button" id="SubClassCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSubClassNameTextBox"></div>
                            <div class="invalid-feedback" id="SubClassNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Code (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSubClassCodeTextBox"></div>
                            <div class="invalid-feedback" id="SubClassCodeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Part Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSubClassPartTypeLookup"></div>
                            <div class="invalid-feedback" id="SubClassPartTypeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="SubClassComponentTypeGroup" hidden>
                        <label class="form-label col-form-label col-xl-3 col-md-12">Component Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSubClassComponentTypeLookup"></div>
                            <div class="invalid-feedback" id="SubClassComponentTypeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Class (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSubClassClassLookup"></div>
                            <div class="invalid-feedback" id="SubClassClassValidation"></div>
                        </div>
                    </div>


                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSubClassDescriptionTextArea"></div>
                            <div class="invalid-feedback" id="SubClassDescriptionValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSubClassIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row" id="SubClassActionButtons"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="UploadExcelClassModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5">Upload file excel</h1>
                <button type="button" id="UploadExcelClassCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div class="row mb-15px">
                    <div id="dxClassFileUploader"></div>
                </div>
                <div id="successClassMessage" class="alert alert-success" hidden></div>
                <div id="errorClassMessages" class="alert alert-danger" hidden></div>
            </div>
            <div class="modal-footer">
                <button class="btn btn-secondary float-end" id="ClearClassExcelModalButton" type="button">Clear</button>
            </div>
        </div>
    </div>
</div>

    <input type="hidden" id="hiddenClassID" hidden />
    <input type="hidden" id="hiddenClassValueLinkID" hidden />
    <input type="hidden" id="hiddenSubClassID" hidden />
    <input type="hidden" id="hiddenSubClassValueLinkID" hidden />
    <script type="module" src="/App/Features/Engineering/ComponentID/AttributeManagement/ClassAdministration.js"></script>

</asp:Content>
