using CTSTools.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CTSTools.WEB.App.Features.MasterPage
{
    public partial class CTSTools : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["_userID"] = null;
            }
            if (Session["_userID"] == null)
            {
                Auth_Helper.ValidateUserExists(Page);
                Session["_userID"] = Auth_Helper.GetLoggedUserOid();
                Session["_userLogin"] = Auth_Helper.GetDomainLogin();
            }
        }
    }
}