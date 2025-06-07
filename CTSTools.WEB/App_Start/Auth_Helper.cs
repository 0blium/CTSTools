using AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;
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


        //Imported from AMS Ticket
        public static ValidationResultDTO ValidateSupportGroupMemberPermissions_Global(SupportGroupDTO SupportGroupDTO, string ModuleName, int Action)
        {
            var _validationResultDTO = ValidatePermission_Global(ModuleName, Action);
            if (_validationResultDTO.Result)
            {
                var _userDTO = new UserDTO { ID = GetLoggedUserOid() };
                _userDTO.PermissionDTO = Permission_Service.GetPermissionList_Global(new PermissionDTO { ModuleName = ModuleName, ActionDTO = new ActionDTO { ID = Action }, GetPermissionIDArray = true }).FirstOrDefault();
                var _supportGroupMemberDTO = new SupportGroupMemberDTO
                {
                    SupportGroupDTO = SupportGroupDTO,
                    UserDTO = _userDTO
                };
                _validationResultDTO = SupportGroupMember_Service.ValidateSupportGroupMemberPermissions(_supportGroupMemberDTO);
            }
            return _validationResultDTO;

        }

        public static ValidationResultDTO SpecialUpdateItemLineValidation(Item_LineDTO Item_LineDTO, string ModuleName, int Action)
        {
            var _validationResultDTO = Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO.ID != null ?
               ValidateSupportGroupMemberPermissions_Global(Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO, ModuleName, Action)
               : ValidatePermission_Global(ModuleName, Action);
            //Last validation, validate if owner of item 
            if (_validationResultDTO.Result == false)
            {
                _validationResultDTO = Item_Line_Validator.Item_lineOwnerValidation(
                    new Item_LineDTO
                    {
                        ID = Item_LineDTO.ID,
                        OwnerDTO = new UserDTO { ID = GetLoggedUserOid() }
                    });
            }
            return _validationResultDTO;
        }
    }
}