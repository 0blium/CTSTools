using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Security.Permissions.Permission;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.Permission;

public class PermissionController : ApiController
{
    [HttpGet]
    [Route("api/Permission/GetPagedList")]
    public IHttpActionResult GetPermissionPagedList(DataSourceLoadOptions loadOptions, [FromUri] PermissionDTO PermissionDTO)
    {
        var _pagedPermissionDTO = new PagedResultDTO<PermissionDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = PermissionDTO
        };
        _pagedPermissionDTO.DataList = Permission_Service.GetPermissionList_Global(PermissionDTO, _pagedPermissionDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedPermissionDTO.DataList, loadOptions);
        _dsLoader.totalCount = Permission_Service.GetPermissionTotalCount(_pagedPermissionDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Permission/GetList")]
    public IHttpActionResult GetPermissionList([FromUri] PermissionDTO PermissionDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Permission_Service.GetPermissionList_Global(PermissionDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Permission/Create")]
    public IHttpActionResult CreatePermission([FromBody] PermissionDTO PermissionDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Permission), (int)Action_Enum.Create);
        //if (_validationResultDTO.Result)
        //{
        PermissionDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Permission_Service.CreatePermission_Global(PermissionDTO);
        //}
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Permission/Update")]
    public IHttpActionResult UpdatePermission([FromBody] PermissionDTO PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Permission), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            PermissionDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Permission_Service.UpdatePermission_Global(PermissionDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Permission/Delete")]
    public IHttpActionResult DeletePermission([FromBody] PermissionDTO PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Permission), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Permission_Service.DeletePermission_Global(PermissionDTO);
        }
        return Json(_validationResultDTO);
    }
}