using CTSTools.BLL.Common;
using CTSTools.BLL.Features.ChangeLog;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.ChangeLog;

public class ChangeLogController : ApiController
{
    [HttpGet]
    [Route("api/ChangeLog/GetPagedList")]
    public IHttpActionResult GetChangeLogPagedList(DataSourceLoadOptions loadOptions, [FromUri] ChangeLogDTO ChangeLogDTO)
    {
        var _pagedChangeLogDTO = new PagedResultDTO<ChangeLogDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ChangeLogDTO
        };
        _pagedChangeLogDTO.DataList = ChangeLog_Service.GetChangeLogList_Global(ChangeLogDTO, _pagedChangeLogDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedChangeLogDTO.DataList, loadOptions);
        _dsLoader.totalCount = ChangeLog_Service.GetChangeLogTotalCount(_pagedChangeLogDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/ChangeLog/GetList")]
    public IHttpActionResult GetChangeLogList([FromUri] ChangeLogDTO ChangeLogDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = ChangeLog_Service.GetChangeLogList_Global(ChangeLogDTO);

        return Json(_validationResultDTO);
    }
}