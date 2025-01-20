using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.Value;
public class Value_Controller : ApiController
{

    [HttpGet]
    [Route("api/Value/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ValueDTO ValueDTO)
    {

        var _pagedValueDTO = new PagedResultDTO<ValueDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ValueDTO
        };
        _pagedValueDTO.DataList = Value_Service.GetValueList_Global(ValueDTO, _pagedValueDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedValueDTO.DataList, loadOptions);
        _dsLoader.totalCount = Value_Service.GetValueTotalCount(_pagedValueDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Value/GetList")]
    public IHttpActionResult GetValueList([FromUri] ValueDTO ValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Value_Service.GetValueList_Global(ValueDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Value/Create")]
    public IHttpActionResult CreateValue([FromBody] ValueDTO ValueDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Value), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ValueDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Value_Service.CreateValue_Global(ValueDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Value/CreateMassive")]
    public IHttpActionResult CreateMassiveValue([FromBody] FileDTO FileDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Value), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            FileDTO.ID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Value_Service.ValueFileValidation_Global(FileDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Value/Update")]
    public IHttpActionResult UpdateValue([FromBody] ValueDTO ValueDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Value), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ValueDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Value_Service.UpdateValue_Global(ValueDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Value/Delete")]
    public IHttpActionResult DeleteValue([FromBody] ValueDTO ValueDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Value), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Value_Service.DeleteValue_Global(ValueDTO);
        }
        return Json(_validationResultDTO);
    }
}