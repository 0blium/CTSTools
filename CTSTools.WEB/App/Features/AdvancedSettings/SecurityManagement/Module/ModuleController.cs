using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.Module;

public class ModuleController : ApiController
{
    [HttpGet]
    [Route("api/Module/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ModuleDTO ModuleDTO)
    {
        var _pagedModuleDTO = new PagedResultDTO<ModuleDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ModuleDTO
        };
        _pagedModuleDTO.DataList = Module_Service.GetModuleList_Global(ModuleDTO, _pagedModuleDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedModuleDTO.DataList, loadOptions);
        _dsLoader.totalCount = Module_Service.GetModuleTotalCount(_pagedModuleDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Module/GetList")]
    public IHttpActionResult GetModuleList([FromUri] ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Module_Service.GetModuleList_Global(ModuleDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Module/Create")]
    public IHttpActionResult CreateModule([FromBody] ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Module), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ModuleDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Module_Service.CreateModule_Global(ModuleDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Module/AdvancedSetUp")]
    public IHttpActionResult AdvancedModuleSetUp([FromBody] ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Module), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ModuleDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Module_Service.CreateAdvancedModule_Global(ModuleDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Module/Update")]
    public IHttpActionResult UpdateModule([FromBody] ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Module), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ModuleDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Module_Service.UpdateModule_Global(ModuleDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Module/UpdateSetUp")]
    public IHttpActionResult UpdateModuleSetUp([FromBody] ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Module), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ModuleDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Module_Service.UpdateSetUpModule_Global(ModuleDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Module/Delete")]
    public IHttpActionResult DeleteModule([FromBody] ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Module), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Module_Service.DeleteModule_Global(ModuleDTO);
        }
        return Json(_validationResultDTO);
    }
}