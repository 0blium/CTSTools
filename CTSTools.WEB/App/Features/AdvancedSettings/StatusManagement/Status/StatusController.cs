using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.StatusManagement.Status;

public class StatusController : ApiController
{
    [HttpGet]
    [Route("api/Status/GetPagedList")]
    public IHttpActionResult GetStatusPagedList(DataSourceLoadOptions loadOptions, [FromUri] StatusDTO StatusDTO)
    {
        var _pagedStatusDTO = new PagedResultDTO<StatusDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = StatusDTO
        };
        _pagedStatusDTO.DataList = Status_Service.GetStatusList_Global(StatusDTO, _pagedStatusDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedStatusDTO.DataList, loadOptions);
        _dsLoader.totalCount = Status_Service.GetTotalCount(_pagedStatusDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Status/GetList")]
    public IHttpActionResult GetStatusList([FromUri] StatusDTO StatusDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Status_Service.GetStatusList_Global(StatusDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Status/Create")]
    public IHttpActionResult CreateStatus([FromBody] StatusDTO StatusDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Status), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            StatusDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Status_Service.CreateStatus_Global(StatusDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Status/Update")]
    public IHttpActionResult UpdateStatus([FromBody] StatusDTO StatusDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Status), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            StatusDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Status_Service.UpdateStatus_Global(StatusDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Status/Delete")]
    public IHttpActionResult DeleteStatus([FromBody] StatusDTO StatusDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Status), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Status_Service.DeleteStatus_Global(StatusDTO);
        }
        return Json(_validationResultDTO);
    }

}