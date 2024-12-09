import { APIURL } from '../../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../../Common/Utils/SearchParamsService.js';


export async function CreateCalculationType(CalculationTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/CalculationType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CalculationTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateCalculationType(CalculationTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/CalculationType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CalculationTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteCalculationType(CalculationTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/CalculationType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CalculationTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetCalculationTypeInformation(CalculationTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/CalculationType/GetCalculationTypeList?` + new URLSearchParams(CalculationTypeDTO), {
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
export async function GetDXCalculationTypeDataSource(CalculationTypeDTO) {
    let params = await BuildSearchParams(CalculationTypeDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/CalculationType/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _calculationTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _calculationTypeDataSource;
}