import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'

export async function CreatePartNumber(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MassivePartNumber/Create`;
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