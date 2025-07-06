
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';
import { GetDXAttributeDataSource } from '../AttributeManagement/Attribute/Attribute_Service.js'
import { GetDXValueDataSource, GetValueInformation } from '../AttributeManagement/Value/Value_Service.js'
import {
    GetDXSubClass_SupplierDataSource,
    CreateSubClass_Supplier,
    DeleteSubClass_Supplier,
    UpdateSubClass_Supplier
} from './SubClass_Supplier/SubClass_Supplier.js'
import { GetDXSupplierDataSource } from '../SupplierManagement/Supplier/Supplier_Service.js'
import {
    GetDXDecoderStructureDataSource,
    GetDecoderStructureInformation,
    CreateDecoderStructure,
    DeleteDecoderStructure,
    UpdateDecoderStructure,
    UpdateNumberOrder,
    UpdateDescriptionOrder
} from './DecoderStructure/DecoderStructure_Service.js'
import {
    DeleteDecoder,
    GetDecoderInformation,
    SubmitDecoder,
    EditDecoder
} from './Decoder/Decoder_Service.js'
import { GetValueLinkInformation } from '../AttributeManagement/ValueLink/ValueLink_Service.js'
import { Attributes } from '../AttributeManagement/Attribute/Attribute_Enum.js'
import {
    HostResponse,
    ClearErrorFeedback
} from '../../../../Common/Utils/Response.js'
import { GetURLParameter } from '../../../../Common/Utils/Utils.js'
import { Value_Enum } from '../AttributeManagement/Value/Value_Enum.js';
import { Status_Enum } from '../../../AdvancedSettings/StatusManagement/Status/Status_Enum.js';

//#region Decoder Behavior Functions
document.addEventListener("DOMContentLoaded", async () => {
    await PopulateDecoderConfigurator();
    await InitializeDecoderStructureControls();
    await InitializeSubClass_SupplierControls();
    document.getElementById("AddDescriptionAttributeButton").addEventListener("click", ShowDecoderConfiguratorDescriptionModal);
    //document.getElementById("AddNumberAttributeButton").addEventListener("click", ShowDecoderConfiguratorNumberModal);
    document.getElementById("SubmitDecoderButton").addEventListener("click", ShowSubmitDecoderQuestion);
});

//#region Decoder 
async function PopulateDecoderConfigurator() {
    const _decoderID = GetURLParameter("DecoderID");
    if (_decoderID != null) {
        let _decoderDTO = { ID: _decoderID }
        _decoderDTO = await GetDecoderInformation(_decoderDTO);
        $("#hiddenDecoderID").val(_decoderDTO[0].ID);
        $("#DecoderSubClassName").html(_decoderDTO[0].SubClassName);
        $("#hiddenDecoderSubClassID").val(_decoderDTO[0].SubClassID);
        $("#DecoderClassName").html(_decoderDTO[0].ClassName);
        $("#DecoderComponentTypeName").html(_decoderDTO[0].ComponentTypeName);
        $("#DecoderPartTypeName").html(_decoderDTO[0].PartTypeName);
        //$("#DecoderStatusName").html(_decoderDTO[0].StatusName);
        $("#DecoderAddedBy").html(_decoderDTO[0].AddedByName);
        $("#DecoderLastUpdateBy").html(_decoderDTO[0].LastUpdateByName);
        $("#DecoderAddedDate").html(_decoderDTO[0].AddedDate.replace("T", " "));
        $("#DecoderLastUpdate").html(_decoderDTO[0].LastUpdate == null ? "Unnassigned" : _decoderDTO[0].LastUpdate.replace("T", " "));
        document.getElementById("DeleteDecoderButton").addEventListener("click", ShowDeleteDecoderQuestion);
        if (_decoderDTO[0].PartTypeID == Value_Enum.Manufactured)
            document.getElementById("ComponentTypeGroup").hidden = false;
        let statusHTML = "";
        if (_decoderDTO[0].StatusID == Status_Enum.Draft ) {
            document.getElementById("SubmitDecoderButton").hidden = false;
            document.getElementById("DeleteDecoderButton").hidden = false;  
            document.getElementById("EditDecoderButton").hidden = true;    
            document.getElementById("StatusGroup").innerHTML = ' <span style="font-weight: normal !important; font-size: 14px" class="badge text-bg-default form-label col-form-label col-xl-9 mt-2  col-md-12" id="DecoderStatusName"><b>' + _decoderDTO[0].StatusName + '</b></span>';
        }
        if (_decoderDTO[0].StatusID == Status_Enum.Editing) {
            document.getElementById("SubmitDecoderButton").hidden = false;
            document.getElementById("DeleteDecoderButton").hidden = false;
            document.getElementById("EditDecoderButton").hidden = true;
            document.getElementById("StatusGroup").innerHTML = ' <span style="font-weight: normal !important; font-size: 14px" class="badge text-bg-primary form-label col-form-label col-xl-9 mt-2  col-md-12" id="DecoderStatusName"><b>' + _decoderDTO[0].StatusName + '</b></span>';
        }
       
        if (_decoderDTO[0].StatusID == Status_Enum.Released) {
            document.getElementById("SubmitDecoderButton").hidden = true;
            document.getElementById("DeleteDecoderButton").hidden = true;
            document.getElementById("EditDecoderButton").hidden = false;
            document.getElementById("EditDecoderButton").addEventListener("click", ShowEditDecoderQuestion);            
            document.getElementById("StatusGroup").innerHTML = ' <span style="font-weight: normal !important; font-size: 14px" class="badge text-bg-success form-label col-form-label col-xl-9 mt-2  col-md-12" id="DecoderStatusName"><b>' + _decoderDTO[0].StatusName + '</b></span>';
        }

   

    }
}
async function ShowDeleteDecoderQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You are going to delete this decoder',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDecoder_Global();
    }
}
async function ShowEditDecoderQuestion() {
    const _alert = await Swal.fire({
        icon: 'info',
        title: 'Are you sure?',
        text: 'You are going to edit this decoder',
        confirmButtonText: `Confirm`,
        confirmButtonColor: '#3085d6',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        EditDecoder_Global();
    }
}
async function ShowSubmitDecoderQuestion() {
    const _alert = await Swal.fire({
        icon: 'info',
        title: 'Are you sure?',
        text: 'You are going to submit this decoder',
        confirmButtonText: `Confirm`,
        confirmButtonColor: '#3085d6',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        SubmitDecoder_Global();

    }
}
async function DeleteDecoder_Global() {
    await dxLoadPanel.show();
    const _decoderDTO = {
        ID: $("#hiddenDecoderID").val()
    };
    const _validation_resultDTO = await DeleteDecoder(_decoderDTO)
    if (_validation_resultDTO.Result)
        window.location.href = window.location.origin + '/App/Features/Engineering/ComponentID/DecoderManagement/DecoderCatalog.aspx'
    HostResponse(_validation_resultDTO);
    ClearDecoderFields();
    dxLoadPanel.hide();
}
async function SubmitDecoder_Global() {
    await dxLoadPanel.show();
    const _decoderDTO = {
        ID: $("#hiddenDecoderID").val(),
        SubClassID: $("#hiddenDecoderSubClassID").val()
    };
    const _validationResultDTO = await SubmitDecoder(_decoderDTO);
    if (_validationResultDTO.Result)
        PopulateDecoderConfigurator();
    else
        HostResponse(_validationResultDTO);
    dxLoadPanel.hide();
}
async function EditDecoder_Global() {
    await dxLoadPanel.show();
    const _decoderDTO = {
        ID: $("#hiddenDecoderID").val(),
        SubClassID: $("#hiddenDecoderSubClassID").val()
    };
    const _validationResultDTO = await EditDecoder(_decoderDTO);
    if (_validationResultDTO.Result)
        PopulateDecoderConfigurator();
    else
        HostResponse(_validationResultDTO);
    dxLoadPanel.hide();
}

//#endregion

//Decoder Structure
async function PopulateDecoderStructureFieldsFromNumber(DecoderStructureDTO) {
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("readOnly", false);
    $("#hiddenDecoderStructureID").val(DecoderStructureDTO.ID);
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("readOnly", true);
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("value", DecoderStructureDTO.AttributeID);
    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("value", DecoderStructureDTO.DescriptionBody);
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("value", DecoderStructureDTO.NumberBody);
    $("#DecoderConfiguratorNumberOrder").val(DecoderStructureDTO.NumberOrder);
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("readOnly", true);
    $("#dxDecoderStructureValueLookup").dxLookup("instance").option("value", DecoderStructureDTO.ValueID);
    $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").option("value", DecoderStructureDTO.ValueIDArray);

}
async function PopulateDecoderStructureFieldsFromDescription(DecoderStructureDTO) {
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("readOnly", true);
    $("#hiddenDecoderStructureID").val(DecoderStructureDTO.ID); 


    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("value", DecoderStructureDTO.DescriptionBody);
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("value", DecoderStructureDTO.NumberBody);
    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("readOnly", true)
    $("#DecoderConfiguratorDescriptionOrder").val(DecoderStructureDTO.DescriptionOrder);
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("value", DecoderStructureDTO.AttributeID);

    $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").option("value", DecoderStructureDTO.ValueIDArray);
    $("#dxDecoderStructureValueLookup").dxLookup("instance").option("value", DecoderStructureDTO.ValueID);

}
async function InitializeDecoderStructureControls() {
    document.getElementById("DecoderStructureOptionsGroup").hidden = true;
    document.getElementById("DecoderStructureValueGroup").hidden = true;
    DecoderStructureActionButtons("Save");
    $("#dxDecoderStructureAttributeLookup").dxLookup({
        dataSource: await GetAttributeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnabled: true,        
        dropDownOptions: {
            container: $('#DecoderStructureModal')
        },
        onSelectionChanged: async function (e) {
            let _attributeDTO = e.selectedItem;
            if (_attributeDTO != null) {
                if (_attributeDTO.HasMultipleOptions) {
                    document.getElementById("DecoderStructureOptionsGroup").hidden = false;
                    document.getElementById("DecoderStructureValueGroup").hidden = true;
                    //$("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").reset();
                    //$("#dxDecoderStructureValueLookup").dxLookup("instance").reset();
                    await $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").option("dataSource", await GetValueDataSource_Global(_attributeDTO.ID));

                }

                else {
                    document.getElementById("DecoderStructureValueGroup").hidden = false;
                    document.getElementById("DecoderStructureOptionsGroup").hidden = true;
                    if (_attributeDTO.ID === Attributes.Class || _attributeDTO.ID === Attributes.SubClass) {
                        $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").reset();

                        $("#dxDecoderStructureValueLookup").dxLookup("instance").option("dataSource", await GetAttributeValueDataSource_Global(_attributeDTO.ID));
                    }
                    else if (_attributeDTO.ID === Attributes.Variant) {
                        $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").reset();
                        $("#dxDecoderStructureValueLookup").dxLookup("instance").option("dataSource", await GetAttributeValueFromVariantDataSource_Global(_attributeDTO.ID));
                    }
                    else {
                        $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").reset();
                        await $("#dxDecoderStructureValueLookup").dxLookup("instance").option("dataSource", await GetValueDataSource_Global(_attributeDTO.ID));
                    }
                }
            }
            else {
                $("#dxDecoderStructureValueLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxDecoderStructureValueLookup").dxLookup({
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnabled: true,
        dropDownOptions: {
            container: $('#DecoderStructureModal')
        }
    });
    $("#dxDecoderStructureOptionsTagBox").dxTagBox({
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'useButtons'
    });
    $("#dxDecoderStructureNumberGrid").dxDataGrid({
        dataSource: await GetDXDecoderStructureDataSource({ DecoderID: $("#hiddenDecoderID").val(), NumberBody: true, GetValueDTO: true, SubClassID: $("#hiddenDecoderSubClassID").val(), GetAttributeValueLinkList: true }),
        keyExpr: "ID",
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100],
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
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "DecoderStructure",
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
        //rowDragging: {
        //    allowReordering: true,
        //    dropFeedbackMode: 'push',
        //    async onReorder(e) {
        //        let visibleRows = e.component.getVisibleRows();
        //        let newDecoderStructureDTO = visibleRows[e.toIndex].data;
        //        let d = $.Deferred();
        //        await ReorderNumberGrid(newDecoderStructureDTO, e.itemData);
        //        e.component.refresh();
        //    },
        //},
        columns:
            [
                {
                    caption: "Option",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        let _items = [];
                        if (Attributes.Variant == options.data.AttributeID || Attributes.Customer_Consigment == options.data.AttributeID) {
                            $("<div />").dxDropDownButton({
                                displayExpr: "name",
                                icon: 'overflow',
                                dropDownOptions: {
                                    width: 120,
                                },
                                items: [{
                                    "id": 4,
                                    "name": "Edit",
                                    "icon": "edit",
                                    onClick: function () {
                                        DecoderStructureActionButtons("Update");
                                        PopulateDecoderStructureFieldsFromNumber(options.data);
                                        $("#DecoderStructureModal").modal("toggle");
                                    }
                                }],

                            }).appendTo(container);
                        }
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Order", width: 50, dataField: "NumberOrder", sortOrder: "asc" },
                { caption: "Number Body?", dataField: "NumberBody", visible: false },
                { caption: "Attribute", dataField: "AttributeName" },
                { caption: "Description Order ", dataField: "DescriptionOrder", visible: false },
                { caption: "Description Body?", dataField: "DescriptionBody", visible: false },
                { caption: "Value", dataField: "ValueName" },
                { caption: "Code", dataField: "ValueDTO.Code", visible: false },
                { caption: "Added By", dataField: "AddedByName", visible: false },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime', visible: false },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime', visible: false },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName", visible: false }
            ],
    });
    $("#dxDecoderStructureDescriptionGrid").dxDataGrid({
        dataSource: await GetDXDecoderStructureDataSource({ DecoderID: $("#hiddenDecoderID").val(), DescriptionBody: true, GetValueDTO: true, SubClassID: $("#hiddenDecoderSubClassID").val(), GetAttributeValueLinkList: true }),
        keyExpr: "ID",
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100],
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
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "DecoderStructure",
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
        rowDragging: {
            allowReordering: true,
            dropFeedbackMode: 'push',
            async onReorder(e) {
                let visibleRows = e.component.getVisibleRows();
                let newDecoderStructureDTO = visibleRows[e.toIndex].data;
                let d = $.Deferred();
                await ReorderDescriptionGrid(newDecoderStructureDTO, e.itemData);
                e.component.refresh();
            },
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
                        if (Attributes.Class != options.data.AttributeID && Attributes.SubClass != options.data.AttributeID) {
                            $("<div />").dxDropDownButton({
                                displayExpr: "name",
                                icon: 'overflow',
                                dropDownOptions: {
                                    width: 120,
                                },
                                items: [
                                    {
                                        "id": 1,
                                        "name": "Delete",
                                        "icon": "trash",
                                        onClick: function () {
                                            PopulateDecoderStructureFieldsFromDescription(options.data);
                                            ShowDeleteDecoderStructureQuestion();
                                        }

                                    },
                                    {
                                        "id": 4,
                                        "name": "Edit",
                                        "icon": "edit",
                                        onClick: function () {
                                            DecoderStructureActionButtons("Update");
                                            $("#DecoderStructureModal").modal("toggle");

                                            PopulateDecoderStructureFieldsFromDescription(options.data);
                                        }
                                    }],

                            }).appendTo(container);
                        }

                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Order", width: 50, dataField: "DescriptionOrder", sortOrder: "asc" },
                { caption: "Description Body?", dataField: "DescriptionBody", visible: false },
                { caption: "Attribute", dataField: "AttributeName" },
                { caption: "Value", dataField: "ValueName" },
                { caption: "Code", dataField: "ValueDTO.Code", visible: false },
                { caption: "Number Body?", dataField: "NumberBody", visible: false },
                { caption: "Number Order", dataField: "NumberOrder", visible: false },
                { caption: "Added By", dataField: "AddedByName", visible: false },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime', visible: false },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime', visible: false },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName", visible: false }
            ],
    });
    $("#dxDescriptionBodyCheckBox").dxCheckBox({
        value: false,
    });
    $("#dxNumberBodyCheckBox").dxCheckBox({
        value: false,
    });
    await dxLoadPanel.hide();
}
async function DeleteDecoderStructure_Global() {
    await dxLoadPanel.show();
    const _decoderStructureDTO = GetDecoderStructureDTO();
    const _validation_resultDTO = await DeleteDecoderStructure(_decoderStructureDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").refresh();
        $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").refresh();
        ClearDecoderStructureFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
function DecoderStructureActionButtons(Action) {
    $("#DecoderStructureActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("DecoderStructureActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateDecoderStructureButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateDecoderStructureButton").addEventListener("click", CreateDecoderStructure_Global);
        document.getElementById("DecoderStructureCloseModalButton").addEventListener("click", ClearDecoderStructureFields);
    }
    else {
        // Update
        document.getElementById("DecoderStructureActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateDecoderStructureButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateDecoderStructureButton").addEventListener("click", UpdateDecoderStructure_Global);
        document.getElementById("DecoderStructureCloseModalButton").addEventListener("click", ClearDecoderStructureFields);
    }
}
function ClearDecoderStructureFields() {
    ClearErrorFeedback();
    DecoderStructureActionButtons("Save");
    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("readOnly", false)
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("readOnly", false)

    $("#hiddenDecoderStructureID").val("");
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("readOnly", false);
    $("#dxDecoderStructureAttributeLookup").dxLookup("instance").reset();
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("value", false);
    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("value", false);
    $("#DecoderConfiguratorDescriptionOrder").val(0);
    $("#DecoderConfiguratorNumberOrder").val(0);
    $("#dxDecoderStructureValueLookup").dxLookup("instance").reset();
    $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").reset();
    let keys = $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").deselectRows(keys);
    let descriptionGridkeys = $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").deselectRows(descriptionGridkeys);

    document.getElementById("DecoderStructureOptionsGroup").hidden = true;
    document.getElementById("DecoderStructureValueGroup").hidden = true;
}
function GetDecoderStructureDTO() {
    let _decoderStructureDTO = {
        ID: $("#hiddenDecoderStructureID").val(),
        DecoderID: $("#hiddenDecoderID").val(),
        AttributeID: $("#dxDecoderStructureAttributeLookup").dxLookup("instance").option("value"),
        ValueID: $("#dxDecoderStructureValueLookup").dxLookup("instance").option("value"),
        ValueIDArray: $("#dxDecoderStructureOptionsTagBox").dxTagBox("instance").option("value"),
        DescriptionBody: $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("value"),
        NumberBody: $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("value"),
        NumberOrder: $("#DecoderConfiguratorNumberOrder").val(),
        DescriptionOrder: $("#DecoderConfiguratorDescriptionOrder").val(),
        SubClassID: $("#hiddenDecoderSubClassID").val()

    }
    return _decoderStructureDTO;
}
async function ShowDeleteDecoderStructureQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the attribute, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDecoderStructure_Global();
    }
}
async function CreateDecoderStructure_Global() {
    await dxLoadPanel.show();
    const _decoderStructureDTO = GetDecoderStructureDTO();
    const _validation_resultDTO = await CreateDecoderStructure(_decoderStructureDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").refresh();
        $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").refresh();
        ClearDecoderStructureFields();
        $("#DecoderStructureModal").modal('toggle');
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateDecoderStructure_Global() {
    await dxLoadPanel.show();
    const _decoderStructureDTO = GetDecoderStructureDTO();
    const _validation_resultDTO = await UpdateDecoderStructure(_decoderStructureDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").refresh();
        $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").refresh();

        ClearDecoderStructureFields();
        $("#DecoderStructureModal").modal('toggle');
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function ReorderNumberGrid(newDecoderStructureDTO, DecoderStructureDTO) {
    await dxLoadPanel.show();
    DecoderStructureDTO.NewChangedDecoderStructureID = newDecoderStructureDTO.ID;
    let _validationResultDTO = await UpdateNumberOrder(DecoderStructureDTO)
    if (_validationResultDTO.Result) {
        $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").refresh();
        $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").refresh();
        ClearDecoderStructureFields();
    }
    HostResponse(_validationResultDTO);
    dxLoadPanel.hide();
}
async function ReorderDescriptionGrid(newDecoderStructureDTO, DecoderStructureDTO) {
    await dxLoadPanel.show();

    DecoderStructureDTO.NewChangedDecoderStructureID = newDecoderStructureDTO.ID;
    let _validationResultDTO = await UpdateDescriptionOrder(DecoderStructureDTO)
    if (_validationResultDTO.Result) {
        $("#dxDecoderStructureNumberGrid").dxDataGrid("instance").refresh();
        $("#dxDecoderStructureDescriptionGrid").dxDataGrid("instance").refresh();
        ClearDecoderStructureFields();

    }
    HostResponse(_validationResultDTO);
    dxLoadPanel.hide();
}
async function ShowDecoderConfiguratorDescriptionModal() {
    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("readOnly", true)
    $("#dxDescriptionBodyCheckBox").dxCheckBox("instance").option("value", true);
    $('#DecoderStructureModal').modal('show');
}
async function ShowDecoderConfiguratorNumberModal() {
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("readOnly", true)
    $("#dxNumberBodyCheckBox").dxCheckBox("instance").option("value", true);
    $('#DecoderStructureModal').modal('show');
}
async function GetValueDataSource_Global(AttributeID) {
    let _valueDTO = {
        IsActive: true,
        AttributeID: AttributeID,
    }
    return await GetDXValueDataSource(_valueDTO)
}
async function GetAttributeValueDataSource_Global(AttributeID) {
    let _attributeValueDTO = {
        GetChildValueDTO: true,
        IsActive: true,
        ParentAttributeID: Attributes.SubClass,
        ParentValueID: $("#hiddenDecoderSubClassID").val(),
        ChildAttributeID: AttributeID
    }
    var _attributevalueList = await GetValueLinkInformation(_attributeValueDTO);
    var _valueList = _attributevalueList.map(m => m.ChildValueDTO);
    return _valueList;

}
async function GetAttributeValueFromVariantDataSource_Global(AttributeID) {
    let _valueDTO = {
        IsActive: true,
        AttributeID: AttributeID,
    }
    let _variantValueList = await GetValueInformation(_valueDTO);
    let _decoderDTO = {
        DecoderID: $("#hiddenDecoderID").val(),
    }
    var _decoderStructureList = await GetDecoderStructureInformation(_decoderDTO);
    let _list = _variantValueList.filter(variantDTO => _decoderStructureList.find(de => de.AttributeID == variantDTO.Code));
    let _n_avalue = _variantValueList.filter(variantDTO => variantDTO.ID == Value_Enum.Variant_N_A)
    _list.push(_n_avalue[0])
    return _list;

}
const GetAttributeDataSource_Global = () => {
    let _attributeDTO = {
        IsActive: true
    }
    let _filters = [
        ["ID", "<>", Attributes.Class], "and",
        ["ID", "<>", Attributes.ComponentType], "and",
        ["ID", "<>", Attributes.SubClass], "and",
        ["ID", "<>", Attributes.PartType], "and",
        ["ID", "<>", Attributes.ClassID]
    ];
    return GetDXAttributeDataSource(_attributeDTO, _filters);

}

// Manufacturer
async function InitializeSubClass_SupplierControls() {
    SubClass_SupplierActionButtons("Save");
    $("#dxSupplierLookup").dxLookup({
        dataSource: await GetDXSupplierDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnabled: true,
        dropDownOptions: {
            container: $('#SubClass_SupplierModal')
        }

    });
    $("#dxSubClass_SupplierGrid").dxDataGrid({
        dataSource: await GetDXSubClass_SupplierDataSource_Global(),
        keyExpr: "ID",
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
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "DecoderStructure",
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
        columns:
            [
                {
                    caption: "Option",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        let _items = [
                            {
                                "id": 1,
                                "name": "Delete",
                                "icon": "trash",
                                onClick: function () {
                                    PopulateSubClass_SupplierFields(options.data);
                                    ShowDeleteSubClass_SupplierQuestion();
                                }

                            },
                            {
                                "id": 4,
                                "name": "Edit",
                                "icon": "edit",
                                onClick: function () {
                                    SubClass_SupplierActionButtons("Update");
                                    PopulateSubClass_SupplierFields(options.data);
                                    $("#SubClass_SupplierModal").modal("toggle");
                                }
                            }];
                        $("<div />").dxDropDownButton({
                            displayExpr: "name",
                            icon: 'overflow',
                            dropDownOptions: {
                                width: 120,
                            },

                            items: _items,

                        }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Supplier", dataField: "SupplierName" },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" }
            ],
    });
    $("#dxSubClass_SupplierIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    await dxLoadPanel.hide();
}
async function GetDXSupplierDataSource_Global() {
    let _supplierDTO = {
        IsActive: true
    }
    return await GetDXSupplierDataSource(_supplierDTO);
}
async function GetDXSubClass_SupplierDataSource_Global() {
    let _subClass_SupplierDTO = { SubClassID: $("#hiddenDecoderSubClassID").val() }
    return await GetDXSubClass_SupplierDataSource(_subClass_SupplierDTO);
}
function SubClass_SupplierActionButtons(Action) {
    $("#SubClass_SupplierActionButtons").empty();
    document.getElementById("SubClass_SupplierCloseModalButton").addEventListener("click", ClearSubClass_SupplierFields);

    if (Action == "Save") {
        document.getElementById("SubClass_SupplierActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSubClass_SupplierButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSubClass_SupplierButton").addEventListener("click", CreateSubClass_Supplier_Global);
    }
    else {
        // Update
        document.getElementById("SubClass_SupplierActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSubClass_SupplierButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateSubClass_SupplierButton").addEventListener("click", UpdateSubClass_Supplier_Global);
    }
}
async function CreateSubClass_Supplier_Global() {
    await dxLoadPanel.show();
    const _subClass_SupplierDTO = GetSubClass_SupplierDTO();
    const _validation_resultDTO = await CreateSubClass_Supplier(_subClass_SupplierDTO)
    if (_validation_resultDTO.Result) {
        $("#dxSubClass_SupplierGrid").dxDataGrid("instance").refresh();
        ClearSubClass_SupplierFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateSubClass_Supplier_Global() {
    await dxLoadPanel.show();
    const _subClass_SupplierDTO = GetSubClass_SupplierDTO();
    const _validation_resultDTO = await UpdateSubClass_Supplier(_subClass_SupplierDTO)
    if (_validation_resultDTO.Result) {
        $("#dxSubClass_SupplierGrid").dxDataGrid("instance").refresh();
        ClearSubClass_SupplierFields();
        $("#SubClass_SupplierModal").modal('toggle');
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function DeleteSubClass_Supplier_Global() {
    await dxLoadPanel.show();
    const _subClass_SupplierDTO = GetSubClass_SupplierDTO();
    const _validation_resultDTO = await DeleteSubClass_Supplier(_subClass_SupplierDTO)
    if (_validation_resultDTO.Result) {
        $("#dxSubClass_SupplierGrid").dxDataGrid("instance").refresh();
        ClearSubClass_SupplierFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
function GetSubClass_SupplierDTO() {
    let _subClass_SupplierDTO = {
        ID: $("#hiddenSubClass_SupplierID").val(),
        SupplierID: $("#dxSupplierLookup").dxLookup("instance").option("value"),
        SubClassID: $("#hiddenDecoderSubClassID").val(),
        IsActive: $("#dxSubClass_SupplierIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _subClass_SupplierDTO;
}
function ClearSubClass_SupplierFields() {
    ClearErrorFeedback();
    SubClass_SupplierActionButtons("Save");
    $("#dxSubClass_SupplierIsActiveCheckBox").dxCheckBox("instance").option("value", true)
    $("#hiddenSubClass_SupplierID").val("");
    $("#dxSupplierLookup").dxLookup("instance").reset();
    let keys = $("#dxSubClass_SupplierGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSubClass_SupplierGrid").dxDataGrid("instance").deselectRows(keys)
}
async function PopulateSubClass_SupplierFields(SubClass_SupplierDTO) {
    $("#hiddenSubClass_SupplierID").val(SubClass_SupplierDTO.ID);
    $("#dxSupplierLookup").dxLookup("instance").option("value", SubClass_SupplierDTO.SupplierID);
    $("#dxSubClass_SupplierIsActiveCheckBox").dxCheckBox("instance").option("value", SubClass_SupplierDTO.IsActive);
}
async function ShowDeleteSubClass_SupplierQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove this supplier, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSubClass_Supplier_Global();
    }
}
