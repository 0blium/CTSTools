using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.Tickets.Ticket;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.Tickets.Ticket;

public class TicketController : ApiController
{
    [HttpGet]
    [Route("api/Ticket/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] TicketDTO TicketDTO)
    {
        //var _dxFilters = loadOptions.Filter?.Cast<object>().Select(f => f is string str ? str.Replace("ItemNameWithManufactureSerial", "Item_Header.Model") : f).ToList();
    //    var _listavergera = loadOptions.Filter;
    //    var _nuevalistavergera = _listavergera?.Cast<object>()  // Aseguramos que la colección es tratada como 'object'
    //        .Select(item => item switch
    //        {
    //            List<string> sublista => sublista
    //                .Select(subitem => {
    //                    // Debugging: Mostrar qué estamos comparando
    //                    Debug.WriteLine($"Comparando: {subitem}");
    //                    return subitem == "ItemNameWithManufactureSerial" ? "Item_Header.Model" : subitem;
    //                })
    //                .ToList(),
    //            _ => item  // Si no es una lista, lo dejamos igual
    //        })
    //        .ToList();


    //    var _dxFilters = loadOptions.Filter?.Cast<object>().Select(f =>
    //    {
    //        // Caso 1: Si es un string (como "or", "and"), lo dejamos igual
    //        if (f is string str)
    //            return str;

    //        // Caso 2: Si es un objeto con propiedad Selector (usamos dynamic para acceder fácil)
    //        if (f != null)
    //        {
    //            dynamic dynamicObj = f; // Convertimos a dynamic para acceder a .Selector
    //            try
    //            {
    //                //string selector = dynamicObj.Selector?.ToString();
    //                //if (!string.IsNullOrEmpty(selector))
    //                //{
    //                    dynamicObj.Selector.Replace(
    //                        "Item_LineDTO.ItemNameWithManufactureSerial",
    //                        "Item_LineDTO.Item_Header.Model"
    //                    );
    //                //}
    //            }
    //            catch { } // Si falla, no es un objeto con Selector
    //        }
    //        return f;
    //    })
    //.ToList();
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