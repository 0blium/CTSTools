using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Quality.QMS.Customer;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Quality.QMS.Customer;

public class CustomerController : ApiController
{
    [HttpGet]
    [Route("api/Customer/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] CustomerDTO CustomerDTO)
    {
        var _pagedCustomerDTO = new PagedResultDTO<CustomerDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = CustomerDTO
        };
        _pagedCustomerDTO.DataList = Customer_Service.GetList_Global(CustomerDTO, _pagedCustomerDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedCustomerDTO.DataList, loadOptions);
        _dsLoader.totalCount = Customer_Service.GetTotalCount(_pagedCustomerDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Customer/GetList")]
    public IHttpActionResult GetCustomerList([FromUri] CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Customer_Service.GetList_Global(CustomerDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Customer/Create")]
    public IHttpActionResult CreateCustomer([FromBody] CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Customer), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            CustomerDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Customer_Service.Create_Global(CustomerDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Customer/Update")]
    public IHttpActionResult UpdateCustomer([FromBody] CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Customer), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            CustomerDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Customer_Service.Update_Global(CustomerDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Customer/Delete")]
    public IHttpActionResult DeleteCustomer([FromBody] CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Customer), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Customer_Service.Delete_Global(CustomerDTO);
        }
        return Json(_validationResultDTO);
    }
}