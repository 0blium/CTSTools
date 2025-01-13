using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.StatusManagement.Status_StatusType;

public class Status_StatusTypeController : ApiController
{
    [HttpGet]
    [Route("api/Status_StatusType/GetPagedList")]
    public IHttpActionResult GetStatus_StatusTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _pagedStatus_StatusTypeDTO = new PagedResultDTO<Status_StatusTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = Status_StatusTypeDTO
        };
        _pagedStatus_StatusTypeDTO.DataList = Status_StatusType_Service.GetStatus_StatusTypeList_Global(Status_StatusTypeDTO, _pagedStatus_StatusTypeDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedStatus_StatusTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = Status_StatusType_Service.GetTotalCount(_pagedStatus_StatusTypeDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Status_StatusType/GetList")]
    public IHttpActionResult GetStatus_StatusTypeList([FromUri] Status_StatusTypeDTO Status_StatusTypeDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Status_StatusType_Service.GetStatus_StatusTypeList_Global(Status_StatusTypeDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Status_StatusType/Create")]
    public IHttpActionResult CreateStatus_StatusType([FromBody] Status_StatusTypeDTO Status_StatusTypeDTO)
    {

        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Status_StatusType), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            Status_StatusTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Status_StatusType_Service.CreateStatus_StatusType_Global(Status_StatusTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Status_StatusType/Update")]
    public IHttpActionResult UpdateStatus_StatusType([FromBody] Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Status_StatusType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Status_StatusTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Status_StatusType_Service.UpdateStatus_StatusType_Global(Status_StatusTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Status_StatusType/Delete")]
    public IHttpActionResult DeleteStatus_StatusType([FromBody] Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Status_StatusType), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Status_StatusType_Service.DeleteStatus_StatusType_Global(Status_StatusTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}