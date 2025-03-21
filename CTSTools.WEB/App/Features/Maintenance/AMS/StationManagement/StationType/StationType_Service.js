import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreateStationType(StationTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/StationType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StationTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateStationType(StationTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/StationType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StationTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteStationType(StationTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/StationType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StationTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetStationTypeInformation(stationTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/StationType/GetStationTypeList?` + new URLSearchParams(stationTypeDTO), {
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
export async function GetDXStationTypeDataSource(StationTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/StationType/GetPagedList?` + new URLSearchParams(StationTypeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _stationTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _stationTypeDataSource;
}