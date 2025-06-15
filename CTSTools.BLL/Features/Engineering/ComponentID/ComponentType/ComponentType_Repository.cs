using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.ComponentType;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.ComponentType
{
    public class ComponentType_Repository
    {
        public static List<ComponentTypeDTO> GetComponentTypeList(ComponentTypeDTO ComponentTypeDTO, PagedResultDTO<ComponentTypeDTO> PagedResultDTO = null)
        {
            var _componentTypeList = new List<ComponentTypeDTO>();
            try
            {
                // ComponentType Filters
                var _groupOperator = ComponentType_DXFilter.GetComponentType_DXFilter(ComponentTypeDTO);
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
                var _componentTypeCollection = new XPCollection<ComponentTypeXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(ComponentTypeXPO.Oid), SortingDirection.Ascending))
                };

                if (_componentTypeCollection.AsQueryable().Count() > 0)
                {
                    _componentTypeList = _componentTypeCollection.Select(ComponentTypeXPO => ComponentTypeMap.XPOToDTO(ComponentTypeXPO)).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _componentTypeList;
        }
        public static ComponentTypeDTO GetComponentTypeByID(int ComponentTypeID)
        {
            var _ComponentTypeDTO = new ComponentTypeDTO();
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _ComponentTypeXPO = _unit.GetObjectByKey<ComponentTypeXPO>(ComponentTypeID);
                if (_ComponentTypeXPO != null)
                    _ComponentTypeDTO = ComponentTypeMap.XPOToDTO(_ComponentTypeXPO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ComponentTypeDTO;
        }
        public static int GetComponentTypeCount(ComponentTypeDTO ComponentTypeDTO, PagedResultDTO<ComponentTypeDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = ComponentType_DXFilter.GetComponentType_DXFilter(ComponentTypeDTO);
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<ComponentTypeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreateComponentType(ComponentTypeDTO ComponentTypeDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _ComponentTypeXPO = ComponentTypeMap.DTOtoXPO(ComponentTypeDTO, _unit);
                _unit.Save(_ComponentTypeXPO);
                _unit.CommitChanges();
                _validationResultDTO.Data = _ComponentTypeXPO.Oid;
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
        public static ValidationResultDTO UpdateComponentType(ComponentTypeDTO ComponentTypeDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _ComponentTypeXPO = ComponentTypeMap.DTOtoXPO(ComponentTypeDTO, _unit);
                    _unit.Save(_ComponentTypeXPO);
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
        public static ValidationResultDTO DeleteComponentType(ComponentTypeDTO ComponentTypeDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _ComponentTypeXPO = ComponentTypeMap.DTOtoXPO(ComponentTypeDTO, _unit);
                    _unit.Delete(_ComponentTypeXPO);
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
