using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;


namespace CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.ValueLink;

public class ValueLink_Controller : ApiController
{

    [HttpGet]
    [Route("api/ValueLink/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ValueLinkDTO ValueLinkDTO)
    {
        var _pagedValueLinkDTO = new PagedResultDTO<ValueLinkDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ValueLinkDTO
        };
        _pagedValueLinkDTO.DataList = ValueLink_Service.GetValueLinkList_Global(ValueLinkDTO, _pagedValueLinkDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedValueLinkDTO.DataList, loadOptions);
        _dsLoader.totalCount = ValueLink_Service.GetValueLinkTotalCount(_pagedValueLinkDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/ValueLink/GetList")]
    public IHttpActionResult GetValueLinkList([FromUri] ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = ValueLink_Service.GetValueLinkList_Global(ValueLinkDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/ValueLink/Create")]
    public IHttpActionResult CreateValueLink([FromBody] ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ValueLink), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ValueLinkDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = ValueLink_Service.CreateValueLink_Global(ValueLinkDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ValueLink/Update")]
    public IHttpActionResult UpdateValueLink([FromBody] ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ValueLink), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ValueLinkDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = ValueLink_Service.UpdateValueLink_Global(ValueLinkDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ValueLink/CreateMultiple")]
    public IHttpActionResult CreateMultipleValueLink([FromBody] ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ValueLink), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ValueLinkDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = ValueLink_Service.CreateMultipleValueLink(ValueLinkDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ValueLink/Delete")]
    public IHttpActionResult DeleteValueLink([FromBody] ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ValueLink), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = ValueLink_Service.DeleteValueLink_Global(ValueLinkDTO);
        }
        return Json(_validationResultDTO);
    }
}