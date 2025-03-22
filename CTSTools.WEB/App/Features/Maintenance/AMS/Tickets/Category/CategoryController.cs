using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.Tickets.Category;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Ticket.Tickets.Category;

public class CategoryController : ApiController
{
    [HttpGet]
    [Route("api/Category/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] CategoryDTO CategoryDTO)
    {

        var _pagedCategoryDTO = new PagedResultDTO<CategoryDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = CategoryDTO
        };
        _pagedCategoryDTO.DataList = Category_Service.GetCategoryList_Global(CategoryDTO, _pagedCategoryDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedCategoryDTO.DataList, loadOptions);
        _dsLoader.totalCount = Category_Service.GetCategoryTotalCount(_pagedCategoryDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Category/GetList")]
    public IHttpActionResult GetCategoryList([FromUri] CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Category_Service.GetCategoryList_Global(CategoryDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Category/Create")]
    public IHttpActionResult CreateCategory([FromBody] CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(CategoryDTO.SupportGroupDTO, nameof(Category), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            CategoryDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Category_Service.CreateCategory_Global(CategoryDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Category/Update")]
    public IHttpActionResult UpdateCategory([FromBody] CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(CategoryDTO.SupportGroupDTO, nameof(Category), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            CategoryDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Category_Service.UpdateCategory_Global(CategoryDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Category/Delete")]
    public IHttpActionResult DeleteCategory([FromBody] CategoryDTO CategoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(CategoryDTO.SupportGroupDTO, nameof(Category), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            CategoryDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Category_Service.DeleteCategory_Global(CategoryDTO);
        }
        return Json(_validationResultDTO);
    }
}