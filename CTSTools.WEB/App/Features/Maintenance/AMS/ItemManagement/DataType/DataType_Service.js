import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreateDataType(DataTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DataType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DataTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDataType(DataTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DataType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DataTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDataType(DataTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DataType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DataTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDataTypeInformation(dataTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/DataType/GetDataTypeList?` + new URLSearchParams(dataTypeDTO), {
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
export async function GetDXDataTypeDataSource(DataTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DataType/GetPagedList?` + new URLSearchParams(DataTypeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _dataTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _dataTypeDataSource;
}