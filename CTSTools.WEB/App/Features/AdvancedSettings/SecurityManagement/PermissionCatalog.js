
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreatePermission, UpdatePermission, DeletePermission, GetDXPermissionDataSource } from './Permission/Permission_Service.js'
import { CreateAction, UpdateAction, DeleteAction, GetDXActionDataSource } from './Action/Action_Service.js'
import { GetDXModuleDataSource } from '../SecurityManagement/Module/Module_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializePermissionCatalogControls();
    InitializeActionCatalogControls();
});
//#region Permission
async function InitializePermissionCatalogControls() {
    $("#dxPermissionNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxPermissionModuleSelectBox").dxSelectBox({
        dataSource: await GetDXModuleDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxPermissionDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxPermissionActionSelectBox").dxSelectBox({
        dataSource: await GetDXActionDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxPermissionIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxPermissionGrid").dxDataGrid({
        dataSource: await GetDXPermissionDataSource(),
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
        grouping: {
            autoExpandAll: true,
        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "RoleCatalog",
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
        }, headerFilter: {
            visible: true
        },
        onSelectionChanged: function (data) {
            let _permissionData = data.selectedRowsData[0];
            if (_permissionData != null) {
                PermissionActionButtons("Update");
                PopulatePermissionFields(_permissionData);
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
                                    { text: "Edit item", icon: "fa fa-pen-to-square text-info", value: 1 },
                                    { text: "Delete item", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#PermissionModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenPermissionID').value = options.data.ID;
                                    ShowPermissionDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Module", dataField: "ModuleName", sortIndex: 0, sortOrder: "asc" },
                { caption: "Description", dataField: "Description" },
                { caption: "Action", dataField: "ActionName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    document.getElementById("btnClosePermissionModal").addEventListener("click", ClearPermissionFields);
    PermissionActionButtons("Save");
}
function PermissionActionButtons(Action) {
    $("#PermissionActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("PermissionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreatePermissionButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearPermissionButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearPermissionButton").addEventListener("click", ClearPermissionFields);
        document.getElementById("CreatePermissionButton").addEventListener("click", CreatePermission_Global);
    }
    else {
        // Update
        document.getElementById("PermissionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdatePermissionButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearPermissionButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearPermissionButton").addEventListener("click", ClearPermissionFields);
        document.getElementById("UpdatePermissionButton").addEventListener("click", UpdatePermission_Global);
    }
}
function ClearPermissionFields() {
    $('#PermissionModal').modal('hide');
    PermissionActionButtons("Save");
    $('#hiddenPermissionID').val("");
    $("#dxPermissionNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxPermissionModuleSelectBox").dxSelectBox("instance").reset();
    $("#dxPermissionDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxPermissionActionSelectBox").dxSelectBox("instance").reset();
    $("#dxPermissionIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxPermissionGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxPermissionGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxPermissionGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxPermissionGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulatePermissionFields(data) {
    $('#hiddenPermissionID').val(data.ID);
    $("#dxPermissionNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxPermissionModuleSelectBox").dxSelectBox("instance").option("value", data.ModuleID);
    $("#dxPermissionDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxPermissionActionSelectBox").dxSelectBox("instance").option("value", data.ActionID);
    $("#dxPermissionIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
function GetPermissionDTO() {
    let _permissionDTO = {
        ID: $('#hiddenPermissionID').val(),
        Name: $("#dxPermissionNameTextBox").dxTextBox("instance").option("value"),
        ModuleID: $("#dxPermissionModuleSelectBox").dxSelectBox("instance").option("value"),
        Description: $("#dxPermissionDescriptionTextArea").dxTextArea("instance").option("value"),
        ActionID: $("#dxPermissionActionSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxPermissionIsActiveCheckBox").dxCheckBox("instance").option("value"),

    }
    return _permissionDTO;
}
async function ShowPermissionDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this permission',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeletePermission_Global();
    } else {
        ClearPermissionFields();
    }
}
async function CreatePermission_Global() {
    await dxLoadPanel.show();
    const _permissionDTO = GetPermissionDTO();
    const _validation_ResultDTO = await CreatePermission(_permissionDTO);
    if (_validation_ResultDTO.Result) {
        ClearPermissionFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdatePermission_Global() {
    await dxLoadPanel.show();
    const _permissionDTO = GetPermissionDTO();
    const _validation_ResultDTO = await UpdatePermission(_permissionDTO);
    if (_validation_ResultDTO.Result) {
        ClearPermissionFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeletePermission_Global() {
    await dxLoadPanel.show();
    const _permissionDTO = GetPermissionDTO();
    const _validation_ResultDTO = await DeletePermission(_permissionDTO);
    if (_validation_ResultDTO.Result) {
        ClearPermissionFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#endregion
//#region Action
async function InitializeActionCatalogControls() {
    $("#dxActionNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });

    $("#dxActionDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });

    $("#dxActionIsActiveCheckBox").dxCheckBox({
        value: true,
    });

    $("#dxActionGrid").dxDataGrid({
        dataSource: await GetDXActionDataSource(),
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
        paging: {
            pageSize: 10,
        },
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
            fileName: "ActionCatalog",
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
        }, headerFilter: {
            visible: true
        },
        onSelectionChanged: function (data) {
            let _actionData = data.selectedRowsData[0];
            if (_actionData != null) {
                ActionActionButtons("Update");
                PopulateActionFields(_actionData);
            }
        },
        columns:
            [
                {
                    caption: "Delete",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 70,
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenActionID").val(options.data.ID);
                                ShowActionDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    ActionActionButtons("Save");

}
function ActionActionButtons(Action) {
    $("#ActionActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("ActionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateActionButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateActionButton").addEventListener("click", CreateAction_Global);
    }
    else {
        // Update
        document.getElementById("ActionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearActionButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateActionButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearActionButton").addEventListener("click", ClearActionFields);
        document.getElementById("UpdateActionButton").addEventListener("click", UpdateAction_Global);
    }
}
function ClearActionFields() {
    ActionActionButtons("Save");
    $('#hiddenActionID').val("");
    $("#dxActionNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxActionDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxActionIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxActionGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxActionGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxActionGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxActionGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateActionFields(data) {
    $('#hiddenActionID').val(data.ID);
    $("#dxActionNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxActionDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxActionIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
function GetActionDTO() {
    let _actionDTO = {
        ID: $('#hiddenActionID').val(),
        Name: $("#dxActionNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxActionDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxActionIsActiveCheckBox").dxCheckBox("instance").option("value"),

    }
    return _actionDTO;
}
async function ShowActionDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this action',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteAction_Global();
    } else {
        ClearActionFields();
    }
}
async function CreateAction_Global() {
    await dxLoadPanel.show();
    const _actionDTO = GetActionDTO();
    const _validation_ResultDTO = await CreateAction(_actionDTO);
    if (_validation_ResultDTO.Result) {
        ClearActionFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateAction_Global() {
    await dxLoadPanel.show();
    const _actionDTO = GetActionDTO();
    const _validation_ResultDTO = await UpdateAction(_actionDTO);
    if (_validation_ResultDTO.Result) {
        ClearActionFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteAction_Global() {
    await dxLoadPanel.show();
    const _actionDTO = GetActionDTO();
    const _validation_ResultDTO = await DeleteAction(_actionDTO);
    if (_validation_ResultDTO.Result) {
        ClearActionFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#endregion
//Reload select box for related fields
async function ReloadRelated() {
    $("#dxPermissionActionSelectBox").dxSelectBox("instance").option("dataSource", await GetDXActionDataSource());
    $("#dxPermissionActionSelectBox").dxSelectBox("instance").option("dataSource", await GetDXActionDataSource());

}