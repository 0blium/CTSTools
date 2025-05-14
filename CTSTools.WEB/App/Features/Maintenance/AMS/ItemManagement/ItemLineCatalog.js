import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXItem_LineDataSource, CreateItem_Line, UpdateItem_Line, DeleteItem_Line, GetItem_LineMasterDetailInformation, ReassignSupportGroup } from './Item_Line/Item_Line_Service.js'
import { GetDXItem_HeaderDataSource, CreateItem_Header, UpdateItem_Header, DeleteItem_Header, GetItem_HeaderFilesInformation, DeleteItem_HeaderFile } from './Item_Header/Item_Header_Service.js'
import { GetItem_SupportGroupInformation } from './Item_SupportGroup/Item_SupportGroup_Service.js'
import { GetDXUserDataSource } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetDXSupportGroupDataSource } from '../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetDXStationDataSource } from '../StationManagement/Station/Station_Service.js'
import { GetDXSupplyTypeDataSource } from '../../../AdvancedSettings/SupplyType/SupplyType_Service.js'
import { GetDXStatus_StatusTypeDataSource } from '../../../AdvancedSettings/StatusManagement/Status_StatusType/Status_StatusType_Service.js'
import { UpdateUserDefinedTemplate, GetUserDefinedTemplateInformation } from './UserDefinedTemplate/UserDefinedTemplate_Service.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'

import { StatusType_Enum } from '../../../AdvancedSettings/StatusManagement/StatusType/StatusType_Enum.js'
import { SupplyType_Enum } from '../../../AdvancedSettings/SupplyType/SupplyType_Enum.js'
import { DataType_Enum } from '../../../AdvancedSettings/DataType/DataType_Enum.js'

document.addEventListener("DOMContentLoaded", async () => {
    await dxLoadPanel.show();
    await GetItem_SupportGroupIDByURL();
    await dxLoadPanel.hide();
});

let ItemLineList = [];
let UserDefinedTemplateList = [];
async function InitializeItemLineCatalogControls() {
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox({
        dataSource: await GetDXItem_HeaderDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        readOnly: true,
        deferRendering: false,
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
    $("#dxItem_LineDeliveredToSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["Name"],
        searchMode: 'contains'
    })
    $("#dxItem_LineSupportGroupSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        readOnly: true,
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxItem_LineStationSelectBox").dxSelectBox({
        dataSource: await GetDXStationDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
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
                    case SupplyType_Enum.Local:
                        _importFields.classList.add('d-none');
                        _localFields.classList.remove('d-none');
                        break;
                    case SupplyType_Enum.Temporary_Import:
                    case SupplyType_Enum.Definitive_Import:
                        _localFields.classList.add('d-none');
                        _importFields.classList.remove('d-none');
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
    })
    $("#dxItem_LinePONumberTextBox").dxTextBox({
        placeholder: "Type PO Number.."
    });
    $("#dxItem_LinePOLineTextBox").dxTextBox({
        placeholder: "Type PO Line.."
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
    $("#dxItem_LineGenerateSerialCheckBox").dxCheckBox({
        value: false,
        visible: false,
        text: "Generate Serial?",
    });
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox({
        placeholder: "Type import invoice .."
    })
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox({
        placeholder: "Type Price ..",
        onValueChanged: function (e) {
            if (e.event != undefined) {
                let _basePrice = {
                    Price: e.value,
                }
            }
        }
    });
    $("#dxItem_LineCommentsTextArea").dxTextArea({
        placeholder: "Type comments..."
    })


    $("#dxItemLineGrid").dxDataGrid({
        dataSource: ItemLineList,
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
            fileName: "ItemLineCatalog",
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
        onSelectionChanged: async function (data) {
            let _ItemLineData = data.selectedRowsData[0];
            if (_ItemLineData != null) {
                ItemLineActionButtons("Update");
                await BuildUserDefinedOnItem_LineModal(UserDefinedTemplateList);
                PopulateItemLineFields(_ItemLineData, UserDefinedTemplateList);
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
                                    {
                                        text: "Print",
                                        icon: "fa fa-print text-secondary",
                                        items: [  // Aquí añades el submenú de opciones de impresión
                                            { text: 'S', LabelFile: 'LF-0001-15-A' },
                                            { text: 'M', LabelFile: 'LF-2173-03-A' },
                                            { text: 'L', LabelFile: 'LF-2173-02-A' }
                                        ]
                                    },
                                    { text: "Reassign Support Group", icon: "fa fa-right-left text-info", value: 2 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 3 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                let _data = options.data;  // Datos de la fila seleccionada
                                if (e.itemData.value == 1) {
                                    $('#SaveItemLineRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    $('#ReassingSupportGroupModal').modal('show');
                                    PopulateReassignSupportGroupModalFields(options.data);
                                }
                                else if (e.itemData.value == 3) {
                                    $("#hiddenItemLineID").val(options.data.ID);
                                    ShowItem_LineDeleteQuestion();
                                }
                                // Si se hace clic en una de las opciones del submenú de impresión
                                if (e.itemData.LabelFile != null) {
                                    // Aquí se ejecuta la impresión, usando la URL
                                    window.open('http://avmx-s05:8044//LabelPrint.aspx?LabelFile=' + e.itemData.LabelFile + "&Dummy=False&WO=" + _data.Serial + "&Qty=1&From=&To=&SkipEvery=&SerialLength=&ESD=True&SAS=False&FullWorkOrder=" + _data.Serial);
                                }
                            },
                        });
                    }
                }
            ],
    });
    RefreshGrid();
    document.getElementById("btnCloseItemLineModal").addEventListener("click", ClearItemLineFields);
    ItemLineActionButtons("Save");
    ReassignSupportGroupModalActionButtons("Update");
}
async function GetItem_SupportGroupIDByURL() {
    let _item_SupportGroupID = GetURLParameter("Item_SupportGroupID");
    let _item_SupportGroupDTO = { ID: _item_SupportGroupID };
    const _item_LineDTO = { Item_SupportGroupDTO: { ID: _item_SupportGroupID }, IsActive: true };
    ItemLineList = await GetItem_LineMasterDetailInformation(_item_LineDTO);
    let _item_SupportGroupList = await GetItem_SupportGroupInformation(_item_SupportGroupDTO);
    if (_item_SupportGroupID != null && _item_SupportGroupID != undefined && _item_SupportGroupID != 0 && !Number.isNaN(_item_SupportGroupID) && (ItemLineList.length != 0 || _item_SupportGroupList.length != 0)) {
        document.getElementById('hiddenItemLineSupportGroupID').value = _item_SupportGroupID;
        document.getElementById('hiddenItemHeaderID').value = _item_SupportGroupList[0].Item_HeaderDTO.ID;
        document.getElementById('hiddenItemSupportGroupID').value = _item_SupportGroupList[0].SupportGroupDTO.ID;
        document.getElementById('NewItemLineBtn').removeAttribute('hidden');
        InitializeItemLineCatalogControls();
        InitializeReassignSupportGroupModalControls();
        const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItemLineSupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
        UserDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
        await BuildUserDefinedOnItem_LineModal(UserDefinedTemplateList);
        $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", Number($("#hiddenItemHeaderID").val()));
        $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", Number($("#hiddenItemSupportGroupID").val()));
    } else {
        toastr["error"]("Please, select a Line to get the information", "Line Not selected");
    }
}
function RefreshGrid()
{
    let _itemLineMasterDetailsGrid = $(`#dxItemLineGrid`).dxDataGrid("instance");
    if (_itemLineMasterDetailsGrid != null) {
        var _state = _itemLineMasterDetailsGrid.state();
        var _columns = _itemLineMasterDetailsGrid.option("columns");
        if (ItemLineList.length > 0) {
            Object.keys(ItemLineList[0]).forEach(key => {
                let column = { dataField: key }
                if (key.includes("Date")) {
                    column.dataType = 'date';
                }
                if (key.includes("DTO")) {
                    column.visible = false;
                }
                _columns.push(column);

            });
        }
        _itemLineMasterDetailsGrid.option("columns", _columns);
        _itemLineMasterDetailsGrid.state(_state)
    }
}
function ItemLineActionButtons(Action) {
    $("#ItemLineActionButtons").empty();
    document.getElementById('ItemLineModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewItemLineBtn").addEventListener("click", ClearItemLineFields);
        document.getElementById('ItemLineModalTitle').innerText = 'Add Asset';
        document.getElementById("ItemLineActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateItemLineButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearItemLineButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearItemLineButton").addEventListener("click", ClearItemLineFields);
        document.getElementById("CreateItemLineButton").addEventListener("click", CreateItem_Line_Global);
    }
    else {
        // Update
        document.getElementById('ItemLineModalTitle').innerText = 'Update Asset';
        document.getElementById("ItemLineActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateItemLineButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearItemLineButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearItemLineButton").addEventListener("click", ClearItemLineFields);
        document.getElementById("UpdateItemLineButton").addEventListener("click", UpdateItem_Line_Global);
    }
}
async function ClearItemLineFields() {
    const _item_LineDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItemLineSupportGroupID").val() }, IsActive: true };
    ItemLineList = await GetItem_LineMasterDetailInformation(_item_LineDTO);
    $('#SaveItemLineRecordModal').modal('hide');
    ItemLineActionButtons("Save");
    $("#hiddenItemLineID").val("");
    $("#hiddenStationID").val("");
    //$("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineDeliveredToSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value", "");
    $("#dxItem_LinePONumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LinePOLineTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_LineStationSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineGenerateSerialCheckBox").dxCheckBox("instance").option("value", false);
    for (var _userDefined in UserDefinedTemplateList) {
        if (_userDefined != null && _userDefined >= 0) {
            switch (UserDefinedTemplateList[_userDefined].UserDefinedDTO.DataTypeDTO.ID) {
                case DataType_Enum.Text:
                    $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", "");
                    break;
                case DataType_Enum.DateTime:
                    $("#dxUserDefined" + _userDefined).dxDateBox("instance").option("value");
                    break;
                case DataType_Enum.Check:
                    $("#dxUserDefined" + _userDefined).dxCheckBox("instance").option("value", true);
                    break;
                case DataType_Enum.Numeric:
                    $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", "");
                    break;
            }
        }
        else break; 
    }
    let keys = $("#dxItemLineGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxItemLineGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxItemLineGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxItemLineGrid").dxDataGrid("instance").option('dataSource', ItemLineList);
    $("#dxItemLineGrid").dxDataGrid("instance").refresh();
    //ClearErrorFeedback();
}
function PopulateItemLineFields(Data, UserDefinedList) {
    ItemLineActionButtons("Update");
    $("#hiddenItemLineID").val(Data.ID);
    $("#hiddenStationID").val(Data.StationDTOID);
    $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value", Data.StationDTOID)
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value", Data.OwnerDTOID);
    $("#dxItem_LineDeliveredToSelectBox").dxSelectBox("instance").option("value", Data.DeliveredToDTOID);
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value", Data.SupplyTypeDTOID);
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value", Data.ManufactureSerialID);
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value", Data.ShipmentReceiptNumber);
    $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value", Data.StatusDTOID);
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value", Data.ImportInvoice);
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", Data.BasePriceUSD);
    $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value", Data.Comments);
    $("#dxItem_LinePONumberTextBox").dxTextBox("instance").option("value", Data.PONumber);
    $("#dxItem_LinePOLineTextBox").dxTextBox("instance").option("value", Data.POLine);
    $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value", Data.LegacyID);
    $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value", Data.Serial);
    for (var _userDefined in UserDefinedList) {
        switch (UserDefinedList[_userDefined].UserDefinedDTO.DataTypeDTO.ID) {
            case DataType_Enum.Text:
                $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedDTO.Name]);
                break;
            case DataType_Enum.DateTime:
                $("#dxUserDefined" + _userDefined).dxDateBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedDTO.Name]);
                break;
            case DataType_Enum.Check:
                let _userDefinedValue = Data[UserDefinedList[_userDefined].UserDefinedDTO.Name] == 'true' ? true : false;
                $("#dxUserDefined" + _userDefined).dxCheckBox("instance").option("value", _userDefinedValue);
                break;
            case DataType_Enum.Numeric:
                $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedDTO.Name]);
                break;
        }
    }
}
function GetItem_LineDTO(UserDefinedTemplateList) {
    let _item_LineDTO = {
        ID: $("#hiddenItemLineID").val(),
        Item_HeaderDTO: {
            ID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value")
        },
        Item_SupportGroupDTO: {
            ID: $("#hiddenItemLineSupportGroupID").val(),
            SupportGroupDTO: {
                ID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value")
            }
        },
        OwnerDTO: {
            ID: $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value"),
        },
        SupplyTypeDTO: {
            ID: $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value"),
        },
        Serial: $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value"),
        ManufactureSerialID: $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value"),
        ShipmentReceiptNumber: $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value"),
        StatusDTO: {
            ID: $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value")
        },
        StationDTO: {
            ID: $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value")
        },
        DeliveredToID: $("#dxItem_LineDeliveredToSelectBox").dxSelectBox("instance").option("value"),
        ImportInvoice: $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value"),
        BasePriceUSD: $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value"),
        Comments: $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value"),
        PONumber: $("#dxItem_LinePONumberTextBox").dxTextBox("instance").option("value"),
        POLine: $("#dxItem_LinePOLineTextBox").dxTextBox("instance").option("value"),
        LegacyID: $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value"),
        IsActive: true,
        GenerateSerial: $("#dxItem_LineGenerateSerialCheckBox").dxCheckBox("instance").option("value"),
        ShipmentReceiptID: null,
        UserDefinedValueList: [],
    }
    for (var _userDefined in UserDefinedTemplateList) {
        let _userDefinedValueDTO = {
            UserDefinedDTO: {
                ID: UserDefinedTemplateList[_userDefined].UserDefinedDTO.ID
            },
            SupportGroupDTO: {
                ID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value")
            },
            IsActive: true,
            Value: ""
        }
        switch (UserDefinedTemplateList[_userDefined].UserDefinedDTO.DataTypeDTO.ID) {
            case DataType_Enum.Text:
                _userDefinedValueDTO.Value = $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value");
                break;
            case DataType_Enum.DateTime:
                _userDefinedValueDTO.Value = $("#dxUserDefined" + _userDefined).dxDateBox("instance").option("value");
                break;
            case DataType_Enum.Check:
                _userDefinedValueDTO.Value = $("#dxUserDefined" + _userDefined).dxCheckBox("instance").option("value");
                break;
            case DataType_Enum.Numeric:
                _userDefinedValueDTO.Value = $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value");
                break;
        }
        _item_LineDTO.UserDefinedValueList.push(_userDefinedValueDTO);
    }
    return _item_LineDTO;
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
        ClearItemLineFields();
    }
}
async function CreateItem_Line_Global() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItem_LineDTO(UserDefinedTemplateList);
    const _validation_ResultDTO = await CreateItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        await ClearItemLineFields();
        $("#AddNewItemLineModal").modal("hide");
        RefreshGrid();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateItem_Line_Global() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItem_LineDTO(UserDefinedTemplateList);
    const _validation_ResultDTO = await UpdateItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItemLineFields();
        $("#AddNewItemLineModal").modal("hide");
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteItem_Line_Global() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItem_LineDTO(UserDefinedTemplateList);
    const _validation_ResultDTO = await DeleteItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItemLineFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function BuildUserDefinedOnItem_LineModal(UserDefinedTemplateList) {
    let _userDefinedSection = document.getElementById("UserDefinedTemplateSection");
    _userDefinedSection.innerHTML = "";
    let rowContent = '<div class="row">';  // Abre el contenedor de la fila
    for (let i in UserDefinedTemplateList) {
        rowContent += // agregamos los UserDefinedTemplate
        `   <div class="col-md-4 mb-3">
                <label class="form-label col-form-label">${UserDefinedTemplateList[i].UserDefinedDTO.Name}</label>
                <div id="dxUserDefined${i}"></div>
            </div>`;
    }
    rowContent += '</div>';  // Cierra el contenedor de la fila
    _userDefinedSection.innerHTML = rowContent;  // Asigna el contenido HTML a la sección
    for (let i in UserDefinedTemplateList) {
        switch (UserDefinedTemplateList[i].UserDefinedDTO.DataTypeDTO.ID) {
            case DataType_Enum.Text:
                $("#dxUserDefined" + i).dxTextBox({ placeholder: "Type " + UserDefinedTemplateList[i].UserDefinedDTO.Name+" .." });
                break;
            case DataType_Enum.DateTime:
                let now = new Date();
                $("#dxUserDefined" + i).dxDateBox({
                    value: new Date(now.getFullYear(), now.getMonth(), 1),
                    type: "datetime",
                });
                break;
            case DataType_Enum.Check:
                $("#dxUserDefined" + i).dxCheckBox({
                    value: false
                });
                break;
            case DataType_Enum.Numeric:
                $("#dxUserDefined" + i).dxTextBox({});
                break;
        }
    }
}

//#region Item Support Group Region

async function InitializeReassignSupportGroupModalControls() {
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName"],
        searchMode: 'contains'
    });
}

function ReassignSupportGroupModalActionButtons(Action) {
    $("#ReassignSupportGroupModalActionButtons").empty();
    if (Action == "Update") {
        document.getElementById("ReassignSupportGroupModalActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearReassignSupportGroupModalButton" data-bs-dismiss="modal" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateReassignSupportGroupModalButton" data-bs-dismiss="modal" type="button">Assign</button>' +
            '</div>';
        document.getElementById("ClearReassignSupportGroupModalButton").addEventListener("click", ClearReassignSupportGroupModalFields);
        document.getElementById("UpdateReassignSupportGroupModalButton").addEventListener("click", ReassignSupportGroupGlobal);
    }
}
async function PopulateReassignSupportGroupModalFields(data) {
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTOID);
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value", data.SupportGroupDTOID);
}

function ClearReassignSupportGroupModalFields() {
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value", '');
}
function GetItemLine_SupportGroupModalDTO() {
    //This build a item line with Item_supportGroup Relation
    let _item_LineDTO = {
        ID: $("#hiddenItemLineID").val(),
        Item_HeaderDTO: {
            ID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value"),
        },
        Item_SupportGroupDTO: {
            ID: $("#hiddenItemLineSupportGroupID").val(),
            SupportGroupDTO: {
                ID: $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value"),
            },
        },
        IsActive: true
    }
    return _item_LineDTO;
}
async function ReassignSupportGroupGlobal() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItemLine_SupportGroupModalDTO();
    const _validation_ResultDTO = await ReassignSupportGroup(_item_LineDTO);
    ClearItemLineFields();
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion