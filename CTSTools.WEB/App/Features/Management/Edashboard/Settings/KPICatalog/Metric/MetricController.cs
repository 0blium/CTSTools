using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Metric;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPICatalog.Metric;

public class MetricController : ApiController
{
    [HttpGet]
    [Route("api/Metric/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] MetricDTO MetricDTO)
    {

        var _pagedMetricDTO = new PagedResultDTO<MetricDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = MetricDTO
        };
        _pagedMetricDTO.DataList = Metric_Service.GetMetricList_Global(MetricDTO, _pagedMetricDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedMetricDTO.DataList, loadOptions);
        _dsLoader.totalCount = Metric_Service.GetMetricTotalCount(_pagedMetricDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Metric/GetMetricList")]
    public IHttpActionResult GetMetricList([FromUri] MetricDTO MetricDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Metric_Service.GetMetricList_Global(MetricDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Metric/Create")]
    public IHttpActionResult CreateMetric([FromBody] MetricDTO MetricDTO)
    {

        MetricDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Metric_Service.CreateMetric_Global(MetricDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Metric/Update")]
    public IHttpActionResult UpdateMetric([FromBody] MetricDTO MetricDTO)
    {

        MetricDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Metric_Service.UpdateMetric_Global(MetricDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Metric/Delete")]
    public IHttpActionResult DeleteMetric([FromBody] MetricDTO MetricDTO)
    {

        var _validationResultDTO = Metric_Service.DeleteMetric_Global(MetricDTO);
        return Json(_validationResultDTO);
    }
}