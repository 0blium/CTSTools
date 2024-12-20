import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';


export async function CreateGoalRange(GoalRangeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/GoalRange/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', GoalRangeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateGoalRange(GoalRangeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/GoalRange/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', GoalRangeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteGoalRange(GoalRangeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/GoalRange/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', GoalRangeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetGoalRangeInformation(GoalRangeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/GoalRange/GetGoalRangeList?` + new URLSearchParams(GoalRangeDTO), {
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
export async function GetDXGoalRangeDataSource(GoalRangeDTO) {
    let params = await BuildSearchParams(GoalRangeDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/GoalRange/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _GoalRangeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _GoalRangeDataSource;
}