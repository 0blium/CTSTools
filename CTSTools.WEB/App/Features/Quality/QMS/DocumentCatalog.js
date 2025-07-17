import {
    GetDXDocumentDataSource,
    CreateDocument,
    UpdateDocument,
    DeleteDocument
} from './Document/Document_Service.js'
import {
    HostResponse,
    ClearErrorFeedback
} from '../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { GetDXDepartmentDataSource } from '../../Advancedsettings/Locationmanagement/Department/Department_Service.js'
import { GetDXDocumentTypeDataSource } from './Documenttype/Documenttype_Service.js'
import { GetDXCustomerDataSource } from '../../Advancedsettings/Customer/Customer_Service.js'
import { GetDXProductDataSource } from '../../Advancedsettings/Product/Product_Service.js'
import { GetDXUserDataSource } from '../../Advancedsettings/Usermanagement/User/User_Service.js'
import { GetDXStatus_StatusTypeDataSource } from '../../Advancedsettings/Statusmanagement/Status_StatusType/Status_StatusType_Service.js'
import { StatusType_Enum } from '../../Advancedsettings/Statusmanagement/StatusType/StatusType_Enum.js'
import { Status_Enum } from '../../Advancedsettings/Statusmanagement/Status/Status_Enum.js'
import { Facility_Enum } from '../../Advancedsettings/Locationmanagement/Facility/Facility_Enum.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeDocumentCatalogControls();
});

async function InitializeDocumentCatalogControls() {
    const now = new Date();

    $("#dxDocumentGrid").dxDataGrid({
        dataSource: await GetDXDocumentDataSource({
            GetTypeDTO: true,
            GetCustomerDTO: true,
            GetProductDTO: true,
            GetFacilityDTO: true,
            GetDepartmentDTO: true,
            GetFileDTO: true
        }),
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
            fileName: "DocumentCatalog",
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
                                    { text: "Log", icon: "fa regular fa-code-branch text-primary", value: 1 },
                                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 3 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 4 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                let _data = options.data;
                                switch (e.itemData.value) {
                                    case 1:
                                        location.href = "/App/Features/Quality/QMS/DocumentRevisionCatalog.aspx?DocumentID=" + _data.ID;
                                        break;
                                    case 3:
                                        DocumentActionButtons("Update");
                                        PopulateDocumentFields(_data);
                                        $('#SaveDocumentRecordModal').modal('show');
                                        break;
                                    case 4:
                                        document.getElementById('hiddenDocumentID').value = options.data.ID;
                                        ShowDeleteQuestion();
                                        break;
                                    default:
                                        break;
                                }
                            },
                        });
                    }
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false,
                    width: "auto"
                },
                {
                    caption: "Number",
                    dataField: "Number"
                },
                {
                    caption: "Revision",
                    dataField: "LastRevision",
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        const _link = `data:${options.data.FileDTO.MIMEType};base64,${options.data.FileDTO.Data}`
                        if (options.data.StatusID == Status_Enum.Released) {
                            $('<a style="text-decoration:none;">' + options.data.LastRevision + '</a>')
                                .attr('href', _link)
                                .attr('download', options.data.FileDTO.FileName)
                                .appendTo(container);  
                        }
                                           

                    }
                },
                {
                    caption: "Name",
                    dataField: "Name"
                },
                {
                    caption: "Status",
                    dataField: "StatusName"
                },
                { caption: "Description", dataField: "Description" },
                { caption: "Owner", dataField: "OwnerName" },
                { caption: "Department", dataField: "DepartmentName" },
                { caption: "Type", dataField: "TypeName" },
                { caption: "Customer", dataField: "CustomerName" },
                { caption: "Product", dataField: "ProductName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
            ],
    });
    $("#dxAddedStartDateDateBox").dxDateBox({
        type: "date",
        format: "MM/dd/yyyy",
        value: new Date(now.getFullYear(), now.getMonth(), 1),
        onValueChanged: function (e) {
            $("#dxAddedEndDateDateBox").dxDateBox("instance").option("min", e.value);
        }
    });

    $("#dxDocumentNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxDocumentDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxDocumentNumberTextBox").dxTextBox({
        placeholder: 'Type number...'
    });

    $("#dxDocumentOwnerSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentTypeSelectBox").dxSelectBox({
        dataSource: await GetDXTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentCustomerSelectBox").dxSelectBox({
        dataSource: await GetDXCustomerDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentProductSelectBox").dxSelectBox({
        dataSource: await GetDXProductDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxDocumentDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });

    $("#dxDocumentDepartmentSelectBox").dxSelectBox({
        dataSource: await GetDXDepartmentDataSource({ FacilityID: Facility_Enum.Texas }),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Department...",
    });
    document.getElementById("btnCloseDocumentModal").addEventListener("click", ClearDocumentFields);
    DocumentActionButtons("Save");
}

//#endregion

async function PopulateDocumentFields(data) {
    $("#hiddenDocumentID").val(data.ID);
    $("#dxDocumentNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxDocumentDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxDocumentNumberTextBox").dxTextBox("instance").option("value", data.Number);
    $("#dxDocumentTypeSelectBox").dxSelectBox("instance").option("value", data.TypeID);
    $("#dxDocumentCustomerSelectBox").dxSelectBox("instance").option("value", data.CustomerID);
    $("#dxDocumentProductSelectBox").dxSelectBox("instance").option("value", data.ProductID);
    $("#dxDocumentOwnerSelectBox").dxSelectBox("instance").option("value", data.OwnerID);
    await $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("value", data.DepartmentID);
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the Document, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDocument_Global();
    } else {
        ClearDocumentFields();
    }
}

function DocumentActionButtons(Action) {
    $("#DocumentActionButtons").empty();
    document.getElementById('DocumentModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewDocumentBtn").addEventListener("click", ClearDocumentFields);
        document.getElementById('DocumentModalTitle').innerText = 'Add Document'
        document.getElementById("DocumentActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateDocumentButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateDocumentButton").addEventListener("click", CreateDocument_Global);
    }
    else {
        // Update
        document.getElementById('DocumentModalTitle').innerText = 'Update Document'
        document.getElementById("DocumentActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateDocumentButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateDocumentButton").addEventListener("click", UpdateDocument_Global);
    }
}
function ClearDocumentFields() {
    $('#SaveDocumentRecordModal').modal('hide');
    DocumentActionButtons("Save");
    $("#hiddenDocumentID").val("");
    $("#dxDocumentNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxDocumentDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxDocumentNumberTextBox").dxTextBox("instance").option("value", "");
    $("#dxDocumentTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentCustomerSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentProductSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxDocumentGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDocumentGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}

function GetDocumentDTO() {
    let _documentDTO = {
        ID: $("#hiddenDocumentID").val(),
        Name: $("#dxDocumentNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDocumentDescriptionTextArea").dxTextArea("instance").option("value"),
        Number: $("#dxDocumentNumberTextBox").dxTextBox("instance").option("value"),
        DepartmentID: $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("value"),
        TypeID: $("#dxDocumentTypeSelectBox").dxSelectBox("instance").option("value"),
        CustomerID: $("#dxDocumentCustomerSelectBox").dxSelectBox("instance").option("value"),
        ProductID: $("#dxDocumentProductSelectBox").dxSelectBox("instance").option("value"),
        OwnerID: $("#dxDocumentOwnerSelectBox").dxSelectBox("instance").option("value"),
    }
    return _documentDTO;
}
//#endregion

//#region Document CRUD Functions
async function CreateDocument_Global() {
    await dxLoadPanel.show();
    const _documentDTO = GetDocumentDTO();
    const _validation_ResultDTO = await CreateDocument(_documentDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentGrid").dxDataGrid("instance").refresh();
        ClearDocumentFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDocument_Global() {
    await dxLoadPanel.show();
    const _documentDTO = GetDocumentDTO();
    const _validation_ResultDTO = await UpdateDocument(_documentDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentGrid").dxDataGrid("instance").refresh();
        ClearDocumentFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDocument_Global() {
    await dxLoadPanel.show();
    const _documentDTO = GetDocumentDTO();
    const _validation_ResultDTO = await DeleteDocument(_documentDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentGrid").dxDataGrid("instance").refresh();
        ClearDocumentFields();
        ReloadLocationsSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    ClearDocumentFields();
    dxLoadPanel.hide();
}

const GetDXTypeDataSource_Global = () => {
    let _typeDTO = { IsActive: true }
    return GetDXDocumentTypeDataSource(_typeDTO)
}
const GetDXCustomerDataSource_Global = () => {
    let _customerDTO = { IsActive: true }
    return GetDXCustomerDataSource(_customerDTO)
}
const GetDXProductDataSource_Global = () => {
    let _productDTO = { IsActive: true }
    return GetDXProductDataSource(_productDTO)
}

//#endregion

//Reload select box for related fields
async function ReloadLocationsSelectBox() {
    $("#dxDocumentDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource());
}