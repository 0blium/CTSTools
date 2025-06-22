using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.Tickets.Ticket;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Ticket.Tickets.Ticket;

public class TicketController : ApiController
{
    [HttpGet]
    [Route("api/Ticket/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] TicketDTO TicketDTO)
    {

        var _pagedTicketDTO = new PagedResultDTO<TicketDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = TicketDTO
        };
        _pagedTicketDTO.DataList = Ticket_Service.GetTicketList_Global(TicketDTO, _pagedTicketDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedTicketDTO.DataList, loadOptions);
        _dsLoader.totalCount = Ticket_Service.GetTicketTotalCount(_pagedTicketDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Ticket/GetList")]
    public IHttpActionResult GetTicketList([FromUri] TicketDTO TicketDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Ticket_Service.GetTicketList_Global(TicketDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Ticket/Create")]
    public IHttpActionResult CreateTicket([FromBody] TicketDTO TicketDTO)
    {

        TicketDTO.CreatedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Ticket_Service.CreateTicket_Global(TicketDTO);

        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Ticket/Update")]
    public IHttpActionResult UpdateTicket([FromBody] TicketDTO TicketDTO)
    {

        TicketDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Ticket_Service.UpdateTicket_Global(TicketDTO);

        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Ticket/Delete")]
    public IHttpActionResult DeleteTicket([FromBody] TicketDTO TicketDTO)
    {

        TicketDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Ticket_Service.DeleteTicket_Global(TicketDTO);

        return Json(_validationResultDTO);
    }
    #region Files
    [HttpGet]
    [Route("api/Ticket/GetFileList")]
    public IHttpActionResult GetTicketFileList([FromUri] TicketDTO TicketDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Ticket_Service.GetTicketFileList(TicketDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Ticket/FileDelete")]
    public IHttpActionResult DeleteTicketFile([FromBody] FileDTO FileDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(BLL.Common.Files), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Ticket_Service.DeleteTicketFile(FileDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Ticket/UploadFile")]
    public IHttpActionResult UploadTicketFile([FromBody] FileDTO FileDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(BLL.Common.Files), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
             _validationResultDTO = Ticket_Service.UploadTicketFile(FileDTO);
        }
        return Json(_validationResultDTO);
    }
    #endregion
}