using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.Settings.ValueType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPISettings.ValueType;

public class ValueTypeController : ApiController
{
    [HttpGet]
    [Route("api/ValueType/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ValueTypeDTO ValueTypeDTO)
    {
        var _pagedValueTypeDTO = new PagedResultDTO<ValueTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ValueTypeDTO
        };
        _pagedValueTypeDTO.DataList = ValueType_Service.GetValueTypeList_Global(ValueTypeDTO, _pagedValueTypeDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedValueTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = ValueType_Service.GetValueTypeTotalCount(_pagedValueTypeDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/ValueType/GetList")]
    public IHttpActionResult GetValueTypeList([FromUri] ValueTypeDTO ValueTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = ValueType_Service.GetValueTypeList_Global(ValueTypeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ValueType/Create")]
    public IHttpActionResult CreateValueType([FromBody] ValueTypeDTO ValueTypeDTO)
    {
        ValueTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = ValueType_Service.CreateValueType_Global(ValueTypeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ValueType/Update")]
    public IHttpActionResult UpdateValueType([FromBody] ValueTypeDTO ValueTypeDTO)
    {
        ValueTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = ValueType_Service.UpdateValueType_Global(ValueTypeDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ValueType/Delete")]
    public IHttpActionResult DeleteValueType([FromBody] ValueTypeDTO ValueTypeDTO)
    {
        var _validationResultDTO = ValueType_Service.DeleteValueType_Global(ValueTypeDTO);
        return Json(_validationResultDTO);
    }
}