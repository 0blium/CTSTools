using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Brand;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.Brand
{
    public class BrandController : ApiController
    {
        [HttpGet]
        [Route("api/Brand/GetPagedList")]
        public IHttpActionResult GetBrandPagedList(DataSourceLoadOptions loadOptions, [FromUri] BrandDTO BrandDTO)
        {
            var _pagedBrandDTO = new PagedResultDTO<BrandDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = BrandDTO
            };
            _pagedBrandDTO.DataList = Brand_Service.GetBrandList_Global(BrandDTO, _pagedBrandDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedBrandDTO.DataList, loadOptions);
            _dsLoader.totalCount = Brand_Service.GetBrandTotalCount(_pagedBrandDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/Brand/GetList")]
        public IHttpActionResult GetBrandList([FromUri] BrandDTO BrandDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = Brand_Service.GetBrandList_Global(BrandDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/Brand/Create")]
        public IHttpActionResult CreateBrand([FromBody] BrandDTO BrandDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Brand), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                BrandDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Brand_Service.CreateBrand_Global(BrandDTO);
            }

            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/Brand/Update")]
        public IHttpActionResult UpdateBrand([FromBody] BrandDTO BrandDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Brand), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                BrandDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Brand_Service.UpdateBrand_Global(BrandDTO);
            }
            return Json(_validationResultDTO);
        }
        [HttpPost]
        [Route("api/Brand/Delete")]
        public IHttpActionResult DeleteBrand([FromBody] BrandDTO BrandDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Brand), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = Brand_Service.DeleteBrand_Global(BrandDTO);
            }
            return Json(_validationResultDTO);
        }
    }
}