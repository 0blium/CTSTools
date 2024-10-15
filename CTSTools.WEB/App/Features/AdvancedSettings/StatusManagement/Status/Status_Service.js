import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateStatus(StatusDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Status/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StatusDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateStatus(StatusDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Status/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StatusDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteStatus(StatusDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Status/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StatusDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetStatusInformation(statusDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Status/GetStatusList?` + new URLSearchParams(statusDTO), {
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
export async function GetDXStatusDataSource(StatusDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Status/GetPagedList?`+ new URLSearchParams(StatusDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _statusDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _statusDataSource;
}