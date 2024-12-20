using CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement;
using CTSTools.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.PartManagement
{
    public partial class PartList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Auth_Helper.ValidateAccessPage(Page, nameof(PartList));
        }
    }
}