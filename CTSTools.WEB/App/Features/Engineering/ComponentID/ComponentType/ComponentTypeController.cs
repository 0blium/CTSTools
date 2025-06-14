using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.ComponentType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.ComponentType
{
    public class ComponentTypeController : ApiController
    {
        [HttpGet]
        [Route("api/ComponentType/GetPagedList")]
        public IHttpActionResult GetComponentTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] ComponentTypeDTO ComponentTypeDTO)
        {
            var _pagedComponentTypeDTO = new PagedResultDTO<ComponentTypeDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = ComponentTypeDTO
            };
            _pagedComponentTypeDTO.DataList = ComponentType_Service.GetComponentTypeList_Global(ComponentTypeDTO, _pagedComponentTypeDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedComponentTypeDTO.DataList, loadOptions);
            _dsLoader.totalCount = ComponentType_Service.GetComponentTypeTotalCount(_pagedComponentTypeDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/ComponentType/GetList")]
        public IHttpActionResult GetComponentTypeList([FromUri] ComponentTypeDTO ComponentTypeDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = ComponentType_Service.GetComponentTypeList_Global(ComponentTypeDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/ComponentType/Create")]
        public IHttpActionResult CreateComponentType([FromBody] ComponentTypeDTO ComponentTypeDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ComponentType), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                ComponentTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = ComponentType_Service.CreateComponentType_Global(ComponentTypeDTO);
            }

            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/ComponentType/Update")]
        public IHttpActionResult UpdateComponentType([FromBody] ComponentTypeDTO ComponentTypeDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ComponentType), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                ComponentTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = ComponentType_Service.UpdateComponentType_Global(ComponentTypeDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/ComponentType/Delete")]
        public IHttpActionResult DeleteComponentType([FromBody] ComponentTypeDTO ComponentTypeDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ComponentType), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = ComponentType_Service.DeleteComponentType_Global(ComponentTypeDTO);
            }
            return Json(_validationResultDTO);
        }
    }
}