using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.Decoder;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.DecoderManagement.Decoder;
public class Decoder_Controller : ApiController
{

    [HttpGet]
    [Route("api/Decoder/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DecoderDTO DecoderDTO)
    {

        var _pagedDecoderDTO = new PagedResultDTO<DecoderDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DecoderDTO
        };
        _pagedDecoderDTO.DataList = Decoder_Service.GetDecoderList_Global(DecoderDTO, _pagedDecoderDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDecoderDTO.DataList, loadOptions);
        _dsLoader.totalCount = Decoder_Service.GetDecoderTotalCount(_pagedDecoderDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Decoder/GetDecoderList")]
    public IHttpActionResult GetDecoderList([FromUri] DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Decoder_Service.GetDecoderList_Global(DecoderDTO);
        return Json(_validationResultDTO);
    }      

    [HttpPost]
    [Route("api/Decoder/Create")]
    public IHttpActionResult CreateDecoder([FromBody] DecoderDTO DecoderDTO)
    {

        DecoderDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Decoder_Service.CreateDecoder_Global(DecoderDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Decoder/Update")]
    public IHttpActionResult UpdateDecoder([FromBody] DecoderDTO DecoderDTO)
    {

        DecoderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Decoder_Service.UpdateDecoder_Global(DecoderDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Decoder/Delete")]
    public IHttpActionResult DeleteDecoder([FromBody] DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = Decoder_Service.DeleteDecoder_Global(DecoderDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Decoder/SubmitDecoder")]
    public IHttpActionResult SubmitDecoder([FromBody] DecoderDTO DecoderDTO)
    {
        DecoderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Decoder_Service.SubmitDecoder_Global(DecoderDTO);
        return Json(_validationResultDTO);
    }
    
    [HttpPost]
    [Route("api/Decoder/GetDecoderStructureFromDecoder")]
    public IHttpActionResult GetDecoderStructureFromDecoder([FromBody] DecoderDTO DecoderDTO)
    {
        var _validationResultDTO = Decoder_Service.GetDecoderStructureFromDecoder(DecoderDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Decoder/Edit")]
    public IHttpActionResult EditDecoder([FromBody] DecoderDTO DecoderDTO)
    {
        DecoderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();

        var _validationResultDTO = Decoder_Service.EditDecoder_Global(DecoderDTO);
        return Json(_validationResultDTO);
    }
    
}