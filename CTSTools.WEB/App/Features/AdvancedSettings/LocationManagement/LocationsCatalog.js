import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { UpdateFacility, DeleteFacility, GetDXFacilityDataSource, CreateFacility } from './Facility/Facility_Service.js'
import { CreateDepartment, UpdateDepartment, DeleteDepartment, GetDXDepartmentDataSource } from './Department/Department_Service.js'
import { GetDXUserDataSource } from '../UserManagement/User/User_Service.js'


document.addEventListener("DOMContentLoaded", () => {
    InitializeFacilityCatalogControls();
    InitializeDepartmentCatalogControls();
});

//#region Facility Catalog
async function InitializeFacilityCatalogControls() {
    $("#dxFacilityIsActiveCheckBox").dxCheckBox({
        value: true
    });

    $("#dxFacilityNameTextBox").dxTextBox({
        placeholder: 'Type Facility Name...'
    });

    $("#dxFacilityDescriptionTextArea").dxTextArea({
        placeholder: 'Type Facility Description...'
    });

    $("#dxFacilityGrid").dxDataGrid({
        dataSource: await GetDXFacilityDataSource(),
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
            fileName: "FacilityCatalog",
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
            let _facilityData = data.selectedRowsData[0];
            if (_facilityData != null) {
                FacilityActionButtons("Update");
                PopulateFacilityFields(_facilityData);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#FacilityModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenFacilityID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenFacilityID").val(options.data.ID);
                                ShowFacilityDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive"
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Name",
                    dataField: "Name"
                },
                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Added By I D",
                    dataField: "AddedByID",
                    visible: false
                },
                {
                    caption: "Added By",
                    dataField: "AddedByName"
                },
                {
                    caption: "Added Date",
                    dataField: "AddedDate",
                    dataType: 'datetime'
                },
                {
                    caption: "Last Update By I D",
                    dataField: "LastUpdateByID",
                    visible: false
                },
                {
                    caption: "Last Update By",
                    dataField: "LastUpdateByName"
                },
                {
                    caption: "Last Update",
                    dataField: "LastUpdate",
                    dataType: 'datetime'
                },


            ],
    });

    FacilityActionButtons("Save");
}

function FacilityActionButtons(Action) {
    $("#FacilityActionButtons").empty();
    document.getElementById("FacilityModalCloseButton").addEventListener("click", ClearFacilityFields);

    if (Action == "Save") {
        document.getElementById("FacilityActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateFacilityButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateFacilityButton").addEventListener("click", CreateFacility_Global);
    }
    else {
        // Update
        document.getElementById("FacilityActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateFacilityButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateFacilityButton").addEventListener("click", UpdateFacility_Global);
    }
}

function ClearFacilityFields() {
    FacilityActionButtons("Save");
    $('#hiddenFacilityID').val("");
    $("#dxFacilityIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxFacilityNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxFacilityDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxFacilityGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxFacilityGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxFacilityGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxFacilityGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateFacilityFields(data) {
    $('#hiddenFacilityID').val(data.ID);
    $("#dxFacilityIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxFacilityNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxFacilityDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}

function GetFacilityDTO() {
    let _facilityDTO = {
        ID: $('#hiddenFacilityID').val(),
        Name: $("#dxFacilityNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxFacilityDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxFacilityIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _facilityDTO;
}

async function ShowFacilityDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this facility',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteFacility_Global();
    } else {
        ClearFacilityFields();
    }
}

async function CreateFacility_Global() {
    await dxLoadPanel.show();
    const _facilityDTO = GetFacilityDTO();
    const _validation_ResultDTO = await CreateFacility(_facilityDTO);
    if (_validation_ResultDTO.Result) {
        ClearFacilityFields();
        await ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function UpdateFacility_Global() {
    await dxLoadPanel.show();
    const _facilityDTO = GetFacilityDTO();
    const _validation_ResultDTO = await UpdateFacility(_facilityDTO);
    if (_validation_ResultDTO.Result) {
        ClearFacilityFields();
        await ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function DeleteFacility_Global() {
    await dxLoadPanel.show();
    const _facilityDTO = GetFacilityDTO();
    const _validation_ResultDTO = await DeleteFacility(_facilityDTO);
    if (_validation_ResultDTO.Result) {
        ClearFacilityFields();
        await ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion

//#region Department Catalog
async function InitializeDepartmentCatalogControls() {
    $("#dxDepartmentIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxDepartmentResponsiblesTagBox").dxTagBox({
        dataSource: await GetDXUserDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'instantly',
        popupWidth: 450,
        placeholder: "Select responsibles for department...",
    });
    $("#dxDepartmentNameTextBox").dxTextBox({
        placeholder: 'Type Department Name...'
    });

    $("#dxDepartmentDescriptionTextArea").dxTextArea({
        placeholder: 'Type Department Description...'
    });

    $("#dxDepartmentFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Facility...",
    });

    $("#dxDepartmentGrid").dxDataGrid({
        dataSource: await GetDXDepartmentDataSource({ GetDepartmentResponsibleList: true }, ""),
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
            fileName: "DepartmentCatalog",
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
            let _departmentData = data.selectedRowsData[0];
            if (_departmentData != null) {
                DepartmentActionButtons("Update");
                PopulateDepartmentFields(_departmentData);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#DepartmentModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenDepartmentID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenDepartmentID").val(options.data.ID);
                                ShowDepartmentDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Facility", dataField: "FacilityName" },
                { caption: "Responsibles", dataField: "ResponsibleNames" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
            ],
    });

    DepartmentActionButtons("Save");

}

function DepartmentActionButtons(Action) {
    $("#DepartmentActionButtons").empty();
    document.getElementById("DepartmentModalCloseButton").addEventListener("click", ClearDepartmentFields);

    if (Action == "Save") {
        document.getElementById("DepartmentActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateDepartmentButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateDepartmentButton").addEventListener("click", CreateDepartment_Global);
    }
    else {
        // Update
        document.getElementById("DepartmentActionButtons").innerHTML =
            '<div class="col-md-12">' +            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateDepartmentButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateDepartmentButton").addEventListener("click", UpdateDepartment_Global);
    }
}

function ClearDepartmentFields() {
    DepartmentActionButtons("Save");
    $('#hiddenDepartmentID').val("");
    $("#dxDepartmentIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxDepartmentNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxDepartmentDescriptionTextArea").dxTextArea("instance").option("value", '');
    $("#dxDepartmentFacilitySelectBox").dxSelectBox("instance").option("value", '');
    $("#dxDepartmentResponsiblesTagBox").dxTagBox("instance").reset();

    let keys = $("#dxDepartmentGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDepartmentGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxDepartmentGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxDepartmentGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

async function PopulateDepartmentFields(FacilityDTO) {
    $('#hiddenDepartmentID').val(FacilityDTO.ID);
    $("#dxDepartmentIsActiveCheckBox").dxCheckBox("instance").option("value", FacilityDTO.IsActive);
    $("#dxDepartmentNameTextBox").dxTextBox("instance").option("value", FacilityDTO.Name);
    $("#dxDepartmentDescriptionTextArea").dxTextArea("instance").option("value", FacilityDTO.Description);
    await $("#dxDepartmentFacilitySelectBox").dxSelectBox("instance").option("value", FacilityDTO.FacilityID);

    data.ResponsiblesIDArray = [];
    data.ResponsiblesIDArray = data.Department_ResponsibleList.map(departmentResponsible => departmentResponsible.ResponsibleID)

    await $("#dxDepartmentResponsiblesTagBox").dxTagBox("instance").option("value", FacilityDTO.ResponsiblesIDArray);

}

function GetDepartmentDTO() {
    let _departmentDTO = {
        ID: $('#hiddenDepartmentID').val(),
        Name: $("#dxDepartmentNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDepartmentDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxDepartmentIsActiveCheckBox").dxCheckBox("instance").option("value"),
        FacilityID: $("#dxDepartmentFacilitySelectBox").dxSelectBox("instance").option("value"),
        ResponsiblesIDArray: $("#dxDepartmentResponsiblesTagBox").dxTagBox("instance").option("value")
    }
    return _departmentDTO;
}

async function ShowDepartmentDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this department unit',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDepartment_Global();
    } else {
        ClearDepartmentFields();
    }
}

async function CreateDepartment_Global() {
    await dxLoadPanel.show();
    const _departmentDTO = GetDepartmentDTO();
    const _validation_ResultDTO = await CreateDepartment(_departmentDTO);
    if (_validation_ResultDTO.Result) {
        ClearDepartmentFields();
        await ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function UpdateDepartment_Global() {
    await dxLoadPanel.show();
    const _departmentDTO = GetDepartmentDTO();
    const _validation_ResultDTO = await UpdateDepartment(_departmentDTO);
    if (_validation_ResultDTO.Result) {
        ClearDepartmentFields();
        await ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function DeleteDepartment_Global() {
    await dxLoadPanel.show();
    const _departmentDTO = GetDepartmentDTO();
    const _validation_ResultDTO = await DeleteDepartment(_departmentDTO);
    if (_validation_ResultDTO.Result) {
        ClearDepartmentFields();
        await ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#endregion


//Reload select box for related fields
async function ReloadLocationsSelectBox() {
    $("#dxDepartmentFacilitySelectBox").dxSelectBox("instance").option("dataSource", await GetDXFacilityDataSource());
}