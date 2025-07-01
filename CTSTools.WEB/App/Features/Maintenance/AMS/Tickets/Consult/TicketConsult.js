import { GetUserInformation } from '../../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetItem_LineInformationBySupportGroup } from '../../ItemManagement/Item_Line/Item_Line_Service.js'
import { dxLoadPanel } from '../../../../../Common/Components/dxLoadPanel.js'
import { GetDXFacilityDataSource } from '../../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXSupportGroupDataSource } from '../../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetDXStatus_StatusTypeDataSource } from '../../../../AdvancedSettings/StatusManagement/Status_StatusType/Status_StatusType_Service.js'
import { StatusType_Enum } from '../../../../AdvancedSettings/StatusManagement/StatusType/StatusType_Enum.js'
import { GetDXPriorityDataSource } from '../Priority/Priority_Service.js'
import { GetDXTicketDataSource } from '../Ticket/Ticket_Service.js'
import { Status_Enum } from '../../../../AdvancedSettings/StatusManagement/Status/Status_Enum.js'
import { Role_Enum } from '../../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'



document.addEventListener("DOMContentLoaded", async () => {
    await InitializeTicketConsultControls();
    await GetUserInformationbyID();
    await EventHandler();
    await GetTicketInformation();
});
function EventHandler() {
    document.getElementById("GetTicketInformation").addEventListener("click", GetTicketInformation);
    document.getElementById("ClearTicketFilters").addEventListener("click", ClearTicketFields);
    document.getElementById("OpenFilterTicket").addEventListener("click", function () {
        BuildFilterAppliedToTicketDataGrid();
        document.getElementById("FiltersTicketApply").classList.toggle('hideFilter');
    });
}

async function GetUserInformationbyID() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _userInformation = await GetUserInformation(_userDTO);
    await PopulateSupportGroupField(_userInformation[0]);
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

async function PopulateSupportGroupField(UserDTO) {
    if (UserDTO.SupportGroupIDArray != null || UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
        $("#dxTicketSupportGroupTagBox").dxTagBox("instance").option("value", UserDTO.SupportGroupIDArray);
        $("#ItemContainer").removeAttr("hidden");
    }
}

async function InitializeTicketConsultControls() {
    const now = new Date();
    $("#dxTicketStartDateDateBox").dxDateBox({
        type: "date",
        format: "MM/dd/yyyy",
        value: new Date(now.getFullYear(), now.getMonth(), 1),
        onValueChanged: function (e) {
            $("#dxTicketEndDateDateBox").dxDateBox("instance").option("min", e.value);
        }
    });
    $("#dxTicketEndDateDateBox").dxDateBox({
        type: "date",
        format: "MM/dd/yyyy",

        value: new Date(now.getFullYear(), now.getMonth() + 1, 1),
        onValueChanged: function (e) {
            $("#dxTicketStartDateDateBox").dxDateBox("instance").option("max", e.value);
        }
    });
    $("#dxTicketNumberNumberBox").dxNumberBox({
        format: '#',
    });
    $("#dxTicketFacilityTagBox").dxTagBox({
        dataSource: await GetDXFacilityDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
    });
    $("#dxTicketSupportGroupTagBox").dxTagBox({
        dataSource: await GetDXSupportGroupDataSource({ IsActive: true }),
        displayExpr: "EnglishName",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchExpr: ["EnglishName"],
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
        onValueChanged: async function (e) {
            ResetSupportGroupFields();
            if (e.value != 0 && e.value != null) {
                await DisabledSupportGroupFields(e.value.length);
                await InitializePriorityDataSource(e.value);
                if (e.value.length == 1) {
                    await InitializeItemDataSource(e.value);
                }
            }
            else {
                DisabledSupportGroupFields()
            }
        }
    });
    $("#dxTicketPriorityTagBox").dxTagBox({
        dataSource: [],
        displayExpr: "PriorityWithSupportGroup",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchExpr: ["Name"],
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
        readOnly: true
    });
    $("#dxTicketItemTagBox").dxTagBox({
        dataSource: [],
        displayExpr: "ItemHeaderNameWithPartNumberSerial",
        valueExpr: "ID",
        showSelectionControls: true,
        searchEnabled: true,
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
        readOnly: true
    });
    $("#dxTicketStatusTagBox").dxTagBox({
        dataSource: await GetDXStatus_StatusTypeDataSource({ StatusTypeID: StatusType_Enum.Tickets }),
        valueExpr: "StatusDTO.ID",
        displayExpr: "StatusDTO.Name",
        showSelectionControls: true,
        searchEnabled: true,
        searchMode: "contains",
        popupWidth: 450,
        deferRendering: false,
    });
    $("#dxTicketGrid").dxDataGrid({
        dataSource: [],
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
        showColumnLines: true,
        showBorders: true,
        focusedRowEnabled: false,
        hoverStateEnabled: false,
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
            fileName: "TicketConsult",
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
        //onCellPrepared: function (e) {
        //    if (e.rowType == "data" && e.column.dataField === 'StatusName') {
        //        e.cellElement.css("color", "black");
        //        e.cellElement.css("text-align", "center");
        //        //e.cellElement.css("text-transform", "uppercase");
        //        switch (e.data.StatusID) {
        //            case Status_Enum.Active:
        //                e.cellElement.addClass("bg-green");
        //                break;
        //            case Status_Enum.In_Progress:
        //                e.cellElement.addClass("bg-orange");
        //                break;
        //            case Status_Enum.Closed:
        //                e.cellElement.addClass("bg-gray");
        //                break;
        //        }
        //    }
        //},
        onSelectionChanged: function (data) {
        },
        columns:
            [
                {
                    caption: "View Ticket",
                    //visibleIndex: 1,
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        container.height(30);

                        $('<button type="button"  class="btn btn-primary ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fas fa-eye"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                let _data = options.data;
                                if (options.data.ID != null && options.data.ID > 0) {
                                    // Open the label
                                    window.open('/App/Features/Maintenance/AMS/Tickets/ViewTicket/ViewTicket.aspx?TicketID=' + _data.ID);
                                }
                            }).appendTo(container);
                    }
                },
                {
                    caption: "Support Group",
                    dataField: "SupportGroupEnglishName",
                    //groupIndex: 0
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive",
                    visible: false
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Ticket #",
                    dataField: "TicketNumber"
                },
                {
                    caption: "Status",
                    dataField: "StatusName",
                },
                {
                    caption: "Priority",
                    dataField: "PriorityName"
                },
                {
                    caption: "Item",
                    dataField: "Item_LineDTO.ItemNameWithManufactureSerial",
                    calculateSortValue: "Item_LineDTO.Item_HeaderDTO.EnglishName",
                    calculateFilterExpression: function (filterValue, selectedFilterOperation, target) {
                        // Build Filter Search Expression 
                        return [["Item_LineDTO.Item_HeaderDTO.EnglishName", "contains", filterValue],
                            "or",
                        ["Item_LineDTO.ManufactureSerialID", "contains", filterValue]];
                    }
                },
                {
                    caption: "Title",
                    dataField: "Title"
                },
                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Note",
                    dataField: "Note",
                    visible: false
                },
                {
                    caption: "Solution",
                    dataField: "Solution"
                },
                {
                    caption: "Resolution",
                    dataField: "Resolution"
                },
                {
                    caption: "Category",
                    dataField: "CategoryName"
                },
                {
                    caption: "Subcategory",
                    dataField: "SubCategoryName",
                    visible: false
                },
                {
                    caption: "Third Level Category",
                    dataField: "ThirdLevelCategoryName",
                    visible: false
                },
                {
                    caption: "Facility",
                    dataField: "FacilityName"
                },
                {
                    caption: "Department",
                    dataField: "DepartmentName",
                    visible: false
                },
                {
                    caption: "Assigned To",
                    dataField: "AssignedToName"
                },
                {
                    caption: "Created Date",
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
                    dataField: "LastUpdateByName",
                    visible: false
                },
                {
                    caption: "Last Update",
                    dataField: "LastUpdate",
                    dataType: 'datetime'
                },
                {
                    caption: "Closed Date",
                    dataField: "ClosedDate",
                    dataType: 'datetime'
                },
                {
                    caption: "Closed By",
                    dataField: "ClosedByName",
                },


            ],
    });
}

function GetTicketDTO() {
    let _TicketDTO = {
        FacilityIDArray: $("#dxTicketFacilityTagBox").dxTagBox("instance").option("value"),
        TicketNumber: $("#dxTicketNumberNumberBox").dxNumberBox("instance").option("value"),
        SupportGroupIDArray: $("#dxTicketSupportGroupTagBox").dxTagBox("instance").option("value"),
        PriorityIDArray: $("#dxTicketPriorityTagBox").dxTagBox("instance").option("value"),
        Item_LineIDArray: $("#dxTicketItemTagBox").dxTagBox("instance").option("value"),
        StatusIDArray: $("#dxTicketStatusTagBox").dxTagBox("instance").option("value"),
        StartAddedDate: $("#dxTicketStartDateDateBox").dxDateBox("instance").option("value").toJSON(),
        EndAddedDate: $("#dxTicketEndDateDateBox").dxDateBox("instance").option("value").toJSON(),
        IsActive: true,
        Item_LineDTO: {
            GetItem_HeaderDTO: true
        },
        GetItem_LineDTO: true
    }
    return _TicketDTO;
}
function ClearTicketFields() {
    const now = new Date();
    $("#dxTicketFacilityTagBox").dxTagBox("instance").reset()
    $("#dxTicketSupportGroupTagBox").dxTagBox("instance").reset()
    $("#dxTicketPriorityTagBox").dxTagBox("instance").reset()
    $("#dxTicketItemTagBox").dxTagBox("instance").reset()
    $("#dxTicketStatusTagBox").dxTagBox("instance").reset()
    $("#dxTicketNumberNumberBox").dxNumberBox("instance").reset()
    $("#dxTicketStartDateDateBox").dxDateBox("instance").option("value", new Date(now.getFullYear(), now.getMonth(), 1));
    $("#dxTicketEndDateDateBox").dxDateBox("instance").option("value", new Date(now.getFullYear(), now.getMonth() + 1, 1));
}
async function GetTicketInformation() {
    await dxLoadPanel.show()
    let _TicketDTO = await GetTicketDTO();
    await BuildFilterAppliedToTicketDataGrid();
    let _ds = await GetDXTicketDataSource(_TicketDTO);
    $("#dxTicketGrid").dxDataGrid("instance").option("dataSource", _ds);
    await dxLoadPanel.hide();
}

async function BuildFilterAppliedToTicketDataGrid() {
    let _filterSection = document.getElementById('FiltersTicketApply');
    _filterSection.innerHTML = "";

    if ($("#dxTicketStartDateDateBox").dxDateBox('instance').option("value") != null && $("#dxTicketEndDateDateBox").dxDateBox('instance').option("value")) {
        _filterSection.innerHTML +=
            '<div class=\"ms-2 bg-filter text-white rounded-5 mb-1 p-1 px-3\">' +
            '<i class=\"fas fa-calendar-days me-2 text-white\"></i>Requested Date from ' +
            $("#dxTicketStartDateDateBox").dxDateBox("instance").option("value").toLocaleDateString("en-US") +
            ' to ' +
            $("#dxTicketEndDateDateBox").dxDateBox("instance").option("value").toLocaleDateString("en-US") +
            '</div>';
    }

    if ($("#dxTicketNumberNumberBox").dxNumberBox("instance").option("value") != null && $("#dxTicketNumberNumberBox").dxNumberBox("instance").option("value") != 0) {
        let _supplyTypeSelected = $("#dxTicketNumberNumberBox").dxNumberBox("instance").option("value");
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 p-1 px-3\"> <i class=\"fas fa-hashtag me-2 text-white\"></i>Ticket #" + _supplyTypeSelected + "</div>";
    }

    if ($("#dxTicketFacilityTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _facilityApply = "";
        let _facilitySelected = $("#dxTicketFacilityTagBox").dxTagBox("instance").option("selectedItems");
        _facilitySelected.forEach(function (item, index, arr) {
            if (index > 0) {
                _facilityApply += ", " + item.Name;
            } else {
                _facilityApply += item.Name;
            }
        });
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 mb-1 p-1 px-3\"> <i class=\"fas fa-location-dot me-2 text-white\"></i>Facility as " + _facilityApply + "</div>";
    }
    if ($("#dxTicketSupportGroupTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _supportGroupCounter = 0;
        let _supportGroupApply = "";
        let _supportGroupSelected = $("#dxTicketSupportGroupTagBox").dxTagBox("instance").option("selectedItems");
        _supportGroupSelected.forEach(function (item, index, arr) {
            _supportGroupCounter++;
            if (index > 0) {
                _supportGroupApply += ", " + item.EnglishName;
            } else {
                _supportGroupApply += item.EnglishName;
            }
            if (_supportGroupCounter == 5 || index == arr.length - 1) {
                _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 mb-1 p-1 px-3\"> <i class=\"fas fa-user-group me-2 text-white\"></i>Support Group as " + _supportGroupApply + "</div>";
                _supportGroupCounter = 0;
                _supportGroupApply = "";
            }
        });

    }
    if ($("#dxTicketPriorityTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _priorityCounter = 0;
        let _priorityApply = "";
        let _prioritySelected = $("#dxTicketPriorityTagBox").dxTagBox("instance").option("selectedItems");
        _prioritySelected.forEach(function (item, index, arr) {
            _priorityCounter++;
            if (index > 0) {
                _priorityApply += ", " + item.PriorityWithSupportGroup;
            } else {
                _priorityApply += item.PriorityWithSupportGroup;
            }
            if (_priorityCounter == 5 || index == arr.length - 1) {
                _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 mb-1 p-1 px-3\"> <i class=\"fas fa-filter me-2 text-white\"></i>Priority as " + _priorityApply + "</div>";
                _priorityCounter = 0;
                _priorityApply = "";
            }
        });

    }
    if ($("#dxTicketItemTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _itemApply = "";
        let _itemSelected = $("#dxTicketItemTagBox").dxTagBox("instance").option("selectedItems");
        _itemSelected.forEach(function (item, index, arr) {
            if (index > 0) {
                _itemApply += ", " + item.Name;
            } else {
                _itemApply += item.Name;
            }
        });
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-white rounded-5 mb-1 p-1 px-3\"> <i class=\"fas fa-filter me-2 text-white\"></i>Item as " + _itemApply + "</div>";
    }
    if ($("#dxTicketStatusTagBox").dxTagBox("instance").option("selectedItems").length > 0) {
        let _statusApply = "";
        let _statusSelected = $("#dxTicketStatusTagBox").dxTagBox("instance").option("selectedItems");
        _statusSelected.forEach(function (item, index, arr) {
            if (index > 0) {
                _statusApply += ", " + item.StatusDTO.Name;
            } else {
                _statusApply += item.StatusDTO.Name;
            }
        });
        _filterSection.innerHTML += "<div class=\"ms-2 bg-filter text-black rounded-5 mb-1 p-1 px-3\"> <i class=\"fas fa-filter me-2 text-black\"></i>Status as " + _statusApply + "</div>";
    }
}

//#region support group fields
async function ResetSupportGroupFields() {
    $("#dxTicketPriorityTagBox").dxTagBox("instance").reset()
    $("#dxTicketItemTagBox").dxTagBox("instance").reset()
}

async function DisabledSupportGroupFields(SupportGroupLength) {
    if (SupportGroupLength == 1) {
        $("#dxTicketPriorityTagBox").dxTagBox("instance").option("readOnly", false);
        $("#dxTicketItemTagBox").dxTagBox("instance").option("readOnly", false);
    } else if (SupportGroupLength > 1) {
        $("#dxTicketPriorityTagBox").dxTagBox("instance").option("readOnly", false);
        $("#dxTicketItemTagBox").dxTagBox("instance").option("readOnly", true);
    } else {
        $("#dxTicketPriorityTagBox").dxTagBox("instance").option("readOnly", true);
        $("#dxTicketItemTagBox").dxTagBox("instance").option("readOnly", true);
    }
}

async function InitializePriorityDataSource(SupportGroupIDArray) {
    let _priorityDTO = {
        SupportGroupIDArray: SupportGroupIDArray,
        IsActive: true
    };
    await $("#dxTicketPriorityTagBox").dxTagBox("instance").option("dataSource", await GetDXPriorityDataSource(_priorityDTO));
}

async function InitializeItemDataSource(SupportGroupIDArray) {
    let _item_lineDTO = {
        Item_SupportGroupDTO: {
            SupportGroupIDArray: SupportGroupIDArray
        }
    };
    await $("#dxTicketItemTagBox").dxTagBox("instance").reset();
    //await $("#dxTicketItemLookup").dxLookup("instance").option("readOnly", false);
    await $("#dxTicketItemTagBox").dxTagBox("instance").option("dataSource", await GetItem_LineInformationBySupportGroup(_item_lineDTO));
}

//#endregion