import { dxLoadPanel } from '../../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../../Common/Utils/Response.js'
import { GetURLParameter } from '../../../../../Common/Utils/Utils.js'
import { GetDXSupportGroupDataSource } from '../../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetDXPriorityDataSource } from '../Priority/Priority_Service.js'
import { GetDXCategoryDataSource } from '../Category/Category_Service.js'
import { GetItem_LineInformationBySupportGroup } from '../../ItemManagement/Item_Line/Item_Line_Service.js'
import { GetUserInformation, GetDXUserDataSource } from '../../../../AdvancedSettings/UserManagement/User/User_Service.js'
//import { GetDXEmployeeTressDataSource } from '../../AdvancedSettings/Users/EmployeeTress/EmployeeTress_Service.js'//
import { Role_Enum } from '../../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'
import { GetDXFacilityDataSource } from '../../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXDepartmentDataSource } from '../../../../AdvancedSettings/LocationManagement/Department/Department_Service.js'
import { StatusType_Enum } from '../../../../AdvancedSettings/StatusManagement/StatusType/StatusType_Enum.js'
import { GetDXStatus_StatusTypeDataSource } from '../../../../AdvancedSettings/StatusManagement/Status_StatusType/Status_StatusType_Service.js'
import { DeleteTicketFile, GetTicketFilesInformation, GetTicketInformation, UpdateTicket, UploadTicketFile } from '../Ticket/Ticket_Service.js'
import { GetDXSupportGroupMemberDataSource } from '../../SupportGroupManagement/SupportGroupMember/SupportGroupMember_Service.js'
import { Status_Enum } from '../../../../AdvancedSettings/StatusManagement/Status/Status_Enum.js'
import { GetDXSparePart_LotDataSource, GetSparePart_LotInformation } from '../../SparePartManagement/SparePart_Lot/SparePart_Lot_Service.js'
import { DeleteSparePartUsage, GetDXSparePartUsageDataSource, CreateSparePartUsage } from '../../SparePartManagement/SparePartUsage/SparePartUsage_Service.js'

let _fileDTOList = [];
let _fileDTO = {};

document.addEventListener("DOMContentLoaded", async () => {
    await GetURLParameter_Global()
    const _ticketID = $("#hiddenTicketID").val()
    if (_ticketID != null && _ticketID != undefined && _ticketID != "" && _ticketID != "0") {
        try {
            await InitializeTicketControls();
            await InitializeSparePartUsageControls();
            await GetTicketInformation_Global();
            await GetUserInformationbyID();
            EventHandler();
        } catch (err) {
            //show error message
            console.log("Message Error :")
            console.log(err)
            //hide loader animation
            dxLoadPanel.hide();
        }
    } else {
        TicketNotFound()
    }

});

function TicketNotFound() {
    $("#TicketContent").empty()
    let errorContent = $("<div>").addClass("error ");
    errorContent.append('<div class="error-code textColor">404</div>');
    errorContent.append('<h1 class="m-b-15 text-center">Ticket Not Found !</h1>');
    $('#TicketContent').append(errorContent);
}





async function GetURLParameter_Global() {
    let _ticketID = GetURLParameter("TicketID");
    $('#hiddenTicketID').val(_ticketID);
}

async function EventHandler() {
    document.getElementById('SaveButton').addEventListener('click', ShowUpdateTicketQuestion);
    document.getElementById('CreateSparePartUsageButton').addEventListener('click', CreateSparePartUsage_Global);
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
    await DisabledAdministrationFields(_userInformation[0]);
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

function hideActionsButtons() {
    $("#CreateSparePartUsageButton").attr("hidden", true);
    $("#SaveButton").attr("hidden", true);
}



//#region Behavior for support group and user
function DisabledAdministrationFields(UserDTO) {
    const _ticketStatusID = $("#hiddenCurrentStatusID").val();
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin) || UserDTO.RoleIDArray.includes(Role_Enum.Administrator) || UserDTO.RoleIDArray.includes(Role_Enum.Technician)) {
            $("#dxTicketSupportGroupSelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketFacilitySelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketAssignedToSelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketStatusSelectBox").dxSelectBox("instance").option("readOnly", false);
            $("#dxTicketFileUploader").dxFileUploader("instance").option("disabled", false);
            $("#dxTicketSolutionTextArea").dxTextArea("instance").option("readOnly", false);
            $("#dxTicketResolutionTextArea").dxTextArea("instance").option("readOnly", false);
            $("#dxTicketItemLookup").dxLookup("instance").option("readOnly", false);
            $("#SaveButton").removeAttr("hidden");
            $("#CreateSparePartUsageButton").removeAttr("disabled");
        }
    }
    if (_ticketStatusID != null && _ticketStatusID != undefined && _ticketStatusID != "" && _ticketStatusID != 0) {
        if (_ticketStatusID == Status_Enum.Closed) {
            hideActionsButtons();
        }
    }
}
//#endregion





//#region Initialize Ticket Controls

async function InitializeTicketControls() {
    const now = new Date();
    //$("#dxTicketRequestorSelectBox").dxSelectBox({
    //    dataSource: await GetDXEmployeeTressDataSource({ IsActive: true }),
    //    valueExpr: "ID",
    //    displayExpr: "Name",
    //    deferRendering: false,
    //    readOnly: true,
    //    searchEnabled: true
    //});

    $("#dxTicketStatusSelectBox").dxSelectBox({
        dataSource: await GetDXStatus_StatusTypeDataSource({ StatusTypeID: StatusType_Enum.Tickets }),
        displayExpr: "StatusName",
        valueExpr: "StatusID",
        searchEnable: true
    })
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
        dataSource: await GetDXSupportGroupDataSource({ IsActive: true }),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true,
        searchExpr: ["EnglishName"],
        searchMode: 'contains',
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await InitializeCategoryDataSource(e.value);
                await InitializePriorityDataSource(e.value);
                await InitializeAssignedToDataSource(e.value);
                await InitializeItemLineDataSource(e.value);
                await InitializeSparePart_LotDataSource(e.value);
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
        readOnly: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await $("#SparePartAccordion").removeAttr("hidden");
            }
        }
    });

    $("#dxTicketPrioritySelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true,
    });
    $("#dxTicketCategorySelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true,
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
        readOnly: true,
    });
    $("#dxTicketDescriptionTextArea").dxTextArea({
        placeholder: 'Type Description',
        readOnly: true,
        height: 100,
    });
    $("#dxTicketNoteTextArea").dxTextArea({
        readOnly: true,
        height: 100,
    });
    $("#dxTicketFileUploader").dxFileUploader({
        selectButtonText: "Select a file",
        labelText: "or drop it here",
        accept: "file",
        uploadedMessage: "Loading",
        multiple: true,
        uploadMode: "instantly",
        invalidMaxFileSizeMessage: 'The file is too large. Allowed maximun size is 5MB',
        maxFileSize: 5000000,
        showFileList: true,
        disabled: true,
        width: "100%",
        onValueChanged: function (e) {
            var files = e.value;
            _fileDTOList = [];
            e.element.find(".dx-fileuploader-upload-button").hide();
            $.each(files, function (i, file) {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = async function (e) {
                    var _fileDTO = {
                        Name: $("#dxTicketFileUploader").dxFileUploader("instance").option("value")[i].name,
                        Size: $("#dxTicketFileUploader").dxFileUploader("instance").option("value")[i].size,
                        Data: e.target.result,
                    };
                    _fileDTOList = [];
                    _fileDTOList.push(_fileDTO);
                    await UploadAttachment({
                        ID: document.getElementById('hiddenTicketID').value,
                        Name: _fileDTO.Name,
                        Data: _fileDTO.Data,
                        FileList: _fileDTOList
                    });
                }
            });

        },
    });


    $("#dxTicketSolutionTextArea").dxTextArea({
        placeholder: 'Type solution',
        height: 100,
        readOnly: true
    });
    $("#dxTicketResolutionTextArea").dxTextArea({
        placeholder: 'Type resolution',
        height: 100,
        readOnly: true
    });
    $("#dxTicketAddedDateDateBox").dxDateBox({
        type: "datetime",
        max: now,
        label: "Date and time",
        labelMode: "floating",
        readOnly: true
    });
    $("#dxTicketAssignedToSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "UserDTO.ID",
        displayExpr: "UserDTO.Name",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true,
    });
    $("#dxTicketAssignedDateDateBox").dxDateBox({
        type: "datetime",
        label: "Date and time",
        labelMode: "floating",
        readOnly: true
    });
    $("#dxTicketClosedBySelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true
    });
    $("#dxTicketClosedDateDateBox").dxDateBox({
        type: "datetime",
        label: "Date and time",
        labelMode: "floating",
        readOnly: true
    });
}


async function GetTicketInformation_Global() {
    const TicketDTO = {
        ID: $("#hiddenTicketID").val()
    };
    if (TicketDTO.ID != null && TicketDTO.ID != undefined && TicketDTO.ID != "" && TicketDTO.ID != "0") {
        let _ticketInformation = await GetTicketInformation(TicketDTO);
        await PopulateTicketInformation(_ticketInformation[0]);
    }
}

async function PopulateTicketInformation(TicketDTO) {

    if (TicketDTO != null) {
        await dxLoadPanel.show();
        await $("#dxTicketSupportGroupSelectBox").dxSelectBox("instance").option("value", TicketDTO.SupportGroupID);
        $("#hiddenCreatedByID").val(TicketDTO.CreatedByID);
        document.getElementById('TicketNumber').innerHTML = `Ticket #<span class="fw-bold">${TicketDTO.TicketNumber}<span>`;
        document.getElementById('CreatedBySection').innerHTML = `Created By : <span class="fw-bold">${TicketDTO.CreatedByName}<span>`;
        await PopulateStatusSection(TicketDTO.StatusID, TicketDTO.StatusName);
        //await $("#dxTicketRequestorSelectBox").dxSelectBox("instance").option("value", TicketDTO.RequestorID);
        await $("#dxTicketFacilitySelectBox").dxSelectBox("instance").option("value", TicketDTO.FacilityID);
        await $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("value", TicketDTO.DepartmentID);      
        await $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("value", TicketDTO.CategoryID);
        await $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("value", TicketDTO.SubCategoryID);
        await $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("value", TicketDTO.ThirdLevelCategoryID);
        await $("#dxTicketTitleTextBox").dxTextBox("instance").option("value", TicketDTO.Title);
        await $("#dxTicketDescriptionTextArea").dxTextArea("instance").option("value", TicketDTO.Description);
        await $("#dxTicketNoteTextArea").dxTextArea("instance").option("value", TicketDTO.Note);
        await $("#dxTicketSolutionTextArea").dxTextArea("instance").option("value", TicketDTO.Solution);
        await $("#dxTicketResolutionTextArea").dxTextArea("instance").option("value", TicketDTO.Resolution);
        await $("#dxTicketAddedDateDateBox").dxDateBox("instance").option("value", TicketDTO.AddedDate);
        await $("#dxTicketAssignedToSelectBox").dxSelectBox("instance").option("value", TicketDTO.AssignedToID);
        await $("#dxTicketAssignedDateDateBox").dxDateBox("instance").option("value", TicketDTO.AssignedDate);
        await $("#dxTicketClosedBySelectBox").dxSelectBox("instance").option("value", TicketDTO.ClosedByID);
        await $("#dxTicketClosedDateDateBox").dxDateBox("instance").option("value", TicketDTO.ClosedDate);
        await PopulateItemSection(TicketDTO.Item_LineID);
        await $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("value", TicketDTO.PriorityID);
        await $("#dxTicketStatusSelectBox").dxSelectBox("instance").option("value", TicketDTO.StatusID);
        console.log(TicketDTO.StatusID)
        console.log($("#dxTicketStatusSelectBox").dxSelectBox("instance").option("value"))
        console.log($("#dxTicketStatusSelectBox").dxSelectBox("instance").option("displayValue"))
        await GetFileList();
        dxLoadPanel.hide();
    } else {
        TicketNotFound();
    }
}

async function PopulateStatusSection(StatusID,StatusName) {
    $("#hiddenCurrentStatusID").val(StatusID);
    let bgcolor;
    switch (StatusID) {
        case Status_Enum.Active:
            bgcolor = "bg-green";
            break;
        case Status_Enum.In_Progress:
            bgcolor = "bg-orange";
            break;
        case Status_Enum.Closed:
            hideActionsButtons();
            bgcolor = "bg-gray";
            break;
    }
    document.getElementById('TicketStatus').innerHTML = `<div class="  btn btn-sm ${bgcolor} statusTitle py-0 px-2 text-white fs-5" >${StatusName}</div>`;
    await $("#dxTicketStatusSelectBox").dxSelectBox("instance").option("value", StatusID);
}

async function PopulateItemSection(Item_LineID) {
    if (Item_LineID != null) {
        await $("#dxTicketItemLookup").dxLookup("instance").option("value", Item_LineID);
    }
}

function GetTicketDTO() {
    let _ticketDTO = {
        ID: document.getElementById('hiddenTicketID').value,
        FacilityID: $("#dxTicketFacilitySelectBox").dxSelectBox("instance").option("value"),
        SupportGroupID: $("#dxTicketSupportGroupSelectBox").dxSelectBox("instance").option("value"),
        DepartmentID: $("#dxTicketDepartmentSelectBox").dxSelectBox("instance").option("value"),        
        Item_LineID: $("#dxTicketItemLookup").dxLookup("instance").option("value"),        
        CreatedByID: document.getElementById('hiddenCreatedByID').value,        
        PriorityID: $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("value"),
        CategoryID: $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("value"),        
        SubCategoryID: $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("value"),
        ThirdLevelCategoryID: $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("value"),
        StatusID: $("#dxTicketStatusSelectBox").dxSelectBox("instance").option("value"),        
        AssignedToID: $("#dxTicketAssignedToSelectBox").dxSelectBox("instance").option("value"),
        
        Title: $("#dxTicketTitleTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxTicketDescriptionTextArea").dxTextArea("instance").option("value"),
        Note: $("#dxTicketNoteTextArea").dxTextArea("instance").option("value"),
        Solution: $("#dxTicketSolutionTextArea").dxTextArea("instance").option("value"),
        Resolution: $("#dxTicketResolutionTextArea").dxTextArea("instance").option("value"),
        IsActive: true
    }
    return _ticketDTO
}


async function ShowUpdateTicketQuestion() {

    const _alert = await Swal.fire({
        title: "Are you sure you want to save the changes?",
        icon: 'question',
        confirmButtonText: `Save`,
        showCancelButton: true,
        reverseButtons: true,
        confirmButtonColor: '#265755',
    });
    if (_alert.isConfirmed) {
        UpdateTicket_Global();
    }
}


async function UpdateTicket_Global() {
    await dxLoadPanel.show();
    const _TicketDTO = GetTicketDTO();
    const _validation_ResultDTO = await UpdateTicket(_TicketDTO);
    if (_validation_ResultDTO.Result) {
        GetTicketInformation_Global()
        ClearErrorFeedback();
    }
    HostResponse(_validation_ResultDTO);
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
    //$("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxTicketPrioritySelectBox").dxSelectBox("instance").option("dataSource", await GetDXPriorityDataSource(_priorityDTO));
}

async function InitializeCategoryDataSource(SupportGroupID) {
    let _categoryDTO = { HasParent: false, SupportGroupDTO: { ID: SupportGroupID }, IsActive: true };
    await $("#dxTicketCategorySelectBox").dxSelectBox("instance").reset();
    //$("#dxTicketCategorySelectBox").dxSelectBox("instance").option("readOnly", false);
    await $("#dxTicketCategorySelectBox").dxSelectBox("instance").option("dataSource", await GetDXCategoryDataSource(_categoryDTO));
}

async function InitializeAssignedToDataSource(SupportGroupID) {
    let _supportGroupMemberDTO = { SupportGroupDTO: { ID: SupportGroupID }, IsActive: true };
    await $("#dxTicketAssignedToSelectBox").dxSelectBox("instance").reset();
    //$("#dxTicketAssignedToSelectBox").dxSelectBox("instance").option("readOnly", false);
    await $("#dxTicketAssignedToSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupMemberDataSource(_supportGroupMemberDTO));

}

async function InitializeItemLineDataSource(SupportGroupID) {
    let _item_lineDTO = {
        Item_SupportGroupDTO: {
            SupportGroupIDArray: [SupportGroupID]
        }
    };
    await $("#dxTicketItemLookup").dxLookup("instance").reset();
    //await $("#dxTicketItemLookup").dxLookup("instance").option("readOnly", false);
    await $("#dxTicketItemLookup").dxLookup("instance").option("dataSource", await GetItem_LineInformationBySupportGroup(_item_lineDTO));
}

async function InitializeSparePart_LotDataSource(SupportGroupID) {
    let _sparePart_Lot = {
        SupportGroupDTO: {
            ID: SupportGroupID
        },
        SparePartDTO: {
            GetImage: true
        },
        GetSparePartDTO: true,
        IsActive: true
    };
    await $("#dxSparePart_LotSelectBox").dxSelectBox("instance").reset();
    await $("#dxSparePart_LotSelectBox").dxSelectBox("instance").option("readOnly", false);
    await $("#dxSparePart_LotSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSparePart_LotDataSource(_sparePart_Lot));
}

async function InitializeSubcategoryDataSource(CategoryParentID) {
    let _categoryDTO = await GetCategoryDTO(CategoryParentID);
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").reset();
    //$("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxTicketSubcategorySelectBox").dxSelectBox("instance").option("dataSource", await GetDXCategoryDataSource(_categoryDTO));
}

async function InitializeThirdLevelCategoryDataSource(CategoryParentID) {
    let _categoryDTO = await GetCategoryDTO(CategoryParentID);
    $("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").reset();
    //$("#dxTicketThirdLevelCategorySelectBox").dxSelectBox("instance").option("readOnly", false);
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

    $("#dxSparePart_LotSelectBox").dxSelectBox("instance").reset();
    //$("#dxSparePart_LotSelectBox").dxSelectBox("instance").option("readOnly", true);
    $("#dxSparePart_LotSelectBox").dxSelectBox("instance").option("dataSource", []);

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


//#region Attachments

async function GetFileList() {
    let _ticketDTO = { ID: $("#hiddenTicketID").val() }
    let _tickeFileList = await GetTicketFilesInformation(_ticketDTO);
    await $("#TicketFilelist").empty();
    if ($(_tickeFileList).length > 0) {
        $.each(_tickeFileList, function (i, attachment) {
            let _attachmentIcon;
            switch (attachment.Extension) {
                default:
                    _attachmentIcon = `<i class="fas fa-file "></i>`;
                    break;
                case ".docx":
                    _attachmentIcon = `<i class="fas fa-file-word "></i>`;
                    break;
                case ".jpg":
                    _attachmentIcon = `<i class="fas fa-file-image "></i>`;
                    break;
                case ".pdf":
                    _attachmentIcon = `<i class="fas fa-file-pdf "></i>`;
                    break;
                case ".png":
                    _attachmentIcon = `<i class="fas fa-file-image "></i>`;
                    break;
                case ".xlsx":
                    _attachmentIcon = `<i class="fas fa-file-excel "></i>`;
                    break;
                case ".msg":
                    _attachmentIcon = `<i class="fas fa-envelope "></i>`;
                    break;
                case ".txt":
                    _attachmentIcon = `<i class="fas fa-file-lines "></i>`;
                    break;
            }
            let _attachmentName = attachment.Name;

            if (_attachmentName.length > 35) {
                _attachmentName = _attachmentName.substring(0, 35) + "...";
            }

            //Exclusion list for unwanted files to display
            if (attachment.Extension.toLowerCase() != '.db') {
                //Prepare preview for Images START
                if (attachment.Extension.toLowerCase() == '.jpg' || attachment.Extension.toLowerCase() == '.png' || attachment.Extension.toLowerCase() == '.bmp') {
                    $('#TicketFilelist').append(
                        `<div class="col-lg-3 col-xl-2 col-md-3 col-sm-3 mb-2">
                            <div class="card text-center h-100 d-flex flex-column">
                                <div class="my-2 mx-3">
                                    <a href="#" id="AttachmentID${i}" data-FileName="${attachment.Name}"
                                        data-Extension="${attachment.Extension}" class="btn btn-icon btn-sm btn-danger file float-end"><i
                                            class="fas fa-times"></i></a>
                                </div>
                                <img src="${attachment.URL}" class="px-2 mb-1 img-fluid align-items-center mx-auto"
                                    style="height:80px;">
                                    <div class="card-body">
                                        <div class="mb-2">
                                            <h5 class="card-title">${attachment.Name.substring(0, 15)}${attachment.Extension}</h5>
                                        </div>
                                    </div>
                                    <div class="mt-auto mx-3 mb-3">
                                        <a href="${attachment.URL}" target="_blank" class="btn btn-sm btn-default" download="${attachment.Name}">
                                            <i class="fas fa-download me-1"></i>Download
                                        </a>
                                    </div>
                            </div>
                         </div>`
                    );
                    //Prepare preview for Images END  
                } else {
                    $('#TicketFilelist').append(
                        `<div class="col-lg-3 col-xl-2 col-md-3 col-sm-3 mb-2">
                            <div class="card text-center h-100">
                                <div class="my-2 mx-3">
                                    <a href="#" id="AttachmentID${i}" data-FileName="${attachment.Name}"
                                        data-Extension="${attachment.Extension}" class="btn btn-icon btn-sm btn-danger file float-end"><i
                                            class="fas fa-times"></i></a>
                                </div>
                                <div class="display-4 d-flex mb-1 align-items-center mx-auto sidebar-color" style="height:80px;">
                                    ${_attachmentIcon}
                                </div>
                                <div class="card-body">
                                    <div class="mb-2">
                                        <h5 class="card-title">${_attachmentName}${attachment.Extension}</h5>
                                    </div>
                                </div>
                                <div class="mt-auto mx-3 mb-3">
                                    <a href="${attachment.URL}" target="_blank" class="btn btn-sm btn-default" download="${attachment.Name}">
                                        <i class="fas fa-download me-1"></i>Download
                                    </a>
                                </div>
                            </div>
                        </div>`
                    );
                }
            }
        });
        let _files = document.querySelectorAll(".file");
        _files.forEach(function (_file) {
            _file.addEventListener('click', function () {
                ShowDeleteAttachmentQuestion({ ID: $("#hiddenTicketID").val(), Name: this.dataset.filename, Extension: this.dataset.extension });
            });
        });
    }
}


async function UploadAttachment(FileDTO) {
    await dxLoadPanel.show();
    const _validation_resultDTO = await UploadTicketFile(FileDTO)
    HostResponse(_validation_resultDTO);
    $("#dxTicketFileUploader").dxFileUploader("instance").reset();
    await GetFileList();
    dxLoadPanel.hide()
}

async function ShowDeleteAttachmentQuestion(FileDTO) {
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
        DeleteAttachmentTicket(FileDTO);
    }
}

async function DeleteAttachmentTicket(FileDTO) {
    await dxLoadPanel.show();
    const _validation_resultDTO = await DeleteTicketFile(FileDTO);
    HostResponse(_validation_resultDTO);
    GetFileList(FileDTO);
    dxLoadPanel.hide()
}
//#endregion

//#region SparePart Usage
async function InitializeSparePartUsageControls() {
    let _ticketID = document.getElementById("hiddenTicketID").value;
    $("#dxSparePart_LotSelectBox").dxSelectBox({
        valueExpr: "ID",
        displayExpr: "LotSerialWithSparePartName",
        deferRendering: false,
        readOnly: true,
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await GetSparePart_LotAvailableQty(e.value);
            }
            else {
                await ClearSparePart_LotFields();
            }
        },
        itemTemplate(data) {
            // Show Name with image in SelectBox
            return '<div><img with="80"  height="80" src="' +
                data.SparePartDTO.SparePartImage + '" /> ' + data.LotSerialWithSparePartName + '</div>';

        }
    });
    $("#dxSparePartUsageQuantityNumberBox").dxNumberBox({
        format: '#0',
        min: 0,
        showSpinButtons: true,
        showClearButton: true,
        value: 0,
    });
    if (_ticketID != null && _ticketID != undefined && _ticketID != 0) {
        $("#dxSparePartUsageGrid").dxDataGrid({
            dataSource: await GetDXSparePartUsageDataSource({ TicketDTO: { ID: _ticketID }, SparePartInventoryDTO: { GetSparePartDTO: true, SparePartDTO: { GetImage: true } }, GetSparePartInventoryDTO: true, GetSparePart_LotDTO: true }),
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
                fileName: "SparePartUsage",
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
                                    $("#hiddenSparePartUsageID").val(options.data.ID);
                                    ShowSparePartDeleteQuestion();
                                }).appendTo(container);
                        },
                    },
                    {
                        caption: 'Spare Part image',
                        width: 'auto',
                        allowFiltering: false,
                        allowSorting: false,
                        cellTemplate(container, options) {
                            if (options.data.SparePartInventoryDTO != null && options.data.SparePartInventoryDTO.SparePartDTO != null) {
                                $('<div>')
                                    .append($('<img>', { src: options.data.SparePartInventoryDTO.SparePartDTO.SparePartImage, height: 120, width: 120 }))
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
                        caption: "Lot #",
                        dataField: "SparePart_LotDTO.Serial",
                        visible: true
                    },
                    {
                        caption: "Spare Part",
                        dataField: "SparePartInventoryDTO.SparePartDTO.Name"
                    },
                    {
                        caption: "Description",
                        dataField: "SparePartInventoryDTO.SparePartDTO.Description"
                    },
                    {
                        caption: "Quantity",
                        dataField: "Quantity"
                    },
                    {
                        caption: "Is Active",
                        dataField: "IsActive",
                        visible: false
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
                        dataField: "LastUpdateByName",
                        visible: false
                    },
                    {
                        caption: "Last Update",
                        dataField: "LastUpdate",
                        dataType: 'datetime',
                        visible: false
                    },


                ],
        });
    }
}


async function GetSparePart_LotAvailableQty(SparePart_LotID) {
    let _sparePart_LotDTO = {
        ID: SparePart_LotID
    };
    const _sparePart_LotInformation = await GetSparePart_LotInformation(_sparePart_LotDTO);
    await PopulateSparePart_LotFields(_sparePart_LotInformation[0]);
}


async function PopulateSparePart_LotFields(SparePart_LotDTO) {
    if (SparePart_LotDTO != null) {
        document.getElementById('sparePart_LotAvailableQty').innerText = `/ ${SparePart_LotDTO.AvailableQty}`
        $("#dxSparePartUsageQuantityNumberBox").dxNumberBox("instance").option('max', SparePart_LotDTO.AvailableQty);
        $("#dxSparePartUsageQuantityNumberBox").dxNumberBox("instance").option('value', 0);
        $("#hiddenSparePartInventoryID").val(SparePart_LotDTO.SparePartInventoryDTO.ID);
    } else {
        await ClearSparePart_LotFields()
    }
}

async function ClearSparePart_LotFields() {
    await $("#dxSparePart_LotSelectBox").dxSelectBox("instance").reset();
    $("#dxSparePartUsageQuantityNumberBox").dxNumberBox("instance").reset();
    $("#dxSparePartUsageQuantityNumberBox").dxNumberBox("instance").option('value', 0);
    $("#hiddenSparePartInventoryID").val("0");
    document.getElementById('sparePart_LotAvailableQty').innerText = `/ 0`
}

async function GetSparePartUsageDTO() {
    let _sparePartUsageDTO = {
        ID: document.getElementById("hiddenSparePartUsageID").value,
        TicketDTO: {
            ID: $("#hiddenTicketID").val()
        },
        SparePartInventoryDTO: {
            ID: $("#hiddenSparePartInventoryID").val()
        },
        SparePart_LotDTO: {
            ID: $("#dxSparePart_LotSelectBox").dxSelectBox("instance").option("value")
        },
        Item_LineDTO: {
            ID: $("#dxTicketItemLookup").dxLookup("instance").option("value")
        },
        Quantity: $("#dxSparePartUsageQuantityNumberBox").dxNumberBox("instance").option('value'),
        IsActive: true
    };
    return _sparePartUsageDTO;
}


async function CreateSparePartUsage_Global() {
    await dxLoadPanel.show();
    //clear always SparePartUsage ID when create a new record
    $('#hiddenSparePartUsageID').val(0);
    const _sparePartUsageDTO = await GetSparePartUsageDTO()
    const _validation_ResultDTO = await CreateSparePartUsage(_sparePartUsageDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePart_LotFields();
        $('#dxSparePartUsageGrid').dxDataGrid("instance").refresh();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}


async function ShowSparePartDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this SparePart',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSparePartUsage_Global();
    } else {

    }
}


async function DeleteSparePartUsage_Global() {
    await dxLoadPanel.show();
    const _SparePartUsageDTO = await GetSparePartUsageDTO();
    const _validation_ResultDTO = await DeleteSparePartUsage(_SparePartUsageDTO);
    if (_validation_ResultDTO.Result) {
        ClearSparePart_LotFields();
        $('#dxSparePartUsageGrid').dxDataGrid("instance").refresh();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion