import { GetDXDocumentTypeDataSource, CreateDocumentType, UpdateDocumentType, DeleteDocumentType } from './DocumentType/DocumentType_Service.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js';
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js';

//#region DocumentType Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeDocumentTypeCatalogControls();
});
async function InitializeDocumentTypeCatalogControls() {
    $("#dxDocumentTypeIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxDocumentTypeNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxDocumentTypeDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxDocumentTypeGrid").dxDataGrid({
        dataSource: await GetDXDocumentTypeDataSource_Global({ IsActive: true }),
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
            fileName: "DocumentTypeCatalog",
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
            let _documentTypeData = data.selectedRowsData[0];
            if (_documentTypeData != null) {
                DocumentTypeActionButtons("Update");
                PopulateDocumentTypeFields(_documentTypeData);
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
                                    { text: "Edit", icon: "fa fa-pen-to-square text-info", value: 1 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveDocumentTypeRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenDocumentTypeID').value = options.data.ID;
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
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseDocumentTypeModal").addEventListener("click", ClearDocumentTypeFields);
    DocumentTypeActionButtons("Save");
}
async function PopulateDocumentTypeFields(data) {
    $("#hiddenDocumentTypeID").val(data.ID);
    $("#dxDocumentTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxDocumentTypeDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxDocumentTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the DocumentType, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDocumentType_Global();
    } else {
        ClearDocumentTypeFields();
    }
}
function DocumentTypeActionButtons(Action) {
    $("#DocumentTypeActionButtons").empty();
    document.getElementById('DocumentTypeModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewDocumentTypeBtn").addEventListener("click", ClearDocumentTypeFields);
        document.getElementById('DocumentTypeModalTitle').innerText = 'Add DocumentType'
        document.getElementById("DocumentTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateDocumentTypeButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDocumentTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDocumentTypeButton").addEventListener("click", ClearDocumentTypeFields);
        document.getElementById("CreateDocumentTypeButton").addEventListener("click", CreateDocumentType_Global);
    }
    else {
        // Update
        document.getElementById('DocumentTypeModalTitle').innerText = 'Update DocumentType'
        document.getElementById("DocumentTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateDocumentTypeButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDocumentTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDocumentTypeButton").addEventListener("click", ClearDocumentTypeFields);
        document.getElementById("UpdateDocumentTypeButton").addEventListener("click", UpdateDocumentType_Global);
    }
}
function ClearDocumentTypeFields() {
    $('#SaveDocumentTypeRecordModal').modal('hide');
    DocumentTypeActionButtons("Save");
    $("#hiddenDocumentTypeID").val("");
    $("#dxDocumentTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxDocumentTypeDescription").dxTextArea("instance").option("value", '');
    $("#dxDocumentTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxDocumentTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDocumentTypeGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetDocumentTypeDTO() {
    let _documentTypeDTO = {
        ID: $("#hiddenDocumentTypeID").val(),
        Name: $("#dxDocumentTypeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDocumentTypeDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxDocumentTypeIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _documentTypeDTO;
}
//#endregion

//#region DocumentType CRUD Functions
async function CreateDocumentType_Global() {
    await dxLoadPanel.show();
    const _documentTypeDTO = GetDocumentTypeDTO();
    const _validation_ResultDTO = await CreateDocumentType(_documentTypeDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentTypeGrid").dxDataGrid("instance").refresh();
        ClearDocumentTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDocumentType_Global() {
    await dxLoadPanel.show();
    const _documentTypeDTO = GetDocumentTypeDTO();
    const _validation_ResultDTO = await UpdateDocumentType(_documentTypeDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentTypeGrid").dxDataGrid("instance").refresh();
        ClearDocumentTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDocumentType_Global() {
    await dxLoadPanel.show();
    const _documentTypeDTO = GetDocumentTypeDTO();
    const _validation_ResultDTO = await DeleteDocumentType(_documentTypeDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentTypeGrid").dxDataGrid("instance").refresh();
        ClearDocumentTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearDocumentTypeFields();
    dxLoadPanel.hide();
}
const GetDXDocumentTypeDataSource_Global = () => {
    return GetDXDocumentTypeDataSource()
}
//#endregion