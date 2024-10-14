export const dxLoadPanel = $("#dxLoader").dxLoadPanel({
    shadingColor: "rgba(0,0,0,0.4)",
    visible: false,
    showIndicator: true,
    showPane: true,
    shading: true,
    closeOnOutsideClick: false,
    message: "Loading information, please wait a minute."
}).dxLoadPanel('instance');