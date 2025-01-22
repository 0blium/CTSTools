import { GetDXDocumentRevisionDataSource, CreateDocumentRevision, UpdateDocumentRevision, DeleteDocumentRevision } from './DocumentRevision/DocumentRevision_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../common/utils/response.js'
import { dxLoadPanel } from '../../../common/components/dxloadpanel.js'
import { GetURLParameter } from '../../../Common/Utils/GetURLParameter.js'
import { GetDXDocumentDataSource, GetDocumentInformation } from './document/document_service.js'
import { GetDXStatus_StatusTypeDataSource } from '../../advancedsettings/statusmanagement/Status_StatusType/Status_StatusType_Service.js'
import { StatusType_Enum } from '../../advancedsettings/statusmanagement/StatusType/StatusType_Enum.js'


document.addEventListener("DOMContentLoaded", async function () {
    await dxLoadPanel.show();
    await GetDocumentIDByURL();
    await InitializeDocumentRevisionCatalogControls();
    await dxLoadPanel.hide();
});

async function GetDocumentIDByURL() {
    let _documentID = GetURLParameter("DocumentID");
    let _documentDTO = await GetDocumentInformation({ ID: _documentID })
    if (_documentID != null && _documentID != undefined && _documentID != 0 && !Number.isNaN(_documentID)) {
        document.getElementById('hiddenDocumentID').value = _documentID;
        document.getElementById('DocumentRevisionTitle').textContent = _documentDTO[0].Number + " - " + _documentDTO[0].Name;
    } else {
        toastr["error"]("Please, select a document to get the information", "Document Not selected");
    }
}

async function InitializeDocumentRevisionCatalogControls() {

    $("#dxDocumentRevisionGrid").dxDataGrid({
        dataSource: await GetDXDocumentRevisionDataSource({ GetDocumentDTO: true, GetStatusDTO: true, DocumentID: document.getElementById('hiddenDocumentID').value }),
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
            fileName: "DocumentRevisionCatalog",
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
            let _documentRevisionData = data.selectedRowsData[0];
            if (_documentRevisionData != null) {
                DocumentRevisionActionButtons("Update");
                PopulateDocumentRevisionFields(_documentRevisionData);
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
                                    $('#SaveDocumentRevisionRecordModal').modal('show');
                                    DocumentRevisionActionButtons("Update");
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenDocumentRevisionID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Revision", dataField: "Revision" },
                { caption: "Status", dataField: "StatusName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
            ],
    });

    $("#dxDocumentRevisionRevisionTextBox").dxTextBox({
        placeholder: 'Type revision...'
    });
    $("#dxDocumentRevisionStatusSelectBox").dxSelectBox({
        dataSource: await GetDXStatus_StatusTypeDataSource({ StatusTypeID: StatusType_Enum.QMS_Documents }),
        displayExpr: "StatusName",
        valueExpr: "StatusID",
        searchEnable: true,
    });
    
    $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").hide();
    
    document.getElementById("btnCloseDocumentRevisionModal").addEventListener("click", ClearDocumentRevisionFields);
    DocumentRevisionActionButtons("Save");
}

async function PopulateDocumentRevisionFields(data) {
    $("#hiddenDocumentRevisionID").val(data.ID);
    $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance").option("value", data.Revision);
    $("#dxDocumentRevisionStatusSelectBox").dxSelectBox("instance").option("value", data.StatusID);
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the Document Revision, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDocumentRevision_Global();
    } else {
        ClearDocumentRevisionFields();
    }
}

function DocumentRevisionActionButtons(Action) {
    $("#DocumentRevisionActionButtons").empty();
    document.getElementById('DocumentRevisionModalTitle').innerText = '';

    const revisionField = $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance");
    if (Action == "Save") {
        document.getElementById('DocumentRevisionModalTitle').innerText = 'New Revision Form';
        $("#DocumentRevisionActionButtons").html(
            `<div class="col-md-12">
                <button class="btn btn-success float-end" id="CreateDocumentRevisionButton" type="button">Save</button>
                <button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDocumentRevisionButton" type="button">Cancel</button>
            </div>`
        );
        revisionField.option("disabled", true); 
        $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").hide();

        document.getElementById("ClearDocumentRevisionButton").addEventListener("click", ClearDocumentRevisionFields);
        document.getElementById("CreateDocumentRevisionButton").addEventListener("click", CreateDocumentRevision_Global);
    }
    else {
        // Update
        document.getElementById('DocumentRevisionModalTitle').innerText = 'Update Revision Form';
        $("#DocumentRevisionActionButtons").html(
            `<div class="col-md-12">
                <button class="btn btn-success float-end" id="UpdateDocumentRevisionButton" type="button">Update</button>
                <button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDocumentRevisionButton" type="button">Cancel</button>
            </div>`
        );
        revisionField.option("disabled", true); 
        $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").show(); 

        document.getElementById("ClearDocumentRevisionButton").addEventListener("click", ClearDocumentRevisionFields);
        document.getElementById("UpdateDocumentRevisionButton").addEventListener("click", UpdateDocumentRevision_Global);
    }
}

function ClearDocumentRevisionFields() {
    $('#SaveDocumentRevisionRecordModal').modal('hide');
    DocumentRevisionActionButtons("Save");
    $("#hiddenDocumentRevisionID").val("");
    $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance").option("value", "");
    $("#dxDocumentRevisionStatusSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").hide(); 
    let keys = $("#dxDocumentRevisionGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDocumentRevisionGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}

function GetDocumentRevisionDTO() {
    let _documentRevisionDTO = {
        ID: $("#hiddenDocumentRevisionID").val(),
        Revision: $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance").option("value"),
        DocumentID: $("#hiddenDocumentID").val(),
        StatusID: $("#dxDocumentRevisionStatusSelectBox").dxSelectBox("instance").option("value"),
    }
    return _documentRevisionDTO;
}
//#endregion

//#region DocumentRevision CRUD Functions
async function CreateDocumentRevision_Global() {
    await dxLoadPanel.show();
    const _documentRevisionDTO = GetDocumentRevisionDTO();
    const _validation_ResultDTO = await CreateDocumentRevision(_documentRevisionDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentRevisionGrid").dxDataGrid("instance").refresh();
        ClearDocumentRevisionFields();
        
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateDocumentRevision_Global() {
    await dxLoadPanel.show();
    const _documentRevisionDTO = GetDocumentRevisionDTO();
    const _validation_ResultDTO = await UpdateDocumentRevision(_documentRevisionDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentRevisionGrid").dxDataGrid("instance").refresh();
        ClearDocumentRevisionFields();
        
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteDocumentRevision_Global() {
    await dxLoadPanel.show();
    const _documentRevisionDTO = GetDocumentRevisionDTO();
    const _validation_ResultDTO = await DeleteDocumentRevision(_documentRevisionDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentRevisionGrid").dxDataGrid("instance").refresh();
        ClearDocumentRevisionFields();
        
    }
    HostResponse(_validation_ResultDTO);
    ClearDocumentRevisionFields();
    dxLoadPanel.hide();
}

const GetDXStatusDataSource_Global = () => {
    let _statusDTO = { IsActive: true }
    return GetDXStatusDataSource(_statusDTO)
}

//#endregion