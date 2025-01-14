using CTSTools.WEB.App_Start;
using System;


namespace CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement;

public partial class RoleAdministration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth_Helper.ValidateAccessPage(Page, nameof(RoleAdministration));
    }
}