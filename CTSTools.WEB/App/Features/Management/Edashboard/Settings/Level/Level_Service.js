import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';

export async function CreateLevel(LevelDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Level/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', LevelDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateLevel(LevelDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Level/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', LevelDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteLevel(LevelDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Level/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', LevelDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetLevelInformation(LevelDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Level/GetLevelList?` + new URLSearchParams(LevelDTO), {
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
export async function GetDXLevelDataSource(LevelDTO) {
    let params = await BuildSearchParams(LevelDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Level/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _levelDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _levelDataSource;
}