using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.StatusType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.StatusManagement.StatusType;

public class StatusTypeController : ApiController
{
    [HttpGet]
    [Route("api/StatusType/GetPagedList")]
    public IHttpActionResult GetStatusTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] StatusTypeDTO StatusTypeDTO)
    {
        var _pagedStatusTypeDTO = new PagedResultDTO<StatusTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = StatusTypeDTO
        };
        _pagedStatusTypeDTO.DataList = StatusType_Service.GetStatusTypeList_Global(StatusTypeDTO, _pagedStatusTypeDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedStatusTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = StatusType_Service.GetTotalCount(_pagedStatusTypeDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/StatusType/GetList")]
    public IHttpActionResult GetStatusTypeList([FromUri] StatusTypeDTO StatusTypeDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = StatusType_Service.GetStatusTypeList_Global(StatusTypeDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/StatusType/Create")]
    public IHttpActionResult CreateStatusType([FromBody] StatusTypeDTO StatusTypeDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(BLL.Features.Statuses.StatusType), (int)Action_Enum.Create);
        //if (_validationResultDTO.Result)
        //{
        StatusTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = StatusType_Service.CreateStatusType_Global(StatusTypeDTO);
        //}
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/StatusType/Update")]
    public IHttpActionResult UpdateStatusType([FromBody] StatusTypeDTO StatusTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(StatusType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            StatusTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = StatusType_Service.UpdateStatusType_Global(StatusTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/StatusType/Delete")]
    public IHttpActionResult DeleteStatusType([FromBody] StatusTypeDTO StatusTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(StatusType), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = StatusType_Service.DeleteStatusType_Global(StatusTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}