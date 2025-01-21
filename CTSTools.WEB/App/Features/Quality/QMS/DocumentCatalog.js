import { GetDXDocumentDataSource, CreateDocument, UpdateDocument, DeleteDocument } from './Document/Document_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../common/utils/response.js'
import { dxLoadPanel } from '../../../common/components/dxloadpanel.js'
import { GetDXDepartmentDataSource } from '../../advancedsettings/locationmanagement/department/department_service.js'
import { GetDXFacilityDataSource } from '../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXDocumentTypeDataSource } from './documenttype/documenttype_service.js'
import { GetDXUserDataSource } from '../../advancedsettings/usermanagement/user/user_service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeDocumentCatalogControls();
});

async function InitializeDocumentCatalogControls() {

    $("#dxDocumentGrid").dxDataGrid({
        dataSource: await GetDXDocumentDataSource({ GetTypeDTO: true, GetFacilityDTO: true, GetDepartmentDTO: true }),
        keyExpr: "ID",
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        columnMinWidth: 100,
        showRowLines: true,
        showColumnLines: false,
        showBorders: true,
        focusedRowEnabled: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: false,
        columnAutoWidth: true,
        groupPanel: {
            visible: true
        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "DocumentCatalog",
            allowExportSelectedData: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: true,
            placeholder: "Search...",
            width: 300
        },
        sorting: {
            mode: "multiple"
        },
        selection: {
            mode: 'single'
        },
        headerFilter: {
            visible: true
        },

        columns:
            [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "View", icon: "fa-regular fa-eye text-primary", value: 1 },
                                    { text: "Edit", icon: "fa fa-pen-to-square text-info", value: 3 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 4 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                let _data = options.data;
                                switch (e.itemData.value) {
                                    case 1:
                                        location.href = "/App/Features/Quality/QMS/DocumentRevisionCatalog.aspx?DocumentID=" + _data.ID;
                                        break;
                                    case 3:
                                        DocumentActionButtons("Update");
                                        PopulateDocumentFields(_data);
                                        $('#SaveDocumentRecordModal').modal('show');
                                        break;
                                    case 4:
                                        document.getElementById('hiddenDocumentID').value = options.data.ID;
                                        ShowDeleteQuestion();
                                        break;
                                    default:
                                        break;
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Number", dataField: "Number" },,
                { caption: "Owner", dataField: "OwnerName" },
                { caption: "Department", dataField: "DepartmentName" },
                { caption: "Type", dataField: "TypeName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Last Revision", dataField: "LastRevision" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
            ],
    });

    $("#dxDocumentNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxDocumentNumberTextBox").dxTextBox({
        placeholder: 'Type number...'
    });

    $("#dxDocumentOwnerSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentTypeSelectBox").dxSelectBox({
        dataSource: await GetDXTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Facility...",
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                let _departmentDTO = {
                    IsActive: true,
                    FacilityID: e.value
                }
                $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").reset();
                $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource(_departmentDTO));
            } else {
                $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        },
    });

    $("#dxDocumentDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Department...",
    });
    document.getElementById("btnCloseDocumentModal").addEventListener("click", ClearDocumentFields);
    DocumentActionButtons("Save");
}

//#endregion

async function PopulateDocumentFields(data) {
    $("#hiddenDocumentID").val(data.ID);
    $("#dxDocumentNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxDocumentNumberTextBox").dxTextBox("instance").option("value", data.Number);
    $("#dxDocumentTypeSelectBox").dxSelectBox("instance").option("value", data.TypeID);
    $("#dxDocumentOwnerSelectBox").dxSelectBox("instance").option("value", data.OwnerID);
    await $("#dxDocumentFacilitySelectBox").dxSelectBox("instance").option("value", data.DepartmentDTO.FacilityID);
    await $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("value", data.DepartmentID);
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the Document, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDocument_Global();
    } else {
        ClearDocumentFields();
    }
}

function DocumentActionButtons(Action) {
    $("#DocumentActionButtons").empty();
    document.getElementById('DocumentModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewDocumentBtn").addEventListener("click", ClearDocumentFields);
        document.getElementById('DocumentModalTitle').innerText = 'Add Document Form'
        document.getElementById("DocumentActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateDocumentButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDocumentButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDocumentButton").addEventListener("click", ClearDocumentFields);
        document.getElementById("CreateDocumentButton").addEventListener("click", CreateDocument_Global);
    }
    else {
        // Update
        document.getElementById('DocumentModalTitle').innerText = 'Update Document Form'
        document.getElementById("DocumentActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateDocumentButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDocumentButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDocumentButton").addEventListener("click", ClearDocumentFields);
        document.getElementById("UpdateDocumentButton").addEventListener("click", UpdateDocument_Global);
    }
}
function ClearDocumentFields() {
    $('#SaveDocumentRecordModal').modal('hide');
    DocumentActionButtons("Save");
    $("#hiddenDocumentID").val("");
    $("#dxDocumentNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxDocumentNumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxDocumentTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentFacilitySelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxDocumentGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDocumentGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}

function GetDocumentDTO() {
    let _documentDTO = {
        ID: $("#hiddenDocumentID").val(),
        Name: $("#dxDocumentNameTextBox").dxTextBox("instance").option("value"),
        Number: $("#dxDocumentNumberTextBox").dxTextBox("instance").option("value"),
        DepartmentID: $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("value"),
        TypeID: $("#dxDocumentTypeSelectBox").dxSelectBox("instance").option("value"),
        OwnerID: $("#dxDocumentOwnerSelectBox").dxSelectBox("instance").option("value"),
    }
    return _documentDTO;
}
//#endregion

//#region Document CRUD Functions
async function CreateDocument_Global() {
    await dxLoadPanel.show();
    const _documentDTO = GetDocumentDTO();
    const _validation_ResultDTO = await CreateDocument(_documentDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentGrid").dxDataGrid("instance").refresh();
        ClearDocumentFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDocument_Global() {
    await dxLoadPanel.show();
    const _documentDTO = GetDocumentDTO();
    const _validation_ResultDTO = await UpdateDocument(_documentDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentGrid").dxDataGrid("instance").refresh();
        ClearDocumentFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDocument_Global() {
    await dxLoadPanel.show();
    const _documentDTO = GetDocumentDTO();
    const _validation_ResultDTO = await DeleteDocument(_documentDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentGrid").dxDataGrid("instance").refresh();
        ClearDocumentFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    ClearDocumentFields();
    dxLoadPanel.hide();
}

const GetDXTypeDataSource_Global = () => {
    let _typeDTO = { IsActive: true }
    return GetDXDocumentTypeDataSource(_typeDTO)
}

//#endregion

//Reload select box for related fields
async function ReloadLocationsSelectBox() {
    $("#dxDocumentFacilitySelectBox").dxSelectBox("instance").option("dataSource", await GetDXFacilityDataSource());
    $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource());
}