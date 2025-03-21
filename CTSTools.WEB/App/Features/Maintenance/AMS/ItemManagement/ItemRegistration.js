//#region Imports
import { GetDXItem_HeaderDataSource, CreateItem_Header, UpdateItem_Header, DeleteItem_Header, GetItem_HeaderFilesInformation, DeleteItem_HeaderFile } from '../ItemManagement/Item_Header/Item_Header_Service.js'
import { CreateItem_Line, UpdateItem_Line, DeleteItem_Line, GetItem_LineFilesTreeView, GetDXItem_LineDataSource, GetItem_LineInformation, ItemDelivery, GetItem_LineFilesInformation, DeleteItem_LineFile } from '../ItemManagement/Item_Line/Item_Line_Service.js'
import { GetDXUserDataSource } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetDXStationDataSource } from '../StationManagement/Station/Station_Service.js'
import { GetDXSupplyTypeDataSource } from '../ItemManagement/SupplyType/SupplyType_Service.js'
import { GetDXStatus_StatusTypeDataSource } from '../../../AdvancedSettings/StatusManagement/Status_StatusType/Status_StatusType_Service.js'
import { GetCurrencyInformation } from '../../../AdvancedSettings/Currency/Currency_Service.js'
import { GetDXItemClassificationDataSource } from '../ItemManagement/ItemClassification/ItemClassification_Service.js'
//import { GetDXEmployeeTressDataSource } from '../../AdvancedSettings/Users/EmployeeTress/EmployeeTress_Service.js'
import { GetDXTransactionOriginDataSource } from '../ItemManagement/TransactionOrigin/TransactionOrigin_Service.js'

import { StatusType_Enum } from '../../../AdvancedSettings/StatusManagement/StatusType/StatusType_Enum.js'
import { Currency_Enum } from '../../../AdvancedSettings/Currency/Currency_Enum.js'
import { SupplyType_Enum } from '../ItemManagement/SupplyType/SupplyType_Enum.js'

import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXSupportGroupDataSource } from '../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
//#endregion

document.addEventListener("DOMContentLoaded", async () => {
    InitializeItemRegistrationControls();
    InitializeItemTypeControls();
    //InitializeDeliverToControls();
    InitializeDepartureLogControls();
    EventHandler();
});
let _fileDTOList = [];
let _fileDTO = {};
function EventHandler() {
    //document.getElementById('SaveDeliverToInfo').addEventListener('click', AssignDeliverToForItem);
    document.getElementById("GetItem_LineInformation").addEventListener("click", GetItemInformation);
    document.getElementById("AddNewItemLineBtn").addEventListener("click", ClearItem_LineFields);
    document.getElementById("AddNewItemHeaderBtn").addEventListener("click", ClearItem_HeaderFields);
    document.getElementById("ClearItem_LineFilters").addEventListener("click", ClearDepartureLogItem_LineFields);
    document.getElementById("OpenFilterItem_Line").addEventListener("click", function () {
        BuildFilterAppliedToItem_LineDataGrid();
        document.getElementById("FiltersItem_LineApply").classList.toggle('hideFilter');
    });
    document.getElementById("Departure-log-tab").addEventListener("click", GetItemInformation);
}
//#region Item Header
async function InitializeItemTypeControls() {
    $("#dxTreeViewList").dxTreeView({
        itemTemplate: function (itemData, itemIndex, itemElement) {

            if (itemData.ParentID == null) {
                itemElement.append(itemData.Name);
            }
            else {
                itemElement.append('<div class="">' +
                    "<i class=\"fas fa-file pe-2\" style=\"color:#265755\"></i>" +
                    '<a href="' + itemData.URL + '" target="_blank" download="' + itemData.Name + '">' +
                    itemData.Name +
                    '</a>' +
                    '</div>');
            }
        },
        dataStructure: 'plain',
        keyExpr: 'TreeViewID',
        displayExpr: 'Name',
        parentIdExpr: 'ParentID',
        expandAllEnabled: true,
    });
    $("#dxFileOptionsSelectBox").dxSelectBox({
        dataSource: [
            "Item",
            "Lines"
        ],
        onSelectionChanged: function (e) {
            if ($("#dxFileOptionsSelectBox").dxSelectBox("instance").option("value") != 0 &&
                $("#dxFileOptionsSelectBox").dxSelectBox("instance").option("value") != null
            ) {
                if (e.selectedItem == "Item") {
                    $("#Item_HeaderAttachment").removeAttr("hidden");
                    $("#TreeView").attr("hidden", true);
                }
                else {
                    $("#Item_HeaderAttachment").attr("hidden", true);
                    $("#TreeView").removeAttr("hidden");
                }
            }
        },
        searchEnabled: false,
        popupWidth: 400,
    });
    $("#dxItem_HeaderDatGrid").dxDataGrid({
        dataSource: await GetDXItem_HeaderDataSource({ GetItemClassificationDTO: true, GetItemHeaderPicture: true }),
        remoteOperations: true,
        paging: {
            pageSize: 10,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        keyExpr: 'ID',
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
            fileName: "ItemAdministration",
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
        onCellClick: function (e) {

        },
        onSelectionChanged: function (data) {
            let _itemAdministrationData = data.selectedRowsData[0];
            if (_itemAdministrationData != null) {
                Item_HeaderActionButtons("Update");
                PopulateItem_HeaderFields(_itemAdministrationData);
                GetItem_HeaderFileList();
                GetItem_LineTreeViewList();
            }
        },
        columns:
            [
                {
                    caption: "Actions",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#AddNewItemHeaderModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenItem_HeaderID").val(options.data.ID);
                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenItem_HeaderID").val(options.data.ID);
                                ShowItem_HeaderDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: 'Item image',
                    width: 200,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate(container, options) {
                        if (options.data.ItemImg != null) {
                            $('<div>')
                                .append($('<img>', { src: options.data.ItemImg, height: 120, width: 120 }))
                                .appendTo(container);
                        } else {
                            $('<div>')
                                .append($('<img>', { src: '/App/Common/Assets/img/no-product-image.png', height: 120 }))
                                .appendTo(container);
                        }
                    },
                },
                {
                    caption: "English Name",
                    dataField: "EnglishName"
                },
                {
                    caption: "Spanish Name",
                    dataField: "SpanishName",
                },
                {
                    caption: "Model",
                    dataField: "Model"
                },
                {
                    caption: "Brand",
                    dataField: "Brand"
                },
                {
                    caption: "Item Classification Spanish",
                    dataField: "ItemClassificationDTO.SpanishName",

                },
                {
                    caption: "Item Classification English",
                    dataField: "ItemClassificationDTO.EnglishName",
                },
                {
                    caption: "Is Active?",
                    dataField: "IsActive"
                },
                {
                    caption: "Is ESD?",
                    dataField: "IsESD"
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
        masterDetail: {
            enabled: true,
            template: MasterDetailTemplate
        }
    });
    $("#dxItem_HeaderEnglishNameTextBox").dxTextBox({
        placeholder: "Type name.."
    });
    $("#dxItem_HeaderSpanishNameTextBox").dxTextBox({
        placeholder: "Type name.."
    });
    $("#dxItem_HeaderModelTextBox").dxTextBox({
        placeholder: "Type model.."
    });
    $("#dxItem_HeaderBrandTextBox").dxTextBox({
        placeholder: "Type brand.."
    });
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox({
        value: true,
        text: "Is Active?"
    });
    $("#dxItem_HeaderItemClassificationSelectBox").dxSelectBox({
        dataSource: await GetDXItemClassificationDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName", "SpanishName"],
        searchMode: 'contains'
    });
    $("#dxItem_HeaderIsESDCheckBox").dxCheckBox({
        value: false,
        text: "Is ESD?",
    });
    $("#dxItem_HeaderAttachmentFileUploader").dxFileUploader({
        selectButtonText: "Select a file",
        labelText: "or drop it here",
        accept: "file",
        uploadedMessage: "Loading",
        multiple: true,
        uploadMode: "useButtons",
        invalidMaxFileSizeMessage: 'The file is too large. Allowed maximun size is 5MB',
        maxFileSize: 5000000,
        showFileList: true,
        width: "100%",
        onValueChanged: function (e) {
            var files = e.value;
            _fileDTOList = [];

            e.element.find(".dx-fileuploader-upload-button").hide();
            $.each(files, function (i, file) {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var _fileDTO = {
                        Name: $("#dxItem_HeaderAttachmentFileUploader").dxFileUploader("instance").option("value")[i].name,
                        Size: $("#dxItem_HeaderAttachmentFileUploader").dxFileUploader("instance").option("value")[i].size,
                        Data: e.target.result,
                    };
                    _fileDTOList.push(_fileDTO);
                }
            })
        },
    })
    $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader({
        selectButtonText: "Select an image",
        labelText: "or drop it here",
        accept: "image",
        uploadedMessage: "Loading",
        multiple: false,
        uploadMode: "instantly",
        invalidMaxFileSizeMessage: 'The file is too large. Allowed maximun size is 5MB',
        maxFileSize: 5000000,
        showFileList: true,
        width: "100%",
        allowedFileExtensions: ['.jpg', '.jpeg', '.png'],
        onValueChanged: function (e) {
            var file = e.value;
            _fileDTO = {};
            if (file.length) {
                let reader = new FileReader();
                reader.readAsDataURL(file[0]);
                reader.onload = function (e) {
                    $("#ItemThumbnail").attr('src', e.target.result);
                    _fileDTO = {
                        Name: $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").option("value")[0].name,
                        Size: $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").option("value")[0].size,
                        Data: e.target.result,
                    };
                }
            }

        },
    });

    Item_HeaderActionButtons("Save");
}
async function MasterDetailTemplate(container, masterDetailOptions) {
    $("#dxItem_HeaderDatGrid").dxDataGrid("instance").updateDimensions();
    $(`<div id="Letter_HeaderLineList${masterDetailOptions.data.ID}">`).dxDataGrid({
        dataSource: await GetDXItem_LineDataSource({ GetItem_SupportGroupDTO: true, Item_HeaderDTO: { ID: masterDetailOptions.data.ID },IsActive : true }),
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        showRowLines: true,
        showColumnLines: true,
        showBorders: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: true,
        columnAutoWidth: true,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        columnMinWidth: 50,
        groupPanel: {
            visible: false
        },
        columnChooser: {
            enabled: false
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: false,
            allowExportSelectedData: false
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: false,
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
                    caption: "Actions",
                    visibleIndex: 1,
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
                    cellTemplate: function (container, options) {
                        container.height(30);
                        //ShowDeliverToButton(container, options);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#AddNewItemLineModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', async function () {
                                $("#hiddenItem_LineID").val(options.data.ID);
                                await PopulateItem_LineFields(options.data);
                                GetItem_LineFileList();
                            }).appendTo(container);

                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenItem_LineID").val(options.data.ID);
                                ShowItem_LineDeleteQuestion();
                            }).appendTo(container);

                    },
                },
                {
                    // Dropdown
                    caption: "Labels",
                    width: 110,
                    visibleIndex: 0,
                    alignment: "center",
                    editCellTemplate: function (cellElement, cellInfo) {
                        let wrapper = $('<div class="d-flex justify-content-center align-items-center">').appendTo(cellElement);
                        $('<div class="d-flex justify-content-center">').appendTo(wrapper).dxMenu({
                            items: [{
                                // Dropdown tittle
                                text: 'Print',
                                icon: 'print',
                                // Declaration of the list of options for the dropdown
                                items: [{
                                    text: 'S',
                                    LabelFile: 'LF-0001-15-A'
                                }, {
                                    text: 'M',
                                    LabelFile: 'LF-2173-03-A'
                                }, {
                                    text: 'L',
                                    LabelFile: 'LF-2173-02-A'
                                }, {
                                    text: 'S ESD',
                                    LabelFile: 'LF-0001-13-A'
                                }, {
                                    text: 'M ESD',
                                    LabelFile: 'LF-2173-01-A'
                                }, {
                                    text: 'L ESD',
                                    LabelFile: 'LF-2173-00-A'
                                }]
                            }],
                            showFirstSubmenuMode: 'onHover',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                var _data = cellInfo.data;
                                // Validates if the LabelFile property exists in the selected option
                                if (e.itemData.LabelFile != null) {
                                    // Open the label
                                    window.open('http://avmx-s05:8044//LabelPrint.aspx?LabelFile=' + e.itemData.LabelFile + "&Dummy=False&WO=" + _data.Serial + "&Qty=1&From=&To=&SkipEvery=&SerialLength=&ESD=True&SAS=False&FullWorkOrder=" + _data.Serial);
                                }
                            },
                            onItemRendered: function (e) {
                                let menuItemText = e.itemElement.find('.dx-menu-item-text');
                                if (menuItemText.text().trim() === 'Print') {
                                    menuItemText.append('<i class="ms-1 fa fa-angle-down"></i>');
                                }
                            }
                        });
                    },
                    showEditorAlways: true,
                    allowEditing: false
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive",
                    visible: false
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    sortIndex: 0,
                    sortOrder: "desc",
                    visible: false
                },
                {
                    caption: "Manufacture Serial ID",
                    dataField: "ManufactureSerialID"
                },
                {
                    caption: "Legacy ID",
                    dataField: "LegacyID"
                },
                {
                    caption: "Serial",
                    dataField: "Serial"
                },
                {
                    caption: "Owner",
                    dataField: "OwnerDTO.Name",
                },
                {
                    caption: "Station",
                    dataField: "StationDTO.Name"
                },
                {
                    caption: "Introduction Date",
                    dataField: "IntroductionDate",
                    dataType: 'datetime'
                },
                {
                    caption: "Status",
                    dataField: "StatusDTO.Name"
                },
                {
                    caption: "Supply Type",
                    dataField: "SupplyTypeDTO.Name"
                },
                {
                    caption: "Import Invoice",
                    dataField: "ImportInvoice"
                },
                {
                    caption: "Import Invoice Line",
                    dataField: "ImportInvoiceLine"
                },
                {
                    caption: "Shipment Receipt",
                    dataField: "ShipmentReceiptNumber"
                },
                {
                    caption: "Declaration Number",
                    dataField: "DeclarationNumber"
                },
                {
                    caption: "Transaction Origin",
                    dataField: "TransactionOriginDTO.Name"
                },
                {
                    caption: "Transaction Number",
                    dataField: "TransactionNumber"
                },
                {
                    caption: "Transaction Line",
                    dataField: "TransactionLine"
                },
                {
                    caption: "COO",
                    dataField: "COO"
                },

                {
                    caption: "Base Price MXN",
                    dataField: "BasePriceMXN",
                    format: {
                        type: 'currency',
                        precision: 2
                    }
                },
                {
                    caption: "Base Price USD",
                    dataField: "BasePriceUSD",
                    format: {
                        type: 'currency',
                        precision: 2
                    }
                },
                {
                    caption: "Comments",
                    dataField: "Comments"
                },
                {
                    caption: "Delivered Date",
                    dataField: "DeliveredDate",
                    dataType: 'datetime',
                },
                {
                    caption: "Support Group",
                    dataField: "Item_SupportGroupDTO.SupportGroupDTO.EnglishName"
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
    }).appendTo(container);
}
async function PopulateItem_HeaderFields(data) {
    $("#hiddenItem_HeaderID").val(data.ID);//new
    $("#dxItem_HeaderEnglishNameTextBox").dxTextBox("instance").option("value", data.EnglishName);
    $("#dxItem_HeaderSpanishNameTextBox").dxTextBox("instance").option("value", data.SpanishName);
    $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value", data.Model);
    $("#dxItem_HeaderBrandTextBox").dxTextBox("instance").option("value", data.Brand);
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value", data.IsESD)
    await $("#dxItem_HeaderItemClassificationSelectBox").dxSelectBox("instance").option("value", data.ItemClassificationDTO.ID);
    if (data.ItemImg != null) {
        $("#ItemThumbnail").attr('src', data.ItemImg);
    } else {
        $("#ItemThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    }
    ClearErrorFeedback();
}
async function ShowItem_HeaderDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this item from support group',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteItem_Header_Global();
    } else {
        ClearItem_HeaderFields(true);
    }
}
function Item_HeaderActionButtons(Action) {
    $("#Item_HeaderActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("Item_HeaderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearItem_HeaderButton" data-bs-dismiss="modal" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="CreateItem_HeaderButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateItem_HeaderButton").addEventListener("click", CreateItem_Header_Global);
        document.getElementById("ClearItem_HeaderButton").addEventListener("click", function () {
            ClearItem_HeaderFields(false)
        });

    }
    else {
        // Update
        document.getElementById("Item_HeaderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearItem_HeaderButton" data-bs-dismiss="modal" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateItem_HeaderButton"  type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearItem_HeaderButton").addEventListener("click", function () {
            ClearItem_HeaderFields(false)
        });
        document.getElementById("UpdateItem_HeaderButton").addEventListener("click", UpdateItem_Header_Global);
    }
}
function ClearItem_HeaderFields() {
    Item_HeaderActionButtons("Save");
    $("#hiddenItem_HeaderID").val("0");
    $("#dxItem_HeaderEnglishNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_HeaderSpanishNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_HeaderBrandTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value", false);
    $("#dxItem_HeaderItemClassificationSelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxItem_HeaderDatGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxItem_HeaderDatGrid").dxDataGrid("instance").deselectRows(keys);
    var dataGrid = $("#dxItem_HeaderDatGrid").dxDataGrid("instance");
    dataGrid.collapseAll(-1); // -1 always works
    $("#dxItem_HeaderDatGrid").dxDataGrid("instance").refresh();
    $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").reset();
    $("#TreeView").attr("hidden", true);
    $("#Item_HeaderAttachment").attr("hidden", true);
    _fileDTOList = [];
    _fileDTO = {};
    $("#ItemThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    $("#dxItem_HeaderDatGrid").dxDataGrid("instance").updateDimensions();
    ClearErrorFeedback();
}
function GetItem_HeaderDTO() {
    let _item_HeaderDTO = {
        ID: $("#hiddenItem_HeaderID").val(),
        EnglishName: $("#dxItem_HeaderEnglishNameTextBox").dxTextBox("instance").option("value"),
        SpanishName: $("#dxItem_HeaderSpanishNameTextBox").dxTextBox("instance").option("value"),
        Model: $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value"),
        Brand: $("#dxItem_HeaderBrandTextBox").dxTextBox("instance").option("value"),
        IsActive: $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value"),
        IsESD: $("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value"),
        ItemClassificationDTO: {
            ID: $("#dxItem_HeaderItemClassificationSelectBox").dxSelectBox("instance").option("value"),
        },
        FileDTO: GetFileDTO()
    }
    return _item_HeaderDTO;
}
async function CreateItem_Header_Global() {
    await dxLoadPanel.show();
    const _item_HeaderDTO = GetItem_HeaderDTO();
    const _validation_ResultDTO = await CreateItem_Header(_item_HeaderDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateItem_Header_Global() {
    await dxLoadPanel.show();
    const _item_HeaderDTO = GetItem_HeaderDTO();
    const _validation_ResultDTO = await UpdateItem_Header(_item_HeaderDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true); // Item_Header fields still get cleared even when sent false as a parameter, also fixes multiple Expand buttons showing
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteItem_Header_Global() {
    await dxLoadPanel.show();
    const _item_HeaderDTO = GetItem_HeaderDTO();
    const _validation_ResultDTO = await DeleteItem_Header(_item_HeaderDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//function ShowDeliverToButton(container, options) {
//    if (options.data.DeliveredToDTO.ID == 0) {
//        $('<button type="button" class="btn btn-green me-2" data-bs-toggle="modal" data-bs-target="#ItemDeliverToModal" style="padding-top: 2px; ' +
//            'padding-bottom:5px"><i class="fa fa-user-check"></i><span>' +
//            + '</span></button>')
//            .height(30)
//            .on('dxclick', function () {
//                $("#hiddenItem_LineID").val(options.data.ID);
//                $("#hiddenItem_HeaderID").val(options.data.Item_HeaderDTO.ID);
//            }).appendTo(container);
//    } else {
//        $('<button type="button" class="btn btn-green me-2 disabled" data-bs-toggle="modal" data-bs-target="#ItemDeliverToModal" style="padding-top: 2px; ' +
//            'padding-bottom:5px"><i class="fa fa-user-check"></i><span>' +
//            + '</span></button>')
//            .height(30)
//            .on('dxclick', function () {
//                $("#hiddenItem_LineID").val(options.data.ID);
//            }).appendTo(container);
//    }
//}

//#endregion
//#region Item Lines
async function InitializeItemRegistrationControls() {
    const now = new Date();
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox({
        dataSource: await GetDXItem_HeaderDataSource(),
        valueExpr: "ID",
        displayExpr: "NamesWithModel",
        readOnly: false,
        deferRendering: false,
        searchExpr: ["Model", "EnglishName", "SpanishName"],
        searchMode: 'contains',
        searchEnabled: true
    });
    $("#dxItem_LineOwnerSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["Name"],
        searchMode: 'contains'
    })
    $("#dxItem_LineStationSelectBox").dxSelectBox({
        dataSource: await GetDXStationDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        readOnly: false,
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox({
        dataSource: await GetDXSupplyTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        onValueChanged: function (e) {
            if (e.value != 0 && e.value != null) {
                let _importFields = document.getElementById("SupplyTypeImport")
                let _localFields = document.getElementById("SupplyTypeLocal")
                switch (e.value) {

                    case SupplyType_Enum.Temporary_Import:
                    case SupplyType_Enum.Definitive_Import:
                        //_localFields.classList.add('d-none');
                        _importFields.classList.remove('d-none');
                        //_localFields.classList.remove('d-none');
                        break;
                    default:
                        //_localFields.classList.add('d-none');
                        _importFields.classList.add('d-none');
                        break;
                }
            }

        }
    });
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox({
        placeholder: "Type Manufacture Serial ID.."
    });
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox({
        placeholder: "Type shipment receipt number.."
    })
    $("#dxItem_LineLegacyIDTextBox").dxTextBox({
        placeholder: "Type Legacy ID.."
    });
    $("#dxItem_LineSerialTextBox").dxTextBox({
        readOnly: false,
        placeholder: "Type serial ID..",
        inputAttr: { 'style': "text-transform: uppercase" }
    })
    $("#dxItem_LineTransactionNumberTextBox").dxTextBox({
        placeholder: "Type Transaction Number.."
    });
    $("#dxItem_LineTransactionLineTextBox").dxTextBox({
        placeholder: "Type Transaction Line.."
    });
    $("#dxItem_LineTransactionOriginSelectBox").dxSelectBox({
        dataSource: await GetDXTransactionOriginDataSource({}),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxItem_LineStatusSelectBox").dxSelectBox({
        dataSource: await GetDXStatus_StatusTypeDataSource({
            StatusTypeDTO: {
                ID: StatusType_Enum.Item
            },
        }),
        valueExpr: "StatusDTO.ID",
        displayExpr: "StatusDTO.Name",
        deferRendering: false,
        searchEnabled: true
    })
    $("#dxItem_LineIntroductionDateDateBox").dxDateBox({
        type: "datetime",
        value: now,
        max: now,
        label: "Date and time",
        labelMode: "floating",
    });
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox({
        placeholder: "Type import invoice .."
    })
    $("#dxItem_LineImportInvoiceLineTextBox").dxTextBox({
        placeholder: "Type Import Invoice Line..",
    });
    $("#dxItem_LineDeclarationNumberTextBox").dxTextBox({
        placeholder: "Type declaration number .."
    })
    $("#dxItem_LineBasePriceMXNTextBox").dxTextBox({
        placeholder: "Type Base Price MXN ..",
        onValueChanged: function (e) {
            if (e.event != undefined) {
                let _basePrice = {
                    Price: e.value,
                    Currency: Currency_Enum.MXN
                }
                if (e.value != 0 && $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value") == '') {
                    handleMXNInput(_basePrice)
                }
            }

        }
    });

    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox({
        placeholder: "Type Base Price USD ..",
        onValueChanged: function (e) {
            if (e.event != undefined) {
                let _basePrice = {
                    Price: e.value,
                    Currency: Currency_Enum.USD
                }
                if (e.value != 0 && $("#dxItem_LineBasePriceMXNTextBox").dxTextBox("instance").option("value") == '') {
                    handleUSDInput(_basePrice)
                }
            }

        }
    });
    $("#dxItem_LineCOOTextBox").dxTextBox({
        placeholder: "Type COO .."
    });
    $("#dxItem_LineCommentsTextArea").dxTextArea({
        placeholder: "Type comments..."
    })
    $("#dxItem_LineAttachmentFileUploader").dxFileUploader({
        selectButtonText: "Select a file",
        labelText: "or drop it here",
        accept: "file",
        uploadedMessage: "Loading",
        multiple: true,
        uploadMode: "useButtons",
        invalidMaxFileSizeMessage: 'The file is too large. Allowed maximun size is 5MB',
        maxFileSize: 5000000,
        showFileList: true,
        width: "100%",
        onValueChanged: function (e) {
            var files = e.value;
            _fileDTOList = [];
            e.element.find(".dx-fileuploader-upload-button").hide();
            $.each(files, function (i, file) {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var _fileDTO = {
                        Name: $("#dxItem_LineAttachmentFileUploader").dxFileUploader("instance").option("value")[i].name,
                        Size: $("#dxItem_LineAttachmentFileUploader").dxFileUploader("instance").option("value")[i].size,
                        Data: e.target.result,
                    };
                    _fileDTOList.push(_fileDTO);
                }
            })
        },
    });
    Item_LineActionButtons("Save");
}
async function Item_LineActionButtons(Action) {
    $("#Item_LineActionButtons").empty();
    document.getElementById("Item_LineActionButtons").innerHTML =
        '<button class="btn btn-secondary float-end" id="ClearItem_LineButton" data-bs-dismiss="modal" type="button">Cancel</button>';
    if (Action == "Save") {
        document.getElementById("Item_LineActionButtons").innerHTML +=
            '<button class="btn btn-success me-1 m-b-15 float-end" id="CreateItem_LineButton" type="button">Save</button>';
        document.getElementById("CreateItem_LineButton").addEventListener("click", CreateItem_Line_Global);
        document.getElementById("ClearItem_LineButton").addEventListener("click", ClearItem_LineFields);
    }
    else {
        // Update
        document.getElementById("Item_LineActionButtons").innerHTML +=
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateItem_LineButton" type="button">Update</button>';
        document.getElementById("UpdateItem_LineButton").addEventListener("click", UpdateItem_Line_Global);
        document.getElementById("ClearItem_LineButton").addEventListener("click", ClearItem_LineFields);
    }
}
function GetItem_LineDTO() {
    let _item_LineDTO = {
        ID: $("#hiddenItem_LineID").val(),
        Item_HeaderDTO: {
            ID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value")
        },
        OwnerDTO: {
            ID: $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value"),
        },
        SupplyTypeDTO: {
            ID: $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value"),
        },
        Serial: $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value").toUpperCase(),
        ManufactureSerialID: $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value"),
        ShipmentReceiptNumber: $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value"),
        StatusDTO: {
            ID: $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value")
        },
        StationDTO: {
            ID: $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value")
        },
        IntroductionDate: $("#dxItem_LineIntroductionDateDateBox").dxDateBox("instance").option("value"),
        BasePriceMXN: $("#dxItem_LineBasePriceMXNTextBox").dxTextBox("instance").option("value"),
        ImportInvoice: $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value"),
        ImportInvoiceLine: $("#dxItem_LineImportInvoiceLineTextBox").dxTextBox("instance").option("value"),
        DeclarationNumber: $("#dxItem_LineDeclarationNumberTextBox").dxTextBox("instance").option("value"),
        BasePriceUSD: $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value"),
        COO: $("#dxItem_LineCOOTextBox").dxTextBox("instance").option("value"),
        Comments: $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value"),
        TransactionOriginDTO: {
            ID: $("#dxItem_LineTransactionOriginSelectBox").dxSelectBox("instance").option("value")
        },
        TransactionNumber: $("#dxItem_LineTransactionNumberTextBox").dxTextBox("instance").option("value"),
        TransactionLine: $("#dxItem_LineTransactionLineTextBox").dxTextBox("instance").option("value"),
        LegacyID: $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value"),
        IsActive: true,
        ShipmentReceiptID: null,
        UserDefinedValueList: [],

        FileDTO: GetFileDTO()
    }
    return _item_LineDTO;
}
async function PopulateItem_LineFields(Data) {
    Item_LineActionButtons("Update");
    $("#hiddenItem_LineID").val(Data.ID);
    $("#hiddenStationID").val(Data.StationDTOID);
    $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value", Data.StationDTO.ID)
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", Data.Item_HeaderDTO.ID);
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value", Data.OwnerDTO.ID);
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value", Data.SupplyTypeDTO.ID);
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value", Data.ManufactureSerialID);
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value", Data.ShipmentReceiptNumber);
    $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value", Data.StatusDTO.ID);
    $("#dxItem_LineTransactionOriginSelectBox").dxSelectBox("instance").option("value", Data.TransactionOriginDTO.ID);
    $("#dxItem_LineIntroductionDateDateBox").dxDateBox("instance").option("value", Data.IntroductionDate);
    $("#dxItem_LineBasePriceMXNTextBox").dxTextBox("instance").option("value", Data.BasePriceMXN);
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value", Data.ImportInvoice);
    $("#dxItem_LineImportInvoiceLineTextBox").dxTextBox("instance").option("value", Data.ImportInvoiceLine);
    $("#dxItem_LineDeclarationNumberTextBox").dxTextBox("instance").option("value", Data.DeclarationNumber);
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", Data.BasePriceUSD);
    $("#dxItem_LineCOOTextBox").dxTextBox("instance").option("value", Data.COO);
    $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value", Data.Comments);
    $("#dxItem_LineTransactionNumberTextBox").dxTextBox("instance").option("value", Data.TransactionNumber);
    $("#dxItem_LineTransactionLineTextBox").dxTextBox("instance").option("value", Data.TransactionLine);
    $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value", Data.LegacyID);
    $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value", Data.Serial);

}
function ClearItem_LineFields() {
    Item_LineActionButtons("Save");
    $("#hiddenItem_LineID").val("");
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineTransactionOriginSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineIntroductionDateDateBox").dxDateBox("instance").option("value", new Date());
    $("#dxItem_LineBasePriceMXNTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineImportInvoiceLineTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineDeclarationNumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineCOOTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value", "");
    $("#dxItem_LineTransactionNumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineTransactionLineTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineStationSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineAttachmentFileUploader").dxFileUploader("instance").reset();
    _fileDTOList = [];
    _fileDTO = {};
}
async function ShowItem_LineDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this item',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteItem_Line_Global();
    } else {
        ClearItem_LineFields();
    }
}
async function CreateItem_Line_Global() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItem_LineDTO();
    const _validation_ResultDTO = await CreateItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
        ClearItem_LineFields();
        $("#AddNewItemLineModal").modal("hide");
        GetItem_LineTreeViewList();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateItem_Line_Global() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItem_LineDTO();
    const _validation_ResultDTO = await UpdateItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
        ClearItem_LineFields();
        $("#AddNewItemLineModal").modal("hide");
        GetItem_LineTreeViewList();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteItem_Line_Global() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItem_LineDTO();
    const _validation_ResultDTO = await DeleteItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
        ClearItem_LineFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion
//#region Files
async function GetItem_HeaderFileList() {
    $("#Item_HeaderFilelistSection").empty();

    let _item_HeaderFileDTO = { ID: $("#hiddenItem_HeaderID").val() }
    let _item_HeaderFileList = await GetItem_HeaderFilesInformation(_item_HeaderFileDTO);
    if ($(_item_HeaderFileList).length > 0) {
        $.each(_item_HeaderFileList, function (i, attachment) {
            //Exclusion list for unwanted files to display
            if (attachment.Extension.toLowerCase() != '.db') {
                //Prepare preview for Images START
                if (attachment.Extension.toLowerCase() == '.jpg' || attachment.Extension.toLowerCase() == '.png' || attachment.Extension.toLowerCase() == '.bmp') {
                    $('#Item_HeaderFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4  mb-2" style="height:120px;">' +
                        '<div class="card" style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.URL + '" style="height:90px;"  alt="Responsive image">' +
                        '<label class="text-center text-truncate text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a href="#" id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger file btn-xs float-right"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn  btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                    //Prepare preview for Images END  
                } else {
                    $('#Item_HeaderFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4  mb-2" style="height:120px;">' +
                        '<div class="card" style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.Icon + '" style="height:90px;" alt="Responsive image">' +
                        '<label class="text-center text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a  id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger btn-xs float-right file"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn btn-inverse btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                }
            }
        });
        let _files = document.querySelectorAll(".file");
        _files.forEach(function (_file) {
            _file.addEventListener('click', function () {
                ShowDeleteAttachmentHeaderQuestion({ ID: $("#hiddenItem_HeaderID").val(), Name: this.dataset.filename, Extension: this.dataset.extension });
            });
        });
    }
}
function GetFileDTO() {
    if ((_fileDTO != null || _fileDTO != undefined) && (_fileDTOList != null || _fileDTOList != undefined)) {
        _fileDTO.FileList = _fileDTOList;
    }
    return _fileDTO;
}

async function ShowDeleteAttachmentHeaderQuestion(FileDTO) {
    const _alert = await Swal.fire({
        title: 'Are you sure?',
        text: 'This file will be deleted',
        icon: 'warning',
        confirmButtonText: `Delete`,
        showCancelButton: true,
        reverseButtons: true,
        confirmButtonColor: '#ea4335',
    });
    if (_alert.isConfirmed) {
        DeleteAttachmentHeader(FileDTO);
    }
}
async function DeleteAttachmentHeader(FileDTO) {
    await dxLoadPanel.show();
    const _validation_resultDTO = await DeleteItem_HeaderFile(FileDTO);
    HostResponse(_validation_resultDTO);
    GetItem_HeaderFileList(FileDTO);
    dxLoadPanel.hide()
}

async function GetItem_LineTreeViewList() {
    $("#dxTreeViewList").dxTreeView("instance").option("dataSource", await GetItem_LineFilesTreeView({ Item_HeaderDTO: { ID: $("#hiddenItem_HeaderID").val() } }));
    $("#dxTreeViewList").dxTreeView("expandAll");
}


//#region Line Files 
async function GetItem_LineFileList() {
    $("#Item_LineFilelistSection").empty();

    let _item_LineFileDTO = { ID: $("#hiddenItem_LineID").val() }
    let _item_LineFileList = await GetItem_LineFilesInformation(_item_LineFileDTO);
    if ($(_item_LineFileList).length > 0) {

        $.each(_item_LineFileList, function (i, attachment) {
            //Exclusion list for unwanted files to display
            if (attachment.Extension.toLowerCase() != '.db') {
                //Prepare preview for Images START
                if (attachment.Extension.toLowerCase() == '.jpg' || attachment.Extension.toLowerCase() == '.png' || attachment.Extension.toLowerCase() == '.bmp') {
                    $('#Item_LineFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4 mb-2" style="height:120px;">' +
                        '<div class="card "  style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.URL + '" style="height:90px;"  alt="Responsive image">' +
                        '<label class="text-center text-truncate text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a href="#" id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger file btn-xs float-right"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn  btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                    //Prepare preview for Images END  
                } else {
                    $('#Item_LineFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4 mb-2" style="height:120px;">' +
                        '<div class="card"  style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.Icon + '" style="height:90px;" alt="Responsive image">' +
                        '<label class="text-center text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a  id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger btn-xs float-right file"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn btn-inverse btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                }
            }
        });
        let _files = document.querySelectorAll(".file");
        _files.forEach(function (_file) {
            _file.addEventListener('click', function () {
                ShowDeleteAttachmentLineQuestion({ ID: $("#hiddenItem_LineID").val(), Name: this.dataset.filename, Extension: this.dataset.extension });
            });
        });
    }
}
async function ShowDeleteAttachmentLineQuestion(FileDTO) {
    const _alert = await Swal.fire({
        title: 'Are you sure?',
        text: 'This file will be deleted',
        icon: 'warning',
        confirmButtonText: `Delete`,
        showCancelButton: true,
        reverseButtons: true,
        confirmButtonColor: '#ea4335',
    });
    if (_alert.isConfirmed) {
        DeleteAttachmentLine(FileDTO);
    }
}
async function DeleteAttachmentLine(FileDTO) {
    await dxLoadPanel.show();
    const _validation_resultDTO = await DeleteItem_LineFile(FileDTO);
    HostResponse(_validation_resultDTO);
    GetItem_LineFileList(FileDTO);
    GetItem_LineTreeViewList();
    dxLoadPanel.hide()
}
//#endregion
//#endregion
//#region Currency functions
async function handleMXNInput(BasePrice) {
    let _currencyList = await GetCurrencyInformation({ IsActive: true });

    if (!isNaN(BasePrice.Price)) {
        let _exchangeRateMXN = _currencyList.filter(currencyType => currencyType.Name === Currency_Enum.MXN)
        let _usdEquivalent = BasePrice.Price * _exchangeRateMXN[0].ExchangeRate;
        $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", _usdEquivalent.toFixed(2));
    } else {
        $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", "0");
    }

}
async function handleUSDInput(BasePrice) {
    let _currencyList = await GetCurrencyInformation({ IsActive: true });

    if (!isNaN(BasePrice.Price)) {
        let _exchangeRateUSD = _currencyList.filter(currencyType => currencyType.Name === Currency_Enum.USD)
        let _mxnEquivalent = BasePrice.Price * _exchangeRateUSD[0].ExchangeRate
        $("#dxItem_LineBasePriceMXNTextBox").dxTextBox("instance").option("value", _mxnEquivalent.toFixed(2));
    } else {
        $("#dxItem_LineBasePriceMXNTextBox").dxTextBox("instance").option("value", "0");
    }

}
//#endregion
//#region Deliver to functionality
//async function InitializeDeliverToControls() {
//    $("#dxItem_LineDeliverToIDSelectBox").dxSelectBox({
//        dataSource: await GetDXEmployeeTressDataSource(),
//        valueExpr: "ID",
//        displayExpr: "Name",
//        readOnly: false,
//        deferRendering: false,
//        searchExpr: ["Name", "EmployeeNumber"],
//        searchMode: 'contains',
//        searchEnabled: true
//    });
//    $("#dxItem_LineSupportGroupSelectBox").dxSelectBox({
//        dataSource: await GetDXSupportGroupDataSource({ GetSupportGroupWithStation: true }),
//        valueExpr: "ID",
//        displayExpr: "Names",
//        readOnly: false,
//        deferRendering: false,
//        searchExpr: ["EnglishName", "SpanishName"],
//        searchMode: 'contains',
//        searchEnabled: true
//    });
//}
//function ClearDeliverToFields() {
//    $("#dxItem_LineDeliverToIDSelectBox").dxSelectBox("instance").reset();
//    $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").reset();
//    $("#hiddenItem_LineID").val("");
//    $("#hiddenItem_HeaderID").val("");

//    var dataGrid = $("#dxItem_HeaderDatGrid").dxDataGrid("instance");
//    dataGrid.collapseAll(-1); // -1 always works
//    $("#dxItem_HeaderDatGrid").dxDataGrid("instance").refresh();
//}
//function GetItem_LineDTODeliverTo() {
//    let _item_LineDTO = {
//        ID: $("#hiddenItem_LineID").val(),
//        Item_HeaderDTO: {
//            ID: $("#hiddenItem_HeaderID").val()
//        },
//        DeliveredToDTO: {
//            ID: $("#dxItem_LineDeliverToIDSelectBox").dxSelectBox("instance").option("value")
//        },
//        Item_SupportGroupDTO: {
//            SupportGroupDTO: {
//                ID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value")
//            }
//        },
//    }
//    return _item_LineDTO;
//}
//async function AssignDeliverToForItem() {
//    await dxLoadPanel.show()
//    let _item_LineDTO = await GetItem_LineDTODeliverTo();
//    const _validation_ResultDTO = await ItemDelivery(_item_LineDTO);
//    if (_validation_ResultDTO.Result) {
//        ClearDeliverToFields();
//        GetItemInformation();
//        $("#ItemDeliverToModal").modal("hide");
//    }
//    HostResponse(_validation_ResultDTO);
//    await dxLoadPanel.hide()
//}
//#endregion
function toggleMasterRow(rowKey, event) {
    var dataGrid = $("#dxItem_HeaderDatGrid").dxDataGrid("instance");

    // Check if the click is on the expand/collapse button
    var isClickOnExpandCollapseButton = $(event.target).closest(".dx-datagrid-expand").length > 0;

    // If the click is on the expand/collapse button, proceed with expanding/collapsing
    if (isClickOnExpandCollapseButton) {
        // Collapse the currently open row if there is one
        if (currentlyOpenRowKey !== null && currentlyOpenRowKey !== rowKey) {
            dataGrid.collapseRow(currentlyOpenRowKey);
        }

        // Toggle the clicked row
        if (dataGrid.isRowExpanded(rowKey)) {
            dataGrid.collapseRow(rowKey);
            currentlyOpenRowKey = null; // No row is open
        } else {
            dataGrid.expandRow(rowKey);
            currentlyOpenRowKey = rowKey; // Update the currently open row
        }
    }
}

//#region Item Departure Log
async function InitializeDepartureLogControls() {
    const now = new Date();
    $("#dxItem_LineStartDateDateBox").dxDateBox({
        type: "date",
        format: "MM/dd/yyyy",
        value: new Date(now.getFullYear(), now.getMonth(), 1),
        onValueChanged: function (e) {
            $("#dxItem_LineEndDateDateBox").dxDateBox("instance").option("min", e.value);
        }
    });
    $("#dxItem_LineEndDateDateBox").dxDateBox({
        type: "date",
        format: "MM/dd/yyyy",

        value: new Date(now.getFullYear(), now.getMonth() + 1, 1),
        onValueChanged: function (e) {
            $("#dxItem_LineStartDateDateBox").dxDateBox("instance").option("max", e.value);
        }
    });
    $("#dxItem_LineSupplyTypeTagBox").dxTagBox({
        dataSource: await GetDXSupplyTypeDataSource(),
        displayExpr: "Name",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
    });
    $("#dxItem_LineOwnerTagBox").dxTagBox({
        dataSource: await GetDXUserDataSource(),
        displayExpr: "Name",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
    });
    $("#dxItem_LineStationTagBox").dxTagBox({
        dataSource: await GetDXStationDataSource(),
        displayExpr: "Name",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
    });
    //$("#dxItem_LineDeliveredToTagBox").dxTagBox({
    //    dataSource: await GetDXEmployeeTressDataSource(),
    //    displayExpr: "Name",
    //    valueExpr: "ID",
    //    showSelectionControls: true,
    //    searchEnabled: true,
    //    searchMode: "contains",
    //    popupWidth: 450,
    //    deferRendering: false,
    //});
    $("#dxItemLineGrid").dxDataGrid({
        dataSource: [],
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
            fileName: "OwnerInventoryReport",
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

        },
        onRowPrepared: function (row) {
            if (row.rowType == "data" && row.data.DeliveredToDTO.ID == 0) {
                row.rowElement.css('background', '#fffad5');
            }
        },
        columns:
            [
                {
                    caption: "Actions",
                    visibleIndex: 1,
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
                    cellTemplate: function (container, options) {
                        container.height(30);
                        //ShowDeliverToButton(container, options);
                    }
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive",
                    visible: false
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Delivered Date",
                    dataField: "DeliveredDate",
                    dataType: 'datetime',
                    sortIndex: 0, sortOrder: "desc"
                },
                {
                    caption: "Introduction Date",
                    dataField: "IntroductionDate",
                    dataType: 'datetime',
                },
                {
                    caption: "Facility",
                    dataField: "StationDTO.FacilityDTO.Name"
                },
                {
                    caption: "Station",
                    dataField: "StationDTO.Name"
                },
                {
                    caption: "Serial",
                    dataField: "Serial"
                },
                {
                    caption: "Legacy ID",
                    dataField: "LegacyID"
                },
                {
                    caption: "English Name",
                    dataField: "Item_HeaderDTO.EnglishName"
                },
                {
                    caption: "Spanish Name",
                    dataField: "Item_HeaderDTO.SpanishName"
                },
                {
                    caption: "Owner",
                    dataField: "OwnerDTO.Name",
                },
                {
                    caption: "Support Group",
                    dataField: "Item_SupportGroupDTO.SupportGroupDTO.EnglishName"
                },


                {
                    caption: "Manufacture Serial ID",
                    dataField: "ManufactureSerialID"
                },
                {
                    caption: "Supply Type",
                    dataField: "SupplyTypeDTO.Name"
                },
                {
                    caption: "COO",
                    dataField: "COO"
                },
                {
                    caption: "Import Invoice",
                    dataField: "ImportInvoice"
                },
                {
                    caption: "Shipment Receipt",
                    dataField: "ShipmentReceiptNumber"
                },
                {
                    caption: "Transaction Origin",
                    dataField: "TransactionOriginDTO.Name"
                },
                {
                    caption: "Transaction Number",
                    dataField: "TransactionNumber"
                },
                {
                    caption: "Transaction Line",
                    dataField: "TransactionLine"
                },
                {
                    caption: "Base Price MXN",
                    dataField: "BasePriceMXN",
                    format: {
                        type: 'currency',
                        precision: 2
                    }
                },
                {
                    caption: "Base Price USD",
                    dataField: "BasePriceUSD",
                    format: {
                        type: 'currency',
                        precision: 2
                    }
                },
                {
                    caption: "Comments",
                    dataField: "Comments"
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

function GetDepartureLogItem_LineDTO() {
    let _item_LineDTO = {
        OwnerIDArray: $("#dxItem_LineOwnerTagBox").dxTagBox("instance").option("value"),
        SupplyTypeIDArray: $("#dxItem_LineSupplyTypeTagBox").dxTagBox("instance").option("value"),
        StationIDArray: $("#dxItem_LineStationTagBox").dxTagBox("instance").option("value"),
        //DeliveredToIDArray: $("#dxItem_LineDeliveredToTagBox").dxTagBox("instance").option("value"),
        StartIntroductionDate: $("#dxItem_LineStartDateDateBox").dxDateBox("instance").option("value").toJSON(),
        EndIntroductionDate: $("#dxItem_LineEndDateDateBox").dxDateBox("instance").option("value").toJSON(),
        GetStationDTO: true,
        GetItem_SupportGroupDTO: true,
    }
    return _item_LineDTO;
}
function ClearDepartureLogItem_LineFields() {
    const now = new Date();
    $("#dxItem_LineOwnerTagBox").dxTagBox("instance").reset()
    $("#dxItem_LineSupplyTypeTagBox").dxTagBox("instance").reset()
    $("#dxItem_LineStationTagBox").dxTagBox("instance").reset()
    $("#dxItem_LineStartDateDateBox").dxDateBox("instance").option("value", new Date(now.getFullYear(), now.getMonth(), 1));
    $("#dxItem_LineEndDateDateBox").dxDateBox("instance").option("value", new Date(now.getFullYear(), now.getMonth() + 1, 1));
}
async function GetItemInformation() {
    await dxLoadPanel.show()
    let _item_LineDTO = await GetDepartureLogItem_LineDTO();
    await BuildFilterAppliedToItem_LineDataGrid();
    let _ds = await GetItem_LineInformation(_item_LineDTO);
    $("#dxItemLineGrid").dxDataGrid("instance").option("dataSource", _ds);
    await dxLoadPanel.hide();
}

async function BuildFilterAppliedToItem_LineDataGrid() {
    let _filterSection = document.getElementById('FiltersItem_LineApply');
    _filterSection.innerHTML = "";

    if ($("#dxItem_LineStartDateDateBox").dxDateBox('instance').option("value") != null && $("#dxItem_LineEndDateDateBox").dxDateBox('instance').option("value")) {
        _filterSection.innerHTML +=
            '<div class=\"ms-2 bg-filter text-white rounded-5 p-1 px-3\">' +
            '<i class=\"fas fa-calendar-days me-2 text-white\"></i>Introduction Date from ' +
            $("#dxItem_LineStartDateDateBox").dxDateBox("instance").option("value").toLocaleDateString("en-US") +
            ' to ' +
            $("#dxItem_LineEndDateDateBox").dxDateBox("instance").option("value").toLocaleDateString("en-US") +
            '</div>';
    }
    if ($("#dxItem_LineOwnerTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _ownerApply = "";
        let _ownerSelected = $("#dxItem_LineOwnerTagBox").dxTagBox("instance").option("selectedItems");
        _ownerSelected.forEach(function (item, index, arr) {
            if (index > 0) {
                _ownerApply += "," + item.Name;
            } else {
                _ownerApply += item.Name;
            }
        });
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 p-1 px-3\"> <i class=\"fas fa-user me-2 text-white\"></i>Owner as " + _ownerApply + "</div>";
    }
    if ($("#dxItem_LineSupplyTypeTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _supplyTypeApply = "";
        let _supplyTypeSelected = $("#dxItem_LineSupplyTypeTagBox").dxTagBox("instance").option("selectedItems");
        _supplyTypeSelected.forEach(function (item, index, arr) {
            if (index > 0) {
                _supplyTypeApply += "," + item.Name;
            } else {
                _supplyTypeApply += item.Name;
            }
        });
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 p-1 px-3\"> <i class=\"fas fa-filter me-2 text-white\"></i>Supply Type as " + _supplyTypeApply + "</div>";
    }
    if ($("#dxItem_LineStationTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _stationApply = "";
        let _stationSelected = $("#dxItem_LineStationTagBox").dxTagBox("instance").option("selectedItems");
        _stationSelected.forEach(function (item, index, arr) {
            if (index > 0) {
                _stationApply += "," + item.Name;
            } else {
                _stationApply += item.Name;
            }
        });
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 p-1 px-3\"> <i class=\"fas fa-filter me-2 text-white\"></i>Station as " + _stationApply + "</div>";
    }
}
//#endregion