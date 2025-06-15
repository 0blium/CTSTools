using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.PartType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.PartType
{
    public class PartTypeController : ApiController
    {
        [HttpGet]
        [Route("api/PartType/GetPagedList")]
        public IHttpActionResult GetPartTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] PartTypeDTO PartTypeDTO)
        {
            var _pagedPartTypeDTO = new PagedResultDTO<PartTypeDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = PartTypeDTO
            };
            _pagedPartTypeDTO.DataList = PartType_Service.GetPartTypeList_Global(PartTypeDTO, _pagedPartTypeDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedPartTypeDTO.DataList, loadOptions);
            _dsLoader.totalCount = PartType_Service.GetPartTypeTotalCount(_pagedPartTypeDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/PartType/GetList")]
        public IHttpActionResult GetPartTypeList([FromUri] PartTypeDTO PartTypeDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = PartType_Service.GetPartTypeList_Global(PartTypeDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/PartType/Create")]
        public IHttpActionResult CreatePartType([FromBody] PartTypeDTO PartTypeDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(PartType), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                PartTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = PartType_Service.CreatePartType_Global(PartTypeDTO);
            }

            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/PartType/Update")]
        public IHttpActionResult UpdatePartType([FromBody] PartTypeDTO PartTypeDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(PartType), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                PartTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = PartType_Service.UpdatePartType_Global(PartTypeDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/PartType/Delete")]
        public IHttpActionResult DeletePartType([FromBody] PartTypeDTO PartTypeDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(PartType), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = PartType_Service.DeletePartType_Global(PartTypeDTO);
            }
            return Json(_validationResultDTO);
        }
    }
}