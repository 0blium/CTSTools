import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'

export async function CreateDomain(DomainDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Domain/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DomainDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDomain(DomainDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Domain/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DomainDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDomain(DomainDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Domain/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DomainDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDomainInformation(DomainDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _params = await BuildSearchParams(DomainDTO);
        const _response = await fetch(`${APIURL}/Domain/GetList?` + _params.toString(), {
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
export async function GetDXDomainDataSource(DomainDTO) {
    let params = await BuildSearchParams(DomainDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Domain/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {

        },
    });
    let _domainDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
    });
    return _domainDataSource;
}