using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.MailGroups.MailGroupMember
{
    public class MailGroupMember_Service
    {
        #region Global CRUD
        public static ValidationResultDTO CreateMailGroupMember_Global(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _ValidationResultDTO = MailGroupMember_Validator.CreateMailGroupMember_Validation(MailGroupMemberDTO);
            if (_ValidationResultDTO.Result)
            {
                MailGroupMemberDTO.AddedDate = DateTime.Now;
                _ValidationResultDTO = MailGroupMember_Repository.CreateMailGroupMember(MailGroupMemberDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO UpdateMailGroupMember_Global(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _ValidationResultDTO = MailGroupMember_Validator.UpdateMailGroupMember_Validation(MailGroupMemberDTO);
            if (_ValidationResultDTO.Result)
            {
                MailGroupMemberDTO.LastUpdate = DateTime.Now;
                _ValidationResultDTO = MailGroupMember_Repository.UpdateMailGroupMember(MailGroupMemberDTO);
            }
            return _ValidationResultDTO;
        }
        public static ValidationResultDTO DeleteMailGroupMember_Global(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _ValidationResultDTO = MailGroupMember_Validator.DeleteMailGroupMember_Validation(MailGroupMemberDTO);
            if (_ValidationResultDTO.Result)
            {
                _ValidationResultDTO = MailGroupMember_Repository.DeleteMailGroupMember(MailGroupMemberDTO);
            }
            return _ValidationResultDTO;
        }
        public static List<MailGroupMemberDTO> GetMailGroupMemberList_Global(MailGroupMemberDTO MailGroupMemberDTO,PagedResultDTO<MailGroupMemberDTO> PagedResultDTO = null)
        {
            var _mailgroupmemberglobalList = new List<MailGroupMemberDTO>();
            try
            {
                var _mailgroupmemberList = MailGroupMember_Repository.GetMailGroupMemberList(MailGroupMemberDTO,PagedResultDTO);
                // if MailGroupMember is empty, return list
                if (_mailgroupmemberList.Count() == 0)
                {
                        _mailgroupmemberglobalList = _mailgroupmemberList;
                        return _mailgroupmemberglobalList;
                }
                if (!MailGroupMemberDTO.GetMailGroupDTO && !MailGroupMemberDTO.GetUserDTO)
                {
                        _mailgroupmemberglobalList = _mailgroupmemberList;
                        return _mailgroupmemberglobalList;
                }
                _mailgroupmemberglobalList = GetMailGroupMemberRelatedData(MailGroupMemberDTO,_mailgroupmemberList);                        

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _mailgroupmemberglobalList;
        }



        public static List<MailGroupMemberDTO> GetMailGroupMemberRelatedData(MailGroupMemberDTO MailGroupMemberDTO,List<MailGroupMemberDTO> MailGroupMemberList)
        {
            var _mailgroupmemberglobalList = new List<MailGroupMemberDTO>();
            var _mailgroupDict = new Dictionary<int?,MailGroupDTO>();
            var _userDict = new Dictionary<int?,UserDTO>();
            
            try
            {
                if (MailGroupMemberDTO.GetMailGroupDTO)
                {
                        MailGroupMemberDTO.MailGroupDTO.MailGroupIDArray = MailGroupMemberList.GroupBy(g => g.MailGroupDTO.ID)
                                .Select(s => s.Key)
                                .ToArray();

                        _mailgroupDict = MailGroup_Service.GetMailGroupList_Global(MailGroupMemberDTO.MailGroupDTO)
                                .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                if (MailGroupMemberDTO.GetUserDTO)
                {
                        MailGroupMemberDTO.UserDTO.UserIDArray = MailGroupMemberList.GroupBy(g => g.UserDTO.ID)
                                .Select(s => s.Key)
                                .ToArray();

                        _userDict = User_Service.GetUserList_Global(MailGroupMemberDTO.UserDTO)
                                .ToDictionary(keySelector: m => m.ID, elementSelector: m => m);
                }
                foreach (var _mailgroupmemberDTO in MailGroupMemberList)
                {
                        if (MailGroupMemberDTO.GetMailGroupDTO && _mailgroupDict.ContainsKey(_mailgroupmemberDTO.MailGroupDTO.ID))
                        {
                                _mailgroupmemberDTO.MailGroupDTO = _mailgroupDict[_mailgroupmemberDTO.MailGroupDTO.ID];
                        }
                        if (MailGroupMemberDTO.GetUserDTO && _userDict.ContainsKey(_mailgroupmemberDTO.UserDTO.ID))
                        {
                                _mailgroupmemberDTO.UserDTO = _userDict[_mailgroupmemberDTO.UserDTO.ID];
                        }
                        _mailgroupmemberglobalList.Add(_mailgroupmemberDTO);
                }                        

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _mailgroupmemberglobalList;
        }

        


         public static int GetMailGroupMemberTotalCount(PagedResultDTO<MailGroupMemberDTO> PagedResultDTO)
        {
            try
            {
                //Get Total Count
                PagedResultDTO.TotalCount =MailGroupMember_Repository.GetMailGroupMemberCount(PagedResultDTO.Filter, PagedResultDTO);
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
