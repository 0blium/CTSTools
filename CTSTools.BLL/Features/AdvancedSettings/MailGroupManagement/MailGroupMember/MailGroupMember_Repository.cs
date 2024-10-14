using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.MailGroups.MailGroupMember
{
    public class MailGroupMember_Repository
    {
        public static List<MailGroupMemberDTO> GetMailGroupMemberList(MailGroupMemberDTO MailGroupMemberDTO, PagedResultDTO<MailGroupMemberDTO> PagedResultDTO = null)
        {
            var _mailgroupmemberList = new List<MailGroupMemberDTO>();
            try
            {
                // MailGroupMember Filters
                var _groupOperator = MailGroupMember_DXFilter.GetMailGroupMember_DXFilter(MailGroupMemberDTO);
                var _sortProperty = new SortProperty();
                if (PagedResultDTO?.SortPropertyName != null)
                {
                    //Sorting
                    _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
                }
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
                }

                using (var _session = XPO_Helper.GetNewSession())
                {
                    var _mailgroupmemberCollection = new XPCollection<MailGroupMemberXPO>(_session, _groupOperator, _sortProperty)
                    {
                        TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                        SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                        Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(MailGroupMemberXPO.Oid), SortingDirection.Ascending))
                    };

                    if (_mailgroupmemberCollection.AsQueryable().Count() > 0)
                    {
                        _mailgroupmemberList = _mailgroupmemberCollection.Select(MailGroupMemberXPO => MailGroupMemberMap.XPOToDTO(MailGroupMemberXPO)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _mailgroupmemberList;
        }
        public static int GetMailGroupMemberCount(MailGroupMemberDTO MailGroupMemberDTO, PagedResultDTO<MailGroupMemberDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = MailGroupMember_DXFilter.GetMailGroupMember_DXFilter(MailGroupMemberDTO);
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<MailGroupMemberXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreateMailGroupMember(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _mailgroupmemberXPO = MailGroupMemberMap.DTOtoXPO(MailGroupMemberDTO, _unit);
                    _unit.Save(_mailgroupmemberXPO);
                    _unit.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
            }
            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdateMailGroupMember(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _mailgroupmemberXPO = MailGroupMemberMap.DTOtoXPO(MailGroupMemberDTO, _unit);
                    _unit.Save(_mailgroupmemberXPO);
                    _unit.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
            }
            return _validationResultDTO;
        }
        public static ValidationResultDTO DeleteMailGroupMember(MailGroupMemberDTO MailGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _mailgroupmemberXPO = MailGroupMemberMap.DTOtoXPO(MailGroupMemberDTO, _unit);
                    _unit.Delete(_mailgroupmemberXPO);
                    _unit.CommitChanges();
                    _unit.PurgeDeletedObjects();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
            }
            return _validationResultDTO;
        }              
    }
}
