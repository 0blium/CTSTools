import { GetUser_PermissionInformation } from '../../Features/AdvancedSettings/UserManagement/User_Permission/User_Permission_Service.js'

export async function ShowSidebarMenu(UserID) {
    let _user_PermissionDTO = {
        UserID: UserID,
        GetPermissionDTO: true,
    }
    let _validationResultDTO = await GetUser_PermissionInformation(_user_PermissionDTO);
    if (_validationResultDTO != null) {
        let _moduleList = _validationResultDTO.map(up => up.PermissionDTO.ModuleName);
        const _moduleNav = document.querySelectorAll('.module-nav');
        const _subMenuNav = document.querySelectorAll('.submenu-nav');
        const _menuNav = document.querySelectorAll('.menu-nav');
        let _moduleNavList = [];
        let _subMenuList = [];
        _moduleNav.forEach(item => {
            // Getting value from data-tech
            const _moduleMenuList = item.getAttribute('data-tech').split(' ');
            // Check if any of the modules in "data-tech" are in the user's permissions
            const _exist = _moduleMenuList.some(Module => _moduleList.includes(Module));
            // If you have permission, remove the hidden; If not, keep it
            if (_exist) {
                item.removeAttribute('hidden');
                _moduleNavList.push(item.id);
            } else {
                item.setAttribute('hidden', true);
            }
        });
        _subMenuNav.forEach(MenuName => {
            const _exist = _moduleNavList.some(id => id.endsWith(MenuName.id));
            if (_exist && !_subMenuList.includes(MenuName.id)) {
                _subMenuList.push(MenuName.id);
            }
        });
        _subMenuList.forEach(MenuName => {
            document.getElementById(MenuName).removeAttribute('hidden');
        });
        _menuNav.forEach(MenuName => {
            const _menuNavList = MenuName.getAttribute('data-tech').split(' ');
            let _exist = _menuNavList.some(Menu => _subMenuList.includes(Menu));
            if (_exist) {
                // Cycle through related submenus and display them
                document.getElementById(MenuName.id).removeAttribute('hidden');
            }
        });
    }
}