using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.Provider;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.SparePartManagement.Provider;

public class ProviderController : ApiController
{
    [HttpGet]
    [Route("api/Provider/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ProviderDTO ProviderDTO)
    {

        var _pagedProviderDTO = new PagedResultDTO<ProviderDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ProviderDTO
        };
        _pagedProviderDTO.DataList = Provider_Service.GetProviderList_Global(ProviderDTO, _pagedProviderDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedProviderDTO.DataList, loadOptions);
        _dsLoader.totalCount = Provider_Service.GetProviderTotalCount(_pagedProviderDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Provider/GetList")]
    public IHttpActionResult GetProviderList([FromUri] ProviderDTO ProviderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Provider_Service.GetProviderList_Global(ProviderDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Provider/Create")]
    public IHttpActionResult CreateProvider([FromBody] ProviderDTO ProviderDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Provider), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ProviderDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Provider_Service.CreateProvider_Global(ProviderDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Provider/Update")]
    public IHttpActionResult UpdateProvider([FromBody] ProviderDTO ProviderDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Provider), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ProviderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Provider_Service.UpdateProvider_Global(ProviderDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Provider/Delete")]
    public IHttpActionResult DeleteProvider([FromBody] ProviderDTO ProviderDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Provider), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            ProviderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Provider_Service.DeleteProvider_Global(ProviderDTO);
        }
        return Json(_validationResultDTO);
    }
}