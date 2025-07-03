using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.PartType;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartType
{
    public class PartType_Repository
    {
        public static List<PartTypeDTO> GetPartTypeList(PartTypeDTO PartTypeDTO, PagedResultDTO<PartTypeDTO> PagedResultDTO = null)
        {
            var _PartTypeList = new List<PartTypeDTO>();
            try
            {
                // PartType Filters
                var _groupOperator = PartType_DXFilter.GetPartType_DXFilter(PartTypeDTO);
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
                var _PartTypeCollection = new XPCollection<PartTypeXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(PartTypeXPO.Oid), SortingDirection.Ascending))
                };

                if (_PartTypeCollection.AsQueryable().Count() > 0)
                {
                    _PartTypeList = _PartTypeCollection.Select(PartTypeXPO => PartTypeMap.XPOToDTO(PartTypeXPO)).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _PartTypeList;
        }
        public static PartTypeDTO GetPartTypeByID(int PartTypeID)
        {
            var _PartTypeDTO = new PartTypeDTO();
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _PartTypeXPO = _unit.GetObjectByKey<PartTypeXPO>(PartTypeID);
                if (_PartTypeXPO != null)
                    _PartTypeDTO = PartTypeMap.XPOToDTO(_PartTypeXPO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _PartTypeDTO;
        }
        public static int GetPartTypeCount(PartTypeDTO PartTypeDTO, PagedResultDTO<PartTypeDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = PartType_DXFilter.GetPartType_DXFilter(PartTypeDTO);
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<PartTypeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreatePartType(PartTypeDTO PartTypeDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _PartTypeXPO = PartTypeMap.DTOtoXPO(PartTypeDTO, _unit);
                _unit.Save(_PartTypeXPO);
                _unit.CommitChanges();
                _validationResultDTO.Data = _PartTypeXPO.Oid;
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
        public static ValidationResultDTO UpdatePartType(PartTypeDTO PartTypeDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _PartTypeXPO = PartTypeMap.DTOtoXPO(PartTypeDTO, _unit);
                    _unit.Save(_PartTypeXPO);
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
        public static ValidationResultDTO DeletePartType(PartTypeDTO PartTypeDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _PartTypeXPO = PartTypeMap.DTOtoXPO(PartTypeDTO, _unit);
                    _unit.Delete(_PartTypeXPO);
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
