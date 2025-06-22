import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreateStatus_StatusType(Status_StatusTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Status_StatusType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Status_StatusTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateStatus_StatusType(Status_StatusTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Status_StatusType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Status_StatusTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteStatus_StatusType(Status_StatusTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Status_StatusType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Status_StatusTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}


export async function GetStatus_StatusTypeInformation(Status_StatusTypeDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(Status_StatusTypeDTO);
    try {
        const _response = await fetch(`${APIURL}/Status_StatusType/GetList?` + params.toString(), {
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
export async function GetDXStatus_StatusTypeDataSource(Status_StatusTypeDTO) {
    let params = await BuildSearchParams(Status_StatusTypeDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Status_StatusType/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });

    let _Status_StatusTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _Status_StatusTypeDataSource;
}