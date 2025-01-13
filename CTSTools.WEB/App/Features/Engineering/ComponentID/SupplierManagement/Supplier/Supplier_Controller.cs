using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.SupplierManagement.Supplier;

public class Supplier_Controller : ApiController
{

    [HttpGet]
    [Route("api/Supplier/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SupplierDTO SupplierDTO)
    {

        var _pagedSupplierDTO = new PagedResultDTO<SupplierDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SupplierDTO
        };
        _pagedSupplierDTO.DataList = Supplier_Service.GetSupplierList_Global(SupplierDTO, _pagedSupplierDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSupplierDTO.DataList, loadOptions);
        _dsLoader.totalCount = Supplier_Service.GetTotalCount(_pagedSupplierDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Supplier/GetSupplierList")]
    public IHttpActionResult GetSupplierList([FromUri] SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Supplier_Service.GetSupplierList_Global(SupplierDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Supplier/Create")]
    public IHttpActionResult CreateSupplier([FromBody] SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Supplier), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SupplierDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Supplier_Service.CreateSupplier_Global(SupplierDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Supplier/Update")]
    public IHttpActionResult UpdateSupplier([FromBody] SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Supplier), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SupplierDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Supplier_Service.UpdateSupplier_Global(SupplierDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Supplier/Delete")]
    public IHttpActionResult DeleteSupplier([FromBody] SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Supplier), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Supplier_Service.DeleteSupplier_Global(SupplierDTO);
        }
        return Json(_validationResultDTO);
    }
}