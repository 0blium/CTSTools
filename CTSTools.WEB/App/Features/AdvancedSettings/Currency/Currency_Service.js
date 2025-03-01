import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'

export async function CreateCurrency(CurrencyDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Currency/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CurrencyDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateCurrency(CurrencyDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Currency/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CurrencyDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteCurrency(CurrencyDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Currency/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CurrencyDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetCurrencyInformation(currencyDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Currency/GetList?` + new URLSearchParams(currencyDTO), {
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
export async function GetDXCurrencyDataSource(CurrencyDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Currency/GetPagedList?` + new URLSearchParams(CurrencyDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _currencyDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _currencyDataSource;
}