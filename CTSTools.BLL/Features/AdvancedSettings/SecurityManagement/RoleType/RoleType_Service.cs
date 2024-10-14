using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Security.Roles.RoleType
{
    public class RoleType_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateRoleType_Global(RoleTypeDTO RoleTypeDTO)
        {
            var _ValidationResultDTO = RoleType_Validator.CreateRoleType_Validation(RoleTypeDTO);
            if (_ValidationResultDTO.Result)
            {
                RoleTypeDTO.AddedDate = DateTime.Now;
                _ValidationResultDTO = RoleType_Repository.CreateRoleType(RoleTypeDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO UpdateRoleType_Global(RoleTypeDTO RoleTypeDTO)
        {
            var _ValidationResultDTO = RoleType_Validator.UpdateRoleType_Validation(RoleTypeDTO);
            if (_ValidationResultDTO.Result)
            {
                RoleTypeDTO.LastUpdate = DateTime.Now;
                _ValidationResultDTO = RoleType_Repository.UpdateRoleType(RoleTypeDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO DeleteRoleType_Global(RoleTypeDTO RoleTypeDTO)
        {
            var _ValidationResultDTO = RoleType_Validator.DeleteRoleType_Validation(RoleTypeDTO);
            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = RoleType_Repository.DeleteRoleType(RoleTypeDTO);
            }
            return _ValidationResultDTO;
        }
        public static List<RoleTypeDTO> GetRoleTypeList_Global(RoleTypeDTO RoleTypeDTO,PagedResultDTO<RoleTypeDTO> PagedResultDTO = null)
        {
            var _roletypeglobalList = new List<RoleTypeDTO>();
            try
            {
                var _roletypeList = RoleType_Repository.GetRoleTypeList(RoleTypeDTO,PagedResultDTO);
                // if RoleType is empty, return list
                _roletypeglobalList = _roletypeList;


            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _roletypeglobalList;
        }

         public static int GetRoleTypeTotalCount(PagedResultDTO<RoleTypeDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount =RoleType_Repository.GetRoleTypeCount(PagedResultDTO.Filter, PagedResultDTO);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return PagedResultDTO.TotalCount;
        }
        #endregion

        #region Business Logic

        // Aqui va la logica 

        #endregion
    }
}
