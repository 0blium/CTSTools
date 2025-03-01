import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateSparePartUsage(SparePartUsageDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePartUsage/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePartUsageDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSparePartUsage(SparePartUsageDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePartUsage/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePartUsageDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSparePartUsage(SparePartUsageDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePartUsage/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePartUsageDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSparePartUsageInformation(SparePartUsageDTO) {
    let _params = await BuildSearchParams(SparePartUsageDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/SparePartUsage/GetSparePartUsageList?` + _params.toString(), {
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
export async function GetDXSparePartUsageDataSource(SparePartUsageDTO) {
    let _params = await BuildSearchParams(SparePartUsageDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SparePartUsage/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _SparePartUsageDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _SparePartUsageDataSource;
}