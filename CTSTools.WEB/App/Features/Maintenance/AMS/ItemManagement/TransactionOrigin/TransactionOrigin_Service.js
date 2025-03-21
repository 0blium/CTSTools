import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateTransactionOrigin(TransactionOriginDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/TransactionOrigin/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', TransactionOriginDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateTransactionOrigin(TransactionOriginDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/TransactionOrigin/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', TransactionOriginDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteTransactionOrigin(TransactionOriginDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/TransactionOrigin/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', TransactionOriginDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetTransactionOriginInformation(transactionOriginDTO) {
    let _params = await BuildSearchParams(transactionOriginDTO)
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/TransactionOrigin/GetList?` + _params.toString(), {
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
export async function GetDXTransactionOriginDataSource(TransactionOriginDTO) {
    let _params = await BuildSearchParams(TransactionOriginDTO)
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/TransactionOrigin/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _transactionOriginDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _transactionOriginDataSource;
}