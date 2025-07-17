import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../Common/Utils/SearchParamsService.js';

export async function CreateCustomer(CustomerDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Customer/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CustomerDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateCustomer(CustomerDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Customer/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CustomerDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteCustomer(CustomerDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Customer/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CustomerDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetCustomerInformation(CustomerDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Customer/GetCustomerList?` + new URLSearchParams(CustomerDTO), {
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
export async function GetDXCustomerDataSource(CustomerDTO) {
    let params = await BuildSearchParams(CustomerDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Customer/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _CustomerDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _CustomerDataSource;
}