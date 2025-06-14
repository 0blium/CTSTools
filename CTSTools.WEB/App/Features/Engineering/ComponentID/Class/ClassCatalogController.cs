using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.Class;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.Class
{
    public class ClassCatalogController : ApiController
    {
        [HttpGet]
        [Route("api/ClassCatalog/GetPagedList")]
        public IHttpActionResult GetClassPagedList(DataSourceLoadOptions loadOptions, [FromUri] ClassDTO ClassDTO)
        {
            var _pagedClassDTO = new PagedResultDTO<ClassDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = ClassDTO
            };
            _pagedClassDTO.DataList = Class_Service.GetClassList_Global(ClassDTO, _pagedClassDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedClassDTO.DataList, loadOptions);
            _dsLoader.totalCount = Class_Service.GetClassTotalCount(_pagedClassDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/ClassCatalog/GetList")]
        public IHttpActionResult GetClassList([FromUri] ClassDTO ClassDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = Class_Service.GetClassList_Global(ClassDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/ClassCatalog/Create")]
        public IHttpActionResult CreateClass([FromBody] ClassDTO ClassDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Class), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                ClassDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Class_Service.CreateClass_Global(ClassDTO);
            }

            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/ClassCatalog/Update")]
        public IHttpActionResult UpdateClass([FromBody] ClassDTO ClassDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Class), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                ClassDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Class_Service.UpdateClass_Global(ClassDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/ClassCatalog/Delete")]
        public IHttpActionResult DeleteClass([FromBody] ClassDTO ClassDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Class), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = Class_Service.DeleteClass_Global(ClassDTO);
            }
            return Json(_validationResultDTO);
        }
    }
}