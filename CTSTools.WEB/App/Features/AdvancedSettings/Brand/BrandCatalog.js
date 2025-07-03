import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreateBrand, UpdateBrand, DeleteBrand, GetDXBrandDataSource } from '../Brand/Brand_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeBrandCatalogControls();
});

async function InitializeBrandCatalogControls() {
    $("#dxBrandNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxBrandDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxBrandIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxBrandGrid").dxDataGrid({
        dataSource: await GetDXBrandDataSource(),
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
            fileName: "BrandCatalog",
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
            let _BrandData = data.selectedRowsData[0];
            if (_BrandData != null) {
                BrandActionButtons("Update");
                PopulateBrandFields(_BrandData);
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
                                    $('#SaveBrandRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenBrandID').value = options.data.ID;
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
    document.getElementById("btnCloseBrandModal").addEventListener("click", ClearBrandFields);
    BrandActionButtons("Save");
}
function BrandActionButtons(Action) {
    $("#BrandActionButtons").empty();
    document.getElementById('BrandModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewBrandBtn").addEventListener("click", ClearBrandFields);
        document.getElementById('BrandModalTitle').innerText = 'Add Brand';
        document.getElementById("BrandActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateBrandButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearBrandButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearBrandButton").addEventListener("click", ClearBrandFields);
        document.getElementById("CreateBrandButton").addEventListener("click", CreateBrand_Global);
    }
    else {
        // Update
        document.getElementById('BrandModalTitle').innerText = 'Update Brand';
        document.getElementById("BrandActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateBrandButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearBrandButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearBrandButton").addEventListener("click", ClearBrandFields);
        document.getElementById("UpdateBrandButton").addEventListener("click", UpdateBrand_Global);
    }
}
function ClearBrandFields() {
    $('#SaveBrandRecordModal').modal('hide');
    BrandActionButtons("Save");
    $('#hiddenBrandID').val("");
    $("#dxBrandIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxBrandNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxBrandDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxBrandGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxBrandGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxBrandGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxBrandGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateBrandFields(data) {
    $('#hiddenBrandID').val(data.ID);
    $("#dxBrandIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxBrandNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxBrandDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}

function GetBrandDTO() {
    let _BrandDTO = {
        ID: $('#hiddenBrandID').val(),
        Name: $("#dxBrandNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxBrandDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxBrandIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _BrandDTO;
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this data type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteBrand_Global();
    } else {
        ClearBrandFields();
    }
}

async function CreateBrand_Global() {
    await dxLoadPanel.show();
    const _BrandDTO = GetBrandDTO();
    const _validation_ResultDTO = await CreateBrand(_BrandDTO);
    if (_validation_ResultDTO.Result) {
        ClearBrandFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateBrand_Global() {
    await dxLoadPanel.show();
    const _BrandDTO = GetBrandDTO();
    const _validation_ResultDTO = await UpdateBrand(_BrandDTO);
    if (_validation_ResultDTO.Result) {
        ClearBrandFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteBrand_Global() {
    await dxLoadPanel.show();
    const _BrandDTO = GetBrandDTO();
    const _validation_ResultDTO = await DeleteBrand(_BrandDTO);
    if (_validation_ResultDTO.Result) {
        ClearBrandFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}