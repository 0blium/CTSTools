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
                                    $('#SaveStationTypeRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenStationTypeID').value = options.data.ID;
                                    ShowDeleteQuestion();
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
    document.getElementById("btnCloseStationTypeModal").addEventListener("click", ClearStationTypeFields);
    StationTypeActionButtons("Save");
}
function StationTypeActionButtons(Action) {
    $("#StationTypeActionButtons").empty();
    document.getElementById('StationTypeModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewStationTypeBtn").addEventListener("click", ClearStationTypeFields);
        document.getElementById('StationTypeModalTitle').innerText = 'Add Station Type';
        document.getElementById("StationTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateStationTypeButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearStationTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearStationTypeButton").addEventListener("click", ClearStationTypeFields);
        document.getElementById("CreateStationTypeButton").addEventListener("click", CreateStationType_Global);
    }
    else {
        // Update
        document.getElementById('StationTypeModalTitle').innerText = 'Update Station Type';
        document.getElementById("StationTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateStationTypeButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearStationTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearStationTypeButton").addEventListener("click", ClearStationTypeFields);
        document.getElementById("UpdateStationTypeButton").addEventListener("click", UpdateStationType_Global);
    }
}
function ClearStationTypeFields() {
    $('#SaveStationTypeRecordModal').modal('hide');
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
async function ShowDeleteQuestion() {
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