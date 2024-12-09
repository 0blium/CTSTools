import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';


export async function CreateDashboardCategory(DashboardCategoryDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardCategory/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardCategoryDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDashboardCategory(DashboardCategoryDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardCategory/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardCategoryDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDashboardCategory(DashboardCategoryDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DashboardCategory/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DashboardCategoryDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDashboardCategoryInformation(DashboardCategoryDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/DashboardCategory/GetDashboardCategoryList?` + new URLSearchParams(DashboardCategoryDTO), {
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
export async function GetDXDashboardCategoryDataSource(DashboardCategoryDTO) {
    let params = await BuildSearchParams(DashboardCategoryDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DashboardCategory/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _dashboardCategoryDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _dashboardCategoryDataSource;
}