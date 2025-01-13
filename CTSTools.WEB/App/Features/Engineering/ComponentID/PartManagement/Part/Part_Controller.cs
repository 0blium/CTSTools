using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.PartManagement.Part;

public class Part_Controller : ApiController
{
    [HttpGet]
    [Route("api/Part/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] PartDTO PartDTO)
    {

        var _pagedPartDTO = new PagedResultDTO<PartDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = PartDTO
        };
        _pagedPartDTO.DataList = Part_Service.GetPartList_Global(PartDTO, _pagedPartDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedPartDTO.DataList, loadOptions);
        _dsLoader.totalCount = Part_Service.GetTotalCount(_pagedPartDTO);
        return Json(_dsLoader);
    }
    [HttpPost]
    [Route("api/Part/Create")]
    public IHttpActionResult CreatePart([FromBody] PartDTO PartDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Part), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            PartDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Part_Service.CreatePart_Global(PartDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Part/Update")]
    public IHttpActionResult UpdatePart([FromBody] PartDTO PartDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Part), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            PartDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Part_Service.UpdatePart_Global(PartDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Part/Delete")]
    public IHttpActionResult DeletePart([FromBody] PartDTO PartDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Part), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Part_Service.DeletePart_Global(PartDTO);
        }
        return Json(_validationResultDTO);
    }
}