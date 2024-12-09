using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.GoalRange;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPISettings.GoalRange;

public class GoalRangeController : ApiController
{
    [HttpGet]
    [Route("api/GoalRange/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] GoalRangeDTO GoalRangeDTO)
    {

        var _pagedGoalRangeDTO = new PagedResultDTO<GoalRangeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = GoalRangeDTO
        };
        _pagedGoalRangeDTO.DataList = GoalRange_Service.GetGoalRangeList_Global(GoalRangeDTO, _pagedGoalRangeDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedGoalRangeDTO.DataList, loadOptions);
        _dsLoader.totalCount = GoalRange_Service.GetGoalRangeTotalCount(_pagedGoalRangeDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/GoalRange/GetList")]
    public IHttpActionResult GetGoalRangeList([FromUri] GoalRangeDTO GoalRangeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = GoalRange_Service.GetGoalRangeList_Global(GoalRangeDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/GoalRange/Create")]
    public IHttpActionResult CreateGoalRange([FromBody] GoalRangeDTO GoalRangeDTO)
    {

        GoalRangeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = GoalRange_Service.CreateGoalRange_Global(GoalRangeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/GoalRange/Update")]
    public IHttpActionResult UpdateGoalRange([FromBody] GoalRangeDTO GoalRangeDTO)
    {

        GoalRangeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = GoalRange_Service.UpdateGoalRange_Global(GoalRangeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/GoalRange/Delete")]
    public IHttpActionResult DeleteGoalRange([FromBody] GoalRangeDTO GoalRangeDTO)
    {

        var _validationResultDTO = GoalRange_Service.DeleteGoalRange_Global(GoalRangeDTO);
        return Json(_validationResultDTO);
    }
}