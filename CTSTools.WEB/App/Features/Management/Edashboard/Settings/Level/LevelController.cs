using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Level;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.Level;

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
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Level), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            LevelDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Level_Service.CreateLevel_Global(LevelDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Level/Update")]
    public IHttpActionResult UpdateLevel([FromBody] LevelDTO LevelDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Level), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            LevelDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Level_Service.UpdateLevel_Global(LevelDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Level/Delete")]
    public IHttpActionResult DeleteLevel([FromBody] LevelDTO LevelDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Level), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Level_Service.DeleteLevel_Global(LevelDTO);
        }
        return Json(_validationResultDTO);
    }
}