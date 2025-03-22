using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.Tickets.Priority;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Ticket.Tickets.Priority;

public class PriorityController : ApiController
{
    [HttpGet]
    [Route("api/Priority/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] PriorityDTO PriorityDTO)
    {

        var _pagedPriorityDTO = new PagedResultDTO<PriorityDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = PriorityDTO
        };
        _pagedPriorityDTO.DataList = Priority_Service.GetPriorityList_Global(PriorityDTO, _pagedPriorityDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedPriorityDTO.DataList, loadOptions);
        _dsLoader.totalCount = Priority_Service.GetPriorityTotalCount(_pagedPriorityDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Priority/GetList")]
    public IHttpActionResult GetPriorityList([FromUri] PriorityDTO PriorityDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Priority_Service.GetPriorityList_Global(PriorityDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Priority/Create")]
    public IHttpActionResult CreatePriority([FromBody] PriorityDTO PriorityDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(PriorityDTO.SupportGroupDTO, nameof(Priority), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            PriorityDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
             _validationResultDTO = Priority_Service.CreatePriority_Global(PriorityDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Priority/Update")]
    public IHttpActionResult UpdatePriority([FromBody] PriorityDTO PriorityDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(PriorityDTO.SupportGroupDTO, nameof(Priority), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            PriorityDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
             _validationResultDTO = Priority_Service.UpdatePriority_Global(PriorityDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Priority/Delete")]
    public IHttpActionResult DeletePriority([FromBody] PriorityDTO PriorityDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(PriorityDTO.SupportGroupDTO, nameof(Priority), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            PriorityDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
             _validationResultDTO = Priority_Service.DeletePriority_Global(PriorityDTO);
        }
        return Json(_validationResultDTO);
    }
}