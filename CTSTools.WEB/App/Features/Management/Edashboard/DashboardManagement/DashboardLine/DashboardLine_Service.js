import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';

export async function CreateDashboardLine(DashboardLineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardLine/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardLineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdateDashboardLine(DashboardLineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardLine/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardLineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeleteDashboardLine(DashboardLineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardLine/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardLineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function GetDashboardLineInformation(DashboardLineDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(DashboardLineDTO);
    try {
        const _response = await fetch(`${APIURL}/DashboardLine/GetDashboardLineList?` + params.toString(), {
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

// DX DataSource
export async function GetDXDashboardLineDataSource(DashboardLineDTO) {
    let params = await BuildSearchParams(DashboardLineDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DashboardLine/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _DashboardLineDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _DashboardLineDataSource;
}

export async function GetDashboardMetricTendence(DashboardLineDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(DashboardLineDTO);
    try {
        const _response = await fetch(`${APIURL}/DashboardLine/GetDashboardMetricTendence?` + params.toString(), {
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


export async function AddMonthlyValue(DashboardLineDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardLine/AddMonthlyValue`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardLineDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}