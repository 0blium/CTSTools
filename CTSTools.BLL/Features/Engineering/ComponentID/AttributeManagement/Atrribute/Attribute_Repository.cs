using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Attribute;
public class Attribute_Repository
{
    public static List<AttributeDTO> GetAttributeList(AttributeDTO AttributeDTO, PagedResultDTO<AttributeDTO> PagedResultDTO = null)
    {
        var _attributeList = new List<AttributeDTO>();
        try
        {
            // Attribute Filters
            var _groupOperator = Attribute_DXFilter.GetAttribute_DXFilter(AttributeDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            if (PagedResultDTO?.dxFilters != null)
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            using var _session = XPO_Helper.GetNewSession();
            var _attributeCollection = new XPCollection<AttributeXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(AttributeXPO.Oid), SortingDirection.Ascending))
            };

            if (_attributeCollection.AsQueryable().Count() > 0)
            {
                _attributeList = _attributeCollection.Select(AttributeXPO => AttributeMap.XPOToDTO(AttributeXPO)).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _attributeList;
    }
    public static AttributeDTO GetAttributeByID(int AttributeID)
    {
        var _attributeDTO = new AttributeDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _attributeXPO = _unit.GetObjectByKey<AttributeXPO>(AttributeID);
            if (_attributeXPO != null)
                _attributeDTO = AttributeMap.XPOToDTO(_attributeXPO);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _attributeDTO;
    }
    public static int GetAttributeCount(AttributeDTO AttributeDTO, PagedResultDTO<AttributeDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Attribute_DXFilter.GetAttribute_DXFilter(AttributeDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<AttributeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateAttribute(AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _attributeXPO = AttributeMap.DTOtoXPO(AttributeDTO, _unit);
            _unit.Save(_attributeXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _attributeXPO.Oid;
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
    public static ValidationResultDTO UpdateAttribute(AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _attributeXPO = AttributeMap.DTOtoXPO(AttributeDTO, _unit);
                _unit.Save(_attributeXPO);
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
    public static ValidationResultDTO DeleteAttribute(AttributeDTO AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _attributeXPO = AttributeMap.DTOtoXPO(AttributeDTO, _unit);
                _unit.Delete(_attributeXPO);
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
    public static ValidationResultDTO CreateMultipleAttribute(List<AttributeDTO> AttributeDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _attributeXPOList = AttributeMap.DTOListToXPOList(AttributeDTOList, _unit);
            _unit.Save(_attributeXPOList);
            _unit.CommitChanges();

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
