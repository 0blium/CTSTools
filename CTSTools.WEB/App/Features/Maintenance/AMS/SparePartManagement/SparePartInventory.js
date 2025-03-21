
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXSupportGroupDataSource } from '../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetDXSparePartDataSource } from './SparePart/SparePart_Service.js'
import { GetUserInformation } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { Role_Enum } from '../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'
import { GetDXSparePartInventoryDataSource, CreateSparePartInventory, UpdateSparePartInventory, DeleteSparePartInventory } from './SparePartInventory/SparePartInventory_Service.js'
import { GetDXSparePart_LotDataSource, CreateSparePart_Lot, UpdateSparePart_Lot, DeleteSparePart_Lot } from './SparePart_Lot/SparePart_Lot_Service.js'
import { GetDXTransactionOriginDataSource } from '../ItemManagement/TransactionOrigin/TransactionOrigin_Service.js'
import { GetDXProviderDataSource } from './Provider/Provider_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    await InitializeSparePartControls();
    await InitializeSparePartIventoryControls();
    await InitializeSparePartIventoryModalControls();
    await InitializeSparePartLotControls();
    await GetUserInformationbyID();
    EventHandler();
});

async function EventHandler() {
    document.getElementById("CloseModalXButton").addEventListener("click", ClearSparePartInventoryModalFields);
    document.getElementById("Inventory-Tab").addEventListener("click", GetSparePartInventoryInformation);
    document.getElementById("SparePart-lot-tab").addEventListener("click", GetSparePartLotInformation);
}

async function GetUserInformationbyID() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _userInformation = await GetUserInformation(_userDTO);
    //await DisabledAdministrationFields(_userInformation[0]);
    await FilterDataSourceBySupportGroups(_userInformation[0]);
    dxLoadPanel.hide();
}
function GetUserDTO() {
    let _userDTO = {
        ID: document.getElementById('hiddenUserID').value,
        GetSupportGroupArray: true,
        GetRoleArray: true
    }
    return _userDTO;
}
async function FilterDataSourceBySupportGroups(UserDTO) {
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.SupportGroupIDArray != null && !UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            if (UserDTO.SupportGroupIDArray.length > 0) {
                $("#dxSparePartInventorySupportGroupModalSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxSparePartInventoryGrid").dxDataGrid("instance").option("dataSource", await GetDXSparePartInventoryDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetSupportGroupDTO: true, GetSparePartDTO: true, SparePartDTO: { GetImage: true } }));
            }
        }
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            $("#dxSparePartInventorySupportGroupModalSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource());
            $("#dxSparePartInventoryGrid").dxDataGrid("instance").option("dataSource", await GetDXSparePartInventoryDataSource({ GetSupportGroupDTO: true, GetSparePartDTO: true, SparePartDTO: { GetImage: true } }));
        }
    }
}




//#region Spare Part Inventory

async function InitializeSparePartIventoryControls() {
    $("#dxSparePartInventoryMaxQtyNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePartInventoryMinQtyNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePartInventoryAvailableQtyTextBox").dxTextBox({
        value: 0,
        readOnly: true,
    });
    $("#dxSparePartInventoryIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxSparePartInventoryGrid").dxDataGrid({
        dataSource: [],
        remoteOperations: true,
        keyExpr: "ID",
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
            fileName: "SparePartInventoryCatalog",
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
            let _SparePartInventoryData = data.selectedRowsData[0];
            if (_SparePartInventoryData != null) {
                $('.nav-tabs a[href="#SparePart-tab"]').tab('show');
                SparePartActionButtons("Update");
                PopulateSparePartFields(_SparePartInventoryData);
            }
        },
        columns:
            [
                //{
                //    caption: "Delete",
                //    alignment: "center",
                //    allowFiltering: false,
                //    allowSorting: false,
                //    width: 70,
                //    cellTemplate: function (container, options) {
                //        container.height(30);
                //        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenSparePartInventoryID").val(options.data.ID);
                //                ShowSparePartInventoryDeleteQuestion();
                //            }).appendTo(container);
                //    },
                //},
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
                    caption: "Support Group",
                    dataField: "SupportGroupDTO.EnglishName",
                    //groupIndex: 0
                },
                {
                    caption: "SparePart",
                    dataField: "SparePartDTO.Name"
                },
                {
                    caption: "Manufacture ID",
                    dataField: "SparePartDTO.ManufactureID"
                },
                {
                    caption: "Description",
                    dataField: "SparePartDTO.Description"
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


    $("#dxSparePartInventorySparePartGrid").dxDataGrid({
        dataSource: [],
        remoteOperations: true,
        keyExpr: "ID",
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        paging: {
            pageSize: 5,
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
            fileName: "SparePartInventoryCatalog",
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
            let _SparePartInventoryData = data.selectedRowsData[0];
            if (_SparePartInventoryData != null) {
                SparePartInventoryActionButtons("Update");
                PopulateSparePartInventoryFields(_SparePartInventoryData);
            }
        },
        columns:
            [
                //{
                //    caption: "Delete",
                //    alignment: "center",
                //    allowFiltering: false,
                //    allowSorting: false,
                //    width: 70,
                //    cellTemplate: function (container, options) {
                //        container.height(30);
                //        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenSparePartInventoryID").val(options.data.ID);
                //                ShowSparePartInventoryDeleteQuestion();
                //            }).appendTo(container);
                //    },
                //},
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
                    caption: "SparePart",
                    dataField: "SparePartDTO.Name"
                },
                {
                    caption: "Support Group",
                    dataField: "SupportGroupDTO.EnglishName",
                },
                {
                    caption: "Minimum",
                    dataField: "MinQty",
                },
                {
                    caption: "Maximum",
                    dataField: "MaxQty",
                },
                {
                    caption: "Available Quantity",
                    dataField: "AvailableQty",
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
}

function SparePartActionButtons(Action) {
    $("#SparePartActionButtons").empty();
    if (Action == "Update") {
        document.getElementById("SparePartActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearSparePartButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSparePartButton").addEventListener("click", ClearSparePartFields);
    }
}

function SparePartInventoryActionButtons(Action) {
    $("#SparePartInventoryActionButtons").empty();
    if (Action == "Update") {
        document.getElementById("SparePartInventoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearSparePartInventoryButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSparePartInventoryButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearSparePartInventoryButton").addEventListener("click", ClearSparePartInventoryFields);
        document.getElementById("UpdateSparePartInventoryButton").addEventListener("click", UpdateSparePartInventory_Global);
    }
}

function PopulateSparePartInventoryFields(data) {

    $('#hiddenSparePartInventoryID').val(data.ID);
    $('#hiddenSparePartID').val(data.SparePartDTO.ID);
    $('#hiddenSupportGroupID').val(data.SupportGroupDTO.ID);
    $("#dxSparePartInventoryMaxQtyNumberBox").dxNumberBox("instance").option("value", data.MaxQty)
    $("#dxSparePartInventoryMinQtyNumberBox").dxNumberBox("instance").option("value", data.MinQty)
    $("#dxSparePartInventoryAvailableQtyTextBox").dxTextBox("instance").option("value", data.AvailableQty)
    $("#dxSparePartInventoryIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
function PopulateSparePartFields(data) {
    TabMode("Active");
    $('#hiddenSparePartInventoryID').val(data.ID);
    $('#hiddenSparePartID').val(data.SparePartDTO.ID);
    $('#hiddenSupportGroupID').val(data.SupportGroupDTO.ID);
    $("#dxSparePartNameTextBox").dxTextBox("instance").option("value", data.SparePartDTO.Name)
    $("#dxSparePartManufactureIDTextBox").dxTextBox("instance").option("value", data.SparePartDTO.ManufactureID)
    $("#dxSparePartDescriptionTextArea").dxTextArea("instance").option("value", data.SparePartDTO.Description)
    $("#dxSparePartIsActiveCheckBox").dxCheckBox("instance").option("value", data.SparePartDTO.IsActive)
    if (data.SparePartDTO.SparePartImage != null) {
        $("#SparePartThumbnail").attr('src', data.SparePartDTO.SparePartImage);
    } else {
        $("#SparePartThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    }
}

function ClearSparePartFields() {
    $("#SparePartActionButtons").empty();
    TabMode("Disabled");
    $("#SparePartThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    $("#dxSparePartNameTextBox").dxTextBox("instance").option("value", '')
    $("#dxSparePartManufactureIDTextBox").dxTextBox("instance").option("value", '')
    $("#dxSparePartDescriptionTextArea").dxTextArea("instance").option("value", '')
    $("#dxSparePartIsActiveCheckBox").dxCheckBox("instance").option("value", true)
    let keys = $("#dxSparePartInventoryGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSparePartInventoryGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSparePartInventoryGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSparePartInventoryGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}



function ClearSparePartInventoryFields() {
    $("#SparePartInventoryActionButtons").empty();
    //$('#hiddenSparePartInventoryID').val("0");
    //$('#hiddenSparePartID').val("0");
    //$('#hiddenSupportGroupID').val("0");
    $("#dxSparePartInventoryMaxQtyNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePartInventoryMinQtyNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePartInventoryAvailableQtyTextBox").dxTextBox("instance").option("value", '0');
    $("#dxSparePartInventoryIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxSparePartInventorySparePartGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSparePartInventorySparePartGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSparePartInventorySparePartGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSparePartInventorySparePartGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function GetSparePartInventoryDTO() {
    let _sparePartInventoryDTO = {
        ID: $('#hiddenSparePartInventoryID').val(),
        SupportGroupDTO: {
            ID: $('#hiddenSupportGroupID').val()
        },
        SparePartDTO: {
            ID: $('#hiddenSparePartID').val()
        },
        MaxQty: $("#dxSparePartInventoryMaxQtyNumberBox").dxNumberBox("instance").option("value"),
        MinQty: $("#dxSparePartInventoryMinQtyNumberBox").dxNumberBox("instance").option("value"),
        AvailableQty: $("#dxSparePartInventoryAvailableQtyTextBox").dxTextBox("instance").option("value"),
        IsActive: $("#dxSparePartInventoryIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _sparePartInventoryDTO;
}



//Modal

async function InitializeSparePartIventoryModalControls() {
    $("#dxSparePartInventorySparePartTextBox").dxTextBox({
        placeholder: 'select a spare part from the table',
    });
    $("#dxSparePartInventoryMinQtyModalNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePartInventoryMaxQtyModalNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePartInventorySupportGroupModalSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName", "SpanishName"],
        searchMode: 'contains'
    });
    $("#dxSparePartInventoryModalGrid").dxDataGrid({
        dataSource: await GetDXSparePartDataSource({ GetImage: true }),
        keyExpr: "ID",
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        paging: {
            pageSize: 5,
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
            fileName: "SparePartCatalog",
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
            let _SparePartData = data.selectedRowsData[0];
            if (_SparePartData != null) {
                PopulateSparePartInventoryModalFields(_SparePartData);
            }
        },
        columns:
            [
                //{
                //    caption: "Delete",
                //    alignment: "center",
                //    allowFiltering: false,
                //    allowSorting: false,
                //    width: 70,
                //    cellTemplate: function (container, options) {
                //        container.height(30);
                //        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenSparePartID").val(options.data.ID);
                //                ShowSparePartDeleteQuestion();
                //            }).appendTo(container);
                //    },
                //},
                {
                    caption: 'Spare Part image',
                    width: 100,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate(container, options) {
                        if (options.data.SparePartImage != null) {
                            $('<div>')
                                .append($('<img>', { src: options.data.SparePartImage, height: 80, width: 80 }))
                                .appendTo(container);
                        } else {
                            $('<div>')
                                .append($('<img>', { src: '/App/Common/Assets/img/no-product-image.png', height: 80 }))
                                .appendTo(container);
                        }
                    },
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
                    caption: "Manufacture ID",
                    dataField: "ManufactureID"
                },
                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive"
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

    SparePartInventoryModalActionButtons("Save");
}
function SparePartInventoryModalActionButtons(Action) {
    $("#SparePartInventoryModalActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("SparePartInventoryModalActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSparePartInventoryButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSparePartInventoryButton").addEventListener("click", CreateSparePartInventory_Global);
    }

}

function PopulateSparePartInventoryModalFields(data) {
    $('#hiddenSparePartModalID').val(data.ID);
    $("#dxSparePartInventorySparePartTextBox").dxTextBox("instance").option("value", data.Name);
}

function ClearSparePartInventoryModalFields() {
    SparePartInventoryModalActionButtons("Save");
    $('#hiddenSparePartModalID').val("0");
    $("#dxSparePartInventorySparePartTextBox").dxTextBox("instance").reset();
    $("#dxSparePartInventoryMinQtyModalNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePartInventoryMaxQtyModalNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePartInventorySupportGroupModalSelectBox").dxSelectBox("instance").option("value", '');
    let keys = $("#dxSparePartInventoryModalGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSparePartInventoryModalGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSparePartInventoryModalGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSparePartInventoryModalGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function GetSparePartInventoryModalDTO() {
    let _sparePartInventoryDTO = {
        SupportGroupDTO: {
            ID: $("#dxSparePartInventorySupportGroupModalSelectBox").dxSelectBox("instance").option("value")
        },
        SparePartDTO: {
            ID: $('#hiddenSparePartModalID').val()
        },
        MaxQty: $("#dxSparePartInventoryMaxQtyModalNumberBox").dxNumberBox("instance").option("value"),
        MinQty: $("#dxSparePartInventoryMinQtyModalNumberBox").dxNumberBox("instance").option("value"),
        IsActive: true
    }
    return _sparePartInventoryDTO
}

async function CreateSparePartInventory_Global() {
    await dxLoadPanel.show();
    const _SparePartInventoryDTO = GetSparePartInventoryModalDTO();
    const _validation_ResultDTO = await CreateSparePartInventory(_SparePartInventoryDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartInventoryModalFields();
        $("#dxSparePartInventoryGrid").dxDataGrid("instance").refresh();
        $('#SparePartInventoryModal').modal('hide');
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSparePartInventory_Global() {
    await dxLoadPanel.show();
    const _SparePartInventoryDTO = GetSparePartInventoryDTO();
    const _validation_ResultDTO = await UpdateSparePartInventory(_SparePartInventoryDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartInventoryFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//async function DeleteSparePartInventory_Global() {
//    await dxLoadPanel.show();
//    const _SparePartInventoryDTO = GetSparePartInventoryDTO();
//    const _validation_ResultDTO = await DeleteSparePartInventory(_SparePartInventoryDTO);
//    if (_validation_ResultDTO.Result) {
//        ClearSparePartInventoryFields();
//    }
//    HostResponse(_validation_ResultDTO);
//    dxLoadPanel.hide();
//}
//#endregion

//#region Spare Part 
async function InitializeSparePartControls() {
    $("#dxSparePartNameTextBox").dxTextBox({
        placeholder: 'Name'
    });
    $("#dxSparePartDescriptionTextArea").dxTextArea({
        placeholder: 'Description'
    });
    $("#dxSparePartManufactureIDTextBox").dxTextBox({
        placeholder: 'Manufacture ID'
    });
    $("#dxSparePartIsActiveCheckBox").dxCheckBox({
        value: true,
    });

}


async function GetSparePartInventoryInformation() {
    await dxLoadPanel.show();
    ClearSparePartInventoryFields();
    let _ds = await GetDXSparePartInventoryDataSource({ SupportGroupDTO: { ID: $('#hiddenSupportGroupID').val() }, SparePartDTO: { ID: $('#hiddenSparePartID').val() } });
    $("#dxSparePartInventorySparePartGrid").dxDataGrid("instance").option("dataSource", _ds);
    await dxLoadPanel.hide();
}
//#endregion


//#region Spare Part Lot

async function InitializeSparePartLotControls() {
    $("#dxSparePart_LotPartNumberTextBox").dxTextBox({
        placeholder: 'Type Part Number',
    });
    $("#dxSparePart_LotProviderSelectBox").dxSelectBox({
        dataSource: await GetDXProviderDataSource({}),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxSparePart_LotQtyNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePart_LotAvailableQtyTextBox").dxTextBox({
        placeholder: 'Type Available Qty',
        value: 0,
        readOnly: true,
    });
    $("#dxSparePart_LotSerialTextBox").dxTextBox({
        placeholder: 'Serial',
        readOnly: true,
    });
    $("#dxSparePart_LotTransactionOriginSelectBox").dxSelectBox({
        dataSource: await GetDXTransactionOriginDataSource({}),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxSparePart_LotTransactionNumberTextBox").dxTextBox({
        placeholder: 'Type Transaction Number',
    });
    $("#dxSparePart_LotTransactionLineNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
    });
    $("#dxSparePart_LotCostNumberBox").dxNumberBox({
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePart_LotUnitCostNumberBox").dxNumberBox({
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    $("#dxSparePart_LotIsActiveCheckBox").dxCheckBox({
        value: true,
        visible: false
    });
    $("#dxSparePartLotGrid").dxDataGrid({
        dataSource: [],
        remoteOperations: true,
        keyExpr: "ID",
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        paging: {
            pageSize: 7,
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
            fileName: "SparePartLotCatalog",
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
            let _SparePartLotData = data.selectedRowsData[0];
            if (_SparePartLotData != null) {
                SparePartLotActionButtons("Update");
                PopulateSparePartLotFields(_SparePartLotData);
            }
        },
        columns:
            [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenSparePartLotID").val(options.data.ID);
                                ShowSparePartLotDeleteQuestion();
                            }).appendTo(container);
                        $('<button type="button" class="btn btn-info ms-2 " style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fas fa-print"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                var _data = options.data;
                                // Open Label
                                window.open("http://avmx-s05:8044//LabelPrint.aspx?LabelFile=LF-2083-02-A&Dummy=False&WO=" + _data.Serial + "&Qty=1&From=&To=&SkipEvery=&SerialLength=&ESD=True&SAS=False&FullWorkOrder=" + _data.Serial);
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
                    caption: "SparePart",
                    dataField: "SparePartDTO.Name"
                },
                {
                    caption: "Reference #",
                    dataField: "TransactionNumber",
                },
                {
                    caption: "Serial",
                    dataField: "Serial",
                },
                {
                    caption: "Provider",
                    dataField: "ProviderDTO.Name",
                },
                {
                    caption: "Part Number",
                    dataField: "PartNumber",
                },
                {
                    caption: "Quantity",
                    dataField: "Quantity",
                },
                {
                    caption: "Available Quantity",
                    dataField: "AvailableQty",
                },
                {
                    caption: "Cost",
                    dataField: "Cost",
                },
                {
                    caption: "Unit Cost",
                    dataField: "UnitCost",
                },
                {
                    caption: "Support Group",
                    dataField: "SupportGroupDTO.EnglishName",
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
    SparePartLotActionButtons("Save")
}

async function ShowSparePartLotDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Spare Part Lot',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSparePartLot_Global();
    } else {
        ClearSparePartLotFields();
    }
}

function SparePartLotActionButtons(Action) {
    $("#SparePartLotActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("SparePartLotActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSparePartLotButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSparePartLotButton").addEventListener("click", CreateSparePartLot_Global);
    }
    else {
        // Update
        document.getElementById("SparePartLotActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearSparePartLotButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSparePartLotButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearSparePartLotButton").addEventListener("click", ClearSparePartLotFields);
        document.getElementById("UpdateSparePartLotButton").addEventListener("click", UpdateSparePartLot_Global);
    }

}

function PopulateSparePartLotFields(data) {
    $('#hiddenSparePartLotID').val(data.ID);
    $('#hiddenSparePartID').val(data.SparePartDTO.ID);
    $('#hiddenSupportGroupID').val(data.SupportGroupDTO.ID);
    $("#dxSparePart_LotPartNumberTextBox").dxTextBox("instance").option("value", data.PartNumber)
    $("#dxSparePart_LotQtyNumberBox").dxNumberBox("instance").option("value", data.Quantity)
    $("#dxSparePart_LotQtyNumberBox").dxNumberBox("instance").option("readOnly", true)
    $("#dxSparePart_LotAvailableQtyTextBox").dxTextBox("instance").option("value", data.AvailableQty)
    $("#dxSparePart_LotSerialTextBox").dxTextBox("instance").option("value", data.Serial)
    $("#dxSparePart_LotProviderSelectBox").dxSelectBox("instance").option("value", data.ProviderDTO.ID)
    $("#dxSparePart_LotTransactionOriginSelectBox").dxSelectBox("instance").option("value", data.TransactionOriginDTO.ID)
    $("#dxSparePart_LotTransactionNumberTextBox").dxTextBox("instance").option("value", data.TransactionNumber)
    $("#dxSparePart_LotTransactionLineNumberBox").dxNumberBox("instance").option("value", data.TransactionLine)
    $("#dxSparePart_LotCostNumberBox").dxNumberBox("instance").option("value", data.Cost)
    $("#dxSparePart_LotUnitCostNumberBox").dxNumberBox("instance").option("value", data.UnitCost)
    $("#dxSparePart_LotIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}

function ClearSparePartLotFields() {
    SparePartLotActionButtons("Save");
    $('#hiddenSparePartLotID').val("0");
    //$('#hiddenSupportGroupID').val("0");
    //$('#hiddenSparePartID').val("0");
    $("#dxSparePart_LotIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSparePart_LotQtyNumberBox").dxNumberBox("instance").option("readOnly", false)
    $("#dxSparePart_LotUnitCostNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePart_LotPartNumberTextBox").dxTextBox("instance").option("value", '');
    $("#dxSparePart_LotSerialTextBox").dxTextBox("instance").option("value", '');
    $("#dxSparePart_LotTransactionLineNumberBox").dxNumberBox("instance").option("value", '');
    $("#dxSparePart_LotTransactionNumberTextBox").dxTextBox("instance").option("value", '');
    $("#dxSparePart_LotQtyNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePart_LotCostNumberBox").dxNumberBox("instance").option("value", '0');
    $("#dxSparePart_LotAvailableQtyTextBox").dxTextBox("instance").option("value", '0');
    $("#dxSparePart_LotTransactionOriginSelectBox").dxSelectBox("instance").option("value", '');
    $("#dxSparePart_LotProviderSelectBox").dxSelectBox("instance").option("value", '');
    let keys = $("#dxSparePartLotGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSparePartLotGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSparePartLotGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSparePartLotGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function GetSparePartLotDTO() {
    let _sparePartLotDTO = {
        ID: $('#hiddenSparePartLotID').val(),
        SupportGroupDTO: {
            ID: $('#hiddenSupportGroupID').val()
        },
        SparePartDTO: {
            ID: $('#hiddenSparePartID').val()
        },
        ProviderDTO: {
            ID: $("#dxSparePart_LotProviderSelectBox").dxSelectBox("instance").option("value"),
        },
        TransactionOriginDTO: {
            ID: $("#dxSparePart_LotTransactionOriginSelectBox").dxSelectBox("instance").option("value"),
        },
        Quantity: $("#dxSparePart_LotQtyNumberBox").dxNumberBox("instance").option("value"),
        AvailableQty: $("#dxSparePart_LotAvailableQtyTextBox").dxTextBox("instance").option("value"),
        PartNumber: $("#dxSparePart_LotPartNumberTextBox").dxTextBox("instance").option("value"),
        TransactionNumber: $("#dxSparePart_LotTransactionNumberTextBox").dxTextBox("instance").option("value"),
        TransactionLine: $("#dxSparePart_LotTransactionLineNumberBox").dxNumberBox("instance").option("value"),
        Cost: $("#dxSparePart_LotCostNumberBox").dxNumberBox("instance").option("value"),
        UnitCost: $("#dxSparePart_LotUnitCostNumberBox").dxNumberBox("instance").option("value"),
        Serial: $("#dxSparePart_LotSerialTextBox").dxTextBox("instance").option("value"),
        IsActive: $("#dxSparePart_LotIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _sparePartLotDTO
}

async function CreateSparePartLot_Global() {
    await dxLoadPanel.show();
    const _SparePartLotDTO = GetSparePartLotDTO();
    const _validation_ResultDTO = await CreateSparePart_Lot(_SparePartLotDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartLotFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSparePartLot_Global() {
    await dxLoadPanel.show();
    const _SparePartInventoryDTO = GetSparePartLotDTO();
    const _validation_ResultDTO = await UpdateSparePart_Lot(_SparePartInventoryDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartLotFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteSparePartLot_Global() {
    await dxLoadPanel.show();
    const _SparePartLotDTO = GetSparePartLotDTO();
    const _validation_ResultDTO = await DeleteSparePart_Lot(_SparePartLotDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartLotFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function GetSparePartLotInformation() {
    await dxLoadPanel.show();
    ClearSparePartLotFields();
    let _ds = await GetDXSparePart_LotDataSource({ SparePartInventoryDTO: { ID: $('#hiddenSparePartInventoryID').val() }, SparePartDTO: { ID: $('#hiddenSparePartID').val() } });
    $("#dxSparePartLotGrid").dxDataGrid("instance").option("dataSource", _ds);
    await dxLoadPanel.hide();
}


//#endregion
function TabMode(Action) {
    if (Action == 'Active') {
        var _tabs = document.querySelectorAll(".nav-link");
        [].forEach.call(_tabs, function (element) {
            element.classList.remove("disabled");
        })

    } else {
        var _tabs = document.querySelectorAll(".nav-link");
        [].forEach.call(_tabs, function (element) {
            element.classList.add("disabled");
        })
    }
}