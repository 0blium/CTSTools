import { CreateAttribute, CreateMassiveAttribute, UpdateAttribute, DeleteAttribute, GetDXAttributeDataSource } from './Attribute/Attribute_Service.js';
import { CreateValue, CreateMassiveValue, UpdateValue, DeleteValue, GetDXValueDataSource } from './Value/Value_Service.js'
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
    $("#dxValueFileUploader").dxFileUploader({
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
                    var _propetieNameArray = ValuePropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveValue(_fileDTO);
                    ShowValueValidationResults(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
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
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 80,
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
                                    $("#hiddenValueID").val(options.data.ID);
                                    $('#ValueModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    $("#hiddenValueID").val(options.data.ID);
                                    ShowDeleteValueQuestion();
                                }
                            },
                        });
                    }
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
    document.getElementById("UploadExcelValueCloseModalButton").addEventListener("click", ClearExcelValueModal);
    document.getElementById("ClearExcelValueButton").addEventListener("click", ClearExcelValueModal);
    document.getElementById("ExcelValueFormatButton").addEventListener("click", ExportValueExcelFormat);
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
    $('#ValueModal').modal('hide');
    $("#hiddenValueID").val("");
    $("#dxValueNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxCodeTextBox").dxTextBox("instance").option("value", "");
    $("#dxValueDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxValueIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxValueGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxValueGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
//#region Value Excel functions
function ExportValueExcelFormat() {
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Attribute', key: 'attribute', width: 30 },
        { header: 'Name', key: 'name', width: 30 },
        { header: 'Code', key: 'code', width: 30 },
        { header: 'Description', key: 'description', width: 30 },
    ];
    // Set the header style to bold
    worksheet.getRow(1).font = { bold: true };
    // Create the Excel file and download it
    workbook.xlsx.writeBuffer().then(function (buffer) {
        var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'ValueFormat.xlsx';
        link.click();
    });
}
function ClearExcelValueModal() {
    $('#successValueMessage').hide();
    $('#errorValueMessages').hide();
    var uploader = $("#dxValueFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (uploader) {
        uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (uploader) {
        uploader.reset();
    }
}
function ShowValueSuccessMessageExcelModal(Message) {
    $('#successValueMessage').text(Message).show();
    $('#successValueMessage').removeAttr('hidden');
}
function ShowValueErrorMessagesExcelModal(Message) {
    $('#errorValueMessages').html(Message).show();
    $('#errorValueMessages').removeAttr('hidden');
}
function ShowValueValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "Values were created successfully.";
            ShowValueSuccessMessageExcelModal(successMessage);
            $("#dxValueGrid").dxDataGrid("instance").refresh();
            ClearValueFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "Values created: Some were skipped due to missing or invalid data.";
            ShowValueSuccessMessageExcelModal(successMessage);
            $("#dxValueGrid").dxDataGrid("instance").refresh();
            ClearValueFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row ${badLine.ID}:<br>Name: ${badLine.Name}, Code: ${badLine.Code}, Attribute: ${badLine.AttributeName}, Description = ${badLine.Description}.</li>`;
            });
            errorMessages += '</ul>';
            ShowValueErrorMessagesExcelModal(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowValueErrorMessagesExcelModal(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelValueModal').modal('hide');
        ClearExcelValueModal();
        return HostResponse(_validationResultDTO);
    }
    // Hide FileUploader to show messages
    $("#dxValueFileUploader").dxFileUploader("instance").option("visible", false);
}
function ValuePropertyNameArray() {
    // With Object.keys we create an array of properties of the ValueDTO object
    var _propertyNameArray = Object.keys(GetValueDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive");
    return _propertyNameArray;
}
//#endregion
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
                    var _propetieNameArray = AttributePropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
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
    document.getElementById("ExcelAttributeFormatButton").addEventListener("click", ExportAttributteExcelFormat);
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
            '<button class="btn btn-success m-b-15 float-end" id="CreateAttributeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateAttributeButton").addEventListener("click", CreateAttribute_Global);
    }
    else {
        // Update
        document.getElementById("AttributeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearAttributeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateAttributeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearAttributeButton").addEventListener("click", ClearAttributeFields);
        document.getElementById("UpdateAttributeButton").addEventListener("click", UpdateAttribute_Global);
    }
}
//#region Attributte Excel functions
function ExportAttributteExcelFormat() {
    // Example data
    var data = [
        { HasMultipleOptions: 'true' }
    ];
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Name', key: 'name', width: 30 },
        { header: 'Description', key: 'description', width: 30 },
        { header: 'Has Multiple Options', key: 'HasMultipleOptions', width: 25 },
    ];
    // Set the header style to bold
    worksheet.getRow(1).font = { bold: true };
    // Add the data to the Excel file
    data.forEach(item => {
        worksheet.addRow(item);
    });
    // Create the Excel file and download it
    workbook.xlsx.writeBuffer().then(function (buffer) {
        var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'AttributteFormat.xlsx';
        link.click();
    });
}
function ClearExcelAttributeModal() {
    $('#successAttributeMessage').hide();
    $('#errorAttributeMessages').hide();
    var uploader = $("#dxAttributeFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (uploader) {
        uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (uploader) {
        uploader.reset();
    }
}
function ShowAttributeSuccessMessageExcelModal(Message) {
    $('#successAttributeMessage').text(Message).show();
    $('#successAttributeMessage').removeAttr('hidden');
}
function ShowAttributeErrorMessagesExcelModal(Message) {
    $('#errorAttributeMessages').html(Message).show();
    $('#errorAttributeMessages').removeAttr('hidden');
}
function ShowAttributeValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "Attributes were created successfully.";
            ShowAttributeSuccessMessageExcelModal(successMessage);
            $("#dxAttributeGrid").dxDataGrid("instance").refresh();
            ClearAttributeFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "Attributes created: Some were skipped due to missing or invalid data.";
            ShowAttributeSuccessMessageExcelModal(successMessage);
            $("#dxAttributeGrid").dxDataGrid("instance").refresh();
            ClearAttributeFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row ${badLine.ID}:<br>Name: ${badLine.Name}, Description = ${badLine.Description}, Has Multiple Options = ${badLine.HasMultipleOptions}.</li>`;
            });
            errorMessages += '</ul>';
            ShowAttributeErrorMessagesExcelModal(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowAttributeErrorMessagesExcelModal(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelAttributeModal').modal('hide');
        ClearExcelAttributeModal();
        return HostResponse(_validationResultDTO);
    }
    // Hide FileUploader to show messages
    $("#dxAttributeFileUploader").dxFileUploader("instance").option("visible", false);
}
function AttributePropertyNameArray() {
    // With Object.keys we create an array of properties of the AttributeDTO object
    var _propertyNameArray = Object.keys(GetAttributeDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive");
    return _propertyNameArray;
}
//#endregion
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


