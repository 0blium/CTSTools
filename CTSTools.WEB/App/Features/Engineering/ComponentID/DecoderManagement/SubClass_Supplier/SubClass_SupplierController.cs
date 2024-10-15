using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;

using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.SubClass_SupplierManagement.SubClass_Supplier;

public class SubClass_SupplierController : ApiController
{
    [HttpGet]
    [Route("api/SubClass_Supplier/GetPagedList")] 
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SubClass_SupplierDTO SubClass_SupplierDTO)
    {

        var _pagedSubClass_SupplierDTO = new PagedResultDTO<SubClass_SupplierDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SubClass_SupplierDTO
        };
        _pagedSubClass_SupplierDTO.DataList = SubClass_Supplier_Service.GetSubClass_SupplierList_Global(SubClass_SupplierDTO, _pagedSubClass_SupplierDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSubClass_SupplierDTO.DataList, loadOptions);
        _dsLoader.totalCount = SubClass_Supplier_Service.GetTotalCount(_pagedSubClass_SupplierDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/SubClass_Supplier/GetSubClass_SupplierList")]
    public IHttpActionResult GetSubClass_SupplierList([FromUri] SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SubClass_Supplier_Service.GetSubClass_SupplierList_Global(SubClass_SupplierDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SubClass_Supplier/Create")]
    public IHttpActionResult CreateSubClass_Supplier([FromBody] SubClass_SupplierDTO SubClass_SupplierDTO)
    {

        SubClass_SupplierDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = SubClass_Supplier_Service.CreateSubClass_Supplier_Global(SubClass_SupplierDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SubClass_Supplier/Update")]
    public IHttpActionResult UpdateSubClass_Supplier([FromBody] SubClass_SupplierDTO SubClass_SupplierDTO)
    {

        SubClass_SupplierDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = SubClass_Supplier_Service.UpdateSubClass_Supplier_Global(SubClass_SupplierDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SubClass_Supplier/Delete")]
    public IHttpActionResult DeleteSubClass_Supplier([FromBody] SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = SubClass_Supplier_Service.DeleteSubClass_Supplier_Global(SubClass_SupplierDTO);
        return Json(_validationResultDTO);
    }
}