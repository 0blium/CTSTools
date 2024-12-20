import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';

export async function CreateUnitOfMeasure(UnitOfMeasureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UnitOfMeasure/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UnitOfMeasureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateUnitOfMeasure(UnitOfMeasureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UnitOfMeasure/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UnitOfMeasureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteUnitOfMeasure(UnitOfMeasureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UnitOfMeasure/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UnitOfMeasureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetUnitOfMeasureInformation(UnitOfMeasureDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/UnitOfMeasure/GetUnitOfMeasureList?` + new URLSearchParams(UnitOfMeasureDTO), {
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
export async function GetDXUnitOfMeasureDataSource(UnitOfMeasureDTO) {
    let params = await BuildSearchParams(UnitOfMeasureDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/UnitOfMeasure/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _UnitOfMeasureDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _UnitOfMeasureDataSource;
}