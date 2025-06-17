
//#region Import service and resources
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXItem_HeaderDataSource, CreateItem_Header, UpdateItem_Header, DeleteItem_Header, GetItem_HeaderFilesInformation, DeleteItem_HeaderFile, SaveItem_HeaderMultipleFile } from './Item_Header/Item_Header_Service.js'
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
    //document.getElementById('AssignUserDefinedBtn').addEventListener("click", GetUserDefinedList);
    //document.getElementById("UpdateUserDefinedTemplateButton").addEventListener("click", UpdateUserDefinedTemplate_Global);
    //document.getElementById("WizardCloseButton").addEventListener("click", CloseWizard);
    //document.getElementById('AddNewItemLineModal').addEventListener('hidden.bs.modal', async function (event) {
    //    const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };//new
    //    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    //    ClearItem_LineFields(_userDefinedTemplateList);
    //});
    document.getElementById('AddNewItemHeaderModal').addEventListener('hidden.bs.modal', async function (event) {
        ClearItem_HeaderFields(true);
    });
    document.getElementById("AddNewItemHeaderBtn").addEventListener("click", function () {
        ClearItem_HeaderFields();
        //SetWizardStyle("add");
        //GetCurrentStep();
    }); // Clears all fields before opening the New Item modal in case ID isn't cleared


    // Assuming your DevExpress button has a specific class like "dx-my-button"
    let devExpressButton = document.querySelectorAll(".dx-fileuploader-button");

    devExpressButton.forEach(function (currentButton) {
        // Remove DevExpress button classes
        currentButton.classList.remove("dx-button", "dx-widget", "dx-button-normal", "dx-button-mode-contained", "dx-widget", "dx-button-has-text");

        // Add Bootstrap button classes
        currentButton.classList.add("btn", "btn-default");
    });

    //document.getElementById('btnAddNewItemBack').addEventListener("click", function () {
    //    ShowPreviousStep()
    //    GetCurrentStep();
    //});
    //document.getElementById('btnAddNewItemNext').addEventListener("click", function (e) {
    //    //e.preventDefault()
    //    //if (event.target.innerText == 'Next' || event.target.innerText == 'Save') {
    //    //    let _wizard = document.querySelector('#AddNewItemHeaderModal');
    //    //    let activeTab = _wizard.querySelector('.workflow-wizard li a.active').parentNode;
    //    //    let nextTab = activeTab.nextElementSibling;
    //    //    ShowNextStep();
    //    //    GetCurrentStep();
    //    //}
    //    //if (event.target.innerText == "Update") {
    //    //    UpdateItem_Header_Global();
    //    //    $("#AddNewItemHeaderModal").modal("hide");
    //    //}


    //});

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
            fileName: "ItemAdministration",
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
        //onCellClick: async function (e) {
        //    // Assuming you have access to the rowKey
        //    var rowKey = e.key;
        //    // Pass the event to the function
        //    //toggleMasterRow(rowKey, e.event);
        //    event.preventDefault()
        //    if (e.row != null) {
        //        $("#hiddenItem_HeaderID").val(e.row.data.Item_HeaderDTO.ID);
        //        $("#hiddenItem_SupportGroupID").val(e.row.data.ID);
        //        $("#hiddenUserSupportGroupID").val(e.row.data.SupportGroupDTO.ID)
        //        //document.getElementById("AssignUserDefinedBtn").classList.remove("d-none");
        //        console.log(e.row.data);
        //        //await GetUserDefinedTemplateList();
        //    }
        //},
        onRowClick: function (e) {
            var rowKey = e.key;
            // Pass the event to the function
            //toggleMasterRow(rowKey, e.event);
        },
        onSelectionChanged: async function (data) {
            let _itemAdministrationData = data.selectedRowsData[0];
            if (_itemAdministrationData != null) {
                $("#hiddenItem_HeaderID").val(_itemAdministrationData.Item_HeaderDTO.ID);
                $("#hiddenItem_SupportGroupID").val(_itemAdministrationData.ID);
                $("#hiddenUserSupportGroupID").val(_itemAdministrationData.SupportGroupDTO.ID)
                Item_HeaderActionButtons("Update");
                await PopulateItem_HeaderFields(_itemAdministrationData);
                GetItem_HeaderFileList();
                await GetUserDefinedTemplateList();
                //GetItem_LineTreeViewList();
            }
        },
        columns:
            [
                //{
                //    caption: "Actions",
                //    alignment: "center",
                //    allowFiltering: false,
                //    allowSorting: false,
                //    width: 'auto',
                //    cellTemplate: function (container, options) {
                //        container.height(30);
                //        $('<button type="button" data-bs-toggle="modal" data-bs-target="#AddNewItemHeaderModal" class="btn btn-success" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenItem_HeaderID").val(options.data.ID);
                //                SetWizardStyle("edit");
                //            }).appendTo(container);
                //        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                //            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                //            + '</span></button>')
                //            .height(30)
                //            .on('dxclick', function () {
                //                $("#hiddenItem_HeaderID").val(options.data.ID);
                //                $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupDTO.ID)
                //                ShowItem_SupportGroupDeleteQuestion();
                //            }).appendTo(container);
                //    },
                //},
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
                                    $("#hiddenItem_HeaderID").val(options.data.Item_HeaderDTO.ID);
                                    $("#hiddenItem_SupportGroupID").val(options.data.ID);
                                    $('#AddNewItemHeaderModal').modal('show');
                                    //SetWizardStyle("edit");
                                }
                                else if (e.itemData.value == 2) {
                                    $('#AttachmentsModal').modal('show');
                                    document.getElementById("AttachmentsModalSaveBtn").addEventListener("click", SaveAttachmentsItem_Header);
                                }
                                else if (e.itemData.value == 3) {
                                    window.open("/App/Features/Maintenance/AMS/ItemManagement/ItemLineCatalog.aspx?Item_SupportGroupID=" + options.data.ID + "&Item_HeaderID=" + options.data.Item_HeaderDTO.ID, "_blank");
                                }
                                else if (e.itemData.value == 4) {
                                    $("#hiddenItem_SupportGroupID").val(options.data.ID);
                                    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupDTO.ID)
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
                    dataField: "SupportGroupDTO.EnglishName",
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
        //masterDetail: {
        //    enabled: true,
        //    template: MasterDetailItem_Line
        //}
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
    document.getElementById("btnCloseItem_HeaderModal").addEventListener("click", ClearItem_HeaderFields);
    Item_HeaderActionButtons("Save");
}
//async function MasterDetailItem_Line(container, masterDetailOptions) {
//    await dxLoadPanel.show()
//    const _item_LineDTO = { Item_SupportGroupDTO: { ID: masterDetailOptions.data.ID }, IsActive: true };
//    const _item_LineList = await GetItem_LineMasterDetailInformation(_item_LineDTO);
//    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", masterDetailOptions.data.Item_HeaderDTO.ID);
//    $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", masterDetailOptions.data.SupportGroupDTO.ID)
//    $("#hiddenItem_HeaderID").val(masterDetailOptions.data.Item_HeaderDTO.ID);
//    $("#hiddenItem_SupportGroupID").val(masterDetailOptions.data.ID);
//    await BuildItem_LineGrid(container, _item_LineList, masterDetailOptions);
//    dxLoadPanel.hide();
//}
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
    document.getElementById("hiddenItem_HeaderID").value = data.Item_HeaderDTO.ID;//new
    document.getElementById("hiddenItem_SupportGroupID").value = data.ID;
    document.getElementById("hiddenUserSupportGroupID").value = data.SupportGroupDTO.ID
    //$("#dxItem_HeaderSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.EnglishName);
    $("#dxItem_HeaderModelTextBox").dxTextBox("instance").option("value", data.Item_HeaderDTO.Model);
    $("#dxItem_HeaderBrandSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.BrandID);
    $("#dxItem_HeaderClassSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.ClassID);
    $("#dxItem_HeaderSubClassSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTO.SubClassID);
    $("#dxItem_HeaderIsActiveCheckBox").dxCheckBox("instance").option("value", data.Item_HeaderDTO.IsActive);
    //$("#dxItem_HeaderIsESDCheckBox").dxCheckBox("instance").option("value", data.Item_HeaderDTO.IsESD)
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value", data.SupportGroupDTO.ID);
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
//async function BuildItem_LineGrid(container, ItemLineList, masterDetailOptions) {
//    $(`<div id="dxItem_HeaderLineList${masterDetailOptions.data.ID}">`).dxDataGrid({
//        dataSource: ItemLineList,
//        remoteOperations: true,
//        paging: {
//            pageSize: 5,
//        },
//        pager: {
//            showPageSizeSelector: true,
//            allowedPageSizes: [10, 50, 100],
//            showInfo: true
//        },
//        allowColumnReordering: true,
//        showRowLines: true,
//        showColumnLines: false,
//        showBorders: true,
//        focusedRowEnabled: false,
//        hoverStateEnabled: true,
//        rowAlternationEnabled: false,
//        columnAutoWidth: true,
//        allowColumnResizing: true,
//        columnResizingMode: 'widget',
//        columnMinWidth: 50,
//        groupPanel: {
//            visible: false
//        },
//        columnChooser: {
//            enabled: true
//        },
//        columnFixing: {
//            enabled: true
//        },
//        "export": {
//            enabled: true,
//            fileName: "ItemAdministration",
//            allowExportSelectedData: true
//        },
//        filterRow: {
//            visible: true,
//            applyFilter: "auto"
//        },
//        searchPanel: {
//            visible: false,
//            placeholder: "Search...",
//            width: 300
//        },
//        onSelectionChanged: async function (data) {
//            let _item_LineData = data.selectedRowsData[0];
//            if (_item_LineData != null) {
//                Item_LineActionButtons("Update");
//                const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
//                const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
//                PopulateItem_LineFields(_item_LineData, _userDefinedTemplateList);
//                GetItem_LineFileList();
//            }
//        },
//        sorting: {
//            mode: "multiple"
//        },
//        selection: {
//            mode: 'single'
//        },
//        headerFilter: {
//            visible: true
//        },
//        columns:
//            [
//                {
//                    caption: "ID",
//                    dataField: "ID",
//                    width: 'auto',
//                    visible: false
//                },
//                {
//                    caption: "Actions",
//                    visibleIndex: 1,
//                    alignment: "center",
//                    allowFiltering: false,
//                    allowSorting: false,
//                    width: 'auto',
//                    cellTemplate: function (container, options) {
//                        container.height(30);
//                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#AddNewItemLineModal" class="btn btn-success" style="padding-top: 2px; ' +
//                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
//                            + '</span></button>')
//                            .height(30)
//                            .on('dxclick', async function () {
//                                $("#hiddenItem_LineID").val(options.data.ID);
//                                const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: options.data.Item_SupportGroupDTOID }, IsActive: true, GetUserDefinedDTO: true };
//                                const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
//                                await PopulateItem_LineFields(options.data, _userDefinedTemplateList);
//                            }).appendTo(container);
//                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#ReassingSupportGroupModal" class="btn btn-primary ms-2" style="padding-top: 2px; ' +
//                            'padding-bottom:5px"><i class="fa fa-right-left"></i><span>' +
//                            + '</span></button>')
//                            .height(30)
//                            .on('dxclick', function () {
//                                PopulateReassignSupportGroupModalFields(options.data);
//                            }).appendTo(container);
//                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
//                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
//                            + '</span></button>')
//                            .height(30)
//                            .on('dxclick', function () {
//                                $("#hiddenItem_LineID").val(options.data.ID);
//                                ShowItem_LineDeleteQuestion();
//                            }).appendTo(container);

//                    },
//                },
//                {
//                    // Dropdown
//                    caption: "Labels",
//                    width: 110,
//                    visibleIndex: 0,
//                    alignment: "center",
//                    editCellTemplate: function (cellElement, cellInfo) {
//                        let wrapper = $('<div class="d-flex justify-content-center align-items-center">').appendTo(cellElement);
//                        $('<div class="d-flex justify-content-center">').appendTo(wrapper).dxMenu({
//                            items: [{
//                                // Dropdown tittle
//                                text: 'Print',
//                                icon: 'print',
//                                // Declaration of the list of options for the dropdown
//                                items: [{
//                                    text: 'S',
//                                    LabelFile: 'LF-0001-15-A'
//                                }, {
//                                    text: 'M',
//                                    LabelFile: 'LF-2173-03-A'
//                                }, {
//                                    text: 'L',
//                                    LabelFile: 'LF-2173-02-A'
//                                }, {
//                                    text: 'S ESD',
//                                    LabelFile: 'LF-0001-13-A'
//                                }, {
//                                    text: 'M ESD',
//                                    LabelFile: 'LF-2173-01-A'
//                                }, {
//                                    text: 'L ESD',
//                                    LabelFile: 'LF-2173-00-A'
//                                }]
//                            }],
//                            showFirstSubmenuMode: 'onHover',
//                            hideSubmenuOnMouseLeave: true,
//                            onItemClick: function (e) {
//                                var _data = cellInfo.data;
//                                // Validates if the LabelFile property exists in the selected option
//                                if (e.itemData.LabelFile != null) {
//                                    // Open the label
//                                    window.open('http://avmx-s05:8044//LabelPrint.aspx?LabelFile=' + e.itemData.LabelFile + "&Dummy=False&WO=" + _data.Serial + "&Qty=1&From=&To=&SkipEvery=&SerialLength=&ESD=True&SAS=False&FullWorkOrder=" + _data.Serial);
//                                }
//                            },
//                            onItemRendered: function (e) {
//                                let menuItemText = e.itemElement.find('.dx-menu-item-text');
//                                if (menuItemText.text().trim() === 'Print') {
//                                    menuItemText.append('<i class="ms-1 fa fa-angle-down"></i>');
//                                }
//                            }
//                        });
//                    },
//                    showEditorAlways: true,
//                    allowEditing: false
//                },
//            ],
//    }).appendTo(container);
//    let _itemLineMasterDetailsGrid = $(`#dxItem_HeaderLineList${masterDetailOptions.data.ID}`).dxDataGrid("instance");
//    if (_itemLineMasterDetailsGrid != null) {
//        var _state = _itemLineMasterDetailsGrid.state();
//        var _columns = _itemLineMasterDetailsGrid.option("columns");
//        if (ItemLineList.length > 0) {
//            Object.keys(ItemLineList[0]).forEach(key => {
//                let column = { dataField: key }
//                if (key.includes("Date")) {
//                    column.dataType = 'date';
//                }
//                if (key.includes("DTO")) {
//                    column.visible = false;
//                }
//                _columns.push(column);

//            });
//        }
//        _itemLineMasterDetailsGrid.option("columns", _columns);
//        _itemLineMasterDetailsGrid.state(_state)
//    }

//    $('<a id="AddNewItemBtn" class="btn btn-success mb-2" data-bs-target="#AddNewItemLineModal" data-bs-toggle="modal">Add new Item</a>').prependTo(container);
//    let _addNewItemBtn = document.getElementById("AddNewItemBtn");
//    if (_addNewItemBtn != null) {
//        _addNewItemBtn.addEventListener("click", async function () {
//            const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };//new
//            const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
//            ClearItem_LineFields(_userDefinedTemplateList);
//            await PopulateNewItemLineField(masterDetailOptions.data);
//        });
//    }
//    ReassignSupportGroupModalActionButtons("Update");
//}
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
        valueExpr: "StatusDTO.ID",
        displayExpr: "StatusDTO.Name",
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
    $("#hiddenStationID").val(Data.StationDTOID);
    $("#hiddenItem_SupportGroupID").val(Data.Item_SupportGroupDTOID);
    $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", Data.SupportGroupDTOID);
    $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value", Data.StationDTOID)
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", Data.Item_HeaderDTOID);
    $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value", Data.OwnerDTOID);
    $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value", Data.SupplyTypeDTOID);
    $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value", Data.ManufactureSerialID);
    $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value", Data.ShipmentReceiptNumber);
    $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value", Data.StatusDTOID);
    $("#dxItem_LineImportInvoiceTextBox").dxTextBox("instance").option("value", Data.ImportInvoice);
    $("#dxItem_LineBasePriceUSDTextBox").dxTextBox("instance").option("value", Data.BasePriceUSD);
    $("#dxItem_LineCommentsTextArea").dxTextArea("instance").option("value", Data.Comments);
    $("#dxItem_LinePONumberTextBox").dxTextBox("instance").option("value", Data.PONumber);
    $("#dxItem_LinePOLineTextBox").dxTextBox("instance").option("value", Data.POLine);
    $("#dxItem_LineLegacyIDTextBox").dxTextBox("instance").option("value", Data.LegacyID);
    $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value", Data.Serial);
    for (var _userDefined in UserDefinedList) {
        switch (UserDefinedList[_userDefined].UserDefinedDTO.DataTypeDTO.ID) {
            case DataType_Enum.Text:
                $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedDTO.Name]);
                break;
            case DataType_Enum.DateTime:
                $("#dxUserDefined" + _userDefined).dxDateBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedDTO.Name]);
                break;
            case DataType_Enum.Check:
                let _userDefinedValue = Data[UserDefinedList[_userDefined].UserDefinedDTO.Name] == 'true' ? true : false;
                $("#dxUserDefined" + _userDefined).dxCheckBox("instance").option("value", _userDefinedValue);
                break;
            case DataType_Enum.Numeric:
                $("#dxUserDefined" + _userDefined).dxTextBox("instance").option("value", Data[UserDefinedList[_userDefined].UserDefinedDTO.Name]);
                break;
        }
    }

}
async function PopulateNewItemLineField(Data) {
    await $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", Data.Item_HeaderDTO.ID);
    await $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", Data.SupportGroupDTO.ID);

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
        switch (UserDefinedTemplateList[_userDefined].UserDefinedDTO.DataTypeDTO.ID) {
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
        Item_HeaderDTO: {
            ID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value")
        },
        Item_SupportGroupDTO: {
            ID: $("#hiddenItem_SupportGroupID").val(),
            SupportGroupDTO: {
                ID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value")
            }
        },
        OwnerDTO: {
            ID: $("#dxItem_LineOwnerSelectBox").dxSelectBox("instance").option("value"),
        },
        SupplyTypeDTO: {
            ID: $("#dxItem_LineSupplyTypeSelectBox").dxSelectBox("instance").option("value"),
        },
        Serial: $("#dxItem_LineSerialTextBox").dxTextBox("instance").option("value"),
        ManufactureSerialID: $("#dxItem_LineManufactureSerialIDTextBox").dxTextBox("instance").option("value"),
        ShipmentReceiptNumber: $("#dxItem_LineShipmentReceiptNumberTextBox").dxTextBox("instance").option("value"),
        StatusDTO: {
            ID: $("#dxItem_LineStatusSelectBox").dxSelectBox("instance").option("value")
        },
        StationDTO: {
            ID: $("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("value")
        },
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
            UserDefinedDTO: {
                ID: UserDefinedTemplateList[_userDefined].UserDefinedDTO.ID
            },
            SupportGroupDTO: {
                ID: $("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value")
            },
            IsActive: true,
            Value: ""
        }
        switch (UserDefinedTemplateList[_userDefined].UserDefinedDTO.DataTypeDTO.ID) {
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
        const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
        const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
        ClearItem_LineFields(_userDefinedTemplateList);
    }
}
async function CreateItem_Line_Global() {
    await dxLoadPanel.show();
    const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
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
    const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
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
    const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    const _item_LineDTO = GetItem_LineDTO();
    const _validation_ResultDTO = await DeleteItem_Line(_item_LineDTO);
    if (_validation_ResultDTO.Result) {
        ClearItem_LineFields(_userDefinedTemplateList);
        //toggleMasterRow()
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
            fileName: "ItemAdministration",
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
                    dataField: "SupportGroupDTO.EnglishName"
                },
                {
                    caption: "Name",
                    dataField: "Name"
                },
                {
                    caption: "Data Type",
                    dataField: "DataTypeDTO.Name"
                },
                {
                    caption: "Required?",
                    dataField: "IsMandatory"
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
    $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("value", data.SupportGroupDTO.ID);
    $("#dxUserDefinedDataTypeSelectBox").dxSelectBox("instance").option("value", data.DataTypeDTO.ID);
    $("#dxUserDefinedNameTextBox").dxTextBox("instance").option("value", data.Name);
}
function GetUserDefinedDTO() {
    let _userDefinedDTO = {
        ID: $("#hiddenUserDefinedID").val(),
        Name: $("#dxUserDefinedNameTextBox").dxTextBox("instance").option("value"),
        IsMandatory: $("#dxUserDefinedIsMandatoryCheckBox").dxCheckBox("instance").option("value"),
        IsActive: $("#dxUserDefinedIsActiveCheckBox").dxCheckBox("instance").option("value"),
        DataTypeDTO: {
            ID: $("#dxUserDefinedDataTypeSelectBox").dxSelectBox("instance").option("value")
        },
        SupportGroupDTO: {
            ID: $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("value")
        }
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
        SupportGroupDTO: {
            ID: SupportGroupID, IsActive: true
        }
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
        Item_SupportGroupDTO: {
            ID: $("#hiddenItem_SupportGroupID").val(),
            SupportGroupDTO: {
                ID: $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value"),
            },
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
        //$("#AssignUserDefinedFieldsModal").modal("hide");
        GetUserDefinedTemplateList();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function GetUserDefinedTemplateList() {
    //await dxLoadPanel.show();
    const _userDefinedTemplateDTO = { Item_SupportGroupDTO: { ID: $("#hiddenItem_SupportGroupID").val() }, IsActive: true, GetUserDefinedDTO: true };
    const _userDefinedTemplateList = await GetUserDefinedTemplateInformation(_userDefinedTemplateDTO);
    await SelectUserDefinedFieldsFromTemplate(_userDefinedTemplateList);
    //await BuildUserDefinedOnItem_LineModal(_userDefinedTemplateList);

    //dxLoadPanel.hide();
}
async function SelectUserDefinedFieldsFromTemplate(UserDefinedTemplateList) {
    var _list = $("#dxUserDefinedList").dxList("instance");
    _list.option("selectedItemKeys", []);
    for (let i in UserDefinedTemplateList) {
        var _selectedFields = _list.option("selectedItemKeys");
        _selectedFields.push(UserDefinedTemplateList[i].UserDefinedDTO.ID);
        _list.option("selectedItemKeys", _selectedFields);
    }
}
async function BuildUserDefinedOnItem_LineModal(UserDefinedTemplateList) {

    let _userDefinedSection = document.getElementById("UserDefinedTemplateSection");
    _userDefinedSection.innerHTML = ""
    for (let i in UserDefinedTemplateList) {
        _userDefinedSection.innerHTML += "<label class=\"form-label col-form-label col-md-12\">" + UserDefinedTemplateList[i].UserDefinedDTO.Name + "</label>";
        _userDefinedSection.innerHTML += '<div class="col-md-12"><div id="dxUserDefined' + i + '"></div></div>';
    }
    for (let i in UserDefinedTemplateList) {
        switch (UserDefinedTemplateList[i].UserDefinedDTO.DataTypeDTO.ID) {
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
            //$("#dxItem_LineStationSelectBox").dxSelectBox("instance").option("readOnly", false);
            //$("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("readOnly", false);
        }
    }
}
async function FilterDataSourceBySupportGroups(UserDTO) {
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.SupportGroupIDArray != null && !UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            if (UserDTO.SupportGroupIDArray.length == 1) {
                document.getElementById("hiddenUserSupportGroupID").value = UserDTO.SupportGroupIDArray[0]
                //$("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("value", UserDTO.SupportGroupIDArray[0]);
                $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("value", UserDTO.SupportGroupIDArray[0]);
                $("#dxItemAdministrationDatGrid").dxDataGrid("instance").option('dataSource', await GetDXItem_SupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetItem_HeaderDTO: true }));
            } else {
                //$("#dxItem_LineSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxUserDefinedSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
            }
            $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
            //$("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
            //$("#dxUserDefinedDataGrid").dxDataGrid("instance").option("dataSource", await GetDXUserDefinedDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
            $("#dxItemAdministrationDatGrid").dxDataGrid("instance").option('dataSource', await GetDXItem_SupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetItem_HeaderDTO: true, Item_HeaderDTO: { GetItemHeaderPicture: true } }));

        }
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            $("#dxItemAdministrationDatGrid").dxDataGrid("instance").option('dataSource', await GetDXItem_SupportGroupDataSource({ GetItem_HeaderDTO: true, Item_HeaderDTO: { GetItemHeaderPicture: true } }));
            //$("#dxUserDefinedDataGrid").dxDataGrid("instance").option("dataSource", await GetDXUserDefinedDataSource());
            $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource());
        }
    }
}
//#endregion

//let currentlyOpenRowKey = null;
//function toggleMasterRow(rowKey, event) {
//    var dataGrid = $("#dxItemAdministrationDatGrid").dxDataGrid("instance");

//    // Check if the click is on the expand/collapse button
//    var isClickOnExpandCollapseButton = $(event.target).closest(".dx-datagrid-expand").length > 0;

//    // If the click is on the expand/collapse button, proceed with expanding/collapsing
//    if (isClickOnExpandCollapseButton) {
//        // Collapse the currently open row if there is one
//        if (currentlyOpenRowKey !== null && currentlyOpenRowKey !== rowKey) {
//            dataGrid.collapseRow(currentlyOpenRowKey);
//        }

//        // Toggle the clicked row
//        if (dataGrid.isRowExpanded(rowKey)) {
//            dataGrid.collapseRow(rowKey);
//            currentlyOpenRowKey = null; // No row is open
//        } else {
//            dataGrid.expandRow(rowKey);
//            currentlyOpenRowKey = rowKey; // Update the currently open row
//        }
//    }
//}
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
    $("#dxTreeViewList").dxTreeView("instance").option("dataSource", await GetItem_LineFilesTreeView({ Item_HeaderDTO: { ID: $("#hiddenItem_HeaderID").val() } }));
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
        searchExpr: ["EnglishName"],
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
    $("#hiddenItem_SupportGroupID").val(data.Item_SupportGroupDTOID);
    $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value", data.Item_HeaderDTOID);
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value", data.SupportGroupDTOID);
}

function ClearReassignSupportGroupModalFields() {
    $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value", '');
}
function GetItemLine_SupportGroupModalDTO() {
    //This build a item line with Item_supportGroup Relation
    let _item_LineDTO = {
        ID: $("#hiddenItem_LineID").val(),
        Item_HeaderDTO: {
            ID: $("#dxItem_LineItem_HeaderSelectBox").dxSelectBox("instance").option("value"),
        },
        Item_SupportGroupDTO: {
            ID: $("#hiddenItem_SupportGroupID").val(),
            SupportGroupDTO: {
                ID: $("#dxReassignSupportGroupModalSelectBox").dxSelectBox("instance").option("value"),
            },
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
    //$("#dxItem_SupportGroupItem_HeaderIDSelectBox").dxSelectBox({
    //    dataSource: await GetDXItem_HeaderDataSource(),
    //    valueExpr: "ID",
    //    displayExpr: "NamesWithModel",
    //    deferRendering: false,
    //    searchEnabled: true,
    //    searchExpr: ["Model", "EnglishName"],
    //    searchMode: 'contains'
    //});
    $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox({
        dataSource: [], //await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        readOnly: false,
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName"],
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
        Item_HeaderDTO: {
            //ID: $("#hiddenItem_HeaderID").val() == 0 ? $("#dxItem_SupportGroupItem_HeaderIDSelectBox").dxSelectBox("instance").option("value") : $("#hiddenItem_HeaderID").val(),
            ID: $("#hiddenItem_HeaderID").val(),
        },
        SupportGroupDTO: {
            ID: $("#dxItem_SupportGroupSupportGroupSelectBox").dxSelectBox("instance").option("value"),
        },
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
        //CloseWizard();
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