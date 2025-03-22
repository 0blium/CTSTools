using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroupMember
{
    public class SupportGroupMember_Repository
    {
        public static List<SupportGroupMemberDTO> GetSupportGroupMemberList(SupportGroupMemberDTO SupportGroupMemberDTO, PagedResultDTO<SupportGroupMemberDTO> PagedResultDTO = null)
        {
            var _supportgroupmemberList = new List<SupportGroupMemberDTO>();
            try
            {
                // SupportGroupMember Filters
                var _groupOperator = SupportGroupMember_DXFilter.GetSupportGroupMember_DXFilter(SupportGroupMemberDTO);
                var _sortProperty = new SortProperty();
                if (PagedResultDTO?.SortPropertyName != null)
                {
                    //Sorting
                    _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
                }
                if ( PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }

                using (var _session = XPO_Helper.GetNewSession())
                {
                    var _supportgroupmemberCollection = new XPCollection<SupportGroupMemberXPO>(_session, _groupOperator, _sortProperty)
                    {
                        TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                        SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                        Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SupportGroupMemberXPO.Oid), SortingDirection.Ascending))
                    };

                    if (_supportgroupmemberCollection.AsQueryable().Count() > 0)
                    {
                        _supportgroupmemberList = _supportgroupmemberCollection.Select(SupportGroupMemberXPO => SupportGroupMemberMap.XPOToDTO(SupportGroupMemberXPO)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _supportgroupmemberList;
        }
        public static int GetSupportGroupMemberCount(SupportGroupMemberDTO SupportGroupMemberDTO, PagedResultDTO<SupportGroupMemberDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = SupportGroupMember_DXFilter.GetSupportGroupMember_DXFilter(SupportGroupMemberDTO);
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<SupportGroupMemberXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreateSupportGroupMember(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _supportgroupmemberXPO = SupportGroupMemberMap.DTOtoXPO(SupportGroupMemberDTO, _unit);
                    _unit.Save(_supportgroupmemberXPO);
                    _unit.CommitChanges();
                    SupportGroupMemberDTO.ID = _supportgroupmemberXPO.Oid;
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
        public static ValidationResultDTO UpdateSupportGroupMember(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _supportgroupmemberXPO = SupportGroupMemberMap.DTOtoXPO(SupportGroupMemberDTO, _unit);
                    _unit.Save(_supportgroupmemberXPO);
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
        public static ValidationResultDTO DeleteSupportGroupMember(SupportGroupMemberDTO SupportGroupMemberDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _supportgroupmemberXPO = SupportGroupMemberMap.DTOtoXPO(SupportGroupMemberDTO, _unit);
                    _unit.Delete(_supportgroupmemberXPO);
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
