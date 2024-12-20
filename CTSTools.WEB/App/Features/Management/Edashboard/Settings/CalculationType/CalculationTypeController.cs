using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.CalculationType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPISettings.CalculationType;

public class CalculationTypeController : ApiController
{
    [HttpGet]
    [Route("api/CalculationType/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] CalculationTypeDTO CalculationTypeDTO)
    {
        var _pagedCalculationTypeDTO = new PagedResultDTO<CalculationTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = CalculationTypeDTO
        };
        _pagedCalculationTypeDTO.DataList = CalculationType_Service.GetCalculationTypeList_Global(CalculationTypeDTO, _pagedCalculationTypeDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedCalculationTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = CalculationType_Service.GetCalculationTypeTotalCount(_pagedCalculationTypeDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/CalculationType/GetList")]
    public IHttpActionResult GetCalculationTypeList([FromUri] CalculationTypeDTO CalculationTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = CalculationType_Service.GetCalculationTypeList_Global(CalculationTypeDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/CalculationType/Create")]
    public IHttpActionResult CreateCalculationType([FromBody] CalculationTypeDTO CalculationTypeDTO)
    {
        CalculationTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = CalculationType_Service.CreateCalculationType_Global(CalculationTypeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/CalculationType/Update")]
    public IHttpActionResult UpdateCalculationType([FromBody] CalculationTypeDTO CalculationTypeDTO)
    {
        CalculationTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = CalculationType_Service.UpdateCalculationType_Global(CalculationTypeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/CalculationType/Delete")]
    public IHttpActionResult DeleteCalculationType([FromBody] CalculationTypeDTO CalculationTypeDTO)
    {
        var _validationResultDTO = CalculationType_Service.DeleteCalculationType_Global(CalculationTypeDTO);
        return Json(_validationResultDTO);
    }
}