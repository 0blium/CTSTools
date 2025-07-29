import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { CreateSupplier, CreateMassiveSupplier, UpdateSupplier, DeleteSupplier, GetDXSupplierDataSource } from './Supplier/Supplier_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeSupplierCatalogControls();
});


async function InitializeSupplierCatalogControls() {
    $("#dxSupplierNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });

    $("#dxSupplierDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });

    $("#dxSupplierIsActiveCheckBox").dxCheckBox({
        value: true
    });

    $("#dxSupplierIsVendorCheckBox").dxCheckBox({
        value: false
    });
    $("#dxSupplierIsManufacturerCheckBox").dxCheckBox({
        value: false
    });
    $("#dxSupplierFileUploader").dxFileUploader({
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
                    var _propetieNameArray = SupplierPropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveSupplier(_fileDTO);
                    ShowSupplierValidationResults(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
        }
    });
    $("#dxSupplierGrid").dxDataGrid({
        dataSource: await GetDXSupplierDataSource(),
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
            fileName: "SupplierCatalog",
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
        }, headerFilter: {
            visible: true
        },
        onSelectionChanged: function (data) {
            let _supplierData = data.selectedRowsData[0];
            if (_supplierData != null) {
                SupplierActionButtons("Update");
                PopulateSupplierFields(_supplierData);
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
                                    $("#hiddenSupplierID").val(options.data.ID);
                                    $('#SupplierModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    $("#hiddenSupplierID").val(options.data.ID);
                                    ShowSupplierDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Is Vendor", dataField: "IsVendor" },
                { caption: "Is Manufacturer", dataField: "IsManufacturer" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
            ],
    });
    SupplierActionButtons("Save");
    document.getElementById("UploadExcelSupplirCloseModalButton").addEventListener("click", ClearExcelModalFields);
    document.getElementById("ClearExcelSupplierButton").addEventListener("click", ClearExcelModalFields);   
    document.getElementById("ExcelSupplierFormatButton").addEventListener("click", ExportSupplierExcelFormat);   
}
function SupplierActionButtons(Action) {
    $("#SupplierActionButtons").empty();
    document.getElementById("SupplierModalButton").addEventListener("click", ClearSupplierFields);
    document.getElementById("SupplierCloseModalButton").addEventListener("click", ClearSupplierFields);
    document.getElementById('SupplierModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById('SupplierModalTitle').innerText = 'Add Supplier';
        document.getElementById("SupplierActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSupplierButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSupplierButton").addEventListener("click", CreateSupplier_Global);
    }
    else {
        // Update
        document.getElementById('SupplierModalTitle').innerText = 'Update Supplier';
        document.getElementById("SupplierActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearSupplierButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSupplierButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateSupplierButton").addEventListener("click", UpdateSupplier_Global);
    }
}
function ClearSupplierFields() {
    $("#SupplierModal").modal("hide");
    SupplierActionButtons("Save");
    $('#hiddenSupplierID').val("");
    $("#dxSupplierIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSupplierNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxSupplierDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxSupplierGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSupplierGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSupplierGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSupplierGrid").dxDataGrid("instance").refresh();
    $("#dxSupplierIsVendorCheckBox").dxCheckBox("instance").option("value", false);
    $("#dxSupplierIsManufacturerCheckBox").dxCheckBox("instance").option("value", false);
    ClearErrorFeedback();
}
//#region Supplier Excel functions
function ExportSupplierExcelFormat() {
    // Example data
    var data = [
        { isvendor: 'true', ismanufacturer: 'false' }
    ];
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Name', key: 'name', width: 30 },
        { header: 'Description', key: 'description', width: 30 },
        { header: 'Is Vendor', key: 'isvendor', width: 20 },
        { header: 'Is Manufacturer', key: 'ismanufacturer', width: 20 }
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
        link.download = 'SupplierFormat.xlsx';
        link.click();
    });
}
function ClearExcelModalFields() {
    $('#successMessage').hide();
    $('#errorMessages').hide();
    var uploader = $("#dxSupplierFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (uploader) {
        uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (uploader) {
        uploader.reset();
    }
}
function ShowSupplierSuccessMessageExcelModal(Message) {
    $('#successMessage').text(Message).show();
    $('#successMessage').removeAttr('hidden');
}
function ShowSupplierErrorMessagesExcelModal(Message) {
    $('#errorMessages').html(Message).show();
    $('#errorMessages').removeAttr('hidden');
}
function ShowSupplierValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    // Hide FileUploader to show messages
    $("#dxSupplierFileUploader").dxFileUploader("instance").option("visible", false);
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "Suppliers were created successfully.";
            ShowSupplierSuccessMessageExcelModal(successMessage);
            ClearSupplierFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "Suppliers created: Some were skipped due to missing or invalid data.";
            ShowSupplierSuccessMessageExcelModal(successMessage);
            ClearSupplierFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row ${badLine.ID}:<br>Name: ${badLine.Name}, Description = ${badLine.Description}, Is Vendor = ${badLine.IsVendor}, Is Manufacturer = ${badLine.IsManufacturer}.</li>`;
            });
            errorMessages += '</ul>';
            ShowSupplierErrorMessagesExcelModal(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowSupplierErrorMessagesExcelModal(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelSupplierModal').modal('hide');
        ClearExcelModalFields();
        return HostResponse(_validationResultDTO);
    }
}
function SupplierPropertyNameArray() {
    // With Object.keys we create an array of properties of the SupplierDTO object
    var _propertyNameArray = Object.keys(GetSupplierDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive");
    return _propertyNameArray;
}
//#endregion
function PopulateSupplierFields(data) {
    $('#hiddenSupplierID').val(data.ID);
    $("#dxSupplierIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxSupplierNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxSupplierIsVendorCheckBox").dxCheckBox("instance").option("value");
    $("#dxSupplierIsManufacturerCheckBox").dxCheckBox("instance").option("value");
    $("#dxSupplierDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetSupplierDTO() {
    let _supplierDTO = {
        ID: $('#hiddenSupplierID').val(),
        Name: $("#dxSupplierNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxSupplierDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxSupplierIsActiveCheckBox").dxCheckBox("instance").option("value"),
        IsVendor: $("#dxSupplierIsVendorCheckBox").dxCheckBox("instance").option("value"),
        IsManufacturer: $("#dxSupplierIsManufacturerCheckBox").dxCheckBox("instance").option("value"),
    }
    return _supplierDTO;
}
async function ShowSupplierDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this supplier',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSupplier_Global();
    } else {
        ClearSupplierFields();
    }
}
async function CreateSupplier_Global() {
    await dxLoadPanel.show();
    const _supplierDTO = GetSupplierDTO();
    const _validation_ResultDTO = await CreateSupplier(_supplierDTO);
    if (_validation_ResultDTO.Result)
        ClearSupplierFields();
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSupplier_Global() {
    await dxLoadPanel.show();
    const _supplierDTO = GetSupplierDTO();
    const _validation_ResultDTO = await UpdateSupplier(_supplierDTO);
    if (_validation_ResultDTO.Result)
        ClearSupplierFields();
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteSupplier_Global() {
    await dxLoadPanel.show();
    const _supplierDTO = GetSupplierDTO();
    const _validation_ResultDTO = await DeleteSupplier(_supplierDTO);
    if (_validation_ResultDTO.Result)
        ClearSupplierFields();
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}