using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.Domain;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.UserManagement.Domain;
public class DomainController : ApiController
{

    [HttpGet]
    [Route("api/Domain/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DomainDTO DomainDTO)
    {
        var _pagedDomainDTO = new PagedResultDTO<DomainDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DomainDTO
        };
        _pagedDomainDTO.DataList = Domain_Service.GetDomainList_Global(DomainDTO, _pagedDomainDTO);
        loadOptions.Skip = 0;
        var _dsLoader = DataSourceLoader.Load(_pagedDomainDTO.DataList, loadOptions);
        _dsLoader.totalCount = Domain_Service.GetDomainTotalCount(_pagedDomainDTO);
        return Json(_dsLoader);

    }
    [HttpGet]
    [Route("api/Domain/GetList")]
    public IHttpActionResult GetDomainList([FromUri] DomainDTO DomainDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Domain_Service.GetDomainList_Global(DomainDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Domain/Create")]
    public IHttpActionResult CreateDomain([FromBody] DomainDTO DomainDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Domain), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            DomainDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Domain_Service.CreateDomain_Global(DomainDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Domain/Update")]
    public IHttpActionResult UpdateDomain([FromBody] DomainDTO DomainDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Domain), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            DomainDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Domain_Service.UpdateDomain_Global(DomainDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Domain/Delete")]
    public IHttpActionResult DeleteDomain([FromBody] DomainDTO DomainDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Domain), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Domain_Service.DeleteDomain_Global(DomainDTO);
        }
        return Json(_validationResultDTO);
    }
}