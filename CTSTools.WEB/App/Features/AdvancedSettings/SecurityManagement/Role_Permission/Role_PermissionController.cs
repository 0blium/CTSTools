using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Security.Permissions.Role_Permission;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.Role_Permission;

public class Role_PermissionController : ApiController
{
    [HttpGet]
    [Route("api/Role_Permission/GetPagedList")]
    public IHttpActionResult GetRole_PermissionPagedList(DataSourceLoadOptions loadOptions, [FromUri] Role_PermissionDTO Role_PermissionDTO)
    {
        var _pagedRole_PermissionDTO = new PagedResultDTO<Role_PermissionDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = Role_PermissionDTO
        };
        _pagedRole_PermissionDTO.DataList = Role_Permission_Service.GetRole_PermissionList_Global(Role_PermissionDTO, _pagedRole_PermissionDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedRole_PermissionDTO.DataList, loadOptions);
        _dsLoader.totalCount = Role_Permission_Service.GetRole_PermissionTotalCount(_pagedRole_PermissionDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Role_Permission/GetList")]
    public IHttpActionResult GetRole_PermissionList([FromUri] Role_PermissionDTO Role_PermissionDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Role_Permission_Service.GetRole_PermissionList_Global(Role_PermissionDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Role_Permission/Create")]
    public IHttpActionResult CreateRole_Permission([FromBody] Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Role_Permission), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            Role_PermissionDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Role_Permission_Service.CreateRole_PermissionByArray(Role_PermissionDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Role_Permission/Update")]
    public IHttpActionResult UpdateRole_Permission([FromBody] Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Role_Permission), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Role_PermissionDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Role_Permission_Service.UpdateRole_Permission_Global(Role_PermissionDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Role_Permission/Delete")]
    public IHttpActionResult DeleteRole_Permission([FromBody] Role_PermissionDTO Role_PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Role_Permission), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Role_Permission_Service.DeleteRole_Permission_Global(Role_PermissionDTO);
        }
        return Json(_validationResultDTO);
    }
}