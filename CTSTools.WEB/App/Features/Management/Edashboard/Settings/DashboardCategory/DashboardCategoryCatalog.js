import { GetDXDashboardCategoryDataSource, CreateDashboardCategory, UpdateDashboardCategory, DeleteDashboardCategory } from './DashboardCategory_Service.js'
import { dxLoadPanel } from '../../../../../Common/Components/dxLoadPanel.js';
import { HostResponse, ClearErrorFeedback } from '../../../../../Common/Utils/Response.js';

//#region DashboardCategory Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeDashboardCategoryCatalogControls();
});
async function InitializeDashboardCategoryCatalogControls() {
    $("#dxDashboardCategoryIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxDashboardCategoryPanelNameTextBox").dxTextBox({
        placeholder: 'Type panel name...'
    });
    $("#dxDashboardCategoryNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxDashboardCategoryDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxDashboardCategoryGrid").dxDataGrid({
        dataSource: await GetDXDashboardCategoryDataSource_Global({ IsActive: true }),
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
            fileName: "DashboardCategoryCatalog",
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
            let _dashboardCategoryData = data.selectedRowsData[0];
            if (_dashboardCategoryData != null) {
                DashboardCategoryActionButtons("Update");
                PopulateDashboardCategoryFields(_dashboardCategoryData);
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
                                    $('#SaveDashboardCategoryRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenDashboardCategoryID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Panel Name", dataField: "PanelName" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseDashboardCategoryModal").addEventListener("click", ClearDashboardCategoryFields);
    DashboardCategoryActionButtons("Save");
}
async function PopulateDashboardCategoryFields(data) {
    $("#hiddenDashboardCategoryID").val(data.ID);
    $("#dxDashboardCategoryNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxDashboardCategoryPanelNameTextBox").dxTextBox("instance").option("value", data.PanelName);
    $("#dxDashboardCategoryDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxDashboardCategoryIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the dashboard category, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDashboardCategory_Global();
    } else {
        ClearDashboardCategoryFields();
    }
}
function DashboardCategoryActionButtons(Action) {
    $("#DashboardCategoryActionButtons").empty();
    document.getElementById('DashboardCategoryModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewDashboardCategoryBtn").addEventListener("click", ClearDashboardCategoryFields);
        document.getElementById('DashboardCategoryModalTitle').innerText = 'Add Dashboard Category'
        document.getElementById("DashboardCategoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateDashboardCategoryButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardCategoryButton").addEventListener("click", ClearDashboardCategoryFields);
        document.getElementById("CreateDashboardCategoryButton").addEventListener("click", CreateDashboardCategory_Global);
    }
    else {
        // Update
        document.getElementById('DashboardCategoryModalTitle').innerText = 'Update Dashboard Category'
        document.getElementById("DashboardCategoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateDashboardCategoryButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardCategoryButton").addEventListener("click", ClearDashboardCategoryFields);
        document.getElementById("UpdateDashboardCategoryButton").addEventListener("click", UpdateDashboardCategory_Global);
    }
}
function ClearDashboardCategoryFields() {
    $('#SaveDashboardCategoryRecordModal').modal('hide');
    DashboardCategoryActionButtons("Save");
    $("#hiddenDashboardCategoryID").val("");
    $("#dxDashboardCategoryNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxDashboardCategoryPanelNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxDashboardCategoryDescription").dxTextArea("instance").option("value", '');
    $("#dxDashboardCategoryIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxDashboardCategoryGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDashboardCategoryGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetDashboardCategoryDTO() {
    let _DashboardCategoryDTO = {
        ID: $("#hiddenDashboardCategoryID").val(),
        Name: $("#dxDashboardCategoryNameTextBox").dxTextBox("instance").option("value"),
        PanelName: $("#dxDashboardCategoryPanelNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDashboardCategoryDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxDashboardCategoryIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _DashboardCategoryDTO;
}
//#endregion

//#region DashboardCategory CRUD Functions
async function CreateDashboardCategory_Global() {
    await dxLoadPanel.show();
    const _dashboardCategoryDTO = GetDashboardCategoryDTO();
    const _validation_ResultDTO = await CreateDashboardCategory(_dashboardCategoryDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDashboardCategoryGrid").dxDataGrid("instance").refresh();
        ClearDashboardCategoryFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDashboardCategory_Global() {
    await dxLoadPanel.show();
    const _dashboardCategoryDTO = GetDashboardCategoryDTO();
    const _validation_ResultDTO = await UpdateDashboardCategory(_dashboardCategoryDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDashboardCategoryGrid").dxDataGrid("instance").refresh();
        ClearDashboardCategoryFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDashboardCategory_Global() {
    await dxLoadPanel.show();
    const _dashboardCategoryDTO = GetDashboardCategoryDTO();
    const _validation_ResultDTO = await DeleteDashboardCategory(_dashboardCategoryDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDashboardCategoryGrid").dxDataGrid("instance").refresh();
        ClearDashboardCategoryFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearDashboardCategoryFields();
    dxLoadPanel.hide();
}
const GetDXDashboardCategoryDataSource_Global = () => {
    return GetDXDashboardCategoryDataSource()
}
//#endregion