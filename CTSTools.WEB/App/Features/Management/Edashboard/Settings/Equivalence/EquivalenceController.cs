using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.Equivalence;

public class EquivalenceController : ApiController
{
    [HttpGet]
    [Route("api/Equivalence/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] EquivalenceDTO EquivalenceDTO)
    {
        var _pagedEquivalenceDTO = new PagedResultDTO<EquivalenceDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = EquivalenceDTO
        };
        _pagedEquivalenceDTO.DataList = Equivalence_Service.GetEquivalenceList_Global(EquivalenceDTO, _pagedEquivalenceDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedEquivalenceDTO.DataList, loadOptions);
        _dsLoader.totalCount = Equivalence_Service.GetEquivalenceTotalCount(_pagedEquivalenceDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Equivalence/GetList")]
    public IHttpActionResult GetEquivalenceList([FromUri] EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Equivalence_Service.GetEquivalenceList_Global(EquivalenceDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Equivalence/Create")]
    public IHttpActionResult CreateEquivalence([FromBody] EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Equivalence), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            EquivalenceDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Equivalence_Service.CreateEquivalence_Global(EquivalenceDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Equivalence/Update")]
    public IHttpActionResult UpdateEquivalence([FromBody] EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Equivalence), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            EquivalenceDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Equivalence_Service.UpdateEquivalence_Global(EquivalenceDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Equivalence/Delete")]
    public IHttpActionResult DeleteEquivalence([FromBody] EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Equivalence), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Equivalence_Service.DeleteEquivalence_Global(EquivalenceDTO);
        }
        return Json(_validationResultDTO);
    }
}