using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.PartManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.DecoderStructure;

namespace CTSTools.BLL.Features.Engineering.ComponentID.PartManagement.Part_Attribute;

public class Part_Attribute_Repository
{
    public static List<Part_AttributeDTO> GetPart_AttributeList(Part_AttributeDTO Part_AttributeDTO, PagedResultDTO<Part_AttributeDTO> PagedResultDTO = null)
    {
        var _part_attributeList = new List<Part_AttributeDTO>();
        try
        {
            // Part Filters
            var _groupOperator = Part_Attribute_DXFilter.GetPart_Attribute_DXFilter(Part_AttributeDTO);
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
            var _part_attributeCollection = new XPCollection<Part_AttributeXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(Part_AttributeXPO.Oid), SortingDirection.Ascending))
            };

            if (_part_attributeCollection.AsQueryable().Count() > 0)
            {
                _part_attributeList = _part_attributeCollection.Select(Part_AttributeXPO => Part_AttributeMap.XPOToDTO(Part_AttributeXPO)).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _part_attributeList;
    }
    public static Part_AttributeDTO GetPart_AttributeByID(int PartID)
    {
        var _part_AttributeDTO = new Part_AttributeDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _part_AttributeXPO = _unit.GetObjectByKey<Part_AttributeXPO>(PartID);
            if (_part_AttributeXPO != null)
                _part_AttributeDTO = Part_AttributeMap.XPOToDTO(_part_AttributeXPO);

        }
        catch (Exception ex) 
        {
            throw ex;
        }
        return _part_AttributeDTO;
    }
    public static int GetPart_AttributeCount(Part_AttributeDTO Part_AttributeDTO, PagedResultDTO<Part_AttributeDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Part_Attribute_DXFilter.GetPart_Attribute_DXFilter(Part_AttributeDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<PartXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreatePart_Attribute(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _part_AttributeXPO = Part_AttributeMap.DTOtoXPO(Part_AttributeDTO, _unit);
            _unit.Save(_part_AttributeXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _part_AttributeXPO.Oid;
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
    public static ValidationResultDTO UpdatePart_Attribute(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _part_AttributeXPO = Part_AttributeMap.DTOtoXPO(Part_AttributeDTO, _unit);
            _unit.Save(_part_AttributeXPO);
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
    public static ValidationResultDTO DeletePart_Attribute(Part_AttributeDTO Part_AttributeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _part_AttributeXPO = Part_AttributeMap.DTOtoXPO(Part_AttributeDTO, _unit);
            _unit.Delete(_part_AttributeXPO);
            _unit.CommitChanges();
            _unit.PurgeDeletedObjects();

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

    public static ValidationResultDTO CreateMultiplePart_Attribute(List<Part_AttributeDTO> Part_AttributeList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _decoderStructureXPOList = Part_AttributeMap.DTOListToXPOList(Part_AttributeList, _unit);
            _unit.Save(_decoderStructureXPOList);
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
