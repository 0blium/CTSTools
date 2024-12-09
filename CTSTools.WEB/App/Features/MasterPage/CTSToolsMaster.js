//const { dxLoadPanel } = import("../../Common/Components/dxLoadPanel");
import { dxLoadPanel } from "../../Common/Components/dxLoadPanel.js"

document.addEventListener("DOMContentLoaded", async () => {
    await dxLoadPanel.show();
    InitializeDevExtremeStyle();
    CollapseSidebar();
    await ActiveMenuOption();
    await dxLoadPanel.hide();
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




