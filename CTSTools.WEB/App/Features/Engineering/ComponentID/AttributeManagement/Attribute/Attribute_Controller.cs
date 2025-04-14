using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.Attribute;

public class Attribute_Controller : ApiController
{

    [HttpGet]
    [Route("api/Attribute/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] AttributeDTO AttributeDTO)
    {

        var _pagedAttributeDTO = new PagedResultDTO<AttributeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = AttributeDTO,
        };
        _pagedAttributeDTO.DataList = Attribute_Service.GetAttributeList_Global(AttributeDTO, _pagedAttributeDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedAttributeDTO.DataList, loadOptions);
        _dsLoader.totalCount = Attribute_Service.GetAttributeTotalCount(_pagedAttributeDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Attribute/GetList")]
    public IHttpActionResult GetAttributeList([FromUri] AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Attribute_Service.GetAttributeList_Global(AttributeDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Attribute/Create")]
    public IHttpActionResult CreateAttribute([FromBody] AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Attribute), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            AttributeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Attribute_Service.CreateAttribute_Global(AttributeDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Attribute/CreateMassive")]
    public IHttpActionResult CreateMassiveAttribute([FromBody] FileDTO FileDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Attribute), (int)Action_Enum.Import);
        if (_validationResultDTO.Result)
        {
            FileDTO.ID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Attribute_Service.GenerateAttributeFromExcel(FileDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Attribute/Update")]
    public IHttpActionResult UpdateAttribute([FromBody] AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Attribute), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            AttributeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Attribute_Service.UpdateAttribute_Global(AttributeDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Attribute/Delete")]
    public IHttpActionResult DeleteAttribute([FromBody] AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Attribute), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Attribute_Service.DeleteAttribute_Global(AttributeDTO);
        }
        return Json(_validationResultDTO);
    }
}