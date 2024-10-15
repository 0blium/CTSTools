using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Facility;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.LocationManagement.Facility;

public class FacilityController : ApiController
{
    [HttpGet]
    [Route("api/Facility/GetPagedList")]
    public IHttpActionResult GetFacilityPagedList(DataSourceLoadOptions loadOptions, [FromUri] FacilityDTO FacilityDTO)
    {
        var _pagedFacilityDTO = new PagedResultDTO<FacilityDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = FacilityDTO
        };
        _pagedFacilityDTO.DataList = Facility_Service.GetFacilityList_Global(FacilityDTO, _pagedFacilityDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedFacilityDTO.DataList, loadOptions);
        _dsLoader.totalCount = Facility_Service.GetTotalCount(_pagedFacilityDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Facility/GetList")]
    public IHttpActionResult GetFacilityList([FromUri] FacilityDTO FacilityDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Facility_Service.GetFacilityList_Global(FacilityDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Facility/Create")]
    public IHttpActionResult CreateFacility([FromBody] FacilityDTO FacilityDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Facility), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            FacilityDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Facility_Service.CreateFacility_Global(FacilityDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Facility/Update")]
    public IHttpActionResult UpdateFacility([FromBody] FacilityDTO FacilityDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Facility), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            FacilityDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Facility_Service.UpdateFacility_Global(FacilityDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Facility/Delete")]
    public IHttpActionResult DeleteFacility([FromBody] FacilityDTO FacilityDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Facility), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Facility_Service.DeleteFacility_Global(FacilityDTO);
        }
        return Json(_validationResultDTO);
    }
}