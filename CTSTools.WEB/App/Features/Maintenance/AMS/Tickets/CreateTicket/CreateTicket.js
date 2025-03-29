import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXSupportGroupDataSource } from '../../SupportGroup/SupportGroup_Service.js'
import { GetDXPriorityDataSource } from '../Priority/Priority_Service.js'
import { GetDXCategoryDataSource } from '../Category/Category_Service.js'
import { GetItem_LineInformation, GetItem_LineInformationBySupportGroup } from '../../Item/ItemAdministration/Item_Line/Item_Line_Service.js'
import { GetUserInformation } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
//import { GetDXEmployeeTressDataSource } from '../../AdvancedSettings/Users/EmployeeTress/EmployeeTress_Service.js'//
//import { Role_Enum } from '../../AdvancedSettings/Security/Roles/Role/Role_Enum.js'//
import { GetDXFacilityDataSource } from '../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXDepartmentDataSource } from '../../../AdvancedSettings/LocationManagement/Department/Department_Service.js'//
import { CreateTicket } from '../Ticket/Ticket_Service.js'

let _fileDTOList = [];
let _fileDTO = {};

document.addEventListener("DOMContentLoaded", async () => {
    //await InitializeSparePartControls();
    //await InitializeSparePartIventoryControls();
    //await InitializeSparePartIventoryModalControls();
    await InitializeTicketControls();
    await GetUserInformationbyID();
    EventHandler();
});

async function EventHandler() {
    document.getElementById("SubmitButton").addEventListener("click", SubmitTicket_Global);
    //document.getElementById("Inventory-Tab").addEventListener("click", GetSparePartInventoryInformation);
    //document.getElementById("SparePart-lot-tab").addEventListener("click", GetSparePartLotInformation);

    let devExpressButton = document.querySelectorAll(".dx-fileuploader-button");

    devExpressButton.forEach(function (currentButton) {
        // Remove DevExpress button classes
        currentButton.classList.remove("dx-button", "dx-widget", "dx-button-normal", "dx-button-mode-contained", "dx-widget", "dx-button-has-text");

        // Add Bootstrap button classes
        currentButton.classList.add("btn", "btn-default");
    });
}

async function GetUserInformationbyID() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _userInformation = await GetUserInformation(_userDTO);
    //await DisabledAdministrationFields(_userInformation[0]);
    await PopulateUserInformation(_userInformation[0]);
    dxLoadPanel.hide();
}


async function PopulateUserInformation(UserDTO) {
    if (UserDTO != null) {
        //await $("#dxTicketRequestorSelectBox").dxSelectBox("instance").option("value", UserDTO.EmployeeTressDTO.ID);
        await $("#dxTicketFacilitySelectBox").dxSelectBox("instance").option("value", UserDTO.FacilityDTO.ID);
        await $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("value", UserDTO.DepartmentDTO.ID);
    }
}



function GetUserDTO() {
    let _userDTO = {
        ID: document.getElementById('hiddenUserID').value,
        GetSupportGroupArray: true,
        GetRoleArray: true
    }
    return _userDTO;
}


//#region Spare Part Lot

async function InitializeTicketControls() {

    //$("#dxTicketRequestorSelectBox").dxSelectBox({
    //    dataSource: await GetDXEmployeeTressDataSource({ IsActive: true }),
    //    valueExpr: "ID",
    //    displayExpr: "Name",
    //    deferRendering: false,
    //    searchEnabled: true
    //});
    $("#dxTicketDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxTicketFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Facility...",
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                let _departmentDTO = {
                    IsActive: true,
                    FacilityID: e.value
                }
                $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").reset();
                $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource(_departmentDTO));
                await InitializeSupportGroupDataSource(e.value);
            } else {
                $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        },
    });
    $("#dxTicketDepartmentSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        placeholder: "Select a Department...",
    });

    $("#dxTicketSupportGroupSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName"],
        searchMode: 'contains',
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await InitializePriorityDataSource(e.value);
                await InitializeCategoryDataSource(e.value);
                await InitializeItemLineDataSource(e.value);
            }
            else {
                ClearSupportGroupFields();
            }
        }
    });
    $("#dxTicketItemLookup").dxLookup({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "ItemHeaderNameWithPartNumberSerial",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true
    });

    $("#dxTicketPrioritySelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true
    });
    $("#dxTicketCategorySelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        visible: true,
        readOnly: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await InitializeSubcategoryDataSource(e.value);
            }
            else {
                ClearSubcategoryDataSource();
                ClearThirdLevelCategoryDataSource();
            }
        }
    });
    $("#dxTicketSubcategorySelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        visible: true,
        readOnly: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await InitializeThirdLevelCategoryDataSource(e.value);
            }
            else {
                ClearThirdLevelCategoryDataSource();
            }
        }
    });
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        visible: true,
        readOnly: true
    });
    $("#dxTicketTitleTextBox").dxTextBox({
        placeholder: 'Type Title',
    });
    $("#dxTicketDescriptionTextArea").dxTextArea({
        placeholder: 'Type Description',
        height: 100,
    });
    $("#dxTicketNoteTextArea").dxTextArea({
        placeholder: 'Type Note',
        height: 100,
    });
    $("#dxTicketFileUploader").dxFileUploader({
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
            var customPreviewContainer = $("#TicketFilelist");
            customPreviewContainer.empty();
            e.element.find(".dx-fileuploader-upload-button").hide();
            $.each(files, function (i, file) {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var _fileDTO = {
                        Name: $("#dxTicketFileUploader").dxFileUploader("instance").option("value")[i].name,
                        Size: $("#dxTicketFileUploader").dxFileUploader("instance").option("value")[i].size,
                        Data: e.target.result,
                    };
                    _fileDTOList.push(_fileDTO);

                    let preview = $("<div>").addClass("text-center p-1 col-md-2")

                    let fileIconClass = getFileIconClass(file);

                    let cancelBtn = $("<div>").addClass("col-10 text-end").append($("<button>").addClass("btn btn-sm btn-danger").text("X"));
                    cancelBtn.on("click", function () {
                        removeFile(file);
                        preview.remove();
                    });

                    preview.append(cancelBtn);
                    preview.append($("<i>").addClass(" fa " + fileIconClass + " fa-5x"));
                    preview.append($("<p>").addClass("mt-3 fw-bold").text("File Name: " + file.name));
                    customPreviewContainer.append(preview);

                }
            })
        },
    });
}

function removeFile(file) {
    const value = $("#dxTicketFileUploader").dxFileUploader("instance").option('value');
    const index = value.indexOf(file);
    if (index > -1) {
        value.splice(index, 1);
    }
    $("#dxTicketFileUploader").dxFileUploader("instance").reset();
    $("#dxTicketFileUploader").dxFileUploader("instance").option({ value });
}

function getFileIconClass(file) {
    let fileIconClass = " fa-file";
    if (file.type.includes("pdf")) {
        fileIconClass = "fa-file-pdf";
    } else if (file.type.includes("word") || file.name.endsWith(".doc") || file.name.endsWith(".docx")) {
        fileIconClass = "fa-file-word";
    } else if (file.type.includes("excel") || file.name.endsWith(".xls") || file.name.endsWith(".xlsx")) {
        fileIconClass = "fa-file-excel";
    } else if (file.type.includes("powerpoint") || file.name.endsWith(".ppt") || file.name.endsWith(".pptx")) {
        fileIconClass = "fa-file-powerpoint";
    }
    return fileIconClass
}

function GetTicketDTO() {
    let _ticketDTO = {
        //RequestorDTO: {
        //    ID: $("#dxTicketRequestorSelectBox").dxSelectBox("instance").option("value")
        //},
        FacilityDTO: {
            ID: $("#dxTicketFacilitySelectBox").dxSelectBox("instance").option("value")
        },
        SupportGroupDTO: {
            ID: $("#dxTicketSupportGroupSelectBox").dxSelectBox("instance").option("value")
        },
        DepartmentDTO: {
            ID: $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("value"),
        },
        Item_LineDTO: {
            ID: $("#dxTicketItemLookup").dxLookup("instance").option("value"),
        },
        PriorityDTO: {
            ID: $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("value"),
        },
        CategoryDTO: {
            ID: $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("value"),
        },
        SubCategoryDTO: {
            ID: $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("value"),
        },
        ThirdLevelCategoryDTO: {
            ID: $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("value"),
        },
        Title: $("#dxTicketTitleTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxTicketDescriptionTextArea").dxTextArea("instance").option("value"),
        Note: $("#dxTicketNoteTextArea").dxTextArea("instance").option("value"),
        IsActive: true,
        FileDTO: GetFileDTO()
    }
    return _ticketDTO
}

async function SubmitTicket_Global() {
    await dxLoadPanel.show();
    const _TicketDTO = GetTicketDTO();
    const _validation_ResultDTO = await CreateTicket(_TicketDTO);
    if (_validation_ResultDTO.Result) {
        BuildSuccessMessage(_validation_ResultDTO);
    } else {
        HostResponse(_validation_ResultDTO);
    }
    dxLoadPanel.hide();
}

function GetFileDTO() {
    if ((_fileDTO != null || _fileDTO != undefined) && (_fileDTOList != null || _fileDTOList != undefined)) {
        _fileDTO.FileList = _fileDTOList;
    }
    return _fileDTO;
}

//#endregion

//#Initialize Datasources 
//#region Support Group Features
async function InitializeSupportGroupDataSource(FacilityID) {
    let _supportGroupDTO = { FacilityDTO: { ID: FacilityID }, IsActive: true };
    $("#dxTicketSupportGroupSelectBox").dxSelectBox("instance").reset();
    $("#dxTicketSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource(_supportGroupDTO));
}

async function InitializePriorityDataSource(SupportGroupID) {
    let _priorityDTO = { SupportGroupDTO: { ID: SupportGroupID }, IsActive: true };
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("dataSource", await GetDXPriorityDataSource(_priorityDTO));
}

async function InitializeCategoryDataSource(SupportGroupID) {
    let _categoryDTO = { HasParent: false, SupportGroupDTO: { ID: SupportGroupID }, IsActive: true };
    $("#dxTicketCategorySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("dataSource", await GetDXCategoryDataSource(_categoryDTO));
}


async function InitializeItemLineDataSource(SupportGroupID) {
    let _item_lineDTO = {
        Item_SupportGroupDTO: {
            SupportGroupIDArray: [SupportGroupID]
        },
        IsActive: true
    };
    $("#dxTicketItemLookup").dxLookup("instance").reset();
    $("#dxTicketItemLookup").dxLookup("instance").option("readOnly", false);
    $("#dxTicketItemLookup").dxLookup("instance").option("dataSource", await GetItem_LineInformationBySupportGroup(_item_lineDTO));
}

async function InitializeSubcategoryDataSource(CategoryParentID) {
    let _categoryDTO = await GetCategoryDTO(CategoryParentID);
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("dataSource", await GetDXCategoryDataSource(_categoryDTO));
}

async function InitializeThirdLevelCategoryDataSource(CategoryParentID) {
    let _categoryDTO = await GetCategoryDTO(CategoryParentID);
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("dataSource", await GetDXCategoryDataSource(_categoryDTO));
}

async function GetCategoryDTO(CategoryParentID) {
    let _categoryDTO = {
        ParentID: CategoryParentID,
        HasParent: true,
        IsActive: true
    };
    return _categoryDTO
}

async function ClearSubcategoryDataSource() {
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("readOnly", true);
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("dataSource", []);
}

async function ClearThirdLevelCategoryDataSource() {
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("readOnly", true);
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("dataSource", []);
}



async function ClearSupportGroupFields() {
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("readOnly", true);
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("dataSource", []);


    $("#dxTicketCategorySelectBox").dxSelectBox("instance").reset();
    $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("readOnly", true);
    $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("dataSource", []);


    $("#dxTicketItemLookup").dxLookup("instance").reset();
    $("#dxTicketItemLookup").dxLookup("instance").option("readOnly", true);
    $("#dxTicketItemLookup").dxLookup("instance").option("dataSource", []);
}






//#endregion

//#region Locations Feature

async function InitializeDepartmentDataSource(BusinessUnitID) {
    let _departmentDTO = { BusinessUnitDTO: { ID: BusinessUnitID }, IsActive: true };
    $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXDepartmentDataSource(_departmentDTO));
}

async function ClearDepartmentDataSource() {
    $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("dataSource", []);
}

//#endregion
//#endregion


function BuildSuccessMessage(ValidationResult) {
    $("#success-message").empty();
    $("#tabsContainer").empty();
    $("#success-message").append('<div>' +
        '<div class="card py-3 mt-1">' +
        '<div class="card-body text-center" style="font-size:20px;"><i style="color:#2F6B68;" class="fas fa-envelope-circle-check fa-3x"></i>' +
        '<h2 class="pb-3"><strong>The ticket has been sent successfully!</strong></h2>' +
        '<p class="mb-2">You will receive support from the members of the support group of the correspondig area.</p>' +
        '<p> We are sending you a confirmation email.  <br>  <b style="font-size: x-large">Ticket Number #' + ValidationResult.Data + '</b></p>' +
        /* '<p><u>:</u></p>' +*/
        '<a class="btn btn-default mt-3 mr-3" href="/App/Features/Default.aspx">Home</a> <a class="btn btn-success mt-3 mr-3" href="/App/Features/Ticket/Tickets/CreateTicket/CreateTicket.aspx">Submit Another ticket</a>' +
        '</div>' +
        '</div>' +
        '</div>');
}