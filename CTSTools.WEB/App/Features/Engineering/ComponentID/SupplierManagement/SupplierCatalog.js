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
    $("#dxSupplirFileUploader").dxFileUploader({
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
                    caption: "Option",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#SupplierModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenSupplierID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenSupplierID").val(options.data.ID);
                                ShowSupplierDeleteQuestion();
                            }).appendTo(container);
                    },
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
function SupplierActionButtons(Action) {
    $("#SupplierActionButtons").empty();
    document.getElementById("SupplierModalButton").addEventListener("click", ClearSupplierFields);
    document.getElementById("SupplierCloseModalButton").addEventListener("click", ClearSupplierFields);

    if (Action == "Save") {
        document.getElementById("SupplierActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSupplierButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSupplierButton").addEventListener("click", CreateSupplier_Global);
    }
    else {
        // Update
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
function ClearExcelModalFields() {
    $('#successMessage').hide();
    $('#errorMessages').hide();
    var uploader = $("#dxSupplirFileUploader").dxFileUploader("instance");
    if (uploader) {
        uploader.option("visible", true);
    }
    if (uploader) {
        uploader.reset();
    }
}
function ShowSupplierValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null)
    {
        if (_validationResultDTO.Data.SupplierGoodLinesList.length > 0) {
            successMessage = `Suppliers were created successfully.`;
            $('#successMessage').text(successMessage).show();
            document.getElementById('successMessage').removeAttribute('hidden');
            ClearSupplierFields();
        }
        if (_validationResultDTO.Data.SupplierBadLinesList.length > 0) {
            errorMessages = '<strong>Wrong data:</strong><ul>';
            _validationResultDTO.Data.SupplierBadLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row error:<br>Name: ${badLine.Name}. Description = ${badLine.Description}. Is Vendor = ${badLine.IsVendor}. Is Manufacturer = ${badLine.IsManufacturer}.</li>`;
            });
            errorMessages += '</ul>';
            $('#errorMessages').html(errorMessages).show();
            document.getElementById('errorMessages').removeAttribute('hidden');
        }
    }
    if (_validationResultDTO.Message == "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        $('#errorMessages').html(errorMessages).show();
        document.getElementById('errorMessages').removeAttribute('hidden');
    }
    if (_validationResultDTO.Message == "Don't have access to this action.") {
        $('#UploadExcelSupplirModal').modal('hide');
        ClearExcelModalFields();
        return HostResponse(_validationResultDTO);
    }
    $("#dxSupplirFileUploader").dxFileUploader("instance").option("visible", false);
}
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