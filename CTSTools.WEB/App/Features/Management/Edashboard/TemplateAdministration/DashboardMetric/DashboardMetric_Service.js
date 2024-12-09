import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'

export async function CreateDashboardMetric(DashboardMetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardMetric/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardMetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDashboardMetric(DashboardMetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardMetric/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardMetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDashboardMetric(DashboardMetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardMetric/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardMetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDashboardMetricInformation(DashboardMetricDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(DashboardMetricDTO);
    try {
        const _response = await fetch(`${APIURL}/DashboardMetric/GetDashboardMetricList?` + params.toString(), {
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
export async function GetDashboardMetricWithUI(DashboardMetricDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(DashboardMetricDTO);
    try {
        const _response = await fetch(`${APIURL}/DashboardMetric/GetDashboardMetricWithUI?` + params.toString(), {
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
export async function CreateDashboardMetricFromMetricList(DashboardMetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardMetric/CreateFromMetricList`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardMetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDashboardMetricOrder(DashboardMetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardMetric/UpdateDashboardMetricOrder`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardMetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//DX DataSource
export async function GetDXDashboardMetricDataSource(DashboardMetricDTO) {
    let params = await BuildSearchParams(DashboardMetricDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DashboardMetric/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _DashboardMetricDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _DashboardMetricDataSource;
}