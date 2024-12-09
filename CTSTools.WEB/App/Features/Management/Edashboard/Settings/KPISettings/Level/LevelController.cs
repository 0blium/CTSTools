using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.Level;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPISettings.Level;

public class LevelController : ApiController
{
    [HttpGet]
    [Route("api/Level/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] LevelDTO LevelDTO)
    {
        var _pagedLevelDTO = new PagedResultDTO<LevelDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = LevelDTO
        };
        _pagedLevelDTO.DataList = Level_Service.GetLevelList_Global(LevelDTO, _pagedLevelDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedLevelDTO.DataList, loadOptions);
        _dsLoader.totalCount = Level_Service.GetLevelTotalCount(_pagedLevelDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Level/GetList")]
    public IHttpActionResult GetLevelList([FromUri] LevelDTO LevelDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Level_Service.GetLevelList_Global(LevelDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Level/Create")]
    public IHttpActionResult CreateLevel([FromBody] LevelDTO LevelDTO)
    {
        LevelDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Level_Service.CreateLevel_Global(LevelDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Level/Update")]
    public IHttpActionResult UpdateLevel([FromBody] LevelDTO LevelDTO)
    {
        LevelDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Level_Service.UpdateLevel_Global(LevelDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Level/Delete")]
    public IHttpActionResult DeleteLevel([FromBody] LevelDTO LevelDTO)
    {
        var _validationResultDTO = Level_Service.DeleteLevel_Global(LevelDTO);
        return Json(_validationResultDTO);
    }
}