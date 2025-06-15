using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.SubClass;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.Class.SubClass
{
    public class SubClassController : ApiController
    {
        [HttpGet]
        [Route("api/SubClassCatalog/GetPagedList")]
        public IHttpActionResult GetSubClassPagedList(DataSourceLoadOptions loadOptions, [FromUri] SubClassDTO SubClassDTO)
        {
            var _pagedSubClassDTO = new PagedResultDTO<SubClassDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = SubClassDTO
            };
            _pagedSubClassDTO.DataList = SubClass_Service.GetSubClassList_Global(SubClassDTO, _pagedSubClassDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedSubClassDTO.DataList, loadOptions);
            _dsLoader.totalCount = SubClass_Service.GetSubClassTotalCount(_pagedSubClassDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/SubClassCatalog/GetList")]
        public IHttpActionResult GetSubClassList([FromUri] SubClassDTO SubClassDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = SubClass_Service.GetSubClassList_Global(SubClassDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/SubClassCatalog/Create")]
        public IHttpActionResult CreateSubClass([FromBody] SubClassDTO SubClassDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SubClass), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                SubClassDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = SubClass_Service.CreateSubClass_Global(SubClassDTO);
            }

            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/SubClassCatalog/Update")]
        public IHttpActionResult UpdateSubClass([FromBody] SubClassDTO SubClassDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SubClass), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                SubClassDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = SubClass_Service.UpdateSubClass_Global(SubClassDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/SubClassCatalog/Delete")]
        public IHttpActionResult DeleteSubClass([FromBody] SubClassDTO SubClassDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SubClass), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = SubClass_Service.DeleteSubClass_Global(SubClassDTO);
            }
            return Json(_validationResultDTO);
        }
    }
}