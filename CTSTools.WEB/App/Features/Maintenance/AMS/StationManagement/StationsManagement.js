import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXStationTypeDataSource } from './StationType/StationType_Service.js'
import { GetDXFacilityDataSource } from '../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXDepartmentDataSource } from '../../../AdvancedSettings/LocationManagement/Department/Department_Service.js'
import { GetDXStationDataSource, CreateStation, UpdateStation, DeleteStation } from './Station/Station_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeStationCatalogControls();
    EventHandler();
});

async function EventHandler() {
    document.getElementById("AddNewStationButton").addEventListener("click", ClearStationFields);
}

async function InitializeStationCatalogControls() {
    $("#dxStationNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxStationSerialTextBox").dxTextBox({
        placeholder: 'Type serial...',
        readOnly: true,
    });
    $("#dxStationDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxStationIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxStationStationTypeSelectBox").dxSelectBox({
        dataSource: await GetDXStationTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });

    $("#dxStationDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxStationFacilitySelectBox").dxSelectBox({
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
                $("#dxStationDepartmentSelectBox").dxSelectBox("instance").reset();
                $("#dxStationDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource(_departmentDTO));
            } else {
                $("#dxStationDepartmentSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        },
    });

    $("#dxStationDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Department...",
    });

    $("#dxStationGrid").dxDataGrid({
        dataSource: await GetDXStationDataSource({ IsActive: true, GetFacilityDTO: true, GetDepartmentDTO: true }),
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
            fileName: "StationManagement",
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
                //{
                //    caption: "Actions",
                //    alignment: "center",
                //    allowFiltering: false,
                //    allowSorting: false,
                //    width: 'auto',
                //    cellTemplate: function (container, options) {
                //        container.height(30);
                //        $('<button type="button" class="btn btn-danger me-1" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenStationID").val(options.data.ID);
                //                ShowStationDeleteQuestion();
                //            }).appendTo(container);
                //        $('<button type="button" class="btn btn-info me-1" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fas fa-print"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                var _data = options.data;
                //                // Open Label
                //                window.open("http://avmx-s05:8044//LabelPrint.aspx?LabelFile=LF-2173-00-A&Dummy=False&WO=" + _data.Serial + "&Qty=1&From=&To=&SkipEvery=&SerialLength=&ESD=True&SAS=False&FullWorkOrder=" + _data.Serial);
                //            }).appendTo(container);
                //        $('<button type="button" class="btn btn-success" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenStationID").val(options.data.ID);
                //                StationActionButtons("Update");
                //                $("#AddNewStationModal").modal("show");
                //                PopulateStationFields(options.data);
                //            }).appendTo(container);
                //    },
                //},
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
                                    { text: "Print", icon: "fa fa-print text-secondary", value: 2 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 3 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $("#hiddenStationID").val(options.data.ID);
                                    StationActionButtons("Update");
                                    $('#AddNewStationModal').modal('show');
                                    PopulateStationFields(options.data);
                                }
                                else if (e.itemData.value == 2) {
                                    var _data = options.data;
                                    // Open Label
                                    window.open("http://avmx-s05:8044//LabelPrint.aspx?LabelFile=LF-2173-00-A&Dummy=False&WO=" + _data.Serial + "&Qty=1&From=&To=&SkipEvery=&SerialLength=&ESD=True&SAS=False&FullWorkOrder=" + _data.Serial);
                                }
                                else if (e.itemData.value == 3) {
                                    document.getElementById('hiddenStationID').value = options.data.ID;
                                    ShowStationDeleteQuestion();
                                }
                            },
                        });
                    }
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
                    caption: "Serial",
                    dataField: "Serial"
                },
                {
                    caption: "Station Type",
                    dataField: "StationTypeDTO.Name"
                },
                {
                    caption: "Facility",
                    dataField: "FacilityDTO.Name"
                },
                {
                    caption: "Department",
                    dataField: "DepartmentDTO.Name"
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
                    caption: "Last Update By",
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
    document.getElementById("btnCloseStationModal").addEventListener("click", ClearStationFields);
    StationActionButtons("Save");
}
function StationActionButtons(Action) {
    $("#StationActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("StationActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateStationButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearStationButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearStationButton").addEventListener("click", ClearStationFields);
        document.getElementById("CreateStationButton").addEventListener("click", CreateStation_Global);
    }
    else {
        // Update
        document.getElementById("StationActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateStationButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearStationButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearStationButton").addEventListener("click", ClearStationFields);
        document.getElementById("UpdateStationButton").addEventListener("click", UpdateStation_Global);
    }
}
function ClearStationFields() {
    StationActionButtons("Save");
    $('#hiddenStationID').val("");
    $("#dxStationNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxStationSerialTextBox").dxTextBox("instance").option("value", "");
    $("#dxStationDescriptionTextArea").dxTextArea("instance").option("value", '');
    $("#dxStationIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxStationStationTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxStationFacilitySelectBox").dxSelectBox("instance").reset();
    $("#dxStationDepartmentSelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxStationGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxStationGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxStationGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxStationGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
    $("#AddNewStationModal").modal("hide");
}
async function PopulateStationFields(data) {
    $('#hiddenStationID').val(data.ID);
    $("#dxStationNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxStationSerialTextBox").dxTextBox("instance").option("value", data.Serial);
    $("#dxStationDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxStationIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxStationStationTypeSelectBox").dxSelectBox("instance").option("value", data.StationTypeDTO.ID);
    $("#dxStationFacilitySelectBox").dxSelectBox("instance").option("value", data.FacilityDTO.ID);
    $("#dxStationDepartmentSelectBox").dxSelectBox("instance").option("value", data.DepartmentDTO.ID);
    await $("#dxStationFacilitySelectBox").dxSelectBox("instance").option("value", data.DepartmentDTO.FacilityID);
    await $("#dxStationDepartmentSelectBox").dxSelectBox("instance").option("value", data.DepartmentDTO.ID);
}
function GetStationDTO() {
    let _stationDTO = {
        ID: $('#hiddenStationID').val(),
        Name: $("#dxStationNameTextBox").dxTextBox("instance").option("value"),
        Serial: $("#dxStationSerialTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxStationDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxStationIsActiveCheckBox").dxCheckBox("instance").option("value"),
        StationTypeDTO: {
            ID: $("#dxStationStationTypeSelectBox").dxSelectBox("instance").option("value")
        },
        FacilityDTO: {
            ID: $("#dxStationFacilitySelectBox").dxSelectBox("instance").option("value")
        },
        DepartmentDTO: {
            ID: $("#dxStationDepartmentSelectBox").dxSelectBox("instance").option("value"),
        },
    }
    return _stationDTO;
}
async function ShowStationDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this station',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteStation_Global();
    } else {
        ClearStationFields();
    }
}
async function CreateStation_Global() {
    await dxLoadPanel.show();
    const _stationDTO = GetStationDTO();
    const _validation_ResultDTO = await CreateStation(_stationDTO);
    if (_validation_ResultDTO.Result) {
        $("#dxStationGrid").dxDataGrid("instance").refresh();
        ClearStationFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateStation_Global() {
    await dxLoadPanel.show();
    const _stationDTO = GetStationDTO();
    const _validation_ResultDTO = await UpdateStation(_stationDTO);
    if (_validation_ResultDTO.Result) {
        $("#dxStationGrid").dxDataGrid("instance").refresh();
        ClearStationFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteStation_Global() {
    await dxLoadPanel.show();
    const _stationDTO = GetStationDTO();
    const _validation_ResultDTO = await DeleteStation(_stationDTO);
    if (_validation_ResultDTO.Result) {
        $("#dxStationGrid").dxDataGrid("instance").refresh();
        ClearStationFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//Reload select box for related fields
async function ReloadLocationsSelectBox() {
    $("#dxStationFacilitySelectBox").dxSelectBox("instance").option("dataSource", await GetDXFacilityDataSource());
    $("#dxStationDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource());
}