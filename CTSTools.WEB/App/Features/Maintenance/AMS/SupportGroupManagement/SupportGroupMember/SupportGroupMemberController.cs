using AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace AMS.WEB.App.Features.SupportGroups.SupportGroupMember
{
    public class SupportGroupMemberController : ApiController
    {
        [HttpGet]
        [Route("api/SupportGroupMember/GetPagedList")]
        public IHttpActionResult GetSupportGroupMemberPagedList(DataSourceLoadOptions loadOptions, [FromUri] SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _pagedSupportGroupMemberDTO = new PagedResultDTO<SupportGroupMemberDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = SupportGroupMemberDTO
            };
            _pagedSupportGroupMemberDTO.DataList = SupportGroupMember_Service.GetSupportGroupMemberList_Global(SupportGroupMemberDTO, _pagedSupportGroupMemberDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedSupportGroupMemberDTO.DataList, loadOptions);
            _dsLoader.totalCount = SupportGroupMember_Service.GetSupportGroupMemberTotalCount(_pagedSupportGroupMemberDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/SupportGroupMember/GetList")]
        public IHttpActionResult GetSupportGroupMemberList([FromUri] SupportGroupMemberDTO SupportGroupMemberDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = SupportGroupMember_Service.GetSupportGroupMemberList_Global(SupportGroupMemberDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/SupportGroupMember/Create")]
        public IHttpActionResult CreateSupportGroupMember([FromBody] SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SupportGroupMemberDTO.SupportGroupDTO, nameof(SupportGroupMember), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                SupportGroupMemberDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = SupportGroupMember_Service.CreateSupportGroupMember_Global(SupportGroupMemberDTO);
            }
            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/SupportGroupMember/Update")]
        public IHttpActionResult UpdateSupportGroupMember([FromBody] SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SupportGroupMemberDTO.SupportGroupDTO, nameof(SupportGroupMember), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                SupportGroupMemberDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = SupportGroupMember_Service.UpdateSupportGroupMember_Global(SupportGroupMemberDTO);
            }
            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/SupportGroupMember/Delete")]
        public IHttpActionResult DeleteSupportGroupMember([FromBody] SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SupportGroupMemberDTO.SupportGroupDTO, nameof(SupportGroupMember), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                SupportGroupMemberDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = SupportGroupMember_Service.DeleteSupportGroupMember_Global(SupportGroupMemberDTO);
            }
            return Json(_validationResultDTO);
        }
    }

}