import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateProvider(ProviderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Provider/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ProviderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateProvider(ProviderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Provider/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ProviderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteProvider(ProviderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Provider/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ProviderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetProviderInformation(ProviderDTO) {
    let _params = await BuildSearchParams(ProviderDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Provider/GetProviderList?` + _params.toString(), {
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
export async function GetDXProviderDataSource(ProviderDTO) {
    let _params = await BuildSearchParams(ProviderDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Provider/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _ProviderDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _ProviderDataSource;
}