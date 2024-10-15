using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.Action;

public class ActionController : ApiController
{
    [HttpGet]
    [Route("api/Action/GetPagedList")]
    public IHttpActionResult GetActionPagedList(DataSourceLoadOptions loadOptions, [FromUri] ActionDTO ActionDTO)
    {
        var _pagedActionDTO = new PagedResultDTO<ActionDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ActionDTO
        };
        _pagedActionDTO.DataList = Action_Service.GetActionList_Global(ActionDTO, _pagedActionDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedActionDTO.DataList, loadOptions);
        _dsLoader.totalCount = Action_Service.GetActionTotalCount(_pagedActionDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Action/GetList")]
    public IHttpActionResult GetActionList([FromUri] ActionDTO ActionDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Action_Service.GetActionList_Global(ActionDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Action/Create")]
    public IHttpActionResult CreateAction([FromBody] ActionDTO ActionDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Action), (int)Action_Enum.Create);
        //if (_validationResultDTO.Result)
        //{
        ActionDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Action_Service.CreateAction_Global(ActionDTO);
        //}
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Action/Update")]
    public IHttpActionResult UpdateAction([FromBody] ActionDTO ActionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Action), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ActionDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Action_Service.UpdateAction_Global(ActionDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Action/Delete")]
    public IHttpActionResult DeleteAction([FromBody] ActionDTO ActionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Action), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Action_Service.DeleteAction_Global(ActionDTO);
        }
        return Json(_validationResultDTO);
    }
}