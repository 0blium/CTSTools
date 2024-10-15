import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateFacility(FacilityDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Facility/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FacilityDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateFacility(FacilityDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Facility/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FacilityDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteFacility(FacilityDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Facility/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FacilityDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

//Read
export async function GetFacilityInformation(FacilityDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Facility/GetFacilityList?` + new URLSearchParams(FacilityDTO), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

//DX DataSource
export async function GetDXFacilityDataSource(FacilityDTO) {
    let params = await BuildSearchParams(FacilityDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Facility/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _facilityDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _facilityDataSource;
}