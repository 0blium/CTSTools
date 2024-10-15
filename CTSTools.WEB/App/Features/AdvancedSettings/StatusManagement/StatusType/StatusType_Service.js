import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreateStatusType(StatusTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/StatusType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StatusTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateStatusType(StatusTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/StatusType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StatusTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteStatusType(StatusTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/StatusType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StatusTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}


export async function GetStatusTypeInformation(StatusTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/StatusType/GetList?` + new URLSearchParams(StatusTypeDTO), {
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
export async function GetDXStatusTypeDataSource(StatusTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/StatusType/GetPagedList?` + new URLSearchParams(StatusTypeDTO),
        key: "ID",
        //loadParams: {
        //    "StatusTypeDTO": StatusTypeDTO
        //},
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _StatusTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _StatusTypeDataSource;
}