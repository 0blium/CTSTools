using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Linq;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;
public class DecoderStructure_Controller : ApiController
{

    [HttpGet]
    [Route("api/DecoderStructure/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DecoderStructureDTO DecoderStructureDTO)
    {

        var _pagedDecoderStructureDTO = new PagedResultDTO<DecoderStructureDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DecoderStructureDTO
        };
        _pagedDecoderStructureDTO.DataList = DecoderStructure_Service.GetDecoderStructureList_Global(DecoderStructureDTO, _pagedDecoderStructureDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDecoderStructureDTO.DataList, loadOptions);
        _dsLoader.totalCount = DecoderStructure_Service.GetDecoderStructureTotalCount(_pagedDecoderStructureDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/DecoderStructure/GetList")]
    public IHttpActionResult GetDecoderStructureList([FromUri] DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        DecoderStructureDTO.AttributeDTO.GetValueList = DecoderStructureDTO.GetValueList;
        _validationResultDTO.Data = DecoderStructure_Service.GetDecoderStructureList_Global(DecoderStructureDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DecoderStructure/Create")]
    public IHttpActionResult CreateDecoderStructure([FromBody] DecoderStructureDTO DecoderStructureDTO)
    {

        DecoderStructureDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DecoderStructure_Service.CreateDecoderStructure_Global(DecoderStructureDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DecoderStructure/Update")]
    public IHttpActionResult UpdateDecoderStructure([FromBody] DecoderStructureDTO DecoderStructureDTO)
    {

        DecoderStructureDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        DecoderStructureDTO.LastUpdate = DateTime.Now;

        var _validationResultDTO = DecoderStructure_Service.UpdateDecoderStructure_Global(DecoderStructureDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DecoderStructure/UpdateNumberOrder")]
    public IHttpActionResult UpdateNumberOrder([FromBody] DecoderStructureDTO DecoderStructureDTO)
    {
        DecoderStructureDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        DecoderStructureDTO.LastUpdate = DateTime.Now;
        var _validationResultDTO = DecoderStructure_Service.UpdateNumberOrder(DecoderStructureDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DecoderStructure/UpdateDescriptionOrder")]
    public IHttpActionResult UpdateDescriptionOrder([FromBody] DecoderStructureDTO DecoderStructureDTO)
    {
        DecoderStructureDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        DecoderStructureDTO.LastUpdate = DateTime.Now;
        var _validationResultDTO = DecoderStructure_Service.UpdateDescriptionOrder(DecoderStructureDTO);
        return Json(_validationResultDTO);
    }


    [HttpPost]
    [Route("api/DecoderStructure/Delete")]
    public IHttpActionResult DeleteDecoderStructure([FromBody] DecoderStructureDTO DecoderStructureDTO)
    {
        var _validationResultDTO = DecoderStructure_Service.DeleteDecoderStructure_Global(DecoderStructureDTO);
        return Json(_validationResultDTO);
    }
    

    
}