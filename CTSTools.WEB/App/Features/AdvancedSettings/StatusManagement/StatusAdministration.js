import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { UpdateStatus, DeleteStatus, GetDXStatusDataSource, CreateStatus } from './Status/Status_Service.js'
import { CreateStatusType, UpdateStatusType, DeleteStatusType, GetDXStatusTypeDataSource } from './StatusType/StatusType_Service.js'
import { CreateStatus_StatusType, UpdateStatus_StatusType, DeleteStatus_StatusType, GetDXStatus_StatusTypeDataSource } from './Status_StatusType/Status_StatusType_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeStatusCatalogControls();
    InitializeStatusTypeCatalogControls();
    InitializeStatus_StatusTypeCatalogControls();
});
//#region Status catalog
async function InitializeStatusCatalogControls() {
    $("#dxStatusIsActiveCheckBox").dxCheckBox({
        value: true
    });

    $("#dxStatusNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });

    $("#dxStatusDescription").dxTextArea({
        placeholder: 'Type description...'
    });

    $("#dxStatusGrid").dxDataGrid({
        dataSource: await GetDXStatusDataSource(),
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
            fileName: "StatusCatalog",
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
            let _statusData = data.selectedRowsData[0];
            if (_statusData != null) {
                StatusActionButtons("Update");
                PopulateStatusFields(_statusData);
            }
        },
        columns:
            [
                {
                    caption: "Option",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#StatusModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenStatusID").val(options.data.ID);
                               
                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenStatusID").val(options.data.ID);
                                ShowStatusDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    StatusActionButtons("Save");

}
function StatusActionButtons(Action) {
    $("#StatusActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("StatusActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateStatusButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateStatusButton").addEventListener("click", CreateStatus_Global);
    }
    else {
        // Update
        document.getElementById("StatusActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearStatusButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateStatusButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearStatusButton").addEventListener("click", ClearStatusFields);
        document.getElementById("UpdateStatusButton").addEventListener("click", UpdateStatus_Global);
    }
}
function ClearStatusFields() {
    $("#StatusModal").modal("hide");

    StatusActionButtons("Save");
    $('#hiddenStatusID').val("");
    $("#dxStatusIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxStatusNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxStatusDescription").dxTextArea("instance").option("value", '');

    let keys = $("#dxStatusGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxStatusGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxStatusGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxStatusGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateStatusFields(data) {
    $('#hiddenStatusID').val(data.ID);
    $("#dxStatusIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxStatusNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxStatusDescription").dxTextArea("instance").option("value", data.Description);
}
function GetStatusDTO() {
    let _statusDTO = {
        ID: $('#hiddenStatusID').val(),
        Name: $("#dxStatusNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxStatusDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxStatusIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _statusDTO;
}
async function ShowStatusDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this status',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteStatus_Global();
    } else {
        ClearStatusFields();
    }
}
async function CreateStatus_Global() {
    await dxLoadPanel.show();
    const _statusDTO = GetStatusDTO();
    const _validation_ResultDTO = await CreateStatus(_statusDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatusFields();
        await ReloadStatusRelatedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateStatus_Global() {
    await dxLoadPanel.show();
    const _statusDTO = GetStatusDTO();
    const _validation_ResultDTO = await UpdateStatus(_statusDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatusFields();
        await ReloadStatusRelatedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteStatus_Global() {
    await dxLoadPanel.show();
    const _statusDTO = GetStatusDTO();
    const _validation_ResultDTO = await DeleteStatus(_statusDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatusFields();
        await ReloadStatusRelatedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#endregion 
//#region Status Type Catalog
async function InitializeStatusTypeCatalogControls() {
    $("#dxStatusTypeIsActiveCheckBox").dxCheckBox({
        value: true
    });

    $("#dxStatusTypeNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });

    $("#dxStatusTypeDescription").dxTextArea({
        placeholder: 'Type description..'
    });

    $("#dxStatusTypeGrid").dxDataGrid({
        dataSource: await GetDXStatusTypeDataSource(),
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
            fileName: "StatusTypeCatalog",
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
            let _statusTypeData = data.selectedRowsData[0];
            if (_statusTypeData != null) {
                StatusTypeActionButtons("Update");
                PopulateStatusTypeFields(_statusTypeData);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#StatusTypeModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenStatusTypeID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenStatusTypeID").val(options.data.ID);
                                ShowStatusTypeDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date ", dataField: "AddedDate", dataType: "datetime" },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update ", dataField: "LastUpdate", dataType: "datetime" },

            ],
    });
    StatusTypeActionButtons("Save");

}
function StatusTypeActionButtons(Action) {
    $("#StatusTypeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("StatusTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateStatusTypeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateStatusTypeButton").addEventListener("click", CreateStatusType_Global);
    }
    else {
        // Update
        document.getElementById("StatusTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearStatusTypeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateStatusTypeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearStatusTypeButton").addEventListener("click", ClearStatusTypeFields);
        document.getElementById("UpdateStatusTypeButton").addEventListener("click", UpdateStatusType_Global);
    }
}
function ClearStatusTypeFields() {
    $("#StatusTypeModal").modal("hide");

    StatusTypeActionButtons("Save");
    $('#hiddenStatusTypeID').val("");
    $("#dxStatusTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxStatusTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxStatusTypeDescription").dxTextArea("instance").option("value", '');

    let keys = $("#dxStatusTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxStatusTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxStatusTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxStatusTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateStatusTypeFields(data) {
    $('#hiddenStatusTypeID').val(data.ID);
    $("#dxStatusTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxStatusTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxStatusTypeDescription").dxTextArea("instance").option("value", data.Description);
}
function GetStatusTypeDTO() {
    let _statusTypeDTO = {
        ID: $('#hiddenStatusTypeID').val(),
        Name: $("#dxStatusTypeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxStatusTypeDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxStatusTypeIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _statusTypeDTO;
}
async function ShowStatusTypeDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this status type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteStatusType_Global();
    } else {
        ClearStatusTypeFields();
    }
}
async function CreateStatusType_Global() {
    await dxLoadPanel.show();
    const _statusTypeDTO = GetStatusTypeDTO();
    const _validation_ResultDTO = await CreateStatusType(_statusTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatusTypeFields();
        await ReloadStatusRelatedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateStatusType_Global() {
    await dxLoadPanel.show();
    const _statusTypeDTO = GetStatusTypeDTO();
    const _validation_ResultDTO = await UpdateStatusType(_statusTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatusTypeFields();
        await ReloadStatusRelatedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteStatusType_Global() {
    await dxLoadPanel.show();
    const _statusTypeDTO = GetStatusTypeDTO();
    const _validation_ResultDTO = await DeleteStatusType(_statusTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatusTypeFields();
        await ReloadStatusRelatedFields();
    }
    HostResponse(_validation_ResultDTO);

    dxLoadPanel.hide();
}
//#endregion
//#region Status relation Catalog
async function InitializeStatus_StatusTypeCatalogControls() {
    $("#dxStatus_StatusTypeStatusSelectBox").dxSelectBox({
        dataSource: await GetDXStatusDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });

    $("#dxStatus_StatusTypeStatusTypeSelectBox").dxSelectBox({
        dataSource: await GetDXStatusTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxStatus_StatusTypeIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxStatus_StatusTypeGrid").dxDataGrid({
        dataSource: [],
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
            fileName: "StatusTypeCatalog",
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
            let _status_StatusTypeData = data.selectedRowsData[0];
            if (_status_StatusTypeData != null) {
                Status_StatusTypeActionButtons("Update");
                PopulateStatus_StatusTypeFields(_status_StatusTypeData);
            }
        },
        columns:
            [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#StatusRelationModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenStatus_StatusTypeID").val(options.data.ID);
                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenStatus_StatusTypeID").val(options.data.ID);
                                ShowStatus_StatusTypeDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Status", dataField: "StatusDTO.Name" },
                { caption: "Status Type", dataField: "StatusTypeDTO.Name" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: "datetime" },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: "datetime" },


            ],
    });
    Status_StatusTypeActionButtons("Save");
    GetDXStatus_StatusTypeDataSource_Global();
}
function Status_StatusTypeActionButtons(Action) {
    $("#Status_StatusTypeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("Status_StatusTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateStatus_StatusTypeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateStatus_StatusTypeButton").addEventListener("click", CreateStatus_StatusType_Global);
    }
    else {
        // Update
        document.getElementById("Status_StatusTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearStatus_StatusTypeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateStatus_StatusTypeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearStatus_StatusTypeButton").addEventListener("click", ClearStatus_StatusTypeFields);
        document.getElementById("UpdateStatus_StatusTypeButton").addEventListener("click", UpdateStatus_StatusType_Global);
    }
}
function ClearStatus_StatusTypeFields() {
    $("#StatusRelationModal").modal("hide");
    Status_StatusTypeActionButtons("Save");
    $('#hiddenStatus_StatusTypeID').val("");

    $("#dxStatus_StatusTypeStatusTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxStatus_StatusTypeStatusSelectBox").dxSelectBox("instance").reset();
    $("#dxStatus_StatusTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxStatus_StatusTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxStatus_StatusTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxStatus_StatusTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxStatus_StatusTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateStatus_StatusTypeFields(data) {
    $('#hiddenStatus_StatusTypeID').val(data.ID);
    $("#dxStatus_StatusTypeStatusTypeSelectBox").dxSelectBox("instance").option("value", data.StatusTypeDTO.ID);
    $("#dxStatus_StatusTypeStatusSelectBox").dxSelectBox("instance").option("value", data.StatusDTO.ID);
    $("#dxStatus_StatusTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
function GetStatus_StatusTypeDTO() {
    let _status_StatusTypeDTO = {
        ID: $('#hiddenStatus_StatusTypeID').val(),
        StatusDTO: {
            ID: $("#dxStatus_StatusTypeStatusSelectBox").dxSelectBox("instance").option("value")
        },
        StatusTypeDTO: {
            ID: $("#dxStatus_StatusTypeStatusTypeSelectBox").dxSelectBox("instance").option("value")
        },
        IsActive: $("#dxStatus_StatusTypeIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _status_StatusTypeDTO;
}
async function ShowStatus_StatusTypeDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this relation',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteStatus_StatusType_Global();
    } else {
        ClearStatus_StatusTypeFields();
    }
}
async function CreateStatus_StatusType_Global() {
    await dxLoadPanel.show();
    const _status_StatusTypeDTO = GetStatus_StatusTypeDTO();
    const _validation_ResultDTO = await CreateStatus_StatusType(_status_StatusTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatus_StatusTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateStatus_StatusType_Global() {
    await dxLoadPanel.show();
    const _status_StatusTypeDTO = GetStatus_StatusTypeDTO();
    const _validation_ResultDTO = await UpdateStatus_StatusType(_status_StatusTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatus_StatusTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteStatus_StatusType_Global() {
    await dxLoadPanel.show();
    const _status_StatusTypeDTO = GetStatus_StatusTypeDTO();
    const _validation_ResultDTO = await DeleteStatus_StatusType(_status_StatusTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStatus_StatusTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion


//#region datasources 
async function GetDXStatus_StatusTypeDataSource_Global() {
    let _status_StatusTypeDTO = {
        GetStatusDTO: true,
        GetStatusTypeDTO: true
    }
    let _status_StatusTypeDataSourceList = await GetDXStatus_StatusTypeDataSource(_status_StatusTypeDTO);
    $("#dxStatus_StatusTypeGrid").dxDataGrid("instance").option("dataSource", _status_StatusTypeDataSourceList);
}
//#endregion

async function ReloadStatusRelatedFields() {
    $("#dxStatus_StatusTypeStatusSelectBox").dxSelectBox("instance").option("dataSource", await GetDXStatusDataSource());
    $("#dxStatus_StatusTypeStatusTypeSelectBox").dxSelectBox("instance").option("dataSource", await GetDXStatusTypeDataSource());
}