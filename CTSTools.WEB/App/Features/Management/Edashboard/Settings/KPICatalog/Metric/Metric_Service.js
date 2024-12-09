import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateMetric(MetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Metric/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateMetric(MetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Metric/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteMetric(MetricDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Metric/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MetricDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetMetricInformation(MetricDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Metric/GetMetricList?` + new URLSearchParams(MetricDTO), {
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
export async function GetDXMetricDataSource(MetricDTO) {
    let params = await BuildSearchParams(MetricDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Metric/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _MetricDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _MetricDataSource;
}