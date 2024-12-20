import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';

export async function CreateEquivalence(EquivalenceDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Equivalence/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', EquivalenceDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateEquivalence(EquivalenceDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Equivalence/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', EquivalenceDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteEquivalence(EquivalenceDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Equivalence/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', EquivalenceDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetEquivalenceInformation(EquivalenceDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Equivalence/GetEquivalenceList?` + new URLSearchParams(EquivalenceDTO), {
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
export async function GetDXEquivalenceDataSource(EquivalenceDTO) {
    let params = await BuildSearchParams(EquivalenceDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Equivalence/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _equivalenceDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _equivalenceDataSource;
}