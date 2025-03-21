import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXStationTypeDataSource, CreateStationType, UpdateStationType, DeleteStationType } from './StationType/StationType_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeStationTypeCatalogControls();
});

async function InitializeStationTypeCatalogControls() {
    $("#dxStationTypeNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxStationTypeDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxStationTypeIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxStationTypeGrid").dxDataGrid({
        dataSource: await GetDXStationTypeDataSource(),
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
            fileName: "StationTypeCatalog",
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
            let _stationTypeData = data.selectedRowsData[0];
            if (_stationTypeData != null) {
                StationTypeActionButtons("Update");
                PopulateStationTypeFields(_stationTypeData);
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
                                $("#hiddenStationTypeID").val(options.data.ID);
                                ShowStationTypeDeleteQuestion();
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
    StationTypeActionButtons("Save");
}
function StationTypeActionButtons(Action) {
    $("#StationTypeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("StationTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateStationTypeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateStationTypeButton").addEventListener("click", CreateStationType_Global);
    }
    else {
        // Update
        document.getElementById("StationTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearStationTypeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateStationTypeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearStationTypeButton").addEventListener("click", ClearStationTypeFields);
        document.getElementById("UpdateStationTypeButton").addEventListener("click", UpdateStationType_Global);
    }
}
function ClearStationTypeFields() {
    StationTypeActionButtons("Save");
    $('#hiddenStationTypeID').val("");
    $("#dxStationTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxStationTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxStationTypeDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxStationTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxStationTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxStationTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxStationTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateStationTypeFields(data) {
    $('#hiddenStationTypeID').val(data.ID);
    $("#dxStationTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxStationTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxStationTypeDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetStationTypeDTO() {
    let _stationTypeDTO = {
        ID: $('#hiddenStationTypeID').val(),
        Name: $("#dxStationTypeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxStationTypeDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxStationTypeIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _stationTypeDTO;
}
async function ShowStationTypeDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Station Type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteStationType_Global();
    } else {
        ClearStationTypeFields();
    }
}
async function CreateStationType_Global() {
    await dxLoadPanel.show();
    const _stationTypeDTO = GetStationTypeDTO();
    const _validation_ResultDTO = await CreateStationType(_stationTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStationTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateStationType_Global() {
    await dxLoadPanel.show();
    const _stationTypeDTO = GetStationTypeDTO();
    const _validation_ResultDTO = await UpdateStationType(_stationTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStationTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteStationType_Global() {
    await dxLoadPanel.show();
    const _stationTypeDTO = GetStationTypeDTO();
    const _validation_ResultDTO = await DeleteStationType(_stationTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearStationTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}