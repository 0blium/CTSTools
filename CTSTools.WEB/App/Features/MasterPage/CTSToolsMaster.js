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
async function ShowSidebarByUser_Permissions() {
    debugger;
    var _validationResultDTO = await GetUser_PermissionWithUserID();
    if (_validationResultDTO != null) {
        var _moduleList = _validationResultDTO.Data
            .filter(up => up.PermissionDTO != null && up.PermissionDTO.Module != null)
            .map(up => up.PermissionDTO.Module);

        // Define element groups
        const groups = [
            { selector: "[id*='Eng']", modulePrefix: "Eng" },
            { selector: "[id*='ComponentID']", modulePrefix: "ComponentID" },
            { selector: "[id*='AdvancedSettings']", modulePrefix: "AdvancedSettings" },
            { selector: "[id*='OrgManagement']", modulePrefix: "OrgManagement" },
            { selector: "[id*='EDashboard']", modulePrefix: "EDashboard" }
        ];

        // Hide/show submenus depending on permissions
        let visibleGroups = {};
        groups.forEach(group => {
            debugger;
            let elements = document.querySelectorAll(group.selector);
            let hasVisibleItems = false;  // We initialize as false
            // We use .forEach() to loop through all the elements in the group
            Array.from(elements).forEach(element => {
                let moduleName = element.id.replace(group.modulePrefix, "");
                let hasPermission = _moduleList.includes(moduleName);
                element.hidden = !hasPermission;  // We hide if you do not have permission
                if (hasPermission) {
                    hasVisibleItems = true;  // If at least one has permissions, we mark the group as visible
                }
            });
            // We save if the group has visible elements
            visibleGroups[group.modulePrefix] = hasVisibleItems;
        });

        // Check visibility and hide main elements if necessary
        // Hide/show the Engineering menu if it has no visible items
        if (!visibleGroups["ComponentID"]) {
            document.querySelector("[id='Engineering']").hidden = true;
        } else {
            document.querySelector("[id='Engineering']").hidden = false;
            document.querySelector("[id='ComponentIDEng']").hidden = false;
        }

        // Hide/show the Organization menu if it has no visible items
        if (!visibleGroups["OrgManagement"]) {
            document.querySelector("[id='OrgMgmtAdvSettings']").hidden = true;
            if (!visibleGroups["AdvancedSettings"]) {
                document.querySelector("[id='AdvSettings']").hidden = true;
            } else {
                document.querySelector("[id='AdvSettings']").hidden = false;
            }
        } else {
            document.querySelector("[id='OrgMgmtAdvSettings']").hidden = false;
            document.querySelector("[id='AdvSettings']").hidden = !visibleGroups["OrgManagement"];
        }

        // Hide/show EDashboard menu if it has no visible items
        if (!visibleGroups["EDashboard"]) {
            document.querySelector("[id='Management']").hidden = true;
            document.querySelector("[id='ManagementE-Dashboard']").hidden = true;
        } else {
            document.querySelector("[id='Management']").hidden = false;
            document.querySelector("[id='ManagementE-Dashboard']").hidden = false;
        }
    }
}


