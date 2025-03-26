import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXSparePartDataSource, CreateSparePart, UpdateSparePart, DeleteSparePart } from './SparePart/SparePart_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeSparePartCatalogControls();
});
let _fileDTOList = [];
let _fileDTO = {};

async function InitializeSparePartCatalogControls() {
    $("#dxSparePartNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxSparePartDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxSparePartManufactureIDTextBox").dxTextBox({
        placeholder: 'Type description..'
    });
    $("#dxSparePartIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxSparePartThumbnailFileUploader").dxFileUploader({
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
                    $("#SparePartThumbnail").attr('src', e.target.result);
                    _fileDTO = {
                        Name: $("#dxSparePartThumbnailFileUploader").dxFileUploader("instance").option("value")[0].name,
                        Size: $("#dxSparePartThumbnailFileUploader").dxFileUploader("instance").option("value")[0].size,
                        Data: e.target.result,
                    };
                }
            }

        },
    });
    $("#dxSparePartGrid").dxDataGrid({
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
                SparePartActionButtons("Update");
                PopulateSparePartFields(_SparePartData);
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
                                    { text: "Edit item", icon: "fa fa-pen-to-square text-info", value: 1 },
                                    { text: "Delete item", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveSparePartRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenSparePartID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: 'Spare Part image',
                    width: 200,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate(container, options) {
                        if (options.data.SparePartImage != null) {
                            $('<div>')
                                .append($('<img>', { src: options.data.SparePartImage, height: 120, width: 120 }))
                                .appendTo(container);
                        } else {
                            $('<div>')
                                .append($('<img>', { src: '/App/Common/Assets/img/no-product-image.png', height: 120 }))
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
    document.getElementById("btnCloseSparePartModal").addEventListener("click", ClearSparePartFields);
    SparePartActionButtons("Save");
}
function SparePartActionButtons(Action) {
    $("#SparePartActionButtons").empty();
    document.getElementById('SparePartModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewSparePartBtn").addEventListener("click", ClearSparePartFields);
        document.getElementById('SparePartModalTitle').innerText = 'Add Spare Part';
        document.getElementById("SparePartActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateSparePartButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSparePartButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSparePartButton").addEventListener("click", ClearSparePartFields);
        document.getElementById("CreateSparePartButton").addEventListener("click", CreateSparePart_Global);
    }
    else {
        // Update
        document.getElementById('SparePartModalTitle').innerText = 'Update Spare Part';
        document.getElementById("SparePartActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateSparePartButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSparePartButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSparePartButton").addEventListener("click", ClearSparePartFields);
        document.getElementById("UpdateSparePartButton").addEventListener("click", UpdateSparePart_Global);
    }
}
function ClearSparePartFields() {
    $('#SaveSparePartRecordModal').modal('hide');
    SparePartActionButtons("Save");
    $('#hiddenSparePartID').val("");
    $("#dxSparePartIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSparePartNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxSparePartDescriptionTextArea").dxTextArea("instance").option("value", '');
    $("#dxSparePartManufactureIDTextBox").dxTextBox("instance").option("value", '');
    $("#dxSparePartThumbnailFileUploader").dxFileUploader("instance").reset();

    _fileDTOList = [];
    _fileDTO = {};
    $("#SparePartThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    let keys = $("#dxSparePartGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSparePartGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSparePartGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSparePartGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateSparePartFields(data) {
    $('#hiddenSparePartID').val(data.ID);
    $("#dxSparePartIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxSparePartNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxSparePartDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxSparePartManufactureIDTextBox").dxTextBox("instance").option("value", data.ManufactureID);
    if (data.SparePartImage != null) {
        $("#SparePartThumbnail").attr('src', data.SparePartImage);
    } else {
        $("#SparePartThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    }
}
function GetSparePartDTO() {
    let _SparePartDTO = {
        ID: $('#hiddenSparePartID').val(),
        Name: $("#dxSparePartNameTextBox").dxTextBox("instance").option("value"),
        ManufactureID: $("#dxSparePartManufactureIDTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxSparePartDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxSparePartIsActiveCheckBox").dxCheckBox("instance").option("value"),
        FileDTO: GetFileDTO()
    }
    return _SparePartDTO;
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this SparePart',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSparePart_Global();
    } else {
        ClearSparePartFields();
    }
}
async function CreateSparePart_Global() {
    await dxLoadPanel.show();
    const _SparePartDTO = GetSparePartDTO();
    const _validation_ResultDTO = await CreateSparePart(_SparePartDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSparePart_Global() {
    await dxLoadPanel.show();
    const _SparePartDTO = GetSparePartDTO();
    const _validation_ResultDTO = await UpdateSparePart(_SparePartDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteSparePart_Global() {
    await dxLoadPanel.show();
    const _SparePartDTO = GetSparePartDTO();
    const _validation_ResultDTO = await DeleteSparePart(_SparePartDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePartFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#region Files
function GetFileDTO() {
    if ((_fileDTO != null || _fileDTO != undefined) && (_fileDTOList != null || _fileDTOList != undefined)) {
        _fileDTO.FileList = _fileDTOList;
    }
    return _fileDTO;
}

//#endregion 