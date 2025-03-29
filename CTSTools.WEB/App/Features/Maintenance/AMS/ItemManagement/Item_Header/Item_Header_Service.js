import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateItem_Header(Item_HeaderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Header/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_HeaderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateItem_Header(Item_HeaderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Header/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_HeaderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteItem_Header(Item_HeaderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Header/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_HeaderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetItem_HeaderInformation(item_HeaderDTO) {
    let params = await BuildSearchParams(item_HeaderDTO);

    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Header/GetList?` + params.toString(), {
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
export async function GetDXItem_HeaderDataSource(Item_HeaderDTO) {
    let params = await BuildSearchParams(Item_HeaderDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Item_Header/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _item_HeaderDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 8,
        remoteOperations: true,
    });
    return _item_HeaderDataSource;
}

//#region Attachments

export async function GetItem_HeaderFilesInformation(item_HeaderDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_Header/GetFileList?` + new URLSearchParams(item_HeaderDTO), {
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
export async function SaveItem_HeaderMultipleFile(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Header/SaveMultipleFile`;
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
export async function DeleteItem_HeaderFile(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_Header/FileDelete`;
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