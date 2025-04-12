using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.DataType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.DataType;

public class DataTypeController : ApiController
{
    [HttpGet]
    [Route("api/DataType/GetPagedList")]
    public IHttpActionResult GetDataTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] DataTypeDTO DataTypeDTO)
    {
        var _pagedDataTypeDTO = new PagedResultDTO<DataTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DataTypeDTO
        };
        _pagedDataTypeDTO.DataList = DataType_Service.GetDataTypeList_Global(DataTypeDTO, _pagedDataTypeDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDataTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = DataType_Service.GetDataTypeTotalCount(_pagedDataTypeDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/DataType/GetList")]
    public IHttpActionResult GetDataTypeList([FromUri] DataTypeDTO DataTypeDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DataType_Service.GetDataTypeList_Global(DataTypeDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DataType/Create")]
    public IHttpActionResult CreateDataType([FromBody] DataTypeDTO DataTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DataType), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            DataTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = DataType_Service.CreateDataType_Global(DataTypeDTO);
        }

        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DataType/Update")]
    public IHttpActionResult UpdateDataType([FromBody] DataTypeDTO DataTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DataType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            DataTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = DataType_Service.UpdateDataType_Global(DataTypeDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DataType/Delete")]
    public IHttpActionResult DeleteDataType([FromBody] DataTypeDTO DataTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DataType), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = DataType_Service.DeleteDataType_Global(DataTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}