import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'

export async function CreateItemClassification(ItemClassificationDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ItemClassification/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ItemClassificationDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateItemClassification(ItemClassificationDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ItemClassification/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ItemClassificationDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteItemClassification(ItemClassificationDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ItemClassification/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ItemClassificationDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetItemClassificationInformation(itemClassificationDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/ItemClassification/GetItemClassificationList?` + new URLSearchParams(itemClassificationDTO), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

//DX DataSource
export async function GetDXItemClassificationDataSource(ItemClassificationDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/ItemClassification/GetPagedList?` + new URLSearchParams(ItemClassificationDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _itemClassificationDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _itemClassificationDataSource;
}