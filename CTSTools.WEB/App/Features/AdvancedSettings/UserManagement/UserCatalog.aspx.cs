using CTSTools.WEB.App.Features.AdvancedSettings;
using CTSTools.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTSTools.WEB.App.Features.AdvancedSettings.UserManagement;

public partial class UserCatalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Auth_Helper.ValidateAccessPage(Page, nameof(UserCatalog));
    }
}