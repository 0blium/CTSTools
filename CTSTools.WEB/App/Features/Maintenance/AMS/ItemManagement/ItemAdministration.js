
//#region Import service and resources
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXItem_HeaderDataSource, CreateItem_Header, CreateMassiveItem_Header, UpdateItem_Header, DeleteItem_Header, GetItem_HeaderFilesInformation, DeleteItem_HeaderFile, SaveItem_HeaderMultipleFile } from './Item_Header/Item_Header_Service.js'
import { GetDXItem_SupportGroupDataSource, CreateItem_SupportGroup, UpdateItem_SupportGroup, DeleteItem_SupportGroup, GetItem_SupportGroupInformation } from './Item_SupportGroup/Item_SupportGroup_Service.js'
import { GetDXSupportGroupDataSource } from '../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetDXUserDataSource } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetDXBrandDataSource } from '../../../AdvancedSettings/Brand/Brand_Service.js'
import { GetDXStatus_StatusTypeDataSource } from '../../../AdvancedSettings/StatusManagement/Status_StatusType/Status_StatusType_Service.js'
import { CreateItem_Line, UpdateItem_Line, DeleteItem_Line, GetDXItem_LineDataSource, GetItem_LineMasterDetailInformation, GetItem_LineFilesInformation, DeleteItem_LineFile, GetItem_LineFilesTreeView, ReassignSupportGroup } from './Item_Line/Item_Line_Service.js'
import { CreateUserDefined, UpdateUserDefined, DeleteUserDefined, GetDXUserDefinedDataSource } from './UserDefined/UserDefined_Service.js'
import { GetDXDataTypeDataSource } from '../../../AdvancedSettings/DataType/DataType_Service.js'
import { GetUserInformation } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { UpdateUserDefinedTemplate, GetUserDefinedTemplateInformation } from './UserDefinedTemplate/UserDefinedTemplate_Service.js'
import { GetDXStationDataSource } from '../StationManagement/Station/Station_Service.js'
import { GetDXSupplyTypeDataSource } from '../../../AdvancedSettings/SupplyType/SupplyType_Service.js'


import { StatusType_Enum } from '../../../AdvancedSettings/StatusManagement/StatusType/StatusType_Enum.js'
import { Role_Enum } from '../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'
import { DataType_Enum } from '../../../AdvancedSettings/DataType/DataType_Enum.js'
import { SupplyType_Enum } from '../../../AdvancedSettings/SupplyType/SupplyType_Enum.js'
import { GetDXClassDataSource } from '../../../Engineering/ComponentID/Class/Class_Service.js'
import { GetDXSubClassDataSource } from '../../../Engineering/ComponentID/Class/SubClass/SubClass_Service.js'
//#endregion

let _fileDTOList = [];
let _fileDTO = {};
document.addEventListener("DOMContentLoaded", async () => {
    InitializeItemAdministrationCatalogControls();
    InitializeUserDefinedControls();
    //InitializeItem_LineModalControls();
    InitializeItem_SupportGroupControls();
    //InitializeReassignSupportGroupModalControls();
    await GetUserInformationbyID();
    EventHandler();
});
async function EventHandler() {
    document.getElementById('AddNewItemHeaderModal').addEventListener('hidden.bs.modal', async function (event) {
        ClearItem_HeaderFields(true);
    });
    document.getElementById("AddNewItemHeaderBtn").addEventListener("click", function () {
        ClearItem_HeaderFields();
    }); // Clears all fields before opening the New Item modal in case ID isn't cleare
    // Assuming your DevExpress button has a specific class like "dx-my-button"
    let devExpressButton = document.querySelectorAll(".dx-fileuploader-button");
    devExpressButton.forEach(function (currentButton) {
        // Remove DevExpress button classes
        currentButton.classList.remove("dx-button", "dx-widget", "dx-button-normal", "dx-button-mode-contained", "dx-widget", "dx-button-has-text");

        // Add Bootstrap button classes
        currentButton.classList.add("btn", "btn-default");
    });
}
//#region User
async function GetUserInformationbyID() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _userInformation = await GetUserInformation(_userDTO);
    await DisabledAdministrationFields(_userInformation[0]);
    await FilterDataSourceBySupportGroups(_userInformation[0])
    dxLoadPanel.hide();
}
function GetUserDTO() {
    let _userDTO = {
        ID: document.getElementById('hiddenUserID').value,
        GetSupportGroupArray: true,
        GetRoleArray: true
    }
    return _userDTO;
}
//#endregion
//#region CRUD Item Administration
async function InitializeItemAdministrationCatalogControls() {
    $("#dxTreeViewList").dxTreeView({
        itemTemplate: function (itemData, itemIndex, itemElement) {

            if (itemData.ParentID == null) {
                itemElement.append(itemData.Name);
            }
            else {
                itemElement.append('<div class="">' +
                    "<i class=\"fas fa-file pe-2\" style=\"color:#265755\"></i>" +
                    '<a href="' + itemData.URL + '" target="_blank" download="' + itemData.Name + '">' +
                    itemData.Name +
                    '</a>' +
                    '</div>');
            }
        },
        dataStructure: 'plain',
        keyExpr: 'TreeViewID',
        displayExpr: 'Name',
        parentIdExpr: 'ParentID',
        expandAllEnabled: true,
    });
    //$("#dxFileOptionsSelectBox").dxSelectBox({
    //    dataSource: [
    //        "Item",
    //        "Lines"
    //    ],
    //    onSelectionChanged: function (e) {
    //        if ($("#dxFileOptionsSelectBox").dxSelectBox("instance").option("value") != 0 &&
    //            $("#dxFileOptionsSelectBox").dxSelectBox("instance").option("value") != null
    //        ) {
    //            if (e.selectedItem == "Item") {
    //                $("#Item_HeaderAttachment").removeAttr("hidden");
    //                $("#TreeView").attr("hidden", true);
    //            }
    //            else {
    //                $("#Item_HeaderAttachment").attr("hidden", true);
    //                $("#TreeView").removeAttr("hidden");
    //            }
    //        }
    //    },
    //    searchEnabled: false,
    //    popupWidth: 400,
    //});
    $("#dxItemAdministrationDatGrid").dxDataGrid({
        dataSource: [],
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        keyExpr: 'ID',
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
            fileName: "Inventory",
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
            let _itemAdministrationData = data.selectedRowsData[0];
            if (_itemAdministrationData != null) {
                $("#hiddenItem_HeaderID").val(_itemAdministrationData.Item_HeaderID);
                $("#hiddenItem_SupportGroupID").val(_itemAdministrationData.ID);
                $("#hiddenUserSupportGroupID").val(_itemAdministrationData.SupportGroupID)
                Item_HeaderActionButtons("Update");
                await PopulateItem_HeaderFields(_itemAdministrationData);
                GetItem_HeaderFileList();
                await GetUserDefinedTemplateList();
                //GetItem_LineTreeViewList();
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
                                    { text: "Attachments", icon: "fa fa-paperclip text-info", value: 2 },
                                    { text: "Assets", icon: "fas fa-th-list text-primary", value: 3 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 4 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $("#hiddenItem_HeaderID").val(options.data.Item_HeaderID);
                                    $("#hiddenItem_SupportGroupID").val(options.data.ID);
                                    $('#AddNewItemHeaderModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    $('#AttachmentsModal').modal('show');
                                    document.getElementById("AttachmentsModalSaveBtn").addEventListener("click", SaveAttachmentsItem_Header);
                                }
                                else if (e.itemData.value == 3) {
                                    window.open("/App/Features/Maintenance/AMS/ItemManagement/ItemLineCatalog.aspx?Item_SupportGroupID=" + options.data.ID + "&Item_HeaderID=" + options.data.Item_HeaderID, "_blank");
                                }
                                else if (e.itemData.value == 4) {
                                    $("#hiddenItem_SupportGroupID").val(options.data.ID);
                                    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    ShowItem_SupportGroupDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Support Group",
                    dataField: "SupportGroupName",
                    alignment: "center",
                },
                {
                    caption: 'Image',
                    width: 200,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate(container, options) {
                        if (options.data.Item_HeaderDTO.ItemImg != null) {
                            $('<div>')
                                .append($('<img>', { src: options.data.Item_HeaderDTO.ItemImg, height: 150, width: 150 }))
                                .appendTo(container);
                        } else {
                            $('<div>')
                                .append($('<img>', { src: '/App/Common/Assets/img/no-product-image.png', height: 150 }))
                                .appendTo(container);
                        }
                    },
                },
                {
                    caption: "Model",
                    dataField: "Item_HeaderDTO.Model"
                },
                {
                    caption: "Class",
                    dataField: "Item_HeaderDTO.ClassName"
                },
                {
                    caption: "Sub Class",
                    dataField: "Item_HeaderDTO.SubClassName"
                },
                {
                    caption: "Brand",
                    dataField: "Item_HeaderDTO.BrandName"
                },
                {
                    caption: "Is Active?",
                    dataField: "Item_HeaderDTO.IsActive"
                },
                //{
                //    caption: "Is ESD?",
                //    dataField: "Item_HeaderDTO.IsESD"
                //},
                {
                    caption: "Added By ID",
                    dataField: "Item_HeaderDTO.AddedByID",
                    visible: false
                },
                {
                    caption: "Added By",
                    dataField: "Item_HeaderDTO.AddedByName"
                },
                {
                    caption: "Added Date",
                    dataField: "Item_HeaderDTO.AddedDate",
                    dataType: 'datetime'
                },
                {
                    caption: "Last Update By ID",
                    dataField: "Item_HeaderDTO.LastUpdateByID",
                    visible: false
                },
                {
                    caption: "Last Update By",
                    dataField: "Item_HeaderDTO.LastUpdateByName"
                },
                {
                    caption: "Last Update",
                    dataField: "Item_HeaderDTO.LastUpdate",
                    dataType: 'datetime'
                },
            ],
    });
    $("#dxItem_HeaderSelectBox").dxSelectBox({
        dataSource: await GetDXItem_HeaderDataSource(),
        valueExpr: "ID",
        displayExpr: "ModelWithBrand",
        deferRendering: false,
        searchEnabled: true,
        onSelectionChanged: function (e) {
            if ($("#dxItem_HeaderSelectBox").dxSelectBox("instance").option("value") != 0 &&
                $("#dxItem_HeaderSelectBox").dxSelectBox("instance").option("value") != null
            ) {
                DisabledProperties(true);
                $("#hiddenItem_HeaderID").val($("#dxItem_HeaderSelectBox").dxSelectBox("instance").option("value"));
            }
        },
    });
    $("#dxItem_HeaderModelTextBox").dxTextBox({
        placeholder: "Type model.."
    });
    $("#dxItem_HeaderBrandSelectBox").dxSelectBox({
        dataSource: await GetDXBrandDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
    });
    $("#dxItem_HeaderClassSelectBox").dxSelectBox({
        dataSource: await GetDXClassDataSource({IsActive:true}),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        onValueChanged: async function (e) {
            await $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").reset();

            if (e.value != 0 && e.value != null) {
                $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSubClassDataSource(
                    {
                        IsActive: true,
                        ClassID: e.value
                    }));                
            }
            else
            {
                $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").reset();
                $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        }
    });
    $("#dxItem_HeaderSubClassSelectBox").dxSelectBox({
        dataSource: await GetDXSubClassDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
    });
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox({
        value: true,
        readOnly: true,
        text: "Is Active?"
    });
    //$("#dxItem_HeaderIsESDCheckBox").dxCheckBox({
    //    value: false,
    //    text: "Is ESD?",
    //});
    $("#dxItem_HeaderAttachmentFileUploader").dxFileUploader({
        selectButtonText: "Select a file",
        labelText: "or drop it here",
        accept: "file",
        uploadedMessage: "Loading",
        multiple: true,
        uploadMode: "useButtons",
        invalidMaxFileSizeMessage: 'The file is too large. Allowed maximun size is 5MB',
        maxFileSize: 5000000,
        showFileList: true,
        width: "100%",
        onValueChanged: function (e) {
            var files = e.value;
            _fileDTOList = [];

            e.element.find(".dx-fileuploader-upload-button").hide();
            $.each(files, function (i, file) {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var _fileDTO = {
                        ID: $("#hiddenItem_HeaderID").val(),
                        Name: $("#dxItem_HeaderAttachmentFileUploader").dxFileUploader("instance").option("value")[i].name,
                        Size: $("#dxItem_HeaderAttachmentFileUploader").dxFileUploader("instance").option("value")[i].size,
                        Data: e.target.result,
                    };
                    _fileDTOList.push(_fileDTO);
                }
            })
        },
    })
    $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader({
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
                    $("#ItemThumbnail").attr('src', e.target.result);
                    _fileDTO = {
                        Name: $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").option("value")[0].name,
                        Size: $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").option("value")[0].size,
                        Data: e.target.result,
                    };
                    //_fileDTOList.push(_fileDTO);
                }
            }

        },
    });
    $("#dxItem_HeaderFileUploader").dxFileUploader({
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
                    var _propetieNameArray = Item_HeaderPropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveItem_Header(_fileDTO);
                    ShowItem_HeaderValidationResults(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
        }
    });
    document.getElementById("btnCloseItem_HeaderModal").addEventListener("click", ClearItem_HeaderFields);
    document.getElementById("UploadExcelItem_HeaderCloseModalButton").addEventListener("click", ClearExcelItem_HeaderModalFields);
    document.getElementById("ClearItem_HeaderExcelModalButton").addEventListener("click", ClearExcelItem_HeaderModalFields);
    document.getElementById("ExcelItem_HeaderFormatButton").addEventListener("click", ExportItem_HeaderExcelFormat);
    Item_HeaderActionButtons("Save");
}
function DisabledProperties(Action)
{
    $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").option("disabled", Action);
    $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("disabled", Action);
    $("#dxItem_HeaderBrandSelectBox").dxSelectBox("instance").option("disabled", Action);
    $("#dxItem_HeaderClassSelectBox").dxSelectBox("instance").option("disabled", Action);
    $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").option("disabled", Action);
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("disabled", Action);
    //$("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("disabled", Action);
}
async function SaveAttachmentsItem_Header() {
    await dxLoadPanel.show();
    const _fileList = GetFileDTO();
    if (!_fileList.FileList || _fileList.FileList.length === 0) {
        toastr["error"]("The list of files to upload is empty.", "Attachment error")
    }
    else {
        const _validation_ResultDTO = await SaveItem_HeaderMultipleFile(_fileList);
        HostResponse(_validation_ResultDTO);
    }
    dxLoadPanel.hide();
}
function ClearItem_HeaderFields(CleanGrid) {
    $('#AddNewItemHeaderModal').modal('hide');
    Item_HeaderActionButtons("Save");
    $("#hiddenItem_HeaderID").val("0");
    $("#hiddenItem_SupportGroupID").val("0");
    $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value", "");
    $("#dxItem_HeaderBrandSelectBox").dxSelectBox("instance").option("value", "");
    $("#dxItem_HeaderClassSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    //$("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value", false);
    //$("#dxItem_SupportGroupItem_HeaderIDSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_HeaderSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("disabled", false);
    $("#dxUserDefinedList").dxList("option", "dataSource", []);
    DisabledProperties(false);
    if (CleanGrid) {
        let keys = $("#dxItemAdministrationDatGrid").dxDataGrid("instance").getSelectedRowKeys();
        $("#dxItemAdministrationDatGrid").dxDataGrid("instance").deselectRows(keys);
        var dataGrid = $("#dxItemAdministrationDatGrid").dxDataGrid("instance");
        dataGrid.collapseAll(-1); // -1 always works
        $("#dxItemAdministrationDatGrid").dxDataGrid("instance").refresh();
    }
    $("#dxItem_HeaderThumbnailFileUploader").dxFileUploader("instance").reset();
    $("#TreeView").attr("hidden", true);
    $("#Item_HeaderAttachment").attr("hidden", true);
    _fileDTOList = [];
    _fileDTO = {};
    $("#ItemThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    ClearErrorFeedback();
    // Gets a reference to the "Create" tab link using its ID.
    // In this case, the <a> element must have id="CreateTab" in the HTML.
    let _createTab = new bootstrap.Tab(document.getElementById('CreateTab'));
    // Calls the 'show()' method to activate and display the "Create" tab.
    _createTab.show();
}
async function PopulateItem_HeaderFields(data) {
    document.getElementById("hiddenItem_HeaderID").value = data.Item_HeaderID;//new
    document.getElementById("hiddenItem_SupportGroupID").value = data.ID;
    document.getElementById("hiddenUserSupportGroupID").value = data.SupportGroupID
    //$("#dxItem_HeaderSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.EnglishName);
    $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value", data.Item_HeaderDTO.Model);
    $("#dxItem_HeaderBrandSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.BrandID);
    $("#dxItem_HeaderClassSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.ClassID);
    $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.SubClassID);
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value", data.Item_HeaderDTO.IsActive);
    //$("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value", data.Item_HeaderDTO.IsESD)
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value", data.SupportGroupID);
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("disabled", true);
    if (data.Item_HeaderDTO.ItemImg != null) {
        $("#ItemThumbnail").attr('src', data.Item_HeaderDTO.ItemImg);
    } else {
        $("#ItemThumbnail").attr('src', "/App/Common/Assets/img/no-product-image.png");
    }
    ClearErrorFeedback();
}
function GetItem_HeaderDTO() {
    let _item_HeaderDTO = {
        ID: $("#hiddenItem_HeaderID").val(),
        Model: $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value"),
        BrandID: $("#dxItem_HeaderBrandSelectBox").dxSelectBox("instance").option("value"),
        ClassID: $("#dxItem_HeaderClassSelectBox").dxSelectBox("instance").option("value"),
        SubClassID: $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value"),
        //IsESD: $("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value"),
        SupportGroupID: $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value"),
        Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(),
        UserDefinedIDArray: $("#dxUserDefinedList").dxList("instance").option("selectedItemKeys"),
        //IsItemCreated: $("#dxItem_HeaderIsItemCreatedCheckBox").dxCheckBox("instance").option("value"),
        FileDTO: GetFileDTO()
    }
    return _item_HeaderDTO;
}
//#region Item_Header Excel functions
function ExportItem_HeaderExcelFormat() {
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Model', key: 'model', width: 30 },
        { header: 'Brand', key: 'brand', width: 30 },
        { header: 'Support Group', key: 'supportgroup', width: 30 },
        { header: 'Sub Class', key: 'subclass', width: 30 },
    ];
    // Set the header style to bold
    worksheet.getRow(1).font = { bold: true };
    // Create the Excel file and download it
    workbook.xlsx.writeBuffer().then(function (buffer) {
        var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'InventoryFormat.xlsx';
        link.click();
    });
}
function ClearExcelItem_HeaderModalFields() {
    $('#successItem_HeaderMessage').hide();
    $('#errorItem_HeaderMessages').hide();
    var uploader = $("#dxItem_HeaderFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (uploader) {
        uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (uploader) {
        uploader.reset();
    }
}
function ShowItem_HeaderSuccessMessage(Message) {
    $('#successItem_HeaderMessage').text(Message).show();
    $('#successItem_HeaderMessage').removeAttr('hidden');
}

function ShowItem_HeaderErrorMessages(Message) {
    $('#errorItem_HeaderMessages').html(Message).show();
    $('#errorItem_HeaderMessages').removeAttr('hidden');
}

function ShowItem_HeaderValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    // Hide FileUploader to show messages
    $("#dxItem_HeaderFileUploader").dxFileUploader("instance").option("visible", false);
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "Items were created successfully.";
            ShowItem_HeaderSuccessMessage(successMessage);
            GetUserInformationbyID();
            ClearItem_HeaderFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "Items created: Some were skipped due to missing or invalid data.";
            ShowItem_HeaderSuccessMessage(successMessage);
            GetUserInformationbyID();
            ClearItem_HeaderFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row ${badLine.ID}:<br>Model: ${badLine.Model}, Brand: ${badLine.BrandName}, Support Group = ${badLine.SupportGroupName}, Sub Class = ${badLine.SubClassName}.</li>`;
            });
            errorMessages += '</ul>';
            ShowItem_HeaderErrorMessages(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowItem_HeaderErrorMessages(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelItem_HeaderModal').modal('hide');
        ClearExcelItem_HeaderModalFields();
        return HostResponse(_validationResultDTO);
    }
}
function Item_HeaderPropertyNameArray() {
    // With Object.keys we create an array of properties of the Item_HeaderDTO object
    var _propertyNameArray = Object.keys(GetItem_HeaderDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive" && PropertyName !== "ClassID" && PropertyName !== "Item_SupportGroupID" && PropertyName !== "UserDefinedIDArray" && PropertyName !== "FileDTO");
    return _propertyNameArray;
}
//#endregion
function Item_HeaderActionButtons(Action) {
    $("#Item_HeaderActionButtons").empty();
    document.getElementById('Item_HeaderModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("SelectItemTab").hidden = false;
        document.getElementById("SelectItem").hidden = false;
        document.getElementById("AddNewItemHeaderBtn").addEventListener("click", ClearItem_HeaderFields);
        document.getElementById('Item_HeaderModalTitle').innerText = 'Add Item Form';
        document.getElementById('CreateTabTitle').innerText = 'Create';
        document.getElementById("Item_HeaderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateItem_HeaderButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearItem_HeaderButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearItem_HeaderButton").addEventListener("click", ClearItem_HeaderFields);
        document.getElementById("CreateItem_HeaderButton").addEventListener("click", CreateItem_Header_Global);
    }
    else {
        // Update
        document.getElementById("SelectItemTab").hidden = true;
        document.getElementById("SelectItem").hidden = true;
        document.getElementById('Item_HeaderModalTitle').innerText = 'Update Item Form';
        document.getElementById('CreateTabTitle').innerText = 'Update';
        document.getElementById("Item_HeaderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateItem_HeaderButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearItem_HeaderButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearItem_HeaderButton").addEventListener("click", ClearItem_HeaderFields);
        document.getElementById("UpdateItem_HeaderButton").addEventListener("click", UpdateItem_Header_Global);
    }
}
function GetFileDTO() {
    if ((_fileDTO != null || _fileDTO != undefined) && (_fileDTOList != null || _fileDTOList != undefined)) {
        _fileDTO.FileList = _fileDTOList;
    }
    return _fileDTO;
}
async function ShowItem_SupportGroupDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this item from support group',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteItem_SuppportGroup_Global();
    } else {
        ClearItem_HeaderFields(true);
    }
}
async function CreateItem_Header_Global() {
    await dxLoadPanel.show();
    const _item_HeaderDTO = GetItem_HeaderDTO();
    const _validation_ResultDTO = await CreateItem_Header(_item_HeaderDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
        $("#hiddenItem_HeaderID").val(_validation_ResultDTO.Data);
        GetUserInformationbyID();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateItem_Header_Global() {
    await dxLoadPanel.show();
    const _item_HeaderDTO = GetItem_HeaderDTO();
    const _validation_ResultDTO = await UpdateItem_Header(_item_HeaderDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(false); // Item_Header fields still get cleared even when sent false as a parameter, also fixes multiple Expand buttons showing
        GetUserInformationbyID();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion

//#region CRUD Item Line
async function InitializeItem_LineModalControls() {
    const now = new Date();
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
        readOnly: true,
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
        valueExpr: "StatusID",
        displayExpr: "StatusName",
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
    $("#dxItem_LineAttachmentFileUploader").dxFileUploader({
        selectButtonText: "Select a file",
        labelText: "or drop it here",
        accept: "file",
        uploadedMessage: "Loading",
        multiple: true,
        uploadMode: "useButtons",
        invalidMaxFileSizeMessage: 'The file is too large. Allowed maximun size is 5MB',
        maxFileSize: 5000000,
        showFileList: true,
        width: "100%",
        onValueChanged: function (e) {
            var files = e.value;
            _fileDTOList = [];
            e.element.find(".dx-fileuploader-upload-button").hide();
            $.each(files, function (i, file) {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var _fileDTO = {
                        Name: $("#dxItem_LineAttachmentFileUploader").dxFileUploader("instance").option("value")[i].name,
                        Size: $("#dxItem_LineAttachmentFileUploader").dxFileUploader("instance").option("value")[i].size,
                        Data: e.target.result,
                    };
                    _fileDTOList.push(_fileDTO);
                }
            })
        },
    });
    Item_LineActionButtons("Save");
}
async function Item_LineActionButtons(Action) {
    $("#Item_LineActionButtons").empty();
    document.getElementById("Item_LineActionButtons").innerHTML =
        '<button class="btn btn-secondary float-end" id="ClearItem_LineButton" data-bs-dismiss="modal" type="button">Cancel</button>';
    if (Action == "Save") {
        document.getElementById("Item_LineActionButtons").innerHTML +=
            '<button class="btn btn-success me-1 m-b-15 float-end" id="CreateItem_LineButton" type="button">Save</button>';
        document.getElementById("CreateItem_LineButton").addEventListener("click", CreateItem_Line_Global);
    }
    else {
        // Update
        document.getElementById("Item_LineActionButtons").innerHTML +=
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateItem_LineButton" type="button">Update</button>';
        document.getElementById("UpdateItem_LineButton").addEventListener("click", UpdateItem_Line_Global);
    }
}
async function PopulateItem_LineFields(Data, UserDefinedList) {
    Item_LineActionButtons("Update");
    $("#hiddenItem_LineID").val(Data.ID);
    $("#hiddenStationID").val(Data.StationID);
    $("#hiddenItem_SupportGroupID").val(Data.Item_SupportGroupID);
    $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", Data.SupportGroupID);
    $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value", Data.StationID)
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", Data.Item_HeaderID);
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value", Data.OwnerID);
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value", Data.SupplyTypeID);
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value", Data.ManufactureSerialID);
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value", Data.ShipmentReceiptNumber);
    $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value", Data.StatusID);
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value", Data.ImportInvoice);
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", Data.BasePriceUSD);
    $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value", Data.Comments);
    $("#dxItem_LinePONumberTextBox").dxTextBox("instance").option("value", Data.PONumber);
    $("#dxItem_LinePOLineTextBox").dxTextBox("instance").option("value", Data.POLine);
    $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value", Data.LegacyID);
    $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value", Data.Serial);
    for (var _userDefined in UserDefinedList) {
        switch (UserDefinedList[_userDefined].UserDefinedDTO.DataTypeID) {
            case DataType_Enum.Text:
                $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedName]);
                break;
            case DataType_Enum.DateTime:
                $("#dxUserDefined" + _userDefined).dxDateBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedName]);
                break;
            case DataType_Enum.Check:
                let _userDefinedValue = Data[UserDefinedList[_userDefined].UserDefinedName] == 'true' ? true : false;
                $("#dxUserDefined" + _userDefined).dxCheckBox("instance").option("value", _userDefinedValue);
                break;
            case DataType_Enum.Numeric:
                $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedName]);
                break;
        }
    }

}
async function PopulateNewItemLineField(Data) {
    await $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", Data.Item_HeaderID);
    await $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", Data.SupportGroupID);

}
function ClearItem_LineFields(UserDefinedTemplateList) {
    Item_LineActionButtons("Save");
    $("#hiddenItem_LineID").val("");
    $("#hiddenStationID").val("");
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").reset();
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").reset();
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
    $("#dxItem_LineAttachmentFileUploader").dxFileUploader("instance").reset();
    $("#dxItem_LineGenerateSerialCheckBox").dxCheckBox("instance").option("value", false);;
    _fileDTOList = [];
    _fileDTO = {};

    for (var _userDefined in UserDefinedTemplateList) {
        switch (UserDefinedTemplateList[_userDefined].UserDefinedDTO.DataTypeID) {
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
    //ClearErrorFeedback();
}
function GetItem_LineDTO(UserDefinedTemplateList) {
    let _item_LineDTO = {
        ID: $("#hiddenItem_LineID").val(),
        Item_HeaderID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value"),
        Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(),
        Item_SupportGroupDTO: {
            SupportGroupID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value")
        },
        OwnerID: $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value"),
        SupplyTypeID: $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value"),
        Serial: $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value"),
        ManufactureSerialID: $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value"),
        ShipmentReceiptNumber: $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value"),
        StatusID: $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value"),
        StationID: $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value"),
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
        FileDTO: GetFileDTO()
    }
    for (var _userDefined in UserDefinedTemplateList) {
        let _userDefinedValueDTO = {
            UserDefinedID: UserDefinedTemplateList[_userDefined].UserDefinedID,
            SupportGroupID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value"),
            IsActive: true,
            Value: ""
        }
        switch (UserDefinedTemplateList[_userDefined].UserDefinedDTO.DataTypeID) {
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
        const _userDefinedTemplateDTO = { Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(), IsActive: true, GetUserDefinedDTO: true };
        const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
        ClearItem_LineFields(_userDefinedTemplateList);
    }
}
async function CreateItem_Line_Global() {
    await dxLoadPanel.show();
    const _userDefinedTemplateDTO = { Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(), IsActive: true, GetUserDefinedDTO: true };
    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    const _item_LineDTO = GetItem_LineDTO(_userDefinedTemplateList);
    const _validation_ResultDTO = await CreateItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_LineFields(_userDefinedTemplateList);
        ClearItem_HeaderFields(true);
        $("#AddNewItemLineModal").modal("hide");
        GetItem_LineTreeViewList();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateItem_Line_Global() {
    await dxLoadPanel.show();
    const _userDefinedTemplateDTO = { Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(), IsActive: true, GetUserDefinedDTO: true };
    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    const _item_LineDTO = GetItem_LineDTO(_userDefinedTemplateList);
    const _validation_ResultDTO = await UpdateItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_LineFields(_userDefinedTemplateList);
        ClearItem_HeaderFields(true);
        $("#AddNewItemLineModal").modal("hide");
        GetItem_LineTreeViewList();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteItem_Line_Global() {
    await dxLoadPanel.show();
    const _userDefinedTemplateDTO = { Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(), IsActive: true, GetUserDefinedDTO: true };
    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    const _item_LineDTO = GetItem_LineDTO();
    const _validation_ResultDTO = await DeleteItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_LineFields(_userDefinedTemplateList);
        GetUserInformationbyID();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion
//#region CRUD User Defined
async function InitializeUserDefinedControls() {
    //User defined fields
    $("#dxUserDefinedIsActiveCheckBox").dxCheckBox({
        value: true,
        text: "Active?",
    });
    $("#dxUserDefinedIsMandatoryCheckBox").dxCheckBox({
        value: true,
        text: "Required?",
    });
    $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        readOnly: true,
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxUserDefinedDataTypeSelectBox").dxSelectBox({
        dataSource: await GetDXDataTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxUserDefinedNameTextBox").dxTextBox({
        placeholder: "Type name.."
    });
    $("#dxUserDefinedDataGrid").dxDataGrid({
        dataSource: await GetDXUserDefinedDataSource(),
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
        focusedRowEnabled: false,
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
            fileName: "UserDefined",
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
            let _userDefinedData = data.selectedRowsData[0];
            if (_userDefinedData != null) {
                UserDefinedActionButtons("Update");
                PopulateUserDefinedFields(_userDefinedData);
            }
        },
        columns:
            [
                {
                    caption: "Actions",
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
                                $("#hiddenUserDefinedID").val(options.data.ID);
                                ShowUserDefinedDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Is Active?",
                    dataField: "IsActive"
                },
                {
                    caption: "Support Group",
                    dataField: "SupportGroupName"
                },
                {
                    caption: "Name",
                    dataField: "Name"
                },
                {
                    caption: "Data Type",
                    dataField: "DataTypeName"
                },
                {
                    caption: "Required?",
                    dataField: "IsMandatory"
                },
                {
                    caption: "Added By ID",
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
                    caption: "Last Update By ID",
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
    $("#dxUserDefinedList").dxList({
        displayExpr: "NameWithDataType",
        valueExpr: "ID",
        popupWidth: 400,
        height: 185,
        showSelectionControls: true,
        selectionMode: "all",
    });
    UserDefinedActionButtons("Save");

}
function UserDefinedActionButtons(Action) {
    $("#UserDefinedActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("UserDefinedActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateUserDefinedButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateUserDefinedButton").addEventListener("click", CreateUserDefined_Global);
    }
    else {
        // Update
        document.getElementById("UserDefinedActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearUserDefinedButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateUserDefinedButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearUserDefinedButton").addEventListener("click", ClearUserDefinedFields);
        document.getElementById("UpdateUserDefinedButton").addEventListener("click", UpdateUserDefined_Global);
    }
}
function ClearUserDefinedFields() {
    UserDefinedActionButtons("Save");
    $("#hiddenUserDefinedID").val("");
    $("#dxUserDefinedIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxUserDefinedIsMandatoryCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").reset();
    let _supportGroupSelectBox = $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance");
    if (!_supportGroupSelectBox.option("readOnly")) {
        $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").reset();
    }
    $("#dxUserDefinedDataTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxUserDefinedNameTextBox").dxTextBox("instance").option("value", "");

    let keys = $("#dxUserDefinedDataGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxUserDefinedDataGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxUserDefinedDataGrid").dxDataGrid("instance").refresh();
}
async function PopulateUserDefinedFields(data) {
    $("#hiddenUserDefinedID").val(data.ID);
    $("#dxUserDefinedIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxUserDefinedIsMandatoryCheckBox").dxCheckBox("instance").option("value", data.IsMandatory);
    $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("value", data.SupportGroupID);
    $("#dxUserDefinedDataTypeSelectBox").dxSelectBox("instance").option("value", data.DataTypeID);
    $("#dxUserDefinedNameTextBox").dxTextBox("instance").option("value", data.Name);
}
function GetUserDefinedDTO() {
    let _userDefinedDTO = {
        ID: $("#hiddenUserDefinedID").val(),
        Name: $("#dxUserDefinedNameTextBox").dxTextBox("instance").option("value"),
        IsMandatory: $("#dxUserDefinedIsMandatoryCheckBox").dxCheckBox("instance").option("value"),
        IsActive: $("#dxUserDefinedIsActiveCheckBox").dxCheckBox("instance").option("value"),
        DataTypeID: $("#dxUserDefinedDataTypeSelectBox").dxSelectBox("instance").option("value"),
        SupportGroupID: $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("value"),
    }
    return _userDefinedDTO;
}
async function ShowUserDefinedDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this user defined',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteUserDefined_Global();
    } else {
        ClearUserDefinedFields();
    }
}
async function CreateUserDefined_Global() {
    await dxLoadPanel.show();
    const _userDefinedDTO = GetUserDefinedDTO();
    const _validation_ResultDTO = await CreateUserDefined(_userDefinedDTO);
    if (_validation_ResultDTO.Result) {
        ClearUserDefinedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateUserDefined_Global() {
    await dxLoadPanel.show();
    const _userDefinedDTO = GetUserDefinedDTO();
    const _validation_ResultDTO = await UpdateUserDefined(_userDefinedDTO);
    if (_validation_ResultDTO.Result) {
        ClearUserDefinedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteUserDefined_Global() {
    await dxLoadPanel.show();
    const _userDefinedDTO = GetUserDefinedDTO();
    const _validation_ResultDTO = await DeleteUserDefined(_userDefinedDTO);
    if (_validation_ResultDTO.Result) {
        ClearUserDefinedFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function GetUserDefinedList(SupportGroupID) {
    //await dxLoadPanel.show();
    const _userDefinedDTO = {
        SupportGroupID: SupportGroupID,
        IsActive: true
    };
    const _userDefinedList = await GetDXUserDefinedDataSource(_userDefinedDTO);
    $("#dxUserDefinedList").dxList("instance").option("dataSource", _userDefinedList);
    //dxLoadPanel.hide();
}
//#endregion

//#region User Defined Template
function GetUserDefinedTemplateDTO() {
    let _userDefinedTemplateDTO = {
        UserDefinedIDArray: $("#dxUserDefinedList").dxList("instance").option("selectedItemKeys"),
        Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(),
        Item_SupportGroupDTO:
        {
            SupportGroupID: $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value")
        },
        IsActive: true
    }
    return _userDefinedTemplateDTO;
}
async function UpdateUserDefinedTemplate_Global() {
    await dxLoadPanel.show()
    let _userDefinedTemplateDTO = GetUserDefinedTemplateDTO();
    const _validation_ResultDTO = await UpdateUserDefinedTemplate(_userDefinedTemplateDTO);
    if (_validation_ResultDTO.Result) {
        GetUserDefinedTemplateList();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function GetUserDefinedTemplateList() {
    //await dxLoadPanel.show();
    const _userDefinedTemplateDTO = { Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(), IsActive: true, GetUserDefinedDTO: true };
    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    await SelectUserDefinedFieldsFromTemplate(_userDefinedTemplateList);
    //dxLoadPanel.hide();
}
async function SelectUserDefinedFieldsFromTemplate(UserDefinedTemplateList) {
    var _list = $("#dxUserDefinedList").dxList("instance");
    _list.option("selectedItemKeys", []);
    for (let i in UserDefinedTemplateList) {
        var _selectedFields = _list.option("selectedItemKeys");
        _selectedFields.push(UserDefinedTemplateList[i].UserDefinedID);
        _list.option("selectedItemKeys", _selectedFields);
    }
}
async function BuildUserDefinedOnItem_LineModal(UserDefinedTemplateList) {
    let _userDefinedSection = document.getElementById("UserDefinedTemplateSection");
    _userDefinedSection.innerHTML = ""
    for (let i in UserDefinedTemplateList) {
        _userDefinedSection.innerHTML += "<label class=\"form-label col-form-label col-md-12\">" + UserDefinedTemplateList[i].UserDefinedName + "</label>";
        _userDefinedSection.innerHTML += '<div class="col-md-12"><div id="dxUserDefined' + i + '"></div></div>';
    }
    for (let i in UserDefinedTemplateList) {
        switch (UserDefinedTemplateList[i].UserDefinedDTO.DataTypeID) {
            case DataType_Enum.Text:
                $("#dxUserDefined" + i).dxTextBox({});
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
//#endregion
//#region Behavior for support group and user
function DisabledAdministrationFields(UserDTO) {
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin) || UserDTO.RoleIDArray.includes(Role_Enum.Administrator) || UserDTO.RoleIDArray.includes(Role_Enum.Warehouse_Receiver)) {
            $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("readOnly", false);
            $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("readOnly", false);
        }
    }
}
async function FilterDataSourceBySupportGroups(UserDTO) {
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.SupportGroupIDArray != null && !UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            if (UserDTO.SupportGroupIDArray.length == 1) {
                document.getElementById("hiddenUserSupportGroupID").value = UserDTO.SupportGroupIDArray[0]
                $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("value", UserDTO.SupportGroupIDArray[0]);
                $("#dxItemAdministrationDatGrid").dxDataGrid("instance").option('dataSource', await GetDXItem_SupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetItem_HeaderDTO: true }));
            } else {
                $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
            }
            $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
            $("#dxItemAdministrationDatGrid").dxDataGrid("instance").option('dataSource', await GetDXItem_SupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetItem_HeaderDTO: true, Item_HeaderDTO: { GetItemHeaderPicture: true } }));
        }
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            $("#dxItemAdministrationDatGrid").dxDataGrid("instance").option('dataSource', await GetDXItem_SupportGroupDataSource({ GetItem_HeaderDTO: true, Item_HeaderDTO: { GetItemHeaderPicture: true } }));
            $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource());
        }
    }
}
//#endregion

//#region Files region

//#region Header Files 
async function GetItem_HeaderFileList() {
    $("#Item_HeaderFilelistSection").empty();
    let _item_HeaderFileDTO = { ID: $("#hiddenItem_HeaderID").val() }
    let _item_HeaderFileList = await GetItem_HeaderFilesInformation(_item_HeaderFileDTO);
    if ($(_item_HeaderFileList).length > 0) {
        $.each(_item_HeaderFileList, function (i, attachment) {
            //Exclusion list for unwanted files to display
            if (attachment.Extension.toLowerCase() != '.db') {
                //Prepare preview for Images START
                if (attachment.Extension.toLowerCase() == '.jpg' || attachment.Extension.toLowerCase() == '.png' || attachment.Extension.toLowerCase() == '.bmp') {
                    $('#Item_HeaderFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4  mb-2" style="height:120px;">' +
                        '<div class="card" style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.URL + '" style="height:90px;"  alt="Responsive image">' +
                        '<label class="text-center text-truncate text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a href="#" id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger file btn-xs float-right"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn  btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                    //Prepare preview for Images END  
                } else {
                    $('#Item_HeaderFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4  mb-2" style="height:120px;">' +
                        '<div class="card" style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.Icon + '" style="height:90px;" alt="Responsive image">' +
                        '<label class="text-center text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a  id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger btn-xs float-right file"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn btn-inverse btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                }
            }
        });
        let _files = document.querySelectorAll(".file");
        _files.forEach(function (_file) {
            _file.addEventListener('click', function () {
                ShowDeleteAttachmentHeaderQuestion({ ID: $("#hiddenItem_HeaderID").val(), Name: this.dataset.filename, Extension: this.dataset.extension });
            });
        });
    }
}
async function ShowDeleteAttachmentHeaderQuestion(FileDTO) {
    const _alert = await Swal.fire({
        title: 'Are you sure?',
        text: 'This file will be deleted',
        icon: 'warning',
        confirmButtonText: `Delete`,
        showCancelButton: true,
        reverseButtons: true,
        confirmButtonColor: '#ea4335',
    });
    if (_alert.isConfirmed) {
        DeleteAttachmentHeader(FileDTO);
    }
}
async function DeleteAttachmentHeader(FileDTO) {
    await dxLoadPanel.show();
    const _validation_resultDTO = await DeleteItem_HeaderFile(FileDTO);
    HostResponse(_validation_resultDTO);
    GetItem_HeaderFileList(FileDTO);
    dxLoadPanel.hide()
}
//#endregion 

//#region Line Files 
async function GetItem_LineFileList() {
    $("#Item_LineFilelistSection").empty();

    let _item_LineFileDTO = { ID: $("#hiddenItem_LineID").val() }
    let _item_LineFileList = await GetItem_LineFilesInformation(_item_LineFileDTO);
    if ($(_item_LineFileList).length > 0) {

        $.each(_item_LineFileList, function (i, attachment) {
            //Exclusion list for unwanted files to display
            if (attachment.Extension.toLowerCase() != '.db') {
                //Prepare preview for Images START
                if (attachment.Extension.toLowerCase() == '.jpg' || attachment.Extension.toLowerCase() == '.png' || attachment.Extension.toLowerCase() == '.bmp') {
                    $('#Item_LineFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4 mb-2" style="height:120px;">' +
                        '<div class="card "  style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.URL + '" style="height:90px;"  alt="Responsive image">' +
                        '<label class="text-center text-truncate text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a href="#" id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger file btn-xs float-right"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn  btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                    //Prepare preview for Images END  
                } else {
                    $('#Item_LineFilelistSection').append(
                        '<div class="col-sm-6 col-md-4 col-xs-4 col-lg-4 mb-2" style="height:120px;">' +
                        '<div class="card"  style="background:#265755 ;">' +
                        '<img class="mx-auto d-block img-fluid" src="' + attachment.Icon + '" style="height:90px;" alt="Responsive image">' +
                        '<label class="text-center text-white" style="font-size: 14px;">' + attachment.Name + '</label>' +
                        '<div class="card-img-overlay">' +
                        '<a  id="AttachmentID' + i + '" data-FileName="' + attachment.Name + '" data-Extension="' + attachment.Extension + '" class="btn btn-danger btn-xs float-right file"><i class="fas fa-times"></i></a>' +
                        '<a href="' + attachment.URL + '" target="_blank" class="btn btn-inverse btn-xs m-r-5 float-right" download="' + attachment.Name + '"><i class="fas fa-download"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                }
            }
        });
        let _files = document.querySelectorAll(".file");
        _files.forEach(function (_file) {
            _file.addEventListener('click', function () {
                ShowDeleteAttachmentLineQuestion({ ID: $("#hiddenItem_LineID").val(), Name: this.dataset.filename, Extension: this.dataset.extension });
            });
        });
    }
}
async function ShowDeleteAttachmentLineQuestion(FileDTO) {
    const _alert = await Swal.fire({
        title: 'Are you sure?',
        text: 'This file will be deleted',
        icon: 'warning',
        confirmButtonText: `Delete`,
        showCancelButton: true,
        reverseButtons: true,
        confirmButtonColor: '#ea4335',
    });
    if (_alert.isConfirmed) {
        DeleteAttachmentLine(FileDTO);
    }
}
async function DeleteAttachmentLine(FileDTO) {
    await dxLoadPanel.show();
    const _validation_resultDTO = await DeleteItem_LineFile(FileDTO);
    HostResponse(_validation_resultDTO);
    GetItem_LineFileList(FileDTO);
    GetItem_LineTreeViewList();
    dxLoadPanel.hide()
}
async function GetItem_LineTreeViewList() {
    $("#dxTreeViewList").dxTreeView("instance").option("dataSource", await GetItem_LineFilesTreeView({ Item_HeaderID: $("#hiddenItem_HeaderID").val() }));
    $("#dxTreeViewList").dxTreeView("expandAll");
}
function TreeViewExpand() {
    $("#dxTreeViewList").dxTreeView("expandAll");
}
function TreeViewCollapse() {
    $("#dxTreeViewList").dxTreeView("collapseAll");
}

//#endregion

//#endregion

//#region Item Support Group Region

async function InitializeReassignSupportGroupModalControls() {
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        readOnly: true,
        searchEnabled: true,
        searchExpr: ["Name"],
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
    $("#hiddenItem_LineID").val(data.ID);
    $("#hiddenItem_SupportGroupID").val(data.Item_SupportGroupID);
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderID);
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value", data.SupportGroupID);
}

function ClearReassignSupportGroupModalFields() {
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value", '');
}
function GetItemLine_SupportGroupModalDTO() {
    //This build a item line with Item_supportGroup Relation
    let _item_LineDTO = {
        ID: $("#hiddenItem_LineID").val(),
        Item_HeaderID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value"),
        Item_SupportGroupID: $("#hiddenItem_SupportGroupID").val(),
        Item_SupportGroupDTO: {
            SupportGroupID: $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value"),
        },
        IsActive: true
    }
    return _item_LineDTO;
}
async function ReassignSupportGroupGlobal() {
    await dxLoadPanel.show();
    const _item_LineDTO = GetItemLine_SupportGroupModalDTO();
    const _validation_ResultDTO = await ReassignSupportGroup(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(false); // Item_Header fields still get cleared even when sent false as a parameter, also fixes multiple Expand buttons showing
        GetUserInformationbyID();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function InitializeItem_SupportGroupControls() {
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox({
        dataSource: [], //await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        readOnly: false,
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["Name"],
        searchMode: 'contains',
        onSelectionChanged: async function (data) {
            if (data.selectedItem != null) {
                let _itemSupportGroupID = data.selectedItem.ID;
                if (_itemSupportGroupID != null) {
                    $("#hiddenUserSupportGroupID").val(_itemSupportGroupID);
                    GetUserDefinedList(_itemSupportGroupID);
                }
            }
        },
    });
}
function GetItem_SupportGroupDTO() {
    let _item_SupportGroupDTO = {
        ID: $("#hiddenItem_SupportGroupID").val(),
        Item_HeaderID: $("#hiddenItem_HeaderID").val(),
        SupportGroupID: $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value"),
        IsActive: true
    }
    return _item_SupportGroupDTO;
}

async function CreateItem_SupportGroup_Global() {
    await dxLoadPanel.show();
    const _item_SupportGroupDTO = GetItem_SupportGroupDTO();
    const _validation_ResultDTO = await CreateItem_SupportGroup(_item_SupportGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
        GetUserInformationbyID();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteItem_SuppportGroup_Global() {
    await dxLoadPanel.show();
    const _item_SupportGroupDTO = GetItem_SupportGroupDTO();
    const _validation_ResultDTO = await DeleteItem_SupportGroup(_item_SupportGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_HeaderFields(true);
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion

//#region Wizard Behaviour
function ShowPreviousStep() {
    let _wizard = document.querySelector(`.wizard`);
    var activeTab = _wizard.querySelector('.workflow-wizard li a.active').parentNode;
    var previousTab = activeTab.previousElementSibling;
    if (previousTab !== null) {
        previousTab = previousTab.querySelector('a');
        activeTab.querySelector('a').classList.remove('active');
        previousTab.classList.add('active');
        var content = document.querySelector(previousTab.getAttribute('href'));
        hideContent();
        content.classList.remove('d-none');
        content.classList.add('active', 'show');
    }
    if (previousTab == null) {
        dxLoadPanel.show();
        CloseWizard();
        dxLoadPanel.hide();
    }
}
async function ShowNextStep() {
    let _wizard = document.querySelector(`.wizard`);
    let activeTab = _wizard.querySelector('.workflow-wizard li a.active').parentNode;
    let nextTab = activeTab.nextElementSibling;
    if (nextTab !== null) {
        TriggerWizardFunctionByState();
        nextTab = nextTab.querySelector('a');
        activeTab.querySelector('a').classList.remove('active');
        nextTab.classList.add('active');
        let content = document.querySelector(nextTab.getAttribute('href'));
        hideContent();
        content.classList.remove('d-none');
        content.classList.add('active', 'show');
    }
    if (nextTab == null) {
        await dxLoadPanel.show();
        await TriggerWizardFunctionByState();
        dxLoadPanel.hide();
    }
}
function hideContent() {
    let contents = document.querySelectorAll(`.wizard .wizard.tab-content .wizard-step.tab-pane`);
    for (var i = 0; i < contents.length; i++) {
        contents[i].classList.add('d-none');
        contents[i].classList.remove('active', 'show');
    }
}
function ResetWizardFlow() {
    let _firstTabContent = document.querySelector(`.wizard .wizard.tab-content .wizard-step.tab-pane:first-child`);
    let _firstTab = document.querySelector('.workflow-wizard li:first-child');
    _firstTab = _firstTab.querySelector('a');
    _firstTabContent.classList.remove('d-none');
    _firstTabContent.classList.add('active', "show");
    _firstTab.classList.add("active");
}
function GetCurrentStep() {
    let _step = document.querySelector(`.workflow-wizard .nav-item.item .active .nav-no`);
    if (_step.innerText == '1') {
        document.getElementById('btnAddNewItemBack').classList.add('invisible');
        document.getElementById('btnAddNewItemNext').innerText = "Next";
    } else {
        document.getElementById('btnAddNewItemBack').classList.remove('invisible');
        document.getElementById('btnAddNewItemNext').innerText = "Save";
    }

}

function TriggerWizardFunctionByState() {
    let _step = document.querySelector(`.workflow-wizard .nav-item.item .active .nav-no`);
    let _actionButton = document.getElementById('btnAddNewItemNext');


    if (_step.innerText == '1' && _actionButton.innerText == 'Save') {
        CloseWizard();
        dxLoadPanel.hide();
    } else {
        if (_step.innerText == '1' && _actionButton.innerText == 'Next') {
            CreateItem_Header_Global();
        } else if (_step.innerText == '2') {
            // If an existing item is selected, call function to create relation between item and support group
            CreateItem_SupportGroup_Global();
        }
    }

}

function SetWizardStyle(isEditMode) {
    // Update modal title based on the mode
    document.getElementById("exampleModalLabel").innerText = isEditMode === "edit" ? "Edit Item" : "Add Item";

    // Toggle 'd-none' class for wizard-card
    document.getElementById("wizard-card").classList.toggle("d-none", isEditMode === "edit");

    // Toggle 'active' class for select-item content
    document.getElementById("select-item-content").classList.toggle('active', isEditMode === "add");

    // Toggle 'active' class for select-item tab
    document.getElementById("select-item-tab").classList.toggle('active', isEditMode === "add");

    // Toggle 'active' class for new-item content
    document.getElementById("new-item-content").classList.toggle('active', isEditMode === "edit");

    // Toggle 'active' class for new-item tab
    document.getElementById("new-item-tab").classList.toggle('active', isEditMode === "edit");
    // Toggle 'show' class for select-item content
    document.getElementById("select-item-content").classList.toggle('show', isEditMode === "add");
    // Toggle 'show' class for new-item content
    document.getElementById("new-item-content").classList.toggle('show', isEditMode === "edit");
    document.getElementById("wizard-panel").classList.toggle('panel', isEditMode === "add");
    document.getElementById("wizard-panel").classList.toggle('p-3', isEditMode === "add");
    document.getElementById("modal-nav-tabs").classList.toggle('d-none', isEditMode === "edit");
    document.getElementById("btnAddNewItemNext").innerText = isEditMode === "edit" ? "Update" : "Next";
    document.getElementById("btnAddNewItemBack").classList.toggle('invisible', isEditMode === "edit")

}

function CloseWizard() {
    let _wizard = document.querySelector(`.wizard`);
    let activeTab = _wizard.querySelector('.workflow-wizard li a.active').parentNode;
    activeTab.querySelector('a').classList.remove('active');
    $(`.wizard`).modal("hide");
    hideContent();
    ResetWizardFlow();
}

//#endregion