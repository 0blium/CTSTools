import { GetDXDocumentRevisionDataSource, UpdateDocumentFile, CreateDocumentRevision, UpdateDocumentRevision, DeleteDocumentRevision } from './DocumentRevision/DocumentRevision_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../common/utils/response.js'
import { dxLoadPanel } from '../../../common/components/dxloadpanel.js'
import { GetFileDTO } from '../../../common/utils/GetFileDTO.js'

import { GetURLParameter } from '../../../Common/Utils/GetURLParameter.js'
import { GetDocumentInformation } from './document/document_service.js'
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

    $("#dxRevisionFileUploader").dxFileUploader({
        selectButtonText: "Select File",
        labelText: "or Drop here",
        uploadMode: "instantly",
        onValueChanged: function (e) {

        }
    });
    $("#dxUpdateRevisionFileUploader").dxFileUploader({
        selectButtonText: "Select File",
        labelText: "or Drop here",
        uploadMode: "instantly",
        onValueChanged: function (e) {

        }
    });
    $("#dxDocumentRevisionStatusSelectBox").dxSelectBox({
        dataSource: await GetDXStatus_StatusTypeDataSource({ StatusTypeID: StatusType_Enum.QMS_Documents }),
        displayExpr: "StatusName",
        valueExpr: "StatusID",
        searchEnable: true
    });
    $("#dxDocumentRevisionGrid").dxDataGrid({
        dataSource: await GetDXDocumentRevisionDataSource({
            GetDocumentDTO: true,
            GetStatusDTO: true,
            DocumentID: document.getElementById('hiddenDocumentID').value,
            GetFileDTO: true
        }),
        keyExpr: "ID",
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 50, 100],
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
        columns:
            [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        // Identify the most recent record
                        const recentID = $("#dxDocumentRevisionGrid")
                            .dxDataGrid("instance")
                            .getDataSource()
                            .items()
                            .reduce((prev, current) => {
                                // Compare based on the ID or any other logic
                                return current.ID > prev.ID ? current : prev;
                            }).ID;
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: options.data.ID === recentID ? [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Edit", icon: "fa fa-pen-to-square text-info", value: 1 },
                                    { text: "Upload", icon: "fa fa-cloud-arrow-up text-info", value: 2 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 3 }]
                            }] : [],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    document.getElementById('RevisionFileSection').hidden = true;
                                    document.getElementById('StatusSection').hidden = false;
                                    $('#SaveDocumentRevisionRecordModal').modal('show');
                                    PopulateDocumentRevisionFields(options.data);
                                    DocumentRevisionActionButtons("Update");
                                }
                                else if (e.itemData.value == 2) {
                                    $('#UpdateDocumentModal').modal('show');

                                }
                                else if (e.itemData.value == 3) {
                                    document.getElementById('hiddenDocumentRevisionID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                {
                    caption: "Revision",
                    dataField: "Revision",
                    alignment: 'center',
                    sortOrder: "desc",
                    cellTemplate: function (container, options) {
                        console.log(options.data)
                        const _link = `data:${options.data.FileDTO.MIMEType};base64,${options.data.FileDTO.Data}`
                        $('<a style="text-decoration:none;">' + options.data.Revision + '</a>')
                            .attr('href', _link)
                            .attr('download', options.data.FileDTO.FileName)
                            .appendTo(container);
                    }

                },
                { caption: "Change Reason", dataField: "ChangeReason" },
                { caption: "Status", dataField: "StatusName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },

            ],
    });

    $("#dxDocumentRevisionChangeReasonTextArea").dxTextArea({
        placeholder: 'Type change reason...'
    });
    $("#dxDocumentRevisionRevisionTextBox").dxTextBox({
        placeholder: 'Type revision...'
    });


    $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").hide();
    document.getElementById("UpdateDocumentButton").addEventListener("click", UpdateDocument_Global);

    document.getElementById("btnCloseDocumentRevisionModal").addEventListener("click", ClearDocumentRevisionFields);
    DocumentRevisionActionButtons("Save");
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
            </div>`
        );
        revisionField.option("disabled", true);
        $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").hide();

        document.getElementById("CreateDocumentRevisionButton").addEventListener("click", CreateDocumentRevision_Global);
    }
    else {
        // Update
        document.getElementById('DocumentRevisionModalTitle').innerText = 'Update Revision Form';
        $("#DocumentRevisionActionButtons").html(
            `<div class="col-md-12">
                <button class="btn btn-success float-end" id="UpdateDocumentRevisionButton" type="button">Update</button>
            </div>`
        );
        revisionField.option("disabled", true);
        $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").show();
        document.getElementById("UpdateDocumentRevisionButton").addEventListener("click", UpdateDocumentRevision_Global);
    }
}

function ClearDocumentRevisionFields() {
    document.getElementById('RevisionFileSection').hidden = false;
    document.getElementById('StatusSection').hidden = true;

    $('#SaveDocumentRevisionRecordModal').modal('hide');
    $("#dxRevisionFileUploader").dxFileUploader("instance").reset();
    DocumentRevisionActionButtons("Save");
    $("#hiddenDocumentRevisionID").val("");
    $("#dxDocumentRevisionChangeReasonTextArea").dxTextArea("instance").option("value", "");
    $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance").option("value", "");
    $("#dxDocumentRevisionStatusSelectBox").dxSelectBox("instance").reset();
    $("#dxDocumentRevisionRevisionTextBox").closest(".mb-3").hide();
    let keys = $("#dxDocumentRevisionGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDocumentRevisionGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}

async function GetDocumentRevisionDTO() {
    let file = $("#dxRevisionFileUploader").dxFileUploader("instance").option("value")[0];
    let _fileDTO = $("#dxRevisionFileUploader").dxFileUploader("instance").option("value").length == 0 ? null : await GetFileDTO(file)
    let _documentRevisionDTO = {
        ID: $("#hiddenDocumentRevisionID").val(),
        ChangeReason: $("#dxDocumentRevisionChangeReasonTextArea").dxTextArea("instance").option("value"),
        FileDTO: _fileDTO,
        Revision: $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance").option("value"),
        DocumentID: $("#hiddenDocumentID").val(),
        StatusID: $("#dxDocumentRevisionStatusSelectBox").dxSelectBox("instance").option("value"),

    }
    return _documentRevisionDTO;
}
function PopulateDocumentRevisionFields(data) {
    $("#hiddenDocumentRevisionID").val(data.ID);
    $("#dxDocumentRevisionChangeReasonTextArea").dxTextArea("instance").option("value", data.ChangeReason);
    $("#dxDocumentRevisionStatusSelectBox").dxSelectBox("instance").option("value", data.StatusID);
    $("#dxDocumentRevisionRevisionTextBox").dxTextBox("instance").option("value", data.Revision);
}
//#endregion

//#region DocumentRevision CRUD Functions
async function CreateDocumentRevision_Global() {
    await dxLoadPanel.show();
    let _file = $("#dxRevisionFileUploader").dxFileUploader("instance").option("value")[0];
    if (!DocumentFile_Validation(_file))
        return;
    const _documentRevisionDTO = await GetDocumentRevisionDTO();
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
    const _documentRevisionDTO = await GetDocumentRevisionDTO();
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
async function UpdateDocument_Global() {
    await dxLoadPanel.show();
    let _file = $("#dxUpdateRevisionFileUploader").dxFileUploader("instance").option("value")[0];
    if (!DocumentFile_Validation(_file))
        return;
    const _documentRevisionDTO = {
        FileDTO: await GetFileDTO(_file),
        ID: $("#hiddenDocumentRevisionID").val(),
        DocumentID: $("#hiddenDocumentID").val()
    }
    const _validation_ResultDTO = await UpdateDocumentFile(_documentRevisionDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDocumentRevisionGrid").dxDataGrid("instance").refresh();
        $("#dxUpdateRevisionFileUploader").dxFileUploader("instance").reset();
        $('#UpdateDocumentModal').modal('hide');

    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
function DocumentFile_Validation(file) {
    if (file == null) {
        Swal.fire("Error", "You must attach a file before to save a record", "error");
        dxLoadPanel.hide();
        return false;
    }
    return true;
}


//#endregion