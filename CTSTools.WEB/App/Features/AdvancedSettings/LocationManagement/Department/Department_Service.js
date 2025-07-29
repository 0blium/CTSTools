import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'


export async function CreateDepartment(DepartmentDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Department/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DepartmentDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdateDepartment(DepartmentDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Department/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DepartmentDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeleteDepartment(DepartmentDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Department/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DepartmentDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function GetDepartmentInformation(DepartmentDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Department/GetDepartmentList?` + new URLSearchParams(DepartmentDTO), {
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
export async function GetDXDepartmentDataSource(DepartmentDTO) {
    let params = await BuildSearchParams(DepartmentDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Department/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });

    let _departmentDTOSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _departmentDTOSource;
}
