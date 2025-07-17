
import { APIURL } from '../../../../Common/Utils/Environment.js';
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js';
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'

export async function CreatePriority(PriorityDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Priority/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PriorityDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdatePriority(PriorityDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Priority/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PriorityDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeletePriority(PriorityDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Priority/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PriorityDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetPriorityInformation(PriorityDTO) {
    let _params = await BuildSearchParams(PriorityDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Priority/GetPriorityList?` + _params.toString(), {
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
export async function GetDXPriorityDataSource(PriorityDTO) {
    let _params = await BuildSearchParams(PriorityDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Priority/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _PriorityDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _PriorityDataSource;
}