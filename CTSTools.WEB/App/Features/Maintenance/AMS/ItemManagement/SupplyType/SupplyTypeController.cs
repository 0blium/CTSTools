using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.SupplyType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.SupplyType;

public class SupplyTypeController : ApiController
{
    [HttpGet]
    [Route("api/SupplyType/GetPagedList")]
    public IHttpActionResult GetSupplyTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] SupplyTypeDTO SupplyTypeDTO)
    {
        var _pagedSupplyTypeDTO = new PagedResultDTO<SupplyTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SupplyTypeDTO
        };
        _pagedSupplyTypeDTO.DataList = SupplyType_Service.GetSupplyTypeList_Global(SupplyTypeDTO, _pagedSupplyTypeDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSupplyTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = SupplyType_Service.GetSupplyTypeTotalCount(_pagedSupplyTypeDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/SupplyType/GetList")]
    public IHttpActionResult GetSupplyTypeList([FromUri] SupplyTypeDTO SupplyTypeDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SupplyType_Service.GetSupplyTypeList_Global(SupplyTypeDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SupplyType/Create")]
    public IHttpActionResult CreateSupplyType([FromBody] SupplyTypeDTO SupplyTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SupplyType), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SupplyTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SupplyType_Service.CreateSupplyType_Global(SupplyTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SupplyType/Update")]
    public IHttpActionResult UpdateSupplyType([FromBody] SupplyTypeDTO SupplyTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SupplyType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SupplyTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SupplyType_Service.UpdateSupplyType_Global(SupplyTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SupplyType/Delete")]
    public IHttpActionResult DeleteSupplyType([FromBody] SupplyTypeDTO SupplyTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SupplyType), (int)Action_Enum.Delete);

        if (_validationResultDTO.Result)
        {
            _validationResultDTO = SupplyType_Service.DeleteSupplyType_Global(SupplyTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}