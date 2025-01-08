using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.Settings.UnitOfMeasure;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.UnitOfMeasure;

public class UnitOfMeasureController : ApiController
{
    [HttpGet]
    [Route("api/UnitOfMeasure/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] UnitOfMeasureDTO UnitOfMeasureDTO)
    {

        var _pagedUnitOfMeasureDTO = new PagedResultDTO<UnitOfMeasureDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = UnitOfMeasureDTO
        };
        _pagedUnitOfMeasureDTO.DataList = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(UnitOfMeasureDTO, _pagedUnitOfMeasureDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedUnitOfMeasureDTO.DataList, loadOptions);
        _dsLoader.totalCount = UnitOfMeasure_Service.GetUnitOfMeasureTotalCount(_pagedUnitOfMeasureDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/UnitOfMeasure/GetList")]
    public IHttpActionResult GetUnitOfMeasureList([FromUri] UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = UnitOfMeasure_Service.GetUnitOfMeasureList_Global(UnitOfMeasureDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UnitOfMeasure/Create")]
    public IHttpActionResult CreateUnitOfMeasure([FromBody] UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        //test comment
        //other test
        UnitOfMeasureDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = UnitOfMeasure_Service.CreateUnitOfMeasure_Global(UnitOfMeasureDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/UnitOfMeasure/Update")]
    public IHttpActionResult UpdateUnitOfMeasure([FromBody] UnitOfMeasureDTO UnitOfMeasureDTO)
    {

        UnitOfMeasureDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = UnitOfMeasure_Service.UpdateUnitOfMeasure_Global(UnitOfMeasureDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/UnitOfMeasure/Delete")]
    public IHttpActionResult DeleteUnitOfMeasure([FromBody] UnitOfMeasureDTO UnitOfMeasureDTO)
    {

        var _validationResultDTO = UnitOfMeasure_Service.DeleteUnitOfMeasure_Global(UnitOfMeasureDTO);
        return Json(_validationResultDTO);
    }
}