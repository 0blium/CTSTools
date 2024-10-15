import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateAction(ActionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Action/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ActionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateAction(ActionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Action/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ActionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteAction(ActionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Action/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ActionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

//Read
export async function GetActionInformation(ActionDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Action/GetActionList?` + new URLSearchParams(ActionDTO), {
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
export async function GetDXActionDataSource(ActionDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Action/GetPagedList?` + new URLSearchParams(ActionDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _actionDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _actionDataSource;
}