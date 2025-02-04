using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Quality.QMS.DocumentType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Quality.QMS.DocumentType;

public class DocumentTypeController : ApiController
{
    [HttpGet]
    [Route("api/DocumentType/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DocumentTypeDTO DocumentTypeDTO)
    {
        var _pagedDocumentTypeDTO = new PagedResultDTO<DocumentTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DocumentTypeDTO
        };
        _pagedDocumentTypeDTO.DataList = DocumentType_Service.GetList_Global(DocumentTypeDTO, _pagedDocumentTypeDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDocumentTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = DocumentType_Service.GetTotalCount(_pagedDocumentTypeDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/DocumentType/GetList")]
    public IHttpActionResult GetDocumentTypeList([FromUri] DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DocumentType_Service.GetList_Global(DocumentTypeDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DocumentType/Create")]
    public IHttpActionResult CreateDocumentType([FromBody] DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DocumentType), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            DocumentTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = DocumentType_Service.Create_Global(DocumentTypeDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DocumentType/Update")]
    public IHttpActionResult UpdateDocumentType([FromBody] DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DocumentType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            DocumentTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = DocumentType_Service.Update_Global(DocumentTypeDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DocumentType/Delete")]
    public IHttpActionResult DeleteDocumentType([FromBody] DocumentTypeDTO DocumentTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DocumentType), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = DocumentType_Service.Delete_Global(DocumentTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}