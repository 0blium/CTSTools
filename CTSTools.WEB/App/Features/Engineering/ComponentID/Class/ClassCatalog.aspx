<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ClassCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.Class.ClassCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
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
                <h1 class="page-header">Classes & Sub Classes</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-12">
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
                    <div class="tab-pane fade active show" id="ClassTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <div class="col-md-12 ">
                                            <a id="NewClassBtn" href="#SaveClassRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>Class</a>
                                        </div>
                                        <div id="dxClassGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade  " id="SubClassTab" role="tabpanel">
                        <div class="panel panel-inverse">
                            <div class="panel-body">
                                <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#SubClassModal" id="NewSubClassBtn"><i class="fa-solid fa-circle-plus"></i>
                                    Sub Class</a>
                                <%--<a class="btn btn-success mb-2 float-end" id="UploadMassiveSubClassModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelSubClassModal" hidden><i class="fa-solid fa-file-import"></i>Excel</a>--%>
                                <div id="dxSubClassGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal save record -->
                <div class="modal fade" id="SaveClassRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
                    <div class="modal-dialog ">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 id="ClassModalTitle" class="modal-title fs-5"></h4>
                                <button type="button" id="btnCloseClassModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
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
                                        <div id="dxCodeTextBox"></div>
                                        <div class="invalid-feedback" id="CodeValidation"></div>
                                    </div>
                                </div>
                                <div class="row mb-15px">
                                    <label class="form-label col-form-label col-xl-3 col-md-12">Part Type (<span class="text-danger">*</span>)</label>
                                    <div class="col-xl-9 col-md-12">
                                        <div id="dxPartTypeSelectBox"></div>
                                        <div class="invalid-feedback" id="PartTypeValidation"></div>
                                    </div>
                                </div>
                                <div class="row mb-15px" id="ComponentTypeGroup" hidden>
                                    <label class="form-label col-form-label col-xl-3 col-md-12">Component Type (<span class="text-danger">*</span>)</label>
                                    <div class="col-xl-9 col-md-12">
                                        <div id="dxComponentTypeSelectBox"></div>
                                        <div class="invalid-feedback" id="ComponentTypeValidation"></div>
                                    </div>
                                </div>
                                <div class="row mb-15px">
                                    <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                                    <div class="col-xl-9 col-md-12">
                                        <div id="dxClassDescriptionTextArea"></div>
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

                <!-- Sub Class Modal save record -->
                <div class="modal fade" id="SubClassModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 id="SubClassModalTitle" class="modal-title fs-5"></h4>
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
                                        <div id="dxSubClassPartTypeSelectBox"></div>
                                        <div class="invalid-feedback" id="SubClassPartTypeValidation"></div>
                                    </div>
                                </div>
                                <div class="row mb-15px" id="SubClassComponentTypeGroup" hidden>
                                    <label class="form-label col-form-label col-xl-3 col-md-12">Component Type (<span class="text-danger">*</span>)</label>
                                    <div class="col-xl-9 col-md-12">
                                        <div id="dxSubClassComponentTypeSelectBox"></div>
                                        <div class="invalid-feedback" id="SubClassComponentTypeValidation"></div>
                                    </div>
                                </div>
                                <div class="row mb-15px">
                                    <label class="form-label col-form-label col-xl-3 col-md-12">Class (<span class="text-danger">*</span>)</label>
                                    <div class="col-xl-9 col-md-12">
                                        <div id="dxSubClassClassSelectBox"></div>
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

            </div>
        </div>
    </div>


    <input type="hidden" id="hiddenClassID" hidden />
    <input type="hidden" id="hiddenValueID" hidden />
    <input type="hidden" id="hiddenValueLinkID" hidden />
    <input type="hidden" id="hiddenSubClassID" hidden />
    <input type="hidden" id="hiddenSubClassValueLinkID" hidden />    
    <input type="hidden" id="hiddenSubClassValueID" hidden />
    <script type="module" src="/App/Features/Engineering/ComponentID/Class/ClassCatalog.js"></script>
</asp:Content>
