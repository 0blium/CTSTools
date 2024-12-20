import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreateRole, UpdateRole, DeleteRole, GetDXRoleDataSource } from './Role/Role_Service.js'
import { CreateRoleType, UpdateRoleType, DeleteRoleType, GetDXRoleTypeDataSource } from './RoleType/RoleType_Service.js'
import { CreateRole_Permission, UpdateRole_Permission, DeleteRole_Permission, GetDXRole_PermissionDataSource } from './Role_Permission/Role_Permission_Service.js'
import { CreatePermission, UpdatePermission, DeletePermission, GetDXPermissionDataSource } from './Permission/Permission_Service.js'
import { CreateAction, UpdateAction, DeleteAction, GetDXActionDataSource } from './Action/Action_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeRoleCatalogControls();
    InitializeRoleTypeCatalogControls();
    InitializeRole_PermissionCatalogControls();
    InitializePermissionCatalogControls();
    InitializeActionCatalogControls();
});

//#region Role
async function InitializeRoleCatalogControls() {
    $("#dxRoleNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxRoleTypeSelectBox").dxSelectBox({
        dataSource: await GetDXRoleTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxRoleDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxRoleIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxRoleGrid").dxDataGrid({
        dataSource: await GetDXRoleDataSource(),
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
            let _roleData = data.selectedRowsData[0];
            if (_roleData != null) {
                RoleActionButtons("Update");
                PopulateRoleFields(_roleData);
            }
        },
        columns:
            [
                {
                    caption: "Delete",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#RoleModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenRoleID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenRoleID").val(options.data.ID);
                                ShowRoleDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Type", dataField: "RoleTypeName" },

                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    RoleActionButtons("Save");
}
function RoleActionButtons(Action) {
    $("#RoleActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("RoleActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateRoleButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateRoleButton").addEventListener("click", CreateRole_Global);
    }
    else {
        // Update
        document.getElementById("RoleActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearRoleButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateRoleButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearRoleButton").addEventListener("click", ClearRoleFields);
        document.getElementById("UpdateRoleButton").addEventListener("click", UpdateRole_Global);
    }
}
function ClearRoleFields() {
    $("#RoleModal").modal("hide");
    RoleActionButtons("Save");
    $('#hiddenRoleID').val("");
    $("#dxRoleTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxRoleNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxRoleDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxRoleIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxRoleGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxRoleGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxRoleGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxRoleGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateRoleFields(data) {
    $('#hiddenRoleID').val(data.ID);
    $("#dxRoleNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxRoleDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxRoleIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxRoleTypeSelectBox").dxSelectBox("instance").option("value", data.RoleTypeID);

}
function GetRoleDTO() {
    let _roleDTO = {
        ID: $('#hiddenRoleID').val(),
        Name: $("#dxRoleNameTextBox").dxTextBox("instance").option("value"),
        RoleTypeID: $("#dxRoleTypeSelectBox").dxSelectBox("instance").option("value"),
        Description: $("#dxRoleDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxRoleIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _roleDTO;
}
async function ShowRoleDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this role',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteRole_Global();
    } else {
        ClearRoleFields();
    }
}
async function CreateRole_Global() {
    await dxLoadPanel.show();
    const _roleDTO = GetRoleDTO();
    const _validation_ResultDTO = await CreateRole(_roleDTO);
    if (_validation_ResultDTO.Result) {
        ClearRoleFields();
        ReloadRolesSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateRole_Global() {
    await dxLoadPanel.show();
    const _roleDTO = GetRoleDTO();
    const _validation_ResultDTO = await UpdateRole(_roleDTO);
    if (_validation_ResultDTO.Result) {
        ClearRoleFields();
        ReloadRolesSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteRole_Global() {
    await dxLoadPanel.show();
    const _roleDTO = GetRoleDTO();
    const _validation_ResultDTO = await DeleteRole(_roleDTO);
    if (_validation_ResultDTO.Result) {
        ClearRoleFields();
        ReloadRolesSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion
//#region RoleType
async function InitializeRoleTypeCatalogControls() {
    $("#dxRoleTypeNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxRoleTypeDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxRoleTypeIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxRoleTypeGrid").dxDataGrid({
        dataSource: await GetDXRoleTypeDataSource(),
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
            fileName: "RoleTypeCatalog",
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
            let _roleTypeData = data.selectedRowsData[0];
            if (_roleTypeData != null) {
                RoleTypeActionButtons("Update");
                PopulateRoleTypeFields(_roleTypeData);
            }
        },
        columns:
            [
                {
                    caption: "Delete",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#RoleTypeModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenRoleTypeID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenRoleTypeID").val(options.data.ID);
                                ShowRoleTypeDeleteQuestion();
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
    RoleTypeActionButtons("Save");
}
function RoleTypeActionButtons(Action) {
    $("#RoleTypeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("RoleTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateRoleTypeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateRoleTypeButton").addEventListener("click", CreateRoleType_Global);
    }
    else {
        // Update
        document.getElementById("RoleTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearRoleTypeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateRoleTypeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearRoleTypeButton").addEventListener("click", ClearRoleTypeFields);
        document.getElementById("UpdateRoleTypeButton").addEventListener("click", UpdateRoleType_Global);
    }
}
function ClearRoleTypeFields() {
    $("#RoleTypeModal").modal("hide");
    RoleTypeActionButtons("Save");
    $('#hiddenRoleTypeID').val("");
    $("#dxRoleTypeNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxRoleTypeDescription").dxTextArea("instance").option("value", "");
    $("#dxRoleTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxRoleTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxRoleTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxRoleTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxRoleTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateRoleTypeFields(data) {
    $('#hiddenRoleTypeID').val(data.ID);
    $("#dxRoleTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxRoleTypeDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxRoleTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
function GetRoleTypeDTO() {
    let _roleTypeDTO = {
        ID: $('#hiddenRoleTypeID').val(),
        Name: $("#dxRoleTypeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxRoleTypeDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxRoleTypeIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _roleTypeDTO;
}
async function ShowRoleTypeDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this role type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteRoleType_Global();
    } else {
        ClearRoleTypeFields();
    }
}
async function CreateRoleType_Global() {
    await dxLoadPanel.show();
    const _roleTypeDTO = GetRoleTypeDTO();
    const _validation_ResultDTO = await CreateRoleType(_roleTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearRoleTypeFields();
        ReloadRolesSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateRoleType_Global() {
    await dxLoadPanel.show();
    const _roleTypeDTO = GetRoleTypeDTO();
    const _validation_ResultDTO = await UpdateRoleType(_roleTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearRoleTypeFields();
        ReloadRolesSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteRoleType_Global() {
    await dxLoadPanel.show();
    const _roleTypeDTO = GetRoleTypeDTO();
    const _validation_ResultDTO = await DeleteRoleType(_roleTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearRoleTypeFields();
        ReloadRolesSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion
//#region Role's Permissions
async function InitializeRole_PermissionCatalogControls() {
    $("#dxRole_PermissionRoleSelectBox").dxSelectBox({
        dataSource: await GetDXRoleDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxRole_PermissionPermissionTagBox").dxTagBox({
        dataSource: await GetDXPermissionDataSource(),
        displayExpr: "Name",
        deferRendering: false,
        valueExpr: "ID",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'instantly',
        popupWidth: 450,
    });
    $("#dxRole_PermissionIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxRole_PermissionGrid").dxDataGrid({
        dataSource: await GetDXRole_PermissionDataSource(),
        keyExpr: "ID",
        //remoteOperations: { groupPaging: true },
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
            let _role_PermissionData = data.selectedRowsData[0];
            if (_role_PermissionData != null) {
                Role_PermissionActionButtons("Update");
                PopulateRole_PermissionFields(_role_PermissionData);
            }
        },
        columns:
            [
                {
                    caption: "Delete",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenRole_PermissionID").val(options.data.ID);
                                ShowRole_PermissionDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Permission", dataField: "PermissionName" },
                { caption: "Module", dataField: "PermissionDTO.Module" },
                { caption: "Role", dataField: "RoleName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    Role_PermissionActionButtons("Save");
}
function Role_PermissionActionButtons(Action) {
    $("#Role_PermissionActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("Role_PermissionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateRole_PermissionButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateRole_PermissionButton").addEventListener("click", CreateRole_Permission_Global);
    }
    else {
        // Update
        document.getElementById("Role_PermissionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearRole_PermissionButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearRole_PermissionButton").addEventListener("click", ClearRole_PermissionFields);
    }
}
function ClearRole_PermissionFields() {
    Role_PermissionActionButtons("Save");
    $('#hiddenRole_PermissionID').val("");
    $("#dxRole_PermissionRoleSelectBox").dxSelectBox("instance").reset();
    $("#dxRole_PermissionPermissionTagBox").dxTagBox("instance").reset();
    $("#dxRole_PermissionIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxRole_PermissionGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxRole_PermissionGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxRole_PermissionGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxRole_PermissionGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
async function PopulateRole_PermissionFields(data) {
    $('#hiddenRole_PermissionID').val(data.ID);
}
function GetRole_PermissionDTO() {
    let _role_PermissionDTO = {
        ID: $('#hiddenRole_PermissionID').val(),
        PermissionIDArray: $("#dxRole_PermissionPermissionTagBox").dxTagBox("instance").option("value"),
        RoleID: $("#dxRole_PermissionRoleSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxRole_PermissionIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _role_PermissionDTO;
}
async function ShowRole_PermissionDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this permission for this role',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteRole_Permission_Global();
    } else {
        ClearRole_PermissionFields();
    }
}
async function CreateRole_Permission_Global() {
    await dxLoadPanel.show();
    const _role_PermissionDTO = GetRole_PermissionDTO();
    const _validation_ResultDTO = await CreateRole_Permission(_role_PermissionDTO);
    if (_validation_ResultDTO.Result) {
        ClearRole_PermissionFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateRole_Permission_Global() {
    await dxLoadPanel.show();
    const _role_PermissionDTO = GetRole_PermissionDTO();
    const _validation_ResultDTO = await UpdateRole_Permission(_role_PermissionDTO);
    if (_validation_ResultDTO.Result) {
        ClearRole_PermissionFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteRole_Permission_Global() {
    await dxLoadPanel.show();
    const _role_PermissionDTO = GetRole_PermissionDTO();
    const _validation_ResultDTO = await DeleteRole_Permission(_role_PermissionDTO);
    if (_validation_ResultDTO.Result) {
        ClearRole_PermissionFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion
//#region Permission
async function InitializePermissionCatalogControls() {
    $("#dxPermissionNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxPermissionModuleTextBox").dxTextBox({
        placeholder: 'Type module...'
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
                { caption: "Module", dataField: "Module" },
                { caption: "Description", dataField: "Description" },
                { caption: "Action", dataField: "ActionName", groupIndex: 0 },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    document.getElementById("btnClosePermissionModal").addEventListener("click", ClearPermissionFields);
    document.getElementById("PermissionsTab").addEventListener("click", ClearPermissionFields);
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
    $("#dxPermissionModuleTextBox").dxTextBox("instance").option("value", "");
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
    $("#dxPermissionModuleTextBox").dxTextBox("instance").option("value", data.Module);
    $("#dxPermissionDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxPermissionActionSelectBox").dxSelectBox("instance").option("value", data.ActionID);
    $("#dxPermissionIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
function GetPermissionDTO() {
    let _permissionDTO = {
        ID: $('#hiddenPermissionID').val(),
        Name: $("#dxPermissionNameTextBox").dxTextBox("instance").option("value"),
        Module: $("#dxPermissionModuleTextBox").dxTextBox("instance").option("value"),
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
//Reload select box for related fields
async function ReloadRolesSelectBox() {
    $("#dxRoleRelationRoleSelectBox").dxSelectBox("instance").option("dataSource", await GetDXRoleDataSource());
    $("#dxRoleRelationRoleTypeSelectBox").dxSelectBox("instance").option("dataSource", await GetDXRoleTypeDataSource());
    $("#dxRole_PermissionPermissionTagBox").dxTagBox("instance").option("dataSource", await GetDXPermissionDataSource());

}