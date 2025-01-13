using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Role;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.UserManagement.User_Role;
public class User_RoleController : ApiController
{
    [HttpGet]
    [Route("api/User_Role/GetPagedList")]
    public IHttpActionResult GetUser_RolePagedList(DataSourceLoadOptions loadOptions, [FromUri] User_RoleDTO User_RoleDTO)
    {
        var _pagedRoleDTO = new PagedResultDTO<User_RoleDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = User_RoleDTO
        };
        _pagedRoleDTO.DataList = User_Role_Service.GetUser_RoleList_Global(User_RoleDTO, _pagedRoleDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedRoleDTO.DataList, loadOptions);
        _dsLoader.totalCount = User_Role_Service.GetUser_RoleTotalCount(_pagedRoleDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/User_Role/GetList")]
    public IHttpActionResult GetUser_RoleList([FromUri] User_RoleDTO User_RoleDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = User_Role_Service.GetUser_RoleList_Global(User_RoleDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/User_Role/Create")]
    public IHttpActionResult CreateUser_Role([FromBody] User_RoleDTO User_RoleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(User_Role), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            User_RoleDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = User_Role_Service.CreateUser_Role_Global(User_RoleDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/User_Role/Delete")]
    public IHttpActionResult DeleteUser_Role([FromBody] User_RoleDTO User_RoleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(User_Role), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = User_Role_Service.DeleteUser_Role_Global(User_RoleDTO);
        }
        return Json(_validationResultDTO);
    }
}