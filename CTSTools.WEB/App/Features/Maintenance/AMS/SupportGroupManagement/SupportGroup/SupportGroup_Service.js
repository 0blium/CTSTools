import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateSupportGroup(SupportGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupportGroup/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupportGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSupportGroup(SupportGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupportGroup/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupportGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSupportGroup(SupportGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupportGroup/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupportGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSupportGroupInformation(supportGroupDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/SupportGroup/GetSupportGroupList?` + new URLSearchParams(supportGroupDTO), {
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
export async function GetDXSupportGroupDataSource(SupportGroupDTO) {
    let params = await BuildSearchParams(SupportGroupDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SupportGroup/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _supportGroupDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _supportGroupDataSource;
}