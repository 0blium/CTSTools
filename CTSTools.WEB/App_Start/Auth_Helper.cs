using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CTSTools.WEB.App_Start
{
    public class Auth_Helper
    {
        public static string GetDomainLogin()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static int GetLoggedUserOid()
        {
            string _userLogged = GetDomainLogin();
            var _userDTO = new UserDTO { Login = _userLogged, IsActive = true };
            _userDTO = User_Service.GetUserList_Global(_userDTO).FirstOrDefault();
            return (int)((_userDTO != null) ? _userDTO.ID : 0);
        }



        public static ValidationResultDTO ValidatePermission_Global(string ModuleName, int Action)
        {
            var _userDTO = new UserDTO { ID = GetLoggedUserOid() };
            _userDTO.PermissionDTO.ModuleName = ModuleName;
            _userDTO.PermissionDTO.ActionDTO.ID = Action;
            return Permission_Service.ValidatePermission(_userDTO);
        }


        public static void ValidateUserExists(Page Page)

        {
            var _login = HttpContext.Current.User.Identity.Name;
            var _userDTO = new UserDTO { Login = _login, IsActive = true };
            if (User_Service.GetUserList_Global(_userDTO).Count() == 0)
                Page.Response.Redirect("~/App/Features/Error/ErrorPage.aspx?Error=NEA_E401");
        }


        public static void ValidateAccessPage(Page Page, string ModuleName)
        {
            var _userDTO = new UserDTO { ID = GetLoggedUserOid() };
            _userDTO.PermissionDTO.ModuleName = ModuleName;
            _userDTO.PermissionDTO.ActionDTO.ID = (int)Action_Enum.Read;
            ///Validate if user can view the page
            var _validation_Result = Permission_Service.ValidatePermission(_userDTO);
            if (!_validation_Result.Result) { Page.Response.Redirect("~/App/Features/Error/ErrorPage.aspx?Error=NA_@403"); }
        }
    }
}