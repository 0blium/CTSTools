import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'

export async function CreateSupplyType(SupplyTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupplyType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupplyTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSupplyType(SupplyTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupplyType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupplyTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSupplyType(SupplyTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupplyType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupplyTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSupplyTypeInformation(supplyTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/SupplyType/GetSupplyTypeList?` + new URLSearchParams(supplyTypeDTO), {
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
export async function GetDXSupplyTypeDataSource(SupplyTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SupplyType/GetPagedList?` + new URLSearchParams(SupplyTypeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _supplyTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _supplyTypeDataSource;
}