import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateItem_Line(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateItem_Line(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteItem_Line(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetItem_LineInformation(item_LineDTO) {
    let params = await BuildSearchParams(item_LineDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Line/GetList?` + params.toString(), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

export async function GetItem_LineInformationBySupportGroup(item_LineDTO) {
    let params = await BuildSearchParams(item_LineDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Line/GetListBySupportGroup?` + params.toString(), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

export async function GetItem_LineMasterDetailInformation(Item_LineDTO) {
    let params = await BuildSearchParams(Item_LineDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Line/GeMasterDetailtList?` + params.toString(), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}
export async function GetItem_LineMasterDetailByStationInformation(Item_LineDTO) {
    let params = await BuildSearchParams(Item_LineDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Line/GeMasterDetailByStationList?` + params.toString(), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

//DX DataSource
export async function GetDXItem_LineDataSource(Item_LineDTO) {
    let params = await BuildSearchParams(Item_LineDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Item_Line/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _item_LineDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 10,
        remoteOperations: true,
    });
    return _item_LineDataSource;
}

//#region Item Station

//Create item station
export async function CreateItem_Line_StationByArray(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/CreateItem_Station`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//update station of the item
export async function UpdateItem_Line_Station(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/UpdateItem_Station`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//#endregion


//#region Item Line Item Support Group
//Update Item Line Item Support Group 
export async function ReassignSupportGroup(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/ReassignSupportGroup`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//#endregion

//#region Reassign Owner To Item
//Update Item Line Item Support Group 
export async function ReassignOwner(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/ReassignOwner`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//#endregion

//#region Item Delivery
//Update Item Line Delivery TO
export async function ItemDelivery(Item_LineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/ItemDelivery`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_LineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//#endregion






//#region Attachments
export async function GetItem_LineFilesInformation(item_LineDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Line/GetFileList?` + new URLSearchParams(item_LineDTO), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}
export async function GetItem_LineFilesTreeView(Item_LineDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(Item_LineDTO);
    try {
        const _response = await fetch(`${APIURL}/Item_Line/GetFileTreeViewList?` + params.toString(), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}


export async function DeleteItem_LineFile(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Line/FileDelete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FileDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

//#endregion
