//const { dxLoadPanel } = import("../../Common/Components/dxLoadPanel");
import { dxLoadPanel } from "../../Common/Components/dxLoadPanel.js"
import { GetUser_PermissionWithUserID } from '../AdvancedSettings/UserManagement/User_Permission/User_Permission_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    await dxLoadPanel.show();
    InitializeDevExtremeStyle();
    CollapseSidebar();
    await ActiveMenuOption();
    await dxLoadPanel.hide();
    await ShowSidebarByUser_Permissions();
});

window.addEventListener("resize", () => {
    CollapseSidebar();
});

function InitializeDevExtremeStyle() {
    DevExpress.config({
        editorStylingMode: "filled", // or 'outlined' | 'underlined',
        forceIsoDateParsing:true
    });
}

// Function to check window width and apply or remove the class
function CollapseSidebar() {
    let windowWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;

    if (windowWidth <= 1024) {
        document.querySelector('#app').classList.add('app-sidebar-minified');
    } else {
        document.querySelector('#app').classList.remove('app-sidebar-minified');
    }
}

async function ActiveMenuOption() {
    //Get path of current page
    let activePage = window.location.pathname;
    //Remove all the info before the last "/"
    let lastIndex = activePage.lastIndexOf('/');
    activePage = activePage.substring(lastIndex + 1);
    //Validate if the page is not the home section
    if (activePage != "") {
        //get the link elements of the menu option
        let navLink = document.querySelectorAll('.menu-item a');
        navLink.forEach(link => {
            //validate each of the link to find the one that has the path of out current page
            if (link.href.includes(`${activePage}`)) {
                //if we find it, we add the active class to its parent element (the div above it)
                link.parentNode.classList.add('active');
                //now, we validate if our element is inside of a submenu of options
                let _subMenu = link.parentNode.parentNode.parentNode;
                //if has the has-sub class, we proceed to open the submenu to show the actual link that is active
                if (_subMenu.className.includes("has-sub")) {
                    let _menuLink = _subMenu.childNodes[1];
                    _menuLink.click();
                }
                //validate if we have another level of submenu
                if (_subMenu.parentNode.parentNode.className.includes("has-sub")) {
                    let _menuLink = _subMenu.parentNode.parentNode.childNodes[1];

                    _menuLink.click();
                }
            }


        });
    }
}

async function ShowSidebarByUser_Permissions()
{
    debugger;
    var _validationResultDTO = await GetUser_PermissionWithUserID();
    if (_validationResultDTO != null)
    {
        var _moduleList = _validationResultDTO.Data
            .filter(up => up.PermissionDTO != null && up.PermissionDTO.Module != null)
            .map(up => up.PermissionDTO.Module);
        let _engineeringList = document.querySelectorAll("[id*='Eng']");
        let _componentIDList = document.querySelectorAll("[id*='ComponentID']");
        let _advancedSettingsList = document.querySelectorAll("[id*='AdvancedSettings']");
        let _orgManagementList = document.querySelectorAll("[id*='OrgManagement']");
        let _eDashboardList = document.querySelectorAll("[id*='EDashboard']");

        _componentIDList.forEach(componentElement => {
            // Get the name of the module that is related to the submenu ID
            let moduleName = componentElement.id.replace("ComponentID", ""); // Extract module name from ID
            // Check if the user has access to that module
            if (!_moduleList.includes(moduleName)) {
                // If the user does not have permission, hide the submenu
                componentElement.style.display = "none";
            }
        });
        _orgManagementList.forEach(componentElement => {
            let moduleName = componentElement.id.replace("OrgManagement", "");
            if (!_moduleList.includes(moduleName)) {
                componentElement.style.display = "none";
            }
        });
        _advancedSettingsList.forEach(componentElement => {
            let moduleName = componentElement.id.replace("AdvancedSettings", "");
            if (!_moduleList.includes(moduleName)) {
                componentElement.style.display = "none";
            }
        });
        _eDashboardList.forEach(componentElement => {
            let moduleName = componentElement.id.replace("EDashboard", "");
            if (!_moduleList.includes(moduleName)) {
                componentElement.style.display = "none";
            }
        });

        // Check if the user has permissions to at least one ComponentID module
        let hasVisibleSubMenuComponentID = Array.from(_componentIDList).some(componentElement => componentElement.style.display !== "none");
        let hasVisibleSubMenuOrgManagement = Array.from(_orgManagementList).some(componentElement => componentElement.style.display !== "none");
        let hasVisibleSubMenuAdvancedSettings = Array.from(_advancedSettingsList).some(componentElement => componentElement.style.display !== "none");
        let hasVisibleSubMenuEDashboard = Array.from(_eDashboardList).some(componentElement => componentElement.style.display !== "none");

        // If you do not have permissions to any ComponentID submenus, hide the Engineering menu
        if (!hasVisibleSubMenuComponentID) {
            _engineeringList.forEach(engElement => {
                engElement.style.display = "none";  // Hide the Engineering menu
            });
        }
        if (!hasVisibleSubMenuOrgManagement) {
            _orgManagementList.forEach(engElement => {
                engElement.style.display = "none";
            });
            document.getElementById("OrgMgmtAdvSettings").style.display = "none";
            if (!hasVisibleSubMenuAdvancedSettings) {
                document.getElementById("AdvSettings").style.display = "none";
            }
        }
        if (!hasVisibleSubMenuEDashboard) {
            document.getElementById("Management").style.display = "none";
            document.getElementById("ManagementE-Dashboard").style.display = "none";
        }
    }
}


