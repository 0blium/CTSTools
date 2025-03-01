import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'

export async function GetChangeLogInformation(changeLogDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/ChangeLog/GetList?` + new URLSearchParams(changeLogDTO), {
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
export async function GetDXChangeLogDataSource(ChangeLogDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/ChangeLog/GetPagedList?` + new URLSearchParams(ChangeLogDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _changeLogDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _changeLogDataSource;
}