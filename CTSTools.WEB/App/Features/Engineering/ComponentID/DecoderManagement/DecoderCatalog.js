import { GetDXDecoderDataSource, CreateDecoder, UpdateDecoder, DeleteDecoder } from './Decoder/Decoder_Service.js'
import { GetDXValueLinkDataSource } from '../AttributeManagement/ValueLink/ValueLink_Service.js'
import { GetDXValueDataSource } from '../AttributeManagement/Value/Value_Service.js'
import { Attributes } from '../AttributeManagement/Attribute/Attribute_Enum.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';
import { Value_Enum } from '../AttributeManagement/Value/Value_Enum.js'


//#region Decoder Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeDecoderCatalogControls();
});
async function InitializeDecoderCatalogControls() {
    $("#dxDecoderPartTypeLookup").dxLookup({
        dataSource: await GetPartTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                if (e.value == Value_Enum.Manufactured) {
                    $("#dxDecoderComponentTypeLookup").dxLookup("instance").reset();
                    $("#dxDecoderClassLookup").dxLookup("instance").reset();
                    document.getElementById("ComponentTypeGroup").hidden = false;
                    await $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("dataSource", await GetComponentTypeDataSource_Global());
                }
                else {
                    $("#dxDecoderComponentTypeLookup").dxLookup("instance").reset();
                    $("#dxDecoderClassLookup").dxLookup("instance").reset();
                    document.getElementById("ComponentTypeGroup").hidden = true;
                    await $("#dxDecoderClassLookup").dxLookup("instance").option("dataSource", await GetClassFromPartTypeDataSource_Global());
                }
            }
            else {
                $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxDecoderComponentTypeLookup").dxLookup({
        //dataSource: GetPartTypeDataSource_Global(),
        displayExpr: "ChildValueName",
        valueExpr: "ChildValueID",
        searchEnable: true,
        onValueChanged: async function (e) {
            $("#dxDecoderClassLookup").dxLookup("instance").reset();
            if (e.value != 0 && e.value != null) {
                await $("#dxDecoderClassLookup").dxLookup("instance").option("dataSource", await GetClassFromComponentTypeDataSource_Global());
            }
            else {
                $("#dxDecoderClassLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxDecoderClassLookup").dxLookup({
        displayExpr: "ChildValueName",
        valueExpr: "ChildValueID",
        searchEnable: true,
        onValueChanged: async function (e) {
            $("#dxDecoderSubClassLookup").dxLookup("instance").reset();
            if (e.value != 0 && e.value != null) {
                await $("#dxDecoderSubClassLookup").dxLookup("instance").option("dataSource", await GetSubClassDataSource_Global());
            }
            else {
                $("#dxDecoderSubClassLookup").dxLookup("instance").option("dataSource", []);
            }

        }
    });
    $("#dxDecoderSubClassLookup").dxLookup({
        displayExpr: "ChildValueName",
        valueExpr: "ChildValueID",
        searchEnable: true,
    });
    $("#dxDecoderDescriptionTextArea").dxTextArea({
        placeholder: 'Type a description...'
    });
    $("#dxDecoderGrid").dxDataGrid({
        dataSource: await GetDXDecoderDataSource(),
        remoteOperations: true,
        keyExpr: "ID",
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        columnMinWidth: "auto",
        showRowLines: true,
        showColumnLines: false,
        showBorders: true,
        focusedRowEnabled: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: false,
        allowColumnDragging: true,
        columnAutoWidth: true,
        groupPanel: {
            visible: "auto",

        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "DecoderCatalog",
            allowExportSelectedData: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: true,
            placeholder: "Search...",
            width: "auto"
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
            let _decoderData = data.selectedRowsData[0];
            if (_decoderData != null) {
                DecoderActionButtons("Update");
                PopulateDecoderFields(_decoderData);
            }
        },
        columns:
            [
                {
                    caption: "View",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 70,
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" class="btn btn-primary" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa-solid fa-eye"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                window.location.href = window.location.origin + '/App/Features/Engineering/ComponentID/DecoderManagement/DecoderConfigurator.aspx?DecoderID=' + options.data.ID;
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Sub Class", dataField: "SubClassName" },
                { caption: "Class", dataField: "ClassName" },
                { caption: "Component Type", dataField: "ComponentTypeName" },
                { caption: "Part Type", dataField: "PartTypeName" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added Date", dataField: "AddedDate", dataType: "datetime" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update", dataField: "LastUpdate", dataType: "datetime" },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    DecoderActionButtons("Save");
}
async function PopulateDecoderFields(DecoderDTO) {
    $("#hiddenDecoderID").val(data.ID);
    $("#dxDecoderStatusLookup").dxLookup("instance").option("value", data.StatusDTO.ID);
    $("#DecoderDescription").val(data.Description);
    $("#dxDecoderIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteDecoderQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the decoder, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDecoder_Global(_decoderDTO);
    } else {
        ClearDecoderFields();
    }
}
function DecoderActionButtons(Action) {
    $("#DecoderActionButtons").empty();
    document.getElementById('DecoderModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById('DecoderModalTitle').innerText = 'Add Decoder';
        document.getElementById("DecoderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateDecoderButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateDecoderButton").addEventListener("click", CreateDecoder_Global);
    }
    else {
        // Update
        document.getElementById('DecoderModalTitle').innerText = 'Update Decoder';
        document.getElementById("DecoderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearDecoderButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateDecoderButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearDecoderButton").addEventListener("click", ClearDecoderFields);
        document.getElementById("UpdateDecoderButton").addEventListener("click", UpdateDecoder_Global);
    }
}
function ClearDecoderFields() {
    DecoderActionButtons("Save");
    $("#hiddenDecoderID").val("");
    $("#dxDecoderSubClassLookup").dxLookup("instance").reset();
    $("#dxDecoderClassLookup").dxLookup("instance").reset();
    $("#dxDecoderComponentTypeLookup").dxLookup("instance").reset();
    $("#dxDecoderPartTypeLookup").dxLookup("instance").reset();
    //$("#DecoderDescription").val("");

    let keys = $("#dxDecoderGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDecoderGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetDecoderDTO() {
    let _decoderDTO = {
        SubClassID: $("#dxDecoderSubClassLookup").dxLookup("instance").option("value"),
        ClassID: $("#dxDecoderClassLookup").dxLookup("instance").option("value"),
        ComponentTypeID: $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("value"),
        PartTypeID: $("#dxDecoderPartTypeLookup").dxLookup("instance").option("value"),
        IsActive: true,
        //Description: $("#DecoderDescription").val(),

    }
    return _decoderDTO;
}


//#region Decoder CRUD Functions
async function CreateDecoder_Global() {
    await dxLoadPanel.show();
    const _decoderDTO = GetDecoderDTO();
    const _validation_resultDTO = await CreateDecoder(_decoderDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDecoderGrid").dxDataGrid("instance").refresh();
        ClearDecoderFields();
        window.location.href = window.location.origin + '/App/Features/Engineering/ComponentID/DecoderManagement/DecoderConfigurator.aspx?DecoderID=' + _validation_resultDTO.Data;
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateDecoder_Global() {
    await dxLoadPanel.show();
    const _decoderDTO = GetDecoderDTO();
    const _validation_resultDTO = await UpdateDecoder(_decoderDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxDecoderGrid").dxDataGrid("instance").refresh();
        ClearDecoderFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function GetPartTypeDataSource_Global() {
    let _partTypeDTO = {
        IsActive: true,
        AttributeID: Attributes.PartType
    }
    return await GetDXValueDataSource(_partTypeDTO)
}
async function GetComponentTypeDataSource_Global() {
    let _componentTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.PartType,
        ParentValueID: $("#dxDecoderPartTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.ComponentType
    }
    return await GetDXValueLinkDataSource(_componentTypeDTO)
}
async function GetClassFromComponentTypeDataSource_Global() {
    let _classTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.ComponentType,
        ParentValueID: $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.Class
    }
    return await GetDXValueLinkDataSource(_classTypeDTO)
}
async function GetClassFromPartTypeDataSource_Global() {
    let _classTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.PartType,
        ParentValueID: $("#dxDecoderPartTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.Class
    }
    return await GetDXValueLinkDataSource(_classTypeDTO)
}
async function GetSubClassDataSource_Global() {
    let _subClassTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.Class,
        ParentValueID: $("#dxDecoderClassLookup").dxLookup("instance").option("value"),
        ChildAttributeID: Attributes.SubClass
    }
    return await GetDXValueLinkDataSource(_subClassTypeDTO)
}

//#endregion