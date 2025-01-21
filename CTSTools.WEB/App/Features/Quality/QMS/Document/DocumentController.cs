using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Quality.QMS.Document;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Quality.QMS.Document
{
    public class DocumentController : ApiController
    {
        [HttpGet]
        [Route("api/Document/GetPagedList")]
        public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DocumentDTO DocumentDTO)
        {

            var _pagedDocumentDTO = new PagedResultDTO<DocumentDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = DocumentDTO
            };
            _pagedDocumentDTO.DataList = Document_Service.GetDocumentList_Global(DocumentDTO, _pagedDocumentDTO);
            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedDocumentDTO.DataList, loadOptions);
            _dsLoader.totalCount = Document_Service.GetDocumentTotalCount(_pagedDocumentDTO);
            return Json(_dsLoader);
        }
        [HttpGet]
        [Route("api/Document/GetDocumentList")]
        public IHttpActionResult GetDocumentList([FromUri] DocumentDTO DocumentDTO)
        {
            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = Document_Service.GetDocumentList_Global(DocumentDTO);
            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/Document/Create")]
        public IHttpActionResult CreateDocument([FromBody] DocumentDTO DocumentDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Document), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                DocumentDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Document_Service.CreateDocument_Global(DocumentDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/Document/Update")]
        public IHttpActionResult UpdateDocument([FromBody] DocumentDTO DocumentDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Document), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                DocumentDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Document_Service.UpdateDocument_Global(DocumentDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/Document/Delete")]
        public IHttpActionResult DeleteDocument([FromBody] DocumentDTO DocumentDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Document), (int)Action_Enum.Delete);
            if (!_validationResultDTO.Result)
            {
                DocumentDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Document_Service.DeleteDocument_Global(DocumentDTO);
            }
            return Json(_validationResultDTO);
        }
    }
}