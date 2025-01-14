import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js';


export async function CreateModule(ModuleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Module/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ModuleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateModule(ModuleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Module/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ModuleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteModule(ModuleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Module/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ModuleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetModuleInformation(ModuleDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Module/GetModuleList?` + new URLSearchParams(ModuleDTO), {
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
export async function GetDXModuleDataSource(ModuleDTO) {
    let params = await BuildSearchParams(ModuleDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Module/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _ModuleDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _ModuleDataSource;
}