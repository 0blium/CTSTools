import { GetDXDashboardDataSource, CreateDashboard, UpdateDashboard, DeleteDashboard } from './Dashboard/Dashboard_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetDXDepartmentDataSource } from '../../../AdvancedSettings/LocationManagement/Department/Department_Service.js'
import { GetDXFacilityDataSource } from '../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXLevelDataSource } from '../Settings/Level/Level_Service.js'
import { GetDXUserDataSource } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetDXGoalRangeDataSource } from '../Settings/GoalRange/GoalRange_Service.js'


document.addEventListener("DOMContentLoaded", () => {
    InitializeDashboardCatalogControls();

});

async function InitializeDashboardCatalogControls() {

    $("#dxDashboardGrid").dxDataGrid({
        dataSource: await GetDXDashboardDataSource({ IsActive: true, GetFacilityDTO: true, GetDepartmentDTO: true }),
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
            fileName: "DashboardCatalog",
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
                                    { text: "View", icon: "fas fa-chart-line text-primary", value: 1 },
                                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 3 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 4 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                let _data = options.data;
                                switch (e.itemData.value) {
                                    case 1:
                                        location.href = "/App/Features/Management/Edashboard/DashboardManagement/DashboardDataEntry.aspx?DashboardID=" + _data.ID;
                                        break;                                  
                                    case 3:
                                        DashboardActionButtons("Update");
                                        PopulateDashboardFields(_data);
                                        $('#SaveDashboardRecordModal').modal('show');
                                        break;
                                    case 4:
                                        document.getElementById('hiddenDashboardID').value = options.data.ID;
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
                { caption: "Description", dataField: "Description" },
                { caption: "Revision", dataField: "Revision" },
                { caption: "Owner", dataField: "OwnerName" },
                { caption: "Department", dataField: "DepartmentName" },
                { caption: "Level", dataField: "LevelName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
            ],
    });

    $("#dxDashboardRevisionTextBox").dxTextBox({
        placeholder: 'Type Revision...'
    });
    $("#dxDashboardNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxDashboardDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxDashboardOwnerSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDashboardLevelSelectBox").dxSelectBox({
        dataSource: await GetDXLevelDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDashboardDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDashboardFacilitySelectBox").dxSelectBox({
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
                $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").reset();
                $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource(_departmentDTO));
            } else {
                $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        },
    });
    $("#dxDashboardGoalRangeSelectBox").dxSelectBox({
        dataSource: await GetDXGoalRangeDataSource({ IsActive: true }),
        displayExpr: "Value",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    let _yearStart = 2024;
    let _date = new Date();
    let _yearEnd = _date.getFullYear();
    let _yearsList = [];

    for (let _year = _yearStart; _year <= _yearEnd; _year++) {
        var _yearObject = { Year: 0 };
        _yearObject.Year = _year;
        _yearsList.push(_yearObject);
    }
    let _yearsData = _yearsList.reverse();

    $("#dxDashboardYearSelectBox").dxSelectBox({
        dataSource: _yearsData,
        valueExpr: "Year",
        displayExpr: "Year",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a year...",
    });

    $("#dxDashboardDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Department...",
    });
    document.getElementById("btnCloseDashboardModal").addEventListener("click", ClearDashboardFields);
    DashboardActionButtons("Save");
}

//#endregion

async function PopulateDashboardFields(data) {
    $("#hiddenDashboardID").val(data.ID);
    $("#dxDashboardNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxDashboardRevisionTextBox").dxTextBox("instance").option("value", data.Revision);
    $("#dxDashboardDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxDashboardYearSelectBox").dxSelectBox("instance").option("value", data.Year);
    $("#dxDashboardLevelSelectBox").dxSelectBox("instance").option("value", data.LevelID);
    $("#dxDashboardOwnerSelectBox").dxSelectBox("instance").option("value", data.OwnerID);
    $("#dxDashboardGoalRangeSelectBox").dxSelectBox("instance").option("value", data.GoalRangeID);

    await $("#dxDashboardFacilitySelectBox").dxSelectBox("instance").option("value", data.DepartmentDTO.FacilityID);
    await $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").option("value", data.DepartmentID);

}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the dashboard, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDashboard_Global();
    } else {
        ClearDashboardFields();
    }

}
function DashboardActionButtons(Action) {
    $("#DashboardActionButtons").empty();
    document.getElementById('DashboardModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewDashboardBtn").addEventListener("click", ClearDashboardFields);
        document.getElementById('DashboardModalTitle').innerText = 'Add Dashboard Form'
        document.getElementById("DashboardActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateDashboardButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardButton").addEventListener("click", ClearDashboardFields);
        document.getElementById("CreateDashboardButton").addEventListener("click", CreateDashboard_Global);
    }
    else {
        // Update
        document.getElementById('DashboardModalTitle').innerText = 'Update Dashboard Form'
        document.getElementById("DashboardActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateDashboardButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardButton").addEventListener("click", ClearDashboardFields);
        document.getElementById("UpdateDashboardButton").addEventListener("click", UpdateDashboard_Global);
    }
}
function ClearDashboardFields() {
    $('#SaveDashboardRecordModal').modal('hide');
    DashboardActionButtons("Save");
    $("#hiddenDashboardID").val("");
    $("#dxDashboardNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxDashboardGoalRangeSelectBox").dxSelectBox("instance").reset();
    $("#dxDashboardRevisionTextBox").dxTextBox("instance").option("value", "");
    $("#dxDashboardDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxDashboardLevelSelectBox").dxSelectBox("instance").reset();
    $("#dxDashboardFacilitySelectBox").dxSelectBox("instance").reset();
    $("#dxDashboardYearSelectBox").dxSelectBox("instance").reset();
    $("#dxDashboardOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxDashboardGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDashboardGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetDashboardDTO() {
    let _dashboardDTO = {
        ID: $("#hiddenDashboardID").val(),
        Name: $("#dxDashboardNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDashboardDescriptionTextArea").dxTextArea("instance").option("value"),
        Revision: $("#dxDashboardRevisionTextBox").dxTextBox("instance").option("value"),
        DepartmentID: $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").option("value"),
        LevelID: $("#dxDashboardLevelSelectBox").dxSelectBox("instance").option("value"),
        OwnerID: $("#dxDashboardOwnerSelectBox").dxSelectBox("instance").option("value"),
        GoalRangeID: $("#dxDashboardGoalRangeSelectBox").dxSelectBox("instance").option("value"),
        Year: $("#dxDashboardYearSelectBox").dxSelectBox("instance").option("value"),
        IsActive: true,

    }
    return _dashboardDTO;
}
//#endregion

//#region Dashboard CRUD Functions
async function CreateDashboard_Global() {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardDTO();
    const _validation_ResultDTO = await CreateDashboard(_dashboardDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDashboardGrid").dxDataGrid("instance").refresh();
        ClearDashboardFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDashboard_Global() {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardDTO();
    const _validation_ResultDTO = await UpdateDashboard(_dashboardDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDashboardGrid").dxDataGrid("instance").refresh();
        ClearDashboardFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDashboard_Global() {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardDTO();
    const _validation_ResultDTO = await DeleteDashboard(_dashboardDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDashboardGrid").dxDataGrid("instance").refresh();
        ClearDashboardFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    ClearDashboardFields();
    dxLoadPanel.hide();
}

const GetDXLevelDataSource_Global = () => {
    let _levelDTO = { IsActive: true }
    return GetDXLevelDataSource(_levelDTO)
}

//#endregion

//Reload select box for related fields
async function ReloadLocationsSelectBox() {
    $("#dxDashboardFacilitySelectBox").dxSelectBox("instance").option("dataSource", await GetDXFacilityDataSource());
    $("#dxDashboardDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource());
}