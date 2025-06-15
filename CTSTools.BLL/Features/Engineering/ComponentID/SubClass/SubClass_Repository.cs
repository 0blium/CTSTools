using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.SubClass;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SubClass
{
    public class SubClass_Repository
    {
        public static List<SubClassDTO> GetSubClassList(SubClassDTO SubClassDTO, PagedResultDTO<SubClassDTO> PagedResultDTO = null)
        {
            var _SubClassList = new List<SubClassDTO>();
            try
            {
                // SubClass Filters
                var _groupOperator = SubClass_DXFilter.GetSubClass_DXFilter(SubClassDTO);
                var _sortProperty = new SortProperty();
                if (PagedResultDTO?.SortPropertyName != null)
                {
                    //Sorting
                    _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
                }
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }
                using var _session = XPO_Helper.GetNewSession();
                var _SubClassCollection = new XPCollection<SubClassXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SubClassXPO.Oid), SortingDirection.Ascending))
                };

                if (_SubClassCollection.AsQueryable().Count() > 0)
                {
                    _SubClassList = _SubClassCollection.Select(SubClassXPO => SubClassMap.XPOToDTO(SubClassXPO)).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _SubClassList;
        }
        public static SubClassDTO GetSubClassByID(int SubClassID)
        {
            var _SubClassDTO = new SubClassDTO();
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _SubClassXPO = _unit.GetObjectByKey<SubClassXPO>(SubClassID);
                if (_SubClassXPO != null)
                    _SubClassDTO = SubClassMap.XPOToDTO(_SubClassXPO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _SubClassDTO;
        }
        public static int GetSubClassCount(SubClassDTO SubClassDTO, PagedResultDTO<SubClassDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = SubClass_DXFilter.GetSubClass_DXFilter(SubClassDTO);
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<SubClassXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreateSubClass(SubClassDTO SubClassDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _SubClassXPO = SubClassMap.DTOtoXPO(SubClassDTO, _unit);
                _unit.Save(_SubClassXPO);
                _unit.CommitChanges();
                _validationResultDTO.Data = _SubClassXPO.Oid;
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
        public static ValidationResultDTO UpdateSubClass(SubClassDTO SubClassDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _SubClassXPO = SubClassMap.DTOtoXPO(SubClassDTO, _unit);
                    _unit.Save(_SubClassXPO);
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
        public static ValidationResultDTO DeleteSubClass(SubClassDTO SubClassDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _SubClassXPO = SubClassMap.DTOtoXPO(SubClassDTO, _unit);
                    _unit.Delete(_SubClassXPO);
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
