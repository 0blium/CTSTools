import { CreateAttribute, CreateMassiveAttribute, UpdateAttribute, DeleteAttribute, GetDXAttributeDataSource } from './Attribute/Attribute_Service.js';
import { CreateValue, UpdateValue, DeleteValue, GetDXValueDataSource } from './Value/Value_Service.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js';
import { Attributes } from './Attribute/Attribute_Enum.js'


//#region Attribute Catalog
document.addEventListener("DOMContentLoaded", () => {
    InitializeAttributeListControls();
    InitializeValueCatalogControls();
});

//#region Value Behavior Functions
async function InitializeValueCatalogControls() {
    $("#dxAttributeSelectBox").dxSelectBox({
        dataSource: await GetValueAttributeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnabled: true,
        onSelectionChanged: function (e) {
            document.getElementById("ValueModalTitle").innerText = e.selectedItem.Name + " Form";
            document.getElementById("valuemodalbutton").style.visibility = null;
            document.getElementById("valuemodalbutton").innerHTML = "<i class=\"fa-solid fa-circle-plus\"></i> " + e.selectedItem.Name;
            GetValueGridDataSource();
        }
    });



    $("#dxValueNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxCodeTextBox").dxTextBox({
        placeholder: 'Type code...'
    });
    $("#dxValueDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxValueIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxValueGrid").dxDataGrid({
        dataSource: '',
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        showRowLines: true,
        showColumnLines: true,
        showBorders: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: true,
        columnAutoWidth: true,
        loadPanel: false,

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
            fileName: "ValueCatalog",
            allowExportSelectedData: true
        },
        filterRow:
        {
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
            let _valueData = data.selectedRowsData[0];
            if (_valueData != null) {
                ValueActionButtons("Update");
                PopulateValueFields(_valueData);
            }
        },
        columns:
            [
                {
                    caption: "Option",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#ValueModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenValueID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenValueID").val(options.data.ID);
                                ShowDeleteValueQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Code", dataField: "Code" },
                { caption: "Attribute", dataField: "AttributeName", visible: false },
                { caption: "Attribute ID", dataField: "AttributeID", visible: false },
                { caption: "Description", dataField: "Description" },
                { caption: "Is a Counter", dataField: "IsCounter", visible: false },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Last Update By", dataField: "LastUpdateByName", visible: false },
                { caption: "Added By", dataField: "AddedByName", visible: false },

                { caption: "Is Active", dataField: "IsActive" }
            ],
    });
    ValueActionButtons("Save");
}
async function GetValueAttributeDataSource_Global() {
    let _filters = [
        ["ID", "<>", Attributes.Class], "and" ,
        ["ID", "<>", Attributes.ComponentType], "and",
        ["ID", "<>", Attributes.SubClass], "and",
        ["ID", "<>", Attributes.PartType], "and" , 
        ["ID", "<>", Attributes.ClassID]

    ];
    let _atttributeDTO = { IsActive: true }
    return await GetDXAttributeDataSource(_atttributeDTO, _filters)
}
async function PopulateValueFields(ValueDTO) {
    $("#hiddenValueID").val(ValueDTO.ID);
    $("#dxValueNameTextBox").dxTextBox("instance").option("value", ValueDTO.Name);
    $("#dxCodeTextBox").dxTextBox("instance").option("value", ValueDTO.Code);
    $("#dxValueDescriptionTextArea").dxTextArea("instance").option("value", ValueDTO.Description);
    $("#dxValueIsActiveCheckBox").dxCheckBox("instance").option("value", ValueDTO.IsActive);

}
async function ShowDeleteValueQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the Value, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        const _valueDTO = GetValueDTO();
        DeleteValue_Global(_valueDTO);
    }
}
function ValueActionButtons(Action) {
    $("#ValueActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("ValueActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateValueButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateValueButton").addEventListener("click", CreateValue_Global);
    }
    else {
        // Update
        document.getElementById("ValueActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="UpdateValueButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateValueButton").addEventListener("click", UpdateValue_Global);
        document.getElementById("ValueModalCloseButton").addEventListener("click", ClearValueFields);
    }
}
function ClearValueFields() {
    ValueActionButtons("Save");
    $("#hiddenValueID").val("");
    $("#dxValueNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxCodeTextBox").dxTextBox("instance").option("value", "");
    $("#dxValueDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxValueIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxValueGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxValueGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetValueDTO() {
    let _valueDTO = {
        ID: $("#hiddenValueID").val(),
        Name: $("#dxValueNameTextBox").dxTextBox("instance").option("value"),
        Code: $("#dxCodeTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxValueDescriptionTextArea").dxTextArea("instance").option("value"),
        AttributeID: $("#dxAttributeSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxValueIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _valueDTO;
}
async function GetValueGridDataSource() {
    let _valueDTO = {
        AttributeID: $("#dxAttributeSelectBox").dxSelectBox("instance").option("value"),
    }
    $("#dxValueGrid").dxDataGrid("instance").option("dataSource", await GetDXValueDataSource(_valueDTO));

}
//#endregion

//#region Attribute CRUD Functions
async function CreateValue_Global() {
    await dxLoadPanel.show();
    const _valueDTO = GetValueDTO();
    const _validation_resultDTO = await CreateValue(_valueDTO)
    $("#dxValueGrid").dxDataGrid("instance").refresh();
    HostResponse(_validation_resultDTO);
    ClearValueFields();
    dxLoadPanel.hide();
}
async function UpdateValue_Global() {
    await dxLoadPanel.show();
    const _valueDTO = GetValueDTO();
    const _validation_resultDTO = await UpdateValue(_valueDTO)
    $("#dxValueGrid").dxDataGrid("instance").refresh();
    HostResponse(_validation_resultDTO);
    ClearValueFields();
    $("#ValueModal").modal('toggle');

    dxLoadPanel.hide();
}
async function DeleteValue_Global() {
    await dxLoadPanel.show();
    const _valueDTO = GetValueDTO();
    const _validation_resultDTO = await DeleteValue(_valueDTO)
    $("#dxValueGrid").dxDataGrid("instance").refresh();
    HostResponse(_validation_resultDTO);
    ClearValueFields();
    dxLoadPanel.hide();
}
//#endregion

//#endregion

//#region Attribute List

//#region Behavior Functions
async function InitializeAttributeListControls() {
    $("#dxAttributeNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxAttributeDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxAttributeIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxHasMultipleOptionsCheckBox").dxCheckBox({
        value: true
    });
    $("#dxAttributeFileUploader").dxFileUploader({
        accept: ".xlsx",
        selectButtonText: "Select Excel File",
        labelText: "or Drop here",
        uploadMode: "instantly",
        onValueChanged: function (e) {
            var file = e.value[0];
            let _fileDTO;
            let _validationResultDTO;
            if (file) {
                var filename = file.name;
                var reader = new FileReader();
                reader.onload = async function (readerEvent) {
                    var base64File = readerEvent.target.result;
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveAttribute(_fileDTO);
                    ShowAttributeValidationResults(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
        }
    });
    $("#dxAttributeGrid").dxDataGrid({
        dataSource: await GetDXAttributeDataSource_Global(),
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        showRowLines: true,
        showColumnLines: true,
        showBorders: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: true,
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
            fileName: "AttributeCatalog",
            allowExportSelectedData: true
        },
        filterRow:
        {
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
            let _attributeData = data.selectedRowsData[0];
            if (_attributeData != null) {
                AttributeActionButtons("Update");
                PopulateAttributeFields(_attributeData);
            }
        },
        columns:
            [
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Added By ID", dataField: "AddedByID", width: 200, visible: false },
                { caption: "Added By", dataField: "AddedByName", visible: false },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName", visible: false },
                { caption: "Has Multiple Options", dataField: "HasMultipleOptions" },
                { caption: "Is Active", dataField: "IsActive" },
                {
                    caption: "Delete",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 70,
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenAttributeID").val(options.data.ID);
                                ShowDeleteAttributeQuestion();
                            }).appendTo(container);
                    },
                },
            ],
    });
    AttributeActionButtons("Save");
    document.getElementById("UploadExcelAttributeCloseModalButton").addEventListener("click", ClearExcelAttributeModal);
    document.getElementById("ClearExcelAttributeButton").addEventListener("click", ClearExcelAttributeModal);
}
async function PopulateAttributeFields(AttributeDTO) {
    $("#hiddenAttributeID").val(AttributeDTO.ID);
    $("#dxAttributeNameTextBox").dxTextBox("instance").option("value", AttributeDTO.Name);
    $("#dxAttributeDescriptionTextArea").dxTextArea("instance").option("value", AttributeDTO.Description);
    $("#dxAttributeIsActiveCheckBox").dxCheckBox("instance").option("value", AttributeDTO.IsActive);
    $("#dxHasMultipleOptionsCheckBox").dxCheckBox("instance").option("value", AttributeDTO.HasMultipleOptions);

}
async function ShowDeleteAttributeQuestion() {
    const _alert = await Swal.fire({
        title: 'You will delete the attribute, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        const _attributeDTO = GetAttributeDTO();
        DeleteAttribute_Global(_attributeDTO);
    }

}
function AttributeActionButtons(Action) {
    $("#AttributeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("AttributeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<a class="btn btn-success mb-2" id="UploadExcelAttributeModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelAttributeModal"><i class="fa-solid fa-file-import"></i>Excel</a>'+
            '<button class="btn btn-success m-b-15 float-end" id="CreateAttributeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateAttributeButton").addEventListener("click", CreateAttribute_Global);
    }
    else {
        // Update
        document.getElementById("AttributeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<a class="btn btn-success mb-2" id="UploadExcelAttributeModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelAttributeModal"><i class="fa-solid fa-file-import"></i>Excel</a>' +
            '<button class="btn btn-secondary float-end" id="ClearAttributeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateAttributeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearAttributeButton").addEventListener("click", ClearAttributeFields);
        document.getElementById("UpdateAttributeButton").addEventListener("click", UpdateAttribute_Global);
    }
}
function ClearExcelAttributeModal() {
    $('#successAttributeMessage').hide();
    $('#errorAttributeMessages').hide();
    var uploader = $("#dxAttributeFileUploader").dxFileUploader("instance");
    if (uploader) {
        uploader.option("visible", true);
    }
    if (uploader) {
        uploader.reset();
    }
}
function ShowAttributeValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    debugger;
    if (_validationResultDTO.Data != null) {
        if (_validationResultDTO.Data.AttributeGoodLinesList.length > 0) {
            successMessage = `Attribute were created successfully.`;
            $('#successAttributeMessage').text(successMessage).show();
            document.getElementById('successAttributeMessage').removeAttribute('hidden');
        }
        if (_validationResultDTO.Data.AttributeBadLinesList.length > 0) {
            errorMessages = '<strong>Wrong data:</strong><ul>';
            _validationResultDTO.Data.AttributeBadLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row error:<br>Name: ${badLine.Name}. Description = ${badLine.Description}. Has Multiple Options = ${badLine.HasMultipleOptions}. Is Active = ${badLine.IsActive}.</li>`;
            });
            errorMessages += '</ul>';
            $('#errorAttributeMessages').html(errorMessages).show();
            document.getElementById('errorAttributeMessages').removeAttribute('hidden');
        }
    }
    if (_validationResultDTO.Message == "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        $('#errorAttributeMessages').html(errorMessages).show();
        document.getElementById('errorAttributeMessages').removeAttribute('hidden');
    } else {
        $('#UploadExcelAttributeModal').modal('hide');
        ClearExcelAttributeModal();
        return HostResponse(_validationResultDTO);
    }
    $("#dxAttributeFileUploader").dxFileUploader("instance").option("visible", false);
    $("#dxAttributeGrid").dxDataGrid("instance").refresh();
    ClearAttributeFields();
}
function ClearAttributeFields() {
    AttributeActionButtons("Save");
    $("#hiddenAttributeID").val("");
    $("#dxAttributeNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxAttributeDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxAttributeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxHasMultipleOptionsCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxAttributeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxAttributeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxAttributeSelectBox").dxSelectBox("getDataSource").reload();
    ClearErrorFeedback();
}
function GetAttributeDTO() {
    let _attributeDTO = {
        ID: $("#hiddenAttributeID").val(),
        Name: $("#dxAttributeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxAttributeDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxAttributeIsActiveCheckBox").dxCheckBox("instance").option("value"),
        HasMultipleOptions: $("#dxHasMultipleOptionsCheckBox").dxCheckBox("instance").option("value")
    }
    return _attributeDTO;
}
//#endregion

//#region Attribute CRUD Functions
async function CreateAttribute_Global() {
    await dxLoadPanel.show();
    const _attributeDTO = GetAttributeDTO();
    const _validation_resultDTO = await CreateAttribute(_attributeDTO)
    $("#dxAttributeGrid").dxDataGrid("instance").refresh();
    HostResponse(_validation_resultDTO)
    if (_validation_resultDTO.Result) {
        ClearAttributeFields();
    }
    dxLoadPanel.hide();
}
async function UpdateAttribute_Global() {
    await dxLoadPanel.show();
    const _attributeDTO = GetAttributeDTO();
    const _validation_resultDTO = await UpdateAttribute(_attributeDTO)
    $("#dxAttributeGrid").dxDataGrid("instance").refresh();
    HostResponse(_validation_resultDTO);
    if (_validation_resultDTO.Result) {
        ClearAttributeFields();
    }
    dxLoadPanel.hide();
}
async function DeleteAttribute_Global() {
    await dxLoadPanel.show();
    const _attributeDTO = GetAttributeDTO();
    const _validation_resultDTO = await DeleteAttribute(_attributeDTO)
    $("#dxAttributeGrid").dxDataGrid("instance").refresh();
    HostResponse(_validation_resultDTO);
    if (_validation_resultDTO.Result) {
        ClearAttributeFields();
    }
    dxLoadPanel.hide();
}
const GetDXAttributeDataSource_Global = () => {
    let _filters = [
        ["ID", "<>", Attributes.Class], "and",
        ["ID", "<>", Attributes.ComponentType], "and",
        ["ID", "<>", Attributes.SubClass], "and",
        ["ID", "<>", Attributes.PartType], "and",
        ["ID", "<>", Attributes.ClassID]

    ];
    return GetDXAttributeDataSource("", _filters)
}
//#endregion

//#endregion


