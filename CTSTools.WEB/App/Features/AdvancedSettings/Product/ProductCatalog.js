import { GetDXProductDataSource, CreateProduct, UpdateProduct, DeleteProduct } from '../Product/Product_Service.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js';
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js';

//#region Product Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeProductCatalogControls();
});
async function InitializeProductCatalogControls() {
    $("#dxProductIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxProductNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxProductDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxProductGrid").dxDataGrid({
        dataSource: await GetDXProductDataSource_Global({ IsActive: true }),
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
            fileName: "ProductCatalog",
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
            let _ProductData = data.selectedRowsData[0];
            if (_ProductData != null) {
                ProductActionButtons("Update");
                PopulateProductFields(_ProductData);
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
                                    $('#SaveProductRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenProductID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseProductModal").addEventListener("click", ClearProductFields);
    ProductActionButtons("Save");
}
async function PopulateProductFields(data) {
    $("#hiddenProductID").val(data.ID);
    $("#dxProductNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxProductDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxProductIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the Product, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteProduct_Global();
    } else {
        ClearProductFields();
    }
}
function ProductActionButtons(Action) {
    $("#ProductActionButtons").empty();
    document.getElementById('ProductModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewProductBtn").addEventListener("click", ClearProductFields);
        document.getElementById('ProductModalTitle').innerText = 'Add Product'
        document.getElementById("ProductActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateProductButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearProductButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearProductButton").addEventListener("click", ClearProductFields);
        document.getElementById("CreateProductButton").addEventListener("click", CreateProduct_Global);
    }
    else {
        // Update
        document.getElementById('ProductModalTitle').innerText = 'Update Product'
        document.getElementById("ProductActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateProductButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearProductButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearProductButton").addEventListener("click", ClearProductFields);
        document.getElementById("UpdateProductButton").addEventListener("click", UpdateProduct_Global);
    }
}
function ClearProductFields() {
    $('#SaveProductRecordModal').modal('hide');
    ProductActionButtons("Save");
    $("#hiddenProductID").val("");
    $("#dxProductNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxProductDescription").dxTextArea("instance").option("value", '');
    $("#dxProductIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxProductGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxProductGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetProductDTO() {
    let _ProductDTO = {
        ID: $("#hiddenProductID").val(),
        Name: $("#dxProductNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxProductDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxProductIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _ProductDTO;
}
//#endregion

//#region Product CRUD Functions
async function CreateProduct_Global() {
    await dxLoadPanel.show();
    const _ProductDTO = GetProductDTO();
    const _validation_ResultDTO = await CreateProduct(_ProductDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxProductGrid").dxDataGrid("instance").refresh();
        ClearProductFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateProduct_Global() {
    await dxLoadPanel.show();
    const _ProductDTO = GetProductDTO();
    const _validation_ResultDTO = await UpdateProduct(_ProductDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxProductGrid").dxDataGrid("instance").refresh();
        ClearProductFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteProduct_Global() {
    await dxLoadPanel.show();
    const _ProductDTO = GetProductDTO();
    const _validation_ResultDTO = await DeleteProduct(_ProductDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxProductGrid").dxDataGrid("instance").refresh();
        ClearProductFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearProductFields();
    dxLoadPanel.hide();
}
const GetDXProductDataSource_Global = () => {
    return GetDXProductDataSource()
}
//#endregion