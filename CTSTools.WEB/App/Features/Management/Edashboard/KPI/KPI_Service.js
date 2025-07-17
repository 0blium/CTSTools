import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js';

export async function CreateKPI(KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/KPI/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function CreateMassiveKPI(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/KPI/CreateMassive`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FileDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdateKPI(KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/KPI/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteKPI(KPIDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/KPI/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', KPIDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetKPIInformation(KPIDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/KPI/GetKPIList?` + new URLSearchParams(KPIDTO), {
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
export async function GetDXKPIDataSource(KPIDTO) {
    let params = await BuildSearchParams(KPIDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/KPI/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _KPIDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _KPIDataSource;
}