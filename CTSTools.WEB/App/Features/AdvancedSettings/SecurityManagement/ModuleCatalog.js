import { GetDXModuleDataSource, CreateModule, UpdateModule, DeleteModule, CreateModuleSetUp } from './Module/Module_Service.js';
import { GetDXActionDataSource } from './Action/Action_Service.js';
import { GetDXRoleDataSource } from './Role/Role_Service.js';
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js';
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js';

//#region Module Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeModuleCatalogControls();
    InitializeModuleSetupControls();
});
async function InitializeModuleCatalogControls() {
    $("#dxModuleIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxModulePanelNameTextBox").dxTextBox({
        placeholder: 'Type panel name...'
    });
    $("#dxModuleNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxModuleDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxModuleGrid").dxDataGrid({
        dataSource: await GetDXModuleDataSource_Global({ IsActive: true }),
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
            fileName: "ModuleCatalog",
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
        onSelectionChanged: function (data) {
            let _moduleData = data.selectedRowsData[0];
            if (_moduleData != null) {
                ModuleActionButtons("Update");
                PopulateModuleFields(_moduleData);
            }
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
                                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 1 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveModuleRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenModuleID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
            ],
    });
    document.getElementById("btnCloseModuleModal").addEventListener("click", ClearModuleFields);
    ModuleActionButtons("Save");
}
async function PopulateModuleFields(data) {
    $("#hiddenModuleID").val(data.ID);
    $("#dxModuleNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxModuleDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxModuleIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the module, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteModule_Global();
    } else {
        ClearModuleFields();
    }
}
function ModuleActionButtons(Action) {
    $("#ModuleActionButtons").empty();
    document.getElementById('ModuleModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewModuleBtn").addEventListener("click", ClearModuleFields);
        document.getElementById('ModuleModalTitle').innerText = 'Add Module'
        document.getElementById("ModuleActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateModuleButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearModuleButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearModuleButton").addEventListener("click", ClearModuleFields);
        document.getElementById("CreateModuleButton").addEventListener("click", CreateModule_Global);
    }
    else {
        // Update
        document.getElementById('ModuleModalTitle').innerText = 'Update Module'
        document.getElementById("ModuleActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateModuleButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearModuleButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearModuleButton").addEventListener("click", ClearModuleFields);
        document.getElementById("UpdateModuleButton").addEventListener("click", UpdateModule_Global);
    }
}
function ClearModuleFields() {
    $('#SaveModuleRecordModal').modal('hide');
    ModuleActionButtons("Save");
    $("#hiddenModuleID").val("");
    $("#dxModuleNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxModuleDescription").dxTextArea("instance").option("value", '');
    $("#dxModuleIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxModuleGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxModuleGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetModuleDTO() {
    let _moduleDTO = {
        ID: $("#hiddenModuleID").val(),
        Name: $("#dxModuleNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxModuleDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxModuleIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _moduleDTO;
}
//#endregion

//#region Module CRUD Functions
async function CreateModule_Global() {
    await dxLoadPanel.show();
    const _moduleDTO = GetModuleDTO();
    const _validation_ResultDTO = await CreateModule(_moduleDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxModuleGrid").dxDataGrid("instance").refresh();
        ClearModuleFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateModule_Global() {
    await dxLoadPanel.show();
    const _moduleDTO = GetModuleDTO();
    const _validation_ResultDTO = await UpdateModule(_moduleDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxModuleGrid").dxDataGrid("instance").refresh();
        ClearModuleFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteModule_Global() {
    await dxLoadPanel.show();
    const _moduleDTO = GetModuleDTO();
    const _validation_ResultDTO = await DeleteModule(_moduleDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxModuleGrid").dxDataGrid("instance").refresh();
        ClearModuleFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearModuleFields();
    dxLoadPanel.hide();
}
const GetDXModuleDataSource_Global = () => {
    return GetDXModuleDataSource()
}
//#endregion


//#region Setup
async function InitializeModuleSetupControls() {
    console.log("sdfdsf")
    $("#dxModuleSetupIsActiveCheckBox").dxCheckBox({
        value: true
    });
    //$("#dxModulePermissionReadCheckBox").dxCheckBox({
    //    value: true, text: 'Read',
    //});
    //$("#dxModulePermissionCreateCheckBox").dxCheckBox({
    //    value: true, text: 'Create',
    //});
    //$("#dxModulePermissionUpdateCheckBox").dxCheckBox({
    //    value: true, text: 'Update',
    //});
    //$("#dxModulePermissionDeleteCheckBox").dxCheckBox({
    //    value: true, text: 'Delete',
    //});
    $("#dxModulePanelNameTextBox").dxTextBox({
        placeholder: 'Type panel name...'
    });
    $("#dxModuleSetupActionsTagBox").dxTagBox({
        dataSource: await GetDXActionDataSource(),
        displayExpr: "Name",
        deferRendering: false,
        valueExpr: "ID",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'useButtons',
        popupWidth: 450,
    });
    $("#dxModuleSetupRoleTagBox").dxTagBox({
        dataSource: await GetDXRoleDataSource(),
        displayExpr: "Name",
        deferRendering: false,
        valueExpr: "ID",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'useButtons',
        popupWidth: 450,
    });
    $("#dxModuleSetupNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxModuleSetupDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxModuleSetupGrid").dxDataGrid({
        dataSource: await GetDXModuleDataSource_Global({ IsActive: true }),
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
            fileName: "ModuleCatalog",
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
        onSelectionChanged: function (data) {
            let _moduleData = data.selectedRowsData[0];
            if (_moduleData != null) {
            }
        },
        columns:
            [
                //{
                //    caption: "Options",
                //    alignment: "center",
                //    allowFiltering: false,
                //    allowSorting: false,
                //    cellTemplate: function (container, options) {
                //        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                //            items: [{
                //                icon: "fa-solid fa-ellipsis-vertical text-dark",
                //                items: [
                //                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 1 },
                //                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 2 },
                //                ]
                //            }],
                //            showFirstSubmenuMode: 'onClick',
                //            hideSubmenuOnMouseLeave: true,
                //            onItemClick: function (e) {
                //                if (e.itemData.value == 1) {
                //                    //$('#SaveModuleRecordModal').modal('show');
                //                }
                //                else if (e.itemData.value == 2) {
                //                //    document.getElementById('hiddenModuleID').value = options.data.ID;
                //                //    ShowDeleteQuestion();
                //                }
                //            },
                //        });
                //    }
                //},
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By ", dataField: "LastUpdateByName" },
            ],
    });
    document.getElementById("btnCloseModuleSetupModal").addEventListener("click", ClearModuleSetupFields);
    ModuleSetupActionButtons("Save");
}


function ModuleSetupActionButtons(Action) {
    $("#ModuleSetupActionButtons").empty();
    document.getElementById('ModuleSetUpModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewModuleSetUpBtn").addEventListener("click", ClearModuleSetupFields);
        document.getElementById('ModuleSetUpModalTitle').innerText = 'Add Module'
        document.getElementById("ModuleSetupActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateModuleSetupButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearModuleSetupButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearModuleSetupButton").addEventListener("click", ClearModuleSetupFields);
        document.getElementById("CreateModuleSetupButton").addEventListener("click", CreateModuleSetUp_Global);
    }
    else {
    }
}

function ClearModuleSetupFields() {
    $('#SaveModuleSetupModal').modal('hide');
    ModuleSetupActionButtons("Save");
    $("#dxModuleSetupNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxModuleSetupDescription").dxTextArea("instance").option("value", '');
    $("#dxModuleSetupIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxModuleSetupRoleTagBox").dxTagBox("instance").reset();
    $("#dxModuleSetupActionsTagBox").dxTagBox("instance").reset();
    let keys = $("#dxModuleSetupGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxModuleSetupGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}

function GetModuleSetupDTO() {
    let _moduleDTO = {
        Name: $("#dxModuleSetupNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxModuleSetupDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxModuleSetupIsActiveCheckBox").dxCheckBox("instance").option("value"),
        RoleIDArray: $("#dxModuleSetupRoleTagBox").dxTagBox("instance").option("value"),
        ActionIDArray: $("#dxModuleSetupActionsTagBox").dxTagBox("instance").option("value"),
    }
    return _moduleDTO;
}

async function CreateModuleSetUp_Global() {
    await dxLoadPanel.show();
    const _moduleDTO = GetModuleSetupDTO();
    const _validation_ResultDTO = await CreateModuleSetUp(_moduleDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxModuleSetupGrid").dxDataGrid("instance").refresh();
        ClearModuleSetupFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#endregion