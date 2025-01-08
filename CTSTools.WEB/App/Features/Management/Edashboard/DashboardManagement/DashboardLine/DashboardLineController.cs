using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardLine;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.DashboardLine;

public class DashboardLineController : ApiController
{
    [HttpGet]
    [Route("api/DashboardLine/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DashboardLineDTO DashboardLineDTO)
    {

        var _pagedDashboardLineDTO = new PagedResultDTO<DashboardLineDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DashboardLineDTO
        };
        _pagedDashboardLineDTO.DataList = DashboardLine_Service.GetDashboardLineList_Global(DashboardLineDTO, _pagedDashboardLineDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDashboardLineDTO.DataList, loadOptions);
        _dsLoader.totalCount = DashboardLine_Service.GetDashboardLineTotalCount(_pagedDashboardLineDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/DashboardLine/GetDashboardLineList")]
    public IHttpActionResult GetDashboardLineList([FromUri] DashboardLineDTO DashboardLineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DashboardLine_Service.GetDashboardLineList_Global(DashboardLineDTO);
        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/DashboardLine/GetDashboard_KPITendence")]
    public IHttpActionResult GetDashboardMetricTendence([FromUri] DashboardLineDTO DashboardLineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DashboardLine_Service.GetDashboard_KPITendence(DashboardLineDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardLine/Create")]
    public IHttpActionResult CreateDashboardLine([FromBody] DashboardLineDTO DashboardLineDTO)
    {

        DashboardLineDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardLine_Service.CreateDashboardLine_Global(DashboardLineDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardLine/Update")]
    public IHttpActionResult UpdateDashboardLine([FromBody] DashboardLineDTO DashboardLineDTO)
    {

        DashboardLineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardLine_Service.UpdateDashboardLine_Global(DashboardLineDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardLine/Delete")]
    public IHttpActionResult DeleteDashboardLine([FromBody] DashboardLineDTO DashboardLineDTO)
    {
        DashboardLineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardLine_Service.DeleteDashboardLine_Global(DashboardLineDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DashboardLine/AddMonthlyValue")]
    public ValidationResultDTO AddMonthlyValue([FromBody] DashboardLineDTO DashboardLineDTO)
    {
        DashboardLineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        return DashboardLine_Service.AddMonthlyValue(DashboardLineDTO);
    }

}