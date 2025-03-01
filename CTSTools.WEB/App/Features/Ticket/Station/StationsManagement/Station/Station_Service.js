import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateStation(StationDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Station/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StationDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateStation(StationDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Station/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StationDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteStation(StationDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Station/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', StationDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetStationInformation(stationDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Station/GetStationList?` + new URLSearchParams(stationDTO), {
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
export async function GetDXStationDataSource(StationDTO) {
    let params = await BuildSearchParams(StationDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Station/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _stationDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _stationDataSource;
}
