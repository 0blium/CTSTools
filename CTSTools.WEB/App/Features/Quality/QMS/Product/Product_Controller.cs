using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Quality.QMS.Product;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Quality.QMS.Product;

public class Product_Controller : ApiController
{
    [HttpGet]
    [Route("api/Product/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ProductDTO ProductDTO)
    {
        var _pagedProductDTO = new PagedResultDTO<ProductDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ProductDTO
        };
        _pagedProductDTO.DataList = Product_Service.GetList_Global(ProductDTO, _pagedProductDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedProductDTO.DataList, loadOptions);
        _dsLoader.totalCount = Product_Service.GetTotalCount(_pagedProductDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Product/GetList")]
    public IHttpActionResult GetProductList([FromUri] ProductDTO ProductDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Product_Service.GetList_Global(ProductDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Product/Create")]
    public IHttpActionResult CreateProduct([FromBody] ProductDTO ProductDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Product), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ProductDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Product_Service.Create_Global(ProductDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Product/Update")]
    public IHttpActionResult UpdateProduct([FromBody] ProductDTO ProductDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Product), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ProductDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Product_Service.Update_Global(ProductDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Product/Delete")]
    public IHttpActionResult DeleteProduct([FromBody] ProductDTO ProductDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Product), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Product_Service.Delete_Global(ProductDTO);
        }
        return Json(_validationResultDTO);
    }
}