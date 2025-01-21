using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Quality.QMS.DocumentRevision;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Quality.QMS.DocumentRevision;

public class DocumentRevisionController : ApiController
{
    [HttpGet]
    [Route("api/DocumentRevision/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DocumentRevisionDTO DocumentRevisionDTO)
    {

        var _pagedDocumentRevisionDTO = new PagedResultDTO<DocumentRevisionDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DocumentRevisionDTO
        };
        _pagedDocumentRevisionDTO.DataList = DocumentRevision_Service.GetDocumentRevisionList_Global(DocumentRevisionDTO, _pagedDocumentRevisionDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDocumentRevisionDTO.DataList, loadOptions);
        _dsLoader.totalCount = DocumentRevision_Service.GetDocumentRevisionTotalCount(_pagedDocumentRevisionDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/DocumentRevision/GetDocumentRevisionList")]
    public IHttpActionResult GetDocumentRevisionList([FromUri] DocumentRevisionDTO DocumentRevisionDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DocumentRevision_Service.GetDocumentRevisionList_Global(DocumentRevisionDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DocumentRevision/Create")]
    public IHttpActionResult CreateDocumentRevision([FromBody] DocumentRevisionDTO DocumentRevisionDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DocumentRevision), (int)Action_Enum.Create);
        //if (_validationResultDTO.Result)
        //{
            DocumentRevisionDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            var _validationResultDTO = DocumentRevision_Service.CreateDocumentRevision_Global(DocumentRevisionDTO);
        //}
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DocumentRevision/Update")]
    public IHttpActionResult UpdateDocumentRevision([FromBody] DocumentRevisionDTO DocumentRevisionDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DocumentRevision), (int)Action_Enum.Update);
        //if (_validationResultDTO.Result)
        //{
            DocumentRevisionDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            var _validationResultDTO = DocumentRevision_Service.UpdateDocumentRevision_Global(DocumentRevisionDTO);
        //}
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DocumentRevision/Delete")]
    public IHttpActionResult DeleteDocumentRevision([FromBody] DocumentRevisionDTO DocumentRevisionDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DocumentRevision), (int)Action_Enum.Delete);
        //if (!_validationResultDTO.Result)
        //{
            DocumentRevisionDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            var _validationResultDTO = DocumentRevision_Service.DeleteDocumentRevision_Global(DocumentRevisionDTO);
        //}
        return Json(_validationResultDTO);
    }
}