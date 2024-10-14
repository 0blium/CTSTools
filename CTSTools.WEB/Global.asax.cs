using System;
using System.Web;
using System.Web.Http;

namespace CTSTools
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //// Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //// Update ORM/DB Schema
            //BLL.Features.XPO.ORM_Helper.UpdateSchema();
        }
    }
}