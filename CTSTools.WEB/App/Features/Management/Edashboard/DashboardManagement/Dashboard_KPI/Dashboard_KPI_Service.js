import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';


export async function CreateDashboard_KPI(Dashboard_KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard_KPI/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Dashboard_KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDashboard_KPI(Dashboard_KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard_KPI/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Dashboard_KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDashboard_KPI(Dashboard_KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard_KPI/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Dashboard_KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDashboard_KPIInformation(Dashboard_KPIDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(Dashboard_KPIDTO);
    try {
        const _response = await fetch(`${APIURL}/Dashboard_KPI/GetDashboard_KPIList?` + params.toString(), {
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
export async function GetDashboard_KPIWithUI(Dashboard_KPIDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(Dashboard_KPIDTO);
    try {
        const _response = await fetch(`${APIURL}/Dashboard_KPI/GetDashboard_KPIWithUI?` + params.toString(), {
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
export async function CreateDashboard_KPIFromKPIList(Dashboard_KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard_KPI/CreateFromKPIList`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Dashboard_KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDashboard_KPIOrder(Dashboard_KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard_KPI/UpdateDashboard_KPIOrder`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Dashboard_KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//DX DataSource
export async function GetDXDashboard_KPIDataSource(Dashboard_KPIDTO) {
    let params = await BuildSearchParams(Dashboard_KPIDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Dashboard_KPI/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _Dashboard_KPIDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _Dashboard_KPIDataSource;
}