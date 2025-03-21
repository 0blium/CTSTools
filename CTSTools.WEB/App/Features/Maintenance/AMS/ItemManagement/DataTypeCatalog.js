import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { CreateDataType, UpdateDataType, DeleteDataType, GetDXDataTypeDataSource } from './DataType_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeDataTypeCatalogControls();
});

async function InitializeDataTypeCatalogControls() {
    $("#dxDataTypeNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxDataTypeDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxDataTypeIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxDataTypeGrid").dxDataGrid({
        dataSource: await GetDXDataTypeDataSource(),
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
            fileName: "DataTypeCatalog",
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
            let _dataTypeData = data.selectedRowsData[0];
            if (_dataTypeData != null) {
                DataTypeActionButtons("Update");
                PopulateDataTypeFields(_dataTypeData);
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
                                $("#hiddenDataTypeID").val(options.data.ID);
                                ShowDataTypeDeleteQuestion();
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
    DataTypeActionButtons("Save");
}
function DataTypeActionButtons(Action) {
    $("#DataTypeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("DataTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateDataTypeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateDataTypeButton").addEventListener("click", CreateDataType_Global);
    }
    else {
        // Update
        document.getElementById("DataTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearDataTypeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateDataTypeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearDataTypeButton").addEventListener("click", ClearDataTypeFields);
        document.getElementById("UpdateDataTypeButton").addEventListener("click", UpdateDataType_Global);
    }
}
function ClearDataTypeFields() {
    DataTypeActionButtons("Save");
    $('#hiddenDataTypeID').val("");
    $("#dxDataTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxDataTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxDataTypeDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxDataTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDataTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxDataTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxDataTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateDataTypeFields(data) {
    $('#hiddenDataTypeID').val(data.ID);
    $("#dxDataTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxDataTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxDataTypeDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}

function GetDataTypeDTO() {
    let _dataTypeDTO = {
        ID: $('#hiddenDataTypeID').val(),
        Name: $("#dxDataTypeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDataTypeDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxDataTypeIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _dataTypeDTO;
}

async function ShowDataTypeDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this data type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDataType_Global();
    } else {
        ClearDataTypeFields();
    }
}

async function CreateDataType_Global() {
    await dxLoadPanel.show();
    const _dataTypeDTO = GetDataTypeDTO();
    const _validation_ResultDTO = await CreateDataType(_dataTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearDataTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDataType_Global() {
    await dxLoadPanel.show();
    const _dataTypeDTO = GetDataTypeDTO();
    const _validation_ResultDTO = await UpdateDataType(_dataTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearDataTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDataType_Global() {
    await dxLoadPanel.show();
    const _dataTypeDTO = GetDataTypeDTO();
    const _validation_ResultDTO = await DeleteDataType(_dataTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearDataTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}