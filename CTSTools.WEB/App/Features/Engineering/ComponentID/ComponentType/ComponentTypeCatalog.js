import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { CreateComponentType, UpdateComponentType, DeleteComponentType, GetDXComponentTypeDataSource } from '../ComponentType/ComponentType_Service.js'
import { GetDXPartTypeDataSource } from '../PartType/PartType_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeComponentTypeCatalogControls();
});

async function InitializeComponentTypeCatalogControls() {
    $("#dxComponentTypeNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxCodeTextBox").dxTextBox({
        placeholder: 'Type code..'
    });
    $("#dxPartTypeSelectBox").dxSelectBox({
        dataSource: await GetDXPartTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxComponentTypeDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxComponentTypeIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxComponentTypeGrid").dxDataGrid({
        dataSource: await GetDXComponentTypeDataSource(),
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
            fileName: "ComponentTypeCatalog",
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
            let _ComponentTypeData = data.selectedRowsData[0];
            if (_ComponentTypeData != null) {
                ComponentTypeActionButtons("Update");
                PopulateComponentTypeFields(_ComponentTypeData);
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
                                    $('#SaveComponentTypeRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenComponentTypeID').value = options.data.ID;
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
                    caption: "Code",
                    dataField: "Code"
                },
                {
                    caption: "Attribute",
                    dataField: "AttributeName"
                },
                {
                    caption: "Value",
                    dataField: "ValueName"
                },
                {
                    caption: "Part Type",
                    dataField: "PartTypeName"
                },
                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Added By ID",
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
                    caption: "Last Update By ID",
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
                }
            ],
    });
    document.getElementById("btnCloseComponentTypeModal").addEventListener("click", ClearComponentTypeFields);
    ComponentTypeActionButtons("Save");
}
function ComponentTypeActionButtons(Action) {
    $("#ComponentTypeActionButtons").empty();
    document.getElementById('ComponentTypeModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewComponentTypeBtn").addEventListener("click", ClearComponentTypeFields);
        document.getElementById('ComponentTypeModalTitle').innerText = 'Add Component Type';
        document.getElementById("ComponentTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateComponentTypeButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearComponentTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearComponentTypeButton").addEventListener("click", ClearComponentTypeFields);
        document.getElementById("CreateComponentTypeButton").addEventListener("click", CreateComponentType_Global);
    }
    else {
        // Update
        document.getElementById('ComponentTypeModalTitle').innerText = 'Update Component Type';
        document.getElementById("ComponentTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateComponentTypeButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearComponentTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearComponentTypeButton").addEventListener("click", ClearComponentTypeFields);
        document.getElementById("UpdateComponentTypeButton").addEventListener("click", UpdateComponentType_Global);
    }
}
function ClearComponentTypeFields() {
    $('#SaveComponentTypeRecordModal').modal('hide');
    ComponentTypeActionButtons("Save");
    $('#hiddenComponentTypeID').val("");
    $('#hiddenValueID').val("");
    $("#dxPartTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxComponentTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxComponentTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCodeTextBox").dxTextBox("instance").option("value", '');
    $("#dxComponentTypeDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxComponentTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxComponentTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxComponentTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxComponentTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateComponentTypeFields(data) {
    $('#hiddenComponentTypeID').val(data.ID);
    $('#hiddenValueID').val(data.ValueID);
    $("#dxComponentTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxComponentTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCodeTextBox").dxTextBox("instance").option("value", data.Code);
    $("#dxComponentTypeDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxPartTypeSelectBox").dxSelectBox("instance").option("value", data.PartTypeID);
}

function GetComponentTypeDTO() {
    let _ComponentTypeDTO = {
        ID: $('#hiddenComponentTypeID').val(),
        ValueID: $('#hiddenValueID').val(),
        Name: $("#dxComponentTypeNameTextBox").dxTextBox("instance").option("value"),
        Code: $("#dxCodeTextBox").dxTextBox("instance").option("value"),
        PartTypeID: $("#dxPartTypeSelectBox").dxSelectBox("instance").option("value"),
        Description: $("#dxComponentTypeDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxComponentTypeIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _ComponentTypeDTO;
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this component type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteComponentType_Global();
    } else {
        ClearComponentTypeFields();
    }
}

async function CreateComponentType_Global() {
    await dxLoadPanel.show();
    const _ComponentTypeDTO = GetComponentTypeDTO();
    const _validation_ResultDTO = await CreateComponentType(_ComponentTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearComponentTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateComponentType_Global() {
    await dxLoadPanel.show();
    const _ComponentTypeDTO = GetComponentTypeDTO();
    const _validation_ResultDTO = await UpdateComponentType(_ComponentTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearComponentTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteComponentType_Global() {
    await dxLoadPanel.show();
    const _ComponentTypeDTO = GetComponentTypeDTO();
    const _validation_ResultDTO = await DeleteComponentType(_ComponentTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearComponentTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}