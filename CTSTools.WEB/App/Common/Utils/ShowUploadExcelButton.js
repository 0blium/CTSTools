import { GetUser_PermissionInformation } from '../../Features/AdvancedSettings/UserManagement/User_Permission/User_Permission_Service.js'

export async function ShowUploadMassiveButton(UserID) {
    let _user_PermissionDTO = {
        UserID: UserID,
        GetPermissionDTO: true,
    }
    // We look for elements that have UploadMassive in their id
    let _buttonList = document.querySelectorAll("[id*='UploadMassive']");
    if (_buttonList == null || _buttonList.length === 0)
        return;
    // If there are elements in _buttonList we will bring the user's permissions
    let _user_PermissionList = await GetUser_PermissionInformation(_user_PermissionDTO);
    if (_user_PermissionList == null || _user_PermissionList.length === 0)
        return;
    // If you have permissions, we will filter the list to get elements that have Import in their ActioneName
    _user_PermissionList = _user_PermissionList.filter(User_PermissionDTO => User_PermissionDTO.PermissionDTO.ActionName === "Import");
    if (_user_PermissionList == null || _user_PermissionList.length === 0)
        return;
    // If the list _user_PermissionList has elements, we will take only the ModuleName
    let _moduleNameList = _user_PermissionList.map(User_PermissionDTO => User_PermissionDTO.PermissionDTO.ModuleName);
    // We perform a forEach to compare the id names of our _buttonList list with our _moduleNameList list and show the button if it has Import permission
    _buttonList.forEach(ItemBtn => {
        // Get the element id
        let _iD = ItemBtn.id;
        // We eliminate 'UploadMassive' and 'ModalButton', to keep the name of the module (e.g. UploadMassiveAttributeModalButton => Attribute)
        let _moduleName = _iD.replace('UploadMassive', '').replace('ModalButton', '');
        // We check in the _moduleNameList list if any element begins with the _moduleName that we previously saved
        let _existUser_Permission = _moduleNameList.some(ModuleName => ModuleName.startsWith(_moduleName));
        if (_existUser_Permission) // If the _moduleName exists, we remove the hidden from the button
            document.getElementById(_iD).removeAttribute('hidden');
    });
}