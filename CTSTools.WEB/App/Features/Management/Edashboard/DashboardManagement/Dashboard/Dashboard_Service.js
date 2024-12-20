import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';



export async function CreateDashboard(DashboardDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDashboard(DashboardDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDashboard(DashboardDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Dashboard/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDashboardInformation(DashboardDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Dashboard/GetDashboardList?` + new URLSearchParams(DashboardDTO), {
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
export async function GetDXDashboardDataSource(DashboardDTO) {
    let params = await BuildSearchParams(DashboardDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Dashboard/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _DashboardDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _DashboardDataSource;
}